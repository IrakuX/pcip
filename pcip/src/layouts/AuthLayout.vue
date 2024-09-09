<template>
  <div class="d-flex flex-column flex-lg-row flex-column-fluid">
    <div class="d-flex flex-column flex-lg-row-fluid w-lg-50 p-10 order-2 order-lg-1">
      <div class="d-flex flex-center flex-column flex-lg-row-fluid">
        <div class="w-lg-500px p-10">
          <router-view></router-view>
        </div>
      </div>
    </div>
    <div
      class="d-flex flex-lg-row-fluid w-lg-50 bgi-size-cover bgi-position-center order-1 order-lg-2"
      :style="`background-image: url('${getAssetPath('media/misc/auth-bg.png')}')`"
    >
      <div class="d-flex flex-column flex-center py-7 py-lg-15 px-5 px-md-15 w-100">
        <router-link to="/" class="mb-0 mb-lg-12">
          <img
            alt="Logo"
            :src="getAssetPath('media/logos/logoWOB.png')"
            class="h-260px h-lg-275px"
          />
        </router-link>
        <img
          class="d-none d-lg-block mx-auto w-275px w-md-50 w-xl-500px mb-10 mb-lg-20"
          :src="getAssetPath('media/misc/auth-screens.png')"
          alt=""
        />
        <h1 class="d-none d-lg-block text-white fs-2qx fw-bolder text-center mb-7">
          {{ appName }}
        </h1>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { getAssetPath } from "@/core/helpers/assets";
import { defineComponent, onMounted } from "vue";
import LayoutService from "@/core/services/LayoutService";
import { useBodyStore } from "@/stores/body";

export default defineComponent({
  name: "auth-layout",
  components: {},
  setup() {
    const store = useBodyStore();
    const appName = import.meta.env.VITE_APP_NAME;
    console.log(appName);

    onMounted(() => {
      LayoutService.emptyElementClassesAndAttributes(document.body);

      store.addBodyClassname("app-blank");
      store.addBodyClassname("bg-body");
    });

    return {
      getAssetPath,
      appName
    };
  },
});
</script>
