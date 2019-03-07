---
title: "SetServiceState Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.date: 03/17/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: wmi-provider-library-reference


ms.topic: conceptual
apiname: 
  - "SetServiceState (WMI MSReportServer_ConfigurationSetting Class)"
apilocation: 
  - "reportingservices.mof"
apitype: MOFDef
helpviewer_keywords: 
  - "SetServiceState method"
ms.assetid: 9e1ee42d-b388-4929-89c7-8741b956c3be
author: markingmyname
ms.author: maghan
---
# ConfigurationSetting Method - SetServiceState
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
 A **Boolean** value indicating the state of the Windows service. A value of **true** starts the Report Server Windows service; a value of **false** stops the Windows service.  
  
 *EnableWebService*  
 A **Boolean** value indicating the state of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Web service. A value of **true** starts the Report Server Web service; a value of **false** stops the Web service  
  
 *EnableReportManager*  
 A **Boolean** value indicating the desired state of the Report manager.
 
 > [!NOTE] 
 > This setting has been deprecated as of SQL Server 2016 Reporting Services Cumulative Update 2. The web portal will always be enabled. The value will be ignored.
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
