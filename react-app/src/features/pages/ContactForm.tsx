import { Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';
import MyTextArea from '../../app/common/MyTextArea';
import MyTextInput from '../../app/common/MyTextInput';
import { Contact } from '../../app/models/contact';
import { useStore } from '../../app/stores/store';
import './contactForm.css';
import { v4 as uuid } from 'uuid';
import { useHistory } from 'react-router-dom';
import { Button, Spinner } from 'react-bootstrap';
import { observer } from 'mobx-react-lite';

export default observer(function ContactForm() {

    const { contactStore } = useStore();
    const { createContact, loading } = contactStore;
    const history = useHistory();

    const validationSchema = Yup.object({
        firstName: Yup.string().required('Name is required!'),
        email: Yup.string().email('Not a valid email').required('Email is required!'),
        subject: Yup.string().required('Subject is required!'),
        phone: Yup.number().required('Number is required!'),
        message: Yup.string().required('Message is required!'),
    })

    function handleFormSubmit(contact: Contact) {
        let newContact = {
            ...contact,
            contactId: uuid()
        };
        createContact(newContact).then(() => history.push('/home'))
    }
    return (
        <div className="contactForm">
            <img src={require('../../images/foo.jpg')} alt="" />
            <div className='contactForm-permbajtja'>
                <h1>Contact Us</h1>
                <Formik
                    validationSchema={validationSchema}
                    enableReinitialize
                    onSubmit={(values) => handleFormSubmit(values)}
                    initialValues={{
                        contactId: '',
                        firstName: '',
                        email: '',
                        subject: '',
                        phone: '',
                        message: ''
                    }}
                >
                    {({ handleSubmit, isSubmitting, dirty, isValid}) => (
                        <Form onSubmit={handleSubmit} autoComplete='off' className='contactForm-Formik'>
                            <div className="inputsOne">
                                <MyTextInput name='firstName' placeholder='Your Name' />
                                <MyTextInput name='email' placeholder='Your Email' />
                            </div>
                            <div className="inputsTwo">
                                <MyTextInput name='subject' placeholder='Subject' />
                                <MyTextInput name='phone' placeholder='Number' />
                            </div>
                            <MyTextArea name='message' placeholder='Your Message' />

                            <Button 
                                type='submit'
                                disabled={isSubmitting || !dirty || !isValid} 
                                className='button'
                            >
                                <Spinner
                                    variant='dark'
                                    as="span"
                                    animation="border"  
                                    size="sm"
                                    role="status"
                                    aria-hidden="true"
                                    hidden={loading}
                                />
                                Submit
                            </Button>
                        </Form>
                    )}
                </Formik>
            </div>
        </div>
    )
})
