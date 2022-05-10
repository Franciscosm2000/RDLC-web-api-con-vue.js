<template>
     <v-dialog
     v-model="retornar"
      persistent
      width="300">
              <v-card
                color="blue darken-4"
                
            >
                <v-card-text>
                <span style="color: white;"> Porfavor espere...</span>
                <v-progress-linear
                    indeterminate
                    color="white"
                    class="mb-0"
                ></v-progress-linear>
                </v-card-text>
            </v-card>
     </v-dialog>
</template>
<script>
export default {
    data(){
        return {
        overlay:false,
        }
    },
    computed:{
         retornar(){                
            this.overlay = this.activo;
            return  this.overlay;
         },
         
    },
    props:{
        activo:null,
    },
    methods:{
        enviarResult(){
            this.$emit('escucharResultPantalla',false);
        }
    }
}
</script>