import Vue from 'vue';
import * as productModel from './articles';

export default {
	namespaced: true,
	state: {
		items: [] //getProducts()
	},
	getters: {
		items(state){
			return state.items;
		},
		itemsMap(state){
			let itemsMap = {};

			for(let i = 0; i < state.items.length; i++){
				let product = state.items[i];
				itemsMap[product.id_product] = product;
			}

			return itemsMap;
		},
		item: (state, getters) => (id) => {
			return getters.itemsMap[id];
		}
	},
	mutations: {
		clearItems(state){
			state.items = [];
		},
		loadItems(state, data){
			state.items = data;
		}
	},
	actions: {
		loadItems(store){
			store.commit('clearItems');
			productModel.all('users').then((response) =>{
				if(response.status === 200){
					store.commit('loadItems', response.data);
				}
				else{
					store.dispatch('error/changeStateComment',{state: false, text: 'User list not loaded'},{ root: true });
				}

			}).catch(() => {
				store.dispatch('error/changeStateComment',{state: false, text: 'User list not loaded'},{ root: true });
			});

		},
		deleteItem(store,id){
			if(confirm("Are you sure you want to delete the user?")){
				productModel.removeUser(id).then((response) =>{
					if(response.status === 204){
						store.commit('clearItems');
						store.dispatch('loadItems');
						store.dispatch('error/changeStateComment',{state: true, text: 'User deleted'},{ root: true });
					}
					else{
						store.dispatch('error/changeStateComment',{state: false, text: 'Error! User not deleted'},{ root: true });
					}

				}).catch(() => {
					store.dispatch('error/changeStateComment',{state: false, text: 'Error! User not deleted'},{ root: true });
				});
			}
		},
		editItem(store, payload){
			productModel.editUser(payload).then((response) =>{
				if(response.status === 204){
					store.commit('clearItems');
					store.dispatch('loadItems');
				}
				else{
					store.dispatch('error/changeStateComment',{state: false, text: 'Error! User not edited'},{ root: true });
				}

			}).catch(() => {
				store.dispatch('error/changeStateComment',{state: false, text: 'Error! User not edited'},{ root: true });
			});
		},
		addItem(store, payload){
			productModel.createUser(payload).then((response) =>{
				if(response.status === 201){
					store.commit('clearItems');
					store.dispatch('loadItems');
					store.dispatch('error/changeStateComment',{state: true, text: 'User added'},{ root: true });
				}
				else{
					store.dispatch('error/changeStateComment',{state: false, text: 'Error! User not added'},{ root: true });
				}

			}).catch(() => {
				store.dispatch('error/changeStateComment',{state: false, text: 'Error! User not added'},{ root: true });
			});
		}
	}
};