import { check, sleep } from "k6";
import http from "k6/http";
import { BASE_URL } from "./config.js"; // Import the global variable
//Load testing helps to determine how your application performs under expected load conditions.


export let options = {
    stages: [
        { duration: "1m", target: 10 },  // Ramp up to 20 users over 1 minute
        { duration: "1m", target: 10 },  // Stay at 20 users for 3 minutes
        { duration: "1m", target: 0 }    // Ramp down to 0 users over 1 minute
    ],
    thresholds: {
        "http_req_duration": ["p(80) < 200"], // 80% of requests must be below 200ms
    },
};

export default function () {
    let res = http.get("https://bpcalulator-green-slot-dscydwgqh3eyfxf4.ukwest-01.azurewebsites.net/");
    check(res, {
        "is status 200": (r) => r.status === 200,
    });
    sleep(1);
}
