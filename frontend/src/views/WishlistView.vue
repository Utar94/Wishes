<script setup lang="ts">
import { computed, inject, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute, useRouter } from "vue-router";

import RankSelect from "@/components/wishes/RankSelect.vue";
import WishItem from "@/components/wishes/WishItem.vue";
import type { ApiError } from "@/types/api";
import type { BreadcrumbOptions, SelectOption } from "@/types/components";
import type { Item, Wishlist } from "@/types/wishes";
import { handleErrorKey } from "@/inject/App";
import { isMobile } from "@/helpers/deviceUtils";
import { orderBy } from "@/helpers/arrayUtils";
import { readWishlist } from "@/api/wishlists";

const handleError = inject(handleErrorKey) as (e: unknown) => void;
const route = useRoute();
const router = useRouter();
const { rt, t, tm } = useI18n();

const isDescending = ref<boolean>(false);
const rankCategory = ref<string>("");
const search = ref<string>("");
const sort = ref<string>("rank");
const wishlist = ref<Wishlist>();

const breadcrumbs = computed<BreadcrumbOptions[]>(() => [
  {
    to: { name: "Home" },
    text: t("home.title"),
  },
  {
    to: route,
    text: title.value,
  },
]);
const items = computed<Item[]>(() => {
  let items = wishlist.value?.items ?? [];
  if (search.value) {
    const searchText = search.value.toLowerCase();
    items = items.filter(({ displayName, summary }) => displayName.toLowerCase().includes(searchText) || summary?.includes(searchText));
  }
  if (rankCategory.value) {
    items = items.filter((item) => item.rankCategory === Number(rankCategory.value));
  }
  if (sort.value === "averagePrice") {
    items = orderBy(
      items.map((item) => ({ item, averagePrice: item.price ? [item.price.category, item.price.average].join("_") : undefined })),
      sort.value,
      isDescending.value
    ).map(({ item }) => item);
  } else {
    items = orderBy(items, sort.value as keyof Item, isDescending.value);
  }
  return items;
});
const maximumPrice = computed<number>(
  () =>
    (wishlist.value?.items
      .map(({ price }) => price?.maximum ?? 0)
      .filter((value) => value > 0)
      .sort()
      .reverse() ?? [])[0]
);
const minimumPrice = computed<number>(
  () =>
    (wishlist.value?.items
      .map(({ price }) => price?.minimum ?? 0)
      .filter((value) => value > 0)
      .sort() ?? [])[0]
);
const sortOptions = computed<SelectOption[]>(() =>
  orderBy(
    Object.entries(tm(rt("wishes.sort.options"))).map(([value, text]) => ({ text, value } as SelectOption)),
    "text"
  )
);
const title = computed<string>(() => t("wishes.title", { displayName: wishlist.value?.displayName }));

onMounted(async () => {
  try {
    const id = route.params.id.toString();
    wishlist.value = await readWishlist(id);
  } catch (e: unknown) {
    const { status } = e as ApiError;
    if (status === 404) {
      router.push({ path: "/not-found" });
    } else {
      handleError(e);
    }
  }
});
</script>

<template>
  <main class="container">
    <template v-if="wishlist">
      <h1><app-avatar :display-name="wishlist.displayName" :url="wishlist.pictureUrl" /> {{ title }}</h1>
      <app-breadbar :breadcrumbs="breadcrumbs" />
      <div class="mb-3">
        <icon-button icon="fas fa-chevron-left" text="actions.back" :to="{ name: 'Home' }" variant="secondary" />
      </div>
      <app-accordion v-if="wishlist.items.length" id="filters" class="mb-3">
        <app-accordion-item :active="!isMobile()" title="wishes.filters">
          <div class="row">
            <div class="col-lg-6 col-xl-4">
              <search-input v-model="search" />
            </div>
            <div class="col-lg-6 col-xl-4">
              <sort-select :descending="isDescending" :options="sortOptions" v-model="sort" @descending="isDescending = $event" />
            </div>
            <div class="col-lg-6 col-xl-4">
              <RankSelect no-state v-model="rankCategory" />
            </div>
          </div>
        </app-accordion-item>
      </app-accordion>
      <div class="row">
        <WishItem v-for="item in items" :key="item.id" class="col-lg-6 col-xl-4 mb-3" :item="item" :list-id="wishlist.id" />
      </div>
    </template>
  </main>
</template>
