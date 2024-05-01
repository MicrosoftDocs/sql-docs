---
title: "Arguments and properties of spatial index stored procedures"
description: "Arguments and properties of spatial index stored procedures."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "spatial indexes [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# Spatial index stored procedures - arguments and properties

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes the arguments and properties for spatial index stored procedures.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

For the syntax of specific spatial index stored procedures, see the following articles:

- [sp_help_spatial_geometry_index](sp-help-spatial-geometry-index-transact-sql.md)
- [sp_help_spatial_geometry_index_xml](sp-help-spatial-geometry-index-xml-transact-sql.md)
- [sp_help_spatial_geography_index](sp-help-spatial-geography-index-transact-sql.md)
- [sp_help_spatial_geography_index_xml](sp-help-spatial-geography-index-xml-transact-sql.md)

## Arguments

#### [ @tabname = ] N'*tabname*'

The qualified or nonqualified name of the table for which the spatial index has been specified.

Quotation marks are required only if a qualified table is specified. If a fully qualified name, including a database name, is provided, the database name must be the name of the current database. *@tabname* is **nvarchar(776)**, with no default.

#### [ @indexname = ] '*indexname*'

The name of the spatial index specified. *@indexname* is **sysname** with no default.

#### [ @verboseoutput = ] *verboseoutput*

The range of property names and values to be returned.

- `0` = core properties
- `>0` = all properties

*@verboseoutput* is **tinyint** with no default.

#### [ @query_sample = ] '*query_sample*'

A representative query sample that can be used to test the usefulness of the index. It may be a representative object or a query window. *query_sample* is **geometry** with no default.

#### [ @xml_output = ] '*xml_output*'

An output parameter that returns the result set in an XML fragment. *xml_output* is **xml** with no default.

## Properties

Set `@verboseoutput = 0` to return core properties as shown in the following table, or `@verboseoutput > 0` to return all properties of the spatial index.

#### Base_Table_Rows

Number of rows in the base table. Value is **bigint**.

#### Bounding_Box_xmin

X-minimum bounding box properties of the spatial index for **geometry** type. This property value is NULL for **geography** type. Value is **float**.

#### Bounding_Box_ymin

Y-minimum bounding box properties of the spatial index for **geometry** type. This property value is NULL for **geography** type. Value is **float**.

#### Bounding_Box_xmax

X-maximum bounding box properties of the spatial index for **geometry** type. This property value is NULL for **geography** type. Value is **float**.

#### Bounding_Box_ymax

Y-maximum bounding box properties of the spatial index for **geometry** type. This property value is NULL for **geography** type. Value is **float**.

#### Grid_Size_Level_1

Level 1 grid density of the spatial index:

- 16 for LOW
- 64 for MEDIUM
- 256 for HIGH

Value is **int**.

#### Grid_Size_Level_2

Level 2 grid density of the spatial index:

- 16 for LOW
- 64 for MEDIUM
- 256 for HIGH

Value is **int**.

#### Grid_Size_Level_3

Level 3 grid density of the spatial index:

- 16 for LOW
- 64 for MEDIUM
- 256 for HIGH

Value is **int**.

#### Grid_Size_Level_4

Level 4 grid density of the spatial index:

- 16 for LOW
- 64 for MEDIUM
- 256 for HIGH

Value is **int**.

#### Cells_Per_Object

Number of cells per object (index property). Value is **int**.

#### Total_Primary_Index_Rows

Number of rows in the index. Value is **bigint**.

#### Total_Primary_Index_Pages

Number of pages in the index. Value is **bigint**.

#### Average_Number_Of_Index_Rows_Per_Base_Row

Number of index rows / number base table rows. Value is **bigint**.

#### Total_Number_Of_ObjectCells_In_Level0_For_QuerySample

Indicates whether the representative query sample falls outside of the bounding box of the **geometry** index and into the root cell (level 0 cell). This is either 0 (not in level 0 cell) or 1. If it is in the level 0 cell, the investigated index isn't an appropriate index for the query sample. This is a core property. Value is **bigint**.

#### Total_Number_Of_ObjectCells_In_Level0_In_Index

Number of cell instances of indexed objects that are tessellated in level 0 (root cell, outside the bounding box for **geometry**). This is a core property. Value is **bigint**.

For **geometry** indexes, this occurs if the bounding box of the index is smaller than the data domain. A high number of objects in level 0 may require secondary filters if the query window falls partially outside the bounding box and decreases the index performance (for example, **Total_Number_Of_ObjectCells_In_Level0_For_QuerySample** is 1). If the query window falls inside the bounding box, a high number of objects in level 0 may actually improve the performance of the index.

NULL and empty instances are counted at level 0 but don't affect performance. Level 0 has as many cells as NULL and empty instances at the base table. For **geography** indexes, level 0 has as many cells as NULL and empty instances +1 cell, because the query sample is counted as 1.

