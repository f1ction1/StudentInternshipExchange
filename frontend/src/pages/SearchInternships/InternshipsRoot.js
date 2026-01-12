import { Outlet, useLoaderData } from "react-router-dom";
import SearchNavigation from "./SearchNavigation";
import { tokenLoader, getTokenClaims } from "../../util/auth";

export default function InternshipsRoot() {
    const tokenClaims = useLoaderData();

    return (
        <>
            <SearchNavigation tokenClaims={tokenClaims}/>
            <Outlet/>
        </>
    );
}

export async function loader() {
    const token = tokenLoader();
    if(token) {
        return await getTokenClaims(token); 
    }
    return null;
}