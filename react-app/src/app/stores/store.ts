import { createContext, useContext } from "react"
import CommonStore from "./commonStore";
import ContactStore from "./contactStore";
import GenreStore from "./genreStore";
import MovieStore from "./movieStore";
import ReviewStore from "./reviewStore";
import TvShowStore from "./tvShowStore";
import UserStore from "./userStore";

interface Store{
    commonStore: CommonStore;
    userStore: UserStore;
    movieStore: MovieStore;
    tvShowStore: TvShowStore;
    genreStore: GenreStore;
    reviewStore: ReviewStore;
    contactStore: ContactStore;
}

export const store: Store={
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    movieStore: new MovieStore(),
    tvShowStore: new TvShowStore(),
    genreStore: new GenreStore(),
    reviewStore: new ReviewStore(),
    contactStore: new ContactStore()
}

export const StoreContext = createContext(store);

export function useStore(){
    return useContext(StoreContext);
}
