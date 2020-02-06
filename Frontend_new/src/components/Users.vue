<template>
  <div class="users">
    <div class="card mt-0">
      <div class="card-header">
        Users List
      </div>
      <div class="card-body">
        <div class="table-responsive">
          <table class="table">
            <thead>
              <tr>
                <th scope="col">
                  Username
                </th>
                <th>
                  Department
                </th>
                <th>
                  Action
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="user in users" v-bind:key="user.id">
                <template v-if="editUserData.id === user.id">
                  <td><input v-model="editUserData.UserName" type="text"></td>
                  <!-- <td><input v-model="editUserData.DepartmentId" type="text"></td> -->
                  <td>
                    <select v-model="editUserData.DepartmentId" class="form-control">
                      <option v-for="option in departments" v-bind:value="option.id" :key="option.id">
                        {{ option.Title }}
                      </option>
                    </select>
                  </td>
                  <td>
                    <span class="icon">
                      <i  @click="onEditSubmit($event)" class="fa fa-check"></i>
                    </span>
                    <span class="icon">
                      <i  @click="onCancel" class="fa fa-ban"></i>
                    </span>
                  </td>
                </template>
                <template v-else>
                  <td>
                    {{user.UserName}}
                  </td>
                  <td>
                    {{departments[user.DepartmentId].Title}}
                  </td>                  
                  <td>
                    <a href="#" class="icon">
                      <i v-on:click="onEdit(user)" class="fa fa-pencil"></i>
                    </a>                   
                    <a href="#" class="icon">
                      <i v-on:click="deleteUser(user.id)" class="fa fa-trash"></i>
                    </a>
                  </td>
                </template>
              </tr>

            </tbody>
          </table>
        </div>
      </div>
    </div>

  </div>
</template>

<script>
import {mapActions,mapGetters} from 'vuex';

export default {
  data () {
    return {
      editUserData: {
        'id' : '',
        'UserName': '',
        'DepartmentId': ''
      },

    }
  },
  beforeCreate(){
      this.$store.dispatch('departments/loadItems');
      this.$store.dispatch('users/loadItems');      
		},
		computed: {
			...mapGetters('users', {
				users: 'items'
      }),
      ...mapGetters('departments', {
				departments: 'items'
      })
		},
		methods: {
			...mapActions('users', {
        deleteUser: 'deleteItem',
        editUser:'editItem'
      }),
      onEdit(user){
        this.editUserData.id = user.id;
        this.editUserData.UserName = user.UserName;
        this.editUserData.DepartmentId = user.DepartmentId;
       },
      onCancel(){
        for (let item in this.editUserData) this.editUserData[item] = '';
      },
      onEditSubmit(){
        this.editUser(Object.assign({},this.editUserData));
        this.onCancel();
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
