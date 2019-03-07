---
title: "DROP INDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/11/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_INDEX_TSQL"
  - "DROP INDEX"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "nonclustered indexes [SQL Server], removing"
  - "MAXDOP index option, DROP INDEX statement"
  - "index removal [SQL Server]"
  - "spatial indexes [SQL Server], dropping"
  - "removing indexes"
  - "deleting indexes"
  - "dropping indexes"
  - "MOVE TO clause"
  - "clustered indexes, removing"
  - "indexes [SQL Server], dropping"
  - "filtered indexes [SQL Server], dropping"
  - "ONLINE option"
  - "indexes [SQL Server], moving"
  - "XML indexes [SQL Server], dropping"
  - "DROP INDEX statement"
ms.assetid: 2b1464c8-934c-405f-8ef7-2949346b5372
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP INDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes one or more relational, spatial, filtered, or XML indexes from the current database. You can drop a clustered index and move the resulting table to another filegroup or partition scheme in a single transaction by specifying the MOVE TO option.  
  
 The DROP INDEX statement does not apply to indexes created by defining PRIMARY KEY or UNIQUE constraints. To remove the constraint and corresponding index, use [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) with the DROP CONSTRAINT clause.  
  
> [!IMPORTANT]
>  The syntax defined in `<drop_backward_compatible_index>` will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this syntax in new development work, and plan to modify applications that currently use the feature. Use the syntax specified under `<drop_relational_or_xml_index>` instead. XML indexes cannot be dropped using backward compatible syntax.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server (All options except filegroup and filestream apply to Azure SQL Database.)  
  
DROP INDEX [ IF EXISTS ]   
{ <drop_relational_or_xml_or_spatial_index> [ ,...n ]   
| <drop_backward_compatible_index> [ ,...n ]  
}  
  
<drop_relational_or_xml_or_spatial_index> ::=  
    index_name ON <object>   
    [ WITH ( <drop_clustered_index_option> [ ,...n ] ) ]  
  
<drop_backward_compatible_index> ::=  
    [ owner_name. ] table_or_view_name.index_name  
  
<object> ::=  
{  
    [ database_name. [ schema_name ] . | schema_name. ]   
    table_or_view_name  
}  
  
<drop_clustered_index_option> ::=  
{  
    MAXDOP = max_degree_of_parallelism  
  | ONLINE = { ON | OFF }  
  | MOVE TO { partition_scheme_name ( column_name )   
            | filegroup_name  
            | "default"   
            }  
  [ FILESTREAM_ON { partition_scheme_name   
            | filestream_filegroup_name   
            | "default" } ]  
}  
```  
  
```  
-- Syntax for Azure SQL Database  
  
DROP INDEX  
{ <drop_relational_or_xml_or_spatial_index> [ ,...n ]   
}  
  
<drop_relational_or_xml_or_spatial_index> ::=   
    index_name ON <object>  
  
