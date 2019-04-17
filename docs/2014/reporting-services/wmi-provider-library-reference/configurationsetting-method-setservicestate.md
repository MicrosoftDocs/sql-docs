---
title: "SetServiceState Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "SetServiceState (WMI MSReportServer_ConfigurationSetting Class)"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetServiceState method"
ms.assetid: 9e1ee42d-b388-4929-89c7-8741b956c3be
author: markingmyname
ms.author: maghan
manager: kfile
---
# SetServiceState Method (WMI MSReportServer_ConfigurationSetting)
  Turns the Report Server Windows and Web services on and off.  
  
## Syntax  
  
```vb  
Public Sub SetServiceState(ByVal EnableWindowsService As Boolean, _  
    ByVal EnableWebService As Boolean, ByVal EnableReportManager As Boolean, _  
    ByRef HRESULT As Int32)  
```  
  
```csharp  
public void SetSecureConnectionLevel(Boolean EnableWindowsService,  
    Boolean EnableWebService, Boolean EnableReportManager, out Int32 HRESULT);  
```  
  
## Parameters  
 *EnableWindowsService*  
 A `Boolean` value indicating the state of the Windows service. A value of `true` starts the Report Server Windows service; a value of `false` stops the Windows service.  
  
 *EnableWebService*  
 A `Boolean` value indicating the state of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Web service. A value of `true` starts the Report Server Web service; a value of `false` stops the Web service  
  
 *EnableReportManager*  
 A `Boolean` value indicating the desired state of the Report manager.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
