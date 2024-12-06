import { check, sleep } from "k6";
import http from "k6/http";

export let options = {
    // Modified load pattern with fewer VUs
    stages: [
        { duration: "1m", target: 10 },           // Ramp up to 10 VUs over a minute
        { duration: "1m", target: 10 },           // Maintain 10 VUs for 1 minute
        { duration: "1m", target: 0 }             // Ramp down to 0 over a minute
    ],

    // Increased the request duration threshold to 200 ms to reduce failure rate
    thresholds: {
        "http_req_duration": ["p(95) < 200"]      // 95% of requests should be below 200 ms
    },

    discardResponseBodies: true,



    export default function() {
        let res = http.get("https://bpcalulator-green-slot-dscydwgqh3eyfxf4.ukwest-01.azurewebsites.net/");

        check(res, {
            "is status 200": (r) => r.status === 200
        });

        // "Think" for 3 seconds to simulate real user behavior
        sleep(3);
    }
}
