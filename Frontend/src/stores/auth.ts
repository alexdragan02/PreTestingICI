import { defineStore } from "pinia";
import { ref, computed } from "vue";

export const useAuthStore = defineStore("auth", () => {
  const user = ref<any>(null);

  try {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      user.value = JSON.parse(storedUser);
    }
  } catch (error) {
    console.error("Error parsing user from localStorage:", error);
  }

  const isAuthenticated = computed(() => !!user.value);
  const userEmail = computed(() => user.value?.email || "Unknown");

  function setUser(newUser: any) {
    user.value = newUser;
    localStorage.setItem("user", JSON.stringify(newUser));
  }

  function logout() {
    user.value = null;
    localStorage.removeItem("user");
  }

  return { user, isAuthenticated, userEmail, setUser, logout };
});