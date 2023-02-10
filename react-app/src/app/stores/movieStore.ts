import { makeAutoObservable } from "mobx";
import agent from "../api/agent";
import { Movie } from "../models/movie";

export default class MovieStore {
    movieRegistry = new Map<string, Movie>();
    movieRegistryTopRated = new Map<string, Movie>();
    // movieRegistryByGenre = new Map<string, MovieGenre>();
    loadingInitial = false;
    // movieState = Array.from(this.movieRegistry.values());

    constructor() {
        makeAutoObservable(this)
    }

    get movies7() {
        return Array.from(this.movieRegistry.values()).slice(0,7);
    }

    get moviesCarousel() {
        return Array.from(this.movieRegistry.values()).slice(0,3);
    }
    
    get movies() {
        return Array.from(this.movieRegistry.values());
    }

    get moviesTopRated() {
        return Array.from(this.movieRegistryTopRated.values()).slice(0,7);
    }

    // setMovieState(movie:Movie[]){
    //     this.movieState = movie;
    // }


    // get moviesByGenre() {
    //     return Array.from(this.movieRegistryByGenre.values());
    // }

    private setMovie = (movie: Movie) => {
        this.movieRegistry.set(movie.movieId, movie);
    }

    private getMovie = (movieId: string) => {
        return this.movieRegistry.get(movieId);
    }

    private setMovieTopRated = (movie: Movie) => {
        this.movieRegistryTopRated.set(movie.movieId, movie);
    }

    private getMovieTopRated = (movieId: string) => {
        return this.movieRegistryTopRated.get(movieId);
    }

    // private setMovieByGenre = (movie: MovieGenre) => {
    //     this.movieRegistryByGenre.set(movie.movieId, movie);
    // }

    // private getMovieByGenre = (movieId: string) => {
    //     return this.movieRegistryByGenre.get(movieId);
    // }


    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadMovies = async () => {
        this.setLoadingInitial(true);
        try {
            const movies = await agent.Movies.list();
            movies.forEach(movie => {
                this.setMovie(movie);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    loadMoviesTopRated = async () => {
        this.setLoadingInitial(true);
        try {
            const movies = await agent.Movies.listTopRated();
            movies.forEach(movie => {
                this.setMovieTopRated(movie);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    loadMovie = async (movieId: string) => {
        let movie = this.getMovie(movieId);
        this.loadingInitial = true;
        try {
            movie = await agent.Movies.details(movieId);
            this.setMovie(movie);
            this.setLoadingInitial(false);
            return movie;
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

}