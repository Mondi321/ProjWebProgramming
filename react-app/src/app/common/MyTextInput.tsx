import { useField } from 'formik';
import React from 'react'
import { Form } from 'react-bootstrap';

interface Props {
    placeholder: string;
    name: string;
    type?: string;
    label?: string;
}

export default function MyTextInput(props: Props) {

    const [ field, meta ] = useField(props.name);

    return (
        <Form.Group className='mb-1 text-input formField'>
            <Form.Label className='formFieldLabel'>{props.label}</Form.Label>
            <Form.Control className='formFieldInput' {...field} {...props} />
            {meta.touched && meta.error ? (
                <Form.Text style ={{color: 'red'}}>
                    {meta.error}
                </Form.Text>
            ): null}
        </Form.Group>
  )
}
