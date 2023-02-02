import { observer } from 'mobx-react-lite'
import React from 'react'
import { Button } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import { useStore } from '../../app/stores/store'

export default observer(function Home() {

  const{userStore: {logout}} = useStore();
  return (
    <div>Home
      <Button onClick={logout}>Logout</Button> 
      <a href="https://localhost:7115/Movies">Go</a>    
      <Link to='errors'>
        <Button>Errors</Button>  
      </Link> 
    </div>
  )
}
)
