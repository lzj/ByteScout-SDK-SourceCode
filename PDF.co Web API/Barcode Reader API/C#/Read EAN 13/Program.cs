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


using System;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace ByteScoutWebApiExample
{
    class Program
    {
        // The authentication key (API Key).
        // Get your own by registering at https://app.pdf.co/documentation/api
        const String API_KEY = "***********************************";

        // Source file name
        const string SourceFile = @".\EAN13.png";
        // Comma-separated list of barcode types to search. 
        // See valid barcode types in the documentation https://app.pdf.co/documentation/api/1.0/barcode/read_from_url.html
        const string BarcodeTypes = "EAN13";
        // Comma-separated list of page indices (or ranges) to process. Leave empty for all pages. Example: '0,2-5,7-'.
        const string Pages = "";
        // (!) Make asynchronous job
        const bool Async = true;

        static void Main(string[] args)
        {
            // Create standard .NET web client instance
            WebClient webClient = new WebClient();

            // Set API Key
            webClient.Headers.Add("x-api-key", API_KEY);

            // 1. RETRIEVE THE PRESIGNED URL TO UPLOAD THE FILE.
            // * If you already have a direct file URL, skip to the step 3.

            // Prepare URL for `Get Presigned URL` API call
            string query = Uri.EscapeUriString(string.Format(
                "https://api.pdf.co/v1/file/upload/get-presigned-url?contenttype=application/octet-stream&name={0}",
                Path.GetFileName(SourceFile)));

            try
            {
                // Execute request
                string response = webClient.DownloadString(query);

                // Parse JSON response
                JObject json = JObject.Parse(response);

                if (json["error"].ToObject<bool>() == false)
                {
                    // Get URL to use for the file upload
                    string uploadUrl = json["presignedUrl"].ToString();
                    // Get URL of uploaded file to use with later API calls
                    string uploadedFileUrl = json["url"].ToString();

                    // 2. UPLOAD THE FILE TO CLOUD.

                    webClient.Headers.Add("content-type", "application/octet-stream");
                    webClient.UploadFile(uploadUrl, "PUT", SourceFile); // You can use UploadData() instead if your file is byte[] or Stream

                    // 3. READ BARCODES FROM UPLOADED FILE

                    // Prepare URL for `Barcode Reader` API call
                    query = Uri.EscapeUriString(string.Format("https://api.pdf.co/v1/barcode/read/from/url?types={0}&pages={1}&url={2}&async={3}",
                        BarcodeTypes,
                        Pages,
                        uploadedFileUrl,
                        Async));


                    try
                    {
                        // Execute request
                        response = webClient.DownloadString(query);

                        // Parse JSON response
                        json = JObject.Parse(response);

                        if (json["error"].ToObject<bool>() == false)
                        {
                            // Asynchronous job ID
                            string jobId = json["jobId"].ToString();
                            // URL of generated JSON file with decoded barcodes that will available after the job completion
                            string resultFileUrl = json["url"].ToString();

                            // Check the job status in a loop. 
                            // If you don't want to pause the main thread you can rework the code 
                            // to use a separate thread for the status checking and completion.
                            do
                            {
                                string status = CheckJobStatus(jobId); // Possible statuses: "working", "failed", "aborted", "success".

                                // Display timestamp and status (for demo purposes)
                                Console.WriteLine(DateTime.Now.ToLongTimeString() + ": " + status);

                                if (status == "success")
                                {
                                    // Download JSON results file as string
                                    string jsonFileString = webClient.DownloadString(resultFileUrl);

                                    JArray jsonFoundBarcodes = JArray.Parse(jsonFileString);

                                    // Display found barcodes in console
                                    foreach (JToken token in jsonFoundBarcodes)
                                    {
                                        Console.WriteLine("Found barcode:");
                                        Console.WriteLine("  Type: " + token["TypeName"]);
                                        Console.WriteLine("  Value: " + token["Value"]);
                                        Console.WriteLine("  Document Page Index: " + token["Page"]);
                                        Console.WriteLine("  Rectangle: " + token["Rect"]);
                                        Console.WriteLine("  Confidence: " + token["Confidence"]);
                                        Console.WriteLine();
                                    }
                                    break;
                                }
                                else if (status == "working")
                                {
                                    // Pause for a few seconds
                                    Thread.Sleep(3000);
                                }
                                else
                                {
                                    Console.WriteLine(status);
                                    break;
                                }
                            }
                            while (true);
                        }
                        else
                        {
                            // Display service reported error
                            Console.WriteLine(json["message"].ToString());
                        }
                    }
                    catch (WebException e)
                    {
                        // Display request error
                        Console.WriteLine(e.ToString());
                    }

                }
                else
                {
                    // Display service reported error
                    Console.WriteLine(json["message"].ToString());
                }
            }
            catch (WebException e)
            {
                // Display request error
                Console.WriteLine(e.ToString());
            }

            webClient.Dispose();

            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }


        /// <summary>
        /// Check Job Status
        /// </summary>
        static string CheckJobStatus(string jobId)
        {
            using (WebClient webClient = new WebClient())
            {
                // Set API Key
                webClient.Headers.Add("x-api-key", API_KEY);

                string url = "https://api.pdf.co/v1/job/check?jobid=" + jobId;

                string response = webClient.DownloadString(url);
                JObject json = JObject.Parse(response);

                return Convert.ToString(json["status"]);
            }
        }

    }
}
