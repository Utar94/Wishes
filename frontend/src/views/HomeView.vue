<script setup lang="ts">
import { computed, inject, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute } from "vue-router";

import WishlistCard from "@/components/wishes/WishlistCard.vue";
import type { BreadcrumbOptions } from "@/types/components";
import type { Wishlist } from "@/types/wishes";
import { handleErrorKey } from "@/inject/App";
import { orderBy } from "@/helpers/arrayUtils";
import { searchWishlists } from "@/api/wishlists";
import { useAccountStore } from "@/stores/account";

const account = useAccountStore();
const handleError = inject(handleErrorKey) as (e: unknown) => void;
const route = useRoute();
const { t } = useI18n();

const key = ref<string>("");
const wishlists = ref<Wishlist[]>([]);

const breadcrumbs = computed<BreadcrumbOptions[]>(() => [
  {
    to: { name: "Home" },
    text: t("home.title"),
  },
]);
const sorted = computed<Wishlist[]>(() => orderBy(wishlists.value, "displayName"));

function onClick(): void {
  account.signIn(key.value);
  refresh();
  key.value = "";
}
async function refresh(): Promise<void> {
  try {
    wishlists.value = (await searchWishlists()).items;
  } catch (e: unknown) {
    handleError(e);
  }
}

onMounted(() => {
  const key = route.query.key?.toString();
  if (key) {
    account.signIn(key);
  }
  if (account.key) {
    refresh();
  }
});
</script>

<template>
  <main class="container">
    <h1>
      <img src="@/assets/img/logo.png" alt="Wishlist Home" height="32" />
      {{ t("home.title") }}
    </h1>
    <app-breadbar :breadcrumbs="breadcrumbs" />
    <div v-if="account.key" class="row">
      <WishlistCard v-for="wishlist in sorted" :key="wishlist.id" class="col-lg-6 col-xl-4 mb-3" :wishlist="wishlist" />
    </div>
    <form-input v-else id="key" label="wishes.accessKey.label" no-label placeholder="wishes.accessKey.placeholder" required v-model="key">
      <template #append>
        <icon-button :disabled="!key" icon="fas fa-key" text="actions.refresh" @click="onClick" />
      </template>
    </form-input>
  </main>
</template>
