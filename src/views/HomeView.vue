<script setup lang="ts">
import { useI18n } from "vue-i18n";

import wishes from "@/resources/wishes.json";

const { t } = useI18n();
</script>

<template>
  <main class="container">
    <h1>Ma liste de souhaits</h1>
    <div class="row">
      <div v-for="wish in wishes" :key="wish.id" class="col-lg-6 col-xl-4 mb-3">
        <app-card :picture="wish.pictures.xs" :subtitle="wish.subtitle" :text="wish.text" :title="wish.title" />
      </div>
    </div>
  </main>
</template>
