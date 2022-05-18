//TODO: if the api server port is wrong no request will be sent to it
// current api server port is 7164
// current client port is 8081
const serverURL = "https://localhost:7134/api/";
const fetcher = async (endpoint) => {
    let payload;
    try {
        let response = await fetch(`${serverURL}${endpoint}`);
        payload = await response.json();
    } catch (err) {
        console.log(err);
        payload = { error: `Error has occured: ${err.message}` };
    }
    return payload;
};
export { fetcher };
