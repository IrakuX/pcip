<template>
  <!--begin::Search form-->
  <div class="card rounded-0 bgi-no-repeat bgi-position-x-end bgi-size-cover" :style="`
      background-color: #663259;
      background-size: auto 100%;
      background-image: url(${getAssetPath('media/misc/taieri.svg')});
    `">
    <!--begin::body-->
    <div class="card-body container-xxl pt-10 pb-8">
      <!--begin::Title-->
      <div class="d-flex align-items-center">
        <h1 class="fw-semibold me-3 text-white">Search Jobs</h1>

        <span class="fw-semibold text-white opacity-50">Job Management System</span>
      </div>
      <!--end::Title-->

      <!--begin::Wrapper-->
      <div class="d-flex flex-column">
        <!--begin::Block-->
        <div class="d-lg-flex align-lg-items-center">
          <!--begin::Simple form-->
          <div
            class="rounded d-flex flex-column flex-lg-row align-items-lg-center bg-body p-5 w-xxl-850px h-lg-60px me-lg-10 my-5">
            <!--begin::Row-->
            <div class="row flex-grow-1 mb-5 mb-lg-0">
              <!--begin::Col-->
              <div class="col-lg-4 d-flex align-items-center mb-3 mb-lg-0">
                <KTIcon icon-name="magnifier" icon-class="fs-1 text-gray-500 me-1" />

                <!--begin::Input-->
                <input type="text" class="form-control form-control-flush flex-grow-1" name="search" value=""
                  placeholder="Your Search" />
                <!--end::Input-->
              </div>
              <!--end::Col-->

              <!--begin::Col-->
              <div class="col-lg-4 d-flex align-items-center mb-5 mb-lg-0">
                <!--begin::Desktop separartor-->
                <div class="bullet bg-secondary d-none d-lg-block h-30px w-2px me-5"></div>
                <!--end::Desktop separartor-->

                <KTIcon icon-name="element-11" icon-class="fs-1 text-gray-500 me-1" />

                <!--begin::Select-->
                <select class="form-select border-0 flex-grow-1">
                  <option value="1" selected>Category</option>
                  <option value="2">In Progress</option>
                  <option value="3">Done</option>
                </select>
                <!--end::Select-->
              </div>
              <!--end::Col-->

              <!--begin::Col-->
              <div class="col-lg-4 d-flex align-items-center">
                <!--begin::Desktop separartor-->
                <div class="bullet bg-secondary d-none d-lg-block h-30px w-2px me-5"></div>
                <!--end::Desktop separartor-->

                <KTIcon icon-name="geolocation" icon-class="fs-1 text-gray-500 me-3" />

                <!--begin::Link-->
                <a href="#" class="btn btn-color-muted px-0 text-start rounded-0 py-1"
                  id="kt_modal_select_location_target">
                  Location
                </a>
                <!--end::Link-->
              </div>
            </div>
            <!--end::Row-->

            <!--begin::Action-->
            <div class="min-w-150px text-end">
              <button type="submit" class="btn btn-dark" id="kt_advanced_search_button_1">
                Search
              </button>
            </div>
            <!--end::Action-->
          </div>
          <!--end::Simple form-->

          <!--begin::Action-->
          <div class="d-flex align-items-center">
            <a class="fw-semibold link-white fs-5" href="#">Advanced Search </a>
          </div>
          <!--end::Action-->
        </div>
        <!--end::Block-->
      </div>
      <!--end::Wrapper-->
    </div>
    <!--end::body-->
  </div>
  <!--end::Search form-->
</template>

<script lang="ts">
import { getAssetPath } from "@/core/helpers/assets";
import { defineComponent } from "vue";
import { toolbarWidthFluid } from "@/layouts/default-layout/config/helper";

export default defineComponent({
  name: "layout-toolbar",
  components: {},
  setup() {
    return {
      toolbarWidthFluid,
      getAssetPath,
    };
  },
});
</script>
