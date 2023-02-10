import { Genre } from './genre';
import {Movie} from './movie';

export interface MovieGenre{
    createdAt: string;
    movieId: string;
    movie?: Movie;
    genreId: string;
    genre?: Genre;
}