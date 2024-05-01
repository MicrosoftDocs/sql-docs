---
title: "ConfigurationSetting method - RemoveUnattendedExecutionAccount"
description: "ConfigurationSetting method - RemoveUnattendedExecutionAccount"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "RemoveUnattendedExecutionAccount method"
apilocation: "reportingservices.mof"
apiname: "RemoveUnattendedExecutionAccount (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - RemoveUnattendedExecutionAccount
  Deletes the unattended execution account entry from the report server configuration file.  
  
## Syntax  
  
```vb  
Public Sub RemoveUnattendedExecutionAccount(ByRef HRESULT as Int32)  
```  
  
```csharp  
public void RemoveUnattendedExecutionAccount (out Int32 HRESULT);  
```  
  
## Parameters  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content 
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
