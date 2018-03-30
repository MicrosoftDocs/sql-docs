---
title: "DatabaseLogonAccount Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
api_name: 
  - "DatabaseLogonAccount"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DatabaseLogonAccount property"
ms.assetid: 55f2863f-1ac1-4519-b512-e7f11c0ea5ea
caps.latest.revision: 24
author: "douglaslM"
ms.author: "douglasl"
manager: "mblythe"
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
 [MSReportServer_ConfigurationSetting Class](../../2014/reporting-services/msreportserver-configurationsetting-class.md)  
  
## Remarks  
 Valid values for this property will vary depending on the value of the [DatabaseLogonType](../../2014/reporting-services/databaselogontype-property-wmi-msreportserver-configurationsetting.md) property.  
  
 This property is ignored if the [DatabaseLogonType](../../2014/reporting-services/databaselogontype-property-wmi-msreportserver-configurationsetting.md) property is set to `2 (Service)`.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../2014/reporting-services/msreportserver-configurationsetting-members.md)  
  
  