<script setup lang="ts">
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    active?: boolean;
    alt?: string;
    src: string;
  }>(),
  {
    active: false,
  }
);

const classes = computed<string[]>(() => {
  const classes = ["carousel-item"];
  if (props.active) {
    classes.push("active");
  }
  return classes;
});
</script>

<template>
  <div :class="classes">
    <img :src="src" class="d-block w-100" :alt="alt" />
  </div>
</template>
