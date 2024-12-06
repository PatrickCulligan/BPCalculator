import { check, sleep } from "k6";
import http from "k6/http";
//Spike testing helps to determine how your application handles sudden, extreme spikes in traffic.

export let options = {
    stages: [
        { duration: "10s", target: 20 }, // Quickly ramp up to 20 users
        { duration: "30s", target: 100 }, // Spike to 100 users
        { duration: "10s", target: 20 },  // Quickly scale down to 20 users
        { duration: "10s", target: 0 },   // Finish with 0 users
    ],
    thresholds: {
        "http_req_duration": ["p(95) < 300"], // 95% of requests must be below 300ms
    },
};

export default function () {
    let res = http.get("https://yourwebsite.com/");
    check(res, {
        "is status 200": (r) => r.status === 200,
    });
    sleep(1);
}
