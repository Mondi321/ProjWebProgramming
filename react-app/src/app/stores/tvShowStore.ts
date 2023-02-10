import { makeAutoObservable } from "mobx";
import agent from "../api/agent";
import { TvShow } from "../models/tvShow";

export default class TvShowStore {
    tvShowRegistry = new Map<string, TvShow>();
    loadingInitialTvShow = false;

    constructor() {
        makeAutoObservable(this)
    }

    get tvShows7() {
        return Array.from(this.tvShowRegistry.values()).slice(0,7);
    }

    get tvShows() {
        return Array.from(this.tvShowRegistry.values());
    }

    private setTvShow = (tvShow: TvShow) => {
        this.tvShowRegistry.set(tvShow.tvShowId, tvShow);
    }

    private getTvShow = (tvShowId: string) => {
        return this.tvShowRegistry.get(tvShowId);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitialTvShow = state;
    }

    loadTvShows = async () => {
        this.setLoadingInitial(true);
        try {
            const tvShows = await agent.TvShows.list();
            tvShows.forEach(tvShow => {
                this.setTvShow(tvShow);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    loadTvShow = async (tvShowId: string) => {
        let tvShow = this.getTvShow(tvShowId);
        this.loadingInitialTvShow = true;
        try {
            tvShow = await agent.TvShows.details(tvShowId);
            this.setTvShow(tvShow);
            this.setLoadingInitial(false);
            return tvShow;
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }
}