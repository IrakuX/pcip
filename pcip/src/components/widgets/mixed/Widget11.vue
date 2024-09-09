<template>
  <div :class="widgetClasses" class="card">
    <div :class="`bg-${widgetColor}`" class="card-header border-0 py-5">
      <h3 class="card-title fw-bold text-white">Sales Progress</h3>

      <div class="card-toolbar">
        <button type="button" :class="`btn-active-color-${widgetColor}`"
          class="btn btn-sm btn-icon btn-color-white btn-active-white border-0 me-n3" data-kt-menu-trigger="click"
          data-kt-menu-placement="bottom-end">
          <KTIcon icon-name="category" icon-class="fs-2" />
        </button>
        <Dropdown3></Dropdown3>
      </div>
    </div>
    <div class="card-body p-0">
      <apexchart ref="chartRef" :class="`bg-${widgetColor}`" class="mixed-widget-12-chart card-rounded-bottom"
        :options="chart" :series="series" height="200" type="bar"></apexchart>
      <div class="card-rounded bg-body mt-n10 position-relative card-px py-15">
        <div class="row g-0 mb-7">
          <div class="col mx-5">
            <div class="fs-6 text-gray-500">Avarage Sale</div>
            <div class="fs-2 fw-bold text-gray-800">$650</div>
          </div>
          <div class="col mx-5">
            <div class="fs-6 text-gray-500">Comissions</div>
            <div class="fs-2 fw-bold text-gray-800">$29,500</div>
          </div>
        </div>
        <div class="row g-0">
          <div class="col mx-5">
            <div class="fs-6 text-gray-500">Revenue</div>
            <div class="fs-2 fw-bold text-gray-800">$55,000</div>
          </div>
          <div class="col mx-5">
            <div class="fs-6 text-gray-500">Expenses</div>
            <div class="fs-2 fw-bold text-gray-800">$1,130,600</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { getAssetPath } from "@/core/helpers/assets";
import { computed, defineComponent, onBeforeMount, ref, watch } from "vue";
import { getCSSVariableValue } from "@/assets/ts/_utils";
import type VueApexCharts from "vue3-apexcharts";
import type { ApexOptions } from "apexcharts";
import { useThemeStore } from "@/stores/theme";
import Dropdown3 from "@/components/dropdown/Dropdown3.vue";

export default defineComponent({
  name: "widget-12",
  props: {
    widgetClasses: String,
    widgetColor: String,
    chartHeight: String,
  },
  components: {
    Dropdown3,
  },
  setup(props) {
    const chartRef = ref<typeof VueApexCharts | null>(null);
    const chart = ref<ApexOptions>({});
    const store = useThemeStore();

    const series = ref([
      {
        name: "Net Profit",
        data: [35, 65, 75, 55, 45, 60, 55, 85, 95, 105, 60, 150],
      },
      {
        name: "Revenue",
        data: [40, 70, 80, 60, 50, 65, 60, 70, 75, 80, 90, 95],
      },
    ]);

    const themeMode = computed(() => {
      return store.mode;
    });

    onBeforeMount(() => {
      Object.assign(chart.value, chartOptions(props.chartHeight));
    });

    const refreshChart = () => {
      if (!chartRef.value) {
        return;
      }

      chartRef.value.updateOptions(chartOptions(props.chartHeight));
    };

    watch(themeMode, () => {
      refreshChart();
    });

    return {
      chart,
      series,
      chartRef,
      refreshChart,
      getAssetPath,
    };
  },
});

const chartOptions = (chartHeight: string = "auto"): ApexOptions => {
  const labelColor = getCSSVariableValue("--bs-gray-500");
  const borderColor = getCSSVariableValue("--bs-gray-200");

  return {
    chart: {
      fontFamily: "inherit",
      type: "bar",
      height: chartHeight,
      toolbar: {
        show: false,
      },
      sparkline: {
        enabled: true,
      },
    },
    plotOptions: {
      bar: {
        horizontal: false,
        columnWidth: "30%",
      },
    },
    legend: {
      show: false,
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      show: true,
      width: 1,
      colors: ["transparent"],
    },
    xaxis: {
      categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Agu", "Sep", "Oct", "Nov", "Dev"],
      axisBorder: {
        show: false,
      },
      axisTicks: {
        show: false,
      },
      labels: {
        style: {
          colors: labelColor,
          fontSize: "12px",
        },
      },
    },
    yaxis: {
      min: 0,
      max: 100,
      labels: {
        style: {
          colors: labelColor,
          fontSize: "12px",
        },
      },
    },
    fill: {
      type: ["solid", "solid"],
      opacity: [0.25, 1],
    },
    states: {
      normal: {
        filter: {
          type: "none",
          value: 0,
        },
      },
      hover: {
        filter: {
          type: "none",
          value: 0,
        },
      },
      active: {
        allowMultipleDataPointsSelection: false,
        filter: {
          type: "none",
          value: 0,
        },
      },
    },
    tooltip: {
      style: {
        fontSize: "12px",
      },
      y: {
        formatter: function (val) {
          return "$" + val + " thousands";
        },
      },
      marker: {
        show: false,
      },
    },
    colors: ["#ffffff", "#ffffff"],
    grid: {
      borderColor: borderColor,
      strokeDashArray: 4,
      yaxis: {
        lines: {
          show: true,
        },
      },
      padding: {
        left: 20,
        right: 20,
      },
    },
  };
};
</script>
