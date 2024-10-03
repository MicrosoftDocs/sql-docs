---
title: "DatabaseLogonTimeout property (WMI MSReportServer_ConfigurationSetting)"
description: "DatabaseLogonTimeout property (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "DatabaseLogonTimeout property"
apilocation: "reportingservices.mof"
apiname: "DatabaseLogonTimeout Property"
apitype: MOFDef
---
# ConfigurationSetting property - DatabaseLogonTimeout
  Specifies the number of seconds to wait before an attempt to sign in to the report server database fails. A value of **0** indicates an infinite wait time. Read only.  
  
## Syntax  
  
```vb  
Public Dim DatabaseLogonTimeout As Int32  
```  
  
```csharp  
public Int32 DatabaseLogonTimeout;  
```  
  
## Property values  
 A 32-bit signed integer object that represents the number of seconds.  
  
## Example code  
 [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
