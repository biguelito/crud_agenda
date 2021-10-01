<template>
  <div>
    <div class="feature-intro" id="title">
      <h1>Agenda Pessoal</h1>
      <h1>Pessoa</h1>
    </div>

    <Button type="button" class="btn btn-primary m-2 fload-end" 
      @click="createModal()">
        Adicionar pessoa
    </Button>

    <div class="container">
      <DataTable :value="pessoas" dataKey="idPessoa" sortMode="multiple">
        <Column field="nome" header="Nome" :sortable="true"></Column>
        <Column field="email" header="Email" :sortable="true"></Column>
        <Column field="telefone" header="Telefone" :sortable="true"></Column>
        <Column header="Apagar" >
          <template #body="pessoa">
            <Button type="button" icon="pi pi-ban" @click="deletePessoa(pessoa.data.idPessoa.toString())"></Button>
          </template>
        </Column>
        <Column header="Editar">
          <template #body="pessoa">
            <Button type="button" icon="pi pi-pencil" @click="updateModal(pessoa)"></Button>
          </template>
        </Column>
      </DataTable>
    </div>

    <!-- Pode existir um erro sendo indicado aqui, mas essa linha esta de acordo com
    a documentacao do primevue, e o mais importante, ela funciona como devia e como se espera -->
    <Dialog header="Atualizar pessoa" footer="" v-model:visible="display" modal>
      <template #header>
        <h3>{{modalTitle}}</h3>
      </template>

      <div class="input-group mb-3">
        <span class="input-group-text">Nome da pessoa</span>
        <InputText type="text" v-model="nome"/>
      </div>

      <div class="input-group mb-3">
        <span class="input-group-text">Email da pessoa</span>
        <InputText type="text" v-model="email"/>
      </div>

      <div class="input-group mb-3">
        <span class="input-group-text">Telefone da pessoa</span>
        <InputText type="text" v-model="telefone"/>
      </div>

      <template #footer>
        <Button type="button" v-if="idPessoa==0" class="btn btn-primary" @click="createPessoa()">
            Criar
        </Button>
        <Button type="button" v-if="idPessoa!=0" class="btn btn-primary" @click="updatePessoa()">
            Atualizar
        </Button>
      </template>
    </Dialog>
  </div>
</template>

<script>
import axios from 'axios';
import {FilterMatchMode} from 'primevue/api';

export default {
  data() {
    return {
      pessoas:[],
      display:false,
      idPessoa:0,
      nome:"",
      email:"",
      telefone:"",
      modalTitle:"",
      filters: {
        "nome": {value: null, matchMode: FilterMatchMode.STARTS_WITH}
      }
    }
  },
  methods:{
    // Metodo que solicita ao back end todas as pessoas salvas
    async getPessoas(){
      try {
        var response = await axios.get('https://localhost:5001/api/Pessoa/');
        return response.data;
      } catch (apierror) {
        alert("Erro ao consultar api, certifique-se que o backend está rodando corretamente\nTambem cheque se não existe erro no endpoint")
        return this.pessoas;
      }
      
    },
    // Metodo que solicita ao banco a insercao de uma pessoa, se seus dados forem validos
    async createPessoa(){
      if (this.dadosValidos()){
        try {
          var response = await axios.post("https://localhost:5001/api/Pessoa/", {
            nome:this.nome,
            email:this.email,
            telefone:this.telefone
          });
          
          alert(response.data);
          this.getPessoas()
          .then(data => this.pessoas = data)
        } catch (apierror) {
          if (apierror.response){
            alert(apierror.response.data);
            return;
          }
          alert("Não foi possivel salvar essa pessoa");
        }
      }
    },
    // Metodo que solicita ao banco a exclusao de uma pessoa, se ela existir, ou seja, se o id informado estiver no banco
    async deletePessoa(id){
      if(confirm("Tem certeza disso?\nEsse contato será apagado permanentemente da sua agenda")){
        try {
          await axios.delete('https://localhost:5001/api/Pessoa/' + id.toString())
        } catch (apierror) {
          if (apierror.response){
            alert(apierror.response.data);
            return;
          }
        alert("Não foi possivel deletar essa pessoa\nVerifique se essa pessoa ainda está salva")
        }
      
        this.getPessoas()
        .then(data => this.pessoas = data)  
      }
    },
    // Metodo que solicita ao banco a atualizacao de uma pessoa, se ela existir, ou seja, se o id informado estiver no banco
    async updatePessoa(){
      if (this.dadosValidos()){
        try {
          var response = await axios.put('https://localhost:5001/api/Pessoa/' + this.idPessoa.toString(), {
            nome:this.nome,
            email:this.email,
            telefone:this.telefone
          })
          alert(response.data);
        } catch (apierror) {
          if (apierror.response){
            alert(apierror.response.data);
            return;
          }
          alert("Não foi possivel atualizar essa pessoa");
        }
        
        this.getPessoas()
        .then(data => this.pessoas = data)
      }
    },
    // Metodo que chama uma janela flutuante, utilizada para indicar os dados da pessoa a ser atualizada
    updateModal(pessoa){
      this.modalTitle = "Atualizar pessoa";
      this.display = true;
      this.idPessoa=pessoa.data.idPessoa;
      this.nome=pessoa.data.nome;
      this.email=pessoa.data.email;
      this.telefone=pessoa.data.telefone;
    },
    // Metodo que chama uma janela flutuante, utilizada para indicar os dados de uma nova pessoa a ser inserida no banco
    createModal(){
      this.modalTitle = "Criar pessoa";
      this.display = true;
      this.idPessoa="";
      this.nome="";
      this.email="";
      this.telefone="";
    },
    // Metodo que valida se os dados da pessoa a ser inserida, ou atualizada, estao validos e de acordo com o banco
    dadosValidos(){
      if (this.nome == ""){
        alert("Por favor, digite um nome valido");
        return false;
      }
      if (this.nome.length > 60){
        alert("Esse nome é grande demais, por favor insira um menor")
        return;
      }
      var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      if(this.email !="" && !re.test(this.email.toLowerCase())){
        alert("email invalido")
        return false;
      }
      re = /[0-9]{0,4}9[0-9]{8}/;
      if (this.telefone != "" && !re.test(this.telefone)){
        alert("telefone invalido\nCerifique-se de digitar 9 mais o numero sem [-]'\nDDD é valido, sem os parenteses")
        return false;
      }

      return true;
    }
  },
  mounted:function(){
    this.getPessoas()
    .then(data => this.pessoas = data)
    
  },
}

</script>

<style>
#app {
  -moz-osx-font-smoothing: grayscale;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  margin-left: 150px;
  color: #2c3e50;
}
</style>

<style>
#title {
  text-align: center;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  margin-top: 60px;
  color:black;
  font-size: 20px;
}
</style>