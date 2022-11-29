---
description: "sp_describe_first_result_set (Transact-SQL)"
title: "sp_describe_first_result_set (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_describe_first_result_set"
  - "sp_describe_first_result_set_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_describe_first_result_set"
ms.assetid: f2355a75-3a8e-43e6-96ad-4f41038f6d22
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_describe_first_result_set (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the metadata for the first possible result set of the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch. Returns an empty result set if the batch returns no results. Raises an error if the [!INCLUDE[ssDE](../../includes/ssde-md.md)] cannot determine the metadata for the first query that will be executed by performing a static analysis. The dynamic management view [sys.dm_exec_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md) returns the same information.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_describe_first_result_set [ @tsql = ] N'Transact-SQL_batch'   
    [ , [ @params = ] N'parameters' ]   
    [ , [ @browse_information_mode = ] <tinyint> ] ]  
```  
  
## Arguments  
`[ @tsql = ] 'Transact-SQL_batch'`
 One or more [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. *Transact-SQL_batch* may be **nvarchar(***n***)** or **nvarchar(max)**.  
  
`[ @params = ] N'parameters'`
 \@params provides a declaration string for parameters for the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch, which is similar to sp_executesql. Parameters may be **nvarchar(n)** or **nvarchar(max)**.  
  
 Is one string that contains the definitions of all parameters that have been embedded in the [!INCLUDE[tsql](../../includes/tsql-md.md)]*_batch*. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in the statement must be defined in \@params. If the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch in the statement does not contain parameters, \@params is not required. NULL is the default value for this parameter.  
  
`[ @browse_information_mode = ] tinyint`
 Specifies if additional key columns and source table information are returned. If set to 1, each query is analyzed as if it includes a FOR BROWSE option on the query. Additional key columns and source table information are returned.  
  
-   If set to 0, no information is returned.  
  
-   If set to 1, each query is analyzed as if it includes a FOR BROWSE option on the query. This will return base table names as the source column information.  
  
-   If set to 2, each query is analyzed as if it would be used in preparing or executing a cursor. This will return view names as source column information.  
  
## Return Code Values  
 **sp_describe_first_result_set** always returns a status of zero on success. If the procedure throws an error and the procedure is called as an RPC, return status is populated by the type of error described in the error_type column of sys.dm_exec_describe_first_result_set. If the procedure is called from [!INCLUDE[tsql](../../includes/tsql-md.md)], the return value is always zero, even when there is an error.  
  
## Result Sets  
 This common metadata is returned as a result set with one row for each column in the results metadata. Each row describes the type and nullability of the column in the format described in the following section. If the first statement does not exist for every control path, a result set with zero rows is returned.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**is_hidden**|**bit NOT NULL**|Indicates that the column is an extra column added for browsing information purposes and that it does not actually appear in the result set.|  
|**column_ordinal**|**int NOT NULL**|Contains the ordinal position of the column in the result set. The first column's position will be specified as 1.|  
|**name**|**sysname NULL**|Contains the name of the column if a name can be determined. Otherwise, it will contain NULL.|  
|**is_nullable**|**bit NOT NULL**|Contains the value 1 if the column allows NULLs, 0 if the column does not allow NULLs, and 1 if it cannot be determined if the column allows NULLs.|  
|**system_type_id**|**int NOT NULL**|Contains the system_type_id of the data type of the column as specified in sys.types. For CLR types, even though the system_type_name column will return NULL, this column will return the value 240.|  
|**system_type_name**|**nvarchar(256) NULL**|Contains the name and arguments (such as length, precision, scale), specified for the data type of the column. If the data type is a user-defined alias type, the underlying system type is specified here. If it is a CLR user-defined type, NULL is returned in this column.|  
|**max_length**|**smallint NOT NULL**|Maximum length (in bytes) of the column.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br /> For **text** columns, the **max_length** value will be 16 or the value set by **sp_tableoption 'text in row'**.|  
|**precision**|**tinyint NOT NULL**|Precision of the column if numeric-based. Otherwise returns 0.|  
|**scale**|**tinyint NOT NULL**|Scale of column if numeric-based. Otherwise returns 0.|  
|**collation_name**|**sysname NULL**|Name of the collation of the column if character-based. Otherwise returns NULL.|  
|**user_type_id**|**int NULL**|For CLR and alias types, contains the user_type_id of the data type of the column as specified in sys.types. Otherwise is NULL.|  
|**user_type_database**|**sysname NULL**|For CLR and alias types, contains the name of the database in which the type is defined. Otherwise is NULL.|  
|**user_type_schema**|**sysname NULL**|For CLR and alias types, contains the name of the schema in which the type is defined. Otherwise is NULL.|  
|**user_type_name**|**sysname NULL**|For CLR and alias types, contains the name of the type. Otherwise is NULL.|  
|**assembly_qualified_type_name**|**nvarchar(4000)**|For CLR types, returns the name of the assembly and class defining the type. Otherwise is NULL.|  
|**xml_collection_id**|**int NULL**|Contains the xml_collection_id of the data type of the column as specified in sys.columns. This column will return NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_database**|**sysname NULL**|Contains the database in which the XML schema collection associated with this type is defined. This column will return NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_schema**|**sysname NULL**|Contains the schema in which the XML schema collection associated with this type is defined. This column will return NULL if the type returned is not associated with an XML schema collection.|  
|**xml_collection_name**|**sysname NULL**|Contains the name of the XML schema collection associated with this type. This column will return NULL if the type returned is not associated with an XML schema collection.|  
|**is_xml_document**|**bit NOT NULL**|Returns 1 if the returned data type is XML and that type is guaranteed to be a complete XML document (including a root node), as opposed to an XML fragment). Otherwise returns 0.|  
|**is_case_sensitive**|**bit NOT NULL**|Returns 1 if the column is a case-sensitive string type and 0 if it is not.|  
|**is_fixed_length_clr_type**|**bit NOT NULL**|Returns 1 if the column is a fixed-length CLR type and 0 if it is not.|  
|**source_server**|**sysname**|Name of the originating server returned by the column in this result (if it originates from a remote server). The name is given as it appears in sys.servers. Returns NULL if the column originates on the local server or if it cannot be determined which server it originates on. Is only populated if browsing information is requested.|  
|**source_database**|**sysname**|Name of the originating database returned by the column in this result. Returns NULL if the database cannot be determined. Is only populated if browsing information is requested.|  
|**source_schema**|**sysname**|Name of the originating schema returned by the column in this result. Returns NULL if the schema cannot be determined. Is only populated if browsing information is requested.|  
|**source_table**|**sysname**|Name of the originating table returned by the column in this result. Returns NULL if the table cannot be determined. Is only populated if browsing information is requested.|  
|**source_column**|**sysname**|Name of the originating column returned by the result column. Returns NULL if the column cannot be determined. Is only populated if browsing information is requested.|  
|**is_identity_column**|**bit NULL**|Returns 1 if the column is an identity column and 0 if not. Returns NULL if it cannot be determined that the column is an identity column.|  
|**is_part_of_unique_key**|**bit NULL**|Returns 1 if the column is part of a unique index (including unique and primary constraint) and 0 if not. Returns NULL if it cannot be determined that the column is part of a unique index. Only populated if browsing information is requested.|  
|**is_updateable**|**bit NULL**|Returns 1 if the column is updateable and 0 if not. Returns NULL if it cannot be determined that the column is updateable.|  
|**is_computed_column**|**bit NULL**|Returns 1 if the column is a computed column and 0 if not. Returns NULL if it cannot be determined that the column is a computed column.|  
|**is_sparse_column_set**|**bit NULL**|Returns 1 if the column is a sparse column and 0 if not. Returns NULL if it cannot be determined that the column is part of a sparse column set.|  
|**ordinal_in_order_by_list**|**smallint NULL**|Position of this column in ORDER BY list. Returns NULL if the column does not appear in the ORDER BY list or if the ORDER BY list cannot be uniquely determined.|  
|**order_by_list_length**|**smallint NULL**|Length of the ORDER BY list. Returns NULL if there is no ORDER BY list or if the ORDER BY list cannot be uniquely determined. Note that this value will be the same for all rows returned by **sp_describe_first_result_set.**|  
|**order_by_is_descending**|**smallint NULL**|If the ordinal_in_order_by_list is not NULL, the **order_by_is_descending** column reports the direction of the ORDER BY clause for this column. Otherwise it reports NULL.|  
|**tds_type_id**|**int NOT NULL**|For internal use.|  
|**tds_length**|**int NOT NULL**|For internal use.|  
|**tds_collation_id**|**int NULL**|For internal use.|  
|**tds_collation_sort_id**|**tinyint NULL**|For internal use.|  
  
## Remarks  
 **sp_describe_first_result_set** guarantees that if the procedure returns the first result-set metadata for (a hypothetical) batch A and if that batch (A) is subsequently executed then the batch will either (1) raises an optimization-time error, (2) raises a run-time error, (3) returns no result set, or (4) returns a first result set with the same metadata described by **sp_describe_first_result_set**.  
  
 The name, nullability, and data type can differ. If **sp_describe_first_result_set** returns an empty result set, the guarantee is that the batch execution will return no-result sets.  
  
 This guarantee presumes there are no relevant schema changes on the server. Relevant schema changes on the server do not include creating a temporary tables or table variables in the batch A between the time that **sp_describe_first_result_set** is called and the time that the result set is returned during execution, including schema changes made by batch B.  
  
 **sp_describe_first_result_set** returns an error in any of the following cases.  
  
-   If the input \@tsql is not a valid [!INCLUDE[tsql](../../includes/tsql-md.md)] batch. Validity is determined by parsing and analyzing the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch. Any errors caused by the batch during query optimization or during execution are not considered when determining whether the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch is valid.  
  
-   If \@params is not NULL and contains a string that is not a syntactically valid declaration string for parameters, or if it contains a string that declares any parameter more than one time.  
  
-   If the input [!INCLUDE[tsql](../../includes/tsql-md.md)] batch declares a local variable of the same name as a parameter declared in \@params.  
  
-   If the statement uses a temporary table.  
  
-   The query includes the creation of a permanent table that is then queried.  
  
 If all other checks succeed, all possible control flow paths inside the input batch are considered. This takes into account all control flow statements (GOTO, IF/ELSE, WHILE, and [!INCLUDE[tsql](../../includes/tsql-md.md)] TRY/CATCH blocks) as well as any procedures, dynamic [!INCLUDE[tsql](../../includes/tsql-md.md)] batches, or triggers invoked from the input batch by an EXEC statement, a DDL statement that causes DDL triggers to be fired, or a DML statement that causes triggers to be fired on a target table or on a table that is modified because of cascading action on a foreign key constraint. In the case of many possible control paths, at some point an algorithm stops.  
  
 For each control flow path, the first statement (if any) that returns a result set is determined by **sp_describe_first_result_set**.  
  
 When multiple possible first statements are found in a batch, their results can differ in number of columns, column name, nullability, and data type. How these differences are handled is described in more detail here:  
  
-   If the number of columns differs, an error is thrown and no result is returned.  
  
-   If the column name differs, the column name returned is set to NULL.  
  
-   It the nullability differs, the nullability returned will allow NULLs.  
  
-   If the data type differs, an error will be thrown and no result is returned except for the following cases:  
  
    -   **varchar(a)** to **varchar(a')** where a' > a.  
  
    -   **varchar(a)** to **varchar(max)**  
  
    -   **nvarchar(a)** to **nvarchar(a')** where a' > a.  
  
    -   **nvarchar(a)** to **nvarchar(max)**  
  
    -   **varbinary(a)** to **varbinary(a')** where a' > a.  
  
    -   **varbinary(a)** to **varbinary(max)**  
  
 **sp_describe_first_result_set** does not support indirect recursion.  
  
## Permissions  
 Requires permission to execute the \@tsql argument.  
  
## Examples  
  
### Typical Examples  
  
#### A. Simple Example  
 The following example describes the result set returned from a single query.  
  
```  
sp_describe_first_result_set @tsql = N'SELECT object_id, name, type_desc FROM sys.indexes'  
```  
  
 The following example shows the result set returned from a single query that contains a parameter.  
  
```  
sp_describe_first_result_set @tsql =   
N'SELECT object_id, name, type_desc   
FROM sys.indexes   
WHERE object_id = @id1'  
, @params = N'@id1 int'  
```  
  
#### B. Browse Mode Examples  
 The following three examples illustrate the key difference between the different browse information modes. Only the relevant columns have been included in the query results.  
  
 Example using 0 indicating no information is returned.  
  
```sql  
CREATE TABLE dbo.t (a int PRIMARY KEY, b1 int);  
GO  
CREATE VIEW dbo.v AS SELECT b1 AS b2 FROM dbo.t;  
GO  
EXEC sp_describe_first_result_set N'SELECT b2 AS b3 FROM dbo.v', null, 0;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
|is_hidden|column_ordinal|name|source_schema|source_table|source_column|is_part_of_unique_key|  
|----------------|---------------------|----------|--------------------|-------------------|--------------------|-------------------------------|  
|0|1|b3|NULL|NULL|NULL|NULL|  
  
 Example using 1 indicating it returns information as if it includes a FOR BROWSE option on the query.  
  
