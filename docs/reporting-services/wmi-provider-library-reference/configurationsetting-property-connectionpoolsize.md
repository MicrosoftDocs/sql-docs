---
title: "ConnectionPoolSize property (WMI MSReportServer_ConfigurationSetting)"
description: "ConnectionPoolSize property (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "ConnectionPoolSize property"
apilocation: "reportingservices.mof"
apiname: "ConnectionPoolSize"
apitype: MOFDef
---
# ConfigurationSetting property - ConnectionPoolSize
  The connection pool size used by the report server to communicate with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database. Read-only.  
  
## Syntax  
  
```vb  
Public Dim ConnectionPoolSize As UInt32  
```  
  
```csharp  
public UInt32 ConnectionPoolSize;  
```  
  
## Property values  
 A read-only **integer** object that returns a value of **768**.  
  
## Example code  
 [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content  
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  