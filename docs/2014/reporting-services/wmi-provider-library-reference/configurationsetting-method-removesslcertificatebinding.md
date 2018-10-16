---
title: "RemoveSSLCertificateBindings Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "RemoveSSLCertificateBindings method"
ms.assetid: b8b484c9-04c4-4ae9-980e-67bbe5aa8481
author: markingmyname
ms.author: maghan
manager: craigg
---
# RemoveSSLCertificateBindings Method (WMI MSReportServer_ConfigurationSetting)
  Removes an SSL Certificate binding.  
  
## Syntax  
  
```vb  
Public Sub RemoveSSLCertificateBinding(ByVal Application As String, _  
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
 The SSL port associated with the binding.  
  
 *lcid*  
 The locale to use for the error messages that are returned.  
  
 *Error*  
 [out] The description of the error that occurred.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful; an error code indicates the call was not successful.  
  
## Remarks  
 This method removes the specific binding from the rsreportserver.config file and optionally HTTP.SYS.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
