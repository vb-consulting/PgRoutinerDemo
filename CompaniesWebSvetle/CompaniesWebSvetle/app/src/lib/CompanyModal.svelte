<script lang="ts">
import { createEventDispatcher } from 'svelte';
import Modal from "./Modal.svelte";
import { get, post } from "../fetch";

export let areas: Promise<Array<ICompanyArea>>;
let company: ICompany = {} as ICompany;

let title: string;
let btnCaption: string;
let isNew: boolean;

let modal: Modal;
const dispatch = createEventDispatcher();

async function update() {
    console.log(`Updating company ${company.id}: ${company.name}`);
    await post<ICompany>("companies", company);
    dispatch("update");
    modal.close();
}

async function remove() {
    console.log(`Deleting company ${company.id}: ${company.name}`);
    await post<any>(`companies/delete/${company.id}`);
    dispatch("update");
    modal.close();
}

export async function open(id?: number) {
    modal.open();
    if (!id) {
        isNew = true;
        title = "Create new company";
        btnCaption = "Insert";
        company = {} as ICompany;
    } else {
        isNew = false;
        title = "Update";
        btnCaption = "Update";
        company = await get<ICompany>(`companies/read/${id}`);
        title = company.name;
    }
}
</script>

<Modal bind:this={modal} title={title}>
    <span slot="body">
        <form>
            <div class="mb-3">
                <label for="name" class="col-form-label">Name:</label>
                <input type="text" class="form-control" id="name" bind:value={company.name}>
            </div>
            <div class="mb-3">
                <label for="website" class="col-form-label">Website:</label>
                <input type="text" class="form-control" id="website" bind:value={company.website}>
            </div>
            <div class="mb-3">
                <label for="areas" class="col-form-label">Area:</label>
                <select class="form-select" id="areas" bind:value={company.areaId}>
                    {#await areas}
                        <option selected>awaiting...</option>
                    {:then areas}
                        <option selected>Select area...</option>
                    {#each areas as area}
                        <option value="{area.id}">{area.name}</option>
                    {/each}
                    {:catch error}
                        <option selected>error</option>
                    {/await}
                </select>
            </div>
            <div class="mb-3">
                <label for="about" class="col-form-label">About:</label>
                <textarea class="form-control" id="about" bind:value={company.about}></textarea>
            </div>
        </form>
    </span>
    <span slot="footer">
        <button type="button" class="btn btn-primary" on:click={update}>{btnCaption}</button>
        {#if !isNew}
            <button type="button" class="btn btn-danger" on:click={remove}>Delete</button>
        {/if}
    </span>
</Modal>
