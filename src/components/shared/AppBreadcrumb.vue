<script setup lang="ts">
import { computed } from "vue";

import type { BreadcrumbOptions } from "@/types/components";

const props = withDefaults(
  defineProps<{
    breadcrumb: BreadcrumbOptions;
    last?: boolean;
  }>(),
  {
    last: false,
  }
);

const classes = computed<string[]>(() => {
  const classes = ["breadcrumb-item"];
  if (props.last) {
    classes.push("active");
  }
  return classes;
});
const text = computed<string>(() => props.breadcrumb.text ?? props.breadcrumb.to.toString());
</script>

<template>
  <li :class="classes" :aria-current="last ? 'page' : undefined">
    <template v-if="last">{{ text }}</template>
    <RouterLink :to="breadcrumb.to" :target="breadcrumb.target" v-else>{{ text }}</RouterLink>
  </li>
</template>
