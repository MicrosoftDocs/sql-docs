---
title: "Use Sparse Columns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "sparse columns, described"
  - "null columns"
  - "sparse columns"
ms.assetid: ea7ddb87-f50b-46b6-9f5a-acab222a2ede
author: stevestein
ms.author: sstein
manager: craigg
---
# Use Sparse Columns
  Sparse columns are ordinary columns that have an optimized storage for null values. Sparse columns reduce the space requirements for null values at the cost of more overhead to retrieve nonnull values. Consider using sparse columns when the space saved is at least 20 percent to 40 percent. Sparse columns and column sets are defined by using the [CREATE TABLE](/sql/t-sql/statements/create-table-transact-sql) or [ALTER TABLE](/sql/t-sql/statements/alter-table-transact-sql) statements.  
  
 Sparse columns can be used with column sets and filtered indexes:  
  
-   Column sets  
  
     INSERT, UPDATE, and DELETE statements can reference the sparse columns by name. However, you can also view and work with all the sparse columns of a table that are combined into a single XML column. This column is called a column set. For more information about column sets, see [Use Column Sets](../tables/use-column-sets.md).  
  
-   Filtered indexes  
  
     Because sparse columns have many null-valued rows, they are especially appropriate for filtered indexes. A filtered index on a sparse column can index only the rows that have populated values. This creates a smaller and more efficient index. For more information, see [Create Filtered Indexes](../indexes/indexes.md).  
  
 Sparse columns and filtered indexes enable applications, such as [!INCLUDE[winSPServ](../../includes/winspserv-md.md)], to efficiently store and access a large number of user-defined properties by using [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## Properties of Sparse Columns  
 Sparse columns have the following characteristics:  
  
-   The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] uses the SPARSE keyword in a column definition to optimize the storage of values in that column. Therefore, when the column value is NULL for any row in the table, the values require no storage.  
  
-   Catalog views for a table that has sparse columns are the same as for a typical table. The sys.columns catalog view contains a row for each column in the table and includes a column set if one is defined.  
  
-   Sparse columns are a property of the storage layer, rather than the logical table. Therefore a SELECT...INTO statement does not copy over the sparse column property into a new table.  
  
-   The COLUMNS_UPDATED function returns a `varbinary` value to indicate all the columns that were updated during a DML action. The bits that are returned by the COLUMNS_UPDATED function are as follows:  
  
    -   When a sparse column is explicitly updated, the corresponding bit for that sparse column is set to 1, and the bit for the column set is set to 1.  
  
    -   When a column set is explicitly updated, the bit for the column set is set to 1, and the bits for all the sparse columns in that table are set to 1.  
  
    -   For insert operations, all bits are set to 1.  
  
     For more information about columns sets, see [Use Column Sets](../tables/use-column-sets.md).  
  
 The following data types cannot be specified as SPARSE:  
  
|||  
|-|-|  
|`geography`|`text`|  
|`geometry`|`timestamp`|  
|`image`|`user-defined data types`|  
|`ntext`||  
  
## Estimated Space Savings by Data Type  
 Sparse columns require more storage space for nonnull values than the space required for identical data that is not marked SPARSE. The following tables show the space usage for each data type. The **NULL Percentage** column indicates what percent of the data must be NULL for a net space savings of 40 percent.  
  
 **Fixed-Length Data Types**  
  
|Data type|Nonsparse bytes|Sparse bytes|NULL percentage|  
|---------------|---------------------|------------------|---------------------|  
|`bit`|0.125|5|98%|  
|`tinyint`|1|5|86%|  
|`smallint`|2|6|76%|  
|`int`|4|8|64%|  
|`bigint`|8|12|52%|  
|`real`|4|8|64%|  
|`float`|8|12|52%|  
|`smallmoney`|4|8|64%|  
|`money`|8|12|52%|  
|`smalldatetime`|4|8|64%|  
|`datetime`|8|12|52%|  
|`uniqueidentifier`|16|20|43%|  
|`date`|3|7|69%|  
  
 **Precision-Dependent-Length Data Types**  
  
