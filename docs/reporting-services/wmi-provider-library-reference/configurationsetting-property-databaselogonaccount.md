---
title: "DatabaseLogonAccount property (WMI MSReportServer_ConfigurationSetting)"
description: "DatabaseLogonAccount property (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "DatabaseLogonAccount property"
apilocation: "reportingservices.mof"
apiname: "DatabaseLogonAccount"
apitype: MOFDef
---
# ConfigurationSetting property - DatabaseLogonAccount
  Specifies the sign in account that the report server uses when connecting to the report server database. Read only.  
  
## Syntax  
  
```vb  
Public Dim DatabaseLogonAccount As String  
```  
  
```csharp  
public string DatabaseLogonAccount;  
```  
  
## Property values  
 A **String** object that represents the sign in account name.  
  
## Example code  
 [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Remarks  
 Valid values for this property vary depending on the value of the [DatabaseLogonType](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogontype.md) property.  
  
 This property is ignored if the [DatabaseLogonType](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogontype.md) property is set to **2 (Service)**.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
