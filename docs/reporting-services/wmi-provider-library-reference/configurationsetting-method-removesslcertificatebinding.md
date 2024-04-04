---
title: "RemoveSSLCertificateBindings method (WMI MSReportServer_ConfigurationSetting)"
description: "RemoveSSLCertificateBindings method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/29/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "RemoveSSLCertificateBindings method"
---
# ConfigurationSetting method - RemoveSSLCertificateBindings
  Removes a TLS/SSL certificate binding.  
  
## Syntax  
  
```vb  
Public Sub RemoveSSLCertificateBindings(ByVal Application As String, _  
    ByVal CertificateHash As String, ByVal IPAddress As String, _  
    ByVal Port As Int32, ByVal Lcid As Int32, _  
    ByRef [Error] As String, ByRef HRESULT As Int32)  
```  
  
```csharp  
public void RemoveSSLCertificateBindings(string Application,  
    string CertificateHash, string IPAddress, Int32 Port, Int32 Lcid,  
    out string Error, out Int32 HRESULT);  
```  
  
## Parameters  
 *Application*  
 The name of application for which the certificate binding should be removed.  
  
 *CertificateHash*  
 The hash for the certificate.  
  
 *IPAddress*  
 The IP address for the application.  
  
 *Port*  
 The TLS port associated with the binding.  
  
 *lcid*  
 The locale to use for the error messages that are returned.  
  
 *Error*  
 [out] The description of the error that occurred.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. An error code indicates the call wasn't successful.  
  
## Remarks  
 This method removes the specific binding from the `rsreportserver.config` file and optionally `HTTP.SYS`.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
