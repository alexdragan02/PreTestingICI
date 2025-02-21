<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useAuthStore } from '../stores/auth'

const auth = useAuthStore()
const uploadedFiles = ref<any[]>([])
const clientForm = ref({
  nui: '',
  profileType: 0,
  profileReset: 'No',
  date: '',
  reconnectMinutes: 0,
  destinationAMEF: '',
  urlAMEF: ''
})

// 🔹 Populează formularul cu datele utilizatorului logat
const populateClientForm = (user: any) => {
  clientForm.value.nui = user.nui || "";
  clientForm.value.profileType = user.profileType ?? 0;
  clientForm.value.profileReset = user.profileReset ?? "No";
  clientForm.value.date = user.date || "";
  clientForm.value.reconnectMinutes = user.reconnectMinutes ?? 0;
  clientForm.value.destinationAMEF = user.destinationAMEF || "";
  clientForm.value.urlAMEF = user.urlAMEF || "";
};

// 🔹 Populează formularul automat după login
onMounted(() => {
  if (auth.user) {
    populateClientForm(auth.user);
  }
});

watch(() => auth.user, (newUser) => {
  if (newUser) {
    populateClientForm(newUser);
  }
});

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
  try {
    // Verificăm că avem un email valid înainte de a trimite request-ul
    if (!auth.userEmail) {
      alert("Nu ești autentificat. Te rog să te loghezi mai întâi.");
      return;
    }

    // Construim payload-ul doar cu valorile care sunt setate (eliminăm `null`)
    const payload: any = {
      Email: auth.userEmail, // Backend-ul așteaptă acest câmp
    };

    if (clientForm.value.nui) payload.NUI = clientForm.value.nui;
    if (clientForm.value.profileType !== null) payload.ProfileType = clientForm.value.profileType;
    if (clientForm.value.profileReset !== null) payload.ProfileReset = clientForm.value.profileReset;
    if (clientForm.value.date) payload.Date = clientForm.value.date;
    if (clientForm.value.reconnectMinutes !== null) payload.ReconnectMinutes = clientForm.value.reconnectMinutes;
    if (clientForm.value.destinationAMEF) payload.DestinationAMEF = clientForm.value.destinationAMEF;
    if (clientForm.value.urlAMEF) payload.UrlAMEF = clientForm.value.urlAMEF;

    console.log("Payload trimis către API:", payload); // ✅ Debugging

    const response = await fetch("http://localhost:5000/api/casademarcat/update", {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      const errorMessage = await response.text(); // Citim răspunsul de eroare
      throw new Error(`Eroare la actualizare: ${errorMessage}`);
    }

    const data = await response.json();
    console.log("Răspuns API:", data); // ✅ Debugging pentru structura răspunsului

    alert("Datele au fost actualizate cu succes.");
  } catch (error:any) {
    console.error("Eroare la actualizarea datelor:", error);
    alert(`Eroare la actualizare: ${error.message}`);
  }
}
</script>

