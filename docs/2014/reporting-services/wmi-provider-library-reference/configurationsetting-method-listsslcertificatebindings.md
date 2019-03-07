---
title: "ListSSLCertificateBindings Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "ListSSLCertificateBindings method"
ms.assetid: d12d280c-9b6f-47a8-bcd9-34cde31c8886
author: markingmyname
ms.author: maghan
manager: kfile
---
# ListSSLCertificateBindings Method (WMI MSReportServer_ConfigurationSetting)
  Returns a list of installed SSL certificates on the computer.  
  
## Syntax  
  
```vb  
Public Sub ListSSLCertificateBindings(ByVal LCID As Int32, ByRef Application() As String, _  
    ByRef CertificateHash() As String, ByRef IPAddresses() As String, ByRef Port() As Int32, _  
    ByRef Errors() As String, ByRef Length As Int32, _  
    ByRef HRESULT As Int32)  
```  
  
```csharp  
public void ListSSLCertificateBindings(Int32 Lcid, out string[] Application,   
    out string[] CertificateHash,out string[] IPAddress,   
    out Int32[] Port, out string Errors,   
    out Int32 length, out Int32 HRESULT);  
```  
  
## Parameters  
 *LCID*  
 The locale to use for the error messages that are returned.  
  
 *Application[]*  
 [out] The applications that have certificate bindings.  
  
 *CertificateHash[]*  
 [out] The hashes for the certificates.  
  
 *IPAddress[]*  
 [out] The IP address for the applications.  
  
 *Port[]*  
 [out] The port number stored in the binding in rsreportserver.config.  
  
 *Errors[]*  
 [out] The descriptions for errors that occurred.  
  
 *Length*  
 [out] The length of the array returned by the method.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
