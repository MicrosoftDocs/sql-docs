---
title: "GetReportServerUrls Method (WMI MSReportServer_Instance) | Microsoft Docs"
ms.date: 06/09/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: wmi-provider-library-reference


ms.topic: conceptual
helpviewer_keywords: 
  - "GetReportServerUrls method"
ms.assetid: 4865e32c-0114-465a-be8c-be223a7bc09e
author: maggiesMSFT
ms.author: maggies
---
# MSReportServer_Instance Methods - GetReportServerUrls
  Returns a list of URLs users can use to access the report server and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)].  
  
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
 An array that contains the applications that are installed. Values are either **ReportServerWebService** or **ReportServerWebApp**.  
  
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
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
