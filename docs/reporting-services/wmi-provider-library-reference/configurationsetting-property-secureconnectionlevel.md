---
description: "SecureConnectionLevel Property (WMI MSReportServer_ConfigurationSetting)"
title: "SecureConnectionLevel Property (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference


ms.topic: conceptual
apiname: 
  - "SecureConnectionLevel"
apilocation: 
  - "reportingservices.mof"
apitype: MOFDef
helpviewer_keywords: 
  - "SecureConnectionLevel property"
ms.assetid: fd5549e7-b874-41e2-866e-2f58caf6f733
author: maggiesMSFT
ms.author: maggies
---
# ConfigurationSetting Property - SecureConnectionLevel
  Returns the secure connection level specified in the RSReportServer.config file. Read-only.  
  
## Syntax  
  
```vb  
Public Dim SecureConnectionLevel As Integer  
```  
  
```csharp  
public Integer SecureConnectionLevel;  
```  
  
## Property Values  
 An **Integer** value that represents the secure connection level. The return values indicate that the TLS is either configured or not. A value of greater than or equal to 1 indicates that TLS is turned on. A value of 0 indicates that TLS is turned off.  
  
## Example Code  
 [MSReportServer_ConfigurationSetting Class](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-class.md)  
  
## Remarks

In SQL Server 2008 R2, SecureConnectionLevel is made an on/off switch. For more information, see [ConfigurationSetting Method - SetSecureConnectionLevel](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-setsecureconnectionlevel.md).

## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
