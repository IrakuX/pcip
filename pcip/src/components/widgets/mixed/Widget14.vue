<template>
  <!--begin::Mixed Widget 14-->
  <div
    :class="widgetClasses"
    class="card theme-dark-bg-body"
    :style="`background-color: ${widgetColor}`"
  >
    <!--begin::Body-->
    <div class="card-body d-flex flex-column">
      <!--begin::Wrapper-->
      <div class="d-flex flex-column mb-7">
        <!--begin::Title-->
        <a href="#" class="text-gray-900 text-hover-primary fw-bold fs-3"
          >Summary</a
        >
        <!--end::Title-->
      </div>
      <!--end::Wrapper-->

      <!--begin::Row-->
      <div class="row g-0">
        <!--begin::Col-->
        <div class="col-6">
          <div class="d-flex align-items-center mb-9 me-2">
            <!--begin::Symbol-->
            <div class="symbol symbol-40px me-3">
              <div class="symbol-label bg-body bg-opacity-50">
                <KTIcon icon-name="abstract-42" icon-class="fs-1 text-gray-900" />
              </div>
            </div>
            <!--end::Symbol-->

            <!--begin::Title-->
            <div>
              <div class="fs-5 text-gray-900 fw-bold lh-1">$50K</div>
              <div class="fs-7 text-gray-600 fw-semobold">Sales</div>
            </div>
            <!--end::Title-->
          </div>
        </div>
        <!--end::Col-->

        <!--begin::Col-->
        <div class="col-6">
          <div class="d-flex align-items-center mb-9 ms-2">
            <!--begin::Symbol-->
            <div class="symbol symbol-40px me-3">
              <div class="symbol-label bg-body bg-opacity-50">
                <KTIcon icon-name="abstract-45" icon-class="fs-1 text-gray-900" />
              </div>
            </div>
            <!--end::Symbol-->

            <!--begin::Title-->
            <div>
              <div class="fs-5 text-gray-900 fw-bold lh-1">$4,5K</div>
              <div class="fs-7 text-gray-600 fw-semobold">Revenue</div>
            </div>
            <!--end::Title-->
          </div>
        </div>
        <!--end::Col-->

        <!--begin::Col-->
        <div class="col-6">
          <div class="d-flex align-items-center me-2">
            <!--begin::Symbol-->
            <div class="symbol symbol-40px me-3">
              <div class="symbol-label bg-body bg-opacity-50">
                <KTIcon icon-name="abstract-21" icon-class="fs-1 text-gray-900" />
              </div>
            </div>
            <!--end::Symbol-->

            <!--begin::Title-->
            <div>
              <div class="fs-5 text-gray-900 fw-bold lh-1">40</div>
              <div class="fs-7 text-gray-600 fw-semobold">Tasks</div>
            </div>
            <!--end::Title-->
          </div>
        </div>
        <!--end::Col-->

        <!--begin::Col-->
        <div class="col-6">
          <div class="d-flex align-items-center ms-2">
            <!--begin::Symbol-->
            <div class="symbol symbol-40px me-3">
              <div class="symbol-label bg-body bg-opacity-50">
                <KTIcon icon-name="abstract-44" icon-class="fs-1 text-gray-900" />
              </div>
            </div>
            <!--end::Symbol-->

            <!--begin::Title-->
            <div>
              <div class="fs-5 text-gray-900 fw-bold lh-1">$5.8M</div>
              <div class="fs-7 text-gray-600 fw-semobold">Sales</div>
            </div>
            <!--end::Title-->
          </div>
        </div>
        <!--end::Col-->
      </div>
      <!--end::Row-->
    </div>
  </div>
  <!--end::Mixed Widget 14-->
</template>

<script lang="ts">
import { getAssetPath } from "@/core/helpers/assets";
import { defineComponent } from "vue";

export default defineComponent({
  name: "widget-14",
  props: {
    widgetClasses: String,
    widgetColor: String,
  },
  setup() {
    return {
      getAssetPath,
    };
  },
});
</script>
