## How to merge PDF documents from uploaded file (node for PDF merging API in JavaScript using PDF.co Web API

### Step By Step Tutorial: how to merge PDF documents from uploaded file (node for PDF merging API in JavaScript

On this page you will learn from code samples for programming in JavaScript. PDF.co Web API was made to help with PDF merging API in JavaScript. PDF.co Web API is the flexible Web API that includes full set of functions from e-signature requests to data extraction, OCR, images recognition, pdf splitting and pdf splitting. Can also generate barcodes and read barcodes from images, scans and pdf.

Fast application programming interfaces of PDF.co Web API for JavaScript plus the instruction and the code below will help to learn how to merge PDF documents from uploaded file (node. This JavaScript sample code should be copied and pasted into your project. After doing this just compile your project and click Run. Enjoy writing a code with ready-to-use sample JavaScript codes to implement PDF merging API using PDF.co Web API.

Trial version of ByteScout is available for free download from our website. This and other source code samples for JavaScript and other programming languages are available.

## REQUEST FREE TECH SUPPORT

[Click here to get in touch](https://bytescout.zendesk.com/hc/en-us/requests/new?subject=PDF.co%20Web%20API%20Question)

or just send email to [support@bytescout.com](mailto:support@bytescout.com?subject=PDF.co%20Web%20API%20Question) 

## ON-PREMISE OFFLINE SDK 

[Get Your 60 Day Free Trial](https://bytescout.com/download/web-installer?utm_source=github-readme)
[Explore SDK Docs](https://bytescout.com/documentation/index.html?utm_source=github-readme)
[Sign Up For Online Training](https://academy.bytescout.com/)


## ON-DEMAND REST WEB API

[Get your API key](https://pdf.co/documentation/api?utm_source=github-readme)
[Explore Web API Documentation](https://pdf.co/documentation/api?utm_source=github-readme)
[Explore Web API Samples](https://github.com/bytescout/ByteScout-SDK-SourceCode/tree/master/PDF.co%20Web%20API)

## VIDEO REVIEW

[https://www.youtube.com/watch?v=NEwNs2b9YN8](https://www.youtube.com/watch?v=NEwNs2b9YN8)




<!-- code block begin -->

##### ****MergePDFDocumentsFromUploadedFile.js:**
    
```
/*jshint esversion: 6 */

var https = require("https");
var path = require("path");
var fs = require("fs");

// `request` module is required for file upload.
// Use "npm install request" command to install.
var request = require("request");

// The authentication key (API Key).
// Get your own by registering at https://app.pdf.co/documentation/api
const API_KEY = "***********************************";


// Source PDF file
const SourceFile1 = "./sample1.pdf";
const SourceFile2 = "./sample2.pdf";

// Destination PDF file name
const DestinationFile = "./result.pdf";


// Upload File-1: - 1. RETRIEVE PRESIGNED URL TO UPLOAD FILE.
getPresignedUrl(SourceFile1)
    .then(([uploadUrl1, uploadedFileUrl1]) => {

        // Upload File-1: - 2. UPLOAD THE FILE TO CLOUD.
        uploadFile(SourceFile1, uploadUrl1)
            .then(() => {

                // Upload File-2: - 1. RETRIEVE PRESIGNED URL TO UPLOAD FILE.
                getPresignedUrl(SourceFile2)
                    .then(([uploadUrl2, uploadedFileUrl2]) => {

                        // Upload File-2: - 2. UPLOAD THE FILE TO CLOUD.
                        uploadFile(SourceFile2, uploadUrl2)
                            .then(() => {

                                const SourceFiles = [
                                    uploadedFileUrl1,
                                    uploadedFileUrl2
                                ];

                                // Perform Merge PDF Documents
                                mergePDFDocuments(SourceFiles, DestinationFile);
                            })
                            .catch(e => {
                                console.log(e);
                            });
                    })
                    .catch(e => {
                        console.log(e);
                    });
            })
            .catch(e => {
                console.log(e);
            });
    })
    .catch(e => {
        console.log(e);
    });


function getPresignedUrl(localFile) {
    return new Promise(resolve => {
        // Prepare request to `Get Presigned URL` API endpoint
        let queryPath = `/v1/file/upload/get-presigned-url?contenttype=application/octet-stream&name=${path.basename(localFile)}`;
        let reqOptions = {
            host: "api.pdf.co",
            path: encodeURI(queryPath),
            headers: { "x-api-key": API_KEY }
        };
        // Send request
        https.get(reqOptions, (response) => {
            response.on("data", (d) => {
                let data = JSON.parse(d);
                if (data.error == false) {
                    // Return presigned url we received
                    resolve([data.presignedUrl, data.url]);
                }
                else {
                    // Service reported error
                    console.log("getPresignedUrl(): " + data.message);
                }
            });
        })
            .on("error", (e) => {
                // Request error
                console.log("getPresignedUrl(): " + e);
            });
    });
}

function uploadFile(sourceFile, uploadUrl) {
    return new Promise(resolve => {
        fs.readFile(sourceFile, (err, data) => {
            request({
                method: "PUT",
                url: uploadUrl,
                body: data,
                headers: {
                    "Content-Type": "application/octet-stream"
                }
            }, (err, res, body) => {
                if (!err) {
                    resolve();
                }
                else {
                    console.log("uploadFile() request error: " + e);
                }
            });
        });
    });
}

function mergePDFDocuments(SourceFiles, DestinationFile) {
    // Prepare request to `Merge PDF` API endpoint
    var queryPath = `/v1/pdf/merge?name=${path.basename(DestinationFile)}&url=${SourceFiles.join(",")}`;
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
                // Download PDF file
                var file = fs.createWriteStream(DestinationFile);
                https.get(data.url, (response2) => {
                    response2.pipe(file)
                        .on("close", () => {
                            console.log(`Generated PDF file saved as "${DestinationFile}" file.`);
                        });
                });
            }
            else {
                // Service reported error
                console.log(data.message);
            }
        });
    }).on("error", (e) => {
        // Request error
        console.log(e);
    });
}
```

<!-- code block end -->