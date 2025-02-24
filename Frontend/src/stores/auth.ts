import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import axios from 'axios'

interface User {
  id: string
  email: string
}

const API_URL = 'http://localhost:8080/api/auth'

export const useAuthStore = defineStore('auth', () => {
  const user = ref<User | null>(null)

  const isAuthenticated = computed(() => !!user.value)
  const email = computed(() => user.value?.email || '')

  // ✅ Reîncarcă user-ul din localStorage
  const storedUser = localStorage.getItem('user')
  if (storedUser) {
    user.value = JSON.parse(storedUser)
  }

  async function register(email: string, password: string) {
    try {
      const response = await axios.post(`${API_URL}/register`, { email, password })
      if (response.status === 200) {
        user.value = { id: response.data.id, email }
        localStorage.setItem('user', JSON.stringify(user.value))
      }
    } catch (error) {
      console.error('Registration failed:', error)
      throw error
    }
  }

  async function login(email: string, password: string) {
    try {
      const response = await axios.post(`${API_URL}/login`, { email, password });
      if (response.status === 200) {
        user.value = { id: response.data.user.id, email: response.data.user.email };
        localStorage.setItem('user', JSON.stringify(user.value)); // ✅ Salvează user-ul în localStorage
      }
    } catch (error) {
      console.error('Login failed:', error);
      throw error;
    }
  }
  

  function logout() {
    user.value = null
    localStorage.removeItem('user')
  }

  return {
    user, // ✅ Adăugat pentru a putea fi accesat în ICITestingView.vue
    isAuthenticated,
    email,
    register,
    login,
    logout
  }
})
