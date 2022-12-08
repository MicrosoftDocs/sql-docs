---
title: "SQL_VARIANT_PROPERTY (Transact-SQL)"
description: "SQL_VARIANT_PROPERTY (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "02/25/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SQL_VARIANT_PROPERTY_TSQL"
  - "SQL_VARIANT_PROPERTY"
helpviewer_keywords:
  - "SQL_VARIANT_PROPERTY function"
  - "sql_variant data type"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SQL_VARIANT_PROPERTY (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the base data type and other information about a **sql_variant** value.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
SQL_VARIANT_PROPERTY ( expression , property )  
```  
  
> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is an expression of type **sql_variant**.  
  
 *property*  
 Contains the name of the **sql_variant** property for which information is to be provided. *property* is **varchar(**128**)**, and can be any one of the following values:  
  
|Value|Description|Base type of sql_variant returned|  
|-----------|-----------------|----------------------------------------|  
|**BaseType**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type, such as:<br /><br /> **bigint**<br /><br /> **binary**<br /><br /> **bit**<br /><br /> **char**<br /><br /> **date**<br /><br /> **datetime**<br /><br /> **datetime2**<br /><br /> **datetimeoffset**<br /><br /> **decimal**<br /><br /> **float**<br /><br /> **int**<br /><br /> **money**<br /><br /> **nchar**<br /><br /> **numeric**<br /><br /> **nvarchar**<br /><br /> **real**<br /><br /> **smalldatetime**<br /><br /> **smallint**<br /><br /> **smallmoney**<br /><br /> **time**<br /><br /> **tinyint**<br /><br /> **uniqueidentifier**<br /><br /> **varbinary**<br /><br /> **varchar**|**sysname**<br /><br /> NULL = Input is not valid.|  
|**Precision**|Number of digits of the numeric base data type:<br /><br /> **date** = 10<br /><br /> **datetime** = 23<br /><br /> **datetime2** = 27<br /><br /> **datetime2** (s) = 19 when s = 0, else s + 20<br /><br /> **datetimeoffset** = 34<br /><br /> **datetimeoffset** (s) = 26 when s = 0, else s + 27<br /><br /> **smalldatetime** = 16<br /><br /> **time** = 16<br /><br /> **time** (s) = 8 when s = 0, else s + 9<br /><br /> **float** = 53<br /><br /> **real** = 24<br /><br /> **decimal** and **numeric** = 18<br /><br /> **decimal** (p,s) and **numeric** (p,s) = p<br /><br /> **money** = 19<br /><br /> **smallmoney** = 10<br /><br /> **bigint** = 19<br /><br /> **int** = 10<br /><br /> **smallint** = 5<br /><br /> **tinyint** = 3<br /><br /> **bit** = 1<br /><br /> All other types = 0|**int**<br /><br /> NULL = Input is not valid.|  
|**Scale**|Number of digits to the right of the decimal point of the numeric base data type:<br /><br /> **decimal** and **numeric** = 0<br /><br /> **decimal** (p,s) and **numeric** (p,s) = s<br /><br /> **money** and **smallmoney** = 4<br /><br /> **datetime** = 3<br /><br /> **datetime2** = 7<br /><br /> **datetime2** (s) = s (0 - 7)<br /><br /> **datetimeoffset** = 7<br /><br /> **datetimeoffset** (s) = s (0 - 7)<br /><br /> **time** = 7<br /><br /> **time** (s) = s (0 - 7)<br /><br /> all other types = 0|**int**<br /><br /> NULL = Input is not valid.|  
|**TotalBytes**|Number of bytes required to hold both the metadata and data of the value. This information would be useful in checking the maximum side of data in a **sql_variant** column. If the value is larger than 900, index creation fails.|**int**<br /><br /> NULL = Input is not valid.|  
|**Collation**|Represents the collation of the particular **sql_variant** value.|**sysname**<br /><br /> NULL = Input is not valid.|  
|**MaxLength**|Maximum data type length, in bytes. For example, **MaxLength** of **nvarchar(**50**)** is 100, **MaxLength** of **int** is 4.|**int**<br /><br /> NULL = Input is not valid.|  
  
## Return Types  
 **sql_variant**  
  
## Examples  
### A. Using a sql_variant in a table  
 The following example retrieves `SQL_VARIANT_PROPERTY` information about the `colA` value `46279.1` where `colB` =`1689`, given that `tableA` has `colA` that is of type `sql_variant` and `colB`.  
  
```sql    
CREATE   TABLE tableA(colA sql_variant, colB int)  
INSERT INTO tableA values ( cast (46279.1 as decimal(8,2)), 1689)  
SELECT   SQL_VARIANT_PROPERTY(colA,'BaseType') AS 'Base Type',  
         SQL_VARIANT_PROPERTY(colA,'Precision') AS 'Precision',  
         SQL_VARIANT_PROPERTY(colA,'Scale') AS 'Scale'  
FROM      tableA  
WHERE      colB = 1689  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)] Note that each of these three values is a **sql_variant**.  
  
```  
Base Type    Precision    Scale  
---------    ---------    -----  
decimal      8           2  
  
(1 row(s) affected)  
```  
  
### B. Using a sql_variant as a variable   
 The following example retrieves `SQL_VARIANT_PROPERTY` information about a variable named @v1.  
  
```sql    
DECLARE @v1 sql_variant;  
SET @v1 = 'ABC';  
SELECT @v1;  
SELECT SQL_VARIANT_PROPERTY(@v1, 'BaseType');  
SELECT SQL_VARIANT_PROPERTY(@v1, 'MaxLength');  
```  
  
## See Also  
 [sql_variant &#40;Transact-SQL&#41;](../../t-sql/data-types/sql-variant-transact-sql.md)  
  
  

