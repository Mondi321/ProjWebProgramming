import React, { useState } from 'react';
import {Button} from "react-bootstrap";
import axios from 'axios';
import { NavLink } from 'react-router-dom';
import ValidationErrors from './ValidationError';

export default function TestErrors() {
    const baseUrl = "https://localhost:7115/api/";
    const [errors, setErrors] = useState(null);

    function handleNotFound() {
        axios.get(baseUrl + 'buggy/not-found').catch(err => console.log(err.response));
    }

    function handleBadRequest() {
        axios.get(baseUrl + 'buggy/bad-request').catch(err => console.log(err.response));
    }

    function handleServerError() {
        axios.get(baseUrl + 'buggy/server-error').catch(err => console.log(err.response));
    }

    function handleUnauthorised() {
        axios.get(baseUrl + 'buggy/unauthorised').catch(err => console.log(err.response));
    }

    function handleBadGuid() {
        axios.get(baseUrl + 'authentication/notaguid').catch(err => console.log(err));
    }

    function handleValidationError() {
        axios.post(baseUrl + 'authentication/register', {}).catch(err => setErrors(err));
    }

    return (
        <>
            <h1 className='text-center' style={{marginTop: '6rem'}}>Test Error component</h1>
            <div className='d-flex flex-row justify-content-center gap-4'>
                <NavLink to='/not-found' >
                    <Button onClick={handleNotFound} variant ="primary">Not Found</Button>
                </NavLink>
                <Button onClick={handleBadRequest}variant ="primary">Bad Request</Button>
                <Button onClick={handleValidationError} variant ="primary">Validation Error</Button>
                <Button onClick={handleServerError} variant ="primary">Server Error</Button>
                <Button onClick={handleUnauthorised} variant ="primary">Unauthorised</Button>
                <Button onClick={handleBadGuid} variant ="primary">Bad Guid</Button>
            </div>
            {errors &&
                <ValidationErrors errors={errors} />
            }
        </>
    )
}