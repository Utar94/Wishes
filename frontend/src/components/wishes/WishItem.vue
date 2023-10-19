<script setup lang="ts">
import { type RouteLocationRaw } from "vue-router";
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import type { Item } from "@/types/wishes";

const { n } = useI18n();

const props = defineProps<{
  item: Item;
  listId: string;
}>();

const to = computed<RouteLocationRaw>(() => ({ name: "WishView", params: { listId: props.listId, itemId: props.item.id } }));
</script>

<template>
  <div>
    <app-card :picture="item.pictureUrl" :text="item.summary" :title="item.displayName" :to="to">
      <icon-button icon="fas fa-circle-info" text="wishes.viewDetail" :to="to" />
      <template #subtitle v-if="item.price || item.rankCategory">
        <h6 class="card-subtitle mb-2 text-body-secondary">
          <font-awesome-icon v-for="index in item.price?.category ?? 0" :key="index" class="me-1" icon="fas fa-dollar-sign" />
          <template v-if="item.price">
            <template v-if="item.price.minimum === item.price.maximum">{{ n(item.price.average, "decimal") }}</template>
            <template v-else>{{ n(item.price.minimum, "decimal") }} &mdash; {{ n(item.price.maximum, "decimal") }}</template>
          </template>
          <template v-if="item.price && item.rankCategory"> | </template>
          <font-awesome-icon v-for="index in item.rankCategory" :key="index" class="me-1" icon="fas fa-heart" />
          <template v-if="item.rank">({{ item.rank }})</template>
        </h6>
      </template>
    </app-card>
  </div>
</template>
