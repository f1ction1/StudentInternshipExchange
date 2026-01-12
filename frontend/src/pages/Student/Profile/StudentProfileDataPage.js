import { ProfileForm } from "../../../components/ProfileForm";
import StudentDataForm from "../../../components/Student/StudentDataForm";
import { getAuthToken } from "../../../util/auth";
import { useLoaderData, Await, redirect } from "react-router-dom";
import { Suspense} from "react";
import StudentCvForm from "../../../components/Student/StudentCvForm";
import { loadProfile } from "../../../util/sharedLoaders";
import Alert from "../../../components/Alert";

export function StudentProfileDataPage() {
    const { profile, student, cvFile } = useLoaderData();
    return (
        <>
            <Alert/>
            <div className="m-4">
                <Suspense fallback={<p style={{ textAlign: 'center' }}>Loading...</p>}>
                    <Await resolve={profile}>
                    {(loadedProfile) => <ProfileForm profile={loadedProfile}/>}
                    </Await>
                </Suspense>
            </div>
            <div className="m-4">
                <Suspense fallback={<p style={{ textAlign: 'center' }}>Loading...</p>}>
                    <Await resolve={student}>
                    {(loadedStudent) => <StudentDataForm student={loadedStudent}/>}
                    </Await>
                </Suspense>
            </div>
            <div className="m-4">
                <Suspense fallback={<p style={{ textAlign: 'center' }}>Loading...</p>}>
                    <Await resolve={cvFile}>
                    {(loadedCvFile) => <StudentCvForm cvFile={loadedCvFile}/>}
                    </Await>
                </Suspense>
                
            </div>
        </>
    );
}

export async function action({ request }) {
    const formData = await request.formData();
    const actionType = formData.get('actionType');
    const token = getAuthToken();

    if(actionType === 'profileData') {
        const profileData = {
            firstName: formData.get('firstName'),
            lastName: formData.get('lastName'),
            phoneNumber: formData.get('phoneNumber')
        }
        
        const response = await fetch('https://localhost:7244/api/User/upsert-profile', {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify(profileData),
        });

        if(!response.ok) {
            throw {message: "Saving personal data failed", statusCode: 500}
        }

        return { success: true, message: "Your personal data saved successfully 🎉"};
    } else if (actionType === 'studentData') {
        const studentData = {
            university: formData.get('university'),
            faculty: formData.get('faculty'),
            degree: formData.get('degree'),
            yearOfStudy: formData.get('yearOfStudy'),
            specialization: formData.get('specialization'),
        }

        const res = await fetch('https://localhost:7244/api/User/upsert-student', { 
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token 
            },
            body: JSON.stringify(studentData), 
        });

        if(!res.ok) {
            throw {message: "Saving education data failed", statusCode: 500}
        }

        return { success: true, message: "Your education data saved successfully 🎉"};
    } else if (actionType === 'cvUpload') {
        const res = await fetch('https://localhost:7244/api/User/upload-cv', {
            method: "POST",
            headers: {
                'Authorization': 'Bearer ' + token 
            },
            body: formData, 
        })
        
        if(!res.ok) {
            throw {message: "Upload CV failed", statusCode: 500}
        }

        return redirect('/student/profile/data');
    }
}

async function loadStudent(token) {
    const response = await fetch('https://localhost:7244/api/User/student', {
        headers: {
            'Authorization': 'Bearer ' + token 
        }
    });

    return await response.json();
}

async function loadCv(token) {
    const response = await fetch('https://localhost:7244/api/User/student/cv', {
        headers: {
            'Authorization': 'Bearer ' + token 
        }
    });
    if(response.status === 204) {
        return null;
    }

    const data = await response.json();
    return data.cvFile;
}

export async function loader() {
    const token = getAuthToken();

    return {
        profile: await loadProfile(token),
        student: await loadStudent(token),
        cvFile: await loadCv(token)
    };
}