<template>
    <v-layout align-start>
        <v-flex>
            <!--CABEZA -->
            <v-toolbar flat color="white">
                <v-toolbar-title><v-icon>ballot</v-icon> Empleados</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>
                    <!--Dialogo para registrar -->
                    <v-dialog
                        v-model="dialog"
                        persistent
                        max-width="600px"
                        >
                        <template v-slot:activator="{ on, attrs }" @click="limpiar()">
                            <v-btn content="Agregar nuevo registro" v-tippy='{ placement : "bottom" }'
                            color="primary"
                            dark
                            v-bind="attrs"
                            v-on="on"
                            >
                            Nuevo
                            </v-btn>
                        </template>
                        <v-card>
                            <v-card-title>
                            <span class="headline">{{ formTitle }}</span>
                            </v-card-title>
                            <v-card-text>
                            <v-container>
                                <v-row>
                                    <v-col v-if="editedIndex==1" class="center"
                                        cols="12"
                                        sm="12"
                                        md="12"
                                    >
                                        <v-chip
                                            class="ma-2"
                                            color="light-blue darken-4"
                                            text-color="white"
                                             x-large
                                            >
                                            <span>Código :
                                             <label v-text="id_empleado"></label></span>

                                           </v-chip>

                                    </v-col>

                                    <v-col
                                        cols="12"
                                        sm="12"
                                        md="12"
                                    >
                                    <span style="font-size:18px"> <v-icon slot="prepend">info</v-icon> General</span>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="cedula"
                                        label="Cedula" v-mask="'###-#######-####A'"
                                        hint="Ejemplo: 001-0610200-1011H"
                                        required
                                        >
                                            <v-icon slot="prepend"
                                             color="light-blue darken-3">badge</v-icon>
                                        </v-text-field>
                                    </v-col>

                                    <v-col
                                        cols="12"
                                        sm="12"
                                        md="12"
                                    >
                                    <span style="font-size:18px "> <v-icon slot="prepend">drive_file_rename_outline</v-icon> Datos</span>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="primer_nombre"
                                        label="Primer Nombre"
                                        required
                                        >
                                        </v-text-field>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="segundo_nombre"
                                        label="Segundo Nombre"
                                        >
                                        </v-text-field>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="primer_apellido"
                                        label="Primer Apellido"
                                        required
                                        >
                                        </v-text-field>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="segundo_apellido"
                                        label="Segundo Apellido"

                                        >
                                        </v-text-field>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="numero"  v-mask="'(###) ####-####'"
                                        label="Telefono"

                                        >
                                        </v-text-field>

                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="direccion"
                                        label="Direccion"

                                        >
                                        </v-text-field>
                                    </v-col>
                                </v-row>
                                <v-flex xs12 sm12 md12 v-show="valida">
                                    <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                                    </div>
                                </v-flex>
                            </v-container>
                            </v-card-text>
                            <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn
                                color="blue darken-1"
                                text
                                @click="dialog = false"
                            >
                                Cerrar
                            </v-btn>
                            <v-btn
                                color="blue darken-1"
                                text
                                @click="guardar()"
                            >
                                Guardar
                            </v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>
                     <!--Fin Dialogo para registrar -->
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
                        <v-btn class="colorBtnDialog" text color="blue accent-4"  style="color: white;" @click="adModalDesactivar=0">
                            Cancelar
                        </v-btn>
                        <v-btn class="colorBtnDialog" text v-if="adAccion==1" color="green darken-4" style="color: white;"  @click="activar">
                            Activar
                        </v-btn>
                        <v-btn class="colorBtnDialog" text v-if="adAccion==2" color="red darken-3" style="color: white;" @click="desactivar">
                            Desactivar
                        </v-btn>
                    </v-card-actions>

                </v-card>
            </v-dialog>
            <!--TABLLA -->
            <v-data-table
                :headers="headers"
                :items="empleados"
                :search="search"
                class="elevation-1"
            >
                <template v-slot:[`item.opciones`]="{ item }">
                   <v-icon small class="mr-2" content="Editar registro" v-tippy='{ placement : "bottom" }'  @click="editItem(item)">edit</v-icon>
                    <template v-if="item.estado == false">
                        <v-icon content="Activar registro" v-tippy='{ placement : "bottom" }'
                        small
                        @click="activarDesactivarMostrar(1,item)"
                        >
                        check
                        </v-icon>
                    </template>
                    <template v-if="item.estado == true">
                        <v-icon content="Desactivar registro" v-tippy='{ placement : "bottom" }'
                        small
                        @click="activarDesactivarMostrar(2,item)"
                        >
                        block
                        </v-icon>
                    </template>
                </template>

                <template slot="items" slot-scope="props">

                    <td>{{ props.item.cedula }}</td>
                    <td>{{ props.item.nombre }}</td>
                    <td>{{ props.item.telefono }}</td>
                    <td>{{ props.item.direccion }}</td>

                </template>

                <template v-slot:[`item.estado`]="{ item }">
                        <v-chip  v-if="item.estado == true"
                            color="green"
                            text-color="white"
                            >
                            Activo
                        </v-chip>
                        <v-chip  v-else
                            color="red"
                            text-color="white"
                            >
                            Desactivado
                        </v-chip>
                </template>

                <template slot="no-data">
                 <v-btn color="primary" content="Listar registros" v-tippy='{ placement : "bottom" }' @click="listar">Resetear</v-btn> <h1 style="text-align:center">No hay datos</h1>
                </template>
            </v-data-table>

            <!-- PANTALLA DE CARGA-->
            <PantallaCarga :activo="carga" @escucharResultPantalla="resultPantalla()"> </PantallaCarga>
             <!-- DIALOGO DE ERROR-->
           <Mensaje :activo="activarError" :msj="msjError" :tipo="tipoMsj" @escucharResultMsj="resultMsj()"> </Mensaje>
            <!--- CIERRE DE SECION -->
            <Secion :activo="login_" @escucharResult="resultHijo()" ></Secion>

        </v-flex>
    </v-layout>
