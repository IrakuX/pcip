import type LayoutConfigTypes from "@/layouts/default-layout/config/types";

const config: LayoutConfigTypes = {
  "general": {
    "mode": "light",
    "iconsType": "solid"
  },
  "main": {
    "type": "default",
    "primaryColor": "#009EF7",
    "logo": {
      "dark": "media/logos/default-dark.svg",
      "light": "media/logos/default.svg"
    }
  },
  "illustrations": {
    "set": "dozzy-1"
  },
  "scrollTop": {
    "display": true
  },
  "header": {
    "display": true,
    "menuIcon": "bootstrap",
    //"menuIcon": "keenthemes",
    "width": "fluid",
    "fixed": {
      "desktop": true,
      "tabletAndMobile": true
    }
  },
  "toolbar": {
    "display": false,
    "width": "fluid",
    "fixed": {
      "desktop": true,
      "tabletAndMobile": true
    }
  },
  "pageTitle": {
    "display": true,
    "breadcrumb": true,
    "direction": "column"
  },
  "aside": {
    "display": true,
    "theme": "dark",
    "fixed": true,
    //"menuIcon": "keenthemes",
    "menuIcon": "bootstrap",
    "minimized": false,
    "minimize": true,
    "hoverable": true
  },
  "content": {
    "width": "fluid"
  },
  "footer": {
    "width": "fluid"
  }
};

export default config;
