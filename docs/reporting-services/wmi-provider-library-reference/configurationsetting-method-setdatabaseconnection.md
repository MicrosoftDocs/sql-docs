---
title: "SetDatabaseConnection Method (WMI MSReportServer_ConfigurationSetting) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: wmi-provider-library-reference


ms.topic: conceptual
apiname: 
  - "SetDatabaseConnection (WMI MSReportServer_ConfigurationSetting Class)"
apilocation: 
  - "reportingservices.mof"
apitype: MOFDef
helpviewer_keywords: 
  - "SetDatabaseConnection method"
ms.assetid: c040aa78-92b8-41e4-9ae2-eff9fcdddc5b
author: maggiesMSFT
ms.author: maggies
---
# ConfigurationSetting Method - SetDatabaseConnection
  Sets the report server database connection to a particular report server database.  
  
## Syntax  
  
```vb  
Public Sub SetDatabaseConnection(Server as String, _  
    DatabaseName as string, CredentialsType as Integer, _  
    Username as String, Password as String, ByRef HRESULT as Int32)  
```  
  
```csharp  
public void BackupEncryptionKey(string Server,   
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
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
 When the *CredentialsType* parameter is set to 0 (Windows), the *UserName* and *Password* parameters must be set. The *UserName* parameter must be in the form "domain\username", and the value must represent a valid Windows logon.  
  
 When the *CredentialsType* parameter is set to 1 ( [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]), the value passed in the *UserName* parameter must conform to the requirements of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name.  
  
 When the *CredentialsType* parameter is set to 2 (Windows Service), the report server uses integrated security to connect to the report server database and the *UserName* and *Password* parameters are ignored. The Reporting Server Web service will use either the [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] account or an application pool's account and the Windows service account to access the report server database.  
  
 When called, the SetDatabaseConnection method encrypts and stores the credentials and database information in the configuration file for the specified report server.  
  
 The SetDatabaseConnection method does not check that the report server can connect to the report server database using the data specified.  
  
 When set for the first time, the ConnectionPoolSize property is set based on the following processors: ConnectionPoolSize = #Processors * 75.  
  
 The SetDatabaseConnection method does not grant permissions to the specified account(s). You must call the [GenerateDatabaseRightsScript](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-generatedatabaserightsscript.md) method for each account that requires access to the report server database and run the resulting script.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
