---
description: "ConfigurationSetting Method - RemoveUnattendedExecutionAccount"
title: "ConfigurationSetting Method - RemoveUnattendedExecutionAccount | Microsoft Docs"
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference


ms.topic: conceptual
apiname: 
  - "RemoveUnattendedExecutionAccount (WMI MSReportServer_ConfigurationSetting Class)"
apilocation: 
  - "reportingservices.mof"
apitype: MOFDef
helpviewer_keywords: 
  - "RemoveUnattendedExecutionAccount method"
ms.assetid: 77e371c1-7c26-44f9-9119-7c8dc838db32
author: maggiesMSFT
ms.author: maggies
---
# ConfigurationSetting Method - RemoveUnattendedExecutionAccount
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
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
