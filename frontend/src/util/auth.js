import { jwtDecode } from "jwt-decode";
import { redirect } from "react-router-dom";

export function getAuthToken() {
    const token = localStorage.getItem('token');
    return token;
}

export function tokenLoader() {
    return getAuthToken();
}

export function checkAuthLoader() {
    const token = getAuthToken();

    if(!token) {
        return redirect('/auth/login');
    }

    return null;
}

export async function getTokenClaims(token) { // should be refactored, because if there are no token error thrown
    if(token) {
        return await jwtDecode(token)
    } 
    return await jwtDecode(getAuthToken()); // this line throw exception
}