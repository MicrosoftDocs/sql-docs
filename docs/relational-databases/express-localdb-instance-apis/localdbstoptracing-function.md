---
description: "LocalDBStopTracing Function"
title: "LocalDBStopTracing Function | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: "reference"
apiname: 
  - "LocalDBStopTracing"
apilocation: 
  - "sqluserinstance.dll"
apitype: "DLLExport"
ms.assetid: 1d50e040-8602-4ffa-be8f-b8633fdfa7ff
author: markingmyname
ms.author: maghan
---
# LocalDBStopTracing Function
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Disables tracing of API calls for all the SQL Server Express LocalDB instances owned by the current Windows user.  
  
 **Header file:** msoledbsql.h  
  
## Syntax  
  
```  
HRESULT LocalDBStopTracing();  
```  
  
## Returns  
 S_OK  
 The function succeeded.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../../relational-databases/express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../../relational-databases/sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](../../relational-databases/express-localdb-instance-apis/sql-server-express-localdb-header-and-version-information.md)  
  
  
