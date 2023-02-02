import { observer } from 'mobx-react-lite';
import React from 'react'
import { Alert, Container } from 'react-bootstrap';
import { useStore } from '../../app/stores/store'

export default observer(function ServerError() {
    const{commonStore} = useStore();
  return (
    <Container>
        <h1 className='text-center mt-3'>Server Error</h1>
        <h5 style={{color: 'red', textAlign: 'center'}}>{commonStore.error?.message}</h5>
        {commonStore.error?.details &&
            <Alert className='mt-5'>
                <Alert.Heading>
                    Stack Trace
                </Alert.Heading>
                <code>{commonStore.error.details}</code>
            </Alert>
        }
    </Container>
  )
})