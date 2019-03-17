---
title: "sys.dm_exec_describe_first_result_set (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_exec_describe_first_result_set"
  - "sys.dm_exec_describe_first_result_set_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_describe_first_result_set catalog view"
ms.assetid: 6ea88346-0bdb-4f0e-9f1f-4d85e3487d23
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_describe_first_result_set (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  This dynamic management function takes a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement as a parameter and describes the metadata of the first result set for the statement.  
  
 **sys.dm_exec_describe_first_result_set** has the same result set definition as [sys.dm_exec_describe_first_result_set_for_object &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md) and is similar to [sp_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md).  
  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.dm_exec_describe_first_result_set(@tsql, @params, @include_browse_information)  
```  
  
## Arguments  
 *\@tsql*  
 One or more [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. *Transact-SQL_batch* may be **nvarchar(***n***)** or **nvarchar(max)**.  
  
 *\@params*  
 \@params provides a declaration string for parameters for the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch, similar to sp_executesql. Parameters may be **nvarchar(n)** or **nvarchar(max)**.  
  
 Is one string that contains the definitions of all parameters that have been embedded in the [!INCLUDE[tsql](../../includes/tsql-md.md)]*_batch*. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in stmt must be defined in \@params. If the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch in the statement does not contain parameters, \@params is not required. NULL is the default value for this parameter.  
  
 *\@include_browse_information*  
 If set to 1, each query is analyzed as if it has a FOR BROWSE option on the query. Additional key columns and source table information are returned.  
  
## Table Returned  
 This common metadata is returned as a result set. One row for each column in the results metadata describes the type and nullability of the column in the format shown in the following table. If the first statement does not exist for every control path, a result set with zero rows is returned.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**is_hidden**|**bit**|Specifies that the column is an extra column added for browsing and informational purposes that does not actually appear in the result set.|  
|**column_ordinal**|**int**|Contains the ordinal position of the column in the result set. Position of the first column will be specified as 1.|  
|**name**|**sysname**|Contains the name of the column if a name can be determined. If not, will contain NULL.|  
|**is_nullable**|**bit**|Contains the following values:<br /><br /> Value 1 if column allows NULLs.<br /><br /> Value 0 if the column does not allow NULLs.<br /><br /> Value 1 if it cannot be determined that the column allows NULLs.|  
|**system_type_id**|**int**|Contains the system_type_id of the column data type as specified in sys.types. For CLR types, even though the system_type_name column will return NULL, this column will return the value 240.|  
|**system_type_name**|**nvarchar(256)**|Contains the name and arguments (such as length, precision, scale), specified for the data type of the column.<br /><br /> If data type is a user-defined alias type, the underlying system type is specified here.<br /><br /> If data type is a CLR user-defined type, NULL is returned in this column.|  
|**max_length**|**smallint**|Maximum length (in bytes) of the column.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br /> For **text** columns, the **max_length** value will be 16 or the value set by **sp_tableoption 'text in row'**.|  
|**precision**|**tinyint**|Precision of the column if numeric-based. Otherwise returns 0.|  
|**scale**|**tinyint**|Scale of column if numeric-based. Otherwise returns 0.|  
|**collation_name**|**sysname**|Name of the collation of the column if character-based. Otherwise returns NULL.|  
|**user_type_id**|**int**|For CLR and alias types, contains the user_type_id of the data type of the column as specified in sys.types. Otherwise is NULL.|  
|**user_type_database**|**sysname**|For CLR and alias types, contains the name of the database in which the type is defined. Otherwise is NULL.|  
|**user_type_schema**|**sysname**|For CLR and alias types, contains the name of the schema in which the type is defined. Otherwise is NULL.|  
|**user_type_name**|**sysname**|For CLR and alias types, contains the name of the type. Otherwise is NULL.|  
|**assembly_qualified_type_name**|**nvarchar(4000)**|For CLR types, returns the name of the assembly and class defining the type. Otherwise is NULL.|  
|**xml_collection_id**|**int**|Contains the xml_collection_id of the data type of the column as specified in sys.columns. This column returns NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_database**|**sysname**|Contains the database in which the XML schema collection associated with this type is defined. This column returns NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_schema**|**sysname**|Contains the schema in which the XML schema collection associated with this type is defined. This column returns NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_name**|**sysname**|Contains the name of the XML schema collection associated with this type. This column returns NULL if the type returned is not associated with an XML schema collection.|  
|**is_xml_document**|**bit**|Returns 1 if the returned data type is XML and that type is guaranteed to be a complete XML document (including a root node), as opposed to an XML fragment). Otherwise returns 0.|  
|**is_case_sensitive**|**bit**|Returns 1 if the column is of a case-sensitive string type. Returns 0 if it is not.|  
|**is_fixed_length_clr_type**|**bit**|Returns 1 if the column is of a fixed-length CLR type. Returns 0 if it is not.|  
|**source_server**|**sysname**|Name of the originating server (if it originates from a remote server). The name is given as it appears in sys.servers. Returns NULL if the column originates on the local server or if it cannot be determined which server it originates on. Is only populated if browsing information is requested.|  
|**source_database**|**sysname**|Name of the originating database returned by the column in this result. Returns NULL if the database cannot be determined. Is only populated if browsing information is requested.|  
|**source_schema**|**sysname**|Name of the originating schema returned by the column in this result. Returns NULL if the schema cannot be determined. Is only populated if browsing information is requested.|  
|**source_table**|**sysname**|Name of the originating table returned by the column in this result. Returns NULL if the table cannot be determined. Is only populated if browsing information is requested.|  
|**source_column**|**sysname**|Name of the originating column returned by the result column. Returns NULL if the column cannot be determined. Is only populated if browsing information is requested.|  
|**is_identity_column**|**bit**|Returns 1 if the column is an identity column and 0 if not. Returns NULL if it cannot be determined that the column is an identity column.|  
|**is_part_of_unique_key**|**bit**|Returns 1 if the column is part of a unique index (including unique and primary constraints) and 0 if it is not. Returns NULL if it cannot be determined that the column is part of a unique index. Is only populated if browsing information is requested.|  
|**is_updateable**|**bit**|Returns 1 if the column is updateable and 0 if not. Returns NULL if it cannot be determined that the column is updateable.|  
|**is_computed_column**|**bit**|Returns 1 if the column is a computed column and 0 if not. Returns NULL if it cannot be determined if the column is a computed column.|  
|**is_sparse_column_set**|**bit**|Returns 1 if the column is a sparse column and 0 if not. Returns NULL if it cannot be determined that the column is a part of a sparse column set.|  
|**ordinal_in_order_by_list**|**smallint**|ihe position of this column is in ORDER BY list. Returns NULL if the column does not appear in the ORDER BY list, or if the ORDER BY list cannot be uniquely determined.|  
|**order_by_list_length**|**smallint**|The length of the ORDER BY list. NULL is returned if there is no ORDER BY list or if the ORDER BY list cannot be uniquely determined. Note that this value will be the same for all rows returned by sp_describe_first_result_set.|  
|**order_by_is_descending**|**smallint NULL**|If the ordinal_in_order_by_list is not NULL, the **order_by_is_descending** column reports the direction of the ORDER BY clause for this column. Otherwise it reports NULL.|  
|**error_number**|**int**|Contains the error number returned by the function. If no error occurred, the column will contain NULL.|  
|**error_severity**|**int**|Contains the severity returned by the function. If no error occurred, the column will contain NULL.|  
|**error_state**|**int**|Contains the state message. returned by the function. If no error occurred, the column will contain NULL.|  
|**error_message**|**nvarchar(4096)**|Contains the message returned by the function. If no error occurred, the column will contain NULL.|  
|**error_type**|**int**|Contains an integer representing the error being returned. Maps to error_type_desc. See the list under remarks.|  
|**error_type_desc**|**nvarchar(60)**|Contains a short uppercase string representing the error being returned. Maps to error_type. See the list under remarks.|  
  
## Remarks  
 This function uses the same algorithm as **sp_describe_first_result_set**. For more information, see [sp_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md).  
  
 The following table lists the error types and their descriptions  
  
|error_type|error_type|Description|  
|-----------------|-----------------|-----------------|  
|1|MISC|All errors that are not otherwise described.|  
|2|SYNTAX|A syntax error occurred in the batch.|  
|3|CONFLICTING_RESULTS|The result could not be determined because of a conflict between two possible first statements.|  
|4|DYNAMIC_SQL|The result could not be determined because of dynamic SQL that could potentially return the first result.|  
|5|CLR_PROCEDURE|The result could not be determined because a CLR stored procedure could potentially return the first result.|  
|6|CLR_TRIGGER|The result could not be determined because a CLR trigger could potentially return the first result.|  
|7|EXTENDED_PROCEDURE|The result could not be determined because an extended stored procedure could potentially return the first result.|  
|8|UNDECLARED_PARAMETER|The result could not be determined because the data type of one or more of the result set's columns potentially depends on an undeclared parameter.|  
|9|RECURSION|The result could not be determined because the batch contains a recursive statement.|  
|10|TEMPORARY_TABLE|The result could not be determined because the batch contains a temporary table and is not supported by **sp_describe_first_result_set** .|  
|11|UNSUPPORTED_STATEMENT|The result could not be determined because the batch contains a statement that is not supported by **sp_describe_first_result_set** (e.g., FETCH, REVERT etc.).|  
|12|OBJECT_TYPE_NOT_SUPPORTED|The \@object_id passed to the function is not supported (i.e. not a stored procedure)|  
|13|OBJECT_DOES_NOT_EXIST|The \@object_id passed to the function was not found in the system catalog.|  
  
## Permissions  
 Requires permission to execute the \@tsql argument.  
  
## Examples  
 Additional examples in the topic [sp_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) can be adapted to use **sys.dm_exec_describe_first_result_set**.  
  
### A. Returning information about a single Transact-SQL statement  
 The following code returns information about the results of a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT * FROM sys.dm_exec_describe_first_result_set  
(N'SELECT object_id, name, type_desc FROM sys.indexes', null, 0) ;  
```  
  
### B. Returning information about a procedure  
 The following example creates a stored procedure named pr_TestProc that returns two result sets. Then the example demonstrates that **sys.dm_exec_describe_first_result_set** returns information about the first result set in the procedure.  
  
```  
USE AdventureWorks2012;  
GO  
  
CREATE PROC Production.TestProc  
AS  
SELECT Name, ProductID, Color FROM Production.Product ;  
SELECT Name, SafetyStockLevel, SellStartDate FROM Production.Product ;  
GO  
  
SELECT * FROM sys.dm_exec_describe_first_result_set  
('Production.TestProc', NULL, 0) ;  
```  
  
### C. Returning metadata from a batch that contains multiple statements  
 The following example evaluates a batch that contains two [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. The result set describes the first result set returned.  
  
```  
USE AdventureWorks2012;  
GO  
  
SELECT * FROM sys.dm_exec_describe_first_result_set(  
N'SELECT CustomerID, TerritoryID, AccountNumber FROM Sales.Customer WHERE CustomerID = @CustomerID;  
SELECT * FROM Sales.SalesOrderHeader;',  
N'@CustomerID int', 0) AS a;  
GO  
```  
  
## See Also  
 [sp_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md)   
 [sp_describe_undeclared_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md)   
 [sys.dm_exec_describe_first_result_set_for_object &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md)  
  
  
