---
title: "RSWindowsExtendedProtectionLevel property"
description: "RSWindowsExtendedProtectionLevel property"
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
---
# RSWindowsExtendedProtectionLevel property
  Returns a string value that indicates the level of protection the report server is configured to support. This property is read-only.  
  
## Syntax  
  
```vb  
Public Dim RSWindowsExtendedProtectionLevel As String  
```  
  
```csharp  
public string RSWindowsExtendedProtectionLevel;  
```  
  
## Remarks  
 Returns a string value that indicates the level of protection the report server is configured to support. If the report server that the WMI provider is connected to doesn't support extended protection, "" (empty string) is returned. The following list shows valid values:  
  
 `"Off" | "Allow" | "Require"`  
  
## Example code  
 [MSReportServer_ConfigurationSetting class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Related content

- [RSWindowsExtendedProtectionScenario property](rswindowsextendedprotectionscenario-property.md)
- [ConfigurationSetting method - SetExtendedProtectionSettings](configurationsetting-method-setextendedprotectionsettings.md)
- [Extended protection for authentication with Reporting Services](../security/extended-protection-for-authentication-with-reporting-services.md)
- [RsReportServer.config configuration file](../report-server/rsreportserver-config-configuration-file.md)
