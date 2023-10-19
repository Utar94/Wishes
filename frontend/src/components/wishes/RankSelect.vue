<script setup lang="ts">
import { computed } from "vue";
import { useI18n } from "vue-i18n";

import type { SelectOption } from "@/types/components";
import { orderBy } from "@/helpers/arrayUtils";

const { rt, tm } = useI18n();

withDefaults(
  defineProps<{
    disabled?: boolean;
    id?: string;
    label?: string;
    modelValue?: string;
    name?: string;
    noLabel?: boolean;
    noState?: boolean;
    placeholder?: string;
    required?: boolean;
  }>(),
  {
    disabled: false,
    id: "rank",
    label: "wishes.rank.label",
    noLabel: false,
    noState: false,
    placeholder: "wishes.rank.placeholder",
    required: false,
  }
);

const options = computed<SelectOption[]>(() =>
  orderBy(
    Object.entries(tm(rt("wishes.rank.options"))).map(([value, text]) => ({ text, value } as SelectOption)),
    "text"
  )
);
</script>

<template>
  <form-select
    :disabled="disabled"
    :id="id"
    :label="label"
    :model-value="modelValue"
    :name="name"
    :no-label="noLabel"
    :no-state="noState"
    :options="options"
    :placeholder="placeholder"
    :required="required"
    @update:model-value="$emit('update:model-value', $event)"
  >
  </form-select>
</template>
