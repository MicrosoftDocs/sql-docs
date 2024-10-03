---
title: "SetDatabaseConnection method (WMI MSReportServer_ConfigurationSetting)"
description: "SetDatabaseConnection method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "SetDatabaseConnection method"
apilocation: "reportingservices.mof"
apiname: "SetDatabaseConnection (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - SetDatabaseConnection
  Sets the report server database connection to a particular report server database.  
  
## Syntax  
  
```vb  
Public Sub SetDatabaseConnection(Server as String, _  
    DatabaseName as string, CredentialsType as Integer, _  
    Username as String, Password as String, ByRef HRESULT as Int32)  
```  
  
```csharp  
public void SetDatabaseConnection(string Server,   
    string DatabaseName, Int32 CredentialsType,   
    string UserName, string Password, out Int32 HRESULT);  
```  
  
## Parameters  
 *Server*  
 The name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that is used to host the report server database.  
  
 *DatabaseName*  
 The name of the report server database.  
  
 *CredentialsType*  
 The type of credentials to use for the connection. Values can be:  
  
-   0 - Windows  
  
-   1 - [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
-   2 - Windows Service  
  
 *UserName*  
 The account name used to connect to the report server database.  
  
 *Password*  
 The password used to connect to the report server database.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Remarks  
 When the *CredentialsType* parameter is set to 0 (Windows), the *UserName* and *Password* parameters must be set. The *UserName* parameter must be in the form `domain\username`, and the value must represent a valid Windows sign in.  
  
 When the *CredentialsType* parameter is set to 1 ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]), the value passed in the *UserName* parameter must conform to the requirements of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign in name.  
  
 When the *CredentialsType* parameter is set to 2 (Windows Service), the report server uses integrated security to connect to the report server database, and the *UserName* and *Password* parameters are ignored. The Reporting Server Web service uses either the [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] account or an application pool's account and the Windows service account to access the report server database.  
  
 When called, the *SetDatabaseConnection* method encrypts and stores the credentials and database information in the configuration file for the specified report server.  
  
 The *SetDatabaseConnection* method doesn't check that the report server can connect to the report server database using the data specified.  
  
 When set for the first time, the *ConnectionPoolSize* property is set based on the following processors: `ConnectionPoolSize = #Processors * 75`.  
  
 The *SetDatabaseConnection* method doesn't grant permissions to the specified account(s). You must call the [GenerateDatabaseRightsScript](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-generatedatabaserightsscript.md) method for each account that requires access to the report server database and run the resulting script.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
