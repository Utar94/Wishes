<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import WishlistCard from "@/components/wishes/WishlistCard.vue";
import type { BreadcrumbOptions } from "@/types/components";
import type { Wishlist } from "@/types/wishes";
import wishlists from "@/resources/wishlists.json";
import { orderBy } from "@/helpers/arrayUtils";

const { t } = useI18n();

const breadcrumbs = computed<BreadcrumbOptions[]>(() => [
  {
    to: { name: "Home" },
    text: t("home.title"),
  },
]);
const sorted = computed<Wishlist[]>(() => orderBy(wishlists, "displayName") as Wishlist[]);
</script>

<template>
  <main class="container">
    <h1>
      <img src="@/assets/img/logo.png" alt="Wishlist Home" height="32" />
      {{ t("home.title") }}
    </h1>
    <app-breadbar :breadcrumbs="breadcrumbs" />
    <div class="row">
      <WishlistCard v-for="wishlist in sorted" :key="wishlist.id" class="col-lg-6 col-xl-4 mb-3" :wishlist="wishlist" />
    </div>
  </main>
</template>