```sql  
EXEC sp_describe_first_result_set N'SELECT b2 AS b3 FROM v', null, 1  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
|is_hidden|column_ordinal|name|source_schema|source_table|source_column|is_part_of_unique_key|  
|----------------|---------------------|----------|--------------------|-------------------|--------------------|-------------------------------|  
|0|1|b3|dbo|t|B1|0|  
|1|2|a|dbo|t|a|1|  
  
 Example using 2 indicating analyzed as if you are preparing a cursor.  
  
```sql  
EXEC sp_describe_first_result_set N'SELECT b2 AS b3 FROM v', null, 2  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
|is_hidden|column_ordinal|name|source_schema|source_table|source_column|is_part_of_unique_key|  
|----------------|---------------------|----------|--------------------|-------------------|--------------------|-------------------------------|  
|0|1|B3|dbo|v|B2|0|  
|1|2|ROWSTAT|NULL|NULL|NULL|0|  
  
### C. Storing results in a table

In some scenarios you would need to put the results of the `sp_describe_first_result_set` procedure in some table so your can further process the schema. 
First you need to create a table that matches the output of the `sp_describe_first_result_set` procedure:

```sql
create table #frs (
    is_hidden bit not null,
    column_ordinal int not null,
    name sysname null,
    is_nullable bit not null,
    system_type_id int not null,
    system_type_name nvarchar(256) null,
    max_length smallint not null,
    precision tinyint not null,
    scale tinyint not null,
    collation_name sysname null,
    user_type_id int null,
    user_type_database sysname null,
    user_type_schema sysname null,
    user_type_name sysname null,
    assembly_qualified_type_name nvarchar(4000),
    xml_collection_id int null,
    xml_collection_database sysname null,
    xml_collection_schema sysname null,
    xml_collection_name sysname null,
    is_xml_document bit not null,
    is_case_sensitive bit not null,
    is_fixed_length_clr_type bit not null,
    source_server sysname null,
    source_database sysname null,
    source_schema sysname null,
    source_table sysname null,
    source_column sysname null,
    is_identity_column bit null,
    is_part_of_unique_key bit null,
    is_updateable bit null,
    is_computed_column bit null,
    is_sparse_column_set bit null,
    ordinal_in_order_by_list smallint null,
    order_by_list_length smallint null,
    order_by_is_descending smallint null,
    tds_type_id int not null,
    tds_length int not null,
    tds_collation_id int null,
    tds_collation_sort_id tinyint null
);
```

