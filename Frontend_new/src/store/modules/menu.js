export default {
	namespaced: true,
	state: {
		items: [
			{
				url: '/users',
				text: 'Users List'
			},
			{
				url: '/addUser',
				text: 'Add User'
			}
		]
	},
	getters: {
		items(state){
			return state.items;
		}
	}
};