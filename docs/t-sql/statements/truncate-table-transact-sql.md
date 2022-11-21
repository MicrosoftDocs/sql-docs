---
title: "TRUNCATE TABLE (Transact-SQL)"
description: TRUNCATE TABLE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "TRUNCATE"
  - "TRUNCATE TABLE"
  - "TRUNCATE_TSQL"
  - "TRUNCATE_TABLE_TSQL"
helpviewer_keywords:
  - "row removal [SQL Server], TRUNCATE TABLE statement"
  - "table truncating [SQL Server]"
  - "removing rows"
  - "TRUNCATE TABLE statement"
  - "deleting rows"
  - "dropping rows"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# TRUNCATE TABLE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Removes all rows from a table or specified partitions of a table, without logging the individual row deletions. TRUNCATE TABLE is similar to the DELETE statement with no WHERE clause; however, TRUNCATE TABLE is faster and uses fewer system and transaction log resources.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
TRUNCATE TABLE   
    { database_name.schema_name.table_name | schema_name.table_name | table_name }  
    [ WITH ( PARTITIONS ( { <partition_number_expression> | <range> }   
    [ , ...n ] ) ) ]  
[ ; ]  
  
<range> ::=  
<partition_number_expression> TO <partition_number_expression>  
```  
  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
TRUNCATE TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }  
[;]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *database_name*  
 Is the name of the database.  
  
#### *schema_name*  
 Is the name of the schema to which the table belongs.  
  
#### *table_name*  
 Is the name of the table to truncate or from which all rows are removed. *table_name* must be a literal. *table_name* cannot be the **OBJECT_ID()** function or a variable.  
  
#### WITH ( PARTITIONS ( { \<*partition_number_expression*> | \<*range*> } [ , ...n ] ) )    
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level))
  
 Specifies the partitions to truncate or from which all rows are removed. If the table is not partitioned, the `WITH PARTITIONS` argument will generate an error. If the `WITH PARTITIONS` clause is not provided, the entire table will be truncated.  
  
 *\<partition_number_expression>* can be specified in the following ways: 
  
-   Provide the number of a partition, for example: `WITH (PARTITIONS (2))`  
  
-   Provide the partition numbers for several individual partitions separated by commas, for example: `WITH (PARTITIONS (1, 5))`  
  
-   Provide both ranges and individual partitions, for example: `WITH (PARTITIONS (2, 4, 6 TO 8))`  
  
-   *\<range>* can be specified as partition numbers separated by the word **TO**, for example: `WITH (PARTITIONS (6 TO 8))`  
  
 To truncate a partitioned table, the table and indexes must be aligned (partitioned on the same partition function).  
  
## Remarks  
 Compared to the DELETE statement, `TRUNCATE TABLE` has the following advantages:  
  
-   Less transaction log space is used.  
  
     The DELETE statement removes rows one at a time and records an entry in the transaction log for each deleted row. `TRUNCATE TABLE` removes the data by deallocating the data pages used to store the table data and records only the page deallocations in the transaction log.  
  
-   Fewer locks are typically used.  
  
     When the DELETE statement is executed using a row lock, each row in the table is locked for deletion. `TRUNCATE TABLE` always locks the table (including a schema (SCH-M) lock) and page but not each row.  
  
-   Without exception, zero pages are left in the table.  
  
     After a DELETE statement is executed, the table can still contain empty pages. For example, empty pages in a heap cannot be deallocated without at least an exclusive (LCK_M_X) table lock. If the delete operation does not use a table lock, the table (heap) will contain many empty pages. For indexes, the delete operation can leave empty pages behind, although these pages will be deallocated quickly by a background cleanup process.  
  
 `TRUNCATE TABLE` removes all rows from a table, but the table structure and its columns, constraints, indexes, and so on remain. To remove the table definition in addition to its data, use the `DROP TABLE` statement.  
  
 If the table contains an identity column, the counter for that column is reset to the seed value defined for the column. If no seed was defined, the default value 1 is used. To retain the identity counter, use DELETE instead.  
 
 > [!NOTE]
 > A `TRUNCATE TABLE` operation can be rolled back.
  
## Restrictions  
 You cannot use `TRUNCATE TABLE` on tables that:  
  
-   Are referenced by a FOREIGN KEY constraint. You can truncate a table that has a foreign key that references itself. 
  
-   Participate in an indexed view.  
  
-   Are published by using transactional replication or merge replication.  

-   Are system-versioned temporal.

-   Are referenced by an EDGE constraint.  
  
 For tables with one or more of these characteristics, use the DELETE statement instead.  
  
 TRUNCATE TABLE cannot activate a trigger because the operation does not log individual row deletions. For more information, see [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md). 
 
 In [!INCLUDE[sssdwfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[sspdw](../../includes/sspdw-md.md)]:

- `TRUNCATE TABLE` is not allowed within the EXPLAIN statement.

- `TRUNCATE TABLE` cannot be ran inside of a transaction.
  
## Truncating Large Tables  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has the ability to drop or truncate tables that have more than 128 extents without holding simultaneous locks on all the extents required for the drop.  
  
## Permissions  
 The minimum permission required is `ALTER` on *table_name*. `TRUNCATE TABLE` permissions default to the table owner, members of the `sysadmin` fixed server role, and the `db_owner` and `db_ddladmin` fixed database roles, and are not transferable. However, you can incorporate the `TRUNCATE TABLE` statement within a module, such as a stored procedure, and grant appropriate permissions to the module using the `EXECUTE AS` clause.  
  
## Examples  
  
### A. Truncate a Table  
 The following example removes all data from the `JobCandidate` table. `SELECT` statements are included before and after the `TRUNCATE TABLE` statement to compare results.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT COUNT(*) AS BeforeTruncateCount   
FROM HumanResources.JobCandidate;  
GO  
TRUNCATE TABLE HumanResources.JobCandidate;  
GO  
SELECT COUNT(*) AS AfterTruncateCount   
FROM HumanResources.JobCandidate;  
GO  
```  
  
### B. Truncate Table Partitions  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level))
  
 The following example truncates specified partitions of a partitioned table. The `WITH (PARTITIONS (2, 4, 6 TO 8))` syntax causes partition numbers 2, 4, 6, 7, and 8 to be truncated.  
  
```sql  
TRUNCATE TABLE PartitionTable1   
WITH (PARTITIONS (2, 4, 6 TO 8));  
GO  
```  
  
## See Also  
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md)   
 [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql-identity-property.md)  
