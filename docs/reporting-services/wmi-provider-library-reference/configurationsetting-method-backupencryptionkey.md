---
description: "BackupEncryptionKey Method (WMI MSReportServer_ConfigurationSetting)"
title: "BackupEncryptionKey Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference


ms.topic: conceptual
apiname: 
  - "BackupEncryptionKey Method (WMI MSReportServer_ConfigurationSetting Class)"
apilocation: 
  - "reportingservices.mof"
apitype: MOFDef
helpviewer_keywords: 
  - "BackupEncryptionKey method"
ms.assetid: da1d5dae-2517-448e-96fb-5379c93222ea
author: maggiesMSFT
ms.author: maggies
---
# ConfigurationSetting Method - BackupEncryptionKey
  Backs up the encryption key for the specified report server instance. The encryption key is stored encrypted with a password.  
  
## Syntax  
  
```vb  
Public Sub BackupEncryptionKey(Password as String, _  
    ByRef KeyFile() as Integer, ByRef Length as Int32, _  
    ByRef HRESULT as Int32, ByRef ExtendedErrors() as String)  
  
```  
  
```csharp  
public void BackupEncryptionKey(string Password, out Byte[] KeyFile,   
    out Int32 Length, out Int32 HRESULT, out string[] ExtendedErrors);  
```  
  
## Parameters  
 *Password*  
 A string used to encrypt the encryption key before it is returned.  
  
 *KeyFile[]*  
 [out] An array containing the encrypted encryption key.  
  
 *Length*  
 [out] The length of the array returned by the method.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
 *ExtendedErrors[]*  
 [out] A string array containing additional errors returned by the call.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