</template>

<script>
    import axios from 'axios'
    import Secion from '@/components/Reutilizable/Secion_vencida'
    import Mensaje from '@/components/Reutilizable/Mensaje';
    import PantallaCarga from '@/components/Reutilizable/PantallaCarga';

    export default {
        data(){
            return {
                empleados:[],
                dialog: false,
                headers: [
                    { text: 'Opciones', value: 'opciones',sortable: false },
                    { text: 'Código', value: 'cedula'},
                    { text: 'Nombre', value: 'nombre' },
                    { text: 'Telefono', value: 'telefono' },
                    { text: 'Direccion', value: 'direccion',sortable: false},
                    { text: 'Estado', value: 'estado',sortable: false},
                ],
                search: '',
                editedIndex: -1,
                cedula:'',
                id_empleado: '',
                primer_nombre:'',
                segundo_nombre:'',
                primer_apellido:'',
                segundo_apellido:'',
                numero:'',
                direccion:'',
                valida: 0,
                validaMensaje:[],
                adAccion: 0,
                adModalDesactivar : 0,
                adIdTipo:0,
                valTipoAccion:'',
                //secion
               // usuario:this.$store.state.usuario.usuario,
                login_:false,
                tipoMsj:'',
                msjError:'',
                activarError:false,
                carga : false

            }

        },
        computed: {
            formTitle () {
                return this.editedIndex === -1 ? 'Nuevo Empleado' : 'Actualizar Empleado'
            }
        },
        components:{
            Secion,
            Mensaje,
            PantallaCarga
        },
        watch: {
            dialog (val) {
            val || this.close()
            }
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
                    setTimeout(this.resultMsj,4000);
                }
            },
            //FIN - SECION
            listar(){
                this.carga = true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Empleados/Listar/as',configuracion).then(function(response){
                    //console.log(response);
                    me.empleados=response.data;
                    me.resultPantalla();
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
                });
            },
            editItem (item) {
                this.cedula = item.cedula;
                this.id_empleado = item.id_empleado;
                this.primer_nombre = item.primer_nombre;
                this.segundo_nombre = item.segundo_nombre;
                this.primer_apellido = item.primer_apellido;
                this.segundo_apellido = item.segundo_apellido;
                this.numero = item.telefono;
                this.direccion = item.direccion;
                this.editedIndex=1;
                this.dialog = true
            },
            close () {
                this.dialog = false;
                this.limpiar();
            },
            limpiar(){
                this.cedula = "";
                this.id_empleado=0;
                this.primer_nombre = "";
                this.segundo_nombre = "";
                this.primer_apellido = "";
                this.segundo_apellido = "";
                this.numero = '',
                this.direccion= '',
                this.validaMensaje =[];
                this.editedIndex=-1;
            },
            guardar () {
                if (this.validar()){
                    return;
                }
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                if (this.editedIndex > -1) {
                    //Código para editar
                    this.carga = true;
                    let me=this;
                    axios.put('api/Empleados/Actualizar',{
                            "id_empleado":me.id_empleado,
                            "cedula":me.cedula,
                            "primer_nombre":me.primer_nombre,
                            "segundo_nombre":me.segundo_nombre,
                            "primer_apellido":me.primer_apellido,
                            "segundo_apellido":me.segundo_apellido,
                            "telefono":me.numero,
                            "direccion":me.direccion
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();
                        me.resultPantalla(); //Cierre de pantalla
                        me.activarErrores(2,"Actualizado correctamente.","green");
                    }).catch(err => {
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
                    });
                } else {
                    //Código para guardar
                    this.carga = true;
                    let me=this;
                    axios.post('api/Empleados/Crear',{
                            "cedula":me.cedula,
                            "primer_nombre":me.primer_nombre,
                            "segundo_nombre":me.segundo_nombre,
                            "primer_apellido":me.primer_apellido,
                            "segundo_apellido":me.segundo_apellido,
                            "telefono":me.numero,
                            "direccion":me.direccion
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();
                        me.resultPantalla(); //Cierre de pantalla
                        me.activarErrores(2,"Guardado correctamente.","green");
                    }).catch(err => {
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
                    });
                }
            },
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if(!this.cedula){
                    this.validaMensaje.push("La cedula es un campo obligatorio.");
                }

                if(!this.primer_nombre || !this.primer_apellido){
                    this.validaMensaje.push("El primer nombre y el primer apellido son campo obligatorio.");
                }

                if (this.validaMensaje.length){
                    this.valida=1;
                }

                return this.valida;
            },
             activarDesactivarMostrar(accion,item){
                this.adModalDesactivar=1;
                this.adIdTipo=item.id_empleado;
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
                axios.put('api/Empleados/Activar/'+this.adIdTipo+'/'+this.usuario,{},configuracion).then(function(response){
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
                axios.put('api/Empleados/Desactivar/'+this.adIdTipo+'/'+this.usuario,{},configuracion).then(function(response){
                    me.adModalDesactivar=0;
                    me.adAccion=0;
                    me.adIdTipo="";
                    me.listar();
                    me.resultPantalla();
                }).catch(err => {me.resultPantalla(); //Cierre de pantalla
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

<style>
    .center{
        text-align: center;
    }
</style>