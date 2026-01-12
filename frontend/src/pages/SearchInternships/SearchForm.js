import { Dropdown, Form } from "react-bootstrap";
import { useState } from "react";
import './SearchForm.css';
import styles from './SearchFrom.module.css'

function searchParamsUpdate(key, value, searchParams) {
    if (value === "" || value == null || value.length === 0) {
        searchParams.delete(key);
    } else {
        // Array serialize
        if (Array.isArray(value)) {
            searchParams.set(key, value.join(','));
        }
        else {
            searchParams.set(key, value);
        }
    }
    searchParams.set('page', 1)
}

export default function SearchForm({ dictionaries, filters, setFilters, tokenClaims }) {
    const { industries, countries, cities } = dictionaries;

    const updateFilter = (key, value) => {
        const newParams = new URLSearchParams(filters);
        searchParamsUpdate(key, value, newParams);
        setFilters(() => newParams);
    };

    const toggleIndustry = (id) => {
        const prev = filters.industryIds?.split(",") || [];
        id = String(id);
        const newIndustries = prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id];
        updateFilter("industryIds", newIndustries);
    };

    const [ citiesList, setCitiesList] = useState([]);
    
    function onCountryChange(event) {
        const selectedCountryId = event.target.value;
        if(selectedCountryId === '' || selectedCountryId === undefined || selectedCountryId === null) {
            console.log('check 1')
            document.getElementById('cityId').disabled = true;
            setCitiesList(() => []);

            const newParams = new URLSearchParams(filters);
            searchParamsUpdate('countryId', selectedCountryId, newParams);
            searchParamsUpdate('cityId', null, newParams);
            setFilters(() => newParams);
        } else {
            const newCitiesList = cities.filter(c => c.countryId == selectedCountryId);
            setCitiesList(() => newCitiesList);
            document.getElementById('cityId').disabled = false;
            updateFilter('countryId',selectedCountryId)
        }
    }

    return (
        <>
            {/* Navigation
            <div className="bg-primary pb-3">
                <div>
                    Some logo
                </div>
                <form onSubmit={(e) => {e.preventDefault(); updateFilter('searchTerm', e.target.searchTerm.value);}}>
                    <div className="input-group mb-3 mt-4 w-75 m-auto">
                        <input name="searchTerm" type="text" className="form-control" placeholder="Search by title, description, company"/>
                        <button className="btn btn-info" type="submit"><i className="bi bi-search"></i></button>
                    </div>
                </form>
            </div> */}
                <div className="header-wrapper">
                    <div className="container-fluid px-4" style={{ maxWidth: '1280px' }}>

                    {/* Search */}
                    <div className="search-container">
                        <form onSubmit={(e) => {e.preventDefault(); updateFilter('searchTerm', e.target.searchTerm.value);}}>
                            <div className="d-flex align-items-center gap-2">
                                <i className="bi bi-search search-icon"></i>
                                <input 
                                    name="searchTerm"
                                    type="text" 
                                    className="form-control search-input" 
                                    placeholder="Search internships by title, description, or company..."
                                    defaultValue={filters?.searchTerm}
                                />
                                <button className="search-btn" >Search</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div> 

<div className={styles.filtersContainer}>
    {/* Industry */}
    <div className={styles.filterItem}>
        <Dropdown>
            <Dropdown.Toggle className={styles.dropdownToggle} id="industry-dropdown">
                {filters.industryIds ? `Industries (${filters.industryIds.split(",").length})` : "Industries"}
            </Dropdown.Toggle>

            <Dropdown.Menu className={styles.dropdownMenu}>
                {industries.map((i) => (
                    <div key={i.id} className={styles.checkboxItem}>
                        <Form.Check
                            type="checkbox"
                            label={i.value}
                            checked={filters.industryIds?.split(",").includes(String(i.id)) ?? false}
                            onChange={() => toggleIndustry(i.id)}
                        />
                    </div>
                ))}
            </Dropdown.Menu>
        </Dropdown>
    </div>

    {/* Country */}
    <div className={styles.filterItem}>
        <select 
            className={styles.selectInput}
            value={filters.countryId || ""}
            onChange={onCountryChange}>
            <option value="">Select country</option>
            {countries.map(c => <option key={c.id} value={c.id}>{c.value}</option>)}
        </select>
    </div>

    {/* City */}
    <div className={styles.filterItem}>
        <select 
            className={styles.selectInput}
            id="cityId"
            value={filters.cityId || ""}
            onChange={(e) => updateFilter('cityId', e.target.value)}
            disabled={true}>
            <option value="">Select city</option>
            {citiesList.map(c => <option key={c.id} value={c.id}>{c.value}</option>)}
        </select>
    </div>

    {/* Remote */}
    <div className={styles.remoteCheckbox}>
        <input 
            type="checkbox" 
            id="checkDefault"
            checked={filters.isRemote === "true"}
            onChange={(e) => updateFilter("isRemote", e.target.checked ? "true" : "")} 
        />
        <label htmlFor="checkDefault">Remote</label>
    </div>

    {/* Clear Button */}
    <div>
        <button 
            className={styles.clearButton} 
            onClick={() => setFilters(new URLSearchParams())}>
            Clear all
        </button>
    </div>
</div>
            </div>
        </>
    );
}

