---
title: "SMTPServer property (WMI MSReportServer_ConfigurationSetting)"
description: "SMTPServer property (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "SMTPServer property"
apilocation: "reportingservices.mof"
apiname: "SMTPServer"
apitype: MOFDef
---
# ConfigurationSetting property - SMTPServer
  Gets the *SMTPServer* property from the report server configuration file. Read-only.  
  
## Syntax  
  
```vb  
Public Dim SMTPServer As String  
```  
  
```csharp  
public string SMTPServer;  
```  
  
## Property Values  
 A read-only **String** object containing the value of the *SMTPServer* property from the `RSReportServer.config` file.  
  
## Example code  
 [MSReportServer_ConfigurationSetting Class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
