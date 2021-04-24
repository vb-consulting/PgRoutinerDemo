const _BASE_URL = window["_BASE_URL"] || "";
let headers: HeadersInit;

if (_BASE_URL) {
    console.log("_BASE_URL set to ", window["_BASE_URL"]);
    headers = {"Access-Control-Allow-Origin": "*"}
}

export const get = (url: string) => fetch(`${_BASE_URL}${url}`, {headers});