// import { Dropdown, Form } from "react-bootstrap";

// export default function SearchForm({ dictionaries, filters, setFilters }) {
//   const { industries, countries, cities } = dictionaries;

//   // ✳️ Ключова функція — оновлення search params у URL
//   const updateFilter = (key, value) => {
//     const newParams = new URLSearchParams(filters);
//     if (value === "" || value == null || value.length === 0) {
//       newParams.delete(key);
//     } else {
//       // Якщо масив — серіалізуємо через кому
//       if (Array.isArray(value)) newParams.set(key, value.join(","));
//       else newParams.set(key, value);
//     }
//     setFilters(newParams);
//   };

//   const toggleIndustry = (name) => {
//     const prev = filters.industries?.split(",") || [];
//     const newIndustries = prev.includes(name)
//       ? prev.filter((x) => x !== name)
//       : [...prev, name];
//     updateFilter("industries", newIndustries);
//   };

//   return (
//     <div className="bg-primary-subtle p-3 rounded">
//       {/* Search bar */}
//       <div className="input-group mb-3 w-75 m-auto">
//         <input
//           type="text"
//           className="form-control"
//           placeholder="Search by title, description, company"
//           value={filters.query || ""}
//           onChange={(e) => updateFilter("query", e.target.value)}
//         />
//       </div>

//       {/* Filters */}
//       <div className="d-flex justify-content-evenly py-3 flex-wrap">
//         {/* Industries */}
//         <Dropdown>
//           <Dropdown.Toggle variant="outline-dark">
//             {filters.industries
//               ? `Industries (${filters.industries.split(",").length})`
//               : "Industries"}
//           </Dropdown.Toggle>
//           <Dropdown.Menu style={{ padding: "10px" }}>
//             {industries.map((i) => (
//               <Form.Check
//                 key={i.id}
//                 type="checkbox"
//                 label={i.value}
//                 checked={
//                   filters.industries?.split(",").includes(i.value) ?? false
//                 }
//                 onChange={() => toggleIndustry(i.value)}
//               />
//             ))}
//           </Dropdown.Menu>
//         </Dropdown>

//         {/* Country */}
//         <select
//           className="form-select"
//           style={{ minWidth: 150 }}
//           value={filters.country || ""}
//           onChange={(e) => updateFilter("countryId", e.target.value)}
//         >
//           <option value="">Select country</option>
//           {countries.map((c) => (
//             <option key={c.id} value={c.id}>
//               {c.value}
//             </option>
//           ))}
//         </select>

//         {/* City */}
//         <select
//           className="form-select"
//           style={{ minWidth: 150 }}
//           value={filters.city || ""}
//           onChange={(e) => updateFilter("city", e.target.value)}
//         >
//           <option value="">Select city</option>
//           {cities.map((c) => (
//             <option key={c.id} value={c.value}>
//               {c.value}
//             </option>
//           ))}
//         </select>

//         {/* Remote */}
//         <div className="form-check d-flex align-items-center">
//           <input
//             className="form-check-input me-2"
//             type="checkbox"
//             checked={filters.remote === "true"}
//             onChange={(e) =>
//               updateFilter("remote", e.target.checked ? "true" : "")
//             }
//           />
//           <label className="form-check-label">Remote</label>
//         </div>

//         {/* Clear all */}
//         <button
//           type="button"
//           className="btn btn-outline-secondary"
//           onClick={() => setFilters(new URLSearchParams())}
//         >
//           Clear all
//         </button>
//       </div>
//     </div>
//   );
// }

