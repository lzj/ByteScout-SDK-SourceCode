## How to generate barcode (node for barcode generator API in JavaScript using PDF.co Web API

### How to generate barcode (node for barcode generator API in JavaScript: How To Tutorial

Today you are going to learn how to generate barcode (node in JavaScript. PDF.co Web API was made to help with barcode generator API in JavaScript. PDF.co Web API is the Rest API that provides set of data extraction functions, tools for documents manipulation, splitting and merging of pdf files. Includes built-in OCR, images recognition, can generate and read barcodes from images, scans and pdf.

JavaScript code snippet like this for PDF.co Web API works best when you need to quickly implement barcode generator API in your JavaScript application. Sample code in JavaScript is all you need. Copy-paste it to your the code editor, then add a reference to PDF.co Web API and you are ready to try it! Enjoy writing a code with ready-to-use sample JavaScript codes to add barcode generator API functions using PDF.co Web API in JavaScript.

PDF.co Web API - free trial version is on available our website. Also, there are other code samples to help you with your JavaScript application included into trial version.

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

##### ****GenerateBarcode.js:**
    
```
var https = require("https");
var path = require("path");
var fs = require("fs");


// The authentication key (API Key).
// Get your own by registering at https://app.pdf.co/documentation/api
const API_KEY = "***********************************";


// Result image file name
const DestinationFile = "./barcode.png";
// Barcode type. See valid barcode types in the documentation https://app.pdf.co/documentation/api/1.0/barcode/generate.html
const BarcodeType = "Code128";
// Barcode value
const BarcodeValue = "qweasd123456";


// Prepare request to `Barcode Generator` API endpoint
var queryPath = `/v1/barcode/generate?name=${path.basename(DestinationFile)}&type=${BarcodeType}&value=${BarcodeValue}`;
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
            // Download the image file
            var file = fs.createWriteStream(DestinationFile);
            https.get(data.url, (response2) => {
                response2.pipe(file).on("close", () => {
                    console.log(`Generated barcode saved to '${DestinationFile}' file.`);
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
    console.error(e);
});

```

<!-- code block end -->