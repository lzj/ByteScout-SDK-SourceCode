'****************************************************************************'
'                                                                            '
' Download evaluation version: https://bytescout.com/download/web-installer  '
'                                                                            '
' Signup Cloud API free trial: https://secure.bytescout.com/users/sign_up    '
'                                                                            '
' Copyright © 2017 ByteScout Inc. All rights reserved.                       '
' http://www.bytescout.com                                                   '
'                                                                            '
'****************************************************************************'


Imports Bytescout.TextRecognition

Module Module1

    Sub Main()

        Dim inputDocument As String = ".\invoice-sample.png"
        Dim outputDocument As String = ".\result.txt"

        ' Create and activate TextRecognizer instance
        Using textRecognizer As TextRecognizer = New TextRecognizer("demo", "demo")

            Try
                ' Load document (image or PDF)
                textRecognizer.LoadDocument(inputDocument)

                ' Set location of "tessdata" folder containing language data files
                textRecognizer.OCRLanguageDataFolder = "c:\Program Files\ByteScout Text Recognition SDK\net4.00\tessdata\"

                ' Set OCR language
                textRecognizer.OCRLanguage = "eng" ' "eng" for english, "deu" for German, "fra" for French, "spa" for Spanish etc - according to files in /tessdata
                ' Find more language files at https://github.com/tesseract-ocr/tessdata/tree/3.04.00

                ' Recognize text from all pages and save it to file
                textRecognizer.SaveText(outputDocument)

                ' Open the result file in default associated application (for demo purposes)
                Process.Start(outputDocument)

            Catch exception As Exception

                Console.WriteLine(exception)

            End Try

        End Using

    End Sub

End Module