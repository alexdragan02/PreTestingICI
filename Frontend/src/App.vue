<script setup lang="ts">
import { RouterView } from 'vue-router'
import { useAuthStore } from './stores/auth'
import { ref } from 'vue'

const auth = useAuthStore()
const showLoginModal = ref(false)
const loginForm = ref({
  username: '',
  password: ''
})

const handleLogin = () => {
  // In a real application, you would validate credentials here
  auth.login(loginForm.value.username)
  showLoginModal.value = false
  loginForm.value = { username: '', password: '' }
}

const handleLogout = () => {
  auth.logout()
}
</script>

<template>
  <div class="  bg-gray-100">
    <nav class="bg-white shadow-lg fixed top-0 left-0 w-full z-50">
      <div class="max-w-7xl  mx-auto px-4">
        <div class="flex justify-between h-16">
          <div class="flex items-center">
            <span class="text-xl font-bold text-gray-800">PreTestare Case</span>
          </div>
          <div class="flex items-center space-x-4">
            <router-link to="/" class="px-3 py-2 rounded-md text-sm font-medium text-gray-700 hover:text-gray-900 hover:bg-gray-50">
              ICI Testing
            </router-link>
            <router-link to="/logs" class="px-3 py-2 rounded-md text-sm font-medium text-gray-700 hover:text-gray-900 hover:bg-gray-50">
              Logs
            </router-link>
            <div v-if="auth.isAuthenticated" class="flex items-center space-x-4">
              <span class="text-sm text-gray-700">{{ auth.username }}</span>
              <button @click="handleLogout" 
                      class="px-3 py-2 rounded-md text-sm font-medium text-red-600 hover:text-red-900 hover:bg-red-50">
                Logout
              </button>
            </div>
            <button v-else
                    @click="showLoginModal = true"
                    class="px-3 py-2 rounded-md text-sm font-medium text-blue-600 hover:text-blue-900 hover:bg-blue-50">
              Login
            </button>
          </div>
        </div>
      </div>
    </nav>

    <!-- Login Modal -->
    <div v-if="showLoginModal" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
      <div class="bg-white rounded-lg p-6 w-full max-w-md">
        <h2 class="text-xl font-bold mb-4">Login</h2>
        <form @submit.prevent="handleLogin" class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">Username</label>
            <input type="text" 
                   v-model="loginForm.username"
                   class="block w-full px-4 py-2 rounded-md border border-gray-300" />
          </div>
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">Password</label>
            <input type="password" 
                   v-model="loginForm.password"
                   class="block w-full px-4 py-2 rounded-md border border-gray-300" />
          </div>
          <div class="flex justify-end space-x-3">
            <button type="button" 
                    @click="showLoginModal = false"
                    class="px-4 py-2 text-gray-600 hover:text-gray-800">
              Cancel
            </button>
            <button type="submit"
                    class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700">
              Login
            </button>
          </div>
        </form>
      </div>
    </div>

    <main class="max-w-7xl mx-auto py-6 px-4">
      <RouterView />
    </main>
  </div>
</template>