const _BASE_URL = window["_BASE_URL"] || "";
let headers;
if (_BASE_URL) {
    console.log("_BASE_URL set to ", window["_BASE_URL"]);
    headers = { "Access-Control-Allow-Origin": "*" };
}
export const get = (url) => fetch(`${_BASE_URL}${url}`, { headers });
//# sourceMappingURL=fetch.js.map