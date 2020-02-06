/* global FormData */
import server from './server';

export async function all(url){
    let response = await server.get(url);
    return response;
}

export async function allDepartments(){
    let response = await server.get('departments');
    return response.data;
}

export async function one(id){
    let response = await server.get(`users`, {
        params: {id}
    });

    return response.data;
}

export async function createUser(user){

    let response = await server.post('users', user,{withCredentials: true});

    return response;
}

export async function editUser(user){
    let formData = new FormData();

    for(let name in user){
        formData.append(name, user[name]);
    }
    let response = await server.put('users/'+user.id,user, {withCredentials: true});
    return response;
}


export async function removeUser(id){
    // {withCredentials: true} - убрать, в продакшене
    let response = await server.delete('users'+'/'+id,{withCredentials: true});

    return response;
}