---
title: "IsSharePointIntegrated Property (WMI) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "IsSharePointIntegrated property"
ms.assetid: c548fed8-5e04-4faf-8b10-b37c86178056
caps.latest.revision: 11
author: "douglaslM"
ms.author: "carlasab"
manager: "mblythe"
---
# IsSharePointIntegrated Property (WMI)
  Specifies whether the report server is in SharePoint integrated mode. Beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], this property always returns `False` because in SharePoint mode, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instances are SharePoint shared services and are not controlled by WMI providers.  
  
## Syntax  
  
```vb  
Public Dim IsSharePointIntegrated As Boolean  
```  
  
```csharp  
public Boolean IsSharePointIntegrated;  
```  
  
## Property Values  
 A `Boolean` object that indicates whether the report server is in SharePoint integrated mode.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](../../2014/reporting-services/msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../2014/reporting-services/msreportserver-configurationsetting-members.md)  
  
  