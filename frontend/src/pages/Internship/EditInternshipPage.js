import { Form, useLoaderData, redirect, useNavigation } from "react-router-dom";
import { useState, useEffect, useRef } from "react";
import Select from "react-select";
import { loadInternshipsDictionaries } from "../../util/sharedLoaders";
import { getEmployerIntershipInfo, updateInternship } from "../../api/internshipApi";

const normalize = (data) => ({
    ...data,
    countryId: Number(data.countryId),
    cityId: Number(data.cityId),
    industryId: Number(data.industryId),
    skillsIds: data.skillsIds.sort((a,b) => a - b),
});

export default function EditInternshipPage() {
    const loaderData = useLoaderData();
    const {cities, industries, skills, countries } = loaderData.dictionaries;

    let internship = loaderData.existingInternship
    const internshipId = loaderData.internshipId;

    let defaultSelectedSkills = skills.filter(s =>
        internship.skillsIds.includes(s.value)
    );

    // Stable copy using UseRef
    const initialDataRef = useRef(internship);
    const [formData, setFormData] = useState(internship);
    const [isDirty, setIsDirty] = useState(false);
    const navigation = useNavigation();

    useEffect(() => {
        console.log('--------------------------');
        console.log('FormData', formData);
        console.log('Inial', normalize(initialDataRef.current));

        setIsDirty(() => JSON.stringify(normalize(formData)) !== JSON.stringify(initialDataRef.current));
    }, [formData]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData(() => {
            return {
                ...formData,
                [name]: type === "checkbox" ? checked : value,}
        });
    };

    const [ citiesList, setCitiesList] = useState(cities.filter(c => c.countryId === internship.countryId));

    function onCountryChange(event) {
        handleChange(event);
        const selectedCountryId = event.target.value;
        const newCitiesList = cities.filter(c => c.countryId == selectedCountryId);
        setCitiesList(() => newCitiesList);
    }
    
    const isSubmitting = navigation.state === "submitting";

    return (
        <Form method="patch" className="p-4 mt-5 mb-5 border border-3 rounded">
            <h4 className="mb-3">Edit Internship</h4>
            <input id="id" name="id" type="text" defaultValue={internshipId} hidden/>
            {/* Title */}
            <div className="mb-3">
                <label htmlFor="title" className="form-label">Title</label>
                <input
                type="text"
                id="title"
                name="title"
                className="form-control"
                value={formData.title}
                onChange={handleChange}
                required
                />
            </div>

            {/* Description */}
            <div className="mb-3">
                <label htmlFor="description" className="form-label">Description</label>
                <textarea
                id="description"
                name="description"
                className="form-control"
                rows="4"
                value={formData.description}
                onChange={handleChange}
                required
                />
            </div>

            <div className="row">
                {/* Country */}
                <div className="col-md-6 mb-3">
                    <label htmlFor="countryId" className="form-label">Country</label>
                    <select
                        id="countryId"
                        name="countryId"
                        className="form-select"
                        onChange={onCountryChange}
                        value={formData.countryId}
                        required
                    >
                        <option value="" disabled>Select country...</option>
                        {countries.map((c) => (
                        <option key={c.id} value={c.id}>{c.value}</option>
                        ))}
                    </select>
                </div>

                {/* City */}
                <div className="col-md-6 mb-3">
                    <label htmlFor="cityId" className="form-label">City</label>
                    <select
                        id="cityId"
                        name="cityId"
                        className="form-select"
                        value={formData.cityId}
                        onChange={handleChange}
                        required     
                    >
                        <option value="">Select city...</option>
                        {citiesList.map((c) => (
                            <option key={c.id} value={c.id}>{c.value}</option>
                        ))}
                    </select>
                </div>
            </div>

            <div className="row align-items-end">
                {/* Industry */}
                <div className="col-md-6 mb-3">
                    <label htmlFor="industryId" className="form-label">Industry</label>
                    <select
                        id="industryId"
                        name="industryId"
                        className="form-select"
                        value={formData.industryId}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Select industry...</option>
                        {industries.map((i) => (
                        <option key={i.id} value={i.id}>{i.value}</option>
                        ))}
                    </select>
                </div>

                {/* Remote */}
                <div className="col-md-6 mb-3">
                    <div className="form-check">
                        <input
                        type="checkbox"
                        id="isRemote"
                        name="isRemote"
                        className="form-check-input"
                        checked={formData.isRemote}
                        onChange={handleChange}
                        />
                        <label className="form-check-label" htmlFor="isRemote">
                        Remote internship
                        </label>
                    </div>
                </div>
            </div>

            {/* Multi-select for Skills */}
            <div className="mb-3">
                <label className="form-label">Required skills</label>
                <Select
                options={skills}
                isMulti
                placeholder="Select or search skills..."
                classNamePrefix="select"
                name="skills"
                defaultValue={defaultSelectedSkills}
                onChange={(selected) => {
                    setFormData((prev) => ({...prev, skillsIds: selected.map((s) => s.value)}));
                }}
                required
                />
            </div>

            <div className="row">
                {/* ExpiresAt */}
                <div className="col-md-6 mb-3">
                <label htmlFor="expiresAt" className="form-label">Expires at</label>
                <input
                    type="date"
                    id="expiresAt"
                    name="expiresAt"
                    className="form-control"
                    value={formData.expiresAt}
                    onChange={handleChange}
                    required
                />
                </div>
            </div>

            <div className="d-flex justify-content-between align-items-center">
                <span
                className={`fw-semibold ${
                    isDirty ? "text-warning" : "text-success"
                }`}
                >
                {isDirty ? "Unsaved changes" : "All changes saved"}
                </span>

                <button
                type="submit"
                className="btn btn-primary"
                disabled={!isDirty || isSubmitting}
                >
                {isSubmitting ? "Saving..." : "Save changes"}
                </button>
            </div>
        </Form>
    )
}

