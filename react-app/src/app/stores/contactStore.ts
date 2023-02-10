import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Contact } from "../models/contact";

export default class ContactStore {
    contactRegistry = new Map<string, Contact>();
    editMode = false;
    loading = true;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this)
    }

    get contacts() {
        return Array.from(this.contactRegistry.values());
    }

    private setContact = (contact: Contact) => {
        this.contactRegistry.set(contact.contactId, contact);
    }

    private getContact = (id: string) => {
        return this.contactRegistry.get(id);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }


    createContact = async (contact: Contact) => {
        this.loading = false;
        try {
            await agent.Contacts.create(contact);
            runInAction(() => {
                this.contactRegistry.set(contact.contactId, contact);
                this.editMode = false;
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