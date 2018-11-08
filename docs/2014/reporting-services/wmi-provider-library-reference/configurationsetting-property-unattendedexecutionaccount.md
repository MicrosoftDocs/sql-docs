---
title: "UnattendedExecutionAccount Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "UnattendedExecutionAccount"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "UnattendedExecutionAccount property"
ms.assetid: ab5203ba-c01e-4020-8619-ee290cf9da07
author: markingmyname
ms.author: maghan
manager: craigg
---
# UnattendedExecutionAccount Property (WMI MSReportServer_ConfigurationSetting)
  Returns the user account that the report server impersonates when running reports unattended. Read-only.  
  
## Syntax  
  
```vb  
Public Dim UnattendedExecutionAccount As String  
```  
  
```csharp  
public string UnattendedExecutionAccount;  
```  
  
## Property Values  
 A `String` object that represents the account name.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