|Data type|Nonsparse bytes|Sparse bytes|NULL percentage|  
|---------------|---------------------|------------------|---------------------|  
|`datetime2(0)`|6|10|57%|  
|`datetime2(7)`|8|12|52%|  
|`time(0)`|3|7|69%|  
|`time(7)`|5|9|60%|  
|`datetimetoffset(0)`|8|12|52%|  
|`datetimetoffset (7)`|10|14|49%|  
|`decimal/numeric(1,s)`|5|9|60%|  
|`decimal/numeric(38,s)`|17|21|42%|  
|`vardecimal(p,s)`|Use the `decimal` type as a conservative estimate.|||  
  
 **Data-Dependent-Length Data Types**  
  
|Data type|Nonsparse bytes|Sparse bytes|NULL percentage|  
|---------------|---------------------|------------------|---------------------|  
|`sql_variant`|Varies with the underlying data type|||  
|`varchar` or `char`|2*|4*|60%|  
|`nvarchar` or `nchar`|2*|4*+|60%|  
|`varbinary` or `binary`|2*|4*|60%|  
|`xml`|2*|4*|60%|  
|`hierarchyid`|2*|4*|60%|  
  
 *The length is equal to the average of the data that is contained in the type, plus 2 or 4 bytes.  
  
## In-Memory Overhead Required for Updates to Sparse Columns  
 When designing tables with sparse columns, keep in mind that an additional 2 bytes of overhead are required for each non-null sparse column in the table when a row is being updated. As a result of this additional memory requirement, updates can fail unexpectedly with error 576 when the total row size, including this memory overhead, exceeds 8019, and no columns can be pushed off the row.  
  
 Consider the example of a table that has 600 sparse columns of type bigint. If there are 571 non-null columns, then the total size on disk is 571 * 12 = 6852 bytes. After including additional row overhead and the sparse column header, this increases to around 6895 bytes. The page still has around 1124 bytes available on disk. This can give the impression that additional columns can be updated successfully. However, during the update, there is additional overhead in memory which is 2\*(number of non-null sparse columns). In this example, including the additional overhead - 2 \* 571 = 1142 bytes - increases the row size on disk to around 8037 bytes. This size exceeds the maximum allowed size of 8019 bytes. Since all the columns are fixed-length data types, they cannot be pushed off the row. As a result, the update fails with the 576 error.  
  
## Restrictions for Using Sparse Columns  
 Sparse columns can be of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type and behave like any other column with the following restrictions:  
  
-   A sparse column must be nullable and cannot have the ROWGUIDCOL or IDENTITY properties. A sparse column cannot be of the following data types: `text`, `ntext`, `image`, `timestamp`, user-defined data type, `geometry`, or `geography`; or have the FILESTREAM attribute.  
  
-   A sparse column cannot have a default value.  
  
-   A sparse column cannot be bound to a rule.  
  
-   Although a computed column can contain a sparse column, a computed column cannot be marked as SPARSE.  
  
-   A sparse column cannot be part of a clustered index or a unique primary key index. However, both persisted and nonpersisted computed columns that are defined on sparse columns can be part of a clustered key.  
  
-   A sparse column cannot be used as a partition key of a clustered index or heap. However, a sparse column can be used as the partition key of a nonclustered index.  
  
-   A sparse column cannot be part of a user-defined table type, which are used in table variables and table-valued parameters.  
  
-   Sparse columns are incompatible with data compression. Therefore sparse columns cannot be added to compressed tables, nor can any tables containing sparse columns be compressed.  
  
-   Changing a column from sparse to nonsparse or nonsparse to sparse requires changing the storage format of the column. The SQL Server Database Engine uses the following procedure to accomplish this change:  
  
    1.  Adds a new column to the table in the new storage size and format.  
  
    2.  For each row in the table, updates and copies the value stored in the old column to the new column.  
  
    3.  Removes the old column from the table schema.  
  
    4.  Rebuilds the table (if there is no clustered index) or rebuilds the clustered index to reclaim space used by the old column.  
  
    > [!NOTE]  
    >  Step 2 can fail when the size of the data in the row exceeds the maximum allowable row size. This size includes the size of the data stored in the old column and the updated data stored in the new column. This limit is 8060 bytes for tables that do not contain any sparse columns or 8018 bytes for tables that contain sparse columns. This error can occur even if all eligible columns have been pushed off-row.  
  
