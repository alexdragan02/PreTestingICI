
import { defineStore } from 'pinia'

export const useCasaStore = defineStore('casaStore', {
  state: () => ({
    selectedCasaId: null as number | null,
  }),
  actions: {
    setCasaId(id: number) {
      this.selectedCasaId = id
    }
  }
})
