import { Movie } from "./movie";

export interface Actor{
    actorId: string;
    firstName: string;
    lastName: string;
    nationality: string;
    birthDate: string;
    image: string;
    movies?: Movie[]
}