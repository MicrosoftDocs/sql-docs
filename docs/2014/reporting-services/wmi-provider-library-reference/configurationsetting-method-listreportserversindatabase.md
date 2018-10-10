---
title: "ListReportServersInDatabase Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
api_name: 
  - "ListReportServersInDatabase (WMI MSReportServer_ConfigurationSetting Class)"
api_location: 
  - "reportingservices.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ListReportServersInDatabase method"
ms.assetid: a4bf5968-c46f-484f-a510-65e2dde65a0d
author: markingmyname
ms.author: maghan
manager: craigg
---
# ListReportServersInDatabase Method (WMI MSReportServer_ConfigurationSetting)
  Returns the list of report server installations that are present in the report server database, regardless of whether they have access to secure information.  
  
## Syntax  
  
```vb  
Public Sub ListReportServersInDatabase(ByRef MachineNames() As String, _  
    ByRef InstanceNames() As String, ByRef InstallationIDs() As String, _  
    ByRef IsInitialized() As Boolean, ByRef Length As Int32, _  
    ByRef HRESULT As Int32, ByRef ExtendedErrors() As String)  
```  
  
```csharp  
public void ListReportServersInDatabase (out string[] MachineNames,   
    out string[] InstanceNames, out string[] InstallationIDs,   
    out Boolean[] IsInitialized,out Int32 Length, out Int32 HRESULT,    
    out string[] ExtendedErrors);  
```  
  
## Parameters  
 *MachineNames[]*  
 [out] An array containing the machine names for the report server installations in the database.  
  
 *InstanceNames[]*  
 [out] An array containing the instance names of each of the report server installations in the database.  
  
 *InstallationIDs[]*  
 [out] An array containing the installation IDs of each report server installation in the database.  
  
 *IsInitialized[]*  
 [out] An array containing initialization status for each report server installation in the database.  
  
 *Length*  
 [out] The length of the arrays returned by the method. All returned arrays have the same length.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
 *ExtendedErrors[]*  
 [out] A string array containing additional errors returned by the call.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
 ListReportServersInDatabase lists the report server installations that are present in the report server database, regardless of whether they have access to secure information, and returns a matched set of arrays containing information about each installation.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](msreportserver-configurationsetting-members.md)  
  
  
