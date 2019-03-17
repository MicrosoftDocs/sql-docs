---
title: "SET ANSI_PADDING (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/04/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ANSI_PADDING_TSQL"
  - "ANSI_PADDING"
  - "SET_ANSI_PADDING_TSQL"
  - "SET ANSI_PADDING"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data types [SQL Server], short values"
  - "ANSI_PADDING option"
  - "short values [SQL Server]"
  - "SET ANSI_PADDING statement"
  - "trailing blanks"
ms.assetid: 92bd29a3-9beb-410e-b7e0-7bc1dc1ae6d0
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET ANSI_PADDING (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Controls the way the column stores values shorter than the defined size of the column, and the way the column stores values that have trailing blanks in **char**, **varchar**, **binary**, and **varbinary** data.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax
  
```
-- Syntax for SQL Server

SET ANSI_PADDING { ON | OFF }
```

```
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse

SET ANSI_PADDING ON
```

## Remarks  
 Columns defined with **char**, **varchar**, **binary**, and **varbinary** data types have a defined size.  
  
 This setting affects only the definition of new columns. After the column is created, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stores the values based on the setting when the column was created. Existing columns are not affected by a later change to this setting.  
  
> [!NOTE]  
> ANSI_PADDING should always be set to ON.  
  
 The following table shows the effects of the SET ANSI_PADDING setting when values are inserted into columns with **char**, **varchar**, **binary**, and **varbinary** data types.  
  
|Setting|char(*n*) NOT NULL or binary(*n*) NOT NULL|char(*n*) NULL or binary(*n*) NULL|varchar(*n*) or varbinary(*n*)|  
|-------------|----------------------------------------------------|--------------------------------------------|----------------------------------------|  
|ON|Pad original value (with trailing blanks for **char** columns and with trailing zeros for **binary** columns) to the length of the column.|Follows same rules as for **char(**_n_**)** or **binary(**_n_**)** NOT NULL when SET ANSI_PADDING is ON.|Trailing blanks in character values inserted into **varchar** columns are not trimmed. Trailing zeros in binary values inserted into **varbinary** columns are not trimmed. Values are not padded to the length of the column.|  
|OFF|Pad original value (with trailing blanks for **char** columns and with trailing zeros for **binary** columns) to the length of the column.|Follows same rules as for **varchar** or **varbinary** when SET ANSI_PADDING is OFF.|Trailing blanks in character values inserted into a **varchar** column are trimmed. Trailing zeros in binary values inserted into a **varbinary** column are trimmed.|  
  
> [!NOTE]  
> When padded, **char** columns are padded with blanks, and **binary** columns are padded with zeros. When trimmed, **char** columns have the trailing blanks trimmed, and **binary** columns have the trailing zeros trimmed.  
  
ANSI_PADDING must be ON when you are creating or changing indexes on computed columns or indexed views. For more information about required SET option settings with indexed views and indexes on computed columns, see "Considerations When You Use the SET Statements" in [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md).  
  
The default for SET ANSI_PADDING is ON. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically set ANSI_PADDING to ON when connecting. This can be configured in ODBC data sources, in ODBC connection attributes, or OLE DB connection properties set in the application before connecting. The default for SET ANSI_PADDING is OFF for connections from DB-Library applications.  
  
 The SET ANSI_PADDING setting does not affect the **nchar**, **nvarchar**, **ntext**, **text**, **image**, **varbinary(max)**, **varchar(max)**, and **nvarchar(max)** data types. They always display the SET ANSI_PADDING ON behavior. This means trailing spaces and zeros are not trimmed.  
  
When ANSI_DEFAULTS is ON, ANSI_PADDING is enabled.  
  
The setting of ANSI_PADDING is defined at execute or run time and not at parse time.  
  
To view the current setting for this setting, run the following query.  
  
```sql  
DECLARE @ANSI_PADDING VARCHAR(3) = 'OFF';  
IF ( (16 & @@OPTIONS) = 16 ) SET @ANSI_PADDING = 'ON';  
SELECT @ANSI_PADDING AS ANSI_PADDING;  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## Examples  
The following example shows how the setting affects each of these data types.  

Set ANSI_PADDING to ON and test.

```sql  
PRINT 'Testing with ANSI_PADDING ON'  
SET ANSI_PADDING ON;  
GO  
  
CREATE TABLE t1 (  
   charcol CHAR(16) NULL,   
   varcharcol VARCHAR(16) NULL,   
   varbinarycol VARBINARY(8)  
);  
GO  
INSERT INTO t1 VALUES ('No blanks', 'No blanks', 0x00ee);  
INSERT INTO t1 VALUES ('Trailing blank ', 'Trailing blank ', 0x00ee00);  
  
SELECT 'CHAR' = '>' + charcol + '\<', 'VARCHAR'='>' + varcharcol + '\<',  
   varbinarycol  
FROM t1;  
GO  
```

Now set ANSI_PADDING to OFF and test.

```sql
PRINT 'Testing with ANSI_PADDING OFF';  
SET ANSI_PADDING OFF;  
GO  
  
CREATE TABLE t2 (  
   charcol CHAR(16) NULL,   
   varcharcol VARCHAR(16) NULL,   
   varbinarycol VARBINARY(8)  
);  
GO  
INSERT INTO t2 VALUES ('No blanks', 'No blanks', 0x00ee);  
INSERT INTO t2 VALUES ('Trailing blank ', 'Trailing blank ', 0x00ee00);  
  
SELECT 'CHAR' = '>' + charcol + '\<', 'VARCHAR'='>' + varcharcol + '<',  
   varbinarycol  
FROM t2;  
GO  
  
DROP TABLE t1;  
DROP TABLE t2;  
```  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SESSIONPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/sessionproperty-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [SET ANSI_DEFAULTS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-defaults-transact-sql.md)  
  
  
