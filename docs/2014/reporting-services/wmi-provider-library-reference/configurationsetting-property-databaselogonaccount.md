---
title: "DatabaseLogonAccount Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "DatabaseLogonAccount"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DatabaseLogonAccount property"
ms.assetid: 55f2863f-1ac1-4519-b512-e7f11c0ea5ea
author: markingmyname
ms.author: maghan
manager: craigg
---
# DatabaseLogonAccount Property (WMI MSReportServer_ConfigurationSetting)
  Specifies the logon account that the report server uses when connecting to the report server database. Read only.  
  
## Syntax  
  
```vb  
Public Dim DatabaseLogonAccount As String  
```  
  
```csharp  
public string DatabaseLogonAccount;  
```  
  
## Property Values  
 A `String` object that represents the logon account name.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](msreportserver-configurationsetting-class.md)  
  
## Remarks  
 Valid values for this property will vary depending on the value of the [DatabaseLogonType](configurationsetting-property-databaselogontype.md) property.  
  
 This property is ignored if the [DatabaseLogonType](configurationsetting-property-databaselogontype.md) property is set to `2 (Service)`.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
