<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../stores/auth'
import { useCasaStore } from '../stores/casaStore'
import { useRouter } from 'vue-router'

const casaStore = useCasaStore()
const router = useRouter()

const selectCasaForLogs = (id: number) => {
  casaStore.setCasaId(id) // Stocăm ID-ul casei în store
  router.push('/logs') // Navigăm la LogsView
}
const auth = useAuthStore()
const uploadedFiles = ref<any[]>([])
const clientForm = ref({
  id: 0, // Adăugat ID pentru a identifica casa la actualizare
  nui: '',
  profileType: 0,
  profileReset: 'No',
  date: '',
  reconnectMinutes: 0,
  destinationAMEF: '',
  urlAMEF: '',
  name: '', // Adăugat pentru a păstra numele casei
  email: '' // Adăugat pentru a păstra email-ul
})

const casas = ref<any[]>([])
const newCasaName = ref('')
const isEditMode = ref(false) // Pentru a ști dacă edităm o casă existentă
const showDetails = ref(false) // Pentru a afișa/ascunde detaliile casei

const handleFileUpload = (event: Event) => {
  const file = (event.target as HTMLInputElement).files?.[0]
  if (file) {
    uploadedFiles.value.push({
      name: file.name,
      size: `${(file.size / 1024).toFixed(2)} KB`,
      uploadDate: new Date().toLocaleString()
    })
  }
}

const deleteFile = (index: number) => {
  uploadedFiles.value.splice(index, 1)
}

const downloadCertificate = () => {
  const certificateData = `Common Name: ANAF TEST
Organization: ANAF TEST
Organization Unit: ANAF TEST
Locality: Bucharest
State: Bucharest
Country: RO
Valid From: May 24, 2020
Valid To: May 22, 2030
Issuer: ANAF TEST, ANAF TEST
Serial Number: 11052086255773359376 (0x9960e5e4a6591d10)`

  const blob = new Blob([certificateData], { type: 'text/plain' })
  const url = window.URL.createObjectURL(blob)
  const a = document.createElement('a')
  a.href = url
  a.download = 'ANAF_Certificate.txt'
  document.body.appendChild(a)
  a.click()
  window.URL.revokeObjectURL(url)
  document.body.removeChild(a)
}
const generateClient = async () => {
  if (isEditMode.value) {
    console.log('Trimitem update cu:', clientForm.value);

    try {
      const response = await fetch(`http://localhost:8080/api/casademarcat/update/${clientForm.value.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          id: clientForm.value.id,
          name: clientForm.value.name,
          email: clientForm.value.email,
          nui: clientForm.value.nui,
          TipProfil: clientForm.value.profileType,
          TipReset: clientForm.value.profileReset === "Yes",
          DateTime: clientForm.value.date ? new Date(clientForm.value.date).toISOString() : null,
          NrMinuteReconectare: clientForm.value.reconnectMinutes,
          DestinatieAmef: clientForm.value.destinationAMEF,
          URLAmef: clientForm.value.urlAMEF,
          UserId: auth.user?.id
        }),
        credentials: 'include'
      });

      if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(`Failed to update casa de marcat: ${errorMessage}`);
      }

      await fetchCaseDeMarcat();
      resetForm();
      alert('Casa de marcat a fost actualizată cu succes!');
    } catch (error) {
      console.error(error);
      alert('A apărut o eroare la actualizarea casei de marcat.');
    }
  }
}

const resetForm = () => {
  clientForm.value = {
    id: 0,
    nui: '',
    profileType: 0,
    profileReset: 'No',
    date: '',
    reconnectMinutes: 0,
    destinationAMEF: '',
    urlAMEF: '',
    name: '',
    email: ''
  }
  isEditMode.value = false
  showDetails.value = false
}

const fetchCaseDeMarcat = async () => {
  try {
    const response = await fetch('http://localhost:8080/api/casademarcat/list', {
      credentials: 'include' // Include credentials to ensure session is sent
    })
    
    if (!response.ok) throw new Error('Failed to fetch case de marcat')
    const data = await response.json()
    casas.value = data
  } catch (error) {
    console.error(error)
  }
}

const addCasa = async () => {
  if (!newCasaName.value.trim()) {
    alert('Introduceți un nume pentru casa de marcat!');
    return;
  }
  
  if (!auth.user?.id) {
    console.error('User is not logged in');
    alert('Trebuie să fii logat pentru a adăuga o casă de marcat!');
    return;
  }

  console.log("Trimitem la backend:", {
    name: newCasaName.value,
    userId: auth.user.id
  });

  try {
    const response = await fetch('http://localhost:8080/api/casademarcat/add', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: newCasaName.value,
        userId: auth.user.id
      }),
      credentials: 'include'
    });

    if (!response.ok) {
      const errorMessage = await response.text();
      throw new Error(`Failed to add casa de marcat: ${errorMessage}`);
    }

    await fetchCaseDeMarcat();
    newCasaName.value = ''; // Resetează câmpul de introducere a numelui
  } catch (error) {
    console.error(error);
    alert('A apărut o eroare la adăugarea casei de marcat.');
  }
};

const deleteCasa = async (id: number) => {
  try {
    const response = await fetch(`http://localhost:8080/api/casademarcat/delete/${id}`, {
      method: 'DELETE',
      credentials: 'include' // Include credentials to ensure session is sent
    })
    if (!response.ok) throw new Error('Failed to delete casa de marcat')
    await fetchCaseDeMarcat()
  } catch (error) {
    console.error(error)
  }
}

