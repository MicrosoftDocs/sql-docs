---
title: "SetServiceState method (WMI MSReportServer_ConfigurationSetting)"
description: "SetServiceState method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 03/17/2017
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "SetServiceState method"
apilocation: "reportingservices.mof"
apiname: "SetServiceState (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - SetServiceState
  Turns the Report Server Windows and Web services on and off.  
  
## Syntax  
  
```vb  
Public Sub SetServiceState(ByVal EnableWindowsService As Boolean, _  
    ByVal EnableWebService As Boolean, ByVal EnableReportManager As Boolean, _  
    ByRef HRESULT As Int32)  
```  
  
```csharp  
public void SetServiceState(Boolean EnableWindowsService,  
    Boolean EnableWebService, Boolean EnableReportManager, out Int32 HRESULT);  
```  
  
## Parameters  
 *EnableWindowsService*  
 A **Boolean** value indicating the state of the Windows service. A value of **true** starts the Report Server Windows service. A value of **false** stops the Windows service.  
  
 *EnableWebService*  
 A **Boolean** value indicating the state of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Web service. A value of **true** starts the Report Server Web service. A value of **false** stops the Web service.  
  
 *EnableReportManager*  
 A **Boolean** value indicating the desired state of the Report manager.
 
 > [!NOTE] 
 > This setting has been deprecated as of SQL Server 2016 Reporting Services Cumulative Update 2. The web portal is always enabled. The value is ignored.
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
    
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content  
 [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
