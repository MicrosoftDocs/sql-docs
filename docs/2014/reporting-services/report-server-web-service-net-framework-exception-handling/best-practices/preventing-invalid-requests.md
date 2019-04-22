---
title: "Preventing Invalid Requests | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "invalid requests [Reporting Services]"
  - "exceptions [Reporting Services], invalid requests"
  - "valid requests [Reporting Services]"
ms.assetid: 4a4a2d97-4c10-43a9-8298-ef5a820ea549
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Preventing Invalid Requests
  You can prevent some types of exceptions from being thrown by analyzing your application flow and ensuring that the requests being sent to the report server are valid. For example, in applications that enable users to add or update the name of a report, data source, or other report server item, you should validate the text that a user might enter. You should always check for reserved characters before sending the request to a report server. Use conditional **if** statements or other logical constructs in your code to alert the user that they have not met the conditions necessary to send requests to the report server.  
  
 In the following, simplified C# example, users are presented with a friendly error message when they attempt to create a report with a name that contains a forward slash (/) character.  
  
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
         "see the help documentation";  
  
      MessageBox.Show(message, "Invalid Input Error");  
   }  
   else // Publish the report  
   {  
      Byte[] definition = null;  
      Warning[] warnings = {};  
      string name = nameTextBox.Text;  
  
      FileStream stream = File.OpenRead("MyReport.rdl");  
      definition = new Byte[stream.Length];  
      stream.Read(definition, 0, (int) stream.Length);  
      stream.Close();  
      // Create report with user-defined name  
      rs.CreateCatalogItem("Report", name, "/Samples", false, definition, null, out warnings);  
      MessageBox.Show("Report: {0} created successfully", name);  
   }  
}  
```  
  
 For more information about the types of errors that can be prevented before requests are sent to the report server, see [SoapException Errors Table](../soapexception-class/soapexception-errors-table.md). For more information about further enhancing the previous example using try/catch blocks, see [Using Try and Catch Blocks](using-try-and-catch-blocks.md).  
  
## See Also  
 [Introducing Exception Handling in Reporting Services](../introducing-exception-handling-in-reporting-services.md)   
 [Reporting Services SoapException Class](../soapexception-class/reporting-services-soapexception-class.md)  
  
  
