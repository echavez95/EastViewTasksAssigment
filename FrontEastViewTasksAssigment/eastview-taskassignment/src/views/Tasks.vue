<template>
    <div class="row">
        <div class="col s12">
            <div class="card">
                <div class="card-content">
                    <div class="row">
                        <div class="col s12">
                            <form @submit.prevent="" autocomplete="off">
                                <span class="card-title">Buscar ciudadano</span>
                                <div class="row">
                                    <div class="input-field col s12 m4 l4">
                                        <input type="text" ref="citizenIdCard" id="idcard" v-model="citizenIdCard" v-on:keyup.enter="getCitizenByIdCard()">
                                        <label class="active" for="idcard">Escriba No. de Identidad</label>
                                        <span class="helper-text">{{errorNoIdentidad}}</span>
                                        <div class="waves-effect waves-light btn-small grey" @click="getCitizenByIdCard()"><i class="material-icons">search</i></div>
                                    </div>
                                    <div class="input-field col s12 m8 l8">
                                        <Matinput disabled ref="citizenFullName" id="fullname" type="text" label="Nombre" v-model="citizenFullName"></Matinput>
                                    </div>
                                </div>
                            </form>
                            <form @submit.prevent="" autocomplete="off">
                                <span class="card-title">Registrar Tarea</span>
                                <div class="row">
                                    <div class="input-field col s12 m3 l3">
                                        <label class="active">Dia de la Semana</label>
                                        <select ref="taskDay" class="browser-default" v-model="task.taskDay" @change="errorWeekDay=''">
                                            <option value="">Seleccionar dia</option>
                                            <option v-for="(day, index) in weekDays" :key="index" :value="day.DayNumber">{{day.DayName}}</option>
                                        </select>
                                        <span class="helper-text">{{errorWeekDay}}</span>
                                    </div>
                                    <div class="input-field col s12 m7 l7">
                                        <Matinput ref="taskDescription" id="description" type="text" label="Descripción de la tarea" v-model="task.taskDescription"></Matinput>
                                    </div>
                                    <div class="input-field col s12 m2 l2">
                                        <Matinput ref="taskTime" id="time" type="time" label="Hora" v-model="task.taskTime"></Matinput>
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
                            <div class="waves-effect waves-light btn orange" @click="clearTask(true)"><i class="material-icons left">clear</i>Limpiar</div>
                        </div>
                        <div class="col s12 m3 l3 right-align">
                            <div class="waves-effect waves-light btn green" @click="saveTask()"><i class="material-icons left">save</i>Guardar</div>
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
                                <th>Dia</th>
                                <th>Hora</th>
                                <th>Tarea</th>
                                <th>Editar</th>
                                <th>Eliminar</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="t in taskList" :key="t.taskId">
                                <td>{{getWeekDayName(t.taskDay)}}</td>
                                <td>{{t.taskTime}}</td>
                                <td>{{t.taskDescription}}</td>
                                <td>
                                    <div class="waves-effect waves-light btn-small blue" @click="getTask(t.taskId)"><i class="material-icons">edit</i></div>
                                </td>
                                <td>
                                    <div class="waves-effect waves-light btn-small red"  @click="deleteTask(t.taskId)"><i class="material-icons">delete_forever</i></div>
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
import { getWeekDays, getWeekDayName } from '../functions/week'

