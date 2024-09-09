<template>
  <div v-if="pageTitleDisplay"
    :class="`page-title d-flex flex-${pageTitleDirection} align-items-start justify-content-center flex-wrap me-lg-20 pb-5 pb-lg-0`">
    <template v-if="pageTitle">
      <h1 class="page-heading d-flex text-gray-900 fw-bold fs-3 flex-column justify-content-center my-0">
        {{ translate(pageTitle) }}
      </h1>
      <span v-if="pageTitleDirection === 'row' && pageTitleBreadcrumbDisplay"
        class="h-20px border-gray-200 border-start mx-3"></span>
      <ul v-if="breadcrumbs && pageTitleBreadcrumbDisplay" class="breadcrumb fw-semibold fs-8 my-1">
        <li class="breadcrumb-item text-muted">
          <router-link to="/" class="text-muted text-hover-primary">{{ translate("home") }}</router-link>
        </li>
        <template v-for="(item, i) in breadcrumbs" :key="i">
          <li class="breadcrumb-item text-muted">{{ translate(item) }}</li>
        </template>
      </ul>
    </template>
  </div>
  <div v-else class="align-items-stretch"></div>
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";
import {
  pageTitleBreadcrumbDisplay,
  pageTitleDirection,
  pageTitleDisplay,
} from "@/layouts/default-layout/config/helper";
import { useRoute } from "vue-router";
import { useI18n } from "vue-i18n";

export default defineComponent({
  name: "layout-page-title",
  components: {},
  setup() {
    const { t, te } = useI18n();
    const route = useRoute();

    const pageTitle = computed(() => {
      return route.meta.pageTitle === 'string' ? route.meta.pageTitle : '';
    });

    const breadcrumbs = computed(() => {
      return Array.isArray(route.meta.breadcrumbs) ? route.meta.breadcrumbs : [];
    });

    const translate = (text: string) => {
      if (te(text)) {
        return t(text);
      } else {
        return text;
      }
    };

    return {
      pageTitle,
      breadcrumbs,
      pageTitleDisplay,
      pageTitleBreadcrumbDisplay,
      pageTitleDirection,
      translate,
    };
  },
});
</script>
