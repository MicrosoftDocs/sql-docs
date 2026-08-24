---
title: "cursor (Transact-SQL)"
description: "cursor (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/23/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "cursor data type"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# cursor (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

A data type for variables or stored procedure OUTPUT parameters that contain a reference to a cursor.
  
## Remarks  
The operations that can reference variables and parameters having a **cursor** data type are:
-   The DECLARE *\@local_variable* and SET *\@local_variable* statements.  
-   The OPEN, FETCH, CLOSE, and DEALLOCATE cursor statements.  
-   Stored procedure output parameters.  
-   The CURSOR_STATUS function.  
-   The **sp_cursor_list**, **sp_describe_cursor**, **sp_describe_cursor_tables**, and **sp_describe_cursor_columns** system stored procedures.  
  
The **cursor_name** output column of **sp_cursor_list** and **sp_describe_cursor** returns the name of the cursor variable.
  
Any variables created with the **cursor** data type are nullable.
  
The **cursor** data type cannot be used for a column in a CREATE TABLE statement.
  
## Related content

- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
- [CURSOR_STATUS (Transact-SQL)](../functions/cursor-status-transact-sql.md)
- [Data type conversion (Database Engine)](data-type-conversion-database-engine.md)
- [Data types (Transact-SQL)](data-types-transact-sql.md)
- [DECLARE CURSOR (Transact-SQL)](../language-elements/declare-cursor-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](../language-elements/declare-local-variable-transact-sql.md)
- [SET @local_variable (Transact-SQL)](../language-elements/set-local-variable-transact-sql.md)
