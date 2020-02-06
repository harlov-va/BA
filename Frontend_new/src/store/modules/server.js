import axios from 'axios';

let server = axios.create({
    baseURL: 'http://localhost:2711/api/'
});

server.interceptors.request.use(function(request){
    //request.headers.Autorization =  '50537266ded1d3eb1e6923f7f4b2f484';
    
    // Защита от CSRF атак
    function getCookie(name) {
        let matches = document.cookie.match(new RegExp(
          "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
        ));
        return matches ? decodeURIComponent(matches[1]) : undefined;
    }
    let token = getCookie("XSRF-TOKEN");
    if(token !== 'undefined'){
        request.headers["X-XSRF-TOKEN"] = token;        
    }
    return request;
});

server.interceptors.response.use(function(response){
    return response;
});

export default server;