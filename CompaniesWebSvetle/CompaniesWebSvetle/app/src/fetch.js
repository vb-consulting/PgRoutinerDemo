const _BASE_URL = window["_BASE_URL"] || "";
let headers;
if (_BASE_URL) {
    console.log("_BASE_URL set to ", window["_BASE_URL"]);
    headers = { "Access-Control-Allow-Origin": "*" };
}
export const get = async (url) => {
    const response = await fetch(`${_BASE_URL}${url}`, { headers });
    if (response.ok) {
        return await response.json();
    }
    throw new Error(await response.text());
};
export const post = async (url, data) => {
    let init = {
        method: "POST",
        body: JSON.stringify(data),
        headers: { ...headers, ...{ "Accept": "application/json", "Content-Type": "application/json" } }
    };
    const response = await fetch(`${_BASE_URL}${url}`, init);
    if (response.ok) {
        return;
    }
    throw new Error(await response.text());
};
//# sourceMappingURL=fetch.js.map