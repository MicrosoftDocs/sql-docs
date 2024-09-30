---
title: "RSWindowsExtendedProtectionScenario property"
description: "RSWindowsExtendedProtectionScenario property"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
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

- [RSWindowsExtendedProtectionLevel property &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/rswindowsextendedprotectionlevel-property.md)
- [SetExtendedProtectionSettings method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-setextendedprotectionsettings.md)
- [Extended Protection for authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md)
- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
