<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">
                <v-toolbar-title>Roles</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>

                    <template >
                        <v-btn content="Nuevo registro" v-tippy='{ placement : "bottom" }'
                        color="primary"
                        dark
                        @click="nuevo()"
                        >
                        Nuevo
                        </v-btn>
                    </template>

                     <v-dialog  v-model="dialog"
                        persistent
                        max-width="600px">

                        <v-card>
                            <v-card-title>
                            <span class="headline">{{ formTitle }}</span>
                            </v-card-title>
                            <v-card-text>

                            <v-container grid-list-md>
                                <v-layout wrap>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="nombre_rol" label="Nombre"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="descripcion" label="Descripcion"></v-text-field>
                                </v-flex>
                                 <v-flex xs12 sm12 md12>
                                  <label >  <v-icon>gpp_good</v-icon> PERMISOS</label>
                                </v-flex>
                                <v-flex>
                                    <template>
                                        <v-treeview
                                           v-model="selection"
                                            selectable
                                            item-disabled="locked"
                                            :items="items"
                                        ></v-treeview>
                                        </template>
                                </v-flex>
                                <v-flex xs12 sm12 md12 v-show="valida">
                                    <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                                    </div>
                                </v-flex>
                                </v-layout>
                            </v-container>
                            </v-card-text>

                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="blue darken-1" text @click.native="dialog=false">Cancelar</v-btn>
                                <v-btn color="blue darken-1" v-if="editedIndex!=0" text @click.native="guardar()">Guardar</v-btn>
                            </v-card-actions>
                        </v-card>
                     </v-dialog>
            </v-toolbar>

            <!--DIALOGO    transition="dialog-bottom-transition"-->
            <v-dialog v-model="adModalDesactivar" max-width="290" persistent   transition="dialog-top-transition">
                <v-card>
                    <v-card-title class="headline" v-if="adAccion==1">¿Activar Item?</v-card-title>
                    <v-card-title class="headline" v-if="adAccion==2">¿Desactivar Item?</v-card-title>
                    <v-card-text>
                        Estás a punto de
                        <span v-if="adModalDesactivar==1">Activar </span>
                        <span v-if="adModalDesactivar==2">Desactivar </span>
                        el ítem con Id {{adIdTipo}}
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn class="colorBtnDialog" text color="blue accent-4" style="color: white;"  @click="adModalDesactivar=0">
                            Cancelar
                        </v-btn>
                        <v-btn class="colorBtnDialog" text v-if="adAccion==1" style="color: white;" color="green darken-4"  @click="activar">
                            Activar
                        </v-btn>
                        <v-btn class="colorBtnDialog" text v-if="adAccion==2" style="color: white;" color="red darken-3"  @click="desactivar">
                            Desactivar
                        </v-btn>
                    </v-card-actions>

                </v-card>
            </v-dialog>

            <v-data-table
                :headers="headers"
                :items="roles"
                :search="search"
                class="elevation-1"
            >
            <template v-slot:[`item.opciones`]="{ item }">
                   <v-icon  content="Mostrar registro" v-tippy='{ placement : "bottom" }' v-if="item.nombre !='Administrador'"  class="mr-2"  @click="mostrar(item)">preview</v-icon>
                   <v-icon content="Editar registro" v-tippy='{ placement : "bottom" }' small class="mr-2" v-if="item.estado == true  && item.nombre !='Administrador'" @click="editItem(item)">edit</v-icon>

                    <template v-if="item.estado == false && item.nombre !='Administrador'">
                        <v-icon content="Activar registro" v-tippy='{ placement : "bottom" }'
                        small
                        @click="activarDesactivarMostrar(1,item)"
                        >
                        check
                        </v-icon>
                    </template>


                    <template v-if="item.estado == true  && item.nombre !='Administrador'">
                        <v-icon content="Desactivar registro" v-tippy='{ placement : "bottom" }'
                        small
                        @click="activarDesactivarMostrar(2,item)"
                        >
                        block
                        </v-icon>
                    </template>
                </template>
                <template v-slot:[`item.estado`]="{ item }">
                        <v-chip v-if="item.estado == true" color="green"
                        text-color="white">
                                Activo
                        </v-chip >
                        <v-chip  v-else color="red"
                            text-color="white">
                            Inactivo
                        </v-chip >
                </template>

                <template slot="no-data">
                <v-btn color="primary" @click="listar" content="Listar registros" v-tippy='{ placement : "bottom" }'>Resetear</v-btn> <h1 style="text-align:center">No hay datos</h1>
                </template>
            </v-data-table>

            <!-- PANTALLA DE CARGA-->
            <PantallaCarga :activo="carga" @escucharResultPantalla="resultPantalla()"> </PantallaCarga>
             <!-- DIALOGO DE ERROR-->
           <Mensaje :activo="activarError" :msj="msjError" :tipo="tipoMsj" @escucharResultMsj="resultMsj()"> </Mensaje>
            <!--- CIERRE DE SECION --->
            <Secion :activo="login_" @escucharResult="resultHijo()" ></Secion>
        </v-flex>
    </v-layout>
