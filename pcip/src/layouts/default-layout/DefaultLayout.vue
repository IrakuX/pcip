<template>
  <!-- begin:: Body -->
  <div class="page d-flex flex-row flex-column-fluid">
    <div id="kt_wrapper" class="d-flex flex-column flex-row-fluid wrapper">
      <KTHeader />
      <!-- begin:: Content -->
      <div id="kt_content" class="content d-flex flex-column flex-column-fluid">
        <!-- begin:: Content Body -->
        <div class="post d-flex flex-column-fluid">
          <div id="kt_content_container"
            :class="{ 'container-fluid': contentWidthFluid, 'container-xxl': !contentWidthFluid }">
            <router-view />
          </div>
        </div>
        <!-- end:: Content Body -->
      </div>
      <!-- end:: Content -->
      <KTFooter />
    </div>
  </div>
  <!-- end:: Body -->
  <KTScrollTop />
  <KTDrawerMessenger />
  <KTActivityDrawer />
  <KTCreateApp />
  <KTInviteFriendsModal />
  <KTHelpDrawer />
</template>

<script lang="ts">
import { defineComponent, nextTick, onBeforeMount, onMounted, watch } from "vue";
import { useRoute } from "vue-router";
import KTAside from "@/layouts/default-layout/components/aside/Aside.vue";
import KTHeader from "@/layouts/default-layout/components/header/Header.vue";
import KTFooter from "@/layouts/default-layout/components/footer/Footer.vue";
import KTToolbar from "@/layouts/default-layout/components//toolbar/Toolbar.vue";
import KTScrollTop from "@/layouts/default-layout/components/extras/ScrollTop.vue";
import KTActivityDrawer from "@/layouts/default-layout/components/drawers/ActivityDrawer.vue";
import KTCreateApp from "@/components/modals/wizards/CreateAppModal.vue";
import KTInviteFriendsModal from "@/components/modals/general/InviteFriendsModal.vue";
import KTHelpDrawer from "@/layouts/default-layout/components/extras/HelpDrawer.vue";
import KTToolButtons from "@/layouts/default-layout/components/extras/ToolButtons.vue";
import KTDrawerMessenger from "@/layouts/default-layout/components/extras/MessengerDrawer.vue";
import { reinitializeComponents } from "@/core/plugins/keenthemes";
import {
  asideEnabled,
  contentWidthFluid,
  loaderEnabled,
  loaderLogo,
  subheaderDisplay,
  themeDarkLogo,
  themeLightLogo,
  toolbarDisplay,
} from "@/layouts/default-layout/config/helper";
import LayoutService from "@/core/services/LayoutService";

export default defineComponent({
  name: "main-layout",
  components: {
    KTAside,
    KTHeader,
    KTFooter,
    KTToolbar,
    KTScrollTop,
    KTCreateApp,
    KTInviteFriendsModal,
    KTActivityDrawer,
    KTHelpDrawer,
    KTToolButtons,
    KTDrawerMessenger,
  },
  setup() {
    const route = useRoute();

    onBeforeMount(() => {
      LayoutService.init();
    });

    onMounted(() => {
      nextTick(() => {
        reinitializeComponents();
      });
    });

    watch(
      () => route.path,
      () => {
        nextTick(() => {
          reinitializeComponents();
        });
      }
    );

    return {
      toolbarDisplay,
      loaderEnabled,
      contentWidthFluid,
      loaderLogo,
      asideEnabled,
      subheaderDisplay,
      themeLightLogo,
      themeDarkLogo,
    };
  },
});
</script>
