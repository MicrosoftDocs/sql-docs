---
title: "SetUnattendedExecutionAccount method (WMI MSReportServer_ConfigurationSetting)"
description: "SetUnattendedExecutionAccount method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "SetUnattendedExecutionAccount method"
apilocation: "reportingservices.mof"
apiname: "SetUnattendedExecutionAccount (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - SetUnattendedExecutionAccount
  Specifies the account used to execute reports unattended.  
  
## Syntax  
  
```vb  
Public Sub SetUnattendedExecutionAccount(UserName as String, _  
    Password as String, ByRef HRESULT as Int32)  
```  
  
```csharp  
public void SetUnattendedExecutionAccount (string UserName,   
    string Password, out Int32 HRESULT);  
```  
  
## Parameters  
 *UserName*  
 A Windows account to be used for unattended executions.  
  
 *Password*  
 The password for the specified account.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Remarks  
 The *SetUnattendedExecutionAccount* method doesn't verify whether the report server can sign in as the specified user.  
  
 It isn't possible to use the *SetUnattendedExecutionAccount* method to run unattended executions in the context of the report server Windows service.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
