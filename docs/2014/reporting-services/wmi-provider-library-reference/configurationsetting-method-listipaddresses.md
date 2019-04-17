---
title: "ListIPAddresses Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "ListIPAddresses method"
ms.assetid: 7e7cf182-fba0-4604-a474-098461e23e9d
author: markingmyname
ms.author: maghan
manager: kfile
---
# ListIPAddresses Method (WMI MSReportServer_ConfigurationSetting)
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
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful; an error code indicates the call was not successful.  
  
## Remarks  
 *IPVersion* strings are V4, V6.  
  
 If *IsDhcpEnabled* is `True`, the *IPAddress* is dynamic. It should not be used for SSL bindings.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
