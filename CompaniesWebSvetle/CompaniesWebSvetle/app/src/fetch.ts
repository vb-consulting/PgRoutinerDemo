const _BASE_URL = window["_BASE_URL"] || "";
let headers: HeadersInit;

if (_BASE_URL) {
    console.log("_BASE_URL set to ", window["_BASE_URL"]);
    headers = {"Access-Control-Allow-Origin": "*"}
}

export const get = async <T> (url: string) => { 
    const response = await fetch(`${_BASE_URL}${url}`, {headers}); 
    if (response.ok) {
        return await response.json() as T;
    }
    throw new Error(await response.text());
}

export const post = async <T> (url: string, data?: T) => { 
    let init: RequestInit = {
        method: "POST",
        body: JSON.stringify(data),
        headers: {...headers, ...{"Accept": "application/json", "Content-Type": "application/json"}}
    }
    const response = await fetch(`${_BASE_URL}${url}`, init); 
    if (response.ok) {
        return;
    }
    throw new Error(await response.text());
}