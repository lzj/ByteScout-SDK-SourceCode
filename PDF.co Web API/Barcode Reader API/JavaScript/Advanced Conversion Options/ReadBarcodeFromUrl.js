//*******************************************************************************************//
//                                                                                           //
// Download Free Evaluation Version From: https://bytescout.com/download/web-installer       //
//                                                                                           //
// Also available as Web API! Get Your Free API Key: https://app.pdf.co/signup               //
//                                                                                           //
// Copyright © 2017-2020 ByteScout, Inc. All rights reserved.                                //
// https://www.bytescout.com                                                                 //
// https://pdf.co                                                                            //
//                                                                                           //
//*******************************************************************************************//


var https = require("https");

// Use "npm install request" command to install.
var request = require("request");

// The authentication key (API Key).
// Get your own by registering at https://app.pdf.co/documentation/api
const API_KEY = "***********************************";

// Direct URL of source file to search barcodes in.
const SourceFileUrl = "https://bytescout-com.s3.amazonaws.com/files/demo-files/cloud-api/barcode-reader/sample.pdf";
// Comma-separated list of barcode types to search. 
// See valid barcode types in the documentation https://app.pdf.co/documentation/api/1.0/barcode/read_from_url.html
const BarcodeTypes = "Code128,Code39,Interleaved2of5,EAN13";
// Comma-separated list of page indices (or ranges) to process. Leave empty for all pages. Example: '0,2-5,7-'.
const Pages = "";

/*
Some of advanced options available through profiles:
(JSON can be single/double-quoted and contain comments.)
{
    "profiles": [
        {
            "profile1": {
                "ScanArea": "WholePage", // Values: "TopLeftQuarter", "TopRightQuarter", "BottomRightQuarter", "BottomLeftQuarter", "TopHalf", "BottomHalf", "WholePage".
                "RequireQuietZones": true, // Whether the quite zone is obligatory for 1D barcodes. Values: true / false
                "MaxNumberOfBarcodesPerPage": 0, // 0 - unlimited.
                "MaxNumberOfBarcodesPerDocument": 0, // 0 - unlimited.
                "ScanStep": 1, // Scan interval for linear (1-dimensional) barcodes.
                "MinimalDataLength": 0, // Minimal acceptable length of decoded data.                
            }
        }
    ]
}
*/
// Sample profile that sets advanced conversion options
//  Advanced options are properties of Reader class from Bytescout BarCodeReader used in the back-end:
// https://cdn.bytescout.com/help/BytescoutBarCodeReaderSDK/html/ba101d21-3db7-eb54-d112-39cadc023d02.htm
const Profiles = '{ "profiles": [ { "profile1": { "FastMode": true } } ] }';

// Prepare request to `Barcode Reader` API endpoint
var queryPath = `/v1/barcode/read/from/url?types=${BarcodeTypes}&pages=${Pages}&url=${SourceFileUrl}&profiles=${Profiles}&async=True`;
var reqOptions = {
    host: "api.pdf.co",
    path: encodeURI(queryPath),
    headers: {
        "x-api-key": API_KEY
    }
};
// Send request
https.get(reqOptions, (response) => {
    response.on("data", (d) => {
        // Parse JSON response
        var data = JSON.parse(d);

        if (data.error == false) {
            console.log(`Job #${data.jobId} has been created!`);
            checkIfJobIsCompleted(data.jobId, data.url);
        }
        else {
            // Service reported error
            console.log(data.message);
        }
    });
}).on("error", (e) => {
    // Request error
    console.error(e);
});

function checkIfJobIsCompleted(jobId, resultFileUrlJson) {
    let queryPath = `/v1/job/check?jobid=${jobId}`;
    let reqOptions = {
        host: "api.pdf.co",
        path: encodeURI(queryPath),
        method: "GET",
        headers: { "x-api-key": API_KEY }
    };

    https.get(reqOptions, (response) => {
        response.on("data", (d) => {
            response.setEncoding("utf8");

            // Parse JSON response
            let data = JSON.parse(d);
            console.log(`Checking Job #${jobId}, Status: ${data.status}, Time: ${new Date().toLocaleString()}`);

            if (data.status == "working") {
                // Check again after 3 seconds
                setTimeout(function () { checkIfJobIsCompleted(jobId, resultFileUrlJson); }, 3000);
            }
            else if (data.status == "success") {

                request({ method: 'GET', uri: resultFileUrlJson, gzip: true },
                    function (error, response, body) {

                        // Parse JSON response
                        let respJsonFileArray = JSON.parse(body);

                        respJsonFileArray.forEach((element) => {
                            console.log("Found barcode:");
                            console.log("  Type: " + element["TypeName"]);
                            console.log("  Value: " + element["Value"]);
                            console.log("  Document Page Index: " + element["Page"]);
                            console.log("  Rectangle: " + element["Rect"]);
                            console.log("  Confidence: " + element["Confidence"]);
                            console.log();
                        }, this);
                    });
            }
            else {
                console.log(`Operation ended with status: "${data.status}".`);
            }
        })
    });
}
