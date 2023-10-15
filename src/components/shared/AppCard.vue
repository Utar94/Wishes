<script setup lang="ts">
withDefaults(
  defineProps<{
    picture?: string;
    subtitle?: string;
    text?: string;
    title: string;
  }>(),
  {}
);
</script>

<template>
  <div class="card">
    <img v-if="picture" :src="picture" class="card-img-top" alt="..." />
    <div class="card-body">
      <h5 class="card-title">{{ title }}</h5>
      <h6 v-if="subtitle" class="card-subtitle mb-2 text-body-secondary">{{ subtitle }}</h6>
      <p v-if="text" class="card-text">{{ text }}</p>
      <a href="#" class="btn btn-primary">Go somewhere</a>
    </div>
  </div>
</template>
