import React from 'react'
import { Button } from 'react-bootstrap'
import { Link } from 'react-router-dom'

export default function NotFound() {
    return (
        <>
            <div className='notFoundPage'>
                <div className="mars"></div>
                <img src="https://assets.codepen.io/1538474/404.svg" className="logo-404" alt=''/>
                <img src="https://assets.codepen.io/1538474/meteor.svg" className="meteor" alt=''/>
                <p className="title">Oh no!!</p>
                <p className="subtitle">
                    You’re either misspelling the URL <br /> or requesting a page that's no longer here.
                </p>
                <div className='text-center'>
                    <Link to='/home'>
                        <Button variant='light'>Go Back</Button>
                    </Link>
                </div>
                <img src="https://assets.codepen.io/1538474/astronaut.svg" className="astronaut" alt=''/>
                <img src="https://assets.codepen.io/1538474/spaceship.svg" className="spaceship" alt='' />
            </div>
        </>
    )
}