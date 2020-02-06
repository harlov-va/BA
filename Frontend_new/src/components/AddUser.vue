<template>
  <div class="addUser">
    <div class="card">
      <div class="card-header">
        Add a new user
      </div>
      <div class="card-body">
        <form class="form-inline" v-on:submit.prevent="onSubmit">
          <div class="form-group">
            <label>Name</label>
            <input v-model="newUser.UserName" type="text" class="form-control ml-sm-2 mr-sm-4 my-2" required>
          </div>
          <div class="form-group">
            <label>Department</label>
            <select v-model="newUser.DepartmentId" class="form-control ml-sm-2 mr-sm-4 my-2" required>
              <option v-for="option in departments" v-bind:value="option.id" :key="option.id">
                {{ option.Title }}
              </option>
            </select>
            <!-- <input v-model="newUser.DepartmentId" type="text" class="form-control ml-sm-2 mr-sm-4 my-2" required> -->
          </div>
          <div class="ml-auto text-right">
            <button 
            :disabled="isDisable"
            type="submit" class="btn btn-primary my-2">Add</button>
            
          </div>
        </form>
      </div>
    </div>    
  </div>
</template>

<script>
import {mapActions,mapGetters} from 'vuex';

export default {
  data () {
    return {
      newUser: {
        'id' : 0,
        'UserName': '',
        'DepartmentId': ''
      }
    }
  },
  computed:{
    ...mapGetters('departments', {
      departments: 'items'
    }),
    isDisable(){
      return !(this.newUser.UserName !== '' && this.newUser.DepartmentId !== '')
    }
  },
  methods: {
    ...mapActions('users', {
      addUser: 'addItem'
    }),
    onSubmit(){
      this.addUser(Object.assign({},this.newUser));
      for (let item in this.newUser) this.newUser[item] = '';
    }
  }  
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3{
  text-align: center;
  /* margin-top: 30px;
  margin-bottom: 20px; */
}
.icon{
  margin-right: 10px;
}
.icon i{
  cursor: pointer;
}
</style>
