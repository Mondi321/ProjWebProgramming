import React from 'react'
import { NavLink, Route } from 'react-router-dom'
import SignIn from './SignIn'
import SignUp from './SignUp'

export default function Forms() {
    return (
        <>
            <div className="App">
                <div className="appAside">
                    <img src="/assets/intros1.jpg" alt=''/>
                </div>
                <div className="appForm">
                    <div className="pageSwitcher">
                        <NavLink
                            exact
                            to="/sign-in"
                            activeClassName="pageSwitcherItem-active"
                            className="pageSwitcherItem"
                        >
                            Sign In
                        </NavLink>
                        <NavLink
                            to="/sign-up"
                            activeClassName="pageSwitcherItem-active"
                            className="pageSwitcherItem"
                        >
                            Sign Up
                        </NavLink>
                    </div>

                    <div className="formTitle">
                        <NavLink
                            exact
                            to="/sign-in"
                            activeClassName="formTitleLink-active"
                            className="formTitleLink"
                        >
                            Sign In
                        </NavLink>{" "}
                        or{" "}
                        <NavLink
                            to="/sign-up"
                            activeClassName="formTitleLink-active"
                            className="formTitleLink"
                        >
                            Sign Up
                        </NavLink>
                    </div>

                    <Route exact path="/sign-in" component={SignIn} />
                    <Route path="/sign-up" component={SignUp} />
                </div>
            </div>
        </>
    )
}
