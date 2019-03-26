---
title: "Format a Reporting Services Script File | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "scripts [Reporting Services], formats"
  - "formats [Reporting Services], script files"
ms.assetid: 85a207dd-4e0f-4d40-a41e-0c75f65d719c
author: markingmyname
ms.author: maghan
manager: kfile
---
# Format a Reporting Services Script File
  A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] script is a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic .NET code file, written against a proxy that is built on Web Service Description Language (WSDL), which defines the Reporting Services SOAP API. A script file is stored as a Unicode or UTF-8 text file with the extension .rss.  
  
 The script file acts as a [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] module and can contain user-defined procedures and module-level variables. For the script file to run successfully, it must contain a Main procedure. The Main procedure is the first procedure that is accessed when your script file runs. Main is where you can add your Web service operations and run your user defined subprocedures. The following code creates a Main procedure:  
  
```  
Public Sub Main()  
    ' Your code goes here.  
End Sub  
```  
  
 The script environment automatically connects to the report server, creates the Web proxy class, and generates a reference variable (*rs*) to the Web service proxy object. Individual statements that you create need only refer to the *rs* module-level variable to perform any of the Web service operations that are available in the Web service library. The following [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] code calls the Web service <xref:ReportService2010.ReportingService2010.ListChildren%2A> method from within a script file:  
  
```  
Public Sub Main()  
    Dim items() As CatalogItem  
    items = rs.ListChildren("/", True)  
  
    Dim item As CatalogItem  
    For Each item In items  
        Console.WriteLine(item.Name)  
    Next item  
End Sub   
```  
  
> [!IMPORTANT]  
>  User credentials are managed by the script environment and passed through command prompt arguments through the use of RS.exe. Although you can use the *rs* variable to set the authentication of the Web service, it is recommended that you use the script environment. You do not need to authenticate the Web service in the script file itself. For more information about authenticating the script environment, see [RS.exe Utility &#40;SSRS&#41;](rs-exe-utility-ssrs.md).  
  
 You do not declare namespaces within the script file. The scripting environment makes several useful [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] namespaces available to you: **System.Web.Services**, **System.Web.Services.Protocols**, **System.Xml**, and **System.IO**.  
  
 For script samples, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## See Also  
 [Report Server Web Service](../report-server-web-service/report-server-web-service.md)   
 [Technical Reference &#40;SSRS&#41;](../technical-reference-ssrs.md)   
 [RS.exe Utility &#40;SSRS&#41;](rs-exe-utility-ssrs.md)  
  
  
