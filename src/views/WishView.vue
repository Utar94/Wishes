<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";

import type { ContentType, Wish, Wishlist } from "@/types/wishes";
import wishlists from "@/resources/wishlists.json";

const route = useRoute();
const router = useRouter();
const { n, t } = useI18n();

const wish = ref<Wish>();

const contentType = computed<ContentType | undefined>(() => (wish.value?.contents ? wish.value.contents.type ?? "text/plain" : undefined));

onMounted(() => {
  const wishlist: Wishlist | undefined = wishlists.find(({ id }) => id === route.params.listId) as Wishlist;
  if (wishlist) {
    wish.value = wishlist.items.find(({ id }) => id === route.params.itemId);
  }
  if (!wish.value) {
    router.push({ path: "/not-found" });
  }
});
</script>

<template>
  <main class="container">
    <template v-if="wish">
      <h1>
        <app-avatar :display-name="wish.title" :url="wish.picture" />
        {{ wish.title }}
        <app-badge v-if="wish.price && wish.price.length >= 1 && wish.price.length <= 2" variant="primary">
          <font-awesome-icon icon="fas fa-dollar-sign" /> {{ n(wish.price[0], "decimal") }}
          <template v-if="wish.price.length === 2">&mdash; {{ n(wish.price[1], "decimal") }}</template>
        </app-badge>
      </h1>
      <div class="row">
        <section class="col-lg-8">
          <div class="mb-3">
            <icon-button
              icon="fas fa-chevron-left"
              text="actions.back"
              :to="{ name: 'WishlistView', params: { id: route.params.listId } }"
              variant="secondary"
            />
          </div>
          <template v-if="wish.contents">
            <div v-if="contentType === 'text/plain'" v-text="wish.contents.text"></div>
            <div v-else-if="contentType === 'text/html'" v-html="wish.contents.text"></div>
            <markdown-block v-else-if="contentType === 'text/markdown'" :contents="wish.contents.text" />
          </template>
          <template v-if="wish.links?.length">
            <h2>{{ t("wishes.externalLinks") }}</h2>
            <ul>
              <li v-for="(link, index) in wish.links" :key="index">
                <a :href="link" target="_blank">{{ link }}</a>
              </li>
            </ul>
          </template>
        </section>
        <section class="col-lg-4">
          <app-carousel v-if="wish.gallery?.length" :alt="wish.title" :pictures="wish.gallery" />
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
