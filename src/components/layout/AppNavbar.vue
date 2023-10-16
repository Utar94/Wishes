<script setup lang="ts">
import { computed, ref, watchEffect } from "vue";
import { RouterLink, useRouter } from "vue-router";
import { useI18n } from "vue-i18n";
import { setLocale } from "@vee-validate/i18n";
import locales from "@/resources/locales.json";
import type { Locale } from "@/types/i18n";
import { orderBy } from "@/helpers/arrayUtils";
import { urlCombine } from "@/helpers/stringUtils";
import { useI18nStore } from "@/stores/i18n";

const { availableLocales, locale, t } = useI18n();
const apiBaseUrl: string = import.meta.env.VITE_APP_API_BASE_URL;

const i18n = useI18nStore();
const router = useRouter();

const props = defineProps<{
  environment: string;
}>();

const search = ref<string>("");

const environmentName = computed<string>(() => props.environment.toLowerCase());
const otherLocales = computed<Locale[]>(() => {
  const otherLocales = new Set<string>(availableLocales.filter((item) => item !== locale.value));
  return orderBy(
    locales.filter(({ code }) => otherLocales.has(code)),
    "nativeName"
  );
});
const swaggerUrl = computed<string | undefined>(() => (environmentName.value === "development" ? urlCombine(apiBaseUrl, "/swagger") : undefined));

function onSearch(): void {
  const query = { search: search.value, page: 1, count: 10 };
  search.value = "";
  router.push({ name: "RealmList", query });
}

watchEffect(() => {
  if (i18n.locale) {
    locale.value = i18n.locale.code;
    setLocale(i18n.locale.code);
  } else {
    const currentLocale = locales.find(({ code }) => code === locale.value);
    if (!currentLocale) {
      throw new Error(`The locale "${locale.value}" is not supported.'`);
    }
    i18n.setLocale(currentLocale);
  }
});
</script>

<template>
  <nav class="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
    <div class="container-fluid">
      <RouterLink :to="{ name: 'Home' }" class="navbar-brand">
        <img src="@/assets/img/logo.png" :alt="`${t('brand')} Logo`" height="32" />
        {{ t("brand") }}
        <span v-if="environmentName !== 'production'" class="badge text-bg-warning">{{ environmentName }}</span>
      </RouterLink>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li v-if="swaggerUrl" class="nav-item">
            <a class="nav-link" :href="swaggerUrl" target="_blank"> <font-awesome-icon icon="fas fa-vial" /> Swagger</a>
          </li>
        </ul>

        <ul class="navbar-nav mb-2 mb-lg-0">
          <template v-if="i18n.locale">
            <li v-if="otherLocales.length > 1" class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">{{ i18n.locale.nativeName }}</a>
              <ul class="dropdown-menu dropdown-menu-end">
                <li v-for="option in otherLocales" :key="option.code">
                  <a class="dropdown-item" href="#" @click.prevent="i18n.setLocale(option)">{{ option.nativeName }}</a>
                </li>
              </ul>
            </li>
            <li v-else-if="otherLocales.length === 1" class="nav-item">
              <a class="nav-link" href="#" @click.prevent="i18n.setLocale(otherLocales[0])">{{ otherLocales[0].nativeName }}</a>
            </li>
          </template>
        </ul>
      </div>
    </div>
  </nav>
</template>
