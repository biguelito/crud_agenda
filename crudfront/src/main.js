import {createApp} from 'vue';
import Pessoa from './Pessoa.vue';
import PrimeVue from 'primevue/config';
import Dialog from 'primevue/dialog';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext'
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';


import 'primevue/resources/themes/saga-blue/theme.css'       //theme
import 'primevue/resources/primevue.min.css'                 //core css
import 'primeicons/primeicons.css'                        //icons


const app = createApp(Pessoa);

app.use(PrimeVue);

app.component('Dialog', Dialog);
app.component('InputText', InputText);
app.component('Button', Button);
app.component('DataTable', DataTable);
app.component('Column', Column);

app.mount('#app')