---
title: "DatabaseLogonType Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "DatabaseLogonType"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DatabaseLogonType property"
ms.assetid: 6b592582-4c35-4029-ab86-982fff47d8d6
author: markingmyname
ms.author: maghan
manager: kfile
---
# DatabaseLogonType Property (WMI MSReportServer_ConfigurationSetting)
  Specifies whether the report server uses a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows service account, a Windows user account, or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to access the report server database. Read-only.  
  
## Syntax  
  
```vb  
Public Dim DatabaseLogonType As Integer  
```  
  
```csharp  
public int DatabaseLogonType;  
```  
  
## Property Values  
 An `integer` object that represents the login type.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](msreportserver-configurationsetting-class.md)  
  
## Remarks  
 Values are:  
  
-   0 for Windows login  
  
-   1 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login  
  
-   2 to log in as a service  
  
 If you specify 0 (Windows), you must set the value in the [DatabaseLogonAccount](configurationsetting-property-databaselogonaccount.md) property to a corresponding a valid Windows user account.  
  
 If you specify 1 (SQL Server), make sure the value of the [DatabaseLogonAccount](configurationsetting-property-databaselogonaccount.md) corresponds to a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 If you specify 2 (Windows service), the report server uses an [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] account and the Windows service account to access the report server database. The DatabaseLogonAccount property is ignored.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
