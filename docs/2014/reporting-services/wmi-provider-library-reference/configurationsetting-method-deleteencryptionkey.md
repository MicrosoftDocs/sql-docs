---
title: "DeleteEncryptionKey Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "DeleteEncryptionKey (WMI MSReportServer_ConfigurationSetting Class)"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DeleteEncryptionKey method"
ms.assetid: ed2f25b6-6a63-468d-9279-a577ca01b096
author: markingmyname
ms.author: maghan
manager: kfile
---
# DeleteEncryptionKey Method (WMI MSReportServer_ConfigurationSetting)
  Deletes the encryption keys from the report server database.  
  
## Syntax  
  
```vb  
Public Sub DeleteEncryptionKeys(ByVal InstallationID As String, _  
    ByRef HRESULT As Int32, ByRef ExtendedErrors() As String)  
```  
  
```csharp  
public void DeleteEncryptionKeys(string InstallationID, out Int32 HRESULT,   
    out string[] ExtendedErrors);  
```  
  
## Parameters  
 *InstallationID*  
 The installation ID of a report server that is in the keys table of the report server database.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
 *ExtendedErrors[]*  
 [out] A string array containing additional errors returned by the call.  
  
## Return Value  
 Returns an HRESULT indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
 The *DeleteEncryptionKey* method deletes entries from the keys table for any report servers that have access to the secure information in the report server database. If the *InstallationID* parameter specified does not correspond to an installation ID in the database, the method returns an error.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
