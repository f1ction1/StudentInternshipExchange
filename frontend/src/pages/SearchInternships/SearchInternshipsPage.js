import { useLoaderData, useRouteLoaderData, useSearchParams } from "react-router-dom";
import { useDictionaries } from "../../hooks/useDictionaries";
import { useInternships } from "../../hooks/useInternships";
import SearchForm from "./SearchForm";
import InternshipsList from "./InternshipList";

export default function SearchInternshipsPage() {
    const { data: dictionaries, isLoading: loadingDicts, isError: dicsError } = useDictionaries();
    const [searchParams, setSearchParams] = useSearchParams();
    const tokenClaims = useRouteLoaderData("internships-root");
    // Convert URLSearchParams into simple object
    const filters = Object.fromEntries(searchParams.entries());
    console.log(filters)
    const { data, isLoading: loadingInternships, isError: internshipsError } = useInternships(filters);

    const handlePageChange = (newPage) => {
        setSearchParams(prev => {
            const newParams = new URLSearchParams(prev);
            newParams.set('page', newPage.toString());
            return newParams;
        });
    };

    let searchbar, results;
    console.log(data)
    console.log(dictionaries);
    if(loadingDicts) {
        searchbar = <p>Loading dictionaries...</p>
    }
    if(dictionaries) {
        searchbar = <SearchForm
                dictionaries={dictionaries}
                filters={filters}
                setFilters={setSearchParams}
                tokenClaims={tokenClaims}
                />
    }
    if(loadingInternships) {
        results = <p className="text-muted">Updating results...</p>
    }

    if(data) { 
        results = <InternshipsList data={data} isAuthenticated={tokenClaims != null} onPageChange={handlePageChange}/>
    }

    if(dicsError || internshipsError) {
        throw "Something goes wrong on our side, sorry :( . We are trying to fix that!"
    }

    return (
        <div>
            {searchbar}
            {results}
        </div>
    );
}

