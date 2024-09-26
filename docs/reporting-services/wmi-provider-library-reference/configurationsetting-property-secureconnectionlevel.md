---
title: "SecureConnectionLevel property (WMI MSReportServer_ConfigurationSetting)"
description: "SecureConnectionLevel property (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "SecureConnectionLevel property"
apilocation: "reportingservices.mof"
apiname: "SecureConnectionLevel"
apitype: MOFDef
---
# ConfigurationSetting property - SecureConnectionLevel
  Returns the secure connection level specified in the `RSReportServer.config` file. Read-only.  
  
## Syntax  
  
```vb  
Public Dim SecureConnectionLevel As Integer  
```  
  
```csharp  
public Integer SecureConnectionLevel;  
```  
  
## Property values  
 An **Integer** value that represents the secure connection level. The return values indicate that the TLS is either configured or not. A value of greater than or equal to 1 indicates that TLS is turned on. A value of 0 indicates that TLS is turned off.  
  
## Example code  
 [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Remarks

In SQL Server 2008 R2, *SecureConnectionLevel* is made an on/off switch. For more information, see [ConfigurationSetting method - SetSecureConnectionLevel](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-setsecureconnectionlevel.md).

## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
