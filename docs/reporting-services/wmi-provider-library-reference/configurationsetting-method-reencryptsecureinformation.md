---
title: "ReencryptSecureInformation method (WMI MSReportServer_ConfigurationSetting)"
description: "ReencryptSecureInformation method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "ReencryptSecureInformation method"
apilocation: "reportingservices.mof"
apiname: "ReencryptSecureInformation (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - ReencryptSecureInformation
  Generates a new encryption key and re-encrypts all secure information in the catalog using this new key.  
  
## Syntax  
  
```vb  
Public Sub ReencryptSecureInformation(ByRef HRESULT as Int32, ByRef ExtendedErrors() As String)  
```  
  
```csharp  
public void ReencryptSecureInformation (out Int32 HRESULT, out string[] ExtendedErrors);  
```  
  
## Parameters  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
 *ExtendedErrors[]*  
 [out] A string array containing other errors returned by the call.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Remarks  
 The *ReencryptSecureInformation* method allows the administrator to replace the existing encryption key with a new key.  
  
 When this method is invoked, the report server generates a new encryption key and iterates through all encrypted content to re-encrypt it with the new encryption key.  
  
 Delivery extensions can store secured information in their delivery settings structures. When *ReencryptSecureInformation* is called, the report server loads each subscription and the corresponding delivery extension to re-encrypt information stored in the associated settings.  
  
 If this method is run on a computer in a scale-out deployment, each computer in the scale-out deployment needs to be initialized again.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content  
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
