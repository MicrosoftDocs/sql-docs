---
title: "SET CONCAT_NULL_YIELDS_NULL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CONCAT_NULL_YIELDS_NULL_TSQL"
  - "SET CONCAT_NULL_YIELDS_NULL"
  - "CONCAT_NULL_YIELDS_NULL"
  - "SET_CONCAT_NULL_YIELDS_NULL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CONCAT_NULL_YIELDS_NULL option"
  - "null values [SQL Server], concatenation results"
  - "concatenation [SQL Server]"
  - "SET CONCAT_NULL_YIELDS_NULL statement"
ms.assetid: 3091b71c-6518-4eb4-88ab-acae49102bc5
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET CONCAT_NULL_YIELDS_NULL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Controls whether concatenation results are treated as null or empty string values.  
  
> [!IMPORTANT]  
>  In a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CONCAT_NULL_YIELDS_NULL will always be ON and any applications that explicitly set the option to OFF will generate an error. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server  
    
SET CONCAT_NULL_YIELDS_NULL { ON | OFF }   
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
SET CONCAT_NULL_YIELDS_NULL ON    
```  
  
## Remarks  
 When SET CONCAT_NULL_YIELDS_NULL is ON, concatenating a null value with a string yields a NULL result. For example, `SELECT 'abc' + NULL` yields `NULL`. When SET CONCAT_NULL_YIELDS_NULL is OFF, concatenating a null value with a string yields the string itself (the null value is treated as an empty string). For example, `SELECT 'abc' + NULL` yields `abc`.  
  
 If SET CONCAT_NULL_YIELDS_NULL is not specified, the setting of the **CONCAT_NULL_YIELDS_NULL** database option applies.  
  
> [!NOTE]  
>  SET CONCAT_NULL_YIELDS_NULL is the same setting as the CONCAT_NULL_YIELDS_NULL setting of ALTER DATABASE.  
  
 The setting of SET CONCAT_NULL_YIELDS_NULL is set at execute or run time and not at parse time.  
  
 SET CONCAT_NULL_YIELDS_NULL must be ON when you are creating or changing indexes on computed columns or indexed views. If SET CONCAT_NULL_YIELDS_NULL is OFF, any CREATE, UPDATE, INSERT, and DELETE statements on tables with indexes on computed columns or indexed views will fail. For more information about required SET option settings with indexed views and indexes on computed columns, see "Considerations When You Use the SET Statements" in [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md).  
  
 When CONCAT_NULL_YIELDS_NULL is set to OFF, string concatenation across server boundaries cannot occur.  
  
 To view the current setting for this setting, run the following query.  
  
```  
DECLARE @CONCAT_NULL_YIELDS_NULL VARCHAR(3) = 'OFF';  
IF ( (4096 & @@OPTIONS) = 4096 ) SET @CONCAT_NULL_YIELDS_NULL = 'ON';  
SELECT @CONCAT_NULL_YIELDS_NULL AS CONCAT_NULL_YIELDS_NULL;  
  
```  
  
## Examples  
 The following example showing using both `SET CONCAT_NULL_YIELDS_NULL` settings.  
  
```  
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
  
  
