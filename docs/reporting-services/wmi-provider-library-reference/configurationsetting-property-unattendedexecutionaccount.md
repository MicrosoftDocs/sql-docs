---
title: "UnattendedExecutionAccount Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "UnattendedExecutionAccount"
apilocation: 
  - "reportingservices.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "UnattendedExecutionAccount property"
ms.assetid: ab5203ba-c01e-4020-8619-ee290cf9da07
caps.latest.revision: 17
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# ConfigurationSetting Property - UnattendedExecutionAccount
  Returns the user account that the report server impersonates when running reports unattended. Read-only.  
  
## Syntax  
  
```vb  
Public Dim UnattendedExecutionAccount As String  
```  
  
```csharp  
public string UnattendedExecutionAccount;  
```  
  
## Property Values  
 A **String** object that represents the account name.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  