export default {
    name: "Tasks",
    components: {
        Matinput
    },
    data: ()=> ({
        citizenIdCard: ' ',
        citizenFullName: ' ',
        citizen: {
            citizenId: 0,
            citizenIdCard: '',
            citizenName: '',
            citizenLastName: '',
            citizenAlive: true
        },
        task: {
            taskId: 0,
            taskDay: '',
            taskTime: '',
            taskDescription: '',
            taskCitizenId: 0
        },
        taskList: [],
        weekDays: [],
        errorWeekDay: '',
        errorNoIdentidad: ''
    }),
    mounted(){
        this.weekDays = getWeekDays();
        this.task.taskDay = new Date().getDay();
        this.getCitizenTasksList();
        if(this.$route.params.id > 0) {
            this.getCitizen(this.$route.params.id);
        }
    },
    methods: {
        clearTask(ask) {
            var resp;
            if(ask){
                resp = confirm('Se perderan los datos no guardados, continuar?');
            } else {
                resp = true;
            }
            if(resp){
                this.task = {
                    taskId: 0,
                    taskDay: new Date().getDay(),
                    taskDescription: '',
                    taskCitizenId: 0,
                    taskTime: ''
                }
                this.$refs.taskDescription.resetInput();
            }
        },
        async saveTask() {
            if(this.task.taskDay === '') {
                this.$refs.taskDay.focus();
                this.errorWeekDay = 'Elija el dia';
            } else if(this.task.taskDescription.trim() === '') {
                this.$refs.taskDescription.focusInput();
                this.$refs.taskDescription.setErrorMessage('Escriba la descripcion de la tarea');
            } else if(this.task.taskTime.trim() === '') {
                this.$refs.taskTime.focusInput();
                this.$refs.taskTime.setErrorMessage('Elija una hora y minutos validos');
            } 
            else if(this.citizen.citizenId == 0) {
                this.$refs.citizenIdCard.focus();
                this.errorNoIdentidad = 'Debe buscar un ciudadano';
            } else if(!this.citizen.citizenAlive) {
                showAlert('No puede asignar tareas a un ciudadano que no esta vivo', 'error');
            }
            else {
                this.task.taskCitizenId = this.citizen.citizenId;

                const { data }  = await axios.post(`${apiRoute}/Tasks/saveTask`, this.task);
                if(data.type==='success') {
                    this.clearTask(false);
                    this.getCitizenTasksList();
                }
                showAlert(data.message, data.type);
            }
        },
        async getCitizenByIdCard(){
            if(this.citizenIdCard.trim()!=='') {
                const { data } = await axios.get(`${apiRoute}/Citizens/getCitizenByIdCard?citizenIdCard=${this.citizenIdCard.trim()}`).catch(error => { alert(error.message) });
                if(data.type === 'success') {
                    this.citizen = data.message;
                    this.citizenFullName = this.citizen.citizenName + ' ' + this.citizen.citizenLastName;
                    this.getCitizenTasksList();
                }
                else {
                    showAlert(data.message, 'error');
                }
            }
        },
        async getCitizen(citizenId){
            const { data } = await axios.get(`${apiRoute}/Citizens/getCitizen?citizenId=${citizenId}`).catch(error => { alert(error.message) });
            if(data.type === 'success') {
                this.citizen = data.message;
                this.citizenIdCard = this.citizen.citizenIdCard;
                this.citizenFullName = this.citizen.citizenName + ' ' + this.citizen.citizenLastName;
                this.getCitizenTasksList();
            }
            else {
                showAlert(data.message, 'error');
            }
        },
        async getCitizenTasksList(){
            if(this.citizen.citizenId > 0){
                const { data } = await axios.get(`${apiRoute}/Tasks/getCitizenTasksList?citizenId=${this.citizen.citizenId}`).catch(error => { alert(error.message) });
                if(data.type === 'success') {
                    this.taskList = data.message;
                }
                else {
                    showAlert(data.message, 'error');
                }
            }
        },
        async getTask(taskId){
            const { data } = await axios.get(`${apiRoute}/Tasks/getTask?taskId=${taskId}`).catch(error => { alert(error.message) });
            if(data.type === 'success') {
                this.clearTask(false);
                this.task = data.message;
            }
            else {
                showAlert(data.message, 'error');
            }
        },
        async deleteTask(taskId){
            let resp = confirm('Seguro que desea eliminar el registro?');
            if(resp) {
                const { data } = await axios.delete(`${apiRoute}/Tasks/deleteTask?taskId=${taskId}`).catch(error => { alert(error.message) });
                showAlert(data.message, data.type);
                if(data.type === 'success') {
                    this.getCitizenTasksList();
                }
            }
        },
        getWeekDayName(weekDay){
            return getWeekDayName(weekDay);
        }
    }
}
</script>