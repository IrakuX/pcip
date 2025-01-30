// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },
  runtimeConfig: {
    originEnvKey: 'AUTH_ORIGIN', 
    baseURL: 'https://localhost:44341/api',
    API_URL: 'https://localhost:44341/',
    public: {
      API_URL: 'https://localhost:44341/'
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
    '@nuxtjs/color-mode',
    '@sidebase/nuxt-auth'
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
  },
  auth: {
    provider: {
      type: 'local',
      endpoints: {
        getSession: { path: '/auth/me' },
        signIn: { path: '/auth/login', method: 'post' },
      }
    }
  },
})
