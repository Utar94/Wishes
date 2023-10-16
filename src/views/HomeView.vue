<script setup lang="ts">
import { useI18n } from "vue-i18n";

const { t } = useI18n();
</script>

<template>
  <main class="container">
    <h1>{{ t("home.title") }}</h1>
    <img src="@/assets/img/en-construction.png" alt="Page en construction" />
  </main>
</template>
