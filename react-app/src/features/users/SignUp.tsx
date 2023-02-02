import { ErrorMessage, Form, Formik } from 'formik';
import React from 'react'
import { Link } from 'react-router-dom';
import { useStore } from '../../app/stores/store';
import { Button} from 'react-bootstrap';
import * as Yup from 'yup';
import { observer } from 'mobx-react-lite';
import ValidationErrors from '../errors/ValidationError';
import MyTextInput from '../../app/common/MyTextInput';

export default observer(function SignUp() {

    const { userStore } = useStore();

    return (
        <div className="formCenter">
            <Formik
                initialValues={{ firstName: '', userName: '', email: '', password: '', error: null }}
                onSubmit={(values, { setErrors }) => userStore.register(values).catch(error =>
                    setErrors({ error }))}
                    validationSchema={Yup.object({
                        firstName: Yup.string().required(),
                        userName: Yup.string().required(),
                        email: Yup.string().required().email(),
                        password: Yup.string().required()
                    })}
            >
                {({ handleSubmit, errors, isValid , dirty, isSubmitting}) => (
                    <Form className='formFields error' onSubmit={handleSubmit} autoComplete='off'>
                        <MyTextInput label='First Name' name='firstName' placeholder='Enter your first name' />
                        <MyTextInput label='Username' name='userName' placeholder='Enter your username' />
                        <MyTextInput label='E-Mail Address' name='email' placeholder='Enter your email' />
                        <MyTextInput label='Password' name='password' placeholder='Enter your password' type='password' />
                        <ErrorMessage
                            name='error'
                            render={() =>
                                (<ValidationErrors errors={errors.error} />)}
                        />
                        <div className="formField">
                        <Button  type='submit' className='formFieldButton' variant='danger'>Sign Up</Button>
                            {" "}
                            <Link to="/sign-in" className="formFieldLink">
                                I'm already member
                            </Link>
                        </div>
                    </Form>
                )}
            </Formik>
        </div>
        // <div className="formCenter">
        //     <form autoComplete='off' onSubmit={handleSubmit} className="formFields">
        //         <div className="formField">
        //             <label className="formFieldLabel" htmlFor="name">
        //                 Full Name
        //             </label>
        //             <input
        //                 type="text"
        //                 id="name"
        //                 className="formFieldInput"
        //                 placeholder="Enter your full name"
        //                 name="name"
        //                 value={info.name}
        //                 onChange={handleChange}
        //             />
        //         </div>
        //         <div className="formField">
        //             <label className="formFieldLabel" htmlFor="password">
        //                 Password
        //             </label>
        //             <input
        //                 type="password"
        //                 id="password"
        //                 className="formFieldInput"
        //                 placeholder="Enter your password"
        //                 name="password"
        //                 value={info.password}
        //                 onChange={handleChange}
        //                 autoComplete='false'
        //             />
        //         </div>
        //         <div className="formField">
        //             <label className="formFieldLabel" htmlFor="email">
        //                 E-Mail Address
        //             </label>
        //             <input
        //                 type="email"
        //                 id="email"
        //                 className="formFieldInput"
        //                 placeholder="Enter your email"
        //                 name="email"
        //                 value={info.email}
        //                 onChange={handleChange}
        //             />
        //         </div>

        //         <div className="formField">
        //             <label className="formFieldCheckboxLabel">
        //                 <input
        //                     className="formFieldCheckbox"
        //                     type="checkbox"
        //                     name="hasAgreed"
        //                     checked={info.hasAgreed}
        //                     onChange={handleChange}
        //                 />{" "}
        //                 I agree all statements in{" "}
        //                 <a href="null" className="formFieldTermsLink">
        //                     terms of service
        //                 </a>
        //             </label>
        //         </div>

        //         <div className="formField">
        //             <button className="formFieldButton">Sign Up</button>{" "}
        //             <Link to="/sign-in" className="formFieldLink">
        //                 I'm already member
        //             </Link>
        //         </div>
        //     </form>
        // </div>
    )
})
