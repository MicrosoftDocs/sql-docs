---
title: "SET FMTONLY (Transact-SQL)"
description: SET FMTONLY (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "06/03/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FMTONLY_TSQL"
  - "FMTONLY"
  - "SET FMTONLY"
  - "SET_FMTONLY_TSQL"
helpviewer_keywords:
  - "metadata [SQL Server], only metadata returned"
  - "SET FMTONLY statement"
  - "FMTONLY option"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET FMTONLY (Transact-SQL)

[!INCLUDE[sql-asdb-asa-pdw](../../includes/applies-to-version/sql-asdb-asa-pdw.md)]

  Returns only metadata to the client. Can be used to test the format of the response without actually running the query.  

> [!NOTE]
> Do not use this feature. This feature has been replaced by the following items:
>
> - [sp_describe_first_result_set (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md)
> - [sp_describe_undeclared_parameters (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md)
> - [sys.dm_exec_describe_first_result_set (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md)
> - [sys.dm_exec_describe_first_result_set_for_object (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md)

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SET FMTONLY { ON | OFF }   
```  

## Remarks

When `FMTONLY` is `ON`, a rowset is returned with the column names, but without any data rows.

`SET FMTONLY ON` has no effect when the Transact-SQL batch is parsed. The effect occurs during execution run time.

The default value is `OFF`.

## Permissions  
 Requires membership in the public role.  

## Examples

The following Transact-SQL code example sets `FMTONLY` to `ON`. This setting causes SQL Server to return only metadata information about the selected columns. Specifically, the column names are returned. No data rows are returned.

In the example, the test execution of stored procedure `prc_gm29` returns the following:

- Multiple rowsets.
- Columns from multiple tables, in one of its `SELECT` statements.

<!--
Issue 2246 inspired this code example, and the replacement of the two pre-existing examples. 2019/June/03, GM.
-->

```sql
SET NOCOUNT ON;
GO

DROP PROCEDURE IF EXISTS prc_gm29;

DROP TABLE IF EXISTS #tabTemp41;
DROP TABLE IF EXISTS #tabTemp42;
GO

CREATE TABLE #tabTemp41
(
   KeyInt41        INT           NOT NULL,
   Name41          NVARCHAR(16)  NOT NULL,
   TargetDateTime  DATETIME      NOT NULL  DEFAULT GetDate()
);

CREATE TABLE #tabTemp42
(
   KeyInt42 INT          NOT NULL,   -- JOIN-able to KeyInt41.
   Name42   NVARCHAR(16) NOT NULL
);
GO

INSERT INTO #tabTemp41 (KeyInt41, Name41) VALUES (10, 't41-c');
INSERT INTO #tabTemp42 (KeyInt42, Name42) VALUES (10, 't42-p');
GO

CREATE PROCEDURE prc_gm29
AS
BEGIN
SELECT * FROM #tabTemp41;
SELECT * FROM #tabTemp42;

SELECT t41.KeyInt41, t41.TargetDateTime, t41.Name41, t42.Name42
   FROM
                 #tabTemp41 AS t41
      INNER JOIN #tabTemp42 AS t42 on t42.KeyInt42 = t41.KeyInt41
END;
GO

SET DATEFORMAT mdy;

SET FMTONLY ON;
EXECUTE prc_gm29;   -- Returns multiple tables.
SET FMTONLY OFF;
GO

DROP PROCEDURE IF EXISTS prc_gm29;

DROP TABLE IF EXISTS #tabTemp41;
DROP TABLE IF EXISTS #tabTemp42;
GO

/****  Actual Output:
[C:\JunkM\]
>> osql.exe -S myazuresqldb.database.windows.net -U somebody -P secret -d MyDatabase -i C:\JunkM\Issue-2246-a.SQL 

 KeyInt41    Name41           TargetDateTime
 ----------- ---------------- -----------------------

 KeyInt42    Name42
 ----------- ----------------

 KeyInt41    TargetDateTime          Name41           Name42
 ----------- ----------------------- ---------------- ----------------


[C:\JunkM\]
>>
****/
```

## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  