const selectCasa = (id: number) => {
  const selectedCasa = casas.value.find(casa => casa.id === id)
  console.log(selectedCasa);
  // Populăm formularul cu datele casei selectate
  clientForm.value = { 
    id: selectedCasa.id,
    name: selectedCasa.name || '',
    email: selectedCasa.email || '', // Asigură-te că acest câmp există în backend
    nui: selectedCasa.nui || '',
    profileType: selectedCasa.tipProfil || 0,  // Schimbă `profileType` -> `tipProfil`
    profileReset: selectedCasa.tipReset ? 'Yes' : 'No', // Schimbă `profileReset` -> `tipReset`
    date: selectedCasa.date || '',
    reconnectMinutes: selectedCasa.reconnectMinutes || 0,
    destinationAMEF: selectedCasa.destinationAMEF || '',
    urlAMEF: selectedCasa.urlAMEF || ''
  }
  isEditMode.value = true
  showDetails.value = true
}

onMounted(() => {
  fetchCaseDeMarcat()
  console.log("user logat",auth.user);
})
</script>

<template>
  <div class="space-y-6">
    <!-- Certificate Container -->
    <div class="bg-white shadow-md rounded-lg p-4 mt-16">
      <div class="flex justify-between items-center mb-4">
        <h2 class="text-xl font-bold text-gray-800">Certificat ANAF Curent</h2>
        <button @click="downloadCertificate"
                class="px-3 py-1.5 bg-blue-600 text-white rounded-md hover:bg-blue-700 
                       focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2
                       transition duration-150 ease-in-out flex items-center text-sm">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 mr-1" viewBox="0 0 20 20" fill="currentColor">
            <path fill-rule="evenodd" d="M3 17a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zm3.293-7.707a1 1 0 011.414 0L9 10.586V3a1 1 0 112 0v7.586l1.293-1.293a1 1 0 111.414 1.414l-3 3a1 1 0 01-1.414 0l-3-3a1 1 0 010-1.414z" clip-rule="evenodd" />
          </svg>
          Download
        </button>
      </div>
      <div class="grid grid-cols-2 gap-2 text-sm">
        <div><span class="font-semibold">Common Name:</span> ANAF TEST</div>
        <div><span class="font-semibold">Organization:</span> ANAF TEST</div>
        <div><span class="font-semibold">Organization Unit:</span> ANAF TEST</div>
        <div><span class="font-semibold">Locality:</span> Bucharest</div>
        <div><span class="font-semibold">State:</span> Bucharest</div>
        <div><span class="font-semibold">Country:</span> RO</div>
        <div><span class="font-semibold">Valid From:</span> May 24, 2020</div>
        <div><span class="font-semibold">Valid To:</span> May 22, 2030</div>
        <div><span class="font-semibold">Issuer:</span> ANAF TEST, ANAF TEST</div>
        <div><span class="font-semibold">Serial Number:</span> 11052086255773359376</div>
      </div>
    </div>

    <!-- File Upload Container -->
    <div class="bg-white shadow-md rounded-lg p-4">
      <h2 class="text-xl font-bold mb-3 text-gray-800">File Upload</h2>
      <div class="mb-3">
        <input type="file" 
               class="block w-full text-sm text-gray-500
                      file:mr-3 file:py-2 file:px-3
                      file:rounded-md file:border-0
                      file:text-sm file:font-semibold
                      file:bg-blue-50 file:text-blue-700
                      hover:file:bg-blue-100
                      cursor-pointer"
               @change="handleFileUpload" />
      </div>
      <div class="overflow-x-auto">
        <table class="min-w-full divide-y divide-gray-200 text-sm">
          <thead class="bg-gray-50">
            <tr>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Name</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Size</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Upload Date</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Actions</th>
            </tr>
          </thead>
          <tbody class="bg-white divide-y divide-gray-200">
            <tr v-for="(file, index) in uploadedFiles" :key="index">
              <td class="px-4 py-2 whitespace-nowrap">{{ file.name }}</td>
              <td class="px-4 py-2 whitespace-nowrap">{{ file.size }}</td>
              <td class="px-4 py-2 whitespace-nowrap">{{ file.uploadDate }}</td>
              <td class="px-4 py-2 whitespace-nowrap">
                <button @click="deleteFile(index)"
                        class="text-red-600 hover:text-red-900 focus:outline-none">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 20 20" fill="currentColor">
                    <path fill-rule="evenodd" d="M9 2a1 1 0 00-.894.553L7.382 4H4a1 1 0 000 2v10a2 2 0 002 2h8a2 2 0 002-2V6a1 1 0 100-2h-3.382l-.724-1.447A1 1 0 0011 2H9zM7 8a1 1 0 012 0v6a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v6a1 1 0 102 0V8a1 1 0 00-1-1z" clip-rule="evenodd" />
                  </svg>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- Casa de Marcat Management -->
    <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
      <!-- Casa List Panel -->
      <div class="bg-white shadow-md rounded-lg p-4">
        <h2 class="text-xl font-bold mb-3 text-gray-800">Case de Marcat</h2>
        
        <!-- Add Casa Form -->
        <div class="mb-4">
          <div class="flex space-x-2">
            <input type="text" v-model="newCasaName" placeholder="Nume Casa"
                  class="block w-full px-3 py-2 rounded-md border border-gray-300 shadow-sm 
                          focus:ring-2 focus:ring-blue-500 focus:border-blue-500
                          text-gray-900 placeholder-gray-400 text-sm" />
            <button @click="addCasa"
                    class="px-3 py-2 bg-green-600 text-white rounded-md hover:bg-green-700 
                          focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-offset-2
                          transition duration-150 ease-in-out text-sm">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd" d="M10 5a1 1 0 011 1v3h3a1 1 0 110 2h-3v3a1 1 0 11-2 0v-3H6a1 1 0 110-2h3V6a1 1 0 011-1z" clip-rule="evenodd" />
              </svg>
            </button>
          </div>
        </div>
        
        <!-- Casa List -->
        <div class="space-y-1 max-h-96 overflow-y-auto">
          <div v-for="casa in casas" :key="casa.id" 
               class="flex justify-between items-center p-2 rounded-md hover:bg-gray-100 cursor-pointer"
               :class="{ 'bg-blue-50': clientForm.id === casa.id }"
               @click="selectCasa(casa.id)">
            <span class="font-medium truncate">{{ casa.name }}</span>
            <div class="flex space-x-1">
              <button @click.stop="selectCasaForLogs(casa.id)" 
                      class="text-green-600 hover:text-green-800 p-1" 
                      title="Vezi Loguri">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 20 20" fill="currentColor">
                  <path d="M10 12a2 2 0 100-4 2 2 0 000 4z" />
                  <path fill-rule="evenodd" d="M.458 10C1.732 5.943 5.522 3 10 3s8.268 2.943 9.542 7c-1.274 4.057-5.064 7-9.542 7S1.732 14.057.458 10zM14 10a4 4 0 11-8 0 4 4 0 018 0z" clip-rule="evenodd" />
                </svg>
              </button>
              <button @click.stop="deleteCasa(casa.id)" 
                      class="text-red-600 hover:text-red-800 p-1" 
                      title="Șterge">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4" viewBox="0 0 20 20" fill="currentColor">
                  <path fill-rule="evenodd" d="M9 2a1 1 0 00-.894.553L7.382 4H4a1 1 0 000 2v10a2 2 0 002 2h8a2 2 0 002-2V6a1 1 0 100-2h-3.382l-.724-1.447A1 1 0 0011 2H9zM7 8a1 1 0 012 0v6a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v6a1 1 0 102 0V8a1 1 0 00-1-1z" clip-rule="evenodd" />
                </svg>
              </button>
            </div>
          </div>
          <div v-if="casas.length === 0" class="text-gray-500 text-center py-4">
            Nu există case de marcat
          </div>
        </div>
      </div>

      <!-- Casa Details Panel -->
      <div class="bg-white shadow-md rounded-lg p-4 md:col-span-2" v-if="isEditMode && showDetails">
        <div class="flex justify-between items-center mb-4">
          <h2 class="text-xl font-bold text-gray-800">
            {{ clientForm.name }}
          </h2>
          <button @click="resetForm" 
                  class="px-3 py-1.5 bg-gray-500 text-white rounded-md hover:bg-gray-600
                        focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-offset-2
                        transition duration-150 ease-in-out text-sm">
            Închide
          </button>
        </div>

        <form @submit.prevent="generateClient" class="space-y-4">
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Nume</label>
              <input type="text" v-model="clientForm.name" class="block w-full px-3 py-2 rounded-md border border-gray-300 text-sm"/>
            </div>

            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">NUI</label>
              <input type="text" v-model="clientForm.nui" class="block w-full px-3 py-2 rounded-md border border-gray-300 text-sm"/>
            </div>

            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Profile Type</label>
              <select v-model="clientForm.profileType" class="block w-full px-3 py-2 rounded-md border border-gray-300 text-sm">
                <option :value="0">0</option>
                <option :value="1">1</option>
              </select>
            </div>

            <div v-if="clientForm.profileType === 0">
              <label class="block text-sm font-medium text-gray-700 mb-1">Profile Reset</label>
              <select v-model="clientForm.profileReset" class="block w-full px-3 py-2 rounded-md border border-gray-300 text-sm">
                <option value="No">No</option>
                <option value="Yes">Yes</option>
              </select>
            </div>

            <div v-if="clientForm.profileType === 0 && clientForm.profileReset === 'Yes'">
              <label class="block text-sm font-medium text-gray-700 mb-1">Date</label>
              <input type="date" v-model="clientForm.date" class="block w-full px-3 py-2 rounded-md border border-gray-300 text-sm"/>
            </div>

            <div v-if="clientForm.profileType === 1">
              <label class="block text-sm font-medium text-gray-700 mb-1">Reconnect Minutes</label>
              <input type="number" v-model="clientForm.reconnectMinutes" class="block w-full px-3 py-2 rounded-md border border-gray-300 text-sm"/>
            </div>

            <div v-if="clientForm.profileType === 1">
              <label class="block text-sm font-medium text-gray-700 mb-1">Destinatie AMEF</label>
              <select v-model="clientForm.destinationAMEF" class="block w-full px-3 py-2 rounded-md border border-gray-300 text-sm">
                <option value="AMEF Uzual">AMEF Uzual</option>
                <option value="AMEF Schimb Valutar">AMEF Schimb Valutar</option>
              </select>
            </div>

            <div v-if="clientForm.profileType === 1">
              <label class="block text-sm font-medium text-gray-700 mb-1">URL AMEF</label>
              <input type="text" v-model="clientForm.urlAMEF" class="block w-full px-3 py-2 rounded-md border border-gray-300 text-sm"/>
            </div>
          </div>

          <button type="submit" class="w-full flex justify-center py-2 px-4 bg-blue-600 text-white rounded-md text-sm">
            Actualizează Casa de Marcat
          </button>
        </form>
      </div>
      
      <!-- Empty State when no casa is selected -->
      <div class="bg-white shadow-md rounded-lg p-4 md:col-span-2 flex items-center justify-center" v-if="!isEditMode || !showDetails">
        <div class="text-center text-gray-500">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-12 w-12 mx-auto mb-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 14v3m4-3v3m4-3v3M3 21h18M3 10h18M3 7l9-4 9 4M4 10h16v11H4V10z" />
          </svg>
          <p class="text-lg font-medium">Selectați o casă de marcat</p>
          <p class="text-sm">Alegeți o casă din lista din stânga pentru a vedea detaliile</p>
        </div>
      </div>
    </div>

    <!-- Casa de Marcat Table -->
    <div class="bg-white shadow-md rounded-lg p-4">
      <h2 class="text-xl font-bold mb-3 text-gray-800">Detalii Case de Marcat</h2>
      <div class="overflow-x-auto">
        <table class="min-w-full divide-y divide-gray-200 text-sm">
          <thead class="bg-gray-50">
            <tr>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Name</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">NUI</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Profile Type</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Profile Reset</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Date</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Reconnect Min</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Dest. AMEF</th>
              <th class="px-4 py-2 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">URL AMEF</th>
            </tr>
          </thead>
          <tbody class="bg-white divide-y divide-gray-200">
            <tr v-for="(casa, index) in casas" :key="index" :class="{ 'bg-blue-50': clientForm.id === casa.id }">
              <td class="px-3 py-2 whitespace-nowrap">{{ casa.name }}</td>
                  <td class="px-3 py-2 whitespace-nowrap">{{ casa.nui }}</td>
                  <td class="px-3 py-2 whitespace-nowrap">{{ casa.tipProfil }}</td>
                  <td class="px-3 py-2 whitespace-nowrap">{{ casa.tipReset ? 'Yes' : 'No' }}</td>
                  <td class="px-3 py-2 whitespace-nowrap">{{ casa.dateTime }}</td>
                  <td class="px-3 py-2 whitespace-nowrap">{{ casa.nrMinuteReconectare }}</td>
                  <td class="px-3 py-2 whitespace-nowrap">{{ casa.destinatieAmef }}</td>
                  <td class="px-3 py-2 whitespace-nowrap">{{ casa.urlAmef }}</td>
            </tr>
            <tr v-if="casas.length === 0">
              <td colspan="8" class="px-4 py-4 text-center text-gray-500">Nu există case de marcat</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>