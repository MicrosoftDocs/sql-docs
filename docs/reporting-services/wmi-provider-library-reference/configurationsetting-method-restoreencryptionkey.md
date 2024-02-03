---
title: "RestoreEncryptionKey method (WMI MSReportServer_ConfigurationSetting)"
description: "RestoreEncryptionKey method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "RestoreEncryptionKey method"
apilocation: "reportingservices.mof"
apiname: "RestoreEncryptionKey (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - RestoreEncryptionKey
  Reapplies the specified encryption key to the report server database.  
  
## Syntax  
  
```vb  
Public Sub RestoreEncryptionKey(ByRef KeyFile() As Integer, _  
    ByRef Length As Int32, ByVal Password As String, _  
    ByRef HRESULT As Int32, ByRef ExtendedErrors() As String)  
```  
  
```csharp  
public void RestoreEncryptionKey(out Byte[] KeyFile, out Int32 Length,   
            string Password, out Int32 HRESULT, out string[] ExtendedErrors);  
```  
  
## Parameters  
 *KeyFile[]*  
 [out] An array containing the encrypted encryption key.  
  
 *Length*  
 [out] The length of the array returned by the method.  
  
 *Password*  
 A string used to encrypt the encryption key.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
 *ExtendedErrors[]*  
 [out] A string array containing other errors returned by the call.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Remarks  
 If an entry already exists for the report server in the report server database, that entry is deleted. The new entry is then created using the specified encryption key and the report server's public key.  
  
 The method is most effective when called after the [DeleteEncryptionKey](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-deleteencryptionkey.md) method, which clears the list of encryption keys.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content  
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
