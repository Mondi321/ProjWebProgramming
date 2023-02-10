import { User } from "./user";

export interface Review{
    id: string;
    mesazhi: string;
    ratingValue: number;
    userId?: string;
    user?: User;
}