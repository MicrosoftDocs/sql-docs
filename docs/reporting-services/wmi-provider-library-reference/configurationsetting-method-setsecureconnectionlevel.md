---
title: "SetSecureConnectionLevel method (WMI MSReportServer_ConfigurationSetting)"
description: "SetSecureConnectionLevel method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "SetSecureConnectionLevel method"
apilocation: "reportingservices.mof"
apiname: "SetSecureConnectionLevel (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - SetSecureConnectionLevel
  Sets the secure connection level of the report server.  
  
## Syntax  
  
```vb  
Public Sub SetSecureConnectionLevel(Level as Integer, _  
    ByRef HRESULT as Int32)  
```  
  
```csharp  
public void SetSecureConnectionLevel(Int32 Level,   
    out Int32 HRESULT);  
```  
  
## Parameters  
 *Level*  
 An integer value representing a secure connection level.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Remarks  
 When called, the report server *SecureConnectionLevel* property is set to the value specified. A value of 0 indicates that TLS is turned off. A value greater than or equal to 1 indicates that TLS is turned on.  
  
-   When the value is set, the *SecureConnectionLevel* element in the report server configuration file is changed, and the *URLRoot* element in the configuration file is set to use `https://` if the specified *Level* is greater than or equal to 1, or `http://` if the specified *Level* is 0.  
  
 In [!INCLUDE[sql2008r2](../../includes/sql2008r2-md.md)], *SecureConnectionLevel* is made an on/off switch, default value is 0. For any value greater than or equal to 1 passed through *SetSecureConnectionLevel* method API, TLS is considered on and the configuration property *SecureConnectionLevel* is set accordingly in the `rsreportserver.config` file. Values of 2 and 3 are still allowed for backward compatibility.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
