---
title: "Index Properties F1 Help"
description: Index Properties F1 Help
author: MikeRayMSFT
ms.author: mikeray
ms.date: "02/17/2017"
ms.service: sql
ms.subservice: table-view-index
ms.topic: "reference"
f1_keywords:
  - "sql13.swb.indexproperties.filter.f1"
  - "sql13.swb.indexproperties.partitions.f1"
  - "sql13.swb.indexproperties.general.f1"
  - "sql13.swb.indexproperties.storage.f1"
  - "sql13.swb.indexproperties.columns.f1"
  - "sql13.swb.indexproperties.options.f1"
  - "sql13.swb.indexproperties.spatial.f1"
ms.assetid: 45efd81a-3796-4b04-b0cc-f3deec94c733
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Index Properties F1 Help
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)] 


  The sections in this topic refer to various index properties that are available by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] dialogs.  
  
 **In This Topic:**  
  
 [Index Properties General Page](#General)  
  
 [Select (Index) Columns Dialog Box](#Columns)  
  
 [Index Properties Storage Page](#Storage)  
  
 [Index Properties Spatial Page](#Spatial)  
  
 [Index Properties Filter Page](#Filter)  
  
##  <a name="General"></a> Index Properties General Page  
 Use the General page to view or modify index properties for the selected table or view. The options for each page may change based on the type of index selected.  
  
 **Table name**  
 Displays the name of the table or view that the index was created on. This field is read-only. To select a different table, close the Index Properties page, select the correct table, and then open the Index Properties page again.  
  
 Spatial indexes cannot be specified on indexed views. Spatial indexes can be defined only for a table that has a primary key. The maximum number of primary key columns on the table is 15. The combined per-row size of the primary-key columns is limited to a maximum of 895 bytes.  
  
 **Index name**  
 Displays the name of the index. This field is read-only for an existing index. When creating a new index, type the name of the index.  
  
 **Index type**  
 Indicates the type of index. For new indexes, indicates the type of index selected when opening the dialog box. Indexes can be: **Clustered**, **Nonclustered**, **Primary XML**, **Secondary XML**, **Spatial**, **Clustered columnstore**, or **Nonclustered Columnstore**.  
  
 **Note** Only one clustered index is allowed for each table. Only one xVelocity memory optimized columnstore index is allowed for each table.  
  
 **Unique**  
 Selecting this check box makes the index unique. No two rows are permitted to have the same index value. By default, this check box is cleared. When modifying an existing index, index creation will fail if two rows have the same value. For columns where NULL is permitted, a unique index permits one NULL value.  
  
 If you select **Spatial** in the **Index type** field, the **Unique** check box is dimmed.  
  
 **Index key columns**  
 Add the desired columns to the **Index key columns** grid. When more than one column is added, the columns must be listed in the order desired. The column order in an index can have a great impact on the index performance.  
  
 No more than 16 columns can participate in a single composite index. For greater than 16 columns, see included columns at the end of this topic.  
  
 A spatial index can be defined only on a single column that contains a spatial data type (a *spatial column*).  
  
 **Name**  
 Displays the name of the column that participates in the index key.  
  
 **Sort Order**  
 Specifies the sort direction of the selected index column, either **Ascending** or **Descending**.  
  
> [!NOTE]  
>  If the index type is **Primary XML** or **Spatial**, this column does not appear in the table.  
  
 **Data Type**  
 Displays the data type information.  
  
> [!NOTE]  
>  If the table column is a computed column, **Data type** displays "computed column."  
  
 **Size**  
 Displays the maximum number of bytes required to store the column data type. Displays zero (0) for a spatial or XML column.  
  
 **Identity**  
 Displays whether the column participating in the index key is an identity column.  
  
 **Allow NULLs**  
 Displays whether the column participating in the index key allows NULL values to be stored in the table or view column.  
  
 **Add**  
 Adds a column to the index key. Select table columns from the **Select Columns from** *\<table name>* dialog box that appears when you click **Add**. For a spatial index, after you select one column, this button is dimmed.  
  
 **Remove**  
 Removes the selected column from participation in the index key.  
  
 **Move Up**  
 Moves the selected column up in the index key grid.  
  
 **Move Down**  
 Moves the selected column down in the index key grid.  
  
 **Columnstore columns**  
 Click **Add** to select columns for the columnstore index. For limitations on a columnstore index, see [CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md).  
  
 **Included columns**  
 Include nonkey columns in the nonclustered index. This option allows you to bypass the current index limits on the total size of an index key and the maximum number of columns participating in an index key by adding columns as nonkey columns in the leaf level of the nonclustered index. For more information, see [Create Indexes with Included Columns](../../relational-databases/indexes/create-indexes-with-included-columns.md)  
  
##  <a name="Columns"></a> Select (Index) Columns Dialog Box  
 Use this page to add columns to the **Index Properties General** page when creating or modifying an index.  
  
 **Check box**  
 Select to add columns.  
  
 **Name**  
 Name of the column.  
  
 **Data Type**  
 The data type of the column.  
  
 **Bytes**  
 The size of the column in bytes.  
  
 **Identity**  
 Displays **Yes** for identity columns, and **No** when the column is not an identity column.  
  
 **Allow Nulls**  
 Displays **Yes** when the table definition allows null values for the column. Displays **No** when the table definition does not allow nulls for the column.  

##  <a name="Options"></a> Options Page Options
 Use this page to view or modify various index options.

### General Options
**Auto recompute statistics**<br>
Specifies whether distribution statistics are recomputed automatically. The default is **True** which is equivalent to setting STATISTICS_NORECOMPUTE to OFF. Setting this to **False** sets STATISTICS_NORECOMPUTE to ON.

**Ignore duplicate values** <br>
Specifies the error response when an insert operation attempts to insert duplicate key values into a unique index.

True<br>
A warning message will occur when duplicate key values are inserted into a unique index. Only the rows violating the uniqueness constraint will fail.

False<br>
An error message will occur when duplicate key values are inserted into a unique index. The entire INSERT operation will be rolled back.

### Locks Options

**Allow row locks**<br>
Specifies whether row locks are allowed.

**Allow page locks**<br>
Specifies whether page locks are allowed.

### Operation Options

 **Allow online DML processing**  
 Allows users to access the underlying table or clustered index data and any associated nonclustered indexes during an index operation such as CREATE or ALTER. For more information, see [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md).  
  
> [!NOTE]  
>  This option is not available for XML indexes, or if the index is a disabled clustered index.  
  
 **Maximum degree of parallelism**  
 Limits the number of processors to use during parallel plan execution. The default value, 0, uses the actual number of available CPUs. Setting the value to 1 suppresses parallel plan generation; setting the value to a number greater than 1 restricts the maximum number of processors used by a single query execution. This option only becomes available if the dialog box is in the **Rebuild** or **Recreate** state. For more information, see [Set the Max Degree of Parallelism Option for Optimal Performance](../../relational-databases/policy-based-management/set-the-max-degree-of-parallelism-option-for-optimal-performance.md).  
  
> [!NOTE]  
>  If a value greater than the number of available CPUs is specified, the actual number of available CPUs is used.  


**Optimize for sequential key**<br>
Specifies whether or not to optimize for last-page insert contention. For more information, see [Sequential Keys](../../t-sql/statements/create-index-transact-sql.md#sequential-keys).

### Storage Options

**Sort in tempdb**<br>
Specifies whether to store temporary sort results in tempdb.

True<br>
The intermediate sort results that are used to build the index are stored in tempdb. This may reduce the time required to create an index if tempdb is on a different set of disks than the user database. However, this increases the amount of disk space that is used during the index build.

False<br>
The intermediate sort results are stored in the same database as the index. For more information, see [SORT_IN_TEMPDB Option For Indexes](./sort-in-tempdb-option-for-indexes.md).

**Fill factor**<br>
Specifies a percentage that indicates how full the Database Engine should make the leaf level of each index page during index creation or rebuild. fillfactor must be an integer value from 1 to 100. If fillfactor is 100, the Database Engine creates indexes with leaf pages filled to capacity.
The FILLFACTOR setting applies only when the index is created or rebuilt. The Database Engine does not dynamically keep the specified percentage of empty space in the pages.

For more information, see [Specify Fill Factor for an Index](./specify-fill-factor-for-an-index.md).

**Pad index**<br>
Specifies index padding.

True<br>
The percentage of free space that is specified by fillfactor is applied to the intermediate-level pages of the index.

False or fillfactor is not specified<br>
The intermediate-level pages are filled to near capacity, leaving sufficient space for at least one row of the maximum size the index can have, considering the set of keys on the intermediate pages.


##  <a name="Storage"></a> Storage Page Options  
 Use this page to view or modify filegroup or partition scheme properties for the selected index. Only shows options related to the type of index.  
  
 **Filegroup**  
 Stores the index in the specified filegroup. The list only displays standard (row) filegroups. The default list selection is the PRIMARY filegroup of the database. For more information, see [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md).  
  
 **Filestream filegroup**  
 Specifies the filegroup for FILESTREAM data. This list displays only FILESTREAM filegroups. The default list selection is the PRIMARY FILESTREAM filegroup. For more information, see [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md).  
  
 **Partition scheme**  
 Stores the index in a partition scheme. Clicking **Partition Scheme** enables the grid below. The default list selection is the partition scheme that is used for storing the table data. When you select a different partition scheme in the list, the information in the grid is updated. For more information, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
 The partition scheme option is unavailable if there are no partition schemes in the database.  
  
 **Filestream partition scheme**  
 Specifies the partition scheme for FILESTREAM data. The partition scheme must be symmetric with the scheme that is specified in the **Partition scheme** option.  
  
 If the table is not partitioned, the field is blank.  
  
 **Partition Scheme Parameter**  
 Displays the name of the column that participates in the partition scheme.  
  
 **Table Column**  
 Select the table or view to map to the partition scheme.  
  
 **Column Data Type**  
 Displays data type information about the column.  
  
> [!NOTE]  
>  If the table column is a computed column, **Column Data Type** displays "computed column."  
  
##  <a name="Spatial"></a> Spatial Page Index Options  
 Use the **Spatial** page to view or specify the values of the spatial properties. For more information, see [Spatial Data &#40;SQL Server&#41;](../../relational-databases/spatial/spatial-data-sql-server.md).  
  
### Bounding Box  
 The *bounding box* is the perimeter of the top-level grid of a geometric plane. The bounding-box parameters exist only in the geometry grid tessellation. These parameters are unavailable if the **Tessellation Scheme** is **Geography grid**.  
  
 The panel displays the **(**_X-min_**,**_Y-min_**)** and **(**_X-max_**,**_Y-max_**)** coordinates of the bounding box. There are no default coordinate values. Therefore, when you are creating a new spatial index on a **geometry** type column, you must specify the coordinate values.  
  
 **X-min**  
 The X-coordinate of the lower-left corner of the bounding box.  
  
 **Y-min**  
 The Y-coordinate of the lower-left corner of the bounding box.  
  
 **X-max**  
 The X-coordinate of the upper-right corner of the bounding box.  
  
 **Y-max**  
 The Y-coordinate of upper-right corner of the bounding box.  
  
### General  
 **Tessellation Scheme**  
 Indicates the tessellation scheme of the index. The supported tessellation schemes are as follows.  
  
 **Geometry grid**  
 Specifies the geometry grid tessellation scheme, which applies to a column of the **geometry** data type.  
  
 **Geometry Auto grid**  
 This option is enabled for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when database compatibility level is set to 110 or higher.  
  
 **Geography grid**  
 Specifies the geography grid tessellation scheme, which applies to a column of the **geography** data type.  
  
 **Geography Auto grid**  
 This option is enabled for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when database compatibility level is set to 110 or higher.  
  
 For information about how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implements tessellation, see [Spatial Data &#40;SQL Server&#41;](../../relational-databases/spatial/spatial-data-sql-server.md).  
  
 **Cells Per Object**  
 Indicates the number of tessellation cells-per-object that can be used for a single spatial object in the index. This number can be any integer between 1 and 8192, inclusive. The default is 16, and 8 for earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when database compatibility level is set to 110 or higher.  
  
 At the top level, if an object covers more cells than specified by *n*, the indexing uses as many cells as necessary to provide a complete top-level tessellation. In such cases, an object might receive more than the specified number of cells. In this case, the maximum number is the number of cells generated by the top-level grid, which depends on the **Level 1** density.  
  
### Grids  
 This panel shows the density of the grid at each level of the tessellation scheme. Density is specified as **Low**, **Medium**, or **High**. The default is **Medium**. **Low** represents a 4x4 grid (16 cells), **Medium** represents an 8x8 grid (64 cells), and **High** represents a 16x16 grid (256 cells). These options are not available when the **Geometry Auto grid** or **Geography Auto grid** tessellation options are chosen.  
  
 **Level 1**  
 The density of the first-level (top) grid.  
  
 **Level 2**  
 The density of the second-level grid.  
  
 **Level 3**  
 The density of the third-level grid.  
  
 **Level 4**  
 The density of the fourth-level grid.  
  
##  <a name="Filter"></a> Filter Page  
 Use this page to enter the filter predicate for a filtered index. For more information, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md).  
  
 **Filter Expression**  
 Defines which data rows to include in the filtered index. For example, `StartDate > '20000101' AND EndDate IS NOT NULL'.`  
  
## See Also  
 [Set Index Options](../../relational-databases/indexes/set-index-options.md)   
 [INDEXPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/indexproperty-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)  
  
  
