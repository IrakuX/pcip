<template>
  <div class="card" :class="widgetClasses">
    <div class="card-header border-0 pt-5">
      <h3 class="card-title align-items-start flex-column">
        <span class="card-label fw-bold fs-3 mb-1">Empleados destacados</span>
      </h3>
    </div>
    <div class="card-body py-3">
      <div class="table-responsive">
        <table class="table table-row-dashed table-row-gray-300 align-middle gs-0 gy-4">
          <thead>
            <tr class="fw-bold text-muted">              
              <th class="min-w-150px">{{
                translate('empleados')
              }}</th>
              <th class="min-w-140px">{{
                translate('departamentos')
              }}</th>
              <th class="min-w-120px">{{
                translate('progreso')
              }}</th>
              <th class="min-w-100px text-end">{{
                translate('acciones')
              }}</th>
            </tr>
          </thead>
          <tbody>
            <template v-for="(item, index) in list" :key="index">
              <tr>
                <td>
                  <div class="d-flex align-items-center">
                    <div class="symbol symbol-45px me-5">
                      <img :src="item.image" alt="" />
                    </div>
                    <div class="d-flex justify-content-start flex-column">
                      <span class="text-gray-900 fw-bold fs-6">{{ item.name }}</span>
                    </div>
                  </div>
                </td>

                <td>
                  <span class="text-gray-900 fw-bold fs-6">{{ item.companyName }}</span>
                </td>

                <td class="text-end">
                  <div class="d-flex flex-column w-100 me-2">
                    <div class="d-flex flex-stack mb-2">
                      <span class="text-muted me-2 fs-7 fw-semibold">
                        {{ item.value }}%
                      </span>
                    </div>

                    <div class="progress h-6px w-100">
                      <div class="progress-bar" :class="`bg-${item.color}`" role="progressbar"
                        :style="{ width: item.value + '%' }" :aria-valuenow="item.value" aria-valuemin="0"
                        aria-valuemax="100"></div>
                    </div>
                  </div>
                </td>

                <td class="text-end">
                  <a href="#" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
                    <KTIcon icon-name="switch" icon-class="fs-3" />
                  </a>

                  <a href="#" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
                    <KTIcon icon-name="pencil" icon-class="fs-3" />
                  </a>

                  <a href="#" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm">
                    <KTIcon icon-name="trash" icon-class="fs-3" />
                  </a>
                </td>
              </tr>
            </template>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { getAssetPath } from "@/core/helpers/assets";
import { defineComponent, ref } from "vue";
import { useI18n } from "vue-i18n";

export default defineComponent({
  name: "kt-widget-9",
  components: {},
  props: {
    widgetClasses: String,
  },
  setup() {
    const { t, te } = useI18n();
    const checkedRows = ref<Array<number>>([]);

    const translate = (text: string) => {
      if (te(text)) {
        return t(text);
      } else {
        return text;
      }
    };

    const list = [
      {
        image: getAssetPath("media/avatars/300-14.jpg"),
        name: "Ana Simmons",
        skills: "HTML, JS, ReactJS",
        companyName: "Calidad",
        companySkills: "Web, UI/UX Design",
        value: "50",
        color: "primary",
      },
      {
        image: getAssetPath("media/avatars/300-2.jpg"),
        name: "Jessie Clarcson",
        skills: "C#, ASP.NET, MS SQL",
        companyName: "Operador",
        companySkills: "Houses & Hotels",
        value: "70",
        color: "danger",
      },
      {
        image: getAssetPath("media/avatars/300-5.jpg"),
        name: "Lebron Wayde",
        skills: "PHP, Laravel, VueJS",
        companyName: "Operador",
        companySkills: "Transportation",
        value: "60",
        color: "success",
      },
      {
        image: getAssetPath("media/avatars/300-20.jpg"),
        name: "Natali Goodwin",
        skills: "Python, PostgreSQL, ReactJS",
        companyName: "Calidad",
        companySkills: "Insurance",
        value: "50",
        color: "warning",
      },
      {
        image: getAssetPath("media/avatars/300-23.jpg"),
        name: "Kevin Leonard",
        skills: "HTML, JS, ReactJS",
        companyName: "Operación aux.",
        companySkills: "Art Director",
        value: "90",
        color: "info",
      },
    ];

    return {
      list,
      checkedRows,
      getAssetPath,
      translate,
    };
  },
});
</script>
