---
title: "SQL_VARIANT_PROPERTY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 96766212-024b-4402-9cf7-8359090cd893
caps.latest.revision: 5
author: BarbKess
---
# SQL_VARIANT_PROPERTY (SQL Server PDW)
Returns the base data type and other information about a **sql_variant** value.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SQL_VARIANT_PROPERTY (expression , property )  
```  
  
## Arguments  
*expression*  
Is an expression of type **sql_variant**.  
  
*property*  
Contains the name of the **sql_variant** property for which information is to be provided. *property* is **varchar(**128**)**, and can be any one of the following values.  
  
|Value|Description|Base type of sql_variant returned|  
|---------|---------------|--------------------------------------|  
|**BaseType**|SQL Server data type, such as:<br /><br />**bigint**<br /><br />**binary**<br /><br />**char**<br /><br />**date**<br /><br />**datetime**<br /><br />**datetime2**<br /><br />**datetimeoffset**<br /><br />**decimal**<br /><br />**float**<br /><br />**int**<br /><br />**money**<br /><br />**nchar**<br /><br />**numeric**<br /><br />**nvarchar**<br /><br />**real**<br /><br />**smalldatetime**<br /><br />**smallint**<br /><br />**smallmoney**<br /><br />**time**<br /><br />**tinyint**<br /><br />**uniqueidentifier**<br /><br />**varbinary**<br /><br />**varchar**|**sysname**<br /><br />NULL = Input is not valid.|  
|**Precision**|Number of digits of the numeric base data type:<br /><br />**datetime** = 23<br /><br />**smalldatetime** = 16<br /><br />**float** = 53<br /><br />**real** = 24<br /><br />**decimal** (p,s) and **numeric** (p,s) = p<br /><br />**money** = 19<br /><br />**smallmoney** = 10<br /><br />**bigint** = 19<br /><br />**int** = 10<br /><br />**smallint** = 5<br /><br />**tinyint** = 3<br /><br />**bit** = 1<br /><br />All other types = 0|**int**<br /><br />NULL = Input is not valid.|  
|**Scale**|Number of digits to the right of the decimal point of the numeric base data type:<br /><br />**decimal** (p,s) and **numeric** (p,s) = s<br /><br />**money** and **smallmoney** = 4<br /><br />**datetime** = 3<br /><br />all other types = 0|**int**<br /><br />NULL = Input is not valid.|  
|**TotalBytes**|Number of bytes required to hold both the metadata and data of the value. This information would be useful in checking the maximum side of data in a **sql_variant** column. If the value is larger than 900, index creation will fail.|**int**<br /><br />NULL = Input is not valid.|  
|**Collation**|Represents the collation of the particular **sql_variant** value.|**sysname**<br /><br />NULL = Input is not valid.|  
|**MaxLength**|Maximum data type length, in bytes. For example, **MaxLength** of **nvarchar(**50**)** is 100, **MaxLength** of **int** is 4.|**int**<br /><br />NULL = Input is not valid.|  
  
## Return Types  
**sql_variant**  
  
## Examples  
The following example retrieves `SQL_VARIANT_PROPERTY` information about a variable named @v1.  
  
```  
DECLARE @v1 sql_variant;  
SET @v1 = 'ABC';  
SELECT @v1;  
SELECT SQL_VARIANT_PROPERTY(@v1, 'BaseType');  
SELECT SQL_VARIANT_PROPERTY(@v1, 'MaxLength');  
```  
  
## See Also  
[DECLARE &#40;SQL Server PDW&#41;](../sqlpdw/declare-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
