---
title: "SET QUOTED_IDENTIFIER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/21/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "QUOTED_IDENTIFIER_TSQL"
  - "SET_QUOTED_IDENTIFIER_TSQL"
  - "SET QUOTED_IDENTIFIER"
  - "QUOTED_IDENTIFIER"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "delimited identifiers [SQL Server]"
  - "identifiers [SQL Server], delimited"
  - "QUOTED_IDENTIFIER option"
  - "quoted identifiers"
  - "ISO delimited identifiers rules"
  - "SET QUOTED_IDENTIFIER statement"
ms.assetid: 10f66b71-9241-4a3a-9292-455ae7252565
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET QUOTED_IDENTIFIER (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to follow the ISO rules regarding quotation mark delimiting identifiers and literal strings. Identifiers delimited by double quotation marks can be either [!INCLUDE[tsql](../../includes/tsql-md.md)] reserved keywords or can contain characters not generally allowed by the [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax rules for identifiers.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```
-- Syntax for SQL Server and Azure SQL Database

SET QUOTED_IDENTIFIER { ON | OFF }
```

```
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse

SET QUOTED_IDENTIFIER ON
```

## Remarks

When SET QUOTED_IDENTIFIER is ON, identifiers can be delimited by double quotation marks, and literals must be delimited by single quotation marks. When SET QUOTED_IDENTIFIER is OFF, identifiers cannot be quoted and must follow all [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md). Literals can be delimited by either single or double quotation marks.

When SET QUOTED_IDENTIFIER is ON (default), all strings delimited by double quotation marks are interpreted as object identifiers. Therefore, quoted identifiers do not have to follow the [!INCLUDE[tsql](../../includes/tsql-md.md)] rules for identifiers. They can be reserved keywords and can include characters not generally allowed in [!INCLUDE[tsql](../../includes/tsql-md.md)] identifiers. Double quotation marks cannot be used to delimit literal string expressions; single quotation marks must be used to enclose literal strings. If a single quotation mark (**'**) is part of the literal string, it can be represented by two single quotation marks (**"**). SET QUOTED_IDENTIFIER must be ON when reserved keywords are used for object names in the database.

When SET QUOTED_IDENTIFIER is OFF, literal strings in expressions can be delimited by single or double quotation marks. If a literal string is delimited by double quotation marks, the string can contain embedded single quotation marks, such as apostrophes.

SET QUOTED_IDENTIFIER must be ON when you are creating or changing indexes on computed columns or indexed views. If SET QUOTED_IDENTIFIER is OFF, CREATE, UPDATE, INSERT, and DELETE statements on tables with indexes on computed columns or indexed views will fail. For more information about required SET option settings with indexed views and indexes on computed columns, see "Considerations When You Use the SET Statements" in [SET Statements](../../t-sql/statements/set-statements-transact-sql.md).

SET QUOTED_IDENTIFIER must be ON when you are creating a filtered index.

SET QUOTED_IDENTIFIER must be ON when you invoke XML data type methods.

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically set QUOTED_IDENTIFIER to ON when connecting. This can be configured in ODBC data sources, in ODBC connection attributes, or OLE DB connection properties. The default for SET QUOTED_IDENTIFIER is OFF for connections from DB-Library applications.

When a table is created, the QUOTED IDENTIFIER option is always stored as ON in the table's metadata even if the option is set to OFF when the table is created.

When a stored procedure is created, the SET QUOTED_IDENTIFIER and SET ANSI_NULLS settings are captured and used for subsequent invocations of that stored procedure.

When executed inside a stored procedure, the setting of SET QUOTED_IDENTIFIER is not changed.

When SET ANSI_DEFAULTS is ON, SET QUOTED_IDENTIFIER is enabled.

SET QUOTED_IDENTIFIER also corresponds to the QUOTED_IDENTIFIER setting of ALTER DATABASE. For more information about database settings, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

SET QUOTED_IDENTIFIER takes effect at parse-time and only affects parsing, not query execution.

For a top-level ad-hoc batch parsing begins using the session's current setting for QUOTED_IDENTIFIER. As the batch is parsed any occurrence of SET QUOTED_IDENTIFIER will change the parsing behavior from that point on, and save that setting for the session. So after the batch is parsed and executed, the session's QUOTED_IDENTIFER setting will be set according to the last occurrence of SET QUOTED_IDENTIFIER in the batch.

Static SQL in a stored procedure is parsed using the QUOTED_IDENTIFIER setting in effect for the batch that created or altered the stored procedure. SET QUOTED_IDENTIFIER has no effect when it appears in the body of a stored procedure as static SQL.

For a nested batch using sp_executesql or exec() the parsing begins using the QUOTED_IDENTIFIER setting of the session. If the nested batch is inside a stored procedure the parsing starts using the QUOTED_IDENTIFIER setting of the stored procedure. As the nested batch is parsed, any occurrence of SET QUOTED_IDENTIFIER will change the parsing behavior from that point on, but the session's QUOTED_IDENTIFIER setting will not be updated.

Using brackets, **[** and **]**, to delimit identifiers is not affected by the QUOTED_IDENTIFIER setting.

To view the current setting for this setting, run the following query.

```sql
DECLARE @QUOTED_IDENTIFIER VARCHAR(3) = 'OFF';
IF ( (256 & @@OPTIONS) = 256 ) SET @QUOTED_IDENTIFIER = 'ON';
SELECT @QUOTED_IDENTIFIER AS QUOTED_IDENTIFIER;

```

## Permissions

Requires membership in the public role.

## Examples

### A. Using the quoted identifier setting and reserved word object names

The following example shows that the `SET QUOTED_IDENTIFIER` setting must be `ON`, and the keywords in table names must be in double quotation marks to create and use objects that have reserved keyword names.

```sql
SET QUOTED_IDENTIFIER OFF
GO
-- An attempt to create a table with a reserved keyword as a name
-- should fail.
CREATE TABLE "select" ("identity" INT IDENTITY NOT NULL, "order" INT NOT NULL);
GO

SET QUOTED_IDENTIFIER ON;
GO

-- Will succeed.
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

### B. Using the quoted identifier setting with single and double quotation marks

 The following example shows the way single and double quotation marks are used in string expressions with `SET QUOTED_IDENTIFIER` set to `ON` and `OFF`.

```sql
SET QUOTED_IDENTIFIER OFF;
GO
USE AdventureWorks2012;
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'Test')
    DROP TABLE dbo.Test;
GO
USE AdventureWorks2012;
CREATE TABLE dbo.Test (ID INT, String VARCHAR(30)) ;
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

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
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

## See Also

- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?view=sql-server-2017)
- [CREATE DEFAULT](../../t-sql/statements/create-default-transact-sql.md)
- [CREATE PROCEDURE](../../t-sql/statements/create-procedure-transact-sql.md)
- [CREATE RULE](../../t-sql/statements/create-rule-transact-sql.md)
- [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md)
- [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md)
- [CREATE VIEW](../../t-sql/statements/create-view-transact-sql.md)
- [Data Types](../../t-sql/data-types/data-types-transact-sql.md)
- [EXECUTE](../../t-sql/language-elements/execute-transact-sql.md)
- [SELECT](../../t-sql/queries/select-transact-sql.md)
- [SET Statements](../../t-sql/statements/set-statements-transact-sql.md)
- [SET ANSI_DEFAULTS](../../t-sql/statements/set-ansi-defaults-transact-sql.md)
- [sp_rename](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)
