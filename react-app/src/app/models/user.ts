import { Movie } from "./movie";

export interface User {
    id?: string;
    firstName: string;
    userName: string;
    token: string;
    movies?: Movie[];
    roli: string[];
}

export interface UserFormValues{
    email: string;
    password: string;
    firstName?: string;
    userName?:string;
}