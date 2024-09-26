---
title: "RemoveURL method (WMI MSReportServer_ConfigurationSetting)"
description: "RemoveURL method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "RemoveURL method"
---
# ConfigurationSetting method - RemoveURL
  Removes a URL reserved for the report server. If there are multiple URLs that need to be removed, you must do so one by one calling this API.  
  
## Syntax  
  
```vb  
Public Sub RemoveURL(ByVal Application As String, _  
    ByVal UrlString As String, ByVal Lcid As Int32, _  
    ByRef [Error] As String, ByRef HRESULT As Int32)  
```  
  
```csharp  
public void RemoveURL(string Application, string UrlString, int Lcid,   
    out string Error, out int HRESULT);  
```  
  
## Parameters  
 *Application*  
 The name of application for which to remove the reservation.  
  
 *URLString*  
 The URL for the reservation.  
  
 *lcid*  
 The locale to use for the error messages returned.  
  
 *Error*  
 [out] The description of the error that occurred.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful; an error code indicates the call wasn't successful.  
  
## Remarks  
 *UrlString* doesn't include the Virtual Directory name - the [SetVirtualDirectory Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-setvirtualdirectory.md) method is provided for that purpose.  
  
 Before calling the [ReserveURL](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-reserveurl.md) method, you must supply a value for the VirtualDirectory configuration property for the *Application* parameter. Use the [SetVirtualDirectory Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-setvirtualdirectory.md) method to set the VirtualDirectory property.  
  
 If you provision a TLS/SSL certificate by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and no other URLs need it, the certificate is removed.  
  
 This method causes all nonconfiguration app domains to hard recycle and stop during this operation. App domains are restarted after this operation complete.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
