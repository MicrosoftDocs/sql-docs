---
title: "COLUMNPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 44
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# COLUMNPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns information about a column or parameter.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
COLUMNPROPERTY ( id , column , property )   
```  
  
## Arguments  
*id*  
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) that contains the identifier (ID) of the table or procedure.
  
*column*  
Is an expression that contains the name of the column or parameter.
  
*property*  
Is an expression that contains the information to be returned for *id*, and can be any one of the following values.
  
|Value|Description|Value returned|  
|---|---|---|
|**AllowsNull**|Allows null values.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**ColumnId**|Column ID value corresponding to **sys.columns.column_id**.|Column ID<br /><br /> **Note:** When querying multiple columns, gaps may appear in the sequence of Column ID values.|  
|**FullTextTypeColumn**|The TYPE COLUMN in the table that holds the document type information of the *column*.|ID of the full-text TYPE COLUMN for the column passed as the second parameter of this property.|  
|**IsComputed**|Column is a computed column.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**IsCursorType**|Procedure parameter is of type CURSOR.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**IsDeterministic**|Column is deterministic. This property applies only to computed columns and view columns.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid. Not a computed column or view column.|  
|**IsFulltextIndexed**|Column has been registered for full-text indexing.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**IsIdentity**|Column uses the IDENTITY property.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**IsIdNotForRepl**|Column checks for the IDENTITY_INSERT setting.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**IsIndexable**|Column can be indexed.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**IsOutParam**|Procedure parameter is an output parameter.|1 = TRUE<br /><br /> 0 = FALSE NULL = Input is not valid.|  
|**IsPrecise**|Column is precise. This property applies only to deterministic columns.|1 = TRUE<br /><br /> 0 = FALSE NULL = Input is not valid. Not a deterministic column|  
|**IsRowGuidCol**|Column has the **uniqueidentifier** data type and is defined with the ROWGUIDCOL property.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**IsSystemVerified**|The determinism and precision properties of the column can be verified by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. This property applies only to computed columns and columns of views.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**IsXmlIndexable**|The XML column can be used in an XML index.|1 = TRUE<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**Precision**|Length for the data type of the column or parameter.|The length of the specified column data type<br /><br /> -1 = **xml** or large value types<br /><br /> NULL = Input is not valid.|  
|**Scale**|Scale for the data type of the column or parameter.|The scale<br /><br /> NULL = Input is not valid.|  
|**StatisticalSemantics**|Column is enabled for semantic indexing.|1 = TRUE<br /><br /> 0 = FALSE|  
|**SystemDataAccess**|Column is derived from a function that accesses data in the system catalogs or virtual system tables of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This property applies only to computed columns and columns of views.|1 = TRUE (Indicates read-only access.)<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**UserDataAccess**|Column is derived from a function that accesses data in user tables, including views and temporary tables, stored in the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This property applies only to computed columns and columns of views.|1 = TRUE (Indicates read-only access.)<br /><br /> 0 = FALSE<br /><br /> NULL = Input is not valid.|  
|**UsesAnsiTrim**|ANSI_PADDING was set ON when the table was first created. This property applies only to columns or parameters of type **char** or **varchar**.|1= TRUE<br /><br /> 0= FALSE<br /><br /> NULL = Input is not valid.|  
|**IsSparse**|Column is a sparse column. For more information, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).|1= TRUE<br /><br /> 0= FALSE<br /><br /> NULL = Input is not valid.|  
|**IsColumnSet**|Column is a column set. For more information, see [Use Column Sets](../../relational-databases/tables/use-column-sets.md).|1= TRUE<br /><br /> 0= FALSE<br /><br /> NULL = Input is not valid.|  
|**GeneratedAlwaysType**|Is column value generated by the system. Corresponds to **sys.columns.generated_always_type**|**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> 0 = Not generated always<br /><br /> 1 = Generated always as row start<br /><br /> 2 – Generated always as row end|  
|**IsHidden**|Is column value generated by the system. Corresponds to **sys.columns.is_hidden**|**Applies to**: [!INCLUDE[ssCurrentLong](../../includes/sscurrentlong-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> 0 = Not hidden<br /><br /> 1 = Hidden|  
  
## Return types
 **int**  
  
## Exceptions  
Returns NULL on error or if a caller does not have permission to view the object.
  
A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as COLUMNPROPERTY may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).
  
## Remarks  
When you check the deterministic property of a column, first test whether the column is a computed column. **IsDeterministic** returns NULL for noncomputed columns. Computed columns can be specified as index columns.
  
## Examples  
The following example returns the length of the `LastName` column.
  
```sql
USE AdventureWorks2012;  
GO  
SELECT COLUMNPROPERTY( OBJECT_ID('Person.Person'),'LastName','PRECISION')AS 'Column Length';  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`Column Length`
  
------------\-
  
 `50`  
  
## See also
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
ms.date: "07/24/2017"
[TYPEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/typeproperty-transact-sql.md)
  
  
