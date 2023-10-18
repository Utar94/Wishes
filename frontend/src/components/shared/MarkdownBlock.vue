<script setup lang="ts">
import MarkdownIt from "markdown-it";
import { computed } from "vue";

const markdown = new MarkdownIt();

const props = defineProps<{
  contents: string;
}>();

const rendered = computed<string>(() => markdown.render(props.contents));
</script>

<template>
  <div v-html="rendered"></div>
</template>
