import { useActionData } from "react-router-dom";
import { useState, useEffect } from "react";

export default function Alert() {
    const actionData = useActionData();
    const [showAlert, setShowAlert] = useState(false);

    useEffect(() => {
        if (actionData?.success) {
        setShowAlert(() => true);

        const timer = setTimeout(() => setShowAlert(false), 3000);
        return () => clearTimeout(timer);
        }
    }, [actionData]);

    return (
        <>
            {/* Alert */}
            {showAlert && (
            <div 
                className="position-fixed top-0 start-50 translate-middle-x mt-3" 
                style={{ zIndex: 2000 }}
            >
                <div className="alert alert-success alert-dismissible fade show shadow-lg" role="alert">
                {actionData.message}
                <button
                    type="button"
                    className="btn-close"
                    onClick={() => setShowAlert(false)}
                ></button>
                </div>
            </div>
            )}
        </>
    );
}