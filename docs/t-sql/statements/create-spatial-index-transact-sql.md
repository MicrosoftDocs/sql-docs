---
title: "CREATE SPATIAL INDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/11/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SPATIAL INDEX"
  - "CREATE SPATIAL INDEX"
  - "CREATE_SPATIAL_INDEX_TSQL"
  - "SPATIAL_INDEX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "spatial indexes [SQL Server], creating"
  - "index creation [SQL Server], spatial indexes"
  - "CREATE SPATIAL INDEX statement"
  - "CREATE INDEX statement"
ms.assetid: ee6b9116-a7ff-463a-a9f0-b360804d8678
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE SPATIAL INDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates a spatial index on a specified table and column in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. An index can be created before there is data in the table. Indexes can be created on tables or views in another database by specifying a qualified database name. Spatial indexes require the table to have a clustered primary key. For information about spatial indexes, see [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- SQL Server Syntax  
  
CREATE SPATIAL INDEX index_name   
  ON <object> ( spatial_column_name )  
    {  
       <geometry_tessellation> | <geography_tessellation>  
    }   
  [ ON { filegroup_name | "default" } ]  
[;]   
  
<object> ::=  
    [ database_name. [ schema_name ] . | schema_name. ]  table_name  
  
<geometry_tessellation> ::=  
{   
  <geometry_automatic_grid_tessellation>   
| <geometry_manual_grid_tessellation>   
}  
  
<geometry_automatic_grid_tessellation> ::=  
{  
    [ USING GEOMETRY_AUTO_GRID ]  
          WITH  (  
        <bounding_box>  
            [ [,] <tessellation_cells_per_object> [ ,...n] ]  
            [ [,] <spatial_index_option> [ ,...n] ]  
                 )  
}  
  
<geometry_manual_grid_tessellation> ::=  
{  
       [ USING GEOMETRY_GRID ]  
         WITH (  
                    <bounding_box>  
                        [ [,]<tessellation_grid> [ ,...n] ]  
                        [ [,]<tessellation_cells_per_object> [ ,...n] ]  
                        [ [,]<spatial_index_option> [ ,...n] ]  
   )  
}   
  
<geography_tessellation> ::=  
{  
      <geography_automatic_grid_tessellation> | <geography_manual_grid_tessellation>  
}  
  
<geography_automatic_grid_tessellation> ::=  
{  
    [ USING GEOGRAPHY_AUTO_GRID ]  
    [ WITH (  
        [ [,] <tessellation_cells_per_object> [ ,...n] ]  
        [ [,] <spatial_index_option> ]  
     ) ]  
}  
  
<geography_manual_grid_tessellation> ::=  
{  
    [ USING GEOGRAPHY_GRID ]  
    [ WITH (  
                [ <tessellation_grid> [ ,...n] ]  
                [ [,] <tessellation_cells_per_object> [ ,...n] ]  
                [ [,] <spatial_index_option> [ ,...n] ]  
                ) ]  
}  
  
<bounding_box> ::=  
{  
      BOUNDING_BOX = ( {  
       xmin, ymin, xmax, ymax   
       | <named_bb_coordinate>, <named_bb_coordinate>, <named_bb_coordinate>, <named_bb_coordinate>   
  } )  
}  
  
<named_bb_coordinate> ::= { XMIN = xmin | YMIN = ymin | XMAX = xmax | YMAX=ymax }  
  
<tessellation_grid> ::=  
{   
    GRIDS = ( { <grid_level> [ ,...n ] | <grid_size>, <grid_size>, <grid_size>, <grid_size>  }   
        )  
}  
<tessellation_cells_per_object> ::=  
{   
   CELLS_PER_OBJECT = n   
}  
  
<grid_level> ::=  
{  
     LEVEL_1 = <grid_size>   
  |  LEVEL_2 = <grid_size>   
  |  LEVEL_3 = <grid_size>   
  |  LEVEL_4 = <grid_size>   
}  
  
<grid_size> ::= { LOW | MEDIUM | HIGH }  
  
<spatial_index_option> ::=  
{  
    PAD_INDEX = { ON | OFF }  
  | FILLFACTOR = fillfactor  
  | SORT_IN_TEMPDB = { ON | OFF }  
  | IGNORE_DUP_KEY = OFF  
  | STATISTICS_NORECOMPUTE = { ON | OFF }  
  | DROP_EXISTING = { ON | OFF }  
  | ONLINE = OFF  
  | ALLOW_ROW_LOCKS = { ON | OFF }  
  | ALLOW_PAGE_LOCKS = { ON | OFF }  
  | MAXDOP = max_degree_of_parallelism  
    | DATA_COMPRESSION = { NONE | ROW | PAGE }  
}  
  
```  
  
```  
-- Windows Azure SQL Database Syntax   
  
CREATE SPATIAL INDEX index_name   
    ON <object> ( spatial_column_name )   
    {   
      [ USING <geometry_grid_tessellation> ]   
          WITH ( <bounding_box>   
                [ [,] <tesselation_parameters> [,... n ] ]   
                [ [,] <spatial_index_option> [,... n ] ] )   
     | [ USING <geography_grid_tessellation> ]   
          [ WITH ( [ <tesselation_parameters> [,... n ] ]   
                   [ [,] <spatial_index_option> [,... n ] ] ) ]   
    }  
  
[ ; ]  
  
<object> ::=  
{  
    [database_name. [schema_name ] . | schema_name. ]   
                table_name   
}  
  
<geometry_grid_tessellation> ::=   
{ GEOMETRY_GRID }  
  
<bounding_box> ::=   
BOUNDING_BOX = ( {  
        xmin, ymin, xmax, ymax   
   | <named_bb_coordinate>, <named_bb_coordinate>, <named_bb_coordinate>, <named_bb_coordinate>   
  } )  
  
<named_bb_coordinate> ::= { XMIN = xmin | YMIN = ymin | XMAX = xmax | YMAX=ymax }  
  
<tesselation_parameters> ::=   
{   
    GRIDS = ( { <grid_density> [ ,... n ] | <density>, <density>, <density>, <density>  } )   
  | CELLS_PER_OBJECT = n   
}  
  
<grid_density> ::=   
{  
     LEVEL_1 = <density>   
  |  LEVEL_2 = <density>   
  |  LEVEL_3 = <density>   
  |  LEVEL_4 = <density>   
}  
  
<density> ::= { LOW | MEDIUM | HIGH }  
  
<geography_grid_tessellation> ::=   
{ GEOGRAPHY_GRID }  
  
<spatial_index_option> ::=   
{  
    IGNORE_DUP_KEY = OFF  
  | STATISTICS_NORECOMPUTE = { ON | OFF }  
  | DROP_EXISTING = { ON | OFF }  
  | ONLINE = OFF   
}  
  
```  
  
## Arguments  
 *index_name*  
 Is the name of the index. Index names must be unique within a table but do not have to be unique within a database. Index names must follow the rules of [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 ON \<object> ( *spatial_column_name* )  
 Specifies the object (database, schema, or table) on which the index is to be created and the name of spatial column.  
  
 *spatial_column_name* specifies the spatial column on which the index is based. Only one spatial column can be specified in a single spatial index definition; however, multiple spatial indexes can be created on a **geometry** or **geography** column.  
  
 USING  
 Indicates the tessellation scheme for the spatial index. This parameter uses the type-specific value, shown in the following table:  
  
|Data type of column|Tessellation scheme|  
|-------------------------|-------------------------|  
|**geometry**|GEOMETRY_GRID|  
|**geometry**|GEOMETRY_AUTO_GRID|  
|**geography**|GEOGRAPY_GRID|  
|**geography**|GEOGRAPHY_AUTO_GRID|  
  
 A spatial index can be created only on a column of type **geometry** or **geography**. Otherwise, an error is raised. Also, if an invalid parameter for a given type is passed, an error is raised.  
  
 For information about how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implements tessellation, see [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).  
  
 ON *filegroup_name*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Creates the specified index on the specified filegroup. If no location is specified and the table is not partitioned, the index uses the same filegroup as the underlying table. The filegroup must already exist.  
  
 ON "default"  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Creates the specified index on the default filegroup.  
  
 The term default, in this context, is not a keyword. It is an identifier for the default filegroup and must be delimited, as in ON "default" or ON [default]. If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
 **\<object>::=**  
  
 Is the fully qualified or non-fully qualified object to be indexed.  
  
 *database_name*  
 Is the name of the database.  
  
 *schema_name*  
 Is the name of the schema to which the table belongs.  
  
 *table_name*  
 Is the name of the table to be indexed.  
  
 Windows Azure SQL Database supports the three-part name format database_name.[schema_name].object_name when the database_name is the current database or the database_name is tempdb and the object_name starts with #.  
  
### USING Options  
 GEOMETRY_GRID  
 Specifies the **geometry** grid tessellation scheme that you are using. GEOMETRY_GRID can be specified only on a column of the **geometry** data type.  GEOMETRY_GRID allows for manual adjusting of the tessellation scheme.  
  
 GEOMETRY_AUTO_GRID  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Can be specified only on a column of the geometry data type. This is the default for this data type and does not need to be specified.  
  
 GEOGRAPHY_GRID  
 Specifies the geography grid tessellation scheme. GEOGRAPHY_GRID can be specified only on a column of the **geography** data type.  
  
 GEOGRAPHY_AUTO_GRID  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Can be specified only on a column of the geography data type.  This is the default for this data type and does not need to be specified.  
  
### WITH Options  
BOUNDING_BOX  
Specifies a numeric four-tuple that defines the four coordinates of the bounding box: the x-min and y-min coordinates of the lower-left corner, and the x-max and y-max coordinates of the upper-right corner.  
  
 *xmin*  
 Specifies the x-coordinate of the lower-left corner of the bounding box.  
  
 *ymin*  
 Specifies the y-coordinate of the lower-left corner of the bounding box.  
  
 *xmax*  
 Specifies the x-coordinate of the upper-right corner of the bounding box.  
  
 *ymax*  
 Specifies the y-coordinate of the upper-right corner of the bounding box.  
  
 XMIN = *xmin*  
 Specifies the property name and value for the x-coordinate of the lower-left corner of the bounding box.  
  
 YMIN =*ymin*  
 Specifies the property name and value for the y-coordinate of the lower-left corner of the bounding box.  
  
 XMAX =*xmax*  
 Specifies the property name and value for the x-coordinate of the upper-right corner of the bounding box.  
  
 YMAX =*ymax*  
 Specifies the property name and value for the y-coordinate of upper-right corner of the bounding box  
  
 > [!NOTE]
 > Bounding-box coordinates apply only within a USING GEOMETRY_GRID clause.  
 >
 > *xmax* must be greater than *xmin* and *ymax* must be greater than *ymin*. You can specify any valid [float](../../t-sql/data-types/float-and-real-transact-sql.md) value representation, assuming that: *xmax* > *xmin* and *ymax* > *ymin*. Otherwise the appropriate errors are raised.  
 > 
 > There are no default values.  
 >
 > The bounding-box property names are case-insensitive regardless of the database collation.  
  
 To specify property names, you must specify each of them once and only once. You can specify them in any order. For example, the following clauses are equivalent:  
  
-   BOUNDING_BOX =( XMIN =*xmin*, YMIN =*ymin*, XMAX =*xmax*, YMAX =*ymax* )  
  
-   BOUNDING_BOX =( XMIN =*xmin*, XMAX =*xmax*, YMIN =*ymin*, YMAX =*ymax*)  
  
GRIDS  
Defines the density of the grid at each level of a tessellation scheme. When GEOMETRY_AUTO_GRID and GEOGRAPHY_AUTO_GRID are selected, this option is disabled.  
  
 For information about tessellation, see [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).  
  
 The GRIDS parameters are as follows:  
  
 LEVEL_1  
 Specifies the first-level (top) grid.  
  
 LEVEL_2  
 Specifies the second-level grid.  
  
 LEVEL_3  
 Specifies the third-level grid.  
  
 LEVEL_4  
 Specifies the fourth-level grid.  
  
 LOW  
 Specifies the lowest possible density for the grid at a given level. LOW equates to 16 cells (a 4x4 grid).  
  
 **MEDIUM**  
 Specifies the medium density for the grid at a given level. MEDIUM equates to 64 cells (an 8x8 grid).  
  
 HIGH  
 Specifies the highest possible density for the grid at a given level. HIGH equates to 256 cells (a 16x16 grid).  
  
> [!NOTE] 
> Using level names allows you to specify the levels in any order and to omit levels. If you use the name for any level, you must use the name of any other level that you specify. If you omit a level, its density defaults to MEDIUM.  
  
> [!WARNING] 
> If an invalid density is specified, an error is raised.  
  
CELLS_PER_OBJECT =*n*  
Specifies the number of tessellation cells per object that can be used for a single spatial object in the index by the tessellation process. *n* can be any integer between 1 and 8192, inclusive. If an invalid number is passed or the number is larger than the maximum number of cells for the specified tessellation, an error is raised.  
  
 CELLS_PER_OBJECT has the following default values:  
  
|USING option|Default Cells per Object|  
|------------------|------------------------------|  
|GEOMETRY_GRID|**16**|  
|GEOMETRY_AUTO_GRID|**8**|  
|GEOGRAPHY_GRID|**16**|  
|GEOGRAPHY_AUTO_GRID|**12**|  
  
 At the top level, if an object covers more cells than specified by *n*, the indexing uses as many cells as necessary to provide a complete top-level tessellation. In such cases, an object might receive more than the specified number of cells. In this case, the maximum number is the number of cells generated by the top-level grid, which depends on the density.  
  
 The CELLS_PER_OBJECT value is used by the cells-per-object tessellation rule. For information about the tessellation rules, see [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).  
  
PAD_INDEX = { ON | **OFF** }  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies index padding. The default is OFF.  
  
 ON  
 Indicates that the percentage of free space that is specified by *fillfactor* is applied to the intermediate-level pages of the index.  
  
 OFF or *fillfactor* is not specified  
 Indicates that the intermediate-level pages are filled to near capacity, leaving sufficient space for at least one row of the maximum size the index can have, considering the set of keys on the intermediate pages.  
  
 The PAD_INDEX option is useful only when FILLFACTOR is specified, because PAD_INDEX uses the percentage specified by FILLFACTOR. If the percentage specified for FILLFACTOR is not large enough to allow for one row, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] internally overrides the percentage to allow for the minimum. The number of rows on an intermediate index page is never less than two, regardless of how low the value of *fillfactor*.  
  
FILLFACTOR =*fillfactor*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies a percentage that indicates how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make the leaf level of each index page during index creation or rebuild. *fillfactor* must be an integer value from 1 to 100. The default is 0. If *fillfactor* is 100 or 0, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates indexes with leaf pages filled to capacity.  
  
> [!NOTE]  
>  Fill factor values 0 and 100 are the same in all respects.  
  
 The FILLFACTOR setting applies only when the index is created or rebuilt. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not dynamically keep the specified percentage of empty space in the pages. To view the fill factor setting, use the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view.  
  
> [!IMPORTANT]  
> Creating a clustered index with a FILLFACTOR less than 100 affects the amount of storage space the data occupies because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] redistributes the data when it creates the clustered index.  
  
 For more information, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).  
  
SORT_IN_TEMPDB = { ON | **OFF** }  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies whether to store temporary sort results in tempdb. The default is OFF.  
  
 ON  
 The intermediate sort results that are used to build the index are stored in tempdb. This may reduce the time required to create an index if tempdb is on a different set of disks than the user database. However, this increases the amount of disk space that is used during the index build.  
  
 OFF  
 The intermediate sort results are stored in the same database as the index.  
  
 In addition to the space required in the user database to create the index, tempdb must have about the same amount of additional space to hold the intermediate sort results. For more information, see [SORT_IN_TEMPDB Option For Indexes](../../relational-databases/indexes/sort-in-tempdb-option-for-indexes.md).  
  
IGNORE_DUP_KEY =**OFF**  
Has no effect for spatial indexes because the index type is never unique. Do not set this option to ON, or else an error is raised.  
  
STATISTICS_NORECOMPUTE = { ON | **OFF**}  
Specifies whether distribution statistics are recomputed. The default is OFF.  
  
 ON  
 Out-of-date statistics are not automatically recomputed.  
  
 OFF  
 Automatic statistics updating are enabled.  
  
 To restore automatic statistics updating, set the STATISTICS_NORECOMPUTE to OFF, or execute UPDATE STATISTICS without the NORECOMPUTE clause.  
  
> [!IMPORTANT]  
> Disabling automatic recomputation of distribution statistics may prevent the query optimizer from picking optimal execution plans for queries involving the table.  
  
DROP_EXISTING = { ON | **OFF** }  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies that the named, preexisting spatial index is dropped and rebuilt. The default is OFF.  
  
 ON  
 The existing index is dropped and rebuilt. The index name specified must be the same as a currently existing index; however, the index definition can be modified. For example, you can specify different columns, sort order, partition scheme, or index options.  
  
 OFF  
 An error is displayed if the specified index name already exists.  
  
 The index type cannot be changed by using DROP_EXISTING.  
  
ONLINE =**OFF**  
Specifies that underlying tables and associated indexes are not available for queries and data modification during the index operation. In this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], online index builds are not supported for spatial indexes. If this option is set to ON for a spatial index, an error is raised. Either omit the ONLINE option or set ONLINE to OFF.  
  
 An offline index operation that creates, rebuilds, or drops a spatial index, acquires a Schema modification (Sch-M) lock on the table. This prevents all user access to the underlying table for the duration of the operation.  
  
> [!NOTE]  
> Online index operations are not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
ALLOW_ROW_LOCKS = { **ON** | OFF }  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies whether row locks are allowed. The default is ON.  
  
 ON  
 Row locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when row locks are used.  
  
 OFF  
 Row locks are not used.  
  
ALLOW_PAGE_LOCKS = { **ON** | OFF }  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies whether page locks are allowed. The default is ON.  
  
 ON  
 Page locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when page locks are used.  
  
 OFF  
 Page locks are not used.  
  
MAXDOP =*max_degree_of_parallelism*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Overrides the `max degree of parallelism` configuration option for the duration of the index operation. Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.  
  
> [!IMPORTANT]  
> Although the MAXDOP option is syntactically supported, CREATE SPATIAL INDEX currently always uses only a single processor.  
  
 *max_degree_of_parallelism* can be:  
  
 1  
 Suppresses parallel plan generation.  
  
 \>1  
 Restricts the maximum number of processors used in a parallel index operation to the specified number or fewer based on the current system workload.  
  
 0 (default)  
 Uses the actual number of processors or fewer based on the current system workload.  
  
 For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
> [!NOTE]
> Parallel index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
DATA_COMPRESSION = {NONE | ROW | PAGE}  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Determines the level of data compression used by the index.  
  
 NONE  
 No compression used on data by the index  
  
 ROW  
 Row compression used on data by the index  
  
 PAGE  
 Page compression used on data by the index  
  
## Remarks  
 Every option can be specified only once per CREATE SPATIAL INDEX statement. Specifying a duplicate of any option raises an error.  
  
 You can create up to 249 spatial indexes on each spatial column in a table. Creating more than one spatial index on specific spatial column can be useful, for example, to index different tessellation parameters in a single column.  
  
> [!IMPORTANT]  
> There are a number of other restrictions on creating a spatial index. For more information, see [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).  
  
 An index build cannot make use of available process parallelism.  
  
## Methods Supported on Spatial Indexes  
 Under certain conditions, spatial indexes support a number of set-oriented geometry methods. For more information, see [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).  
  
## Spatial Indexes and Partitioning  
 By default, if a spatial index is created on a partitioned table, the index is partitioned according to the partition scheme of the table. This assures that index data and the related row are stored in the same partition.  
  
 In this case, to alter the partition scheme of the base table, you would have to drop the spatial index before you can repartition the base table. To avoid this restriction, when you are creating a spatial index, you can specify the "ON filegroup" option. For more information, see "Spatial Indexes and Filegroups," later in this topic.  
  
## Spatial Indexes and Filegroups  
 By default, spatial indexes are partitioned to the same filegroups as the table on which the index is specified. This can be overridden by using the filegroup specification:  
  
 [ ON { *filegroup_name* | "default" } ]  
  
 If you specify a filegroup for a spatial index, the index is placed on that filegroup, regardless of the partitioning scheme of the table.  
  
## Catalog Views for Spatial Indexes  
 The following catalog views are specific to spatial indexes:  
  
 [sys.spatial_indexes](../../relational-databases/system-catalog-views/sys-spatial-indexes-transact-sql.md)  
 Represents the main index information of the spatial indexes.  
  
 [sys.spatial_index_tessellations](../../relational-databases/system-catalog-views/sys-spatial-index-tessellations-transact-sql.md)  
 Represents the information about the tessellation scheme and parameters of each of the spatial indexes.  
  
## Additional Remarks about creating indexes  
 For more information about creating indexes, see the "Remarks" section in [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  
  
## Permissions  
 The user must have ALTER permission on the table or view, or be a member of the sysadmin fixed server role or the db_ddladmin and db_owner fixed database roles.  
  
## Examples  
  
### A. Creating a spatial index on a geometry column  
 The following example creates a table named `SpatialTable` that contains a **geometry** type column, `geometry_col`. The example then creates a spatial index, `SIndx_SpatialTable_geometry_col1`, on the `geometry_col`. The example uses the default tessellation scheme and specifies the bounding box.  
  
```sql  
CREATE TABLE SpatialTable(id int primary key, geometry_col geometry);  
CREATE SPATIAL INDEX SIndx_SpatialTable_geometry_col1   
   ON SpatialTable(geometry_col)  
   WITH ( BOUNDING_BOX = ( 0, 0, 500, 200 ) );  
```  
  
### B. Creating a spatial index on a geometry column  
 The following example creates a second spatial index, `SIndx_SpatialTable_geometry_col2`, on the `geometry_col` in the `SpatialTable` table. The example specifies `GEOMETRY_GRID` as the tessellation scheme. The example also specifies the bounding box, different densities on different grid levels, and 64 cells per object. The example also sets the index padding to `ON`.  
  
```sql  
CREATE SPATIAL INDEX SIndx_SpatialTable_geometry_col2  
   ON SpatialTable(geometry_col)  
   USING GEOMETRY_GRID  
   WITH (  
    BOUNDING_BOX = ( xmin=0, ymin=0, xmax=500, ymax=200 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
```  
  
### C. Creating a spatial index on a geometry column  
 The following example creates a third spatial index, `SIndx_SpatialTable_geometry_col3`, on the `geometry_col` in the `SpatialTable` table. The example uses the default tessellation scheme. The example specifies the bounding box and uses different cell densities on the third and fourth levels, while using the default number of cells per object.  
  
```sql  
CREATE SPATIAL INDEX SIndx_SpatialTable_geometry_col3  
   ON SpatialTable(geometry_col)  
   WITH (  
    BOUNDING_BOX = ( 0, 0, 500, 200 ),  
    GRIDS = ( LEVEL_4 = HIGH, LEVEL_3 = MEDIUM ) );  
```  
  
### D. Changing an option that is specific to spatial indexes  
 The following example rebuilds the spatial index created in the preceding example, `SIndx_SpatialTable_geography_col3`, by specifying a new `LEVEL_3` density with DROP_EXISTING = ON.  
  
```sql  
CREATE SPATIAL INDEX SIndx_SpatialTable_geography_col3  
   ON SpatialTable(geography_col)  
   WITH ( BOUNDING_BOX = ( 0, 0, 500, 200 ),  
        GRIDS = ( LEVEL_3 = LOW ),  
        DROP_EXISTING = ON );  
```  
  
### E. Creating a spatial index on a geography column  
 The following example creates a table named `SpatialTable2` that contains a **geography** type column, `geography_col`. The example then creates a spatial index, `SIndx_SpatialTable_geography_col1`, on the `geography_col`. The example uses the default parameters values of the GEOGRAPHY_AUTO_GRID tessellation scheme.  
  
```sql  
CREATE TABLE SpatialTable2(id int primary key, object GEOGRAPHY);  
CREATE SPATIAL INDEX SIndx_SpatialTable_geography_col1   
   ON SpatialTable2(object);  
```  
  
> [!NOTE]  
>  For geography grid indexes, a bounding box cannot be specified.  
  
### F. Creating a spatial index on a geography column  
 The following example creates a second spatial index, `SIndx_SpatialTable_geography_col2`, on the `geography_col` in the `SpatialTable2` table. The example specifies `GEOGRAPHY_GRID` as the tessellation scheme. The example also specifies different grid densities on different levels and 64 cells per object. The example also sets the index padding to `ON`.  
  
```sql  
CREATE SPATIAL INDEX SIndx_SpatialTable_geography_col2  
   ON SpatialTable2(object)  
   USING GEOGRAPHY_GRID  
   WITH (  
    GRIDS = (MEDIUM, LOW, MEDIUM, HIGH ),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
```  
  
### G. Creating a spatial index on a geography column  
 The example then creates a third spatial index, `SIndx_SpatialTable_geography_col3`, on the `geography_col` in the `SpatialTable2` table. The example uses the default tessellation scheme, GEOGRAPHY_GRID, and the default CELLS_PER_OBJECT value (16).  
  
```sql  
CREATE SPATIAL INDEX SIndx_SpatialTable_geography_col3  
   ON SpatialTable2(object)  
   WITH ( GRIDS = ( LEVEL_3 = HIGH, LEVEL_2 = HIGH ) );  
```  
  
## See Also  
 [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)   
 [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)   
 [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)   
 [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)   
 [DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.spatial_index_tessellations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-spatial-index-tessellations-transact-sql.md)   
 [sys.spatial_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-spatial-indexes-transact-sql.md)   
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)  
  
  
