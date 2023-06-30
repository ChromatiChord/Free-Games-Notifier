import { toast } from 'react-toastify';

const toastConfig = {
  position: "top-right",
  autoClose: 5000,
  hideProgressBar: false,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: true,
  progress: undefined,
  theme: "colored",
  }

export function generateToast(message, type) {
  if (type === 'error') {
    toast.error(message, toastConfig);
  } else {
    toast.success(message, toastConfig);
  }
}