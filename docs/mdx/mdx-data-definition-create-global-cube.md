---
description: "MDX Data Definition - CREATE GLOBAL CUBE"
title: "CREATE GLOBAL CUBE Statement  (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Definition - CREATE GLOBAL CUBE


  Creates and populates a locally persisted cube, based on a subcube from a cube on the server. A connection to the server is not required to connect to the locally persisted cube. For more information about local cubes, see [Local Cubes &#40;Analysis Services - Multidimensional Data&#41;](/analysis-services/multidimensional-models/olap-physical/local-cubes-analysis-services-multidimensional-data).  
  
## Syntax  
  
```  
  
CREATE GLOBAL CUBE local_cube_name STORAGE 'Cube_Location'   
FROM source_cube_name (<param list>)  
  
<param list>::= <param> ,<param list> | <param>  
  
<param>::= <dims list> | <measures list>  
  
<measures list>::= <measure>[, <measures list>]   
  
<dims list>::= <dim def> [, <dims list>]  
  
<measure>::= MEASURE source_cube_name.measure_name [<visibility qualifier>] [AS measure_name]   
  
<dim def>::= <source dim def> | <derived dim def>  
  
<source dim def>::= DIMENSION source_cube_name.dimension_name [<dim flags>] [<visibility qualifier>] [AS dimension_name>] [FROM <dim from clause> ] [<dim content def>]  
  
<dim flags>::= NOT_RELATED_TO_FACTS   
  
<dim from clause>::= < dim DM from clause> | <reg dim from clause>   
  
<dim DM from clause>::= dm_model_name> COLUMN column_name   
  
<dim reg from clause>::= dimension_name  
  
<dim content def>::= ( <level list> [,<grouping list>] [,<member slice list>] [,<default member>] )  
  
<level list>::= <level def> [, <level list>]  
  
<level def>::= LEVEL level_name [<level type> ] [AS level_name] [<level content def>]  
  
<level content def>::= ( <property list> ) | NO_PROPERTIES  
  
<level type>::= GROUPING  
  
<property list>::= <property def> [, <property list>]  
  
<property def>::= PROPERTY property_name   
  
<grouping list>::= <grouping entity> [,<grouping list>]  
  
<grouping entity>::= GROUP group_level_name.group_name (<mixed list>)  
  
<grp mixed list>::= <grp mixed element> [,<grp mixed list>]  
  
<grp mixed element>::= <grouping entity> | <member def>  
  
<member slice list>::= <member list>  
  
<member list>::= <member def> [, <member list>]  
  
<member def>::= MEMBER member_name  
  
<default member>::= DEFAULT_MEMBER AS MDX_expression  
  
<visibility qualifier>::= HIDDEN   
```  
  
## Syntax Elements  
 local_cube_name  
 The name of the local cube.  
  
 'Cube_Location'  
 The name and path for the locally persisted cube.  
  
 source_cube_name  
 The name of the cube on which the local cube is based.  
  
 source_cube_name.measure_name  
 The fully qualified name of the source measure being included in the local cube. Calculated members of the Measures dimension are not permitted.  
  
 measure_name  
 The name of the measure in the local cube.  
  
 source_cube_name.dimension_name  
 The fully qualified name of the source dimension being included in the local cube.  
  
 dimension_name  
 The name of the dimension in the local cube.  
  
 FROM \<dim from clause>  
 Valid specification for derived dimension definition only.  
  
 NOT_RELATED_TO_FACTS  
 Valid specification for derived dimension definition only.  
  
 \<level type>  
 Valid specification for derived dimension definition only.  
  
## Remarks  
 A local cube is definedin terms of the measures and definitions that define it. There are two types of dimensions.  
  
-   Source dimensions - These are dimensions that were part of one of more source cubes  
  
-   Derived dimensions - These are dimensions that provide new analysis capabilities. A derived dimension can be a regular dimension defined based on a source dimension that is either sliced vertically or horizontally, or contains custom grouping of dimension members. A derived dimension can also be a data mining dimension based on a data mining model.  
  
> [!NOTE]  
>  The Dimension keyword can refer to either dimensions or hierarchies.  
  
 In a local cube, you can perform the following tasks:  
  
-   Eliminate dimensions that exist in the source cube  
  
-   Add or eliminate hierarchies from a dimension  
  
-   Eliminate measure groups or specific measures  
  
 The CREATE GLOBAL CUBE statement follows these rules:  
  
-   The CREATE GLOBAL CUBE statement automatically copies all commands, such as calculated measures or actions, to the local cube. If a command contains a Multidimensional Expressions (MDX) expression that references the parent cube explicitly, the local cube cannot run that command. To prevent this problem, use the **CURRENTCUBE** keyword when defining MDX expressions for commands. The **CURRENTCUBE** keyword uses the current cube context when referencing a cube within an MDX expression.  
  
-   A global cube that is created from an existing global cube in a local cube file cannot be saved in the same local cube file. For example, you create a global cube named SalesLocal1 and save this cube to the C:\SalesLocal.cub file. You then connect to the C:\SalesLocal.cub file and create a second global cube named SalesLocal2. If you now try to save the SalesLocal2 global cube to the C:\SalesLocal.cub file, you will receive an error. However, you can save the SalesLocal2 global cube to a different local cube file.  
  
-   Global cubes do not support distinct count measures. Because cubes that include distinct count measures are nonadditive, the CREATE GLOBAL CUBE statement cannot support the creation or use of distinct count measures.  
  
-   When adding a measure to a local cube, you must also include at least one dimension that is related to the measure being added.  
  
-   When adding a parent-child hierarchy to a local cube, levels and filters on a parent-child hierarchy are ignored and the entire parent-child hierarchy is included.  
  
-   Member properties are not supported in local cubes.  
  
-   You cannot create a local cube from a perspective.  
  
-   When you include a semi-additive measure to a local cube, the following rules apply:  
  
    -   You must include the Account dimension if the AggregateFunction property for the measure being added is ByAccount.  
  
    -   You must include the entire Time dimension if the AggregateFunction property measure being added is FirstChild, LastChild, FirstNonEmpty, LastNonEmpty, or AverageOfChildren.  
  
-   Data mining dimensions cannot be added to a local cube.  
  
-   Reference dimensions are materialized and added as regular dimensions.  
  
-   When you include a many-to-many dimension, the following rules apply:  
  
    -   You must add the entire many-to-many dimension.  
  
    -   You must add the intermediary measure group.  
  
    -   You must add the entirety of all dimensions common to the two measure groups involved in the many-to-may relationship.  
  
 The following example demonstrates creating a local, persisted version of the Adventure Works cube that contains only the Reseller Sales Amount measure, the Reseller dimension, and the Date dimension.  
  
```  
CREATE GLOBAL CUBE [LocalReseller]  
   Storage 'C:\LocalAWReseller1.cub'  
   FROM [Adventure Works]  
   (  
      MEASURE  [Adventure Works].[Reseller Sales Amount],  
      DIMENSION [Adventure Works].[Reseller],  
      DIMENSION [Adventure Works].[Date]  
   )  
```  
  
 The following example demonstrates slicing when you create a local cube. The global cube that is created is based on the Adventure Works cube sliced vertically by the 2005 member of the Fiscal Year level, and horizontally by the Fiscal Year and Month levels.  
  
```  
CREATE GLOBAL CUBE [LocalReseller]  
   Storage 'C:\LocalAWReseller2.cub'  
   FROM [Adventure Works]  
   (  
      MEASURE  [Adventure Works].[Reseller Sales Amount],  
      DIMENSION [Adventure Works].[Reseller],  
      DIMENSION [Adventure Works].[Date]  
      (  
LEVEL [Fiscal Year],  
LEVEL [Month],  
MEMBER [Date].[Fiscal].[Fiscal Year].&[2005]  
      )  
   )  
```  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)   
 [CREATE SESSION CUBE Statement  &#40;MDX&#41;](../mdx/mdx-data-definition-create-session-cube.md)  
  
