import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/serverError";


export default class CommonStore{
    token: string | null = window.localStorage.getItem('jwt');
    appLoaded = false;
    error: ServerError | null = null;
    modalShow = false;

    constructor(){
        makeAutoObservable(this);

        reaction(
            () => this.token,
            token => {
                if (token){
                    window.localStorage.setItem('jwt', token)
                }else {
                    window.localStorage.removeItem('jwt')
                }
            }
        )
    }


    setServerError = (error: ServerError) => {
        this.error = error;
    }
    
    setToken = (token: string | null) => {
        this.token = token;
    }

    setAppLoaded = () => {
        this.appLoaded = true;
    }
    setModalShow = (show: boolean) => {
        this.modalShow = show;
    }
}