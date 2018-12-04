---
title: "LocalDBCreateInstance Function | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
api_name: 
  - "LocalDBCreateInstance"
api_location: 
  - "sqluserinstance.dll"
topic_type: 
  - "apiref"
ms.assetid: 3eebb485-8a53-4a79-82a9-57b8de9f8e84
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# LocalDBCreateInstance Function
  Creates a new SQL Server Express LocalDB instance.  
  
 **Header file:** sqlncli.h  
  
## Syntax  
  
```  
HRESULT LocalDBCreateInstance(  
           PCWSTR wszVersion,  
           PCWSTR pInstanceName,   
           DWORD dwFlags   
);  
```  
  
## Parameters  
 *wszVersion*  
 [Input] The LocalDB version, for example 11.0 or 11.0.1094.2.  
  
 *pInstanceName*  
 [Input] The name for the LocalDB instance to create.  
  
 *dwFlags*  
 [Input] Reserved for future use. Currently should be set to 0.  
  
## Returns  
 S_OK  
 The function succeeded.  
  
 [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md)  
 SQL Server Express LocalDB is not installed on the computer.  
  
 [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md)  
 One or more specified input parameters are invalid.  
  
 [LOCALDB_ERROR_INVALID_INSTANCE_NAME](../express-localdb-error-messages/localdb-error-invalid-instance-name.md)  
 The specified instance name is invalid.  
  
 [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../express-localdb-error-messages/localdb-error-instance-folder-path-too-long.md)  
 The path where the instance should be stored is longer than MAX_PATH.  
  
 [LOCALDB_ERROR_INSTANCE_EXISTS_WITH_LOWER_VERSION](../express-localdb-error-messages/localdb-error-instance-exists-with-lower-version.md)  
 The specified instance already exists but its version is lower than requested.  
  
 [LOCALDB_ERROR_UNKNOWN_VERSION](../express-localdb-error-messages/localdb-error-unknown-version.md)  
 The specified version is not available.  
  
 [LOCALDB_ERROR_VERSION_REQUESTED_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-version-requested-not-installed.md)  
 The specified patch level is not installed.  
  
 [LOCALDB_ERROR_CANNOT_CREATE_INSTANCE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-create-instance-folder.md)  
 A folder cannot be created under %userprofile%.  
  
 [LOCALDB_ERROR_CANNOT_GET_USER_PROFILE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-get-user-profile-folder.md)  
 A user profile folder cannot be retrieved.  
  
 [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-access-instance-folder.md)  
 An instance folder cannot be accessed.  
  
 [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-access-instance-registry.md)  
 An instance registry cannot be accessed.  
  
 [LOCALDB_ERROR_CANNOT_MODIFY_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-modify-instance-registry.md)  
 An instance registry cannot be modified.  
  
 [LOCALDB_ERROR_SQL_SERVER_STARTUP_FAILED](../express-localdb-error-messages/localdb-error-sql-server-startup-failed.md)  
 A SQL Server process is started but SQL Server startup failed.  
  
 [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md)  
 An instance configuration is corrupted.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 If a fully functional LocalDB instance with the specified name already exists and its version is equal to or higher than requested, the result is S_OK.  
  
 In cases when an existing instance becomes corrupted, subsequent calls to the `LocalDBCreateInstance` API method will fail. Corrupted instances must be fixed manually or explicitly deleted before they can be used again.  
  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](sql-server-express-localdb-header-and-version-information.md)  
  
  