</template>
<script>
    import axios from 'axios';
    import Secion from '@/components/Reutilizable/Secion_vencida'
    import Mensaje from '@/components/Reutilizable/Mensaje';
    import PantallaCarga from '@/components/Reutilizable/PantallaCarga';
    export default {
        data(){
            return {
                carga :false,
                roles:[],
                dialog: false,
                headers: [
                    { text: 'Opciones', value: 'opciones' },
                    { text: 'Nombre', value: 'nombre' },
                    { text: 'Descripción', value: 'descripcion', sortable: false  },
                    { text: 'Usuario Creación', value: 'usuario_creacion',sortable: false},
                    { text: 'Fecha Creación', value: 'fecha_registro'  },
                    { text: 'Usuario Modificación', value: 'usuario_modificacion', sortable: false  },
                    { text: 'Fecha Modificación', value: 'fecha_modificacion', sortable: false  },
                    { text: 'Estado', value: 'estado'},
                ],
                editedIndex: -1,
                nombre_rol:'',
                id_rol:0,
                descripcion:'',
                estado:false,
                search: '',
                login_:false,
                tipoMsj:'',
                msjError:'',
                activarError:false,
                validaMensaje:[],
                valida:0,
                adAccion: 0,
                adModalDesactivar : 0,
                adIdTipo:0,
                selection:[],
                items: [
                    /*EMPLEDADO*/
                    {
                        id: 1,
                        name: 'Empleados',
                        children: [
                            { id: 101, name: 'Cargos : Empleados', children:[
                                {id: 1011, name: 'Lectura'},
                                {id: 1012, name: 'Escritura'},
                                {id: 1013, name: 'Modificación'}
                            ]},
                            { id: 102, name: 'Empleado : Empleados' , children:[
                                {id: 1021, name: 'Lectura'},
                                {id: 1022, name: 'Escritura'},
                                {id: 1023, name: 'Modificación'}
                            ]},
                        ],
                    },
                    /*COMBUSTIBLE*/
                    {
                        id: 2,
                        name: 'Combustibles',
                        children: [
                            //Ordenes
                            { id: 201, name: 'Ordenes : Combustible', children:[
                             //tipo orden
                             { id: 2011, name: 'Tipo orden : Ordenes', children:[
                                {id: 20111, name: 'Lectura'},
                                {id: 20112, name: 'Escritura'},
                                {id: 20113, name: 'Modificación'}
                              ]},
                              //tipo servicios
                              { id: 2012, name: 'Tipo servicio : Ordenes', children:[
                                {id: 20121, name: 'Lectura'},
                                {id: 20122, name: 'Escritura'},
                                {id: 20123, name: 'Modificación'}
                              ]},
                              //orden
                              { id: 2013, name: 'Orden : Ordenes', children:[
                                {id: 20131, name: 'Lectura'},
                                {id: 20132, name: 'Escritura'},
                                {id: 20133, name: 'Modificación'}
                              ]},
                            ]},
                            //Vehiculos
                            { id: 202, name: 'Vehiculos : Combustible', children:[
                             //tipo orden
                             { id: 2021, name: 'Tipo combustible : Vehiculos', children:[
                                {id: 20211, name: 'Lectura'},
                                {id: 20212, name: 'Escritura'},
                                {id: 20213, name: 'Modificación'}
                              ]},
                              //tipo servicios
                              { id: 2022, name: 'Vehiculo : Vehiculos', children:[
                                {id: 20221, name: 'Lectura'},
                                {id: 20222, name: 'Escritura'},
                                {id: 20223, name: 'Modificación'}
                              ]},
                            ]},
                        ],
                    },
                    /*COMPROBANTES*/
                    {
                        id: 3,
                        name: 'Comprobante',
                        children: [
                            { id: 301, name: 'Comprobantes : Comprobante', children:[
                                {id: 3011, name: 'Lectura'},
                                {id: 3012, name: 'Escritura'},
                                {id: 3013, name: 'Modificación'}
                            ]},
                        ],
                    },
                    /*REPORTE*/
                    {
                        id: 4,
                        name: 'Reportes',
                        children: [
                            { id: 401, name: 'Combustible : Reportes', children:[
                                {id: 4011, name: 'Vista'},
                            ]},
                            { id: 402, name: 'Comprobante : Reportes', children:[
                                {id: 4021, name: 'Vista'},
                            ]}
                        ],
                    },
                ],
                usuario:this.$store.state.usuario.usuario,
            }
        },
        computed: {
            formTitle () {

                switch(this.editedIndex){
                     case -1 :
                        return 'Nuevo Rol';
                     break;
                     case 1 :
                        return 'Actualizar Rol';
                     break;
                     case 0 :
                         var estado =  this.estado === true ? 'Activo': 'Desactivado';
                         return 'Rol ('+this.nombre_rol+'), estado : '+ estado;
                     break;
                }

            }
        },
        watch: {
        },
        components:{
          Secion,
          Mensaje,
          PantallaCarga
        },
        created () {
            this.listar();
        },
        methods:{
            //SECION
            resultHijo(){
                this.login_ = false;
            },
            resultMsj(){
               this.activarError = false;
            },
            resultPantalla(){
                this.carga = false;
            },
            activarErrores(tipo,err,color){
                if(tipo == 1){
                    this.login_ =true;
                }else{
                    this.activarError=true;
                    this.msjError = err;
                    this.tipoMsj = color;
                    setTimeout(this.resultMsj,2000);
                }
            },
            //FIN - SECION
            editItem (item) {
                this.buscar_rol(item.id_rol);

                this.editedIndex=1;
                this.dialog = true
            },
            listar(){
                this.carga = true; //pantalla de carga
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Roles/Listar',configuracion).then(function(response){
                    me.roles=response.data;
                    me.resultPantalla(); //Cierre de pantalla
                }).catch(err => {
                    console.log(err);
                    me.resultPantalla() //Cierre de pantalla
                    if (err.response.status==401){
                        me.activarErrores(1);
                    }
                    else if (err.response.status == 403){
                        me.activarErrores(2,"Error de permisos.","orange");
                    }
                    else{
                        me.activarErrores(2,err.response.data,"red");
                    }
                    console.log(err.response);
                });
            },
            nuevo(){
                this.dialog = true;
                this.editedIndex = -1;
                this.limpiar();
                //this.close();
            },
            close(){
                this.limpiar();
                this.dialog = false;
            },
            limpiar(){
                this.id_rol = 0;
                this.descripcion = '';
                this.nombre_rol ='';
                this.selection=[];

            },
            guardar () {

                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                if(this.editedIndex === 1){
                    //Código para editar
                    this.carga = true;
                    let me=this;
                    axios.put('api/Roles/Actualizar',{
                        'id_rol':me.id_rol,
                        'tipo':me.nombre_rol,
                        'descripcion':me.descripcion,
                        'permisos':me.selection,
                        'usuario_modificacion':me.usuario
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        //me.limpiar();
                        me.resultPantalla();
                        me.activarErrores(2,"Guardado correctamente.","green");
                    }).catch(err => {
                        me.resultPantalla(); //Cierre de pantalla
                        if (err.response.status==401){
                            me.activarErrores(1);
                        }
                        else if (err.response.status == 403){
                            me.activarErrores(2,"Error de permisos.","orange");
                        }
                        else{
                            me.activarErrores(2,err.response.data,"red");
                        }
                    });
                }
                else if(this.editedIndex === -1){

                    //if (this.editedIndex > -1) {
                        //Código para editar
                        this.carga = true;
                        let me=this;
                        axios.post('api/Roles/Crear',{
                            'nombre_rol':me.nombre_rol,
                            'descripcion':me.descripcion,
                            'permisos':me.selection,
                            'usuario_registro':me.usuario
                        },configuracion).then(function(response){
                            me.close();
                            me.listar();
                            //me.limpiar();
                            me.resultPantalla();
                            me.activarErrores(2,"Guardado correctamente.","green");
                        }).catch(err => {
                            me.resultPantalla(); //Cierre de pantalla
                            if (err.response.status==401){
                                me.activarErrores(1);
                            }
                            else if (err.response.status == 403){
                                me.activarErrores(2,"Error de permisos.","orange");
                            }
                            else{
                                me.activarErrores(2,err.response.data,"red");
                            }
                        });
                }
            },
            buscar_rol(id_rol){
                this.carga = true; //pantalla de carga
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Roles/Buscar/'+id_rol,configuracion).then(function(response){
                   var rol_bus=[];

                    rol_bus=response.data;
                    //general
                    me.id_rol = id_rol;
                    me.nombre_rol = rol_bus.nombre_rol;
                    me.descripcion= rol_bus.descripcion;
                    me.estado = rol_bus.estado;
                    //permisos
                    me.selection = rol_bus.permisos;
                    me.resultPantalla(); //Cierre de pantalla
                }).catch(err => {
                    console.log(err);
                    me.resultPantalla() //Cierre de pantalla
                    if (err.response.status==401){
                        me.activarErrores(1);
                    }
                    else if (err.response.status == 403){
                        me.activarErrores(2,"Error de permisos.","orange");
                    }
                    else{
                        me.activarErrores(2,err.response.data,"red");
                    }
                    console.log(err.response);
                });
            },
            mostrar(item){
                //busqueda de data
                this.buscar_rol(item.id_rol);

                this.editedIndex = 0;
                this.dialog = true;
            },
            activarDesactivarMostrar(accion,item){
                this.adModalDesactivar=1;
                this.adIdTipo=item.id_rol;
                if (accion==1){
                    this.adAccion=1;
                }
                else if (accion==2){
                    this.adAccion=2;
                }
                else{
                    this.adAccion=0;
                }
            },
            activar(){
                this.carga = true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.put('api/Roles/Activar/'+this.adIdTipo+'/'+this.usuario,{},configuracion).then(function(response){
                    me.adModalDesactivar=0;
                    me.adAccion=0;
                    me.adIdTipo="";
                    me.listar();
                    me.resultPantalla();
                }).catch(err => {
                    me.resultPantalla(); //Cierre de pantalla
                    if (err.response.status==401){
                        me.activarErrores(1);
                    }
                    else if (err.response.status == 403){
                        me.activarErrores(2,"Error de permisos.","orange");
                    }
                    else{
                        me.activarErrores(2,err.response.data,"red");
                    }
                });
            },
            desactivar(){
                this.carga = true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.put('api/Roles/Desactivar/'+this.adIdTipo+'/'+this.usuario,{},configuracion).then(function(response){
                    me.adModalDesactivar=0;
                    me.adAccion=0;
                    me.adIdTipo="";
                    me.listar();
                    me.resultPantalla();
                }).catch(err => {
                    me.resultPantalla(); //Cierre de pantalla
                    if (err.response.status==401){
                        me.activarErrores(1);
                    }
                    else if (err.response.status == 403){
                        me.activarErrores(2,"Error de permisos.","orange");
                    }
                    else{
                        me.activarErrores(2,err.response.data,"red");
                    }
                });
            },
        }
    }
</script>