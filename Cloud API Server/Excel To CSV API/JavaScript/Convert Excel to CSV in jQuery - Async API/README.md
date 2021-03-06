## How to convert excel to CSV in jquery async API for excel to CSV API in JavaScript and ByteScout Cloud API Server

### How to convert excel to CSV in jquery async API for excel to CSV API in JavaScript: Step By Step Instructions

We regularly create and update our sample code library so you may quickly learn excel to CSV API and the step-by-step process in JavaScript. Excel to CSV API in JavaScript can be applied with ByteScout Cloud API Server. ByteScout Cloud API Server is the ready to deploy Web API Server that can be deployed in less than thirty minutes into your own in-house Windows server (no Internet connnection is required to process data!) or into private cloud server. Can store data on in-house local server based storage or in Amazon AWS S3 bucket. Processing data solely on the server using built-in ByteScout powered engine, no cloud services are used to process your data!.

The SDK samples displayed below below explain how to quickly make your application do excel to CSV API in JavaScript with the help of ByteScout Cloud API Server. For implementation of this functionality, please copy and paste the code below into your app using code editor. Then compile and run your app. Want to see how it works with your data then code testing will allow the function to be tested and work properly.

ByteScout Cloud API Server - free trial version is available on our website. Also, there are other code samples to help you with your JavaScript application included into trial version.

## REQUEST FREE TECH SUPPORT

[Click here to get in touch](https://bytescout.zendesk.com/hc/en-us/requests/new?subject=ByteScout%20Cloud%20API%20Server%20Question)

or just send email to [support@bytescout.com](mailto:support@bytescout.com?subject=ByteScout%20Cloud%20API%20Server%20Question) 

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

##### ****converter.js:**
    
```
// Please NOTE: In this sample we're assuming Cloud Api Server is hosted at "https://localhost". 
// If it's not then please replace this with with your hosting url.
var formData, toType, isInline;

$(document).ready(function () {
    $("#resultBlock").hide();
    $("#errorBlock").hide();
    $("#result").attr("href", '').html('');
});

$(document).on("click", "#submit", function () {
    $("#resultBlock").hide();
    $("#errorBlock").hide();
    $("#inlineOutput").text(''); // inline output div
    $("#status").text(''); // status div

    formData = $("#form input[type=file]")[0].files[0]; // file to upload
    toType = $("#convertType").val(); // output type
    isInline = $("#outputType").val() == "inline"; // if we need output as inline content or link to output file

    $("#status").html('Requesting presigned url for upload... &nbsp;&nbsp;&nbsp; <img src="ajax-loader.gif" />');

    $.ajax({
        url: 'https://localhost/file/upload/get-presigned-url?name=test.pdf&encrypt=true',
        type: 'GET',
        success: function (result) {

            if (result['error'] === false) {

                var presignedUrl = result['presignedUrl']; // reading provided presigned url to put our content into
                $("#status").html('Uploading... &nbsp;&nbsp;&nbsp; <img src="ajax-loader.gif" />');

                $.ajax({
                    url: presignedUrl, // no api key is required to upload file
                    type: 'PUT',
                    data: formData,
                    processData: false,
                    success: function (result) {
                        $("#status").html('Processing... &nbsp;&nbsp;&nbsp; <img src="ajax-loader.gif" />');

                        $.ajax({
                            url: 'https://localhost/xls/convert/to/' + toType + '?url=' + presignedUrl + '&encrypt=true&inline=' + isInline + '&async=True',
                            type: 'POST',
                            success: function (result) {
                                if (result.error) {
                                    $("#status").text('Error uploading file.');
                                }
                                else {
                                    checkIfJobIsCompleted(result.jobId, result.url);
                                }
                            }
                        });
                    },
                    error: function () {
                        $("#status").text('error');
                    }
                });
            }
        }
    });
});

function checkIfJobIsCompleted(jobId, resultFileUrl) {
    $.ajax({
        url: 'https://localhost/job/check?jobid=' + jobId,
        type: 'GET',
        success: function (jobResult) {
            $("#status").html(jobResult.status + ' &nbsp;&nbsp;&nbsp; <img src="ajax-loader.gif" />');

            if (jobResult.status == "working") {
                // Check again after 3 seconds
                setTimeout(function(){checkIfJobIsCompleted(jobId, resultFileUrl)}, 3000);
            }
            else if (jobResult.status == "success") {

                $("#status").text('Done converting.');

                $("#resultBlock").show();

                if (isInline && toType != "pdf") {
                    $.ajax({
                        url: resultFileUrl,
                        dataType: 'text',
                        success: function (respText) {
                            $("#inlineOutput").text(respText);
                        }
                    });
                }
                else {
                    $("#result").attr("href", resultFileUrl).html(resultFileUrl);
                }
            }
        }
    });
}
```

<!-- code block end -->