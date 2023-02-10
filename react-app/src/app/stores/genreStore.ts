import { makeAutoObservable } from "mobx";
import agent from "../api/agent";
import { Genre } from "../models/genre";

export default class GenreStore {
    genreRegistry = new Map<string, Genre>();
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this)
    }
    
    get genres() {
        return Array.from(this.genreRegistry.values()).sort(function(a, b){
            let x = a.name.toLowerCase();
            let y = b.name.toLowerCase();
            if (x < y) {return -1;}
            if (x > y) {return 1;}
            return 0;
          });
    }


    private setGenre = (genre: Genre) => {
        this.genreRegistry.set(genre.genreId, genre);
    }

    private getGenre = (genreId: string) => {
        return this.genreRegistry.get(genreId);
    }



    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadGenres = async () => {
        this.setLoadingInitial(true);
        try {
            const genres = await agent.Genres.list();
            genres.forEach(genre => {
                this.setGenre(genre);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

}