import { useField } from 'formik';
import React from 'react'
import { Form } from 'react-bootstrap';

interface Props {
    placeholder: string;
    name: string;
    options: any[];
    label?: string;
}

export default function MySelectInput(props: Props) {

    const [ field, meta, helpers ] = useField(props.name);

    return (
        <Form.Group className='mb-1'>
            <Form.Label>{props.label}</Form.Label>
            <Form.Select
                value={field.value}
                onChange={(event: React.ChangeEvent<HTMLSelectElement>) => helpers.setValue(event.target.value)}
                onBlur={() => helpers.setTouched(true)}
            >
                <option></option>
                {props.options.map(option => (
                    <option key={option.value} value={option.value}>{option.text}</option>
                ))}
            </Form.Select>
            {meta.touched && meta.error ? (
                <Form.Text style ={{color: 'red'}}>
                    {meta.error}
                </Form.Text>
            ): null}
        </Form.Group>
  )
}
