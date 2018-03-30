---
title: "LocalDBUnshareInstance Function | Microsoft Docs"
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
  - "LocalDBUnshareInstance"
api_location: 
  - "sqluserinstance.dll"
topic_type: 
  - "apiref"
ms.assetid: 54012ccb-eded-43f7-8ea5-da5ce79224c6
caps.latest.revision: 9
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# LocalDBUnshareInstance Function
  Stops the sharing of the specified SQL Server Express LocalDB instance.  
  
 **Header file:** sqlncli.h  
  
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
  
 [LOCALDB_ERROR_NOT_INSTALLED](../../../2014/database-engine/dev-guide/localdb-error-not-installed.md)  
 SQL Server Express LocalDB is not installed on the computer.  
  
 [LOCALDB_ERROR_INVALID_PARAMETER](../../../2014/database-engine/dev-guide/localdb-error-invalid-parameter.md)  
 One or more specified input parameters are invalid.  
  
 [LOCALDB_ERROR_INVALID_INSTANCE_NAME](../../../2014/database-engine/dev-guide/localdb-error-invalid-instance-name.md)  
 The specified instance name is invalid.  
  
 [LOCALDB_ERROR_UNKNOWN_INSTANCE](../../../2014/database-engine/dev-guide/localdb-error-unknown-instance.md)  
 The specified instance does not exist.  
  
 [LOCALDB_ERROR_ADMIN_RIGHTS_REQUIRED](../../../2014/database-engine/dev-guide/localdb-error-admin-rights-required.md)  
 Administrator privileges are required in order to execute this operation.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../../../2014/database-engine/dev-guide/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../../../2014/database-engine/dev-guide/sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](../../../2014/database-engine/dev-guide/sql-server-express-localdb-header-and-version-information.md)  
  
  