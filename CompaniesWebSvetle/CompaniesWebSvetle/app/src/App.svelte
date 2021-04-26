<script lang="ts">
import { onMount } from 'svelte';
import Nav from "./lib/_Nav.svelte";
import Footer from "./lib/_Footer.svelte";
import CompanyModal from "./lib/CompanyModal.svelte";
import { get } from "./fetch";

const pageSize = 10;
const maxButtons = 3;

let page = 1;
let pages: number = null;

let areas: Promise<Array<ICompanyArea>> = get<Array<ICompanyArea>>("companies/areas");
let result: ISearchResults;
let filter: ISearchFilter = {search: "", areaId: undefined};

let buttons: Array<number>;
let modal: CompanyModal;

getSearchResults();

export async function getSearchResults(newPage?: number) {
    let forPage = newPage ? newPage : page;
    filter.areaId = filter.areaId == undefined ? null : filter.areaId;
    console.log(`loading page ${forPage} for area id=${filter.areaId}`);
    result = await get<ISearchResults>(`companies/${encodeURIComponent(JSON.stringify(filter))}/${forPage}/${pageSize}`);
    pages = Math.ceil(result.count / pageSize);
    if (newPage) {
        page = newPage;
    }
    let from = Math.round(page - (maxButtons / 2) < 1 ? 1 : page - (maxButtons / 2));
    let to = Math.round(from + maxButtons > pages ? pages : from + maxButtons - 1);
    buttons = [];
    for (let i = from; i <= to; i++) {
        buttons.push(i);
    }
}

let inputTimeout: number = null;

async function searchInputChanged(e: any) {
    filter.search = e.target.value;
    if (inputTimeout) {
        clearTimeout(inputTimeout)
    }
    inputTimeout = setTimeout(async () => { 
        await getSearchResults(1);
        inputTimeout = null;
    }, 250) as unknown as number;
}

async function areaSelectChanged(e: any) {
    filter.areaId = isNaN(Number(e.target.value)) ? null : Number(e.target.value);
    await getSearchResults(1);
}

function mark(source: string, search: string) {
    if (!source) {
        return source;
    }
    if (!search) {
        return source;
    }
    const index = source.toLowerCase().indexOf(search.toLowerCase());
    if (index !== -1) {
        const segment = source.substring(index, index + search.length);
        return `${source.substring(0, index)}<mark>${segment}</mark>${source.substring(index + search.length, source.length)}`;
    }
    return source;
}
</script>

<Nav brandContent="PgRoutiner Demo" />

<!-- svelte-ignore a11y-no-onchange -->
<main class="container vh-100">

  <div class="row filter">
      <div class="col form-group input-group">
          <input type="text" class="form-control"
              autocorrect="off" autocapitalize="off" spellcheck="false"
              placeholder="Search by name..." on:input={searchInputChanged}/>
          <select class="form-select-lg" on:change={areaSelectChanged}>
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
  </div>

  <div class="row">
      <div class="col">
          <button type="button" class="btn btn-primary" on:click={() => modal.open()}>
              <i class="icon-doc-new" />
              Add New
          </button>
      </div>
  </div>

  <div class="row pager">
      <div class="col">
          <button type="button" class="btn btn-sm btn-outline-primary{(page == 1 ? " disabled" : "")}" on:click={() => getSearchResults(1)}>
              First
          </button>
          {#if pages}
              {#each buttons as i}
                  <button type="button" class="btn btn-sm btn-outline-primary{(page == i ? " disabled" : "")}" on:click={() => getSearchResults(i)}>
                      Page {i}
                  </button>
              {/each}
          {/if}
          <button type="button" class="btn btn-sm btn-outline-primary{(page == pages ? " disabled" : "")}" on:click={() => getSearchResults(pages)}>
              Last
          </button>
      </div>
  </div>

  <div class="row">
      <table class="table table-hover">
          <thead>
              <tr>
                  <th scope="col"></th>
                  <th scope="col">Company</th>
                  <th scope="col">Website</th>
                  <th scope="col">Area</th>
                  <th scope="col">About</th>
              </tr>
          </thead>
          <tbody>
              {#if !pages}
              <tr>
                  <td colspan="5">
                      <div class="text-center justify-content-center">
                          <h1 class="display-4">
                              <span class="icon-spin1 animate-spin"></span>
                          </h1>
                      </div>
                  </td>
              </tr>
              {:else}
                  {#each result.page as company}
                  <tr>
                      <th scope="row">
                          <button type="button" class="btn btn-sm btn-outline-primary icon-edit" on:click={() => modal.open(company.id)}></button>
                      </th>
                      <td>{@html mark(company.name, filter.search)}</td>
                      <td>{company.website}</td>
                      <td>{company.area}</td>
                      <td>{company.about}</td>
                  </tr>
                  {/each}
              {/if}
          </tbody>
      </table>
  </div>

</main>
<Footer />
<CompanyModal bind:this={modal} areas={areas} on:update={() => getSearchResults()}/>

<style lang="scss">
      main {
        padding-top: 65px;
        .filter {
            input, select {
                margin-top: 10px;
                margin-bottom: 10px;
            }
            input {
                max-width: 50%;
            }
            select {
                margin-left: 10px !important;
            }
        }
        .pager {
            margin-top: 25px;
        }
    }
</style>
