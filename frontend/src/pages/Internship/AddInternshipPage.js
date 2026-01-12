import Select from "react-select";
import { Form, redirect, useLoaderData } from "react-router-dom";
import { useState } from "react";
import { addInternship, getInternshipDictionaries } from "../../api/internshipApi";

export default function AddInternshipPage() {
    const { cities, industries, skills, countries } = useLoaderData();

    const [ citiesList, setCitiesList] = useState([]);

    function onCountryChange(event) {
        const selectedCountryId = event.target.value;

        const newCitiesList = cities.filter(c => c.countryId == selectedCountryId);
        setCitiesList(() => newCitiesList);
        document.getElementById('cityId').disabled = false;
    }

    return (
        <Form method="post" className="p-4 mt-5 mb-5 border border-3 rounded">
        <h4 className="mb-3">Add Internship</h4>

        {/* Title */}
        <div className="mb-3">
            <label htmlFor="title" className="form-label">Title</label>
            <input
            type="text"
            id="title"
            name="title"
            className="form-control"
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
                    defaultValue={""}
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
                    required
                    disabled
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
                required
            />
            </div>
        </div>

        <button type="submit" className="btn btn-primary">
            Add Internship
        </button>
        </Form>
  );
}

export async function action({ request }) {
    const data = await request.formData();
    const internshipData = {
        title: data.get('title'),
        description: data.get('description'),
        countryId: data.get('countryId'),
        cityId: data.get('cityId'),
        isRemote: data.get('isRemote') === 'on',
        industryId: data.get('industryId'),
        skillsIds: data.getAll('skills'),
        expiresAt: data.get('expiresAt'),
    }
    console.log(internshipData);
    await addInternship(internshipData);
    return redirect('..')
}

export async function loader() {
    try {
        const response = await getInternshipDictionaries();
        const data = await response.data;
        //console.log(data)
        return {cities: data.cities, industries: data.industries, skills: data.skills.map(s => ({ value: s.id, label: s.value })), countries: data.countries}
    } catch {
        throw 'Something goes wrong on AddInternshipPage '
    }
}