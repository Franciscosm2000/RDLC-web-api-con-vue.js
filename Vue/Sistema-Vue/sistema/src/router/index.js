import Vue from 'vue'
import VueRouter from 'vue-router'
import home from '../views/Home.vue'
//Empleado
import Cliente from '../components/Persona/Cliente.vue'
import Empleado from '../components/Persona/Empleado.vue'
//Combustible
  //Vehiculo
import Vehiculo from '../components/Vehiculo/Vehiculo.vue'
import Categoria from '../components/Vehiculo/Categoria.vue'
import Modelo_Vehiculo from '../components/Vehiculo/Modelo_Vehiculo.vue'
  //Ordenes
import Renta from '../components/Rentas/Renta.vue'
//Reportes 
import Reporte_combustible from '../components/Reporte/Combustible/Reporte.vue'
//Usuarios
import rol from '../components/Usuario/Rol.vue' 
import usuario from '../components/Usuario/Usuario.vue' 
//Login
import login from '../components/Login/Login.vue'
//Store
import store from '../store/index'

Vue.use(VueRouter)

//var x = false;

const routes = [
  {
    path: '/home',
    name: 'home',
    component: home,
    meta: {
      administrador : true,
      operador : true,
    }
  },
  {
    path: '/clientes',
    name: 'cliente',
    component: Cliente,
    meta: {
      administrador :true,
      operador : true,
    }
    
  },
  {
    path: '/empleado',
    name: 'empleado',
    component: Empleado,
    meta: {
      administrador : true,
      operador : true,
    }
  },
  {
    path: '/vehiculo',
    name: 'vehiculo',
    component: Vehiculo,
    meta: {
      administrador : true,
      operador : true
    }
  },
  {
    path: '/categorias',
    name: 'categoria',
    component: Categoria,
    meta: {
      administrador : true,
      operador : true
    }
  },
  {
    path: '/modelos',
    name: 'modelo',
    component: Modelo_Vehiculo,
    meta: {
      administrador : true,
      operador : true
    }
  },
  {
    path: '/rentas',
    name: 'renta',
    component: Renta,
    meta: {
      administrador : true,
      operador : true
    }
  },
  {
    path: '/reporte',
    name: 'reporte',
    component: Reporte_combustible,
    meta: {
      administrador : true,
      operador : true
    }
  },
  {
    path: '/roles',
    name: 'roles',
    component: rol,
    meta: {
      administrador : true,
    }
  },
  {
    path: '/usuarios',
    name: 'usuarios',
    component: usuario,
    meta: {
      administrador : true,
    }
  },
  {
    path: '/login',
    name: 'login',
    component: login,
    meta: {
      libre : true,
    }
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  }
]

const router = new VueRouter({
  routes
})

/*
router.beforeEach((to,from,next)=>{
  if (to.matched.some(record => record.meta.libre)){
    next()
  }
  else if (store.state.usuario && store.state.usuario.rol == 'Administrador'){
    if(to.matched.some(record => record.meta.administrador)){
      next()
    }
  }
  else if (store.state.usuario && store.state.usuario.rol_per == 'ACCESO_SISTEMA' && store.state.usuario.rol != 'Administrador'){
    if(to.matched.some(record => record.meta.operador)){
      next()
    }
  }
  else{
    next({
      name:'login'
    })
  }
});*/

export default router
