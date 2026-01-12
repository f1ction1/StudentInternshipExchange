import { Outlet, useLoaderData } from "react-router";
import MainNavigation from "../components/MainNavigation";
import NewNav from "../components/Navigation/NewNav";
import { getTokenClaims, tokenLoader } from "../util/auth";
import RecommendedInternships from "../components/Internship/RecommendedInternships";


export default function RootLayout() {
    const tokenClaims = useLoaderData();

    return <>
        <NewNav tokenClaims={tokenClaims}/>

        <Outlet/>
    </>
}

export async function loader() {
    const token = tokenLoader();
    if(token) {
        return await getTokenClaims(token);
    }    
    return null;
}