When you create a table, you can store the schema of some query in that table.

```sql
declare @tsql nvarchar(max) = 'select top 0 * from sys.credentials';

insert #frs
exec sys.sp_describe_first_result_set @tsql;

select * from #frs;
```
  
### Examples of problems  
 The following examples use two tables for all examples. Execute the following statements to create the example tables.  
  
```  
CREATE TABLE dbo.t1 (a int NULL, b varchar(10) NULL, c nvarchar(10) NULL);  
CREATE TABLE dbo.t2 (a smallint NOT NULL, d varchar(20) NOT NULL, e int NOT NULL);  
```  
  
#### Error because the number of columns differ  
 Number of columns in possible first result sets differ in this example.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
IF(1=1)  
    SELECT a FROM t1;  
ELSE  
    SELECT a, b FROM t1;  
SELECT * FROM t; -- Ignored, not a possible first result set.'  
  
```  
  
#### Error because the data types differ  
 Columns types differ in different possible first result sets.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
IF(1=1)  
    SELECT a FROM t1;  
ELSE  
    SELECT a FROM t2;  
```  
  
 Result: Error, mismatching types (**int** vs. **smallint**).  
  
#### Column name cannot be determined  
 Columns in possible first result sets differ by length for same variable length type, nullability, and column names:  
  
