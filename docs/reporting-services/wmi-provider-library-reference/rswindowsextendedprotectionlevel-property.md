---
title: "RSWindowsExtendedProtectionLevel property"
description: "RSWindowsExtendedProtectionLevel property"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
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

- [RSWindowsExtendedProtectionScenario property &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/rswindowsextendedprotectionscenario-property.md)
- [SetExtendedProtectionSettings method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-setextendedprotectionsettings.md)
- [Extended Protection for authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md)
- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
