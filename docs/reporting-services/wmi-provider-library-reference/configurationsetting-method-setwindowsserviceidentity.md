---
title: "SetWindowsServiceIdentity method (WMI MSReportServer_ConfigurationSetting)"
description: "SetWindowsServiceIdentity method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "SetWindowsServiceIdentity method"
apilocation: "reportingservices.mof"
apiname: "SetWindowsServiceIdentity (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - SetWindowsServiceIdentity
  Makes the Report Server Windows service run as a specified Windows user, and grants this account sufficient file system access to allow the report server to operate.  
  
## Syntax  
  
```vb  
Public Sub SetWindowsServiceIdentity(UseBuiltInAccount as Boolean, _  
    Account as String, Password as String, ByRef HRESULT as Int32)  
```  
  
```csharp  
public void SetWindowsServiceIdentity(boolean UseBuiltInAccount,   
    string Account, string Password, out Int32 HRESULT);  
```  
  
## Parameters  
 *UseBuiltInAccount*  
 Indicates whether the specified account is a built-in Windows account.  
  
 *Account*  
 The Windows account to use to run the Windows service, in the format `DOMAIN\alias`.  
  
 *Password*  
 The password for the account.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Remarks  
 When the *UseBuiltInAccount* parameter is set to **true** and the report server is running on Microsoft [!INCLUDE[win2000](../../includes/win2000-md.md)] or Windows XP, the value of the *Name*, *Domain*, and *Password* parameters are ignored and the Local system account is used.  
  
 When the *UseBuiltInAccount* parameter is set to **true** and the report server is running on Windows Server 2003, the *Domain* and *Password* properties are ignored, and the name field must contain either `Builtin\NetworkService` or `Builtin\System` or `Builtin\LocalService`.  
  
 The *SetWindowsServiceIdentity* method sets file permissions on files and folders in the report server installation directory.  
  
 The account specified in the *Account* parameter requires **LogonAsService** rights in Windows. The method grants this right to the specified account.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content 
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
