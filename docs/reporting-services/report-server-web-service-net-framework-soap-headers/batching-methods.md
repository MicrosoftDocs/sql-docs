---
title: "Batch methods"
description: Learn how to use SOAP headers in Reporting Services to include multiple Web service methods in a single operation.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server-web-service
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "methods [Reporting Services], batches"
  - "BatchHeader SOAP header"
  - "Web service [Reporting Services], methods"
  - "Report Server Web service, batching"
  - "batches [Reporting Services]"
  - "XML Web service [Reporting Services], methods"
  - "locking [Reporting Services]"
  - "multiple Web service methods"
---
# Batch methods
  The use of SOAP headers in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] enables you to include multiple Web service methods in a single operation. Methods run within the scope of a single database transaction, in the order in which they're called.  
  
 Rollback is one advantage of using multiple-method batch operations. If an error occurs on any method calls while a batch is running, the report server stops running the batch and rolls back any previous operations. This action is useful when a method call depends on the successful completion of other method calls in that batch.  
  
 The Web service doesn't provide locking semantics for multiple-method batch operations. Rows in the report server database aren't locked for updating until the message is sent to the server and the `Execute` command is called.  
  
 No concurrency controls exist to guarantee that no one changed the database since the data was last read. If two clients modify the same item, the last update succeeds if the parameters are still valid (for example, the item's name is the same).  
  
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
      rs.Url = "https://<Server Name>/reportserver/ReportService2005.asmx"  
  
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
      rs.Url = "https://<Server Name>/reportserver/ReportService2005.asmx"  
  
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
  
## Related content

- [Technical reference &#40;SSRS&#41;](../../reporting-services/technical-reference-ssrs.md)
- [Use Reporting Services SOAP headers](../../reporting-services/report-server-web-service-net-framework-soap-headers/using-reporting-services-soap-headers.md)
