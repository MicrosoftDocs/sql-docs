---
title: "Create, Modify, and Drop Spatial Indexes | Microsoft Docs"
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "indexes [SQL Server], creating"
  - "spatial indexes [SQL Server], dropping"
  - "spatial indexes [SQL Server], creating"
  - "indexes [SQL Server], dropping"
  - "indexes [SQL Server], modifying"
  - "spatial indexes [SQL Server], modifying"
ms.assetid: 00c1b927-8ec5-44cf-87c2-c8de59745735
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create, Modify, and Drop Spatial Indexes
  A spatial index can more efficiently perform certain operations on a column of the `geometry` or `geography` data type (a *spatial column*). More than one spatial index can be specified on a spatial column. This is useful, for example, for indexing different tessellation parameters in a single column.  
  
 There are a number of restrictions on creating spatial indexes. For more information, see [Restrictions on Spatial Indexes](#restrictions) in this topic.  
  
> [!NOTE]  
>  For information about the relationship of spatial indexes to partition and to filegroups, see the "Remarks" section in [CREATE SPATIAL INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-spatial-index-transact-sql).  
  
##  <a name="creating"></a> Creating, Modifying, and Dropping Spatial Indexes  
  
###  <a name="create"></a> To create a spatial index  
 **To create a spatial index by using Transact-SQL**  
 [CREATE SPATIAL INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-spatial-index-transact-sql)  
  
 **To create a spatial index by using the New Index dialog box in Management Studio**  
 ##### To create a spatial index in Management Studio  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, expand the database that contains the table with the specified index, and then expand **Tables**.  
  
3.  Expand the table for which you want to create the index.  
  
4.  Right-click **Indexes** and select **New Index**.  
  
5.  In the **Index name** field, enter a name for the index.  
  
6.  In the **Index type** drop-down list, select **Spatial**.  
  
7.  To specify the spatial column that you want to index, click **Add**.  
  
8.  In the **Select Columns from** *\<table name>* dialog box, select a column of type `geometry` or `geography` by selecting the corresponding check box. Any other spatial columns then become uneditable. If you want to select a different spatial column, you must first clear the currently selected column. When finished, click **OK**.  
  
9. Verify your column selection in the **Index key columns** grid.  
  
10. In the **Select a page** pane of the **Index Properties** dialog box, click **Spatial**.  
  
11. On the **Spatial** page, specify the values that you want to use for the spatial properties of the index.  
  
     When creating an index on a `geometry` type column, you must specify the **(*`X-min`*,*`Y-min`*)** and **(*`X-max`*,*`Y-max`*)** coordinates of the bounding box. For an index on a `geography` type column, the bounding-box fields become read-only after you specify the **Geography grid** tessellation scheme, because geography grid tessellation does not use a bounding box.  
  
     Optionally, you can specify nondefault values for the **Cells Per Object** field and for the grid density at any level of the tessellation scheme. The default number of cells per object is 16 for [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] or 8 for [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] or higher, and the default grid density is **Medium** for [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)].  
  
     You can select GEOMETRY_AUTO_GRID or GEOGRAPHY_AUTO_GRID for tessellation scheme in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When GEOMETRY_AUTO_GRID or GEOGRAPHY_AUTO_GRID is selected, then Level 1, Level 2, Level 3, and Level 4 grid density options are disabled.  
  
     For more information about these properties, see [Index Properties F1 Help](../indexes/index-properties-f1-help.md).  
  
12. Click **OK**.  
  
> [!NOTE]  
>  To create another spatial index on the same or a different spatial column, repeat the preceding steps.  
  
  
 **To create a spatial index by using Table Designer in Management Studio**  
 ##### To create a spatial index in Table Designer  
  
1.  In Object Explorer, right-click the table for which you want to create a spatial index, and then click **Design**.  
  
     The table opens in Table Designer.  
  
2.  Select a `geometry` or `geography` column for the index.  
  
3.  On the **Table Designer** menu, click **Spatial Index**.  
  
4.  In the **Spatial Indexes** dialog box, click **Add**.  
  
5.  Select the new index in the **Selected Spatial Index** list, and in the grid to the right, set the properties for the spatial index. For information about the properties, see [Spatial Indexes Dialog Box &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/visual-database-tools.md).  
  
  
###  <a name="alter"></a> To alter a spatial index  
  
-   [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql)  
  
    > [!IMPORTANT]  
    >  To change options that are specific to a spatial index, such as BOUNDING_BOX or GRID, you can either use a CREATE SPATIAL INDEX statement that specifies DROP_EXISTING = ON, or drop the spatial index and create a new one. For an example, see [CREATE SPATIAL INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-spatial-index-transact-sql).  
  
-   [Modify an Index](../indexes/modify-an-index.md)  
  
-   [Move an Existing Index to a Different Filegroup](../indexes/move-an-existing-index-to-a-different-filegroup.md)  
  
  
###  <a name="drop"></a> To drop a spatial index  
 **To drop a spatial index by using Transact-SQL**  
 [DROP INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-index-transact-sql)  
  
 **To drop an index by using Management Studio**  
 [Delete an Index](../indexes/delete-an-index.md)  
  
 **To drop a spatial index by using Table Designer in Management Studio**  
 ##### To drop a spatial index in Table Designer  
  
1.  In Object Explorer, right-click the table with the spatial index you want to delete and click **Design**.  
  
     The table opens in Table Designer.  
  
2.  On the **Table Designer** menu, click **Spatial Index**.  
  
     The **Spatial Index** dialog box opens.  
  
3.  Click the index you want to delete in the **Selected Spatial Index** column.  
  
4.  Click **Delete**.  
  
  
##  <a name="restrictions"></a> Restrictions on Spatial Indexes  
 A spatial index can be created only on a column of type `geometry` or `geography`.  
  
### Table and View Restrictions  
 Spatial indexes can be defined only on a table that has a primary key. The maximum number of primary key columns on the table is 15.  
  
 The maximum size of index key records is 895 bytes. Larger sizes raise an error.  
  
> [!NOTE]  
>  Primary key metadata cannot be changed while a spatial index is defined on a table.  
  
 Spatial indexes cannot be specified on indexed views.  
  
### Multiple Spatial Index Restrictions  
 You can create up to 249 spatial indexes on any of the spatial columns in a supported table. Creating more than one spatial index on the same spatial column can be useful, for example, to index different tessellation parameters in a single column.  
  
 You can create only one spatial index at a time.  
  
### Spatial Indexes and Process Parallelism  
 An index build can use available process parallelism.  
  
### Version Restrictions  
 Spatial tessellations introduced in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] cannot be replicated to [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)]. You must use [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] spatial tessellations for spatial indexes when backward compatibility with [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] databases is a requirement.  
  
  
## See Also  
 [Spatial Indexes Overview](spatial-indexes-overview.md)  
  
  
