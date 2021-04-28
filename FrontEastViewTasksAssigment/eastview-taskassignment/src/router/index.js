import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Citizens from '../views/Citizens.vue'
const routes = [
  {
    path: '/',
    name: 'Inicio',
    component: Home
  },
  {
    path: '/citizens',
    name: 'Ciudadanos',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: Citizens
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
