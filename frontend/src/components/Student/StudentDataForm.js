import { Container } from "react-bootstrap";
import { Form } from "react-router-dom";


export default function StudentDataForm({ student }) {
    return (
        <Container className="border border-3 rounded p-3 bg-lightshade">
            <p className="fw-bold">Education data</p>
            <Form method="post">
                <input type="hidden" name="actionType" value="studentData" />

                {/* University */}
                <div className="form-floating mb-3">
                    <input
                    name="university"
                    type="text"
                    className="form-control"
                    id="university"
                    placeholder="University"
                    defaultValue={student.university || ''}
                    required
                    />
                    <label htmlFor="university">
                    University <span className="text-danger">*</span>
                    </label>
                </div>

                {/* Faculty */}
                <div className="form-floating mb-3">
                    <input
                    name="faculty"
                    type="text"
                    className="form-control"
                    id="faculty"
                    placeholder="Faculty"
                    defaultValue={student.faculty || ''}
                    required
                    />
                    <label htmlFor="faculty">
                    Faculty <span className="text-danger">*</span>
                    </label>
                </div>

                {/* Degree */}
                <div className="form-floating mb-3">
                    <select
                    name="degree"
                    className="form-select"
                    id="degree"
                    defaultValue={student.degree || "Select degree"}
                    required
                    >
                    <option value="Select degree" disabled>Select degree</option>
                    <option value="Bachelor">Bachelor</option>
                    <option value="Master">Master</option>
                    </select>
                    <label htmlFor="degree">Degree <span className="text-danger">*</span></label>
                </div>

                {/* Year of Study */}
                <div className="form-floating mb-3">
                    <input
                    name="yearOfStudy"
                    type="number"
                    min="1"
                    max="6"
                    className="form-control"
                    id="yearOfStudy"
                    placeholder="Year of Study"
                    defaultValue={student.yearOfStudy || ''}
                    required
                    />
                    <label htmlFor="yearOfStudy">
                    Year of Study <span className="text-danger">*</span>
                    </label>
                </div>

                {/* Specialization (optional) */}
                <div className="form-floating mb-3">
                    <input
                    name="specialization"
                    type="text"
                    className="form-control"
                    id="specialization"
                    placeholder="Specialization"
                    defaultValue={student.specialization || ''}
                    />
                    <label htmlFor="specialization">Specialization</label>
                </div>

                {/* Submit button */}
                <button type="submit" className="btn btn-primary">
                    Save
                </button>
            </Form>
        </Container>
    );
}