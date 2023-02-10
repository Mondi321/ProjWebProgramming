import React, { useState } from 'react'
import { Button, Form, Modal } from 'react-bootstrap';
import { Rating } from 'react-simple-star-rating'
import { Review } from '../../app/models/review';
import {v4 as uuid} from 'uuid';
import { useStore } from '../../app/stores/store';
import { useHistory } from 'react-router-dom';

interface Props {
    modalShow: boolean;
    setModalShow: (hide: boolean) => void;
}

export default function ReviewModal({ modalShow, setModalShow }: Props) {
    const [rating, setRating] = useState(0);

    const {reviewStore} = useStore();
    const {createReview} = reviewStore;
    const history = useHistory();
    const [mesazhi, setMesazhi] = useState('');

    const review = {
        id: '',
        mesazhi: mesazhi,
        ratingValue: rating
    }

    const handleRating = (rate: number) => {
        setRating(rate);
        console.log(rating)
    }

    function handleMesazhi(event: React.ChangeEvent<HTMLInputElement>){
        const mesazhiValue = event.target.value;
        setMesazhi(mesazhiValue);
        console.log(mesazhi)
    }

    function handleFormSubmit(review: Review){
        let newReview = {
            ...review,
            id: uuid()
        }
        createReview(newReview).then(() => history.push('/home'));
    }

    return (
        <Modal
            show={modalShow}
            onHide={() => setModalShow(false)}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Review Message
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={() => handleFormSubmit(review)}>
                    <Form.Label>Rating Value</Form.Label>
                    <div>
                        <Rating onClick={handleRating} initialValue={rating} />
                    </div>
                    <Form.Group
                        className="mb-3 mt-3"
                        controlId="exampleForm.ControlTextarea1"
                    >
                        <Form.Label>Message</Form.Label>
                        <Form.Control as="textarea" rows={3} value={mesazhi} onChange={handleMesazhi} />
                    </Form.Group>
                    <Button type='submit'>Submit</Button>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={() => setModalShow(false)}>Close</Button>
            </Modal.Footer>
        </Modal>
    )
}
