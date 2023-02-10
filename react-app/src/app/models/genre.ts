import { Movie } from "./movie";
import { TvShow } from "./tvShow";

export interface Genre{
    genreId: string;
    name: string;
    movies?: Movie[];
    tvShows?: TvShow[];
}