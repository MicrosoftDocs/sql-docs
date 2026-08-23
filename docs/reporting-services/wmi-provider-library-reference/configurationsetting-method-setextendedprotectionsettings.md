---
title: "SetExtendedProtectionSettings method (WMI MSReportServer_ConfigurationSetting)"
description: "SetExtendedProtectionSettings method (WMI MSReportServer_ConfigurationSetting)"
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: reference
ms.custom:
  - updatefrequency5
---
# ConfigurationSetting method - SetExtendedProtectionSettings
  The *SetExtendedProtectionSettings* method is used to set the *RSWindowsExtendedProtectionLevel* and the *RSWindowsExtendedProtectionScenario* properties in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration file `RSReportServer.config`.  
  
## Syntax  
  
```vb  
Public Sub SetExtendedProtectionSettings( _  
        ByVal ExtendedProtectionLevel As String, _  
        ByVal ExtendedProtectionScenario As String, _  
        ByRef Warnings() As String, _  
        ByRef Length As Int32, _  
        ByRef HRESULT As Int32)  
```  
  
```csharp  
public void SetExtendedProtectionSettings(  
            string ExtendedProtectionLevel,  
            string ExtendedProtectionScenario,  
            out string[] Warnings,  
            out Int32 Length,  
            out Int32 HRESULT);  
```  
  
## Parameters  
 *ExtendedProtectionLevel*  
 Sets the *RSWindowsExtendedProtectionLevel* in the `RSRreportserver.config` file. The required value isn't case sensitive.  
  
 The following list shows valid values:  
  
 `"Off | Allow | Require"`  
  
 *ExtendedProtectionScenario*  
 Sets the *RSWindowsExtendedProtectionScenario* in the `RSReportserver.config` file. The required value isn't case sensitive.  
  
 The following list shows valid values:  
  
 `"Any" | "Proxy" | "Direct"`  
  
## Remarks  
 The *RSWindowsExtendedProtectionLevel* and the *RSWindowsExtendedProtectionScenario* properties apply when the *AuthenticationTypes* in the `RSReportServer.config` file include *RSWindowNTLM*, *RSWindowsNegotiate*, or *RSWindowsKerberos*. Setting these properties affects how users and client software authenticate with a report server. You should read the documentation for extended protection before setting *ExtendedProtectionLevel* to either **Allow** or **Require**.  
  
 To set the *ExtendedProtectionLevel*, the user must be a member of the `BUILTIN\Administrators` group on the report server.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [RSWindowsExtendedProtectionScenario property](rswindowsextendedprotectionscenario-property.md)
- [RSWindowsExtendedProtectionLevel property](rswindowsextendedprotectionlevel-property.md)
- [Extended protection for authentication with Reporting Services](../security/extended-protection-for-authentication-with-reporting-services.md)
- [RsReportServer.config configuration file](../report-server/rsreportserver-config-configuration-file.md)
