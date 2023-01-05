---
title: "Cursors (Transact-SQL)"
description: "Cursors (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
helpviewer_keywords:
  - "statements [SQL Server], cursors"
  - "functions [SQL Server], cursors"
  - "cursors [SQL Server], statements"
dev_langs:
  - "TSQL"
---
# Cursors (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statements produce a complete result set, but there are times when the results are best processed one row at a time. Opening a cursor on a result set allows processing the result set one row at a time. You can assign a cursor to a variable or parameter with a **cursor** data type.  
  
 Cursor operations are supported on these statements:  
  
 [CLOSE](../../t-sql/language-elements/close-transact-sql.md)  
  
 [CREATE PROCEDURE](../../t-sql/statements/create-procedure-transact-sql.md)  
  
 [DEALLOCATE](../../t-sql/language-elements/deallocate-transact-sql.md)  
  
 [DECLARE CURSOR](../../t-sql/language-elements/declare-cursor-transact-sql.md)  
  
 [DECLARE @local_variable](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
  
 [DELETE](../../t-sql/statements/delete-transact-sql.md)  
  
 [FETCH](../../t-sql/language-elements/fetch-transact-sql.md)  
  
 [OPEN](../../t-sql/language-elements/open-transact-sql.md)  
  
 [UPDATE](../../t-sql/queries/update-transact-sql.md)  
  
 [SET](../../t-sql/statements/set-statements-transact-sql.md)  
  
 These system functions and system stored procedures also support cursors:  
  
 [@@CURSOR_ROWS](../../t-sql/functions/cursor-rows-transact-sql.md)  
  
 [CURSOR_STATUS](../../t-sql/functions/cursor-status-transact-sql.md)  
  
 [@@FETCH_STATUS](../../t-sql/functions/fetch-status-transact-sql.md)  
  
 [sp_cursor_list](../../relational-databases/system-stored-procedures/sp-cursor-list-transact-sql.md)  
  
 [sp_describe_cursor](../../relational-databases/system-stored-procedures/sp-describe-cursor-transact-sql.md)  
  
 [sp_describe_cursor_columns](../../relational-databases/system-stored-procedures/sp-describe-cursor-columns-transact-sql.md)  
  
 [sp_describe_cursor_tables](../../relational-databases/system-stored-procedures/sp-describe-cursor-tables-transact-sql.md)  
  
## See Also  
 [Cursors](../../relational-databases/cursors.md)  
  
  
