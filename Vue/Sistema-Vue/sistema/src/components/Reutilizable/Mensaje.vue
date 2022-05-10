<template>
     <div>
            <v-snackbar
            right='right'
            top ='top'
            :value="activo"
            :color = tipo
            multi-line= 'multi-line'
            :timeout="8000"
            >
            {{ msj }}
            <v-btn
                dark
                text
                @click="Cerrar()"
            >
                Close
            </v-btn>
            </v-snackbar>
     </div>
</template>
<script>
export default {
    data(){
        return {
        snackbar:true,
        }
    },
    computed:{
         retornar(){                
            this.snackbar = this.activo;
            return  this.snackbar;
         },
         
    },
    props:{
        msj:null,
        activo:null,
        tipo:null,
    },
    methods:{
        enviarResult(){
            this.$emit('escucharResultMsj',false);
        },
        Cerrar(){
            this.enviarResult();
        }
    }
}
</script>