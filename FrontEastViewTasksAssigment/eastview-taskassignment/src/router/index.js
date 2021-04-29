import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Citizens from '../views/Citizens.vue'
import Tasks from '../views/Tasks.vue'
import ViewTasks from '../views/ViewTasks.vue'

const routes = [
  {
    path: '/',
    name: 'Inicio',
    component: Home
  },
  {
    path: '/citizens',
    name: 'Ciudadanos',
    component: Citizens
  },
  {
    path: '/tasks/:id?',
    name: 'Tareas',
    component: Tasks
  },
  {
    path: '/viewtasks',
    name: 'Ver Tareas',
    component: ViewTasks
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
