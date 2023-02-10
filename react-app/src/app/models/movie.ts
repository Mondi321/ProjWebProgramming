import { Actor } from "./actor";
import { Director } from "./director";
import { Genre } from "./genre";
import { User } from "./user";

export interface Movie{
    movieId: string;
    title: string;
    description: string;   
    releaseYear: string;
    rating: number;
    movieLength: number;
    image?: string;
    imageCarousel?:string;
    genres?: Genre[];
    actors?: Actor[];
    users?: User[];
    directorId: string;
    director?: Director; 
}