import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

import Users from './components/Users';
import AddUser from './components/AddUser';
import E404 from './components/E404';





import {store} from './store';

const routes = [
	{
		path: '',
		redirect: {name: 'users'}
	},
	{
		name: 'users',
		path: '/users',
		component: Users,
		// beforeEnter(from, to, next){
		// 	store.commit('checkout/onSubmited',false);
		// 	next();
		// }
	},
	{
		path: '/addUser',
		component: AddUser,
		// beforeEnter(from, to, next){
		// 	store.commit('checkout/onSubmited',false);
		// 	next();
		// }
	},
	{
		path: '*',
		component: E404
	}
];

export const router = new VueRouter({
	routes,
	mode: 'history'
});