#### Total_Number_Of_ObjectCells_In_Level1_In_Index

Number of cell instances of indexed objects that are tessellated with level 1 precision. This is a core property. Value is **bigint**.

#### Total_Number_Of_ObjectCells_In_Level2_In_Index

Number of cell instances of indexed objects that are tessellated with level 2 precision. This is a core property. Value is **bigint**.

#### Total_Number_Of_ObjectCells_In_Level3_In_Index

Number of cell instances of indexed objects that are tessellated with level 3 precision. This is a core property. Value is **bigint**.

#### Total_Number_Of_ObjectCells_In_Level4_In_Index

Number of cell instances of indexed objects that are tessellated with level 4 precision. This is a core property. Value is **bigint**.

#### Total_Number_Of_interior_ObjectCells_In_Level1_In_Index

Number of cells that are completely covered by an object at tessellation level 1 and thus are interior to the object. (Cell_attributevalue is 2.) This is a core property. Value is **bigint**.

#### Total_Number_Of_interior_ObjectCells_In_Level2_In_Index

Number of cells that are completely covered by an object at tessellation level 2 and thus are interior to the object. (Cell_attribute value is 2.) This is a core property. Value is **bigint**.

#### Total_Number_Of_interior_ObjectCells_In_Level3_In_Index

Number of cells that are completely covered by an object at tessellation level 3 and thus are interior to the object. (Cell_attribute value is 2.) This is a core property. Value is **bigint**.

#### Total_Number_Of_interior_ObjectCells_In_Level4_In_Index

Number of cells that are completely covered by an object at tessellation level 4 and thus are interior to the object. (Cell_attribute value is 2.) This is a core property. Value is **bigint**.

#### Total_Number_Of_intersecting_ObjectCells_In_Level1_In_Index

Number of cells that are intersected by an object at tessellation level 1. (Cell_attribute value is 1.) This is a core property. Value is **bigint**.

#### Total_Number_Of_intersecting_ObjectCells_In_Level2_In_Index

Number of cells that are intersected by an object at tessellation level 2. (Cell_attribute value is 1.) This is a core property. Value is **bigint**.

#### Total_Number_Of_intersecting_ObjectCells_In_Level3_In_Index

Number of cells that are intersected by an object at tessellation level 3. (Cell_attribute value is 1.) This is a core property. Value is **bigint**.

#### Total_Number_Of_intersecting_ObjectCells_In_Level4_In_Index

Number of cells that are intersected by an object at tessellation level 4. (Cell_attribute value is 1.) This is a core property. Value is **bigint**.

#### Total_Number_Of_Border_ObjectCells_In_Level0_For_QuerySample

Indicates whether the query sample is in the root cell 0 outside the bounding box, but touching it. This is a core property. Value is **bigint**.

> [!NOTE]  
> This information is only useful in determining whether there are objects that the bounding box may have closely missed.

#### Total_Number_Of_Border_ObjectCells_In_Level0_In_Index

Number of objects in level 0 that touch the bounding box. (Cell_attribute value is 0.)  Value is **bigint**.

#### Total_Number_Of_Border_ObjectCells_In_Level1_In_Index

Number of object cells that touch a grid cell boundary at the tessellation level 1. (Cell_attribute value is 0.) This is a core property. Value is **bigint**.

#### Total_Number_Of_Border_ObjectCells_In_Level2_In_Index

Number of object cells that touch a grid cell boundary at the tessellation level 2. (Cell_attribute value is 0.) This is a core property. Value is **bigint**.

#### Total_Number_Of_Border_ObjectCells_In_Level3_In_Index

Number of object cells that touch a grid cell boundary at the tessellation level 3. (Cell_attribute value is 0.) This is a core property. Value is **bigint**.

#### Total_Number_Of_Border_ObjectCells_In_Level4_In_Index

Number of object cells that touch a grid cell boundary at the tessellation level 4. (Cell_attribute value is 0.) This is a core property. Value is **bigint**.

#### Interior_To_Total_Cells_Normalized_To_Leaf_Grid_Percentage

Percentage of the total area (total leaf cells) of the grid that contains leaf cells covered by an object.

For example, an object is tessellated into 10 cells at the four different grid levels covering an area that is equivalent to 100 leaf cells in total. Suppose there are three interior cells that are completely covered by the object. The area covered by the three interior cells is equivalent to 42 leaf cells. Thus, the percentage of covered area is 42 percent. This is a good measure of how well the objects in the index are shredded.

Value is **float**.

#### Intersecting_To_Total_Cells_Normalized_To_Leaf_Grid_Percentage

Same as **Interior_To_Total_Cells_Normalized_To_Leaf_Grid_Percentage**, except that these are partially covered cells. Value is **float**.

#### Border_To_Total_Cells_Normalized_To_Leaf_Grid_Percentage