<template>
  <div class="mt-20 space-y-8">
    <!-- Certificate Container -->
    <div class="bg-white shadow-md rounded-lg p-6">
      <div class="flex justify-between items-center mb-6">
        <h2 class="text-2xl font-bold text-gray-800">Certificat ANAF Curent</h2>
        <button @click="downloadCertificate"
                class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 
                       focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2
                       transition duration-150 ease-in-out flex items-center">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 mr-2" viewBox="0 0 20 20" fill="currentColor">
            <path fill-rule="evenodd" d="M3 17a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zm3.293-7.707a1 1 0 011.414 0L9 10.586V3a1 1 0 112 0v7.586l1.293-1.293a1 1 0 111.414 1.414l-3 3a1 1 0 01-1.414 0l-3-3a1 1 0 010-1.414z" clip-rule="evenodd" />
          </svg>
          Download
        </button>
      </div>
      <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div><span class="font-semibold">Common Name:</span> ANAF TEST</div>
        <div><span class="font-semibold">Organization:</span> ANAF TEST</div>
        <div><span class="font-semibold">Organization Unit:</span> ANAF TEST</div>
        <div><span class="font-semibold">Locality:</span> Bucharest</div>
        <div><span class="font-semibold">State:</span> Bucharest</div>
        <div><span class="font-semibold">Country:</span> RO</div>
        <div><span class="font-semibold">Valid From:</span> May 24, 2020</div>
        <div><span class="font-semibold">Valid To:</span> May 22, 2030</div>
        <div><span class="font-semibold">Issuer:</span> ANAF TEST, ANAF TEST</div>
        <div><span class="font-semibold">Serial Number:</span> 11052086255773359376 (0x9960e5e4a6591d10)</div>
      </div>
    </div>

    <!-- File Upload Container -->
    <div class="bg-white shadow-md rounded-lg p-6">
      <h2 class="text-2xl font-bold mb-6 text-gray-800">File Upload</h2>
      <div class="mb-6">
        <input type="file" 
               class="block w-full text-sm text-gray-500
                      file:mr-4 file:py-3 file:px-4
                      file:rounded-md file:border-0
                      file:text-sm file:font-semibold
                      file:bg-blue-50 file:text-blue-700
                      hover:file:bg-blue-100
                      cursor-pointer"
               @change="handleFileUpload" />
      </div>
      <div class="overflow-x-auto">
        <table class="min-w-full divide-y divide-gray-200">
          <thead class="bg-gray-50">
            <tr>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Name</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Size</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Upload Date</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Actions</th>
            </tr>
          </thead>
          <tbody class="bg-white divide-y divide-gray-200">
            <tr v-for="(file, index) in uploadedFiles" :key="index">
              <td class="px-6 py-4 whitespace-nowrap">{{ file.name }}</td>
              <td class="px-6 py-4 whitespace-nowrap">{{ file.size }}</td>
              <td class="px-6 py-4 whitespace-nowrap">{{ file.uploadDate }}</td>
              <td class="px-6 py-4 whitespace-nowrap">
                <button @click="deleteFile(index)"
                        class="text-red-600 hover:text-red-900 focus:outline-none focus:underline">
                  <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                    <path fill-rule="evenodd" d="M9 2a1 1 0 00-.894.553L7.382 4H4a1 1 0 000 2v10a2 2 0 002 2h8a2 2 0 002-2V6a1 1 0 100-2h-3.382l-.724-1.447A1 1 0 0011 2H9zM7 8a1 1 0 012 0v6a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v6a1 1 0 102 0V8a1 1 0 00-1-1z" clip-rule="evenodd" />
                  </svg>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- Client Generation Form -->
    <div class="bg-white shadow-md rounded-lg p-6">
      <h2 class="text-2xl font-bold mb-6 text-gray-800">Generate Client</h2>
      <form @submit.prevent="generateClient" class="space-y-6">
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-2">NUI</label>
          <input type="text" v-model="clientForm.nui" 
                 class="block w-full px-4 py-3 rounded-md border border-gray-300 shadow-sm 
                        focus:ring-2 focus:ring-blue-500 focus:border-blue-500
                        text-gray-900 placeholder-gray-400" />
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700 mb-2">Profile Type</label>
          <select v-model="clientForm.profileType"
                  class="block w-full px-4 py-3 rounded-md border border-gray-300 shadow-sm 
                         focus:ring-2 focus:ring-blue-500 focus:border-blue-500
                         text-gray-900">
            <option :value="0">0</option>
            <option :value="1">1</option>
          </select>
        </div>

        <div v-if="clientForm.profileType === 0">
          <label class="block text-sm font-medium text-gray-700 mb-2">Profile Reset</label>
          <select v-model="clientForm.profileReset"
                  class="block w-full px-4 py-3 rounded-md border border-gray-300 shadow-sm 
                         focus:ring-2 focus:ring-blue-500 focus:border-blue-500
                         text-gray-900">
            <option>Yes</option>
            <option>No</option>
          </select>

          <div v-if="clientForm.profileReset === 'Yes'" class="mt-4">
            <label class="block text-sm font-medium text-gray-700 mb-2">Date</label>
            <input type="date" v-model="clientForm.date"
                   class="block w-full px-4 py-3 rounded-md border border-gray-300 shadow-sm 
                          focus:ring-2 focus:ring-blue-500 focus:border-blue-500
                          text-gray-900" />
          </div>
        </div>

        <div v-if="clientForm.profileType === 1" class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">Numar minute reconectare</label>
            <input type="number" v-model="clientForm.reconnectMinutes"
                   class="block w-full px-4 py-3 rounded-md border border-gray-300 shadow-sm 
                          focus:ring-2 focus:ring-blue-500 focus:border-blue-500
                          text-gray-900" />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">Destinatie AMEF</label>
            <select v-model="clientForm.destinationAMEF"
                    class="block w-full px-4 py-3 rounded-md border border-gray-300 shadow-sm 
                           focus:ring-2 focus:ring-blue-500 focus:border-blue-500
                           text-gray-900">
              <option value="option1">Option 1</option>
              <option value="option2">Option 2</option>
            </select>
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">URL AMEF uzual</label>
            <input type="text" v-model="clientForm.urlAMEF"
                   placeholder="xml localhost"
                   class="block w-full px-4 py-3 rounded-md border border-gray-300 shadow-sm 
                          focus:ring-2 focus:ring-blue-500 focus:border-blue-500
                          text-gray-900" />
          </div>
        </div>

        <button type="submit"
                class="w-full flex justify-center py-3 px-4 border border-transparent rounded-md 
                       shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 
                       focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500
                       transition duration-150 ease-in-out">
          Genereaza
        </button>
      </form>
    </div>
  </div>
</template>