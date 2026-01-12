import { Outlet, redirect, useLoaderData } from "react-router-dom"
import EmployerNavigationBar from "../../components/Employer/EmployerNavigationBar"
import { getTokenClaims, tokenLoader } from "../../util/auth";
import { IsCompleteEmployerProfile } from "../../api/userApi";
import { queryClient } from "../../util/reactQuery"

export default function EmployerRoot() {
    const tokenClaims = useLoaderData();

    return (
        <>
            <EmployerNavigationBar username={tokenClaims.email}/>
            <Outlet/>
        </>
    );
}

export async function loader() {
    const token = tokenLoader();
    console.log('Employer Root')
    if(!token) {
        return redirect('/auth/login');
    }

    const tokenClaims = await getTokenClaims();
    if (tokenClaims.role !== 'employer') {
        return redirect('/');
    }

    const cachedData = queryClient.getQueryData(['isCompleteEmployerProfile', tokenClaims.sub]);
    console.log(cachedData)
    if (!cachedData) {
        await queryClient.prefetchQuery({
        queryKey: ['isCompleteEmployerProfile', tokenClaims.sub],
        queryFn: async () => {
            const res = await IsCompleteEmployerProfile(tokenClaims.sub);
            if (res.status !== 200) throw new Error('Failed to fetch completeness');
            return res.data;
        },
        staleTime: 1000 * 60 * 60 * 24, // 24 години валідності
        cacheTime: 1000 * 60 * 60 * 24 * 30, // місяць у кеші
        });
    }
    
    const data = queryClient.getQueryData(['isCompleteEmployerProfile', tokenClaims.sub]);
    console.log(data?.isComplete);
    if (!data?.isComplete || data?.isComplete === false) {
        return redirect('/complete-profile');
    }

    return tokenClaims;
}