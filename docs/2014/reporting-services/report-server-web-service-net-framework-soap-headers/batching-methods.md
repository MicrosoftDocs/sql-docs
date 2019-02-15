---
title: "Batching Methods | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "methods [Reporting Services], batches"
  - "BatchHeader SOAP header"
  - "Web service [Reporting Services], methods"
  - "Report Server Web service, batching"
  - "batches [Reporting Services]"
  - "XML Web service [Reporting Services], methods"
  - "locking [Reporting Services]"
  - "multiple Web service methods"
ms.assetid: 86435534-c9fe-4b49-b88c-7fb6d21976b0
author: markingmyname
ms.author: maghan
manager: kfile
---
# Batching Methods
  The use of SOAP headers in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] enables you to include multiple Web service methods in a single operation. Methods run within the scope of a single database transaction, in the order in which they are called.  
  
 Rollback is one advantage of using multiple-method batch operations. If an error occurs on any of the method calls while a batch is running, the report server stops running the batch and rolls back any previous operations. This is useful when a method call depends on the successful completion of other method calls in that batch.  
  
 The Web service does not provide locking semantics for multiple-method batch operations. Rows in the report server database are not locked for updating until the message is sent to the server and the Execute command is called.  
  
 There are also no concurrency controls to guarantee that the database has not changed since the data was last read. If two clients modify the same item, the last update succeeds if the parameters are still valid (for example, the item has not been renamed).  
  
 The following example calls the <xref:ReportService2005.ReportingService2005.CreateFolder%2A> method three times and runs these calls as a single batch. If any of the calls to <xref:ReportService2005.ReportingService2005.CreateFolder%2A> fail, the entire batch is canceled.  
  
```vb  
Imports System  
Imports System.Web.Services.Protocols  
Imports myNamespace.MyReferenceName  
  
Class Sample  
    Sub Main(args() As String)  
        Dim rs As New ReportingService2005()  
        rs.Credentials = System.Net.CredentialCache.DefaultCredentials  
      ' Set the base Web service URL of the source server  
      rs.Url = "http://<Server Name>/reportserver/ReportService2005.asmx"  
  
        Dim bh As New BatchHeader()  
  
        bh.BatchId = service.CreateBatch()  
        rs.BatchHeaderValue = bh  
        rs.CreateFolder("New Folder1", "/", Nothing)  
        rs.CreateFolder("New Folder2", "/", Nothing)  
        rs.CreateFolder("New Folder3", "/", Nothing)  
  
        Console.WriteLine("Creating folders...")  
        rs.BatchHeaderValue = bh  
        rs.ExecuteBatch()  
        Console.WriteLine("Folders created successfully.")  
  
        rs.BatchHeaderValue = Nothing  
    End Sub  
End Class  
```  
  
```csharp  
using System;  
using System.Web.Services.Protocols;   
using myNamespace.MyReferenceName;  
  
class Sample  
{  
    static void Main(string[] args)  
    {  
        ReportingService2005 rs = new ReportingService2005();  
        rs.Credentials = System.Net.CredentialCache.DefaultCredentials;  
      // Set the base Web service URL of the source server  
      rs.Url = "http://<Server Name>/reportserver/ReportService2005.asmx"  
  
        BatchHeader bh = new BatchHeader();  
  
        bh1.BatchID = service.CreateBatch();  
        rs.BatchHeaderValue = bh;  
        rs.CreateFolder("New Folder1", "/", null);  
        rs.CreateFolder("New Folder2", "/", null);  
        rs.CreateFolder("New Folder3", "/", null);  
  
        Console.WriteLine("Creating folders...");  
        rs.BatchHeaderValue = bh1;  
        rs.ExecuteBatch();  
        Console.WriteLine("Folders created successfully.");  
  
        rs.BatchHeaderValue = null;  
    }  
}  
```  
  
## See Also  
 <xref:ReportService2005.ReportingService2005.CancelBatch%2A>   
 <xref:ReportService2005.ReportingService2005.CreateBatch%2A>   
 [Technical Reference &#40;SSRS&#41;](../technical-reference-ssrs.md)   
 [Using Reporting Services SOAP Headers](using-reporting-services-soap-headers.md)  
  
  
