---
title: "IsSharePointIntegrated Property (WMI MSReportServer_Instance) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: wmi-provider-library-reference


ms.topic: conceptual
helpviewer_keywords: 
  - "IsSharePointIntegrated property"
ms.assetid: e21d00ad-5d9a-4290-8d74-7eeeda39e1ed
author: markingmyname
ms.author: maghan
---
# MSReportServer_Instance Properties - IsSharePointIntegrated
  Specifies whether the report server is in SharePoint integrated mode. Beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], this property always returns **False** because in SharePoint mode, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instances are SharePoint shared services and are not controlled by WMI providers.  
  
## Syntax  
  
```vb  
Public Dim IsSharePointIntegrated As Boolean  
```  
  
```csharp  
public Boolean IsSharePointIntegrated;  
```  
  
## Property Values  
 A **Boolean** value that indicates whether the report server is in SharePoint integrated mode.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspc](../../includes/ssrswminmspc-md.md)]  
  
## See Also  
 [MSReportServer_Instance Members](../../reporting-services/wmi-provider-library-reference/msreportserver-instance-members.md)   
 [MSReportServer_ConfigurationSetting Class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
  
