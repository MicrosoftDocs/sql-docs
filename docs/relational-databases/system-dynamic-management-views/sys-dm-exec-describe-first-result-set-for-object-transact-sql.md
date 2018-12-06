---
title: "sys.dm_exec_describe_first_result_set_for_object (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_exec_describe_first_result_set_for_object_TSQL"
  - "sys.dm_exec_describe_first_result_set_for_object"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_describe_first_result_set_for_object catalog view"
ms.assetid: 63b0fde7-95d7-4ad7-a219-a9feacf1bd89
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_describe_first_result_set_for_object (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  This dynamic management function takes an @object_id as a parameter and describes the first result metadata for the module with that ID. The @object_id specified can be the ID of a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure or a [!INCLUDE[tsql](../../includes/tsql-md.md)] trigger. If it is the ID of any other object (such as a view, table, function, or CLR procedure), an error will be specified in the error columns of the result.  
  
 **sys.dm_exec_describe_first_result_set_for_object** has the same result set definition as [sys.dm_exec_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md) and is similar to [sp_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.dm_exec_describe_first_result_set_for_object   
    ( @object_id , @include_browse_information )  
```  
  
## Arguments  
 *@object_id*  
 The @object_id of a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure or a [!INCLUDE[tsql](../../includes/tsql-md.md)] trigger. @object_id is type **int**.  
  
 *@include_browse_information*  
 @include_browse_information is type **bit**. If set to 1, each query is analyzed as if it has a FOR BROWSE option on the query. Returns additional key columns and source table information.  
  
## Table Returned  
 This common metadata is returned as a result set with one row for each column in the results metadata. Each row describes the type and nullability of the column in the format described in the following section. If the first statement does not exist for every control path, a result set with zero rows is returned.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**is_hidden**|**bit**|Specifies whether the column is an extra column added for browsing information purposes that does not actually appear in the result set.|  
|**column_ordinal**|**int**|Contains the ordinal position of the column in the result set. Position of the first column will be specified as 1.|  
|**name**|**sysname**|Contains the name of the column if a name can be determined. Otherwise is NULL.|  
|**is_nullable**|**bit**|Contains the value 1 if the column allows NULLs, 0 if the column does not allow NULLs, and 1 if it cannot be determined that the column allows NULLs.|  
|**system_type_id**|**int**|Contains the system_type_id of the data type of the column as specified in sys.types. For CLR types, even though the system_type_name column will return NULL, this column will return the value 240.|  
|**system_type_name**|**nvarchar(256)**|Contains the data type name. Includes arguments (such as length, precision, scale) specified for the data type of the column. If the data type is a user-defined alias type, the underlying system type is specified here. If it is a CLR user-defined type, NULL is returned in this column.|  
|**max_length**|**smallint**|Maximum length (in bytes) of the column.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br /> For **text** columns, the **max_length** value will be 16 or the value set by **sp_tableoption 'text in row'**.|  
|**precision**|**tinyint**|Precision of the column if numeric-based. Otherwise returns 0.|  
|**scale**|**tinyint**|Scale of column if numeric-based. Otherwise returns 0.|  
|**collation_name**|**sysname**|Name of the collation of the column if character-based. Otherwise returns NULL.|  
|**user_type_id**|**int**|For CLR and alias types, contains the user_type_id of the data type of the column as specified in sys.types. Otherwise is NULL.|  
|**user_type_database**|**sysname**|For CLR and alias types, contains the name of the database in which the type is defined. Otherwise is NULL.|  
|**user_type_schema**|**sysname**|For CLR and alias types, contains the name of the schema in which the type is defined. Otherwise is NULL.|  
|**user_type_name**|**sysname**|For CLR and alias types, contains the name of the type. Otherwise is NULL.|  
|**assembly_qualified_type_name**|**nvarchar(4000)**|For CLR types, returns the name of the assembly and class defining the type. Otherwise is NULL.|  
|**xml_collection_id**|**int**|Contains the xml_collection_id of the data type of the column as specified in sys.columns. This column will return NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_database**|**sysname**|Contains the database in which the XML schema collection associated with this type is defined. This column will return NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_schema**|**sysname**|Contains the schema in which the XML schema collection associated with this type is defined. This column will return NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_name**|**sysname**|Contains the name of the XML schema collection associated with this type. This column will return NULL if the type returned is not associated with an XML schema collection.|  
|**is_xml_document**|**bit**|Returns 1 if the returned data type is XML and that type is guaranteed to be a complete XML document (including a root node), as opposed to an XML fragment). Otherwise returns 0.|  
|**is_case_sensitive**|**bit**|Returns 1 if the column is of a case-sensitive string type and 0 if it is not.|  
|**is_fixed_length_clr_type**|**bit**|Returns 1 if the column is of a fixed-length CLR type and 0 if it is not.|  
|**source_server**|**sysname**|Name of the originating server returned by the column in this result (if it originates from a remote server). The name is given as it appears in sys.servers.  Returns NULL if the column originates on the local server, or if it cannot be determined which server it originates on. Is only populated if browsing information is requested.|  
|**source_database**|**sysname**|Name of the originating database returned by the column in this result. Returns NULL if the database cannot be determined. Is only populated if browsing information is requested.|  
|**source_schema**|**sysname**|Name of the originating schema returned by the column in this result. Returns NULL if the schema cannot be determined. Is only populated if browsing information is requested.|  
|**source_table**|**sysname**|Name of the originating table returned by the column in this result. Returns NULL if the table cannot be determined. Is only populated if browsing information is requested.|  
|**source_column**|**sysname**|Name of the originating column returned by the column in this result. Returns NULL if the column cannot be determined. Is only populated if browsing information is requested.|  
|**is_identity_column**|**bit**|Returns 1 if the column is an identity column and 0 if not. Returns NULL if it cannot be determined that the column is an identity column.|  
|**is_part_of_unique_key**|**bit**|Returns 1 if the column is part of a unique index (including unique and primary constraint) and 0 if not. Returns NULL if it cannot be determined that the column is part of a unique index. Only populated if browsing information is requested.|  
|**is_updateable**|**bit**|Returns 1 if the column is updateable and 0 if not. Returns NULL if it cannot be determined that the column is updateable.|  
|**is_computed_column**|**bit**|Returns 1 if the column is a computed column and 0 if not. Returns NULL if it cannot be determined that the column is a computed column.|  
|**is_sparse_column_set**|**bit**|Returns 1 if the column is a sparse column and 0 if not. Returns NULL if it cannot be determined that the column is a part of a sparse column set.|  
|**ordinal_in_order_by_list**|**smallint**|Position of this column in ORDER BY list  Returns NULL if the column does not appear in the ORDER BY list or if the ORDER BY list cannot be uniquely determined.|  
|**order_by_list_length**|**smallint**|Length of the ORDER BY list. Returns NULL if there is no ORDER BY list or if the ORDER BY list cannot be uniquely determined. Note that this value will be the same for all rows returned by sp_describe_first_result_set.|  
|**order_by_is_descending**|**smallint NULL**|If the ordinal_in_order_by_list is not NULL, the **order_by_is_descending** column reports the direction of the ORDER BY clause for this column. Otherwise it reports NULL.|  
|**error_number**|**int**|Contains the error number returned by the function. Contains NULL if no error occurred in the column.|  
|**error_severity**|**int**|Contains the severity returned by the function. Contains NULL if no error occurred in the column.|  
|**error_state**|**int**|Contains the state message returned by the function. If no error occurred. the column will contain NULL.|  
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
|12|OBJECT_ID_NOT_SUPPORTED|The @object_id passed to the function is not supported (i.e. not a stored procedure)|  
|13|OBJECT_ID_DOES_NOT_EXIST|The @object_id passed to the function was not found in the system catalog.|  
  
## Permissions  
 Requires permission to execute the @tsql argument.  
  
## Examples  
  
### A. Returning metadata with and without browse information  
 The following example creates a stored procedure named TestProc2 that returns two result sets. Then the example demonstrates that **sys.dm_exec_describe_first_result_set** returns information about the first result set in the procedure, with and without the browse information.  
  
```  
CREATE PROC TestProc2  
AS  
SELECT object_id, name FROM sys.objects ;  
SELECT name, schema_id, create_date FROM sys.objects ;  
GO  
  
SELECT * FROM sys.dm_exec_describe_first_result_set_for_object(OBJECT_ID('TestProc2'), 0) ;  
SELECT * FROM sys.dm_exec_describe_first_result_set_for_object(OBJECT_ID('TestProc2'), 1) ;  
GO  
```  
  
### B. Combining the sys.dm_exec_describe_first_result_set_for_object function and a table or view  
 The following example uses both the sys.procedures system catalog view and the **sys.dm_exec_describe_first_result_set_for_object** function to display metadata for the result sets of all stored procedures in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
USE AdventureWorks2012;  
GO  
  
SELECT p.name, r.*   
FROM sys.procedures AS p  
CROSS APPLY sys.dm_exec_describe_first_result_set_for_object(p.object_id, 0) AS r;  
GO  
  
```  
  
## See Also  
 [sp_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md)   
 [sp_describe_undeclared_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md)   
 [sys.dm_exec_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md)  
  
  
