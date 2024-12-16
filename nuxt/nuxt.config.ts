// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },
  runtimeConfig: {
    API_URL: 'http://localhost:7153/',
    public: {
      API_URL: 'http://localhost:7153/'
    }
  },
  modules: [
    '@nuxt/eslint',
    '@nuxtjs/tailwindcss',
    'shadcn-nuxt',
    '@nuxtjs/google-fonts',
    '@nuxt/icon',
    '@nuxt/image',
    'nuxt-lucide-icons',
    '@nuxtjs/color-mode'
  ],
  css: ['~/assets/css/main.css'],
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {}
    }
  },
  shadcn: {
    /**
     * Prefix for all the imported component
     */
    prefix: '',
    /**
     * Directory that the component lives in.
     * @default "./components/ui"
     */
    componentDir: './components/ui'
  },
  pinia: {
    storesDirs: ['./stores/**', './custom-folder/stores/**']
  },
  googleFonts: {
    families: {
      Montserrat: true
    }
  },
  icon: {
    serverBundle: {
      collections: ['uil', 'mdi'] // <!--- this
    }
  },
  lucide: {
    namePrefix: 'Icon'
  }
})