```  
sp_describe_first_result_set @tsql =   
N'  
IF(1=1)  
    SELECT b FROM t1;  
ELSE  
    SELECT d FROM t2; '  
```  
  
 Result: \<Unknown Column Name> **varchar(20) NULL**  
  
#### Column name forced to be identical through aliasing  
 Same as previous, but columns have the same name through column aliasing.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
IF(1=1)  
    SELECT b FROM t1;  
ELSE  
    SELECT d AS b FROM t2;'  
```  
  
 Result: b **varchar(20)NULL**  
  
#### Error because column types cannot be matched  
 The columns types differ in different possible first result sets.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
IF(1=1)  
    SELECT b FROM t1;  
ELSE  
    SELECT c FROM t1;'  
```  
  
 Result: Error, mismatching types (**varchar(10)** vs. **nvarchar(10)**).  
  
#### Result set can return an error  
 First result set is either error or result set.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
IF(1=1)  
    RAISERROR(''Some Error'', 16, 1);  
  
ELSE  
    SELECT a FROM t1;  
SELECT e FROM t2; -- Ignored, not a possible first result set.;'  
```  
  
 Result: a **intNULL**  
  
#### Some code paths return no results  
 First result set is either null or a result set.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
IF(1=1)  
    RETURN;  
SELECT a FROM t1;'  
```  
  
 Result: a **intNULL**  
  
