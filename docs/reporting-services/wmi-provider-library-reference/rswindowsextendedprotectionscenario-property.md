---
title: "RSWindowsExtendedProtectionScenario property"
description: "RSWindowsExtendedProtectionScenario property"
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
---
# RSWindowsExtendedProtectionScenario property
  Returns a string value that indicates the extended protection scenario the report server is configured to allow.  
  
## Syntax  
  
```vb  
Public Dim RSWindowsExtendedProtectionScenario As String  
```  
  
```csharp  
public string RSWindowsExtendedProtectionScenario;  
```  
  
## Remarks  
 Returns a string value that indicates the extended protection scenario the report server is configured to allow. If the report server that the WMI provider is connected to doesn't support extended protection, "" (empty string) is returned.  
  
 The following list shows valid values:  
  
 `"Any | Proxy | Direct"`  
  
## Example code  
 [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Related content

- [RSWindowsExtendedProtectionLevel property](rswindowsextendedprotectionlevel-property.md)
- [ConfigurationSetting method - SetExtendedProtectionSettings](configurationsetting-method-setextendedprotectionsettings.md)
- [Extended protection for authentication with Reporting Services](../security/extended-protection-for-authentication-with-reporting-services.md)
- [RsReportServer.config configuration file](../report-server/rsreportserver-config-configuration-file.md)
