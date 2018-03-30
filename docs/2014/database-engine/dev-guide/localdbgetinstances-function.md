---
title: "LocalDBGetInstances Function | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "LocalDBGetInstances"
api_location: 
  - "sqluserinstance.dll"
topic_type: 
  - "apiref"
ms.assetid: f95a9980-8bc0-426c-8aa1-e2660b6784cf
caps.latest.revision: 13
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# LocalDBGetInstances Function
  Returns all SQL Server Express LocalDB instances with the given version.  
  
 **Header file:** sqlncli.h  
  
## Syntax  
  
```  
#define MAX_LOCALDB_INSTANCE_NAME_LENGTH 128typedef WCHAR TLocalDBInstanceName[MAX_LOCALDB_INSTANCE_NAME_LENGTH + 1];typedef TLocalDBInstanceName* PTLocalDBInstanceName;  
HRESULT LocalDBGetInstances(  
           PTLocalDBInstanceName pInstanceNames,  
           LPDWORD lpdwNumberOfInstances  
);  
```  
  
## Parameters  
 *pInstanceNames*  
 [Output] When this function returns, contains the names of both named and default LocalDB instances on the user’s workstation.  
  
 *lpdwNumberOfInstances*  
 [Input/Output] On input, contains the number of slots for instance names in the *pInstanceNames* buffer. On output, contains the number of LocalDB instances found on the user’s workstation.  
  
## Returns  
 S_OK  
 The function succeeded.  
  
 [LOCALDB_ERROR_NOT_INSTALLED](../../../2014/database-engine/dev-guide/localdb-error-not-installed.md)  
 SQL Server Express LocalDB is not installed on the computer.  
  
 [LOCALDB_ERROR_INVALID_PARAMETER](../../../2014/database-engine/dev-guide/localdb-error-invalid-parameter.md)  
 One or more specified input parameters are invalid.  
  
 [LOCALDB_ERROR_INSUFFICIENT_BUFFER](../../../2014/database-engine/dev-guide/localdb-error-insufficient-buffer.md)  
 The input buffer is too short, and truncation was not requested.  
  
 [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../../../2014/database-engine/dev-guide/localdb-error-instance-folder-path-too-long.md)  
 The path where the instance should be stored is longer than MAX_PATH.  
  
 [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../../../2014/database-engine/dev-guide/localdb-error-cannot-access-instance-registry.md)  
 An instance registry cannot be accessed.  
  
 [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../../../2014/database-engine/dev-guide/localdb-error-instance-configuration-corrupt.md)  
 An instance configuration is corrupted.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../../../2014/database-engine/dev-guide/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../../../2014/database-engine/dev-guide/sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](../../../2014/database-engine/dev-guide/sql-server-express-localdb-header-and-version-information.md)  
  
  