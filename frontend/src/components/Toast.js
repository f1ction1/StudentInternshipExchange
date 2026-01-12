import { useEffect } from "react";
import styles from './Toast.module.css';
// Toast Notification Component
export const Toast = ({ id, type, message, onRemove }) => {
  useEffect(() => {
    const timer = setTimeout(() => {
      onRemove(id);
    }, 5000); // 5 секунд

    return () => clearTimeout(timer);
  }, [id, onRemove]);

  const icons = {
    success: 'bi-check-circle-fill',
    error: 'bi-x-circle-fill',
    warning: 'bi-exclamation-triangle-fill',
    info: 'bi-info-circle-fill'
  };

  const colors = {
    success: '#10b981',
    error: '#ef4444',
    warning: '#f59e0b',
    info: '#3b82f6'
  };

  const handleClose = () => {
    onRemove(id);
  };

  return (
    <div 
      className={styles.toastItem}
      style={{
        '--toast-color': colors[type]
      }}
    >
      <div className={styles.toastIcon}>
        <i className={`bi ${icons[type]}`}></i>
      </div>
      <div className={styles.toastContent }>
        <p className={styles.toastMessage }>{message}</p>
      </div>
      <button 
        className={styles.toastClose }
        onClick={handleClose}
      >
        <i className="bi bi-x"></i>
      </button>
    </div>
  );
};

// Toast Container Component
export const ToastContainer = ({ toasts, removeToast}) => {
  return (
    <div className={styles.toastContainer }>
      {toasts.map((toast) => (
        <Toast
          key={toast.id}
          id={toast.id}
          type={toast.type}
          message={toast.message}
          onRemove={removeToast}
        />
      ))}
    </div>
  );
};