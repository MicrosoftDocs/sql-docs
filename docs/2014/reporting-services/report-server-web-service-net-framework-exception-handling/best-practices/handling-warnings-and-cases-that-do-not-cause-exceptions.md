---
title: "Handling Warnings and Cases That Do Not Cause Exceptions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "exceptions [Reporting Services], warnings that don't cause"
  - "warnings [Reporting Services]"
ms.assetid: 475c0713-6265-44e7-9ebc-ebdd1b89e0af
author: markingmyname
ms.author: maghan
manager: kfile
---
# Handling Warnings and Cases That Do Not Cause Exceptions
  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] does not throw exceptions for warnings and certain errors. For example, when you use the <xref:ReportService2010.ReportingService2010.CreateCatalogItem%2A> method to publish a new report to a report server, any warnings that occur are returned as an array of <xref:ReportService2010.Warning> objects. These warnings should be handled and displayed so that appropriate action can be taken.  
  
```vb  
Try  
   rs.CreateCatalogItem(name, parentFolder, False, definition, Nothing, warnings)  
  
   If Not (warnings.Length = 0) Then  
      Dim warning As Warning  
      For Each warning In warnings  
         Console.WriteLine(warning.Message)  
      Next warning  
   Else  
      Console.WriteLine("Report {0} created successfully with no warnings", name)  
   End If  
  
Catch ex As SoapException  
   Console.WriteLine(ex.Detail("Message").InnerXml)  
End Try  
```  
  
```csharp  
try  
{  
   rs.CreateCatalogItem("Report", name, parentFolder, false, definition, null, out warnings);  
  
   if (warnings.Length != 0)  
   {  
      foreach (Warning warning in warnings)  
      {  
         Console.WriteLine(warning.Message);  
      }  
   }  
   else  
      Console.WriteLine("Report {0} created successfully with no warnings", name);  
}  
  
catch (SoapException ex)  
{  
   Console.WriteLine(ex.Detail["Message"].InnerXml);  
}  
```  
  
 Another way to handle errors is to evaluate the return values of certain methods. For example, the <xref:ReportService2010.ReportingService2010.FindItems%2A> method can be used to search for specific items in the report server database. If no items are found that match the search criteria, a null array of <xref:ReportService2010.CatalogItem> objects is returned. You should evaluate the array, check for `null`, and let the user know if no items were found.  
  
## See Also  
 <xref:ReportService2010.CatalogItem>   
 [Introducing Exception Handling in Reporting Services](../introducing-exception-handling-in-reporting-services.md)   
 [Reporting Services SoapException Class](../soapexception-class/reporting-services-soapexception-class.md)  
  
  
