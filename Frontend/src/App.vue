<script setup lang="ts">
import { RouterView } from 'vue-router'
import { useAuthStore } from './stores/auth'
import { ref, computed } from 'vue'

const auth = useAuthStore()
const showLoginModal = ref(false)
const isRegistering = ref(false)
const loginForm = ref({
  email: '',
  password: ''
})

const formTitle = computed(() => isRegistering.value ? 'Register' : 'Login')
const submitButtonText = computed(() => isRegistering.value ? 'Register' : 'Login')
const toggleText = computed(() => 
  isRegistering.value 
    ? 'Already have an account? Login' 
    : 'Don\'t have an account? Register'
)

const handleSubmit = async () => {
  try {
    if (isRegistering.value) {
      await auth.register(loginForm.value.email, loginForm.value.password)
      alert('Account created successfully! You can now login.')
    } else {
      await auth.login(loginForm.value.email, loginForm.value.password)
      alert(`Logged in successfully as ${loginForm.value.email}`)
    }

    showLoginModal.value = false
    resetForm()
  } catch (error) {
    console.error('Authentication failed:', error)
    alert('Authentication failed. Please check your credentials.')
  }
}

const resetForm = () => {
  loginForm.value = { email: '', password: '' }
}

const handleLogout = () => {
  auth.logout()
  alert("You have been logged out.")
}

const toggleForm = () => {
  isRegistering.value = !isRegistering.value
  resetForm()
}
</script>

<template>
  <div class="min-h-screen bg-gray-100">
    <nav class="bg-white fixed top-0 left-0 w-full z-50">
      <div class=" max-w-7xl mx-auto px-4">
        <div class="flex justify-between h-16">
          <div class="flex items-center">
            <span class="text-xl font-bold text-gray-800">PreTestare Case</span>
          </div>
          <div class="flex items-center space-x-4">
            <router-link to="/" class="px-3 py-2 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50">
              ICI Testing
            </router-link>
            <router-link to="/logs" class="px-3 py-2 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50">
              Logs
            </router-link>
            <div v-if="auth.isAuthenticated" class="flex items-center space-x-4">
              <span class="text-sm text-gray-700">{{ auth.email }}</span>
              <button @click="handleLogout" 
                      class="px-3 py-2 rounded-md text-sm font-medium text-red-600 hover:bg-red-50">
                Logout
              </button>
            </div>
            <button v-else @click="showLoginModal = true"
                    class="px-3 py-2 rounded-md text-sm font-medium text-blue-600 hover:bg-blue-50">
              Login
            </button>
          </div>
        </div>
      </div>
    </nav>

    <!-- Login/Register Modal -->
    <div v-if="showLoginModal" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
      <div class="bg-white rounded-lg p-6 w-full max-w-md">
        <h2 class="text-xl font-bold mb-4">{{ formTitle }}</h2>
        <form @submit.prevent="handleSubmit" class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">Email</label>
            <input type="email" v-model="loginForm.email" required
                   class="block w-full px-4 py-2 rounded-md border border-gray-300 focus:ring-blue-500 focus:border-blue-500" />
          </div>
          
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">Password</label>
            <input type="password" v-model="loginForm.password" required
                   class="block w-full px-4 py-2 rounded-md border border-gray-300 focus:ring-blue-500 focus:border-blue-500" />
          </div>

          <div class="flex justify-between items-center mt-6">
            <button type="button" @click="toggleForm"
                    class="text-sm text-blue-600 hover:text-blue-800">
              {{ toggleText }}
            </button>
            
            <div class="flex space-x-3">
              <button type="button" @click="showLoginModal = false"
                      class="px-4 py-2 text-gray-600 hover:text-gray-800">
                Cancel
              </button>
              <button type="submit"
                      class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700">
                {{ submitButtonText }}
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>

    <main class="max-w-7xl mx-auto py-6 px-4">
      <RouterView />
    </main>
  </div>
</template>