<object> ::=   
{  
    [ database_name. [ schema_name ] . | schema_name. ]   
    table_or_view_name  
}  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
DROP INDEX index_name ON [ database_name . [schema_name ] . | schema_name . ] table_name  
[;]  
```  
  
## Arguments  
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).  
  
 Conditionally drops the index only if it already exists.  
  
 *index_name*  
 Is the name of the index to be dropped.  
  
 *database_name*  
 Is the name of the database.  
  
 *schema_name*  
 Is the name of the schema to which the table or view belongs.  
  
 *table_or_view_name*  
 Is the name of the table or view associated with the index. Spatial indexes are supported only on tables.  
  
 To display a report of the indexes on an object, use the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.  
  
 Windows Azure SQL Database supports the three-part name format database_name.[schema_name].object_name when the database_name is the current database or the database_name is tempdb and the object_name starts with #.  
  
 \<drop_clustered_index_option>  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Controls clustered index options. These options cannot be used with other index types.  
  
 MAXDOP = *max_degree_of_parallelism*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] (Performance Levels P2 and P3 only).  
  
 Overrides the **max degree of parallelism** configuration option for the duration of the index operation. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.  
  
> [!IMPORTANT]  
>  MAXDOP is not allowed for spatial indexes or XML indexes.  
  
 *max_degree_of_parallelism* can be:  
  
 1  
 Suppresses parallel plan generation.  
  
 \>1  
 Restricts the maximum number of processors used in a parallel index operation to the specified number.  
  
 0 (default)  
 Uses the actual number of processors or fewer based on the current system workload.  
  
 For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
> [!NOTE]  
>  Parallel index operations are not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 ONLINE = ON | **OFF**  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 Specifies whether underlying tables and associated indexes are available for queries and data modification during the index operation. The default is OFF.  
  
 ON  
 Long-term table locks are not held. This allows queries or updates to the underlying table to continue.  
  
 OFF  
 Table locks are applied and the table is unavailable for the duration of the index operation.  
  
 The ONLINE option can only be specified when you drop clustered indexes. For more information, see the Remarks section.  
  
> [!NOTE]  
>  Online index operations are not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 MOVE TO { _partition\_scheme\_name_**(**_column\_name_**)** | _filegroup\_name_ | **"**default**"**  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] supports "default" as the filegroup name.  
  
 Specifies a location to move the data rows that currently are in the leaf level of the clustered index. The data is moved to the new location in the form of a heap. You can specify either a partition scheme or filegroup as the new location, but the partition scheme or filegroup must already exist. MOVE TO is not valid for indexed views or nonclustered indexes. If a partition scheme or filegroup is not specified, the resulting table will be located in the same partition scheme or filegroup as was defined for the clustered index.  
  
 If a clustered index is dropped by using MOVE TO, any nonclustered indexes on the base table are rebuilt, but they remain in their original filegroups or partition schemes. If the base table is moved to a different filegroup or partition scheme, the nonclustered indexes are not moved to coincide with the new location of the base table (heap). Therefore, even if the nonclustered indexes were previously aligned with the clustered index, they might no longer be aligned with the heap. For more information about partitioned index alignment, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
 _partition_scheme_name_ **(** _column_name_ **)**  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies a partition scheme as the location for the resulting table. The partition scheme must have already been created by executing either [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md) or [ALTER PARTITION SCHEME](../../t-sql/statements/alter-partition-scheme-transact-sql.md). If no location is specified and the table is partitioned, the table is included in the same partition scheme as the existing clustered index.  
  
 The column name in the scheme is not restricted to the columns in the index definition. Any column in the base table can be specified.  
  
 *filegroup_name*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies a filegroup as the location for the resulting table. If no location is specified and the table is not partitioned, the resulting table is included in the same filegroup as the clustered index. The filegroup must already exist.  
  
 **"**default**"**  
 Specifies the default location for the resulting table.  
  
> [!NOTE]
>  In this context, default is not a keyword. It is an identifier for the default filegroup and must be delimited, as in MOVE TO **"**default**"** or MOVE TO **[**default**]**. If **"**default**"** is specified, the QUOTED_IDENTIFIER option must be set ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
 FILESTREAM_ON { *partition_scheme_name* | *filestream_filegroup_name* | **"**default**"** }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies a location to move the FILESTREAM table that currently is in the leaf level of the clustered index. The data is moved to the new location in the form of a heap. You can specify either a partition scheme or filegroup as the new location, but the partition scheme or filegroup must already exist. FILESTREAM ON is not valid for indexed views or nonclustered indexes. If a partition scheme is not specified, the data will be located in the same partition scheme as was defined for the clustered index.  
  
 *partition_scheme_name*  
 Specifies a partition scheme for the FILESTREAM data. The partition scheme must have already been created by executing either [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md) or [ALTER PARTITION SCHEME](../../t-sql/statements/alter-partition-scheme-transact-sql.md). If no location is specified and the table is partitioned, the table is included in the same partition scheme as the existing clustered index.  
  
 If you specify a partition scheme for MOVE TO, you must use the same partition scheme for FILESTREAM ON.  
  
 *filestream_filegroup_name*  
 Specifies a FILESTREAM filegroup for FILESTREAM data. If no location is specified and the table is not partitioned, the data is included in the default FILESTREAM filegroup.  
  
 **"**default**"**  
 Specifies the default location for the FILESTREAM data.  
  
> [!NOTE]
>  In this context, default is not a keyword. It is an identifier for the default filegroup and must be delimited, as in MOVE TO **"**default**"** or MOVE TO **[**default**]**. If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
## Remarks  
 When a nonclustered index is dropped, the index definition is removed from metadata and the index data pages (the B-tree) are removed from the database files. When a clustered index is dropped, the index definition is removed from metadata and the data rows that were stored in the leaf level of the clustered index are stored in the resulting unordered table, a heap. All the space previously occupied by the index is regained. This space can then be used for any database object.  
  
 An index cannot be dropped if the filegroup in which it is located is offline or set to read-only.  
  
 When the clustered index of an indexed view is dropped, all nonclustered indexes and auto-created statistics on the same view are automatically dropped. Manually created statistics are not dropped.  
  
 The syntax _table\_or\_view\_name_**.**_index\_name_ is maintained for backward compatibility. An XML index or spatial index cannot be dropped by using the backward compatible syntax.  
  
 When indexes with 128 extents or more are dropped, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits.  
  
 Sometimes indexes are dropped and re-created to reorganize or rebuild the index, such as to apply a new fill factor value or to reorganize data after a bulk load. To do this, using [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md)is more efficient, especially for clustered indexes. ALTER INDEX REBUILD has optimizations to prevent the overhead of rebuilding the nonclustered indexes.  
  
## Using Options with DROP INDEX  
 You can set the following index options when you drop a clustered index: MAXDOP, ONLINE, and MOVE TO.  
  
 Use MOVE TO to drop the clustered index and move the resulting table to another filegroup or partition scheme in a single transaction.  
  
 When you specify ONLINE = ON, queries and modifications to the underlying data and associated nonclustered indexes are not blocked by the DROP INDEX transaction. Only one clustered index can be dropped online at a time. For a complete description of the ONLINE option, see [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  
  
 You cannot drop a clustered index online if the index is disabled on a view, or contains **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml** columns in the leaf-level data rows.  
  
 Using the ONLINE = ON and MOVE TO options requires additional temporary disk space.  
  
 After an index is dropped, the resulting heap appears in the **sys.indexes** catalog view with NULL in the **name** column. To view the table name, join **sys.indexes** with **sys.tables** on **object_id**. For an example query, see example D.  
  
 On multiprocessor computers that are running [!INCLUDE[ssEnterpriseEd2005](../../includes/ssenterpriseed2005-md.md)] or later, DROP INDEX may use more processors to perform the scan and sort operations associated with dropping the clustered index, just like other queries do. You can manually configure the number of processors that are used to run the DROP INDEX statement by specifying the MAXDOP index option. For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
 When a clustered index is dropped, the corresponding heap partitions retain their data compression setting unless the partitioning scheme is modified. If the partitioning scheme is changed, all partitions are rebuilt to an uncompressed state (DATA_COMPRESSION = NONE). To drop a clustered index and change the partitioning scheme requires the following two steps:  
  
1.  Drop the clustered index.  
  
2.  Modify the table by using an ALTER TABLE ... REBUILD ... option specifying the compression option.  
  
When a clustered index is dropped OFFLINE, only the upper levels of clustered indexes are removed; therefore, the operation is quite fast. When a clustered index is dropped ONLINE, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rebuilds the heap two times, once for step 1 and once for step 2. For more information about data compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
## XML Indexes  
 Options cannot be specified when you drop anXML index. Also, you cannot use the _table\_or\_view\_name_**.**_index\_name_ syntax. When a primary XML index is dropped, all associated secondary XML indexes are automatically dropped. For more information, see [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md).  
  
## Spatial Indexes  
 Spatial indexes are supported only on tables. When you drop a spatial index, you cannot specify any options or use **.**_index\_name_. The correct syntax is as follows:  
  
 DROP INDEX *spatial_index_name* ON *spatial_table_name*;  
  
 For more information about spatial indexes, see [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).  
  
## Permissions  
 To execute DROP INDEX, at a minimum, ALTER permission on the table or view is required. This permission is granted by default to the **sysadmin** fixed server role and the **db_ddladmin** and **db_owner** fixed database roles.  
  
## Examples  
  
### A. Dropping an index  
 The following example deletes the index `IX_ProductVendor_VendorID` on the `ProductVendor` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
DROP INDEX IX_ProductVendor_BusinessEntityID   
    ON Purchasing.ProductVendor;  
GO  
```  
  
