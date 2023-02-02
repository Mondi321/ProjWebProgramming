import { useField } from 'formik';
import React from 'react'
import { Form } from 'react-bootstrap';

interface Props {
    placeholder: string;
    name: string;
    label?: string;
}

export default function MyTextArea(props: Props) {

    const [ field, meta ] = useField(props.name);

    return (
        <Form.Group className='mb-1 text-area'>
            <Form.Label>{props.label}</Form.Label>
            <Form.Control as='textarea' {...field} {...props} />
            {meta.touched && meta.error ? (
                <Form.Text style ={{color: 'red'}}>
                    {meta.error}
                </Form.Text>
            ): null}
        </Form.Group>
  )
}