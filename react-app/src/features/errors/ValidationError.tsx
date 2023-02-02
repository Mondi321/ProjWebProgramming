import React from 'react'
import { Alert } from 'react-bootstrap'

interface Props{
    errors:any;
}

export default function ValidationErrors({errors}: Props) {
  return (
      <>
        {errors && (
            <Alert variant='danger' className='mt-5' style={{minHeight:"80px"}}>
                {errors.map((err: any, i:any) => (
                    <p key={i}>{err}</p>
                ))}
            </Alert>
        )}
      </>
  )
}