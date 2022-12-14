---
title: "Using Try and Catch Blocks | Microsoft Docs"
description: Learn how to provide another layer of protection against requests that are not valid by applying adequate exception handling through the use of try/catch blocks.
ms.date: 03/06/2017
ms.prod: reporting-services
ms.technology: report-server-web-service-net-framework-exception-handling


ms.topic: reference
helpviewer_keywords: 
  - "exceptions [Reporting Services], try/catch blocks"
  - "try/catch blocks [Reporting Services]"
ms.assetid: a7a9ef53-e3b6-4bf7-81f3-d85615954e6f
author: maggiesMSFT
ms.author: maggies
---
# Using Try and Catch Blocks
  After you limit invalid requests to the report server by adding conditional statements to your code, you should supply adequate exception handling through the use of try/catch blocks. This technique provides another layer of protection against requests that are not valid. If a request to the report server is encased in a try block and that request causes the report server to throw an exception, the exception is caught in the catch block, thus preventing your application from ending unexpectedly. Once the exception is caught, you can use the exception to either instruct the user to do something differently, or just let the user know, in a friendly way, that an error has occurred. You can then use a finally block to clean up any resources. Ideally, you should generate a general exception-handling plan to avoid unnecessary duplication of try/catch blocks.  
  
 The following example uses try/catch blocks to enhance the reliability of the exception handling code.  
  
```  
// C#  
private void PublishReport()  
{  
   int index;  
   string reservedChar;  
   string message;  
  
   // Check the text value of the name text box for "/",  
   // a reserved character  
   index = nameTextBox.Text.IndexOf(@"/");  
  
   if ( index != -1) // The text contains the character  
   {  
      reservedChar = nameTextBox.Text.Substring(index, 1);  
      // Build a user-friendly error message  
      message = "The name of the report cannot contain the reserved character " +  
         "\"" + reservedChar + "\". " +  
         "Please enter a valid name for the report. " +  
         "For more information about reserved characters, " +  
         "consult the online documentation";  
  
      MessageBox.Show(message, "Invalid Input Error");  
   }  
   else // Publish the report  
   {  
      Byte[] definition = null;  
      Warning[] warnings = {};  
      string name = nameTextBox.Text;  
  
      try  
      {  
         FileStream stream = File.OpenRead("MyReport.rdl");  
         definition = new Byte[stream.Length];  
         stream.Read(definition, 0, (int) stream.Length);  
         stream.Close();  
         // Create report with user-defined name  
         rs.CreateCatalogItem("Report", name, "/Samples", false, definition, null, out warnings);  
         MessageBox.Show("Report: {0} created successfully", name);  
      }  
  
      // Catch expected exceptions beginning with the most specific,  
      // moving to the least specific  
      catch(IOException ex)  
      {  
         MessageBox.Show(ex.Message, "File IO Exception");  
      }  
  
      catch (SoapException ex)  
      {  
         // The exception is a SOAP exception, so use  
         // the Detail property's Message element.  
         MessageBox.Show(ex.Detail["Message"].InnerXml, "SOAP Exception");   
      }  
  
      catch (Exception ex)  
      {  
         MessageBox.Show(ex.Message, "General Exception");  
      }  
   }  
}  
```  
  
## See Also  
 [Introducing Exception Handling in Reporting Services](../../../reporting-services/report-server-web-service-net-framework-exception-handling/introducing-exception-handling-in-reporting-services.md)   
 [Reporting Services SoapException Class](../../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/reporting-services-soapexception-class.md)  
  
  
