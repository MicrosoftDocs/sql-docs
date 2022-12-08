---
description: "LocalDBStartTracing Function"
title: "LocalDBStartTracing Function | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: "reference"
apiname: 
  - "LocalDBStartTracing"
apilocation: 
  - "sqluserinstance.dll"
apitype: "DLLExport"
ms.assetid: c7b83833-6d2a-4a06-9cb7-42767bed52c6
author: markingmyname
ms.author: maghan
---
# LocalDBStartTracing Function
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Enables tracing of API calls for all the SQL Server Express LocalDB instances owned by the current Windows user.  
  
 **Header file:** msoledbsql.h  
  
## Syntax  
  
```  
HRESULT LocalDBStartTracing();  
```  
  
## Returns  
 S_OK  
 The function succeeded.  
  
 [LOCALDB_ERROR_XEVENT_FAILED](../../relational-databases/express-localdb-error-messages/localdb-error-xevent-failed.md)  
 Failed to start XEvent engine within the LocalDB Instance API.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../../relational-databases/express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../../relational-databases/sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](../../relational-databases/express-localdb-instance-apis/sql-server-express-localdb-header-and-version-information.md)  
  
  
