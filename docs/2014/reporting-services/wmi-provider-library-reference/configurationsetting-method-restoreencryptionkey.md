---
title: "RestoreEncryptionKey Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "RestoreEncryptionKey (WMI MSReportServer_ConfigurationSetting Class)"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "RestoreEncryptionKey method"
ms.assetid: 37e949f5-15af-4858-848a-f482ee94fcd9
author: markingmyname
ms.author: maghan
manager: kfile
---
# RestoreEncryptionKey Method (WMI MSReportServer_ConfigurationSetting)
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
 [out] A string array containing additional errors returned by the call.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
 If an entry already exists for the report server in the report server database, it is deleted. The new entry is then created using the specified encryption key and the report server's public key.  
  
 The method is most effective when called after the [DeleteEncryptionKey](configurationsetting-method-deleteencryptionkey.md) method, which clears the list of encryption keys.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
