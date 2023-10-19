<script setup lang="ts">
import { computed, inject, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { type RouteLocationRaw, useRoute, useRouter } from "vue-router";

import type { BreadcrumbOptions } from "@/types/components";
import type { Item } from "@/types/wishes";
import { readItem } from "@/api/items";
import type { ApiError } from "@/types/api";
import { handleErrorKey } from "@/inject/App";

const handleError = inject(handleErrorKey) as (e: unknown) => void;
const route = useRoute();
const router = useRouter();
const { n, t } = useI18n();

const backRoute = ref<RouteLocationRaw>({ name: "WishlistView", params: { id: route.params.listId } });
const item = ref<Item>();

const breadcrumbs = computed<BreadcrumbOptions[]>(() => [
  {
    to: { name: "Home" },
    text: t("home.title"),
  },
  {
    to: backRoute.value,
    text: t("wishes.title", { displayName: item.value?.wishlist.displayName }),
  },
  {
    to: route,
    text: item.value?.displayName,
  },
]);

onMounted(async () => {
  try {
    const listId = route.params.listId.toString();
    const itemId = route.params.itemId.toString();
    item.value = await readItem(listId, itemId);
  } catch (e: unknown) {
    const { status } = e as ApiError;
    if (status === 404) {
      router.push({ path: "/not-found" });
    } else {
      handleError(e);
    }
  }
});
</script>

<template>
  <main class="container">
    <template v-if="item">
      <h1>
        <app-avatar :display-name="item.displayName" :url="item.pictureUrl" />
        {{ item.displayName }}
        <app-badge v-if="item.price" variant="primary" class="me-2">
          <font-awesome-icon v-for="index in item.price?.category ?? 0" :key="index" class="me-1" icon="fas fa-dollar-sign" />
          <template v-if="item.price.minimum === item.price.maximum">{{ n(item.price.average, "decimal") }}</template>
          <template v-else>{{ n(item.price.minimum, "decimal") }} &mdash; {{ n(item.price.maximum, "decimal") }}</template>
        </app-badge>
        <app-badge v-if="item.rank" variant="danger">
          <font-awesome-icon v-for="index in item.rankCategory" :key="index" class="me-1" icon="fas fa-heart" />
          {{ t("wishes.rank.format", { rank: item.rank }) }}
        </app-badge>
      </h1>
      <app-breadbar :breadcrumbs="breadcrumbs" />
      <div class="row">
        <section class="col-lg-8">
          <div class="mb-3">
            <icon-button icon="fas fa-chevron-left" text="actions.back" :to="backRoute" variant="secondary" />
          </div>
          <template v-if="item.contents">
            <div v-if="item.contents.type === 'text/plain'" v-text="item.contents.text"></div>
            <div v-else-if="item.contents.type === 'text/html'" v-html="item.contents.text"></div>
            <markdown-block v-else-if="item.contents.type === 'text/markdown'" :contents="item.contents.text" />
          </template>
          <template v-if="item.links?.length">
            <h2>{{ t("wishes.externalLinks") }}</h2>
            <ul>
              <li v-for="(link, index) in item.links" :key="index">
                <a :href="link" target="_blank">{{ link }}</a>
              </li>
            </ul>
          </template>
        </section>
        <section class="col-lg-4">
          <app-carousel v-if="item.gallery?.length" :alt="item.displayName" :pictures="item.gallery" />
        </section>
      </div>
    </template>
  </main>
</template>

<style scoped>
img {
  width: 100%;
  max-width: 400px;
}
</style>
