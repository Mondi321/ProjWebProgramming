import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Review } from "../models/review";

export default class ReviewStore{
    reviewRegistry = new Map<string, Review>();
    loading = true;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this)
    }

    get reviews() {
        return Array.from(this.reviewRegistry.values());
    }

    private setReview = (review: Review) => {
        this.reviewRegistry.set(review.id!, review);
    }

    private getReview = (id: string) => {
        return this.reviewRegistry.get(id);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    createReview = async (review: Review) => {
        this.loading = false;
        try {
            await agent.Reviews.create(review);
            runInAction(() => {
                this.reviewRegistry.set(review.id!, review);
                this.loading = true;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = true;
            })
        }
    }


}