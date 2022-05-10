import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import Axios from 'axios'
import { VueMaskDirective } from 'v-mask'
Vue.directive('mask', VueMaskDirective);

Axios.defaults.baseURL = 'https://localhost:44303/';
Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