-   When you change a non-sparse column to a sparse column, the sparse column will consume more space for non-null values. When a row is close to the maximum row size limit, the operation can fail.  
  
## SQL Server Technologies That Support Sparse Columns  
 This section describes how sparse columns are supported in the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technologies:  
  
-   Transactional replication  
  
     Transactional replication supports sparse columns, but it does not support column sets, which can be used with sparse columns. For more information about column sets, see [Use Column Sets](../tables/use-column-sets.md).  
  
     The replication of the SPARSE attribute is determined by a schema option that is specified by using [sp_addarticle](/sql/relational-databases/system-stored-procedures/sp-addarticle-transact-sql) or by using the **Article Properties** dialog box in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] do not support sparse columns. If you must replicate data to an earlier version, specify that the SPARSE attribute should not be replicated.  
  
     For tables that are published, you cannot add any new sparse columns to a table or change the sparse property of an existing column. If such an operation is required, drop and re-create the publication.  
  
-   Merge replication  
  
     Merge replication does not support sparse columns or column sets.  
  
-   Change tracking  
  
     Change tracking supports sparse columns and column sets. When a column set is updated in a table, change tracking treats this as an update to the whole row. No detailed change tracking is provided to obtain the exact set of sparse columns that are updated through the column set update operation. If the sparse columns are updated explicitly through a DML statement, change tracking on them will work ordinarily and can identify the exact set of changed columns.  
  
-   Change data capture  
  
     Change data capture supports sparse columns, but it does not support column sets.  
  
-   The sparse property of a column is not preserved when the table is copied.  
  
## Examples  
 In this example, a document table contains a common set that has the columns `DocID` and `Title`. The Production group wants a `ProductionSpecification` and `ProductionLocation` column for all production documents. The Marketing group wants a `MarketingSurveyGroup` column for marketing documents. The code in this example creates a table that uses sparse columns, inserts two rows into the table, and then selects data from the table.  
  
> [!NOTE]  
>  This table has only five columns to make it easier to display and read. Declaring the sparse columns to be nullable is optional if the ANSI_NULL_DFLT_ON option is set.  
  
```  
USE AdventureWorks2012;  
GO  
  
CREATE TABLE DocumentStore  
    (DocID int PRIMARY KEY,  
     Title varchar(200) NOT NULL,  
     ProductionSpecification varchar(20) SPARSE NULL,  
     ProductionLocation smallint SPARSE NULL,  
     MarketingSurveyGroup varchar(20) SPARSE NULL ) ;  
GO  
  
INSERT DocumentStore(DocID, Title, ProductionSpecification, ProductionLocation)  
VALUES (1, 'Tire Spec 1', 'AXZZ217', 27);  
GO  
  
INSERT DocumentStore(DocID, Title, MarketingSurveyGroup)  
VALUES (2, 'Survey 2142', 'Men 25 - 35');  
GO  
```  
  
 To select all the columns from the table returns an ordinary result set.  
  
```  
SELECT * FROM DocumentStore ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `DocID  Title        ProductionSpecification  ProductionLocation  MarketingSurveyGroup`  
  
 `1      Tire Spec 1  AXZZ217                  27                  NULL`  
  
 `2      Survey 2142  NULL                     NULL                Men 25-35`  
  
 Because the Production department is not interested in the marketing data, they want to use a column list that returns only columns of interest, as shown in the following query.  
  
```  
SELECT DocID, Title, ProductionSpecification, ProductionLocation   
FROM DocumentStore   
WHERE ProductionSpecification IS NOT NULL ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `DocID  Title        ProductionSpecification  ProductionLocation`  
  
 `1      Tire Spec 1  AXZZ217                  27`  
  
## See Also  
 [Use Column Sets](../tables/use-column-sets.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql)   
 [ALTER TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-table-transact-sql)   
 [sys.columns &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-columns-transact-sql)  
  
  
