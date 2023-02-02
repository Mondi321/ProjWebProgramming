import { Movie } from "./movie";

export interface Director{
    directorId: string;
    firstName: string;
    lastName: string;
    nationality: string;
    birthDate: string;
    image: string;
    movies?: Movie[]
}