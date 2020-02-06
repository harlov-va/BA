export default {
	namespaced: true,
	state: {
		stateComment: true,
		textComment:'',
		classComment:'',
		show:false
	},
	getters: {
		stateComment(state){
			return state.stateComment;
		},
		textComment(state){
			return state.textComment;
		},
		classComment(state){
			return state.classComment;
		},
		show(state){
			return state.show;
		},
	},
	mutations: {
		shown(state, payload){
			state.show = payload;
		},
		changeStateComment(state, payload){
			if(payload.state === true){
				state.classComment = "alert alert-success alert-dismissable";
				state.textComment = payload.text;
			}
			else{
				state.classComment = "alert alert-danger alert-dismissable";
				state.textComment = payload.text;
			}
		},
	},
	actions: {
		shown(store, payload){
			store.commit('shown', payload);
			setTimeout(()=>{
				store.commit('shown',false);
			},3000);
		},
		changeStateComment(store,payload){
			store.dispatch('shown', true);
			store.commit('changeStateComment', payload);
		}
	}
};