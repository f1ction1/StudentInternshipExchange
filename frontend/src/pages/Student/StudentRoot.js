import { Outlet, redirect, useLoaderData } from "react-router-dom";
import StudentNavigationBar from "../../components/Student/StudentNavigationBar";
import { getTokenClaims, tokenLoader } from "../../util/auth";
import SearchNavigation from "../SearchInternships/SearchNavigation";

export default function StudentHomePage() {
    const tokenClaims = useLoaderData();

    return (
        <>
            {/* <StudentNavigationBar username={tokenClaims.email}/> */}
            <SearchNavigation tokenClaims={tokenClaims}/>
            <Outlet/>
        </>
    );
}

export async function loader() {
    const token = tokenLoader();
    console.log('Student Root check token')
    if(!token) {
        return redirect('/auth/login');
    }

    const tokenClaims = await getTokenClaims();
    //console.log('Token claims: ', tokenClaims)
    if (tokenClaims.role !== 'student') {
        return redirect('/');
    }

    return tokenClaims;
}