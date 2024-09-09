import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
// https://vitejs.dev/config/
export default defineConfig({
  base: "/pcip/",
  plugins: [vue()],
  define: {
    "process.env.VITE_APP_TITLE": JSON.stringify(process.env.VITE_APP_TITLE),
    "process.env.VITE_API_URL": JSON.stringify(process.env.VITE_API_URL)
  },
  resolve: {
    alias: {
      "vue-i18n": "vue-i18n/dist/vue-i18n.cjs.js",
      "@": fileURLToPath(new URL("./src", import.meta.url)),
    },
  },
  build: {
    chunkSizeWarningLimit: 3000,
  },
});
