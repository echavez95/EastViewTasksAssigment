<template>
  
  <nav>
    <div class="nav-wrapper teal darken-2">
      <router-link to="/" class="brand-logo center">East View</router-link>
      <a href="#" data-target="mobile-demo" class="sidenav-trigger"><i class="material-icons">menu</i></a>
      <ul class="right hide-on-med-and-down">
        <li><router-link to="/">Inicio</router-link></li>
        <li><router-link to="/citizens">Cuidadanos</router-link></li>
      </ul>
    </div>
  </nav>

  <div class="sidenav" id="mobile-demo">
    <div class="collection">
      <router-link to="/" class="collection-item">Inicio</router-link>
      <router-link to="/citizens" class="collection-item">Cuidadanos</router-link>
    </div>
  </div>
  <div class="container">
    <router-view/>
  </div>
</template>

<style>
    .row {
      margin-bottom: 0px !important;
    }
</style>
