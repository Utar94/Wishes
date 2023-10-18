<script setup lang="ts">
import AppBreadcrumb from "./AppBreadcrumb.vue";
import type { BreadcrumbOptions } from "@/types/components";

defineProps<{
  breadcrumbs: BreadcrumbOptions[];
}>();
</script>

<template>
  <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
      <AppBreadcrumb v-for="(breadcrumb, index) in breadcrumbs" :key="index" :breadcrumb="breadcrumb" :last="index === breadcrumbs.length - 1" />
    </ol>
  </nav>
</template>

<style scoped>
nav {
  --bs-breadcrumb-divider: "›";
}
</style>
