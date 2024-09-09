<template>
  <div class="row g-5 g-xl-8">
    <div class="col-xl-3">      
      <StatisticsWidget5
        widget-classes="card-xl-stretch mb-xl-8"
        icon-name="ki-filled ki-package"
        color="warning"
        icon-color="white"
        title="500M$"
        description="# Bolsas premium"
      ></StatisticsWidget5>
    </div>
    <div class="col-xl-3">      
      <StatisticsWidget5
        widget-classes="card-xl-stretch mb-xl-8"
        icon-name="ki-filled ki-lots-shopping"
        color="danger"
        icon-color="white"
        title="500M$"
        description="# Bolsas amazon"
      ></StatisticsWidget5>
    </div>
    <div class="col-xl-3">      
      <StatisticsWidget5
        widget-classes="card-xl-stretch mb-xl-8"
        icon-name="ki-filled ki-dropbox"
        color="primary"
        icon-color="white"
        title="500M$"
        description="# Cajas producidas"
      ></StatisticsWidget5>
    </div>
    <div class="col-xl-3">      
      <StatisticsWidget5
        widget-classes="card-xl-stretch mb-xl-8"
        icon-name="ki-filled ki-arrow-zigzag"
        color="info"
        icon-color="white"
        title="500M$"
        description="Envios realizados"
      ></StatisticsWidget5>
    </div>
  </div>
  <div class="row g-5 g-xl-8">    
    <div class="col-xxl-12">
      <TablesWidget9 widget-classes="card-xxl-stretch mb-5 mb-xl-8" />
    </div>
  </div>
  <div class="row gy-5 g-xl-8">    
    <div class="col-xl-12">
      <ChartsWidget1 widget-classes="card-xl-stretch mb-xl-8" :height="400"></ChartsWidget1>
    </div>    
  </div>  
</template>

<script lang="ts">
import { defineComponent, onMounted, onUnmounted } from "vue";
import { getAssetPath } from "@/core/helpers/assets";
import StatisticsWidget5 from "@/components/widgets/statsistics/Widget5.vue";
import TablesWidget9 from "@/components/widgets/tables/Widget9.vue";
import ChartsWidget1 from "@/components/widgets/charts/Widget1.vue";
import LayoutService from "@/core/services/LayoutService";
import { LS_CONFIG_NAME_KEY } from "@/stores/config";

export default defineComponent({
  name: "main-dashboard",
  components: {
    StatisticsWidget5,
    ChartsWidget1,
    TablesWidget9,
  },
  setup() {
    onMounted(() => {
      if (!localStorage.getItem(LS_CONFIG_NAME_KEY)) {
        LayoutService.enableToolbar();
      }
    });

    onUnmounted(() => {
      if (!localStorage.getItem(LS_CONFIG_NAME_KEY)) {
        LayoutService.disableToolbar();
      }
    });

    return {
      getAssetPath,
    };
  },
});
</script>
