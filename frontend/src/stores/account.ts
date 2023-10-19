import { defineStore } from "pinia";
import { ref } from "vue";

export const useAccountStore = defineStore(
  "account",
  () => {
    const key = ref<string>();

    function signIn(value: string) {
      key.value = value;
    }
    function signOut() {
      key.value = undefined;
    }

    return { key, signIn, signOut };
  },
  { persist: true }
);
