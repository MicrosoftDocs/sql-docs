---
title: "LocalDBUnshareInstance Function"
description: "LocalDBUnshareInstance Function"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBUnshareInstance"
apitype: "DLLExport"
---
# LocalDBUnshareInstance Function
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Stops the sharing of the specified SQL Server Express LocalDB instance.  
  
 **Header file:** msoledbsql.h  
  
## Syntax  
  
```  
HRESULT LocalDBUnShareInstance(  
           PCWSTR pInstanceSharedName,   
           DWORD dwFlags   
);  
```  
  
## Parameters  
 *pInstanceSharedName*  
 [Input] The shared name for the LocalDB instance to unshare.  
  
 *dwFlags*  
 [Input] Reserved for future use. Currently should be set to 0.  
  
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
 The specified instance does not exist.  
  
 [LOCALDB_ERROR_ADMIN_RIGHTS_REQUIRED](../../relational-databases/express-localdb-error-messages/localdb-error-admin-rights-required.md)  
 Administrator privileges are required in order to execute this operation.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../../relational-databases/express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../../relational-databases/sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](../../relational-databases/express-localdb-instance-apis/sql-server-express-localdb-header-and-version-information.md)  
  
  
