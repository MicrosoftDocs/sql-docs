---
title: "ListSSLCertificates Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "ListSSLCertificates method"
ms.assetid: 88cd0936-b202-4ab8-90f2-d9c3f66d37f4
caps.latest.revision: 12
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# ConfigurationSetting Method - ListSSLCertificates
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
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful; an error code indicates the call was not successful.  
  
## Remarks  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  