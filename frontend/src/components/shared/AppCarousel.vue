<script setup lang="ts">
import { useI18n } from "vue-i18n";

import CarouselItem from "./CarouselItem.vue";
import type { PictureOptions } from "@/types/components";

const { t } = useI18n();

const props = withDefaults(
  defineProps<{
    alt?: string;
    id?: string;
    pictures: string[] | PictureOptions[];
  }>(),
  {
    id: "carousel",
  }
);

function getAlt(picture: string | PictureOptions): string | undefined {
  return (typeof picture === "string" ? undefined : picture.alt) ?? props.alt;
}
function getSrc(picture: string | PictureOptions): string {
  return typeof picture === "string" ? picture : picture.src;
}
</script>

<template>
  <div :id="id" class="carousel slide" data-bs-ride="carousel">
    <div v-if="pictures.length > 1" class="carousel-indicators">
      <button
        v-for="(_, index) in pictures"
        :key="index"
        type="button"
        :data-bs-target="`#${id}Indicators`"
        :data-bs-slide-to="index"
        :class="{ active: index === 0 }"
        :aria-current="index === 0"
        :aria-label="`Slide ${index + 1}`"
      ></button>
    </div>
    <div class="carousel-inner">
      <CarouselItem v-for="(picture, index) in pictures" :key="index" :active="index === 0" :alt="getAlt(picture)" :src="getSrc(picture)" />
    </div>
    <template v-if="pictures.length > 1">
      <button class="carousel-control-prev" type="button" :data-bs-target="`#${id}`" data-bs-slide="prev">
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="visually-hidden">{{ t("pagination.previous") }}</span>
      </button>
      <button class="carousel-control-next" type="button" :data-bs-target="`#${id}`" data-bs-slide="next">
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="visually-hidden">{{ t("pagination.next") }}</span>
      </button>
    </template>
  </div>
</template>
