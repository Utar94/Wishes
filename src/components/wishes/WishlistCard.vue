<script setup lang="ts">
import type { RouteLocationRaw } from "vue-router";
import { computed } from "vue";

import type { Wishlist } from "@/types/wishes";

const props = defineProps<{
  wishlist: Wishlist;
}>();

const to = computed<RouteLocationRaw>(() => ({ name: "WishlistView", params: { id: props.wishlist.id } }));
</script>

<template>
  <app-card :picture="wishlist.picture" :title="wishlist.displayName" :to="to">
    <icon-button icon="fas fa-gifts" text="wishes.viewWishlist" :to="to" />
  </app-card>
</template>
