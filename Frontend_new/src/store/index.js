import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

import menu from './modules/menu';
import users from './modules/users';
import departments from './modules/departments';
import error from './modules/error';

export const store = new Vuex.Store({
	modules: {
		menu,
		users,
		departments,
		error
	},
	strict: process.env.NODE_ENV !== 'production'
});