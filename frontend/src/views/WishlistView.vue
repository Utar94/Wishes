<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";

import WishItem from "@/components/wishes/WishItem.vue";
import type { BreadcrumbOptions } from "@/types/components";
import type { Wish, Wishlist } from "@/types/wishes";
import wishlists from "@/resources/wishlists.json";
import { orderBy } from "@/helpers/arrayUtils";

const route = useRoute();
const router = useRouter();
const { t } = useI18n();

const wishlist = ref<Wishlist>();

const breadcrumbs = computed<BreadcrumbOptions[]>(() => [
  {
    to: { name: "Home" },
    text: t("home.title"),
  },
  {
    to: route,
    text: title.value,
  },
]);
const items = computed<Wish[]>(() => orderBy(wishlist.value?.items ?? [], "title"));
const title = computed<string>(() => t("wishes.title", { displayName: wishlist.value?.displayName }));

onMounted(() => {
  wishlist.value = wishlists.find(({ id }) => id === route.params.id) as Wishlist;
  if (!wishlist.value) {
    router.push({ path: "/not-found" });
  }
});
</script>

<template>
  <main class="container">
    <template v-if="wishlist">
      <h1><app-avatar :display-name="wishlist.displayName" :url="wishlist.picture" /> {{ title }}</h1>
      <app-breadbar :breadcrumbs="breadcrumbs" />
      <div class="mb-3">
        <icon-button icon="fas fa-chevron-left" text="actions.back" :to="{ name: 'Home' }" variant="secondary" />
      </div>
      <div class="row">
        <WishItem v-for="item in items" :key="item.id" class="col-lg-6 col-xl-4 mb-3" :item="item" :list-id="wishlist.id" />
      </div>
    </template>
  </main>
</template>
