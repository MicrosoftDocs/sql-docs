---
title: "DatabaseLogonAccount Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: wmi-provider-library-reference


ms.topic: conceptual
apiname: 
  - "DatabaseLogonAccount"
apilocation: 
  - "reportingservices.mof"
apitype: MOFDef
helpviewer_keywords: 
  - "DatabaseLogonAccount property"
ms.assetid: 55f2863f-1ac1-4519-b512-e7f11c0ea5ea
author: markingmyname
ms.author: maghan
---
# ConfigurationSetting Property - DatabaseLogonAccount
  Specifies the logon account that the report server uses when connecting to the report server database. Read only.  
  
## Syntax  
  
```vb  
Public Dim DatabaseLogonAccount As String  
```  
  
```csharp  
public string DatabaseLogonAccount;  
```  
  
## Property Values  
 A **String** object that represents the logon account name.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Remarks  
 Valid values for this property will vary depending on the value of the [DatabaseLogonType](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogontype.md) property.  
  
 This property is ignored if the [DatabaseLogonType](../../reporting-services/wmi-provider-library-reference/configurationsetting-property-databaselogontype.md) property is set to **2 (Service)**.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
