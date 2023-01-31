import React from 'react'
import { Button } from 'react-bootstrap'
import {BiCameraMovie} from 'react-icons/bi'
import './intro.css'

export default function Intro() {
  return (
    <>
      <div className='backgroundImg'>

      </div>
      <div className="header">
        <BiCameraMovie />
        <Button variant='danger'>Login</Button>
      </div>
      <div className='introText'>
        <h1>Unlimited movies, TV <br /> shows, and more.</h1>
        <h5>Ready to watch? Register to our streaming platform.</h5>
        <Button variant='danger'>Register</Button>
      </div>

    </>
  )
}
