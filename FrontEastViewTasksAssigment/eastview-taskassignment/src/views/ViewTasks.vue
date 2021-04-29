<template>
    <div class="row">
        <div class="col s12">
            <div class="card">
                <div class="card-content">
                    <div class="row">
                        <div class="col s12">
                            <form @submit.prevent="" autocomplete="off">
                                <span class="card-title">Consultar Tareas</span>
                                <div class="row">
                                    <div class="input-field col s12 m4 l4">
                                        <label class="active">Elija un dia de la semana:</label>
                                        <select ref="taskDay" class="browser-default" v-model="selectedDay" @change="getCitizensListWithTasksByDay()">
                                            <option v-for="(day, index) in weekDays" :key="index" :value="day.DayNumber">{{day.DayName}}</option>
                                        </select>
                                    </div>
                                </div>
                            </form>
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
                    <div v-for="(citizen, index) in citizensList" :key="index">
                        <h6><strong>{{citizen.citizenName + ' ' + citizen.citizenLastName}}</strong></h6>
                        <table width="100%" class="responsive-table highlight striped">
                            <thead>
                                <tr>
                                    <th width="20%">Hora</th>
                                    <th width="80%">Tarea</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(task, index) in citizen.Tasks" :key="index">
                                    <td>{{task.taskTime}}</td>
                                    <td>{{task.taskDescription}}</td>
                                </tr>
                            </tbody>
                        </table>
                        <hr>
                    </div>
                    
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
    name: "ViewTasks",
    components: {
        Matinput
    },
    data: ()=> ({
        selectedDay: new Date().getDay(),
        citizensList: [],
        weekDays: []
    }),
    mounted(){
        this.weekDays = getWeekDays();
        this.getCitizensListWithTasksByDay();
    },
    methods: {
        async getCitizensListWithTasksByDay(){
            const { data }  = await axios.get(`${apiRoute}/Citizens/getCitizensListWithTasksByDay?weekDay=${this.selectedDay}`).catch(error => { alert(error.message) });
            if(data.type === 'success') {
                this.citizensList = data.message;
            }
            else {
                showAlert(data.message, 'error');
            }
        }
    }
}
</script>
