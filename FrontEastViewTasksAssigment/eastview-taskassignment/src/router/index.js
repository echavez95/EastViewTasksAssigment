import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Citizens from '../views/Citizens.vue'
import Tasks from '../views/Tasks.vue'

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
    path: '/tasks',
    name: 'Tareas',
    component: Tasks
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
