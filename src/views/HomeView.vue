<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import rawWishes from "@/resources/wishes.json";
import { orderBy } from "@/helpers/arrayUtils";

const { t } = useI18n();

const wishes = computed(() => orderBy(rawWishes, "title")); // TODO(fpion): typing
</script>

<template>
  <main class="container">
    <h1>{{ t("home.title") }}</h1>
    <div class="row">
      <div v-for="wish in wishes" :key="wish.id" class="col-lg-6 col-xl-4 mb-3">
        <app-card
          :picture="wish.pictures.xs"
          :subtitle="wish.subtitle"
          :text="wish.summary"
          :title="wish.title"
          :to="{ name: 'WishView', params: { id: wish.id } }"
        >
          <icon-button icon="fas fa-circle-info" text="home.viewDetail" :to="{ name: 'WishView', params: { id: wish.id } }" />
        </app-card>
      </div>
    </div>
  </main>
</template>
