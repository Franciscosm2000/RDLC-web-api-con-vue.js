<template>
  <v-app id="inspire">

    <v-app-bar app >
      <v-app-bar-nav-icon  @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>RentaCar</v-toolbar-title>

      <v-spacer></v-spacer>

          <v-menu 
            bottom
            left
          >
           <template v-slot:activator="{ on, attrs }">
              <v-btn content="Opciones" v-tippy='{ placement : "bottom" }'
                dark
                icon
                v-bind="attrs"
                offset-y
                v-on="on"
              >
                <v-icon color="black">expand_more</v-icon>
              </v-btn>
            </template>
            <v-list>
              <v-list-item
                link
              >
                <v-list-item-title content="Cerrar seción" v-tippy='{ placement : "bottom" }' @click="Salir()">Salir</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
    </v-app-bar>

    <v-navigation-drawer
      v-model="drawer"
      app
      tile
      fixed
      class="color"
    >
    <v-list dense nav class="color-lista">
      <v-sheet
        color="transparent"
        class="pa-4"
      >
        <v-avatar
          class="mb-4"
          color="grey lighten-2"
          size="64"
        > <v-icon>perm_identity</v-icon></v-avatar>

        <!--<div style="color:white">Usuario: {{this.$store.state.usuario.usuario}}</div>
        <div style="color:white">Rol: {{this.$store.state.usuario.rol}}</div> -->
      </v-sheet>

      <v-divider></v-divider>

      <!--Home -->

      <template>

        <v-list-item
        :to="{ name: 'home'}"
          >
          <v-tooltip right>
            <template v-slot:activator="{ on, attrs }">
                  <v-list-item-icon>
                    <v-icon >home</v-icon>
                  </v-list-item-icon>

                  <v-list-item-content>
                    <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">Inicio</v-list-item-title>
                  </v-list-item-content>
            </template>
           <span>Inicio</span>
          </v-tooltip>
        </v-list-item>

      </template>

      <!--PERSONA -->
      <template  >

      <v-list-group prepend-icon="badge" color="primary">

        <v-list-item  slot="activator" >
          <v-list-item-content>
            <v-list-item-title  class="color-blanco">
              Persona
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-tooltip  right>
            <template v-slot:activator="{ on, attrs }">

        <v-list-item :to="{ name: 'cliente'}">
          <v-list-item-action>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">
              Cliente
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        </template>
           <span>Clientes</span>
        </v-tooltip>

        <v-tooltip  right>
            <template v-slot:activator="{ on, attrs }">

          <v-list-item :to="{ name: 'empleado'}">
            <v-list-item-action>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">
                Empleado
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

        </template>
           <span>Empleados</span>
        </v-tooltip>

      </v-list-group>
      </template>

      <!--VeHICULOS -->
      <template  >

      <v-list-group prepend-icon="local_shipping" color="primary">

        <v-list-item  slot="activator" >
          <v-list-item-content>
            <v-list-item-title  class="color-blanco">
              Vehiculo
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-tooltip  right>
            <template v-slot:activator="{ on, attrs }">

        <v-list-item :to="{ name: 'categoria'}">
          <v-list-item-action>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">
              Categoria
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        </template>
           <span>Modelos</span>
        </v-tooltip>

        <v-tooltip  right>
            <template v-slot:activator="{ on, attrs }">

          <v-list-item :to="{ name: 'modelo'}">
            <v-list-item-action>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">
                Modelo
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

        </template>
           <span>vehiculo</span>
        </v-tooltip>

        <v-tooltip  right>
            <template v-slot:activator="{ on, attrs }">

          <v-list-item :to="{ name: 'vehiculo'}">
            <v-list-item-action>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">
                Vehiculo
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

        </template>
           <span>vehiculo</span>
        </v-tooltip>

      </v-list-group>
      </template>

      <!--Combustible -->
      <template >
        <v-list-group  prepend-icon="local_gas_station">
        <v-list-item slot="activator">
          <v-list-item-content>
            <v-list-item-title  class="color-blanco">
              Rentas
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-tooltip  right>
            <template v-slot:activator="{ on, attrs }">

          <v-list-item :to="{ name: 'renta'}">
            <v-list-item-action>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">
                Rentar
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

        </template>
           <span>Registro de rentas</span>
        </v-tooltip>

      </v-list-group>
      </template>

      <!--REPORTES -->
      <template >
        <v-list-group prepend-icon="summarize">
        <v-list-item slot="activator">
          <v-list-item-content>
            <v-list-item-title  class="color-blanco">
              Reportes
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-tooltip right>
            <template v-slot:activator="{ on, attrs }">
              <v-list-item :to="{ name: 'reporte'}">
                <v-list-item-action>
                  <v-icon></v-icon>
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">
                    Renta
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
        </template>
           <span>Reporte de rentas</span>
        </v-tooltip>

      </v-list-group>
      </template>

      <!--Usuarios -->
      <template >
        <v-list-group prepend-icon="admin_panel_settings">
        <v-list-item slot="activator">
          <v-list-item-content>
            <v-list-item-title  class="color-blanco">
              Accesos
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-tooltip right>
          <template v-slot:activator="{ on, attrs }">
        <v-list-item :to="{ name: 'roles'}">
          <v-list-item-action>
            <v-icon>supervised_user_circle</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title  v-bind="attrs" v-on="on" class="color-blanco">
              Roles
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        </template>
           <span>Roles de usuario</span>
        </v-tooltip>

        <v-tooltip right>
            <template v-slot:activator="{ on, attrs }">

            <v-list-item :to="{ name: 'usuarios'}">
              <v-list-item-action>
                <v-icon>account_circle</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title v-bind="attrs" v-on="on" class="color-blanco">
                  Usuarios
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>

        </template>
           <span>Usuarios del sistema</span>
          </v-tooltip>

      </v-list-group>
      </template>
      </v-list>
    </v-navigation-drawer>

    <!--CENTRO LAYOUT as -->
    <v-main>
      <v-container fluid fill-height >
        <v-slide-y-transition mode="out-in" >
          <router-view />
        </v-slide-y-transition>
      </v-container>
    </v-main>

      <!-- FOOTER-->
    <v-footer padless  >
      <v-col
        class="text-center  "
        cols="12"

      >
        <strong >RentaCar - copyright @{{ new Date().getFullYear() }} </strong>
      </v-col>
    </v-footer>
  <!--FIN FOOTER-->
  </v-app>
</template>

<script>
  export default {
    data: () => ({
      drawer: null,
    }),

    computed: {
      /*logueado(){
        return this.$store.state.usuario;
      },*/

    },
    watch: {
    },
    created () {

       /*let token = localStorage.getItem("token")
        if(token == null){
            this.$store.dispatch("salir");
        }
        this.$store.dispatch("autoLogin");*/

    },
    methods:{
      //salir del login
        Salir(){
         // this.$store.dispatch("salir");
        },
    }
  }
</script>
<style >
.color >  .v-list-group, .v-list-item .v-icon {
  color: #f2f2f2 !important;
}
.color-blanco{
  color: #f2f2f2 !important;
}
.v-navigation-drawer > .v-list-group, .v-list-item__title {
  font-size: 14px !important;
  font-weight: bold;
}
.v-list-item__title{
  text-transform: uppercase !important;
}
.v-list-group__header__prepend-icon{
  margin-right: 15px !important;
}
.color{
  background: #212120 !important;
}
.v-divider{
  background: #f2f2f2;
  margin-left: 10%;
}
.container{
  background: #f2f2f2;
}
@media (max-width: 600px) {
    td{
    width:100% !important;
  }
}
.tippy-tooltip {
        font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif !important;
    }

</style>