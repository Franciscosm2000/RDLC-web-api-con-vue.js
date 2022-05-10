<template>
    <v-layout align-start>
        <v-flex>
            <!--CABEZA -->
            <v-toolbar flat color="white">
                <v-toolbar-title v-if="nuevo === false" ><v-icon>ballot</v-icon> Rentas de vehiculo</v-toolbar-title>
                <v-toolbar-title v-if="nuevo === true" v-text="formTitle()"><v-icon>ballot</v-icon> </v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field v-if="nuevo === false"  class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>
                    <v-btn  v-if="nuevo === false"  content="Agregar nuevo registro" v-tippy='{ placement : "bottom" }'
                            color="primary" dark @click="nuevo_registro()">Nuevo</v-btn>
            </v-toolbar>
            
            <!--Dialogo para registrar -->
            <v-card
                elevation="1"
                outlined
                v-if="nuevo === true"
            >
                <v-card-text>
                    <span v-text="titulo" style="font-weight: bold;font-size: 1.5em;"></span>
                    <template>
                        <!--GENERAL-->
                        <v-expansion-panels>
                            <v-expansion-panel
                            >
                            <v-expansion-panel-header>
                                <span style="font-size:18px"> <v-icon slot="prepend">info</v-icon> General</span>
                            </v-expansion-panel-header>
                            <v-expansion-panel-content>
                            <v-row>
                                <v-col
                                    cols="6"
                                    sm="6"
                                    md="6"
                                >
                                     <v-autocomplete
                                            chips
                                            clearable
                                            filled
                                            dense
                                            label="Cliente"
                                            v-model="id_cliente"
                                            :items="clientes"

                                        ></v-autocomplete>
                                </v-col>
                                <v-col
                                    cols="6"
                                    sm="6"
                                    md="6"
                                >
                                    <v-autocomplete
                                            chips
                                            clearable
                                            filled
                                            dense
                                            label="Empleado"
                                            v-model="id_empleado"
                                            :items="empleados"

                                        ></v-autocomplete>
                                </v-col>

                            </v-row>
                            </v-expansion-panel-content>
                            </v-expansion-panel>
                        </v-expansion-panels>
                        <br>
                        <!--DETALLE-->
                        <v-expansion-panels>
                            <v-expansion-panel
                            >
                            <v-expansion-panel-header>
                                <span style="font-size:18px"> <v-icon slot="prepend">info</v-icon> Detalle</span>
                            </v-expansion-panel-header>
                            <v-expansion-panel-content>
                                <v-row >
                                   <v-col
                                    cols="6"
                                    sm="6"
                                    md="6"
                                >
                                     <v-autocomplete
                                            chips
                                            clearable
                                            filled
                                            dense
                                            label="Vehiculo"
                                            v-model="id_vehiculo"
                                            :items="vehiculos"

                                        ></v-autocomplete>
                                </v-col>

                                <v-col
                                    cols="6"
                                    sm="6"
                                    md="6"
                                >
                                      <v-text-field
                                            label="Fecha inicio"
                                            v-model="fecha_ini"
                                            type="date"
                                        ></v-text-field>
                                </v-col>

                                <v-col
                                    cols="6"
                                    sm="6"
                                    md="6"
                                        >
                                        <v-text-field
                                            label="Fecha fin"
                                            v-model="fecha_fin"
                                            type="date"
                                        ></v-text-field>
                                </v-col>

                                    <v-col cols="12"
                                        sm="12"
                                        md="12">
                                    <v-flex xs12 sm12 md12 v-show="validaDetalle">
                                        <div class="red--text" v-for="v in validaMensajeDetalle" :key="v" v-text="v">
                                        </div>
                                    </v-flex>
                                    </v-col>
                                </v-row>
                                <v-btn color="success" @click="agregarDetalle()">Agregar</v-btn>
                                <br>
                                <br>
                                <div><span style="font-size: 20px;font-weight: bold;">Elementos</span></div>
                                <br>
                                <v-data-table
                                    :headers="headers_detalle"
                                    :items="detalle_rentas"
                                    :search="search"
                                    class="elevation-1"
                                >
                                    <template v-slot:[`item.opciones`]="{ item }">
                                        <v-icon small class="mr-2" content="Eliminar registro de la lista" v-tippy='{ placement : "bottom" }' @click="eliminarDetalleCurso(detalle_rentas,item)">delete_forever</v-icon>
                                    </template>
                                    <template v-slot:[`item.vehiculo`]="{ item }">
                                        <v-text-field readonly v-model="item.vehiculo" ></v-text-field>
                                    </template>
                                    <template v-slot:[`item.fecha_ini`]="{ item }">
                                        <v-text-field readonly v-model="item.fecha_ini"  ></v-text-field>
                                    </template>
                                    <template v-slot:[`item.fecha_fin`]="{ item }">
                                        <v-text-field readonly v-model="item.fecha_fin" ></v-text-field>
                                    </template>
                                    <template v-slot:[`item.monto_dia`]="{ item }">
                                        <v-text-field  v-model="item.monto_dia" type="number" min="0" ></v-text-field>
                                    </template>
                                    <template v-slot:[`item.num_dia`]="{ item }">
                                        <v-text-field readonly v-model="item.num_dia" type="number" min="0" ></v-text-field>
                                    </template>
                                    <template v-slot:[`item.total`]="{ item }">
                                        <span style="margin-top:5px">{{item.monto_dia * item.num_dia}}</span>
                                    </template>
                                    

                                    <template slot="no-data">
                                        <h1>No hay datos.</h1>
                                    </template>
                                </v-data-table>
                            </v-expansion-panel-content>
                            </v-expansion-panel>
                        </v-expansion-panels>
                    </template>
                </v-card-text>

                <div style="margin-left: 20px;">
                    <v-flex xs12 sm12 md12 v-show="valida">
                        <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                        </div>
                    </v-flex>
                </div>

                <v-card-actions>
                    <v-btn color="accent" content="Regresar" v-tippy='{ placement : "bottom" }' @click="salir()">Cancelar</v-btn>
                    <v-btn color="success" content="Guardar datos" v-tippy='{ placement : "bottom" }'  @click="guardar()">Guardar</v-btn>
                    <v-spacer></v-spacer>

                </v-card-actions>
            </v-card>
            <!--Fin Dialogo para registrar -->

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
                        <v-btn class="colorBtnDialog" color="blue accent-4" text style="color: white;"  @click="adModalDesactivar=0">
                            Cancelar
                        </v-btn>
                        <v-btn class="colorBtnDialog" v-if="adAccion==1" text color="green darken-4" style="color: white;" @click="activar">
                            Activar
                        </v-btn>
                        <v-btn class="colorBtnDialog" v-if="adAccion==2" text color="red darken-3" style="color: white;" @click="desactivar">
                            Desactivar
                        </v-btn>
                    </v-card-actions>

                </v-card>
            </v-dialog>
            <!--TABLLA -->
            <v-data-table  v-if="nuevo === false"
                :headers="headers"
                :items="rentas"
                :search="search"
                class="elevation-1"
            >
                <template v-slot:[`item.opciones`]="{ item }">
                   <v-icon small class="mr-2" content="Editar registro" v-tippy='{ placement : "bottom" }' v-if="item.estado == true"  @click="editItem(item)">edit</v-icon>
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

                    <td>{{ props.item.id_categoria }}</td>
                    <td>{{ props.item.nombre }}</td>
                    <td>{{ props.item.descripcion }}</td>
                    <td>{{ props.item.fecha_modificacion }}</td>
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
                 <v-btn color="primary" @click="listar" content="Listar registros" v-tippy='{ placement : "bottom" }'>Resetear</v-btn><h1 style="text-align:center">No hay datos</h1>
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
    import moment from 'moment';

    export default {
        data(){
            return {
                rentas:[],
                dialog: false,
                clientes: [],
                empleados: [],
                vehiculos:[],
                nuevo:false,
                vehiculos_list:[],
                detalle_rentas:[],
                headers: [
                    { text: 'Opciones', value: 'opciones',sortable: false },
                    { text: 'Nª Renta', value: 'id_renta'},
                    { text: 'Cliente', value: 'cliente' },
                    { text: 'Empleado', value: 'empleado',sortable: false},
                    { text: 'Fecha', value: 'fecha',sortable: false},
                    { text: 'Estado', value: 'estado'},
                ],
                 headers_detalle: [
                    { text: 'Opciones', value: 'opciones',sortable: false },
                    { text: 'Vehiculo', value: 'vehiculo' },
                    { text: 'Fecha inicio', value: 'fecha_ini' },
                    { text: 'Fecha fin', value: 'fecha_fin' },
                    { text: 'Monto por dia', value: 'monto_dia' },
                    { text: 'Num dias', value: 'num_dia',sortable: false},
                    { text: 'Total', value: 'total',sortable: false  },
                ],
                search: '',
                editedIndex: -1,
                id_cliente: '',
                id_empleado:'',
                id_vehiculo:0,
                fecha_ini:'',
                fecha_fin:'',
                nombre: '',
                descripcion:'',
                valida: 0,
                validaDetalle:0,
                validaMensaje:[],
                validaMensajeDetalle:[],
                adAccion: 0,
                adModalDesactivar : 0,
                adIdTipo:0,
                valTipoAccion:'',
                titulo:'',
                //secion
                //usuario:this.$store.state.usuario.usuario,
                carga :false,
                login_:false,
                tipoMsj:'',
                msjError:'',
                activarError:false,

            }

        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },
        components:{
            Secion,
            Mensaje,
            PantallaCarga
        },

        created () {
           this.listarTipo();
           this.listarEmp();
           this.listarVehi();
           this.listarAllVeh();
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
            formTitle () {
                return this.editedIndex === -1 ? this.titulo ='Nueva Renta' : this.titulo ='Actualizar Renta'
            },
            listarTipo(){
                this.carga = true;
                let me=this;
                    var aux = [];
                     let header={"Authorization" : "Bearer " + this.$store.state.token};
                     let configuracion= {headers : header};
                        axios.get('api/Clientes/Select/sd',configuracion).then(function(response){
                            aux = response.data;
                            aux.map(function(x){
                             me.clientes.push({text:( x.cedula+'-'+x.nombre),value:x.id_cliente});
                             });
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
            listarEmp(){
                this.carga = true;
                let me=this;
                    var aux = [];
                     let header={"Authorization" : "Bearer " + this.$store.state.token};
                     let configuracion= {headers : header};
                        axios.get('api/Empleados/Select/sd',configuracion).then(function(response){
                            aux = response.data;
                            aux.map(function(x){
                             me.empleados.push({text:( x.cedula+'-'+x.nombre),value:x.id_empleado});
                             });
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
            listarVehi(){
                this.carga = true;
                let me=this;
                    var aux = [];
                     let header={"Authorization" : "Bearer " + this.$store.state.token};
                     let configuracion= {headers : header};
                        axios.get('api/Vehiculos/Select',configuracion).then(function(response){
                            aux = response.data;
                            aux.map(function(x){
                             me.vehiculos.push({text:( x.placa),value:x.id_vehiculo});
                             });
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
            listarAllVeh(){
                this.carga =true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Vehiculos/Listar',configuracion).then(function(response){
                    //console.log(response);
                    me.vehiculos_list=response.data;
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
            listar(){
                this.carga =true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Rentas/Listar',configuracion).then(function(response){
                    //console.log(response);
                    me.rentas=response.data;
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
            nuevo_registro(){
                this.limpiar();                
                this.nuevo = true;
            },
            salir(){
                this.nuevo = false;
                this.limpiar();
            },
            agregarDetalle(){
             var dat = this.valida_detalles()
             if ( dat === 1){
                    return;
                }

                var vehiculo_select = this.vehiculos_list.find(d=> d.id_vehiculo === this.id_vehiculo);
                var ini = moment(this.fecha_ini) ;
                var fin = moment(this.fecha_fin) ;
                var num_dias_ = fin.diff(ini, 'days') + 1;

                console.log(num_dias_);
                
                this.detalle_rentas.push(
                    {
                    vehiculo:vehiculo_select.placa,
                    id_vehiculo : this.id_vehiculo,
                    fecha_ini: this.fecha_ini,
                    fecha_fin: this.fecha_fin,
                    monto_dia : vehiculo_select.costo_dia,
                    num_dia:num_dias_
                    }
                );
                //this.limpiar();
                this.id_vehiculo = 0;
                this.fecha_ini = '';
                this.fecha_fin = '';

            },
            eliminarDetalleCurso(arr,item){
                var i= arr.indexOf(item);
                if(i!==-1){
                    arr.splice(i,1);
                }
            },
            valida_detalles(){
                this.validaDetalle=0;
                this.validaMensajeDetalle= [];
                if(!this.id_vehiculo || this.id_vehiculo < 1){
                    this.validaMensajeDetalle.push("El vehiculo es un campo obligatorio.");
                }
                if(!this.fecha_ini){
                    this.validaMensajeDetalle.push("La fecha inicio es un campo obligatorio.");
                }
                if(!this.fecha_fin){
                    this.validaMensajeDetalle.push("La fecha fin es un campo obligatorio.");
                }
                if(this.fecha_fin < this.fecha_ini){
                    this.validaMensajeDetalle.push("La fecha fin bo puede ser menor a la fecha inicio.");
                }
                
                if(this.validaMensajeDetalle.length > 0){
                    this.validaDetalle = 1;
                }

                return this.validaDetalle;
            },
            buscar_detalle(id){
                this.carga =true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Detalle_Rentas/Listar/'+id,configuracion).then(function(response){
                    //console.log(response);
                    me.detalle_rentas=response.data;
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
            editItem (item) {
                this.buscar_detalle(item.id_renta);
                this.id_renta = item.id_renta;
                this.id_cliente = item.id_cliente;
                this.id_empleado = item.id_empleado;
                this.editedIndex=1;
                this.nuevo = true
            },
            close () {
                this.dialog = false;
                this.limpiar();
            },
            limpiar(){
                this.id_empleado="";
                this.id_cliente = "";
                this.detalle_rentas = [];
                this.nuevo = false;
                this.validaMensaje = [];
                this.validaMensajeDetalle = [];
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
                    axios.put('api/Rentas/Actualizar',{
                        'id_renta':me.id_renta,
                        'id_cliente':me.id_cliente,
                        'id_empleado':me.id_empleado,
                        'detalle':me.detalle_rentas,
                        'usuario':"123"
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();
                        me.resultPantalla();
                        me.activarErrores(2,"Actualizado correctamente.","green");
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
                } else {
                    //Código para guardar
                    this.carga = true;
                    let me=this;
                    axios.post('api/Rentas/Crear',{
                        'id_cliente':me.id_cliente,
                        'id_empleado':me.id_empleado,
                        'detalle':me.detalle_rentas,
                        'usuario':"123"
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();
                        me.resultPantalla();
                        me.activarErrores(2,"Guardado correctamente.","green");
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
                }
            },
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if(!this.id_cliente){
                    this.validaMensaje.push("El cliente es un campo obligatorio.");
                }
                if(!this.id_empleado){
                    this.validaMensaje.push("El empleado es un campo obligatorio.");
                }
                if(!this.detalle_rentas.length || this.detalle_rentas.length < 1){
                    this.validaMensaje.push("El detalle es un campo obligatorio.");
                }


                if (this.validaMensaje.length){
                    this.valida=1;
                }

                return this.valida;
            },
            activarDesactivarMostrar(accion,item){
                this.adModalDesactivar=1;
                this.adIdTipo=item.id_renta;
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
                axios.put('api/Rentas/Activar/'+this.adIdTipo+'/'+this.usuario,{},configuracion).then(function(response){
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
            desactivar(){
                this.carga = true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.put('api/Rentas/Desactivar/'+this.adIdTipo+'/'+this.usuario,{},configuracion).then(function(response){
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