import React from 'react'
import { Spinner } from 'react-bootstrap';

interface Props{
    inverted?: boolean;
    content?: string;
}

export default function LoadingComponent({inverted = false, content = 'Loading...'}: Props) {
  return (
    <div className='d-flex flex-column justify-content-center align-items-center spinner'>
        <Spinner variant='danger' animation="border" role="status" hidden={inverted}>
            
        </Spinner>
        <span style={{color:"darkred"}}>{content}</span>
    </div>
    
  )
}