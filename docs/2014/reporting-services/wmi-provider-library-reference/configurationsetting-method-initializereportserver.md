---
title: "InitializeReportServer Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "InitializeReportServer (WMI MSReportServer_ConfigurationSetting Class)"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "InitializeReportServer method"
ms.assetid: 0304acc2-1fd7-437b-94d9-1c1073dd3ca4
author: markingmyname
ms.author: maghan
manager: kfile
---
# InitializeReportServer Method (WMI MSReportServer_ConfigurationSetting)
  Initializes the specified report service instance.  
  
## Syntax  
  
```vb  
Public Sub InitializeReportServer(ByVal InstallationID As String, _  
    ByRef HRESULT As Int32, ByRef ExtendedErrors() As String)  
```  
  
```csharp  
public void InitializeReportServer(string InstallationID,   
    out Int32 HRESULT, out string[] ExtendedErrors);  
```  
  
## Parameters  
 *InstallationID*  
 A string used to encrypt the encryption key before it is returned.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
 *ExtendedErrors[]*  
 [out] A string array containing additional errors returned by the call.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
 When this method is called, the encryption key that accesses the report server database secure information is encrypted using the public key of the report server identified by *InstallationID*.  
  
 The specified report server's public key must have previously been written into the report server database.  
  
 The *InitializeReportServer* method must be called against a report server that already has access to the secure information so that it can decrypt the encryption key. The resulting encrypted encryption key is then stored in the report server database.  
  
 If the report server's [IsInitialized](configurationsetting-property-isinitialized.md) property is set to `true` when the InitializeReportServer method is called, the method returns success without trying to encrypt the encryption key.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
