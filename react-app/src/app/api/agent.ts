import axios, { AxiosError, AxiosResponse } from "axios"
import { toast } from "react-toastify";
import { history } from "../..";
import { Contact } from "../models/contact";
import { Genre } from "../models/genre";
import { Movie } from "../models/movie";
import { MovieGenre } from "../models/movieGenre";
import { Review } from "../models/review";
import { TvShow } from "../models/tvShow";
import { User, UserFormValues } from "../models/user";
import { store } from "../stores/store";

const sleep =(delay: number) => {
    return new Promise(resolve => {
        setTimeout(resolve, delay)
    })
}


axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token) {
        config!.headers!.Authorization = `Bearer ${token}`;
    }
    return config;
})

axios.interceptors.response.use(async response => {
        if(process.env.NODE_ENV === 'development') await sleep(1000);
        return response;
}, (error: AxiosError) => {
    const {status, data, config}:any = error.response!;
    switch (status) {
        case 400:
            if (typeof data === 'string'){
                toast.error(data);
            }
            if (config.method === 'get' && data.errors.hasOwnProperty('id')){
                history.push('/not-found');
            }
            if (data.errors) {
                const modalStateErrors = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateErrors.push(data.errors[key])
                    }
                }
                throw modalStateErrors.flat();
            } 
            break;
        case 401:
            toast.error('unathorized');
            break;
        case 403:
            toast.error('Forbidden');
            break;
        case 404:
            history.push('/not-found');
            break;
        case 500:
            store.commonStore.setServerError(data);
            history.push('/server-error');
            break;
    }
    return Promise.reject(error);
})


axios.defaults.baseURL = "https://localhost:7115/api";

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Account ={
    current: () => requests.get<User>('/authentication'),
    login: (user: UserFormValues) => requests.post<User>('/authentication/login', user),
    register: (user: UserFormValues) => requests.post<User>('/authentication/register', user)
}

const Movies = {
    list: () => requests.get<Movie[]>('/movies'),
    listTopRated: () => requests.get<Movie[]>('/movies/toprated'),
    listByGenre: (id:string) => requests.get<MovieGenre[]>(`/bygenre/${id}`),
    details: (id: string) => requests.get<Movie>(`/movies/${id}`)
}

const TvShows = {
    list: () => requests.get<TvShow[]>('/tvshows'),
    details: (id: string) => requests.get<TvShow>(`/tvshows/${id}`)
}

const Genres = {
    list: () => requests.get<Genre[]>('/genres')
}

const Reviews = {
    create: (review: Review) => requests.post<void>('/reviews', review)
}

const Contacts = {
    create: (contact: Contact) => requests.post<void>('/contacts', contact)
}

const agent = {
    Account,
    Movies,
    TvShows,
    Genres,
    Reviews,
    Contacts
}

export default agent;