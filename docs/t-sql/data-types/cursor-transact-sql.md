---
title: "cursor (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/23/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "cursor data type"
ms.assetid: fbea16ef-f2cc-4734-9149-ec2598fd3cca
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# cursor (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

A data type for variables or stored procedure OUTPUT parameters that contain a reference to a cursor.
  
## Remarks  
The operations that can reference variables and parameters having a **cursor** data type are:
-   The DECLARE *@local_variable* and SET *@local_variable* statements.  
-   The OPEN, FETCH, CLOSE, and DEALLOCATE cursor statements.  
-   Stored procedure output parameters.  
-   The CURSOR_STATUS function.  
-   The **sp_cursor_list**, **sp_describe_cursor**, **sp_describe_cursor_tables**, and **sp_describe_cursor_columns** system stored procedures.  
  
The **cursor_name** output column of **sp_cursor_list** and **sp_describe_cursor** returns the name of the cursor variable.
  
Any variables created with the **cursor** data type are nullable.
  
The **cursor** data type cannot be used for a column in a CREATE TABLE statement.
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[CURSOR_STATUS &#40;Transact-SQL&#41;](../../t-sql/functions/cursor-status-transact-sql.md)  
[Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[DECLARE CURSOR &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-cursor-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)
  
  
