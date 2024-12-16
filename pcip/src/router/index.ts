import {
  createRouter,
  createWebHistory,
  type RouteRecordRaw,
} from "vue-router";
import { useAuthStore } from "@/stores/auth";
import { useConfigStore } from "@/stores/config";
import Error404 from "@/views/crafted/authentication/Error404.vue";
import Error500 from "@/views/crafted/authentication/Error500.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    redirect: "/dashboard",
    component: () => import("@/layouts/default-layout/DefaultLayout.vue"),
    //meta: {
      //middleware: "auth",
    //},
    children: [
      {
        path: "/dashboard",
        name: "dashboard",
        component: () => import("@/views/Dashboard.vue"),
        meta: {
          pageTitle: "Dashboard",
          breadcrumbs: ["Dashboards"],
        },
      },
      {
        path: "/catalogosOperativos",
        name: "catalogosOperativos",
        component: () => import("@/components/page-layouts/Profile.vue"),
        meta: {
          breadcrumbs: ["Catálogos operativos", "Almacenes", "Warehouses"],
        },
        children: [
          {
            path: "/catalogosOperativos/warehouses/inventarioInicial",
            name: "inventarioInicial",
            component: () =>
              import(
                "@/views/catalogosOperativos/warehouses/InventarioInicial.vue"
              ),
            meta: {
              pageTitle: "Inventario inicial",
            },
          },
          {
            path: "/catalogosOperativos/warehouses/traspasoAlmacenes",
            name: "traspasoEntreAlmacenes",
            component: () =>
              import(
                "@/views/catalogosOperativos/warehouses/TraspasoEntreAlmacenes.vue"
              ),
            meta: {
              pageTitle: "Traspaso entre almacenes",
            },
          },
        ],
      },
      {
        path: "/reportes",
        name: "reportes",
        //component: () => import("@/components/page-layouts/Profile.vue"),
        meta: {
          breadcrumbs: ["reportes"],
        },
        children: [
          {
            path: "/reports/connector",
            name: "conectorExcel",
            component: () =>
              import(
                "@/views/reports/Connector.vue"
              ),
            meta: {
              pageTitle: "Conector Excel",
            },
          },
          {
            path: "/reportes/basicos/reporte1",
            name: "listadoContactos",
            component: () =>
              import(
                "@/views/reportes/basicos/reporte1.vue"
              ),
            meta: {
              pageTitle: "Listado de contactos",
            },
          },
          {
            path: "/reports/board/general",
            name: "tableroGeneral",
            component: () =>
              import(
                "@/views/reports/board/General.vue"
              ),
            meta: {
              pageTitle: "General Dashboard",
              breadcrumbs: ["reportes", "dashboard"],
            },
          },
          {
            path: "/reports/board/production",
            name: "tableroProduccion",
            component: () =>
              import(
                "@/views/reports/board/Production.vue"
              ),
            meta: {
              pageTitle: "Production Board",
              breadcrumbs: ["reportes", "dashboard"],
            },
          },
          {
            path: "/reports/board/inspection",
            name: "tableroInspeccion",
            component: () =>
              import(
                "@/views/reports/board/Inspection.vue"
              ),
            meta: {
              pageTitle: "Inspection Board",
              breadcrumbs: ["reportes", "dashboard"],
            },
          },
        ],
      },
    ],
  },
  {
    path: "/auth",
    redirect: "sign-in",
    component: () => import("@/layouts/AuthLayout.vue"),
    children: [
      {
        path: "sign-in",
        name: "sign-in",
        component: () =>
          import("@/views/auth/SignIn.vue"),
        meta: {
          pageTitle: "Access control",
        },
      },
      {
        path: "sign-up",
        name: "sign-up",
        component: () =>
          import("@/views/auth/SignUp.vue"),
        meta: {
          pageTitle: "Sign Up",
        },
      },
      {
        path: "password-reset",
        name: "password-reset",
        component: () =>
          import("@/views/auth/PasswordReset.vue"),
        meta: {
          pageTitle: "Forgot your password?",
        },
      },
    ],
  },
  {
    path: "/",
    component: () => import("@/layouts/SystemLayout.vue"),
    children: [
      {
        // the 404 route, when none of the above matches
        path: "/404",
        name: "404",
        component: Error404,
        meta: {
          pageTitle: "Error 404",
        },
      },
      {
        path: "/500",
        name: "500",
        component: Error500,
        meta: {
          pageTitle: "Error 500",
        },
      },
    ],
  },
  {
    path: "/:pathMatch(.*)*",
    redirect: "/404",
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
  scrollBehavior(to) {
    // If the route has a hash, scroll to the section with the specified ID; otherwise, scroll to the top of the page.
    if (to.hash) {
      return {
        el: to.hash,
        top: 80,
        behavior: "smooth",
      };
    } else {
      return {
        top: 0,
        left: 0,
        behavior: "smooth",
      };
    }
  },
});

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore();
  const configStore = useConfigStore();
  // current page view title
  document.title = `${to.meta.pageTitle} - ${import.meta.env.VITE_APP_NAME}`;

  // reset config to initial state
  configStore.resetLayoutConfig();

  // verify auth token before each page change
  authStore.verifyAuth();

  // before page access check if page requires authentication
  if (to.meta.middleware == "auth") {
    if (authStore.isAuthenticated) {
      next();
    } else {
      next({ name: "sign-in" });
    }
  } else {
    next();
  }
});

export default router;
