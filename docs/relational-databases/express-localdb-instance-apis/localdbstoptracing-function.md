---
title: "LocalDBStopTracing Function | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
apiname: 
  - "LocalDBStopTracing"
apilocation: 
  - "sqluserinstance.dll"
apitype: "DLLExport"
ms.assetid: 1d50e040-8602-4ffa-be8f-b8633fdfa7ff
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# LocalDBStopTracing Function
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Disables tracing of API calls for all the SQL Server Express LocalDB instances owned by the current Windows user.  
  
 **Header file:** sqlncli.h  
  
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
  
  
