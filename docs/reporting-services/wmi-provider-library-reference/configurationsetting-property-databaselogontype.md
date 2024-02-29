---
title: "DatabaseLogonType property (WMI MSReportServer_ConfigurationSetting)"
description: "DatabaseLogonType property (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "DatabaseLogonType property"
apilocation: "reportingservices.mof"
apiname: "DatabaseLogonType"
apitype: MOFDef
---
# ConfigurationSetting property - DatabaseLogonType
  Read-only. Specifies whether the report server uses:
  - A [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows service account
  - A Windows user account
  - A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign in to access the report server database.  
  
## Syntax  
  
```vb  
Public Dim DatabaseLogonType As Integer  
```  
  
```csharp  
public int DatabaseLogonType;  
```  
  
## Property values  
 An **integer** object that represents the sign in type.  
  
## Example code  
 [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Remarks  
 Values are:  
  
-   0 for Windows sign in  
  
-   1 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign in  
  
-   2 to sign in as a service  
  
 If you specify 0 (Windows), you must set the value in the [DatabaseLogonAccount](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogonaccount.md) property to a corresponding a valid Windows user account.  
  
 If you specify **1** (SQL Server), make sure the value of the [DatabaseLogonAccount](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogonaccount.md) corresponds to a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign in.  
  
 If you specify **2** (Windows service), the report server uses an [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] account and the Windows service account to access the report server database. The *DatabaseLogonAccount* property is ignored.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content  
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
