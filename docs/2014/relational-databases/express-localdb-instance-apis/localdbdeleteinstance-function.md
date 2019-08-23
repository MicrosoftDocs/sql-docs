---
title: "LocalDBDeleteInstance Function | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
api_name: 
  - "LocalDBDeleteInstance"
api_location: 
  - "sqluserinstance.dll"
topic_type: 
  - "apiref"
ms.assetid: 37cb2a7e-672a-4223-b6f3-a94d7b8d58cd
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# LocalDBDeleteInstance Function
  Removes the specified SQL Server Express LocalDB instance.  
  
 **Header file:** sqlncli.h  
  
## Syntax  
  
```  
HRESULT LocalDBDeleteInstance(  
           PCWSTR pInstanceName,  
           DWORD dwFlags   
);  
```  
  
## Parameters  
 *pInstanceName*  
 [Input] The name of the LocalDB instance to remove.  
  
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
  
 [LOCALDB_ERROR_UNKNOWN_INSTANCE](../express-localdb-error-messages/localdb-error-unknown-instance.md)  
 The specified instance does not exist.  
  
 [LOCALDB_ERROR_INSTANCE_BUSY](../express-localdb-error-messages/localdb-error-instance-busy.md)  
 The specified instance is running.  
  
 [LOCALDB_ERROR_WAIT_TIMEOUT](../express-localdb-error-messages/localdb-error-wait-timeout.md)  
 A time-out occurred while trying to acquire synchronization locks.  
  
 [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../express-localdb-error-messages/localdb-error-instance-folder-path-too-long.md)  
 The path where the instance should be stored is longer than MAX_PATH.  
  
 [LOCALDB_ERROR_CANNOT_GET_USER_PROFILE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-get-user-profile-folder.md)  
 A user profile folder cannot be retrieved.  
  
 [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-access-instance-folder.md)  
 An instance folder cannot be accessed.  
  
 [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-access-instance-registry.md)  
 An instance registry cannot be accessed.  
  
 [LOCALDB_ERROR_CANNOT_MODIFY_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-modify-instance-registry.md)  
 An instance registry cannot be modified.  
  
 [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md)  
 An instance configuration is corrupted.  
  
 [LOCALDB_ERROR_CALLER_IS_NOT_OWNER](../express-localdb-error-messages/localdb-error-caller-is-not-owner.md)  
 API caller is not Local Database instance owner.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](sql-server-express-localdb-header-and-version-information.md)  
  
  
