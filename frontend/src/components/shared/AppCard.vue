<script setup lang="ts">
import type { RouteLocationRaw } from "vue-router";

defineProps<{
  picture?: string;
  subtitle?: string;
  text?: string;
  title: string;
  to?: RouteLocationRaw;
}>();
</script>

<template>
  <div class="card">
    <template v-if="picture">
      <RouterLink v-if="to" :to="to">
        <img :src="picture" class="card-img-top" :alt="title" />
      </RouterLink>
      <img v-else :src="picture" class="card-img-top" :alt="title" />
    </template>
    <div class="card-body">
      <h5 class="card-title">{{ title }}</h5>
      <h6 v-if="subtitle" class="card-subtitle mb-2 text-body-secondary">{{ subtitle }}</h6>
      <p v-if="text" class="card-text">{{ text }}</p>
      <slot></slot>
    </div>
  </div>
</template>
