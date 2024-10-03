---
title: "SET QUOTED_IDENTIFIER (Transact-SQL)"
description: SET QUOTED_IDENTIFIER causes the Database Engine to follow the ISO rules regarding quotation mark delimiting identifiers and literal strings.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 08/09/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "QUOTED_IDENTIFIER_TSQL"
  - "SET_QUOTED_IDENTIFIER_TSQL"
  - "SET QUOTED_IDENTIFIER"
  - "QUOTED_IDENTIFIER"
helpviewer_keywords:
  - "delimited identifiers [SQL Server]"
  - "identifiers [SQL Server], delimited"
  - "QUOTED_IDENTIFIER option"
  - "quoted identifiers"
  - "ISO delimited identifiers rules"
  - "SET QUOTED_IDENTIFIER statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# SET QUOTED_IDENTIFIER (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Causes [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to follow the ISO rules regarding quotation mark delimiting identifiers and literal strings. Identifiers delimited by double quotation marks can be either [!INCLUDE [tsql](../../includes/tsql-md.md)] reserved keywords or can contain characters not generally allowed by the [!INCLUDE [tsql](../../includes/tsql-md.md)] syntax rules for identifiers.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, serverless SQL pool in Azure Synapse Analytics, and Microsoft Fabric.

```syntaxsql
SET QUOTED_IDENTIFIER { ON | OFF }
```

Syntax for Azure Synapse Analytics and Parallel Data Warehouse.

```syntaxsql
SET QUOTED_IDENTIFIER ON
```

## Remarks

When `SET QUOTED_IDENTIFIER` is `ON` (default), identifiers can be delimited by double quotation marks (`""`), and literals must be delimited by single quotation marks (`''`). All strings delimited by double quotation marks are interpreted as object identifiers. Quoted identifiers don't have to follow the [!INCLUDE [tsql](../../includes/tsql-md.md)] rules for identifiers. They can be keywords and can include characters that aren't allowed in [!INCLUDE [tsql](../../includes/tsql-md.md)] identifiers. If a double quotation mark (`"`) is part of the identifier, it can be represented by two double quotation marks (`""`). `SET QUOTED_IDENTIFIER` must be `ON` when reserved keywords are used for object names in the database.

When `SET QUOTED_IDENTIFIER` is `OFF`, identifiers can't be quoted and must follow all [!INCLUDE [tsql](../../includes/tsql-md.md)] rules for identifiers. For more information, see [Database identifiers](../../relational-databases/databases/database-identifiers.md). Literals can be delimited by either single or double quotation marks. If a literal string is delimited by double quotation marks, the string can contain embedded single quotation marks, such as apostrophes.

> [!NOTE]  
> `QUOTED_IDENTIFIER` doesn't affect delimited identifiers enclosed in brackets (`[` and `]`).

`SET QUOTED_IDENTIFIER` must be `ON` when you're creating or changing indexes on computed columns or indexed views. If `SET QUOTED_IDENTIFIER` is `OFF`, then `CREATE`, `UPDATE`, `INSERT`, and `DELETE` statements fail on tables with indexes on computed columns, or tables with indexed views. For more information about required `SET` option settings with indexed views and indexes on computed columns, see [Considerations when you use the SET statements](../../t-sql/statements/set-statements-transact-sql.md#considerations-when-you-use-the-set-statements).

`SET QUOTED_IDENTIFIER` must be `ON` when you're creating a filtered index.

`SET QUOTED_IDENTIFIER` must be `ON` when you invoke **xml** data type methods.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically set `QUOTED_IDENTIFIER` to `ON` when connecting. This can be configured in ODBC data sources, in ODBC connection attributes, or OLE DB connection properties. The default for `SET QUOTED_IDENTIFIER` is `OFF` for connections from DB-Library applications.

When a table is created, the `QUOTED IDENTIFIER` option is always stored as `ON` in the table's metadata even if the option is set to `OFF` when the table is created.

When a stored procedure is created, the `SET QUOTED_IDENTIFIER` and `SET ANSI_NULLS` settings are captured and used for subsequent invocations of that stored procedure.

When executed inside a stored procedure, the setting of `SET QUOTED_IDENTIFIER` isn't changed.

When `SET ANSI_DEFAULTS` is `ON`, `QUOTED_IDENTIFIER` is also `ON`.

`SET QUOTED_IDENTIFIER` corresponds to the `QUOTED_IDENTIFIER` setting of [ALTER DATABASE](alter-database-transact-sql.md).

`SET QUOTED_IDENTIFIER` takes effect at [!INCLUDE [tsql](../../includes/tsql-md.md)] parse-time and only affects parsing, not query optimization or query execution.

For a top-level ad hoc batch, parsing begins using the session's current setting for `QUOTED_IDENTIFIER`. As the batch is parsed any occurrence of `SET QUOTED_IDENTIFIER` changes the parsing behavior from that point on, and save that setting for the session. So after the batch is parsed and executed, the session's `QUOTED_IDENTIFIER` setting will be set according to the last occurrence of `SET QUOTED_IDENTIFIER` in the batch.

