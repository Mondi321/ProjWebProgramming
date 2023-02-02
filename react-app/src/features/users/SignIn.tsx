import { ErrorMessage, Form, Formik } from 'formik';
import React from 'react';
import { Link } from 'react-router-dom';
import { useStore } from '../../app/stores/store';
import {Button, Form as formB} from 'react-bootstrap';
import { observer } from 'mobx-react-lite';
import MyTextInput from '../../app/common/MyTextInput';

export default observer(function SignIn() {

    const {userStore} = useStore();

    return (
        <div className="formCenter">
            <Formik
                initialValues={{ email: '', password: '', error: null}}
                onSubmit={(values, {setErrors}) => userStore.login(values).catch(error =>
                    setErrors({error: 'Invalid email or password'}))}
            >
                {({ handleSubmit, errors}) => (
                    <Form className='formFields' onSubmit={handleSubmit} autoComplete='off'>
                        <MyTextInput label='E-Mail Address' name='email' placeholder='Enter your email' />
                        <MyTextInput label='Password' name='password' placeholder='Enter your password' type='password' />
                        <ErrorMessage 
                            name='error'
                            render={() => 
                            (<formB.Control.Feedback style={{marginBottom: '10px',display: 'block'}} type='invalid'>{errors.error}</formB.Control.Feedback>)}
                        />
                        <div className="formField">
                            {/* <Link to='/homePage'> */}
                                {/* <button type='submit' className="formFieldButton">Sign In</button> */}
                            {/* </Link> */}
                            <Button type='submit' className='formFieldButton' variant='danger'>Sign In</Button>
                            {" "}
                            <Link to="/sign-up" className="formFieldLink">
                                Create an account
                            </Link>
                        </div>
                    </Form>
                )}
            </Formik>
            {/* <form autoComplete='off' className="formFields" onSubmit={handleSubmit}>
                <div className="formField">
                    <label className="formFieldLabel" htmlFor="email">
                        E-Mail Address
                    </label>
                    <input
                        type="email"
                        id="email"
                        className="formFieldInput"
                        placeholder="Enter your email"
                        name="email"
                        value={info.email}
                        onChange={handleChange}
                    />
                </div>

                <div className="formField">
                    <label className="formFieldLabel" htmlFor="password">
                        Password
                    </label>
                    <input
                        type="password"
                        id="password"
                        className="formFieldInput"
                        placeholder="Enter your password"
                        name="password"
                        value={info.password}
                        onChange={handleChange}
                        autoComplete='false'
                    />
                </div>

                <div className="formField">
                    <Link to='/homePage'>
                        <button className="formFieldButton">Sign In</button>
                    </Link>
                    {" "}
                    <Link to="/sign-up" className="formFieldLink">
                        Create an account
                    </Link>
                </div>
            </form> */}
        </div>
    )
})