#### Result from dynamic SQL  
 First result set is dynamic SQL that is discoverable because it is a literal string.  
  
```  
sp_describe_first_result_set @tsql =   
N'EXEC(N''SELECT a FROM t1'');'  
```  
  
 Result: a **INT NULL**  
  
#### Result failure from dynamic SQL  
 First result set is undefined because of dynamic SQL.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
DECLARE @SQL NVARCHAR(max);  
SET @SQL = N''SELECT a FROM t1 WHERE 1 = 1 '';  
IF(1=1)  
    SET @SQL += N'' AND e > 10 '';  
EXEC(@SQL); '  
```  
  
 Result: Error. Result is not discoverable because of the dynamic SQL.  
  
#### Result set specified by user  
 First result set is specified manually by user.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
DECLARE @SQL NVARCHAR(max);  
SET @SQL = N''SELECT a FROM t1 WHERE 1 = 1 '';  
IF(1=1)  
    SET @SQL += N'' AND e > 10 '';  
EXEC(@SQL)  
    WITH RESULT SETS(  
        (Column1 BIGINT NOT NULL)  
    ); '  
```  
  
 Result: Column1 **bigint NOT NULL**  
  
#### Error caused by a ambiguous result set  
 This example assumes that another user named user1 has a table named t1 in the default schema s1 with columns (a **int NOT NULL**).  
  
```  
sp_describe_first_result_set @tsql =   
N'  
    IF(@p > 0)  
    EXECUTE AS USER = ''user1'';  
    SELECT * FROM t1;'  
, @params = N'@p int'  
```  
  
 Result: Error. t1 can be either dbo.t1 or s1.t1, each with a different number of columns.  
  
#### Result even with ambiguous result set  
 Use the same assumptions as the previous example.  
  
```  
sp_describe_first_result_set @tsql =   
N'  
    IF(@p > 0)  
    EXECUTE AS USER = ''user1'';  
    SELECT a FROM t1;'  
```  
  
 Result: a **int NULL** because both dbo.t1.a and s1.t1.a have type **int** and different nullability.  
  
## See Also  
 [sp_describe_undeclared_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-undeclared-parameters-transact-sql.md)   
 [sys.dm_exec_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-transact-sql.md)   
 [sys.dm_exec_describe_first_result_set_for_object &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-describe-first-result-set-for-object-transact-sql.md)  
 
