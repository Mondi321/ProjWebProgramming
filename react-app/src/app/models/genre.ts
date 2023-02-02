import { Movie } from "./movie";

export interface Genre{
    genreId: string;
    name: string;
    movies?: Movie[];
}