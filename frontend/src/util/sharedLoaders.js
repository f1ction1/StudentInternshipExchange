import { getInternshipDictionaries } from "../api/internshipApi";

export async function loadProfile(token) {
    try {
        const response = await fetch('https://localhost:7244/api/User/profile', {
            headers: {
                'Authorization': 'Bearer ' + token 
            }
        });

        return await response.json();
    } catch (ex) {
        throw { message: "Can't load employer data"}
    }
}

export async function loadInternshipsDictionaries() {
    try {
        const response = await getInternshipDictionaries();
        const data = response.data;
        //console.log(data)
        return {cities: data.cities, industries: data.industries, skills: data.skills.map(s => ({ value: s.id, label: s.value })), countries: data.countries}
    } catch {
        throw 'Something goes in loadInternshipsDictionaries '
    }
}