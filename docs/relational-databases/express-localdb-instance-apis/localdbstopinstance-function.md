---
description: "LocalDBStopInstance Function"
title: "LocalDBStopInstance Function | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: "reference"
apiname: 
  - "LocalDBStopInstance"
apilocation: 
  - "sqluserinstance.dll"
apitype: "DLLExport"
ms.assetid: 4bd73187-0aac-4f03-ac54-2b78e41917e5
author: markingmyname
ms.author: maghan
---
# LocalDBStopInstance Function
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Stops the specified SQL Server Express LocalDB instance from running.  
  
 **Header file:** msoledbsql.h  
  
## Syntax  
  
```  
HRESULT LocalDBStopInstance(  
           PCWSTR pInstanceName,  
           DWORD dwFlags,   
           ULONG ulTimeout   
);  
```  
  
## Parameters  
 *pInstanceName*  
 [Input] The name of the LocalDB instance to stop.  
  
 *dwFlags*  
 [Input] One or a combination of the flag values specifying the way to stop the instance.  
  
 Available flags:  
  
 LOCALDB_SHUTDOWN_KILL_PROCESS  
 Shut down immediately using the kill process operating system command.  
  
 LOCALDB_SHUTDOWN_WITH_NOWAIT  
 Shut down using the WITH NOWAIT option Transact-SQL command.  
  
 If none of the flags is set, the LocalDB instance will be shut down using the SHUTDOWN Transact-SQL command. If both flags are set, the LOCALDB_SHUTDOWN_KILL_PROCESS flag takes precedence.  
  
 *ulTimeout*  
 [Input] The time in seconds to wait for this operation to complete. If this value is 0, this function will return immediately without waiting for the LocalDB instance to stop.  
  
## Returns  
 S_OK  
 The function succeeded.  
  
 [LOCALDB_ERROR_NOT_INSTALLED](../../relational-databases/express-localdb-error-messages/localdb-error-not-installed.md)  
 SQL Server Express LocalDB is not installed on the computer.  
  
 [LOCALDB_ERROR_INVALID_PARAMETER](../../relational-databases/express-localdb-error-messages/localdb-error-invalid-parameter.md)  
 One or more specified input parameters are invalid.  
  
 [LOCALDB_ERROR_INVALID_INSTANCE_NAME](../../relational-databases/express-localdb-error-messages/localdb-error-invalid-instance-name.md)  
 The specified instance name is invalid.  
  
 [LOCALDB_ERROR_UNKNOWN_INSTANCE](../../relational-databases/express-localdb-error-messages/localdb-error-unknown-instance.md)  
 The instance does not exist.  
  
 [LOCALDB_ERROR_WAIT_TIMEOUT](../../relational-databases/express-localdb-error-messages/localdb-error-wait-timeout.md)  
 A time-out occurred while trying to acquire the synchronization locks.  
  
 [LOCALDB_ERROR_INSTANCE_STOP_FAILED](../../relational-databases/express-localdb-error-messages/localdb-error-instance-stop-failed.md)  
 The stop operation failed to complete within the given time.  
  
 [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../../relational-databases/express-localdb-error-messages/localdb-error-instance-folder-path-too-long.md)  
 The path where the instance should be stored is longer than MAX_PATH.  
  
 [LOCALDB_ERROR_CANNOT_GET_USER_PROFILE_FOLDER](../../relational-databases/express-localdb-error-messages/localdb-error-cannot-get-user-profile-folder.md)  
 A user profile folder cannot be retrieved.  
  
 [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_FOLDER](../../relational-databases/express-localdb-error-messages/localdb-error-cannot-access-instance-folder.md)  
 An instance folder cannot be accessed.  
  
 [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../../relational-databases/express-localdb-error-messages/localdb-error-cannot-access-instance-registry.md)  
 An instance registry cannot be accessed.  
  
 [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../../relational-databases/express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md)  
 An instance configuration is corrupted.  
  
 [LOCALDB_ERROR_CALLER_IS_NOT_OWNER](../../relational-databases/express-localdb-error-messages/localdb-error-caller-is-not-owner.md)  
 API caller is not LocalDB instance owner.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../../relational-databases/express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../../relational-databases/sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](../../relational-databases/express-localdb-instance-apis/sql-server-express-localdb-header-and-version-information.md)  
  
  
