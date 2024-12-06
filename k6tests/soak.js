import { check, sleep } from "k6";
import http from "k6/http";
//Endurance testing helps determine if your system can handle a prolonged period of a certain load.

export let options = {
    stages: [
        { duration: "2h", target: 50 }, // Maintain 50 users for 2 hours
    ],
    thresholds: {
        "http_req_duration": ["p(95) < 200"], // 95% of requests must be below 200ms
    },
};

export default function () {
    let res = http.get("hhttps://bpcalulator-green-slot-dscydwgqh3eyfxf4.ukwest-01.azurewebsites.net/");
    check(res, {
        "is status 200": (r) => r.status === 200,
    });
    sleep(1);
}
