<template>
 <v-container fluid grid-list-md class="contenido">
    <v-layout wrap>
      <!-- RERPORTE SERVICIOS-->
      <v-flex xs12 sm6 md3>
        <v-hover>
          <v-card slot-scope="{ hover }"
                :class="`elevation-${hover ? 12 : 2}`"   @click="servicios_click = true" >

            <div class="imagen">
              <img
                src="data:image/png;base64,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"
              />
            </div>

            <v-card-title primary-title>
              <div class="texto">
                <h3 class="headline mb-0">Informe de servicio</h3>
                <div>
                  Se muestra de forma detallada los servicios agrupado por empleado.
                </div>
              </div>
            </v-card-title>

            <v-card-actions>
              <v-dialog
                v-model="servicios_click"
                scrollable
                max-width="800px"
                min-width="700px"
                max-heigth = "500px"
              >
                <v-card>
                  <v-card-text>
                    
                      <div id="fechas">
                        <v-text-field type="date" v-model="desdeS" label="Desde">
                        </v-text-field>
                         <v-spacer></v-spacer>
                        <v-text-field type="date" v-model="hastaS" label="Hasta">
                        </v-text-field>
                         <v-spacer></v-spacer>
                      </div>

                      <v-btn style="margin-left:10px" @click="BuscarRegistro('pdf')"
                        ><v-icon>print</v-icon>
                        PDF
                      </v-btn>
                      <v-btn style="margin-left:10px" @click="BuscarRegistro('xls')"
                        ><v-icon>print</v-icon>
                        EXCEL
                      </v-btn>
                      <v-btn style="margin-left:10px" @click="BuscarRegistro('doc')"
                        ><v-icon>print</v-icon>
                        WORD
                      </v-btn>

                        <div id="content">
                            <br>
                            <table class="tabla" v-for="dat in datos" :key="dat.empleado">

                                <span id="titulo">{{dat.empleado}}</span>
                                <tr>
                                <th class="tam-peque">Orden</th>
                                <th class="tam-peque">Placa</th>
                                <th>Servicio</th>
                                <th>Nota</th>
                                <th class="tam-peque">Total</th>
                                <th class="tam-peque">Fecha</th>
                                </tr>

                                <tr  v-for="data in dat.servicios" :key="data.id">
                                    <th v-text="data.id_orden"></th>
                                    <th v-text="data.placa"></th>
                                    <th v-text="data.servicio"></th>
                                    <th v-text="data.nota"></th>
                                    <th v-text="data.total"></th>
                                    <th v-text="data.fecha"></th>

                                </tr>
                            </table>
                        </div>
                  </v-card-text>
                </v-card>
              </v-dialog>
            </v-card-actions>
          </v-card>
        </v-hover>
      </v-flex>
    </v-layout>

    <!-- PANTALLA DE CARGA-->
    <PantallaCarga :activo="carga" @escucharResultPantalla="resultPantalla()"> </PantallaCarga>

    <!-- DIALOGO DE ERROR-->
    <Mensaje :activo="activarError" :msj="msjError" :tipo="tipoMsj" @escucharResultMsj="resultMsj()"> </Mensaje>
    <!--- CIERRE DE SECION -->
    <Secion :activo="login_" @escucharResult="resultHijo()" ></Secion>
  </v-container>

</template>
<script>
import jsPDF from 'jspdf'
import autoTable from 'jspdf-autotable';
import Secion from '@/components/Reutilizable/Secion_vencida'
import Mensaje from '@/components/Reutilizable/Mensaje';
import PantallaCarga from '@/components/Reutilizable/PantallaCarga';
import axios from 'axios'

export default {
    data(){
        return{
            //SERVICIOS
            datos:[],
            servicios_click:false,
            desdeS:'',
            hastaS:'',           
            //secion
            login_:false,
            tipoMsj:'',
            msjError:'',
            activarError:false,
            carga : false,
            //
            datos:[],
            datos_dat : [],
        }
    },
    components:{
            Secion,
            Mensaje,
            PantallaCarga
    },
    created(){
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

            //REPORTE SERVICIOS
            BuscarRegistro(type){
              if(!this.desdeS || !this.hastaS || this.desdeS > this.hastaS){
                alert('Verificar campos');
                return;
              }

               const url = 'api/Detalle_Rentas/Generar_Reporte/'+type+'/'+this.desdeS+'/'+this.hastaS;
                axios({
                  url: url,
                  method: "GET",
                  responseType: "blob", // importante
                  onDownloadProgress: (progressEvent) => {
                    var percentCompleted = Math.round((progressEvent.loaded * 100) /  progressEvent.total);
                    console.log(percentCompleted)
                  },
                }).then((response) => {
                  const url = window.URL.createObjectURL(new  Blob([response.data]));
                  const link = document.createElement("a");
                  link.href = url;
                  link.setAttribute("download", "Reporte."+type);
                  document.body.appendChild(link);
                  link.click();
                });
            },
        
     }
}
</script>
<style>
    .tabla {
    width: 100%;
    border: 1px solid #000;
    margin-bottom: 10px;
    }
    th, td {
    text-align: left;
    vertical-align: top;
    border-collapse: collapse;
    padding: 0.3em;
    caption-side: bottom;
    width: 30px;
    }
    #titulo{
        font-size: 20px;
        font-weight: bold;
    }
    #fechas{
      display: flex;
    width: 85%;
    }
    .imagen {
    text-align: center !important;
    }
    .imagen > img {
    width: 40% !important;
    }
    .texto {
    font-size: 14px;
    }
    #titulo{
      text-align: center !important;
      margin-bottom: 5%;
    }
    #cabecera {
      display: flex;
      flex-flow: row;
      margin-bottom: 2%;
      margin-left: 1%;
    }
    .caja1{
      display: flex;
      width: 70%;
      flex-flow: column;
      text-align: left;
      margin-left: 1%;
    }
    .caja2{
      display: flex;
      flex-flow: column;
      text-align: left;
    }
    #detalle{
      text-align: center;
      display: flex;
      flex-flow: row;
      margin-left: 1%;
    }
    #titulo-detalle{
      margin-bottom: 2%;
      text-align: center;
      font-weight: bold;
      text-decoration: underline;
    }
    #content{
      font-size: 15px;
    }
</style>