// filepath: /C:/Users/Dragan Alexandru/Desktop/PreTestingICI/Frontend/src/views/LogsView.vue
<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'

interface LogEntry {
  date: string
  nui: string
  xml: string
  tlsResponse: string
}

const logs = ref<LogEntry[]>([])

const fetchLogs = async () => {
  try {
    const response = await axios.get('http://localhost:8080/api/msj/xml', { headers: { 'Accept': 'application/xml' } })
    const parser = new DOMParser()
    const xmlDoc = parser.parseFromString(response.data, 'application/xml')

    const logEntries = Array.from(xmlDoc.getElementsByTagName('Msj')).map(msj => ({
      date: msj.querySelector('Date')?.textContent || 'N/A',
      nui: msj.querySelector('Nui')?.textContent || 'N/A',
      xml: new XMLSerializer().serializeToString(msj),
      tlsResponse: msj.querySelector('TlsResponse')?.textContent || '{}'
    }))

    logs.value = logEntries
  } catch (error) {
    console.error('Error fetching logs:', error)
  }
}


onMounted(fetchLogs)

const formatXML = (xml: string): string => {
  return xml
    .replace(/></g, '>\n<')
    .replace(/<\//g, '\n</')
    .split('\n')
    .map(line => {
      const indent = line.match(/<\//) ? 2 : line.match(/<[^/]/) ? 0 : 4
      return ' '.repeat(indent) + line
    })
    .join('\n')
}

const currentPage = ref(1)
const itemsPerPage = 5
const totalPages = computed(() => Math.ceil(logs.value.length / itemsPerPage))

const paginatedLogs = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage
  const end = start + itemsPerPage
  return logs.value.slice(start, end)
})
</script>

<template>
  <div class="mt-20 space-y-6">
    <h2 class="text-2xl font-bold text-black-800">Loguri trimitere rapoarte Z</h2>
    <div class="space-y-8">
      <div v-for="(log, index) in paginatedLogs" :key="index" class="bg-white shadow-md rounded-lg p-6">
        <div class="border-b pb-4 mb-4">
          <div class="flex justify-between items-center mb-2">
            <span class="text-black-600">{{ log.date }}</span>
            <span class="bg-blue-100 text-blue-800 px-3 py-1 rounded-full text-sm">NUI: {{ log.nui }}</span>
          </div>
        </div>

        <div class="space-y-4">
          <div>
            <h3 class="text-lg font-semibold mb-2">XML Trimis:</h3>
            <div class="bg-gray-50 rounded-lg p-4 overflow-x-auto">
              <pre class="text-sm text-black-700 font-mono whitespace-pre">{{ formatXML(log.xml) }}</pre>
            </div>
          </div>

          <div>
            <h3 class="text-lg font-semibold mb-2">TLS Response:</h3>
            <div class="bg-gray-50 rounded-lg p-4">
              <pre class="text-sm text-gray-700 font-mono">{{ JSON.stringify(JSON.parse(log.tlsResponse), null, 2) }}</pre>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Pagination -->
    <div class="flex justify-center space-x-2 mt-6">
      <button 
        v-for="page in totalPages" 
        :key="page"
        @click="currentPage = page"
        :class="[
          'px-3 py-1 rounded-md',
          currentPage === page 
            ? 'bg-blue-600 text-white' 
            : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
        ]"
      >
        {{ page }}
      </button>
    </div>
  </div>
</template>

<style scoped>
pre {
  max-height: 400px;
  overflow-y: auto;
}
</style>