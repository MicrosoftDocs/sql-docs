---
title: "sp_batch_params (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_batch_params"
  - "sp_batch_params_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_batch_params"
ms.assetid: 7b92fe9e-e755-4b7a-8a15-822c58a813d3
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_batch_params (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a rowset that contains information about the parameters included in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch. **sp_batch_params** only parses the batch specified and returns information about embedded parameter values. It does not execute the batch or modify the execution environment.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_batch_params [ [ @tsqlbatch = ] 'tsqlbatch' ]   
```  
  
## Arguments  
`[ @tsqlbatch = ] 'tsqlbatch'`
 Is a Unicode string that contains a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch for which parameter information is that you want. *tsqlbatch* is **nvarchar(max)** or implicitly convertible to **nvarchar(max)**.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**PARAMETER_NAME**|**sysname**|Name of the parameter that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] found in the batch.|  
|**COLUMN_TYPE**|**smallint**|This field returns one of the following values:<br /><br /> 0 = SQL_PARAM_TYPE_UNKNOWN<br /><br /> 1 = SQL_PARAM_TYPE_INPUT<br /><br /> 2 = SQL_PARAM_TYPE_OUTPUT<br /><br /> 3 = SQL_RESULT_COL<br /><br /> 4 = SQL_PARAM_OUTPUT<br /><br /> 5 = SQL_RETURN_VALUE<br /><br /> This column is always 0.|  
|**DATA_TYPE**|**smallint**|Data type of the parameter (Integer code for an ODBC data type). If this data type cannot be mapped to an ISO type, the value is NULL. The native data type name is returned in the **TYPE_NAME** column. This value is always NULL.|  
|**TYPE_NAME**|**sysname**|String representation of the data type as it is presented by the underlying DBMS. This value is NULL.|  
|**PRECISION**|**int**|Number of significant digits. The return value for the **PRECISION** column is in base 10.|  
|**LENGTH**|**int**|Transfer size of the data. This value is NULL.|  
|**SCALE**|**smallint**|Number of digits to the right of the decimal point. This value is NULL.|  
|**RADIX**|**smallint**|Is the base for numeric types. This value is NULL.|  
|**NULLABLE**|**smallint**|Specifies nullability:<br /><br /> 1 = Parameter data type can be created allowing null values.<br /><br /> 0 = Null values are not allowed.<br /><br /> This value is NULL.|  
|**SQL_DATA_TYPE**|**smallint**|Value of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type as it appears in the TYPE field of the descriptor. This column is the same as the **DATA_TYPE** column, except for the **datetime** and ISO **interval** data types. This column always returns a value. This value is NULL.|  
|**SQL_DATETIME_SUB**|**smallint**|The **datetime** or ISO **interval** subcode if the value of **SQL_DATA_TYPE** is SQL_DATETIME or SQL_INTERVAL. For data types other than **datetime** and ISO **interval**, this column is NULL. This value is NULL.|  
|**CHAR_OCTET_LENGTH**|**int**|Maximum length in bytes of a **character** or **binary** data type parameter. For all other data types, this column returns a NULL. This value is always NULL.|  
|**ORDINAL_POSITION**|**int**|Ordinal position of the parameter in the batch. If the parameter name is repeated multiple times, this column contains the ordinal of the first occurrence. The first parameter has ordinal 1. This column always returns a value.|  
  
## Permissions  
 Permission to execute **sp_batch_params** is granted to **public**.  
  
## Examples  
 The following example shows a query being passed to `sp_batch_params`. The result set enumerates the list of embedded parameter values.  
  
```  
DECLARE @SQLString nvarchar(500);  
/* Build the SQL string */  
SET @SQLString =  
     N'SELECT * FROM AdventureWorks2012.HumanResources.Employee   
     WHERE BusinessEntityID = @BusinessEntityID';  
EXECUTE sp_batch_params @SQLString;  
```  
  
## See Also  
 [Running Stored Procedures](../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)   
 [Running Stored Procedures How-to Topics &#40;ODBC&#41;](https://msdn.microsoft.com/library/c2220182-a23d-4475-b353-77a77ab613d6)   
 [Running Stored Procedures &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/stored-procedures-running.md)  
  
  
