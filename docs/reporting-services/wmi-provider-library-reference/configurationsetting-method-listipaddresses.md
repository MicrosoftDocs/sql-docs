---
title: "ListIPAddresses method (WMI MSReportServer_ConfigurationSetting)"
description: "ListIPAddresses method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "ListIPAddresses method"
---
# ConfigurationSetting method - ListIPAddresses
  Lists the IP addresses for the report server computer.  
  
## Syntax  
  
```vb  
Public Sub ListIPAddresses (ByRef IPAddress() as String, _  
    ByRef IPVersion()as String, ByRef IsDhcpEnabled () as Boolean, _   
    ByRef Length as Int32, ByRef HRESULT as Int32)  
```  
  
```csharp  
public void ListIPAddresses (out string[] IPAddress,   
    out string[] IPVersion, out bool[] isDhcpEnabled, out int length,   
    out int HRESULT);  
```  
  
## Parameters  
 *IPAddress[]*  
 [out] The list of IP address for the computer.  
  
 *IPVersion[]*  
 [out] The version for the IP addresses.  
  
 *IsDhcpEnabled[]*  
 [out] Indicates whether the IP addresses are DHCP enabled.  
  
 *Length*  
 [out] The length of the array returned by the method.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. An error code indicates the call wasn't successful.  
  
## Remarks  
 *IPVersion* strings are V4, V6.  
  
 If *IsDhcpEnabled* is **True**, the *IPAddress* is dynamic. It shouldn't be used for TLS bindings.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