### B. Dropping multiple indexes  
 The following example deletes two indexes in a single transaction in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
DROP INDEX  
    IX_PurchaseOrderHeader_EmployeeID ON Purchasing.PurchaseOrderHeader,  
    IX_Address_StateProvinceID ON Person.Address;  
GO  
```  
  
### C. Dropping a clustered index online and setting the MAXDOP option  
 The following example deletes a clustered index with the `ONLINE` option set to `ON` and `MAXDOP` set to `8`. Because the MOVE TO option was not specified, the resulting table is stored in the same filegroup as the index. This examples uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
```  
DROP INDEX AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate   
    ON Production.BillOfMaterials WITH (ONLINE = ON, MAXDOP = 2);  
GO  
```  
  
### D. Dropping a clustered index online and moving the table to a new filegroup  
 The following example deletes a clustered index online and moves the resulting table (heap) to the filegroup `NewGroup` by using the `MOVE TO` clause. The `sys.indexes`, `sys.tables`, and `sys.filegroups` catalog views are queried to verify the index and table placement in the filegroups before and after the move. (Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] you can use the DROP INDEX IF EXISTS syntax.)  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
--Create a clustered index on the PRIMARY filegroup if the index does not exist.  
CREATE UNIQUE CLUSTERED INDEX  
    AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate   
        ON Production.BillOfMaterials (ProductAssemblyID, ComponentID,   
        StartDate)  
    ON 'PRIMARY';  
GO  
-- Verify filegroup location of the clustered index.  
SELECT t.name AS [Table Name], i.name AS [Index Name], i.type_desc,  
    i.data_space_id, f.name AS [Filegroup Name]  
FROM sys.indexes AS i  
    JOIN sys.filegroups AS f ON i.data_space_id = f.data_space_id  
    JOIN sys.tables as t ON i.object_id = t.object_id  
        AND i.object_id = OBJECT_ID(N'Production.BillOfMaterials','U')  
GO  
--Create filegroup NewGroup if it does not exist.  
IF NOT EXISTS (SELECT name FROM sys.filegroups  
                WHERE name = N'NewGroup')  
    BEGIN  
    ALTER DATABASE AdventureWorks2012  
        ADD FILEGROUP NewGroup;  
    ALTER DATABASE AdventureWorks2012  
        ADD FILE (NAME = File1,  
            FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\File1.ndf')  
        TO FILEGROUP NewGroup;  
    END  
GO  
--Verify new filegroup  
SELECT * from sys.filegroups;  
GO  
-- Drop the clustered index and move the BillOfMaterials table to  
-- the Newgroup filegroup.  
-- Set ONLINE = OFF to execute this example on editions other than Enterprise Edition.  
DROP INDEX AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate   
    ON Production.BillOfMaterials   
    WITH (ONLINE = ON, MOVE TO NewGroup);  
GO  
-- Verify filegroup location of the moved table.  
SELECT t.name AS [Table Name], i.name AS [Index Name], i.type_desc,  
    i.data_space_id, f.name AS [Filegroup Name]  
FROM sys.indexes AS i  
    JOIN sys.filegroups AS f ON i.data_space_id = f.data_space_id  
    JOIN sys.tables as t ON i.object_id = t.object_id  
        AND i.object_id = OBJECT_ID(N'Production.BillOfMaterials','U');  
GO  
```  
  
