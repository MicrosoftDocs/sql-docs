---
title: "DatabaseQueryTimeout Property (WMI MSReportServer_ConfigurationSetting)"
description: "DatabaseQueryTimeout Property (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "DatabaseQueryTimeout property"
apilocation: "reportingservices.mof"
apiname: "DatabaseQueryTimeout Property"
apitype: MOFDef
---
# ConfigurationSetting Property - DatabaseQueryTimeout
  Specifies the number of seconds that must elapse before the report server assumes the command failed or took too much time to perform. The report server is timing the querying against the SQL catalog, not a data source for the report. Read/write.  
  
## Syntax  
  
```vb  
Public Dim DatabaseQueryTimeout As UInt32  
```  
  
```csharp  
public UInt32 DatabaseQueryTimeout;  
```  
  
## Property Values  
 A 32-bit unsigned **integer** object that represents the number of seconds that the query is allowed to run.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
