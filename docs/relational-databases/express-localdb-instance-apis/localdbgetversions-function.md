---
title: "LocalDBGetVersions Function | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
apiname: 
  - "LocalDBGetVersions"
apilocation: 
  - "sqluserinstance.dll"
apitype: "DLLExport"
ms.assetid: 033a9c6b-0d7f-4f8a-ab60-33cd6fee0d33
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# LocalDBGetVersions Function
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Returns all SQL Server Express LocalDB versions available on the computer.  
  
 **Header file:** sqlncli.h  
  
## Syntax  
  
```  
#define MAX_LOCALDB_VERSION_LENGTH 43typedef WCHAR TLocalDBVersion[MAX_LOCALDB_VERSION_LENGTH + 1];typedef TLocalDBVersion* PTLocalDBVersion;HRESULT LocalDBGetVersions(           PTLocalDBVersion pVersion,           LPDWORD lpdwNumberOfVersions);  
```  
  
## Parameters  
 *pVersionNames*  
 [Output] Contains names of the LocalDB versions that are available on the user's workstation.  
  
 *lpdwNumberOfVersions*  
 [Input/Output] On input holds the number of slots for versions in the *pVersionNames* buffer.   
On output, holds the number of existing LocalDB versions.  
  
## Returns  
 S_OK  
 The function succeeded.  
  
 [LOCALDB_ERROR_NOT_INSTALLED](../../relational-databases/express-localdb-error-messages/localdb-error-not-installed.md)  
 SQL Server Express LocalDB is not installed on the computer.  
  
 [LOCALDB_ERROR_INVALID_PARAMETER](../../relational-databases/express-localdb-error-messages/localdb-error-invalid-parameter.md)  
 One or more specified input parameters are invalid.  
  
 [LOCALDB_ERROR_INSUFFICIENT_BUFFER](../../relational-databases/express-localdb-error-messages/localdb-error-insufficient-buffer.md)  
 The input buffer is too short, and truncation was not requested.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../../relational-databases/express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../../relational-databases/sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](../../relational-databases/express-localdb-instance-apis/sql-server-express-localdb-header-and-version-information.md)  
  
  
