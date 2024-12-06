import { check, sleep } from "k6";
import http from "k6/http";
//Stress testing helps to determine how your application performs when it is pushed beyond normal operational capacity.

export let options = {
    stages: [
        { duration: "2m", target: 50 },  // Ramp up to 50 users over 2 minutes
        { duration: "3m", target: 100 }, // Ramp up to 100 users over 3 minutes
        { duration: "2m", target: 200 }, // Ramp up to 200 users over 2 minutes
        { duration: "3m", target: 0 },   // Ramp down to 0 users over 3 minutes
    ],
    thresholds: {
        "http_req_duration": ["p(99) < 500"], // 99% of requests must be below 500ms
    },
};

export default function () {
    let res = http.get("https://bpcalulator-green-slot-dscydwgqh3eyfxf4.ukwest-01.azurewebsites.net/");
    check(res, {
        "is status 200": (r) => r.status === 200,
    });
    sleep(1);
}
