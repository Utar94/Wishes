<script setup lang="ts">
import { type RouteLocationRaw } from "vue-router";
import { computed } from "vue";

import type { ButtonVariant } from "@/types/components";
import type { Wish } from "@/types/wishes";

const props = defineProps<{
  item: Wish;
  listId: string;
}>();

const icon = computed<string>(() => (props.item.incomplete ? "fas fa-triangle-exclamation" : "fas fa-circle-info"));
const to = computed<RouteLocationRaw>(() => ({ name: "WishView", params: { listId: props.listId, itemId: props.item.id } }));
const variant = computed<ButtonVariant>(() => (props.item.incomplete ? "warning" : "primary"));
</script>

<template>
  <div>
    <app-card :picture="item.picture" :text="item.summary" :title="item.title" :to="to">
      <icon-button :icon="icon" text="wishes.viewDetail" :to="to" :variant="variant" />
    </app-card>
  </div>
</template>
