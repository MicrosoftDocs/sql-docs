---
title: "RSWindowsExtendedProtectionScenario Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 5ac7ab80-9adf-4f65-abfa-fedf53b082b5
caps.latest.revision: 5
author: "douglaslM"
ms.author: "carlasab"
manager: "mblythe"
---
# RSWindowsExtendedProtectionScenario Property (WMI MSReportServer_ConfigurationSetting)
  Returns a string value that indicates the extended protection scenario the report server is configured to allow.  
  
## Syntax  
  
```vb  
Public Dim RSWindowsExtendedProtectionScenario As String  
```  
  
```csharp  
public string RSWindowsExtendedProtectionScenario;  
```  
  
## Remarks  
 Returns a string value that indicates the extended protection scenario the report server is configured to allow. If the report server that the WMI provider is connected to does not support extended protection, “” (empty string) is returned.  
  
 The following list shows valid values:  
  
 `”Any | Proxy | Direct”`  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](../../2014/reporting-services/msreportserver-configurationsetting-class.md)  
  
## See Also  
 [RSWindowsExtendedProtectionLevel Property &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/rswindowsextendedprotectionlevel-property.md)   
 [SetExtendedProtectionSettings Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../2014/reporting-services/setextendedprotectionsettings-method-wmi-msreportserver-configurationsetting.md)   
 [Extended Protection for Authentication with Reporting Services](../../2014/reporting-services/extended-protection-for-authentication-with-reporting-services.md)   
 [RSReportServer Configuration File](../../2014/reporting-services/rsreportserver-configuration-file.md)  
  
  