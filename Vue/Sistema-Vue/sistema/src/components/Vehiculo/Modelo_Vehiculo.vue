<template>
    <v-layout align-start>
        <v-flex>
            <!--CABEZA -->
            <v-toolbar flat color="white">
                <v-toolbar-title><v-icon>ballot</v-icon> Modelo de vehiculo</v-toolbar-title>
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
                                            <span>ID :
                                             <label v-text="id_categoria"></label></span>

                                           </v-chip>

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
                                            label="Categoria"
                                            v-model="id_categoria"
                                            :items="categorias"

                                        ></v-autocomplete>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="marca"
                                        label="Marca"
                                        hint="Ejemplo: Mazda,Toyota, etc"
                                        required
                                        >
                                        </v-text-field>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="modelo"
                                        label="Modelo"
                                        required
                                        >
                                        </v-text-field>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="combustible"
                                        label="Combustible"
                                        required
                                        >
                                        </v-text-field>
                                    </v-col>

                                    <v-col
                                        cols="6"
                                        sm="6"
                                        md="6"
                                    >
                                        <v-text-field v-model="tipo_carroceria"
                                        label="Tipo de carroceria"
                                        required
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
            <v-data-table
                :headers="headers"
                :items="modelos"
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

    export default {
        data(){
            return {
                modelos:[],
                categorias:[],
                dialog: false,
                headers: [
                    { text: 'Opciones', value: 'opciones',sortable: false },
                    { text: 'Id', value: 'id_modelo'},
                    { text: 'categoria', value: 'categoria' },
                    { text: 'marca', value: 'marca'},
                    { text: 'modelo', value: 'modelo'},
                    { text: 'combustible', value: 'combustible',sortable: false},
                    { text: 'tipo_carroceria', value: 'tipo_carroceria',sortable: false},
                    { text: 'Estado', value: 'estado'},
                ],
                search: '',
                editedIndex: -1,
                id_modelo:0,
                id_categoria: 0,
                marca: '',
                modelo:'',
                combustible:'',
                tipo_carroceria:'',
                valida: 0,
                validaMensaje:[],
                adAccion: 0,
                adModalDesactivar : 0,
                adIdTipo:0,
                valTipoAccion:'',
                //secion
                //usuario:this.$store.state.usuario.usuario,
                carga :false,
                login_:false,
                tipoMsj:'',
                msjError:'',
                activarError:false,

            }

        },
        computed: {
            formTitle () {
                return this.editedIndex === -1 ? 'Nueva Categoria' : 'Actualizar Categoria'
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
            listarTipo(){
                this.carga = true;
                let me=this;
                    var aux = [];
                     let header={"Authorization" : "Bearer " + this.$store.state.token};
                     let configuracion= {headers : header};
                        axios.get('api/Categorias/Select/sd',configuracion).then(function(response){
                            aux = response.data;
                            aux.map(function(x){
                             me.categorias.push({text: x.nombre,value:x.id_categoria});
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
            listar(){
                this.carga =true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/Modelo_Vehiculos/Listar',configuracion).then(function(response){
                    //console.log(response);
                    me.modelos=response.data;
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
                this.id_modelo = item.id_modelo;
                this.id_categoria = item.id_categoria;
                this.marca = item.marca;
                this.modelo = item.modelo;
                this.combustible = item.combustible;
                this.tipo_carroceria = item.tipo_carroceria;
                this.editedIndex=1;
                this.dialog = true
            },
            close () {
                this.dialog = false;
                this.limpiar();
            },
            limpiar(){
                this.id_modelo = 0;
                this.id_categoria = 0;
                this.marca = "";
                this.modelo = "";
                this.combustible = "";
                this.tipo_carroceria = "";
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
                    axios.put('api/Modelo_Vehiculos/Actualizar',{
                        'id_modelo':me.id_modelo,
                        'id_categoria':me.id_categoria,
                        'marca':me.marca,
                        'modelo':me.modelo,
                        'combustible':me.combustible,
                        'tipo_carroceria':me.tipo_carroceria,
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
                    axios.post('api/Modelo_Vehiculos/Crear',{
                        'id_categoria':me.id_categoria,
                        'marca':me.marca,
                        'modelo':me.modelo,
                        'combustible':me.combustible,
                        'tipo_carroceria':me.tipo_carroceria,
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

                if(!this.id_categoria){
                    this.validaMensaje.push("La categoria es un campo obligatorio.");
                }
                if(!this.marca){
                    this.validaMensaje.push("La marca es un campo obligatorio.");
                }
                if(!this.modelo){
                    this.validaMensaje.push("El modelo es un campo obligatorio.");
                }
                 if(!this.combustible){
                    this.validaMensaje.push("El combustible es un campo obligatorio.");
                }

                if (this.validaMensaje.length){
                    this.valida=1;
                }

                return this.valida;
            },
            activarDesactivarMostrar(accion,item){
                this.adModalDesactivar=1;
                this.adIdTipo=item.id_modelo;
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
                axios.put('api/Modelo_Vehiculos/Activar/'+this.adIdTipo+'/'+this.usuario,{},configuracion).then(function(response){
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
                axios.put('api/Modelo_Vehiculos/Desactivar/'+this.adIdTipo+'/'+this.usuario,{},configuracion).then(function(response){
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