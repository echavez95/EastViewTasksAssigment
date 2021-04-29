<template>
    <div class="row">
        <div class="col s12">
            <div class="card">
                <div class="card-content">
                    <span class="card-title">Registro de Ciudadanos</span>
                    <div class="row">
                        <div class="col s12">
                            <form autocomplete="off">
                                <div class="row">
                                    <div class="input-field col s12 m6 l6">
                                        <Matinput ref="citizenIdCard" id="idcard" type="text" label="No. Identidad" v-model.trim="citizen.citizenIdCard"></Matinput>
                                    </div>
                                    <div class="input-field col s12 m6 l6">
                                        <Matinput ref="citizenName" id="name" type="text" label="Nombre" v-model.trim="citizen.citizenName"></Matinput>
                                    </div>
                                    <div class="input-field col s12 m6 l6">
                                        <Matinput ref="citizenLastName" id="lastname" type="text" label="Apellido" v-model.trim="citizen.citizenLastName"></Matinput>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="card-action">
                    <div class="row">
                        <div class="col s12 m6 l6"></div>
                        <div class="col s12 m3 l3 right-align">
                            <div class="waves-effect waves-light btn orange" @click="clearCitizen(true)"><i class="material-icons left">clear</i>Limpiar</div>
                        </div>
                        <div class="col s12 m3 l3 right-align">
                            <div class="waves-effect waves-light btn green" @click="saveCitizen()"><i class="material-icons left">save</i>Guardar</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col s12">
            <div class="card">
                <div class="card-content">
                    <table class="responsive-table highlight striped">
                        <thead>
                            <tr>
                                <th>No. Identidad</th>
                                <th>Nombre</th>
                                <th>Apellido</th>
                                <th>Vivo?</th>
                                <th>Editar</th>
                                <th>Eliminar</th>
                                <th>Tareas</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="c in citizensList" :key="c.citizenId">
                                <td>{{c.citizenIdCard}}</td>
                                <td>{{c.citizenName}}</td>
                                <td>{{c.citizenLastName}}</td>
                                <td>
                                    <div v-if="c.citizenAlive" class="waves-effect waves-light btn-small green" @click="toggleCitizenAlive(c.citizenId)"><i class="material-icons">check</i></div>
                                    <div v-else class="waves-effect waves-light btn-small red" @click="toggleCitizenAlive(c.citizenId)"><i class="material-icons">close</i></div>
                                </td>
                                <td>
                                    <div class="waves-effect waves-light btn-small blue" @click="getCitizen(c.citizenId)"><i class="material-icons">edit</i></div>
                                </td>
                                <td>
                                    <div class="waves-effect waves-light btn-small red"  @click="deleteCitizen(c.citizenId)"><i class="material-icons">delete_forever</i></div>
                                </td>
                                <td>
                                    <router-link :to="'/tasks/' + c.citizenId" class="waves-effect waves-light btn-small grey"><i class="material-icons">assignment_turned_in</i></router-link>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Matinput from '../components/Matinput.vue'
import axios from 'axios'
import { apiRoute } from '../config/apiRoute'
import { showAlert } from '../functions/alert'

export default {
    name: "Citizens",
    components: {
        Matinput
    },
    data: () => ({
        citizensList: [],
        citizen: {
            citizenId: 0,
            citizenIdCard: '',
            citizenName: '',
            citizenLastName: '',
            citizenAlive: true
        }
    }),
    mounted(){
        this.getCitizensList()
    },
    methods: {
        clearCitizen(ask) {
            var resp;
            if(ask){
                resp = confirm('Se perderan los datos no guardados, continuar?');
            } else {
                resp = true;
            }

            if(resp){
                this.citizen = {
                    citizenId: 0,
                    citizenIdCard: '',
                    citizenName: '',
                    citizenLastName: '',
                    citizenAlive: true
                }
                
                this.$refs.citizenIdCard.resetInput();
                this.$refs.citizenName.resetInput();
                this.$refs.citizenLastName.resetInput();
            }
        },
        async saveCitizen() {
            if(this.citizen.citizenIdCard.trim() === '') {
                this.$refs.citizenIdCard.focusInput();
                this.$refs.citizenIdCard.setErrorMessage('Escriba el No. de Identidad');
            } else if(this.citizen.citizenName.trim() === '') {
                this.$refs.citizenName.focusInput();
                this.$refs.citizenName.setErrorMessage('Escriba el Nombre');
            } else if(this.citizen.citizenLastName.trim() === '') {
                this.$refs.citizenLastName.focusInput();
                this.$refs.citizenLastName.setErrorMessage('Escriba el Apellido');
            } else {
                const { data }  = await axios.post(`${apiRoute}/Citizens/saveCitizen`, this.citizen);
                if(data.type==='success') {
                    this.getCitizensList();
                    this.clearCitizen(false);
                }
                showAlert(data.message, data.type);
            }
        },
        async getCitizensList(){
           const { data } = await axios.get(`${apiRoute}/Citizens/getCitizensList`).catch(error => { alert(error.message) });
            if(data.type === 'success') {
                this.citizensList = data.message
            }
            else {
                showAlert(data.message, 'error');
            }
        },
        async getCitizen(citizenId){
            const { data } = await axios.get(`${apiRoute}/Citizens/getCitizen?citizenId=${citizenId}`).catch(error => { alert(error.message) });
            if(data.type === 'success') {
                this.clearCitizen(false);
                this.citizen = data.message;
            }
            else {
                showAlert(data.message, 'error');
            }
        },
        async deleteCitizen(citizenId){
            let resp = confirm('Seguro que desea eliminar el registro? Se eliminaran tambien las tareas asignadas');
            if(resp) {
                const { data } = await axios.delete(`${apiRoute}/Citizens/deleteCitizen?citizenId=${citizenId}`).catch(error => { alert(error.message) });
                showAlert(data.message, data.type);
                if(data.type === 'success') {
                    this.getCitizensList();
                }
            }
        },
        async toggleCitizenAlive(citizenId){
            const { data } = await axios.put(`${apiRoute}/Citizens/toggleCitizenAlive?citizenId=${citizenId}`).catch(error => { alert(error.message) });
            showAlert(data.message, data.type);
            if(data.type === 'success') {
                this.getCitizensList();
            }
        }
    }
}
</script>