export async function action({ request }) {
    const data = await request.formData();
    const internshipId = data.get("id");

    const internshipData = { 
        title: data.get('title'),
        description: data.get('description'),
        countryId: data.get('countryId'),
        cityId: data.get('cityId'),
        isRemote: data.get('isRemote') === 'on',
        industryId: data.get('industryId'),
        skillsIds: data.getAll('skills').map(s => Number(s)).sort((a,b) => a - b),
        expiresAt: data.get('expiresAt'), }

    const current = internshipData;
    const initial = JSON.parse(localStorage.getItem("initial_internship_data") || "{}");
    console.log("Current", current);
    console.log("Initial", initial);
    // Визначаємо diff
    const diff = Object.keys(current).reduce((acc, key) => {
        console.log("Current", String(current[key]))
        console.log("Inial", String(initial[key]))
        if (String(current[key]) !== String(initial[key])) {
            acc[key] = current[key];
        }
        return acc;
    }, {});

    if (Object.keys(diff).length === 0) {
        console.log("No changes detected. Skipping PATCH.");
        return redirect("..");
    }
    console.log("In action", { id: internshipId, ...diff });
    await updateInternship({ id: internshipId, ...diff });
    return redirect("..");
}

export async function loader({request, params}) {  
    try {
        const internshipId = params.internshipId;
        //console.log(internshipId);
        const dictionaries = await loadInternshipsDictionaries();

        const response = await getEmployerIntershipInfo(internshipId);
        const existingInternship = response.data;
        existingInternship.expiresAt = existingInternship.expiresAt.split("T")[0];
        //console.log(existingInternship)

        localStorage.setItem("initial_internship_data", JSON.stringify(existingInternship));

        return {
            internshipId: internshipId,
            dictionaries: dictionaries,
            existingInternship: existingInternship
        } 
    } catch {
        throw "Something goes wrong in EditInternship loader"
    }
}