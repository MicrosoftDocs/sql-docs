---
title: "IsSharePointIntegrated property (WMI MSReportServer_Instance)"
description: "IsSharePointIntegrated property (WMI MSReportServer_Instance)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "IsSharePointIntegrated property"
---
# MSReportServer_Instance properties - IsSharePointIntegrated
  Specifies whether the report server is in SharePoint integrated mode. Beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], this property always returns **False** because in SharePoint mode, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instances are SharePoint shared services and aren't controlled by WMI providers.  
  
## Syntax  
  
```vb  
Public Dim IsSharePointIntegrated As Boolean  
```  
  
```csharp  
public Boolean IsSharePointIntegrated;  
```  
  
## Property values  
 A **Boolean** value that indicates whether the report server is in SharePoint integrated mode.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspc](../../includes/ssrswminmspc-md.md)]  
  
## Related content

- [MSReportServer_Instance members](../../reporting-services/wmi-provider-library-reference/msreportserver-instance-members.md)
- [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)