Same as **Interior_To_Total_Cells_Normalized_To_Leaf_Grid_Percentage** except that these are border cells. Value is **float**.

#### Average_Cells_Per_Object_Normalized_To_Leaf_Grid

Average cells per object normalized to the leaf grid. This gives us an indication of the spatial size of the object, or how large the objects are. Value is **float**.

#### Average_Objects_PerLeaf_GridCell

Sparseness of the index. Average number of objects per leaf cell. Value is **float**.

#### Number_Of_SRIDs_Found

The number of unique SRIDs in the index and column. Value is **int**.

Because a column can contain more than one SRID and objects of different SRIDs never intersect, the number of SRIDs indicates the selectivity of the index.

#### Width_Of_Cell_In_Level1

Width property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Width_Of_Cell_In_Level2

Width property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Width_Of_Cell_In_Level3

Width property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Width_Of_Cell_In_Level4

Width property of cell in the indexing grid. The unit of measurement is provided by the index and is dependent on the SRID of the indexed data. Value is **float**.

#### Height_Of_Cell_In_Level1

Height property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Height_Of_Cell_In_Level2

Height property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Height_Of_Cell_In_Level3

Height property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Height_Of_Cell_In_Level4

Height property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Area_Of_Cell_In_Level1

Area property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Area_Of_Cell_In_Level2

Area property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Area_Of_Cell_In_Level3

Area property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### Area_Of_Cell_In_Level4

Area property of cell in the indexing grid. The unit of measurement is provided by the index and depends on the SRID of the indexed data. Value is **float**.

#### CellArea_To_BoundingBoxArea_Percentage_In_Level1

The percentage of coverage of the bounding box by a level 1 cell. Value is **float**.

#### CellArea_To_BoundingBoxArea_Percentage_In_Level2

The percentage of coverage of the bounding box by a level 2 cell. Value is **float**.

#### CellArea_To_BoundingBoxArea_Percentage_In_Level3

The percentage of coverage of the bounding box by a level 3 cell. Value is **float**.

#### CellArea_To_BoundingBoxArea_Percentage_In_Level4

The percentage of coverage of the bounding box by a level 4 cell. Value is **float**.

#### Number_Of_Rows_Selected_By_Primary_Filter

Number of rows selected by the primary filter. This is a core property. Value is **bigint**.

#### Number_Of_Rows_Selected_By_Internal_Filter

Number of rows selected by the internal filter. The secondary filter isn't called for these rows. This is a core property. Value is **bigint**.

The returned number is only applicable for **STintersects**.

#### Number_Of_Times_Secondary_Filter_Is_Called

Number of times the secondary filter is called. This is a core property. Value is **bigint**.

#### Percentage_Of_Rows_NotSelected_By_Primary_Filter

If there are N rows in the base table, and P are selected by the primary filter, this returns (N-P)/N as percentage. This is a core property. Value is **float**.

#### Percentage_Of_Primary_Filter_Rows_Selected_By_internal_Filter

If P rows are selected by the primary filter and S rows are selected by the internal filter, this returns S/P as a percentage. The higher the percentage, the better the index is in avoiding the more performance-expensive secondary filter. This is a core property. Value is **float**.

#### Number_Of_Rows_Output

Number of rows output by the query. This is a core property. Value is **bigint**.

#### Internal_Filter_Efficiency

If O is the number of rows output, this returns S/O as a percentage. This is a core property. Value is **float**.

#### Primary_Filter_Efficiency

If P rows are selected by the primary filter and O is the number of rows output, this returnsO/P as a percentage. The higher the efficiency of the primary filter, the fewer false positives that the secondary filter has to process. This is a core property. Value is **float**.

## Permissions

User must be a member of the **public** role. Requires READ ACCESS permission on the server and the object. This applies to all spatial index stored procedures.

## Remarks

Properties containing NULL values aren't included in the return set.

## Examples

For examples, see the following articles:

- [sp_help_spatial_geometry_index (Transact-SQL)](sp-help-spatial-geometry-index-transact-sql.md)
- [sp_help_spatial_geometry_index_xml (Transact-SQL)](sp-help-spatial-geometry-index-xml-transact-sql.md)
- [sp_help_spatial_geography_index (Transact-SQL)](sp-help-spatial-geography-index-transact-sql.md)
- [sp_help_spatial_geography_index_xml (Transact-SQL)](sp-help-spatial-geography-index-xml-transact-sql.md)

## Related content

- [sp_help_spatial_geometry_index (Transact-SQL)](sp-help-spatial-geometry-index-transact-sql.md)
- [Spatial Indexes Overview](../spatial/spatial-indexes-overview.md)
- [XQuery Basics](../../xquery/xquery-basics.md)
- [XQuery Language Reference (SQL Server)](../../xquery/xquery-language-reference-sql-server.md)
