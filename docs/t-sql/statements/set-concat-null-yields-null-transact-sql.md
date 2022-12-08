---
title: "SET CONCAT_NULL_YIELDS_NULL (Transact-SQL)"
description: SET CONCAT_NULL_YIELDS_NULL (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CONCAT_NULL_YIELDS_NULL_TSQL"
  - "SET CONCAT_NULL_YIELDS_NULL"
  - "CONCAT_NULL_YIELDS_NULL"
  - "SET_CONCAT_NULL_YIELDS_NULL_TSQL"
helpviewer_keywords:
  - "CONCAT_NULL_YIELDS_NULL option"
  - "null values [SQL Server], concatenation results"
  - "concatenation [SQL Server]"
  - "SET CONCAT_NULL_YIELDS_NULL statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET CONCAT_NULL_YIELDS_NULL (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

  Controls whether concatenation results are treated as null or empty string values.  
  
> [!IMPORTANT]  
>  In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CONCAT_NULL_YIELDS_NULL will always be ON and any applications that explicitly set the option to OFF will generate an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

### Syntax for [!INCLUDE[ssnoversion-md.md](../../includes/ssnoversion-md.md)] and [!INCLUDE[sssodfull-md.md](../../includes/sssodfull-md.md)]

```syntaxsql
    
SET CONCAT_NULL_YIELDS_NULL { ON | OFF }   
```  

### Syntax for [!INCLUDE[sssdw-md.md](../../includes/sssdw-md.md)] and [!INCLUDE[sspdw-md.md](../../includes/sspdw-md.md)]

```syntaxsql
  
SET CONCAT_NULL_YIELDS_NULL ON    
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks
 When SET CONCAT_NULL_YIELDS_NULL is ON, concatenating a null value with a string yields a NULL result. For example, `SELECT 'abc' + NULL` yields `NULL`. When SET CONCAT_NULL_YIELDS_NULL is OFF, concatenating a null value with a string yields the string itself (the null value is treated as an empty string). For example, `SELECT 'abc' + NULL` yields `abc`.  
  
 If SET CONCAT_NULL_YIELDS_NULL is not specified, the setting of the **CONCAT_NULL_YIELDS_NULL** database option applies.  
  
> [!NOTE]  
>  SET CONCAT_NULL_YIELDS_NULL is the same setting as the CONCAT_NULL_YIELDS_NULL setting of ALTER DATABASE.  
  
 The setting of SET CONCAT_NULL_YIELDS_NULL is set at execute or run time and not at parse time.  

SET CONCAT_NULL_YIELDS_NULL must be **ON** when creating or altering indexed views, indexes on computed columns, filtered indexes or spatial indexes. If SET CONCAT_NULL_YIELDS_NULL is **OFF**, any CREATE, UPDATE, INSERT, and DELETE statements on tables with indexes on computed columns, filtered indexes, spatial indexes or indexed views will fail. For more information about required SET option settings with indexed views and indexes on computed columns, see "Considerations When You Use the SET Statements" in [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md).
  
 When CONCAT_NULL_YIELDS_NULL is set to OFF, string concatenation across server boundaries cannot occur.  
  
 To view the current setting for this setting, run the following query.  
  
```sql
DECLARE @CONCAT_SETTING VARCHAR(3) = 'OFF';  
IF ( (4096 & @@OPTIONS) = 4096 ) SET @CONCAT_SETTING = 'ON';  
SELECT @CONCAT_SETTING AS CONCAT_NULL_YIELDS_NULL; 
```  
  
## Examples  
 The following example showing using both `SET CONCAT_NULL_YIELDS_NULL` settings.  
  
```sql
PRINT 'Setting CONCAT_NULL_YIELDS_NULL ON';  
GO  
-- SET CONCAT_NULL_YIELDS_NULL ON and testing.  
SET CONCAT_NULL_YIELDS_NULL ON;  
GO  
SELECT 'abc' + NULL ;  
GO  
  
-- SET CONCAT_NULL_YIELDS_NULL OFF and testing.  
SET CONCAT_NULL_YIELDS_NULL OFF;  
GO  
SELECT 'abc' + NULL;   
GO  
```  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SESSIONPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/sessionproperty-transact-sql.md)  
  
  
