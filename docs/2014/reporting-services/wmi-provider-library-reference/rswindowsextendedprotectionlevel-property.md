---
title: "RSWindowsExtendedProtectionLevel Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 162ffe86-69c3-49d2-b9ed-49d097c05551
author: markingmyname
ms.author: maghan
manager: kfile
---
# RSWindowsExtendedProtectionLevel Property (WMI MSReportServer_ConfigurationSetting)
  Returns a string value that indicates the level of protection the report server is configured to support. This property is read-only.  
  
## Syntax  
  
```vb  
Public Dim RSWindowsExtendedProtectionLevel As String  
```  
  
```csharp  
public string RSWindowsExtendedProtectionLevel;  
```  
  
## Remarks  
 Returns a string value that indicates the level of protection the report server is configured to support. If the report server that the WMI provider is connected to does not support extended protection, "" (empty string) is returned. The following list shows valid values:  
  
 `"Off" | "Allow" | "Require"`  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](msreportserver-configurationsetting-class.md)  
  
## See Also  
 [RSWindowsExtendedProtectionScenario Property &#40;WMI MSReportServer_ConfigurationSetting&#41;](rswindowsextendedprotectionscenario-property.md)   
 [SetExtendedProtectionSettings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](configurationsetting-method-setextendedprotectionsettings.md)   
 [Extended Protection for Authentication with Reporting Services](../security/extended-protection-for-authentication-with-reporting-services.md)   
 [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md)  
  
  
