import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const isAuthenticated = ref(false)
  const username = ref('')

  function login(user: string) {
    isAuthenticated.value = true
    username.value = user
  }

  function logout() {
    isAuthenticated.value = false
    username.value = ''
  }

  return {
    isAuthenticated,
    username,
    login,
    logout
  }
})