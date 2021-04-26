/// <reference types="svelte" />
/// <reference types="vite/client" />

interface ICompanyArea {
    id: number;
    name: string;
}

interface ISearchResultPage
{
    id: number;
    name: string;
    website: string;
    area: string;
    about: string;
    modified: DateTime;
}

interface ISearchResults {
    count: number;
    page: Array<ISearchResultPage>;
}

interface ISearchFilter {
    search: string;
    areaId?: number;
}

interface ICompany {
    id: number;
    name: string;
    nameNormalized: string;
    website: string;
    areaId: number;
    about: string;
    modified: DateTime;
}