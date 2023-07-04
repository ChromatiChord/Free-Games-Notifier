import { useEffect, useState } from "react"
import { Typography } from '@mui/material';
import { generateToast } from "../Util/ToastGenerator"
import { useParams } from "react-router-dom";
import { unsubUser } from '../Util/ApiCall';


export default function UnsubPage() {
    let { uuid } = useParams();

    const [isLoading, setIsLoading] = useState(true);
    const [wasSuccessful, setWasSuccessful] = useState(false);

    useEffect(() => {
        // generateToast("Render!", 'error');

        unsubUser(uuid).then((response) => {
          console.log(response);
          setWasSuccessful(response.status === 200);
          setIsLoading(false);
        });

    }, [])

    return (
        <>
          {isLoading ? 
            <Typography color="white" sx={{ marginBottom: 2 }}>
                Unsubscribing...
            </Typography>
            :
            wasSuccessful ?
                <Typography color="white" sx={{ marginBottom: 2 }}>
                    Unsubscribed!
                </Typography>
                :
                <Typography color="white" sx={{ marginBottom: 2 }}>
                    Error unsubscribing!
                </Typography>
          }   
        </>
    )
}