---
title: "GetReportServerUrls Method (WMI MSReportServer_Instance) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "GetReportServerUrls method"
ms.assetid: 4865e32c-0114-465a-be8c-be223a7bc09e
author: markingmyname
ms.author: maghan
manager: kfile
---
# GetReportServerUrls Method (WMI MSReportServer_Instance)
  Returns a list of URLs users can use to access the report server and report manager.  
  
## Syntax  
  
```vb  
Public Sub GetReportServerUrls (ByRef ApplicationName() As String, ByRef URLs()_  
    As String, ByRef Length As Int32, ByRef HRESULT As Int32)  
```  
  
```csharp  
public void GetReportServerUrls(out string[] applicationName,   
    out string[] URLs, out int length, out int HRESULT);  
```  
  
## Parameters  
 *ApplicationName[]*  
 An array that contains the applications that are installed. Values are either `ReportServerWebService` or `ReportManager`.  
  
 *URLs[]*  
 An array that contains the successfully registered Urls.  
  
 *Length*  
 An integer value that contains the length of the arrays returned.  
  
 *HRESULT*  
 A value that indicates success or an error code.  
  
## Return Values  
  
## Remarks  
 Methods exposed by WMI management objects are called through the InvokeMethod function. For more information, please see "Executing Methods on Management Objects" in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework WMI documentation.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspc](../../includes/ssrswminmspc-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
