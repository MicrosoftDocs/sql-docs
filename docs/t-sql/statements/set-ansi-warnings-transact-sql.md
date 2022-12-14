---
title: "SET ANSI_WARNINGS (Transact-SQL)"
description: SET ANSI_WARNINGS (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "04/15/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET ANSI_WARNINGS"
  - "ANSI_WARNINGS_TSQL"
  - "ANSI_WARNINGS"
  - "SET_ANSI_WARNINGS_TSQL"
helpviewer_keywords:
  - "errors [SQL Server], ISO standard behavior"
  - "warnings [SQL Server]"
  - "SET ANSI_WARNINGS statement"
  - "ANSI_WARNINGS option"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET ANSI_WARNINGS (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Specifies ISO standard behavior for several error conditions.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

### Syntax for [!INCLUDE[ssnoversion-md.md](../../includes/ssnoversion-md.md)] and [!INCLUDE[sssodfull-md.md](../../includes/sssodfull-md.md)]
```syntaxsql
SET ANSI_WARNINGS { ON | OFF }
```

### Syntax for [!INCLUDE[sssdw-md.md](../../includes/sssdw-md.md)] and [!INCLUDE[sspdw-md.md](../../includes/sspdw-md.md)]
```syntaxsql
SET ANSI_WARNINGS ON
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks
 SET ANSI_WARNINGS affects the following conditions:  
  
-   When set to ON, if null values appear in aggregate functions, such as SUM, AVG, MAX, MIN, STDEV, STDEVP, VAR, VARP, or COUNT, a warning message is generated. When set to OFF, no warning is issued.  
  
-   When set to ON, the divide-by-zero and arithmetic overflow errors cause the statement to be rolled back and an error message is generated. When set to OFF, the divide-by-zero and arithmetic overflow errors cause null values to be returned. The behavior in which a divide-by-zero or arithmetic overflow error causes null values to be returned occurs if an INSERT or UPDATE is tried on a **character**, Unicode, or **binary** column in which the length of a new value exceeds the maximum size of the column. If SET ANSI_WARNINGS is ON, the INSERT or UPDATE is canceled as specified by the ISO standard. Trailing blanks are ignored for character columns and trailing nulls are ignored for binary columns. When OFF, data is truncated to the size of the column and the statement succeeds.  
  
> [!NOTE]  
> When truncation occurs in any conversion to or from **binary** or **varbinary** data, no warning or error is issued, regardless of SET options.  
  
> [!NOTE]  
> ANSI_WARNINGS is not honored when passing parameters in a stored procedure, user-defined function, or when declaring and setting variables in a batch statement. For example, if a variable is defined as **char(3)**, and then set to a value larger than three characters, the data is truncated to the defined size and the INSERT or UPDATE statement succeeds.  
  
You can use the user options option of sp_configure to set the default setting for ANSI_WARNINGS for all connections to the server. For more information, see [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).  
  
ANSI_WARNINGS must be ON when you are creating or manipulating indexes on computed columns or indexed views. If SET ANSI_WARNINGS is OFF, CREATE, UPDATE, INSERT, and DELETE statements on tables with indexes on computed columns or indexed views will fail. For more information about required SET option settings with indexed views and indexes on computed columns, see "Considerations When You Use the SET Statements" in [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md).  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes the ANSI_WARNINGS database option. This is equivalent to SET ANSI_WARNINGS. When SET ANSI_WARNINGS is ON, errors or warnings are raised in divide-by-zero, string too large for database column, and other similar errors. When SET ANSI_WARNINGS is OFF, these errors and warnings are not raised. The default value in the model database for SET ANSI_WARNINGS is OFF. If not specified, the setting of ANSI_WARNINGS applies. If SET ANSI_WARNINGS is OFF, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the value of the is_ansi_warnings_on column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
> [!IMPORTANT]
> ANSI_WARNINGS should be set to ON for executing distributed queries.  
  
Clients, such as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and the Microsoft JDBC Driver for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically set ANSI_WARNINGS to ON with a connection flag. This can be configured in ODBC data sources, in ODBC connection attributes, set in the application before connecting. The default for SET ANSI_WARNINGS is OFF for connections from DB-Library applications. For additional information, see [LOGIN7](/openspecs/windows_protocols/ms-tds/773a62b6-ee89-4c02-9e5e-344882630aac) in the  Tabular Data Stream (TDS) protocol specifications. 

When ANSI_DEFAULTS is ON, ANSI_WARNINGS is enabled.  
  
The setting of ANSI_WARNINGS is defined at execute or run time and not at parse time.  
  
If either SET ARITHABORT or SET ARITHIGNORE is OFF and SET ANSI_WARNINGS is ON, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] still returns an error message when encountering divide-by-zero or overflow errors.  
  
To view the current setting for this setting, run the following query.  
  
```sql  
DECLARE @ANSI_WARN VARCHAR(3) = 'OFF';  
IF ( (8 & @@OPTIONS) = 8 ) SET @ANSI_WARN = 'ON';  
SELECT @ANSI_WARN AS ANSI_WARNINGS;  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## Examples  
The following example demonstrates the three situations that are previously mentioned, with the SET ANSI_WARNINGS to ON and OFF.  
  
```sql  
CREATE TABLE T1   
(  
   a int,   
   b int NULL,   
   c varchar(20)  
);  
GO  
  
SET NOCOUNT ON;  
  
INSERT INTO T1   
VALUES (1, NULL, '')   
      ,(1, 0, '')  
      ,(2, 1, '')  
      ,(2, 2, '');  
  
SET NOCOUNT OFF;  
GO  
```

Now set ANSI_WARNINGS to ON and test.

```sql
PRINT '**** Setting ANSI_WARNINGS ON';  
GO  
  
SET ANSI_WARNINGS ON;  
GO  
  
PRINT 'Testing NULL in aggregate';  
GO  
SELECT a, SUM(b)   
FROM T1   
GROUP BY a;  
GO  
  
PRINT 'Testing String Overflow in INSERT';  
GO  
INSERT INTO T1   
VALUES (3, 3, 'Text string longer than 20 characters');  
GO  
  
PRINT 'Testing Divide by zero';  
GO  
SELECT a / b AS ab   
FROM T1;  
GO  
```

Now set ANSI_WARNINGS to OFF and test.

```sql
PRINT '**** Setting ANSI_WARNINGS OFF';  
GO  
SET ANSI_WARNINGS OFF;  
GO  
  
PRINT 'Testing NULL in aggregate';  
GO  
SELECT a, SUM(b)   
FROM T1   
GROUP BY a;  
GO  
  
PRINT 'Testing String Overflow in INSERT';  
GO  
INSERT INTO T1   
VALUES (4, 4, 'Text string longer than 20 characters');  
GO  
SELECT a, b, c   
FROM T1  
WHERE a = 4;  
GO  
  
PRINT 'Testing Divide by zero';  
GO  
SELECT a / b AS ab   
FROM T1;  
GO  
  
DROP TABLE T1;  
```  
  
## See Also  
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET ANSI_DEFAULTS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-defaults-transact-sql.md)   
 [SESSIONPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/sessionproperty-transact-sql.md)  
  
