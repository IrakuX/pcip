import type { MenuItem } from "@/layouts/default-layout/config/types";

const MainMenuConfig: Array<MenuItem> = [
  {
    heading: "dashboard",
    route: "/dashboard",
    keenthemesIcon: "home-2",
    bootstrapIcon: "bi-bar-chart-line",
  },
  {
    sectionTitle: "catalogoOperativo",
    route: "/catalogosOperativos",
    keenthemesIcon: "abstract-35",
    bootstrapIcon: "bi-file-text",
    pages: [
      {
        sectionTitle: "almacenes",
        route: "/catalogosOperativos/warehouses",
        keenthemesIcon: "profile-circle",
        bootstrapIcon: "bi-buildings-fill",
        sub: [
          {
            heading: "inventarioInicial",
            route: "/catalogosOperativos/warehouses/inventarioInicial",
          },
          {
            heading: "traspasoAlmacenes",
            route: "/catalogosOperativos/warehouses/traspasoAlmacenes",
          },
        ],
      },
      {
        sectionTitle: "asentamientos",
        route: "/settlements",
        keenthemesIcon: "geolocation-home",
        bootstrapIcon: "bi-geo",
        sub: [
          {
            heading: "ciudades",
            route: "/settlements/cities",
          },
          {
            heading: "estados",
            route: "/settlements/states",
          },
          {
            heading: "municipios",
            route: "/settlements/municipalities",
          },
          {
            heading: "codigosPostales",
            route: "/settlements/postalCodes",
          },
        ],
      },
      {
        heading: "categoriaProductos",
        route: "/productCategory",
        keenthemesIcon: "element-7",
        bootstrapIcon: "bi-archive",
      },
      {
        sectionTitle: "monedas",
        route: "/coins",
        keenthemesIcon: "abstract-21",
        bootstrapIcon: "bi-currency-exchange",
        sub: [
          {
            heading: "tiposCambio",
            route: "/settlements/cities",
          },
          {
            heading: "importacionXml",
            route: "/settlements/states",
          },
        ],
      },
      {
        heading: "pedimentos",
        route: "/pedimentos",
        keenthemesIcon: "tablet-up",
        bootstrapIcon: "bi-box-arrow-in-up-right",
      },
      {
        heading: "productos",
        route: "/products",
        keenthemesIcon: "tablet-up",
        bootstrapIcon: "bi-boxes",
      },
      {
        sectionTitle: "sat",
        route: "/sat",
        keenthemesIcon: "abstract-21",
        bootstrapIcon: "bi-building",
        sub: [
          {
            heading: "fraccionesArancelarias",
            route: "/sat/tariffClassification",
          },
          {
            heading: "impuestos",
            route: "/sat/taxes",
          },
          {
            heading: "unidadesMedida",
            route: "/sat/unitsMeasurement",
          },
        ],
      },
      {
        heading: "unidadesMedida",
        route: "/unitsMeasurement",
        keenthemesIcon: "tablet-up",
        bootstrapIcon: "bi-rulers",
      },
      {
        heading: "maquinariaEquipo",
        route: "/machinery",
        keenthemesIcon: "tablet-up",
        bootstrapIcon: "bi-wrench-adjustable",
      }
    ],
  },
  {
    sectionTitle: "catalogoAdministrativo",
    route: "/apps",
    keenthemesIcon: "abstract-26",
    bootstrapIcon: "bi-layers",
    pages: [
      {
        heading: "contactos",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-people-fill",
      },
      {
        sectionTitle: "perfiles",
        route: "/customers",
        keenthemesIcon: "abstract-38",
        bootstrapIcon: "bi-person-fill-gear",
        sub: [
          {
            heading: "accesos",
            route: "/apps/customers/getting-started",
          }
        ],
      },
      {
        heading: "usuarios",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-people-fill",
      },
      {
        heading: "empleados",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-people-fill",
      },
    ],
  },
  {
    sectionTitle: "immex",
    route: "/apps",
    keenthemesIcon: "abstract-26",
    bootstrapIcon: "bi-layers",
    pages: [
      {
        sectionTitle: "moduloAduanas",
        route: "/customers",
        keenthemesIcon: "abstract-38",
        bootstrapIcon: "bi-boxes",
        sub: [
          {
            heading: "modulo1",
            route: "/apps/customers/getting-started",
          },
          {
            heading: "modulo2",
            route: "/apps/customers/getting-started",
          },
          {
            heading: "modulo3",
            route: "/apps/customers/getting-started",
          }
        ],
      },
      {
        sectionTitle: "reportesIMMEX",
        route: "/customers",
        keenthemesIcon: "abstract-38",
        bootstrapIcon: "bi-clipboard2-data-fill",
        sub: [
          {
            heading: "reporteImmex1",
            route: "/apps/customers/getting-started",
          },
          {
            heading: "reporteImmex2",
            route: "/apps/customers/getting-started",
          },
          {
            heading: "reporteImmex3",
            route: "/apps/customers/getting-started",
          },
          {
            heading: "reporteImmex4",
            route: "/apps/customers/getting-started",
          }
        ],
      },
    ],
  },
  {
    sectionTitle: "reportes",
    route: "/reports",
    keenthemesIcon: "abstract-26",
    bootstrapIcon: "bi-file-earmark-break-fill",
    pages: [
      {
        heading: "conectorExcel",
        route: "/reports/connector",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-file-earmark-excel-fill",
      },
      {
        sectionTitle: "reporteBasicos",
        route: "/reportes/basicos",
        keenthemesIcon: "abstract-38",
        bootstrapIcon: "bi-database-fill-down",
        sub: [
          {
            heading: "reporte1",
            route: "/reportes/basicos/reporte1",
          }
        ],
      },
      {
        heading: "tableroIndicadores",
        route: "/reports/board/general",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-graph-up-arrow",
      },
      {
        heading: "tableroProduccion",
        route: "/reports/board/production",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-graph-up-arrow",
      },
      {
        heading: "tableroInspeccion",
        route: "/reports/board/inspection",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-graph-up-arrow",
      }
    ],
  },
  {
    sectionTitle: "produccion",
    route: "/apps",
    keenthemesIcon: "abstract-26",
    bootstrapIcon: "bi-kanban-fill",
    pages: [
      {
        heading: "produccionDiaria",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-kanban-fill",
      },
      {
        heading: "pruebaTension",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-align-start",
      },
      {
        heading: "corte",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-scissors",
      },
      {
        heading: "inspeccion",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-journal-check",
      },
      {
        heading: "empaque",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-archive-fill",
      },
    ],
  },
  {
    sectionTitle: "transacciones",
    route: "/apps",
    keenthemesIcon: "abstract-26",
    bootstrapIcon: "bi-kanban-fill",
    pages: [
      {
        heading: "transaccion1",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-kanban-fill",
      },
      {
        sectionTitle: "transaccion3",
        route: "/customers",
        keenthemesIcon: "abstract-38",
        bootstrapIcon: "bi-truck",
        sub: [
          {
            heading: "transaccion2",
            route: "/apps/customers/getting-started",
          }
        ],
      },
      {
        heading: "transaccion4",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-receipt",
      },
      {
        heading: "transaccion5",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-clipboard2-pulse-fill",
      },
      {
        heading: "transaccion6",
        route: "/apps/calendar",
        keenthemesIcon: "calendar-8",
        bootstrapIcon: "bi-clipboard2-pulse-fill",
      },
    ],
  },
];

export default MainMenuConfig;
