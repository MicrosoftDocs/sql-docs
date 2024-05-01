---
title: "ListSSLCertificates method (WMI MSReportServer_ConfigurationSetting)"
description: "ListSSLCertificates method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "ListSSLCertificates method"
---
# ConfigurationSetting method - ListSSLCertificates
  Returns a list of certificates on the report server computer.  
  
## Syntax  
  
```vb  
Public Sub CreateSSLCertificateBinding (ByRef CertificateHash() as String, _  
    ByRef CertName() as String, ByRef HostName() as String, ByRef Length as Int32, _   
    ByRef HRESULT as Int32)  
```  
  
```csharp  
public void ListSSLCertificates(out string[] CertificateHash,   
    out string[] CertName, out string[] Hostname, out Int32 length,   
    out Int32 HRESULT);  
```  
  
## Parameters  
 *CertificateHash[]*  
 [out] The certificate hashes.  
  
 *CertName[]*  
 [out] Names of the certificate.  
  
 *HostName[]*  
 [out] The host names for the certificates.  
  
 *Length*  
 [out] Represents the length of the *CertificateHash*, *CertName* and *HostName* arrays.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. An error code indicates the call wasn't successful.  
    
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content 
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
