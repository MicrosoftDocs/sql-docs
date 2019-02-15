---
title: "GenerateDatabaseRightsScript Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: wmi-provider-library-reference


ms.topic: conceptual
apiname: 
  - "GenerateDatabaseRightsScript (WMI MSReportServer_ConfigurationSetting Class)"
apilocation: 
  - "reportingservices.mof"
apitype: MOFDef
helpviewer_keywords: 
  - "GenerateDatabaseRightsScript method"
ms.assetid: f2e6dcc9-978f-4c2c-bafe-36c330247fd0
author: markingmyname
ms.author: maghan
---
# ConfigurationSetting Method - GenerateDatabaseRightsScript
  Generates a SQL Script that can be used to grant a user rights to the report server database and other databases required for a report server to run. The caller is expected to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database server and execute the script.  
  
## Syntax  
  
```vb  
Public Sub GenerateDatabaseRightsScript(ByVal UserName As String, _  
    ByVal DatabaseName As String, ByVal IsRemote As Boolean, _  
    ByVal IsWindowsUser As Boolean, ByRef Script As String, _  
    ByRef HRESULT As Int32)  
```  
  
```csharp  
public void GenerateDatabaseRightsScript(string UserName, string DatabaseName, bool IsRemote, bool IsWindowsUser, out string Script,   
out Int32 HRESULT);  
```  
  
## Parameters  
 *UserName*  
 The user name or Windows security identifier (SID) of the user to which the script will grant rights.  
  
 *DatabaseName*  
 The database name to which the script will grant access to the user.  
  
 *IsRemote*  
 A Boolean value to indicating whether the database is remote from the report server.  
  
 *IsWindowsUser*  
 A Boolean value indicating whether the specified user name is a Windows user or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user.  
  
 *Script*  
 [out] A string containing the generated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] script.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
 If *DatabaseName* is empty then *IsRemote* is ignored and the report server configuration file value is used for the database name.  
  
 If *IsWindowsUser* is set to **true**, *UserName* should be in the format \<domain>\\<username\>.  
  
 When *IsWindowsUser* is set to **true**, the generated script grants login rights to the user for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], setting the report server database as the default database, and grants the **RSExec** role on the report server database, the report server temporary database, the master database and the MSDB system database.  
  
 When *IsWindowsUser* is set to **true**, the method accepts standard Windows SIDs as input. When a standard Windows SID or service account name is supplied, it is translated to a user name string. If the database is local, the account is translated to the correct localized representation of the account. If the database is remote, the account is represented as the computer's account.  
  
 The following table shows accounts that are translated and their remote representation.  
  
|Account / SID that is translated|Common Name|Remote Name|  
|---------------------------------------|-----------------|-----------------|  
|(S-1-5-18)|Local System|\<Domain>\\<ComputerName\>$|  
|.\LocalSystem|Local System|\<Domain>\\<ComputerName\>$|  
|ComputerName\LocalSystem|Local System|\<Domain>\\<ComputerName\>$|  
|LocalSystem|Local System|\<Domain>\\<ComputerName\>$|  
|(S-1-5-20)|Network Service|\<Domain>\\<ComputerName\>$|  
|NT AUTHORITY\NetworkService|Network Service|\<Domain>\\<ComputerName\>$|  
|(S-1-5-19)|Local Service|Error - see below.|  
|NT AUTHORITY\LocalService|Local Service|Error - see below.|  
  
 On [!INCLUDE[win2kfamily](../../includes/win2kfamily-md.md)], if you are using a built-in account and the report server database is remote, an error is returned.  
  
 If the **LocalService** built-in account is specified and the report server database is remote, an error is returned.  
  
 When *IsWindowsUser* is true and the value supplied in *UserName* needs to be translated, the WMI provider determines whether the report server database is located on the same computer or on a remote computer. To determine if the installation is local, the WMI provider evaluates the DatabaseServerName property against the following list of values. If a match is found, the database is local. Otherwise, it is remote. The comparison is case-insensitive.  
  
|Value of DatabaseServerName|Example|  
|---------------------------------|-------------|  
|"."||  
|"(local)"||  
|"LOCAL"||  
|localhost||  
|\<Machinename>|testlab14|  
|\<MachineFQDN>|example.redmond.microsoft.com|  
|\<IPAddress>|180.012.345,678|  
  
 When *IsWindowsUser* is set to **true**, the WMI provider calls LookupAccountName to get the SID for the account and then calls LookupAccountSID to get the name to put in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] script. This ensures that the account name used will pass [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] validation.  
  
 When *IsWindowsUser* is set to **false**, the generated script grants the **RSExec** role on the report server database, the report server temporary database, and the MSDB database.  
  
 When *IsWindowsUser* is set to **false**, the SQL Server user must already exist on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the script to run successfully.  
  
 If the report server does not have a report server database specified, calling GrantRightsToDatabaseUser returns an error.  
  
 The generated script supports [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005, and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
