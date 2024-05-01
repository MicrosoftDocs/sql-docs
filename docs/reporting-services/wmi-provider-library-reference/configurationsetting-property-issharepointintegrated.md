---
title: "IsSharePointIntegrated property (WMI)"
description: "ConfigurationSetting property - IsSharePointIntegrated"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "IsSharePointIntegrated property"
---
# ConfigurationSetting property - IsSharePointIntegrated
  Specifies whether the report server is in SharePoint integrated mode. Beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], this property always returns **False** because in SharePoint mode, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instances are SharePoint shared services and are not controlled by WMI providers.  
  
## Syntax  
  
```vb  
Public Dim IsSharePointIntegrated As Boolean  
```  
  
```csharp  
public Boolean IsSharePointIntegrated;  
```  
  
## Property values  
 A **Boolean** object that indicates whether the report server is in SharePoint integrated mode.  
  
## Example code  
 [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content  
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
