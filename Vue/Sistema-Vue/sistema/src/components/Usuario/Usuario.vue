<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">
                <v-toolbar-title>Usuarios</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>
                    <v-spacer></v-spacer>
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer>

                    <v-dialog   v-model="dialog"
                        persistent
                        max-width="600px">
                    <template v-slot:activator="{ on, attrs }" @click="limpiar()">
                            <v-btn content="Nuevo registro" v-tippy='{ placement : "bottom" }'
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
                            <v-container grid-list-md>
                                <v-layout wrap>
                                <v-flex xs12 sm12 md12>
                                    <v-select v-model="idrol"
                                    :items="roles" label="Rol">
                                    </v-select>
                                </v-flex>
                                <v-flex xs12>
                                    <v-icon>group</v-icon>
                                    <span>  Datos Personales</span>
                                </v-flex>

                                <v-flex xs12 sm6 md6>
                                    <v-text-field v-model="primerNombre" label="Primer Nombre">
                                    </v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md6>
                                    <v-text-field v-model="segundoNombre" label="Segundo Nombre">
                                    </v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md6>
                                    <v-text-field v-model="primerApellido" label="Primer Apellido">
                                    </v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md6>
                                    <v-text-field v-model="segundoApellido" label="Segundo Apellido">
                                    </v-text-field>
                                </v-flex>

                                <v-flex xs12>
                                    <v-icon>remove_circle_outline</v-icon>
                                    <span>  Datos de login</span>
                                </v-flex>

                                <v-flex xs12 sm6 md6>
                                    <v-text-field v-model="nombre" label="Nombre Usuario">
                                    </v-text-field>
                                </v-flex>
                                <v-flex xs12 sm12 md12>
                                    <v-text-field  :append-icon="show1 ? 'visibility' : 'visibility_off'"   :type="show1 ? 'text' : 'password'"  @click:append="show1 = !show1"  v-model="password" label="Password">
                                    </v-text-field>
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
                                <v-btn color="blue darken-1" text @click.native="close">Cancelar</v-btn>
                                <v-btn color="blue darken-1" text @click.native="guardar">Guardar</v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>

                    <v-dialog persistent v-model="adModal" max-width="290">
                        <v-card>
                            <v-card-title class="headline" v-if="adAccion==1">¿Activar Item?</v-card-title>
                            <v-card-title class="headline" v-if="adAccion==2">¿Desactivar Item?</v-card-title>
                            <v-card-text>
                                Estás a punto de
                                <span v-if="adAccion==1">Activar </span>
                                <span v-if="adAccion==2">Desactivar </span>
                                el ítem {{ adNombre }}
                            </v-card-text>
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn text color="blue accent-4" style="color: white;" tex @click="activarDesactivarCerrar">
                                    Cancelar
                                </v-btn>
                                <v-btn v-if="adAccion==1" style="color: white;" color="green darken-4" text @click="activar">
                                    Activar
                                </v-btn>
                                <v-btn v-if="adAccion==2"  style="color: white;" color="red darken-3"   text @click="desactivar">
                                    Desactivar
                                </v-btn>
                            </v-card-actions>

                        </v-card>
                    </v-dialog>
                </v-toolbar>
                <v-data-table
                    :headers="headers"
                    :items="usuarios"
                    :search="search"
                >
                    <template v-slot:[`item.opciones`]="{ item }">
                        <v-icon small class="mr-2" content="Editar registro" v-tippy='{ placement : "bottom" }'  @click="editItem(item)">edit</v-icon>

                        <template v-if="item.condicion == false">
                            <v-icon content="Activar registro" v-tippy='{ placement : "bottom" }'
                            small
                            @click="activarDesactivarMostrar(1,item)"
                            >
                            check
                            </v-icon>
                        </template>


                        <template v-if="item.condicion == true">
                            <v-icon content="Desactivar registro" v-tippy='{ placement : "bottom" }'
                            small
                            @click="activarDesactivarMostrar(2,item)"
                            >
                            block
                            </v-icon>
                        </template>
                    </template>


                    <template v-slot:[`item.condicion`]="{ item }">
                            <v-chip v-if="item.condicion == true" color="green"
                            text-color="white">
                                    Activo
                            </v-chip >
                            <v-chip  v-else color="red"
                                text-color="white">
                                Inactivo
                            </v-chip >
                    </template>


                    <template slot="no-data">
                    <v-btn color="primary" content="Listar registros" v-tippy='{ placement : "bottom" }' @click="listar">Resetear</v-btn> <h1 style="text-align:center">No hay datos</h1>
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
    import axios from 'axios'
    import Secion from '@/components/Reutilizable/Secion_vencida'
    import Mensaje from '@/components/Reutilizable/Mensaje';
    import PantallaCarga from '@/components/Reutilizable/PantallaCarga';
    export default {
        data(){
            return {
                show1: false,
                usuarios:[],
                dialog: false,
                headers: [
                    { text: 'Opciones', value: 'opciones', sortable: false },
                    { text: 'Nombre', value: 'usuario' },
                    { text: 'Rol', value: 'rol' },
                    {text:'Estado' ,value:'condicion'}
                ],
                search: '',
                editedIndex: -1,
                id: '',
                idrol:'',
                roles:[
                ],
                nombre:'',
                primerNombre:'',
                segundoNombre:'',
                primerApellido:'',
                segundoApellido:'',
                password:'',
                actPassword:false,
                passwordAnt:'',
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adId: '',
                //secion
                usuario:this.$store.state.usuario.usuario,
                login_:false,
                tipoMsj:'',
                msjError:'',
                activarError:false,
                carga : false
            }
        },
        computed: {
            formTitle () {
                return this.editedIndex === -1 ? 'Nuevo usuario' : 'Actualizar usuario'
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
            this.select();
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
                axios.get('api/USUARIO_CLASS/Listar',configuracion).then(function(response){
                    //console.log(response);
                    me.usuarios=response.data;
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
            select(){
                this.carga = true;
                let me=this;
                var rolesArray=[];
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('api/roles/Select',configuracion).then(function(response){
                    rolesArray=response.data;
                    rolesArray.map(function(x){
                        me.roles.push({text: x.nombre,value:x.id_rol});
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
            editItem (item) {
                this.id=item.idUsuario;
                this.idrol=item.idRol;
                this.nombre=item.usuario;
                this.primerNombre=item.primerNombre;
                this.segundoNombre = item.segundoNombre;
                this.primerApellido = item.primerApellido;
                this.segundoApellido = item.segundoApellido;
                this.password=item.password_hash;
                this.passwordAnt=item.password_hash;
                this.editedIndex=1;
                this.dialog = true
            },
            close () {
                this.dialog = false;
                this.limpiar();
            },
            limpiar(){
                this.id="";
                this.idrol="";
                this.nombre="";
                this.password="";
                this.passwordAnt="";
                this.primerNombre="";
                this.segundoNombre="";
                this.primerApellido="";
                this.show1 = false;
                this.segundoApellido="";
                this.actPassword=false;
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
                    //Código para guardar
                    this.carga = true;
                    let me=this;
                    if (me.password!=me.passwordAnt){
                        me.actPassword=true;
                    }
                    axios.put('api/USUARIO_CLASS/Actualizar',{
                        'idusuario':me.id,
                        'idrol':me.idrol,
                        'nombreUsuario':me.nombre,
                        'primerNombre':me.primerNombre,
                        'segundoNombre':me.segundoNombre,
                        'primerApellido':me.primerApellido,
                        'segundoApellido':me.segundoApellido,
                        'password':me.password,
                        'act_password':me.actPassword,
                        'usuario_modificacion':me.usuario
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
                    axios.post('api/USUARIO_CLASS/Crear',{
                        'idrol':me.idrol,
                        'nombreUsuario':me.nombre,
                        'primerNombre':me.primerNombre,
                        'segundoNombre':me.segundoNombre,
                        'primerApellido':me.primerApellido,
                        'segundoApellido':me.segundoApellido,
                        'password':me.password,
                        'usuario_creacion':me.usuario
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar ();
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
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (this.nombre.length<3 || this.nombre.length>100){
                    this.validaMensaje.push("El nombre debe tener más de 3 caracteres y menos de 100 caracteres.");
                }
                if (!this.idrol){
                    this.validaMensaje.push("Seleccione un rol.");
                }
                if(!this.primerNombre){
                    this.validaMensaje.push("El primer nombre es obligatorio.");
                }
                if(!this.primerApellido){
                    this.validaMensaje.push("El primer apellido es obligatorio.");
                }
                if (!this.password){
                    this.validaMensaje.push("Ingrese el password del usuario.");
                }
                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },
            activarDesactivarMostrar(accion,item){
                this.adModal=1;
                this.adNombre=item.nombreUsuario;
                this.adId=item.idUsuario;
                if (accion==1){
                    this.adAccion=1;
                }
                else if (accion==2){
                    this.adAccion=2;
                }
                else{
                    this.adModal=0;
                }
            },
            activarDesactivarCerrar(){
                this.adModal=0;
            },
            activar(){
                this.carga = true;
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.put('api/USUARIO_CLASS/Activar/'+this.adId+'/'+this.usuario,{},configuracion).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;
                    me.adNombre="";
                    me.adId="";
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
                axios.put('api/USUARIO_CLASS/Desactivar/'+this.adId+'/'+this.usuario,{},configuracion).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;
                    me.adNombre="";
                    me.adId="";
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
