import { observer } from 'mobx-react-lite'
import React from 'react'
import { Button } from 'react-bootstrap'
import { BiCameraMovie } from 'react-icons/bi'
import { Link } from 'react-router-dom'
import { useStore } from '../stores/store'
import './intro.css'

export default observer(function Intro() {
  const { userStore } = useStore();
  return (
    <>
      <div className='backgroundImg'>

      </div>
      <div className="header">
        <BiCameraMovie />
        {!userStore.isLoggedIn && (
          <Link to='/sign-in'>
            <Button variant='danger'>Login</Button>
          </Link>
        )
        }
      </div>
      <div className='introText'>
        <h1>Unlimited movies, TV <br /> shows, and more.</h1>
        <h5>Ready to watch? Register to our streaming platform.</h5>
        {userStore.isLoggedIn ? (
          <>
            <Link to='/home'>
              <Button variant='danger'>Continue</Button>
            </Link>
          </>
        ) : (
          <>
            <Link to='/sign-up'>
              <Button variant='danger'>Register</Button>
            </Link>
          </>
        )}

      </div>

    </>
  )
})
