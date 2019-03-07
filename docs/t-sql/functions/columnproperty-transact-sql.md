---
title: "COLUMNPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "COLUMNPROPERTY"
  - "COLUMNPROPERTY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "column properties [SQL Server]"
  - "parameters [SQL Server], properties"
  - "COLUMNPROPERTY function"
ms.assetid: 2408c264-6eca-4120-bb71-df043c7c2792
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# COLUMNPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns column or parameter information.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
COLUMNPROPERTY ( id , column , property )   
```  
  
## Arguments  
*id*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) containing the identifier (ID) of the table or procedure.
  
*column*  
An expression containing the name of the column or parameter.
  
*property*  
For the *id* argument, the *property* argument specifies the information type that the `COLUMNPROPERTY` function will return. The *property* argument can have any one of these values:
  
|Value|Description|Value returned|  
|---|---|---|
|**AllowsNull**|Allows null values.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**ColumnId**|Column ID value corresponding to **sys.columns.column_id**.|Column ID<br /><br /> **Note:** When querying multiple columns, gaps may appear in the sequence of Column ID values.|  
|**FullTextTypeColumn**|The TYPE COLUMN in the table holding the document type information of the *column*.|ID of the full-text TYPE COLUMN for the column name expression passed as the second parameter of this function.|  
|**GeneratedAlwaysType**|Is column value system-generated. Corresponds to **sys.columns.generated_always_type**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> 0: Not generated always<br /><br /> 1: Generated always at row start<br /><br /> 2: Generated always at row end|  
|**IsColumnSet**|Column is a column set. For more information, see [Use Column Sets](../../relational-databases/tables/use-column-sets.md).|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsComputed**|Column is a computed column.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsCursorType**|Procedure parameter is of type CURSOR.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsDeterministic**|Column is deterministic. This property applies only to computed columns and view columns.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input. Not a computed column or view column.|  
|**IsFulltextIndexed**|Column is registered for full-text indexing.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsHidden**|Is column value system-generated. Corresponds to **sys.columns.is_hidden**|**Applies to**: [!INCLUDE[ssCurrentLong](../../includes/sscurrent-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> 0: Not hidden<br /><br /> 1: Hidden|  
|**IsIdentity**|Column uses the IDENTITY property.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsIdNotForRepl**|Column checks for the IDENTITY_INSERT setting.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsIndexable**|Column can be indexed.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsOutParam**|Procedure parameter is an output parameter.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsPrecise**|Column is precise. This property applies only to deterministic columns.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input. Not a deterministic column|  
|**IsRowGuidCol**|Column has the **uniqueidentifier** data type, and is defined with the ROWGUIDCOL property.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsSparse**|Column is a sparse column. For more information, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsSystemVerified**|The [!INCLUDE[ssDE](../../includes/ssde-md.md)] can verify the determinism and precision properties of the column. This property applies only to computed columns and columns of views.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**IsXmlIndexable**|The XML column can be used in an XML index.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**Precision**|Data type length of the column or parameter.|The length of the specified column data type<br /><br /> -1: **xml** or large value types<br /><br /> NULL: invalid input.|  
|**Scale**|Scale for the column or parameter data type.|The scale value<br /><br /> NULL: invalid input.|  
|**StatisticalSemantics**|Column is enabled for semantic indexing.|1: TRUE<br /><br /> 0: FALSE|  
|**SystemDataAccess**|Column is derived from a function that accesses data in the system catalogs or virtual system tables of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This property applies only to computed columns and columns of views.|1: TRUE (Indicates read-only access.)<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**UserDataAccess**|Column is derived from a function that accesses data in user tables, including views and temporary tables, stored in the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This property applies only to computed columns and columns of views.|1: TRUE (Indicates read-only access.)<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
|**UsesAnsiTrim**|ANSI_PADDING was set ON at time of table creation. This property applies only to columns or parameters of type **char** or **varchar**.|1: TRUE<br /><br /> 0: FALSE<br /><br /> NULL: invalid input.|  
  
## Return types
 **int**  
  
## Exceptions  
Returns NULL on error, or if a caller does not have permission to view the object.
  
A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as `COLUMNPROPERTY` might return NULL, if the user does not have correct permission on the object. See [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md) for more information.
  
## Remarks  
When checking the deterministic property of a column, first test whether the column is a computed column. The **IsDeterministic** argument returns NULL for noncomputed columns. Computed columns can be specified as index columns.
  
## Examples  
This example returns the length of the `LastName` column.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT COLUMNPROPERTY( OBJECT_ID('Person.Person'),'LastName','PRECISION')AS 'Column Length';  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Column Length
-------------
50
```  
  
## See also
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
[TYPEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/typeproperty-transact-sql.md)
  
  
