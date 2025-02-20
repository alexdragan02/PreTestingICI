<script setup lang="ts">
import { ref, computed } from 'vue'

interface LogEntry {
  date: string
  nui: string
  xml: string
  tlsResponse: string
}

const logs = ref<LogEntry[]>([
  {
    date: '2024-July-11 12:09:41',
    nui: '2000000001',
    xml: `<?xml version="1.0" encoding="UTF-8"?>
<msj xmlns="mfp:anaf:dgti:a4203:declaratie:v1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" idM="2000000001202407111609100004">
  <bon idB="20000000012024071114214500040001" totB="195.00" totTva="14.88">
    <cote cota="19" tva="7.98"/>
    <cote cota="5" tva="6.90"/>
  </bon>
  <rB idR="2000000001202407111609060004" nrAv="1" nrB="1" totB="195.00" nrBC="0" totBC="0.00" nrA="0" totA="0.00" nrR="0" totR="0.00" nrM="0" totM="0.00" totTva="14.88" totTvaC="0.00" totTaxe="0.00" totNet="0.00" sume_serv_in="0.00" sume_serv_out="0.00" monRef="RON">
    <pl tipP="3" valPl="195.00" monPl="RON"/>
    <coteZ cota="19" valOp="50.00" tva="7.98"/>
    <coteZ cota="9" valOp="0.00" tva="0.00"/>
    <coteZ cota="5" valOp="145.00" tva="6.90"/>
    <coteZ cota="0" valOp="0.00" tva="0.00"/>
    <av data="10.07.2024 13:22:02"/>
  </rB>
  <mE nrB="0">
    <ev dataI="11.07.2024 11:53:09" dataF="11.07.2024 11:53:46" tipE="1"/>
    <ev dataI="11.07.2024 13:09:48" dataF="11.07.2024 13:11:05" tipE="1"/>
    <ev dataI="11.07.2024 14:14:20" dataF="11.07.2024 14:14:58" tipE="1"/>
  </mE>
</msj>`,
    tlsResponse: '{"DidResume":false,"HandshakeComplete":true,"NegotiatedProtocol":"http/1.1","NegotiatedProtocolIsMutual":true,"OCSPResponse":"","ServerName":"","TLSUnique":"","Version":772}'
  },
  {
    date: '2024-July-10 09:15:41',
    nui: '2000000001',
    xml: `<?xml version="1.0" encoding="UTF-8"?>
<msj xmlns="mfp:anaf:dgti:a4203:declaratie:v1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" idM="2000000001202407101315110003">
  <bon idB="20000000012024071013135400030001" totB="50.00" totTva="7.98">
    <cote cota="19" tva="7.98"/>
  </bon>
  <rB idR="2000000001202407101315070003" nrAv="0" nrB="1" totB="50.00" nrBC="0" totBC="0.00" nrA="0" totA="0.00" nrR="0" totR="0.00" nrM="0" totM="0.00" totTva="7.98" totTvaC="0.00" totTaxe="0.00" totNet="0.00" sume_serv_in="0.00" sume_serv_out="0.00" monRef="RON">
    <pl tipP="1" valPl="50.00" monPl="RON"/>
    <coteZ cota="19" valOp="50.00" tva="7.98"/>
    <coteZ cota="9" valOp="0.00" tva="0.00"/>
    <coteZ cota="5" valOp="0.00" tva="0.00"/>
    <coteZ cota="0" valOp="0.00" tva="0.00"/>
  </rB>
</msj>`,
    tlsResponse: '{"DidResume":false,"HandshakeComplete":true,"NegotiatedProtocol":"http/1.1","NegotiatedProtocolIsMutual":true,"OCSPResponse":"","ServerName":"","TLSUnique":"","Version":772}'
  },
  {
    date: '2024-July-10 09:12:02',
    nui: '2000000001',
    xml: `<?xml version="1.0" encoding="UTF-8"?>
<msj xmlns="mfp:anaf:dgti:a4203:declaratie:v1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" idM="2000000001202407101312020002">
  <rB idR="2000000001202407101311580002" nrAv="0" nrB="0" totB="0.00" nrBC="0" totBC="0.00" nrA="0" totA="0.00" nrR="0" totR="0.00" nrM="0" totM="0.00" totTva="0.00" totTvaC="0.00" totTaxe="0.00" totNet="0.00" sume_serv_in="0.00" sume_serv_out="0.00" monRef="RON">
    <coteZ cota="19" valOp="0.00" tva="0.00"/>
    <coteZ cota="9" valOp="0.00" tva="0.00"/>
    <coteZ cota="5" valOp="0.00" tva="0.00"/>
    <coteZ cota="0" valOp="0.00" tva="0.00"/>
  </rB>
</msj>`,
    tlsResponse: '{"DidResume":false,"HandshakeComplete":true,"NegotiatedProtocol":"http/1.1","NegotiatedProtocolIsMutual":true,"OCSPResponse":"","ServerName":"","TLSUnique":"","Version":772}'
  },
  {
    date: '2024-July-09 17:30:15',
    nui: '2000000001',
    xml: `<?xml version="1.0" encoding="UTF-8"?>
<msj xmlns="mfp:anaf:dgti:a4203:declaratie:v1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" idM="2000000001202407091730150001">
  <bon idB="20000000012024070917301500010001" totB="75.50" totTva="12.05">
    <cote cota="19" tva="12.05"/>
  </bon>
  <rB idR="2000000001202407091730100001" nrAv="0" nrB="1" totB="75.50" nrBC="0" totBC="0.00" nrA="0" totA="0.00" nrR="0" totR="0.00" nrM="0" totM="0.00" totTva="12.05" totTvaC="0.00" totTaxe="0.00" totNet="0.00" sume_serv_in="0.00" sume_serv_out="0.00" monRef="RON">
    <pl tipP="1" valPl="75.50" monPl="RON"/>
    <coteZ cota="19" valOp="75.50" tva="12.05"/>
    <coteZ cota="9" valOp="0.00" tva="0.00"/>
    <coteZ cota="5" valOp="0.00" tva="0.00"/>
    <coteZ cota="0" valOp="0.00" tva="0.00"/>
  </rB>
</msj>`,
    tlsResponse: '{"DidResume":false,"HandshakeComplete":true,"NegotiatedProtocol":"http/1.1","NegotiatedProtocolIsMutual":true,"OCSPResponse":"","ServerName":"","TLSUnique":"","Version":772}'
  },
  {
    date: '2024-July-09 14:45:22',
    nui: '2000000001',
    xml: `<?xml version="1.0" encoding="UTF-8"?>
<msj xmlns="mfp:anaf:dgti:a4203:declaratie:v1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" idM="2000000001202407091445220001">
  <bon idB="20000000012024070914452200010001" totB="120.00" totTva="19.15">
    <cote cota="19" tva="19.15"/>
  </bon>
  <rB idR="2000000001202407091445180001" nrAv="0" nrB="1" totB="120.00" nrBC="0" totBC="0.00" nrA="0" totA="0.00" nrR="0" totR="0.00" nrM="0" totM="0.00" totTva="19.15" totTvaC="0.00" totTaxe="0.00" totNet="0.00" sume_serv_in="0.00" sume_serv_out="0.00" monRef="RON">
    <pl tipP="1" valPl="120.00" monPl="RON"/>
    <coteZ cota="19" valOp="120.00" tva="19.15"/>
    <coteZ cota="9" valOp="0.00" tva="0.00"/>
    <coteZ cota="5" valOp="0.00" tva="0.00"/>
    <coteZ cota="0" valOp="0.00" tva="0.00"/>
  </rB>
</msj>`,
    tlsResponse: '{"DidResume":false,"HandshakeComplete":true,"NegotiatedProtocol":"http/1.1","NegotiatedProtocolIsMutual":true,"OCSPResponse":"","ServerName":"","TLSUnique":"","Version":772}'
  }
])

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
    <h2 class="text-2xl font-bold text-gray-800">Loguri trimitere rapoarte Z</h2>
    <div class="space-y-8">
      <div v-for="(log, index) in paginatedLogs" :key="index" class="bg-white shadow-md rounded-lg p-6">
        <div class="border-b pb-4 mb-4">
          <div class="flex justify-between items-center mb-2">
            <span class="text-gray-600">{{ log.date }}</span>
            <span class="bg-blue-100 text-blue-800 px-3 py-1 rounded-full text-sm">NUI: {{ log.nui }}</span>
          </div>
        </div>

        <div class="space-y-4">
          <div>
            <h3 class="text-lg font-semibold mb-2">XML Trimis:</h3>
            <div class="bg-gray-50 rounded-lg p-4 overflow-x-auto">
              <pre class="text-sm text-gray-700 font-mono whitespace-pre">{{ formatXML(log.xml) }}</pre>
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