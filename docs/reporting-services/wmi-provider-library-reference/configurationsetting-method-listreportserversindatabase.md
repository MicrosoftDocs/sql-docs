---
title: "ListReportServersInDatabase method (WMI MSReportServer_ConfigurationSetting)"
description: "ListReportServersInDatabase method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "ListReportServersInDatabase method"
apilocation: "reportingservices.mof"
apiname: "ListReportServersInDatabase (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - ListReportServersInDatabase
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
 [out] A string array containing other errors returned by the call.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Remarks  
 *ListReportServersInDatabase* lists the report server installations that are present in the report server database, regardless of whether they have access to secure information. It returns a matched set of arrays containing information about each installation.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
