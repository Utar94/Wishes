<script setup lang="ts">
import type { RouteLocationRaw } from "vue-router";
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import type { Wishlist } from "@/types/wishes";

const { t } = useI18n();

const props = defineProps<{
  wishlist: Wishlist;
}>();

const to = computed<RouteLocationRaw>(() => ({ name: "WishlistView", params: { id: props.wishlist.id } }));
</script>

<template>
  <div>
    <app-card :picture="wishlist.pictureUrl ?? '/img/guest.png'" :subtitle="t('wishes.items', wishlist.itemCount)" :title="wishlist.displayName" :to="to">
      <icon-button icon="fas fa-gifts" text="wishes.viewWishlist" :to="to" />
    </app-card>
  </div>
</template>
