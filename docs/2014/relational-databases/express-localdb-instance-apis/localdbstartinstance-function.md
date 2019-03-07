---
title: "LocalDBStartInstance Function | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
api_name: 
  - "LocalDBStartInstance"
api_location: 
  - "sqluserinstance.dll"
topic_type: 
  - "apiref"
ms.assetid: cb325f5d-10ee-4a56-ba28-db0074ab3926
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# LocalDBStartInstance Function
  Starts the specified SQL Server Express LocalDB instance.  
  
 **Header file:** sqlncli.h  
  
## Syntax  
  
```  
HRESULT LocalDBStartInstance(  
           PCWSTR pInstanceName,  
           DWORD dwFlags,   
           LPWSTR wszSqlConnection,   
           LPDWORD lpcchSqlConnection   
);  
```  
  
## Parameters  
 *pInstanceName*  
 [Input] The name of the LocalDB instance to start.  
  
 *dwFlags*  
 [Input] Reserved for future use. Currently should be set to 0.  
  
 *wszSqlConnection*  
 [Output] The buffer to store the connection string to the LocalDB instance.  
  
 *lpcchSqlConnection*  
 [Input/Output] On input contains the size of the *wszSqlConnection* buffer in characters, including any trailing nulls. On output, if the given buffer size is too small, contains the required buffer size in characters, including any trailing nulls.  
  
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
 The instance does not exist.  
  
 [LOCALDB_ERROR_INSUFFICIENT_BUFFER](../express-localdb-error-messages/localdb-error-insufficient-buffer.md)  
 The specified buffer *wszSqlConnection* is too small.  
  
 [LOCALDB_ERROR_WAIT_TIMEOUT](../express-localdb-error-messages/localdb-error-wait-timeout.md)  
 A time-out occurred while trying to acquire the synchronization locks.  
  
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
  
 [LOCALDB_ERROR_CANNOT_CREATE_SQL_PROCESS](../express-localdb-error-messages/localdb-error-cannot-create-sql-process.md)  
 A process for SQL Server cannot be created.  
  
 [LOCALDB_ERROR_SQL_SERVER_STARTUP_FAILED](../express-localdb-error-messages/localdb-error-sql-server-startup-failed.md)  
 A SQL Server process was started, but SQL Server startup failed.  
  
 [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md)  
 An instance configuration was corrupted.  
  
 [LOCALDB_ERROR_AUTO_INSTANCE_CREATE_FAILED](../express-localdb-error-messages/localdb-error-auto-instance-create-failed.md)  
 Cannot create an automatic instance. See the Windows Application event log for error details.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Details  
 Both the connection buffer argument (*wszSqlConnection*) and the connection buffer size argument (*lpcchSqlConnection*) are optional. The following table shows options for using these arguments and their results.  
  
|Buffer|Buffer size|Rationale|Action|  
|------------|-----------------|---------------|------------|  
|NULL|NULL|User wants to start the instance and doesn't need a pipe name.|Starts an instance (no pipe return and no required buffer size return).|  
|NULL|Present|User asks for the output buffer size. (In the next call the user will probably ask for an actual start.)|Returns a required buffer size (no start and no pipe return). Result is S_OK.|  
|Present|NULL|Not allowed; incorrect input.|Returned result is LOCALDB_ERROR_INVALID_PARAMETER.|  
|Present|Present|User wants to start the instance and needs the pipe name to connect to it after it is started.|Checks the buffer size, starts the instance, and returns the pipe name in the buffer. <br />The buffer size argument returns the length of the "server=" string, not including terminating nulls.|  
  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](sql-server-express-localdb-header-and-version-information.md)  
  
  
