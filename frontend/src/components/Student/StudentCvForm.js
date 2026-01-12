import { Container } from "react-bootstrap";
import { Form } from "react-router-dom";
import { useState, useEffect } from "react";

export default function StudentCvForm({ cvFile }) {
    const [preview, setPreview] = useState(null);

    useEffect(() => {
        if (cvFile) {
        setPreview(() => cvFile);
        }
    }, [cvFile]);


    // const handleFileChange = (e) => {
    //     const file = e.target.files[0];
    //     if (file && file.type === "application/pdf") {
    //     const reader = new FileReader();
    //     reader.onload = (event) => {
    //         //console.log(event.target.result);
    //         setPreview(() => event.target.result); // base64 pdf
    //     };
    //     reader.readAsDataURL(file);
    //     } else {
    //     setPreview(null);
    //     }
    // };

    return (
        <Container className="border border-3 rounded p-3 bg-lightshade">
            {/* <p className="fw-bold">Cv</p> */}

            {preview && (
                <div className="my-3">
                    <p className="fw-bold">Your current CV:</p>
                    <iframe
                    src={preview}
                    title="PDF Preview"
                    width="100%"
                    height="500px"
                    style={{ border: "1px solid #ccc" }}
                    />
                </div>
            )}

            <Form method="post" encType="multipart/form-data">
                <input type="hidden" name="actionType" value="cvUpload" />
                <div className="mb-3">
                    <label htmlFor="cv" className="form-label">
                    Upload new CV <span className="text-danger">*</span>
                    </label>
                    <input
                    name="cv"
                    type="file"
                    className="form-control"
                    id="cv"
                    accept=".pdf"
                    // onChange={handleFileChange}
                    required
                    />
                </div>

                <Container className="m-0  p-0 text-start">
                    <button className="btn btn-primary mt-3" type="submit">Upload</button>
                </Container>
            </Form>
        </Container>
    );
}