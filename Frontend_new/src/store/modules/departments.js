
import * as departmentModel from './articles';

export default {
	namespaced: true,
	state: {
		items: {}
	},
	getters: {
		items(state){
			return state.items;
		},
		itemsMap(state){
			let itemsMap = {};

			for(let i = 0; i < state.items.length; i++){
				let product = state.items[i];
				itemsMap[product.id] = product;
			}
			return itemsMap;
		},
		item: (state, getters) => (id) => {
			return getters.itemsMap[id];
		}
	},
	mutations: {
		clearItems(state){
			state.items = {};
		},
		loadItems(state, data){
			state.items = data;
			console.log(state.items);
		}
	},
	actions: {
		loadItems(store){
			store.commit('clearItems');
			departmentModel.all('departments').then((response) =>{
				if(response.status === 200){
					let itemsMap = {};

					for(let i = 0; i < response.data.length; i++){
						let product = response.data[i];
						itemsMap[product.id] = product;
					}
					store.commit('loadItems', itemsMap);
				}
				else{
					store.dispatch('error/changeStateComment',{state: false, text: 'Department list not loaded'},{ root: true });
				}

			}).catch(() => {
				store.dispatch('error/changeStateComment',{state: false, text: 'Department list not loaded'},{ root: true });
			});

		}
	}
};