Static [!INCLUDE [tsql](../../includes/tsql-md.md)] in a stored procedure is parsed using the `QUOTED_IDENTIFIER` setting in effect for the batch that created or altered the stored procedure. `SET QUOTED_IDENTIFIER` has no effect when it appears in the body of a stored procedure as static [!INCLUDE [tsql](../../includes/tsql-md.md)].

For a nested batch using [sp_executesql](../../relational-databases/system-stored-procedures/sp-executesql-transact-sql.md) or `exec()`, the parsing begins using the `QUOTED_IDENTIFIER` setting of the session. If the nested batch is inside a stored procedure, parsing starts using the `QUOTED_IDENTIFIER` setting of the stored procedure. As the nested batch is parsed, any occurrence of `SET QUOTED_IDENTIFIER` changes the parsing behavior from that point on, but the session's `QUOTED_IDENTIFIER` setting isn't updated.

To view the current setting for this setting, run the following query:

```sql
DECLARE @QUOTED_IDENTIFIER VARCHAR(3) = 'OFF';

IF ((256 & @@OPTIONS) = 256)
BEGIN
    SET @QUOTED_IDENTIFIER = 'ON';
END

SELECT @QUOTED_IDENTIFIER AS QUOTED_IDENTIFIER;
```

## Permissions

Requires membership in the **public** role.

## Examples

### A. Use the quoted identifier setting and reserved word object names

The following example shows that the `SET QUOTED_IDENTIFIER` setting must be `ON`, and the keywords in table names must be in double quotation marks to create and use objects that have reserved keyword names.

```sql
SET QUOTED_IDENTIFIER OFF
GO

-- Create statement fails.
CREATE TABLE "select" ("identity" INT IDENTITY NOT NULL, "order" INT NOT NULL);
GO

SET QUOTED_IDENTIFIER ON;
GO

-- Create statement succeeds.
CREATE TABLE "select" ("identity" INT IDENTITY NOT NULL, "order" INT NOT NULL);
GO

SELECT "identity","order"
FROM "select"
ORDER BY "order";
GO

DROP TABLE "SELECT";
GO

SET QUOTED_IDENTIFIER OFF;
GO
```

### B. Use the quoted identifier setting with single and double quotation marks

The following example shows the way single and double quotation marks are used in string expressions with `SET QUOTED_IDENTIFIER` set to `ON` and `OFF`.

```sql
SET QUOTED_IDENTIFIER OFF;
GO

USE AdventureWorks2022;
GO

IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'Test')
    DROP TABLE dbo.Test;
GO

USE AdventureWorks2022;
CREATE TABLE dbo.Test (ID INT, String VARCHAR(30));
GO

-- Literal strings can be in single or double quotation marks.
INSERT INTO dbo.Test VALUES (1, "'Text in single quotes'");
INSERT INTO dbo.Test VALUES (2, '''Text in single quotes''');
INSERT INTO dbo.Test VALUES (3, 'Text with 2 '''' single quotes');
INSERT INTO dbo.Test VALUES (4, '"Text in double quotes"');
INSERT INTO dbo.Test VALUES (5, """Text in double quotes""");
INSERT INTO dbo.Test VALUES (6, "Text with 2 """" double quotes");
GO

SET QUOTED_IDENTIFIER ON;
GO

-- Strings inside double quotation marks are now treated
-- as object names, so they cannot be used for literals.
INSERT INTO dbo."Test" VALUES (7, 'Text with a single '' quote');
GO

-- Object identifiers do not have to be in double quotation marks
-- if they are not reserved keywords.
SELECT ID, String
FROM dbo.Test;
GO

DROP TABLE dbo.Test;
GO

SET QUOTED_IDENTIFIER OFF;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
 ID          String
 ----------- ------------------------------
 1           'Text in single quotes'
 2           'Text in single quotes'
 3           Text with 2 '' single quotes
 4           "Text in double quotes"
 5           "Text in double quotes"
 6           Text with 2 "" double quotes
 7           Text with a single ' quote
 ```

## Related content

- [CREATE DATABASE](create-database-transact-sql.md)
- [CREATE DEFAULT (Transact-SQL)](create-default-transact-sql.md)
- [CREATE PROCEDURE (Transact-SQL)](create-procedure-transact-sql.md)
- [CREATE RULE (Transact-SQL)](create-rule-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](create-table-transact-sql.md)
- [CREATE TRIGGER (Transact-SQL)](create-trigger-transact-sql.md)
- [CREATE VIEW (Transact-SQL)](create-view-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [EXECUTE (Transact-SQL)](../language-elements/execute-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [SET Statements (Transact-SQL)](set-statements-transact-sql.md)
- [SET ANSI_DEFAULTS (Transact-SQL)](set-ansi-defaults-transact-sql.md)
- [sp_rename](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)
- [Database identifiers](../../relational-databases/databases/database-identifiers.md)