### E. Dropping a PRIMARY KEY constraint online  
 Indexes that are created as the result of creating PRIMARY KEY or UNIQUE constraints cannot be dropped by using DROP INDEX. They are dropped using the ALTER TABLE DROP CONSTRAINT statement. For more information, see [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
 The following example deletes a clustered index with a PRIMARY KEY constraint by dropping the constraint. The `ProductCostHistory` table has no FOREIGN KEY constraints. If it did, those constraints would have to be removed first.  
  
```  
-- Set ONLINE = OFF to execute this example on editions other than Enterprise Edition.  
ALTER TABLE Production.TransactionHistoryArchive  
DROP CONSTRAINT PK_TransactionHistoryArchive_TransactionID  
WITH (ONLINE = ON);  
```  
  
### F. Dropping an XML index  
 The following example drops an XML index on the `ProductModel` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
DROP INDEX PXML_ProductModel_CatalogDescription   
    ON Production.ProductModel;  
```  
  
### G. Dropping a clustered index on a FILESTREAM table  
 The following example deletes a clustered index online and moves the resulting table (heap) and FILESTREAM data to the `MyPartitionScheme` partition scheme by using both the `MOVE TO` clause and the `FILESTREAM ON` clause.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
DROP INDEX PK_MyClusteredIndex   
    ON dbo.MyTable   
    WITH (MOVE TO MyPartitionScheme,  
          FILESTREAM_ON MyPartitionScheme);  
GO  
```  

  
## See Also  
 [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)   
 [ALTER PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/alter-partition-scheme-transact-sql.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)   
 [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)   
 [CREATE SPATIAL INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-spatial-index-transact-sql.md)   
 [CREATE XML INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-xml-index-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)  
  
  


