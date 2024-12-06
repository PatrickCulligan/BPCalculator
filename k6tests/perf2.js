import { check, sleep } from "k6";
import http from "k6/http";

// Export configuration for the K6 test, including stages and thresholds
export let options = {
    stages: [
        { duration: "1m", target: 20 }, // Ramp up to 20 virtual users over 1 minute
        { duration: "1m", target: 20 }, // Maintain 20 virtual users for 1 minute
        { duration: "1m", target: 0 }   // Ramp down to 0 virtual users over 1 minute
    ],
    thresholds: {
        "http_req_duration": ["p(95) < 200"], // Set a threshold to keep 95% of request durations under 200ms
    },
    discardResponseBodies: false,
    cloud: {
        distribution: {
            distributionLabel1: { loadZone: 'amazon:us:ashburn', percent: 50 },
            distributionLabel2: { loadZone: 'amazon:ie:dublin', percent: 50 },
        },
    },
};

/**
 * Get a random integer between `min` and `max`.
 *
 * @param {number} min - Minimum number
 * @param {number} max - Maximum number
 * @return {number} A random integer between min and max
 */
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

// Default function where the VUs will start executing the script
export default function () {
    // Initial GET request to retrieve the form page
    let res = http.get("https://bpcalulator-green-slot-dscydwgqh3eyfxf4.ukwest-01.azurewebsites.net/", { "responseType": "text" });

    check(res, {
        "is status 200": (r) => r.status === 200
    });

    // Extract anti-forgery token from the response body
    let doc = res.body;
    let tokenMatch = doc.match(/name="__RequestVerificationToken" type="hidden" value="([^"]+)"/);
    let token = tokenMatch ? tokenMatch[1] : null;

    // Ensure that the anti-forgery token was extracted correctly
    check(token, {
        "anti-forgery token extracted": (t) => t !== null
    });

    // POST the form with random systolic and diastolic values and the anti-forgery token
    let postData = {
        "BP.Systolic": getRandomInt(70, 190).toString(),
        "BP.Diastolic": getRandomInt(40, 100).toString(),
        "__RequestVerificationToken": token
    };

    res = http.post("https://bpcalulator-green-slot-dscydwgqh3eyfxf4.ukwest-01.azurewebsites.net/", postData, {
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        }
    });

    // Check that the POST request was successful
    check(res, {
        "is status 200": (r) => r.status === 200,
        "response contains Category": (r) => r.body.includes("Category")
    });

    // Think time of 3 seconds between iterations
    sleep(3);
}

// To run this script locally:
// docker pull grafana/k6
// docker run -i grafana/k6 run - < perf2.js
