---
title: "CHANGE_TRACKING_CURRENT_VERSION (Transact-SQL)"
description: "CHANGE_TRACKING_CURRENT_VERSION (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/08/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "CHANGE_TRACKING_CURRENT_VERSION_TSQL"
  - "CHANGE_TRACKING_CURRENT_VERSION"
helpviewer_keywords:
  - "change tracking [SQL Server], CHANGE_TRACKING_CURRENT_VERSION"
  - "CHANGE_TRACKING_CURRENT_VERSION"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# CHANGE_TRACKING_CURRENT_VERSION (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns a version that is associated with the last committed transaction. This version can be used when you enumerate changes by using [CHANGETABLE](../../relational-databases/system-functions/changetable-transact-sql.md).  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CHANGE_TRACKING_CURRENT_VERSION ( )  
```  
  
## Return Type  
 **bigint**  
  
## Remarks  
 Returns NULL when change tracking is not enabled for the database.  
  
## Examples  
 The following example declares the local variable `@next_baseline` for storing the current version of tracked changes, and then uses the `CHANGE_TRACKING_CURRENT_VERSION()` function to obtain the value for the variable.  
  
```sql  
DECLARE @next_baseline bigint;  
SET @next_baseline = CHANGE_TRACKING_CURRENT_VERSION();  
```  
  
## Related content

- [Change Tracking Functions (Transact-SQL)](change-tracking-functions-transact-sql.md)
- [CHANGETABLE (Transact-SQL)](changetable-transact-sql.md)
- [CHANGE_TRACKING_MIN_VALID_VERSION (Transact-SQL)](change-tracking-min-valid-version-transact-sql.md)
- [Track data changes (SQL Server)](../track-changes/track-data-changes-sql-server.md)
