---
description: "MDX Data Definition - CREATE SESSION CUBE"
title: "CREATE SESSION CUBE Statement  (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Definition - CREATE SESSION CUBE


  Creates and populates a session cube from an existing server cube. The session cube is only visible within the current session; it cannot be browsed or queried from any other session. The session cube is implicitly deleted when the session is closed.  
  
## Syntax  
  
```  
  
CREATE SESSION CUBE session_cube_name FROM <cube list> (<param list>)  
  
<cube list>::= source_cube_name [,<cube list>]  
  
<param list>::= <param> ,<param list> | <param>  
  
<param>::= <dims list> | <measures list>  
  
<measures list>::= <measure>[, <measures list>]   
  
<dims list>::= <dim def> [, <dims list>]  
  
<measure>::= MEASURE source_cube_name.measure_name [<visibility qualifier>] [AS measure_name]   
  
<dim def>::= <source dim def> | <derived dim def>  
  
<source dim def>::= DIMENSION source_cube_name.dimension_name [<dim flags>] [<visibility qualifier>] [AS dimension_name>] [FROM <dim from clause> ] [<dim content def>]  
  
<dim flags>::= NOT_RELATED_TO_FACTS   
  
<dim from clause>::= <reg dim from clause>   
  
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
 session_cube_name  
 The name of the session cube.  
  
 source_cube_name  
 The name of the cube on which the session cube is based.  
  
 source_cube_name.measure_name  
 The fully qualified name of the source measure being included in the session cube. Calculated members of the Measures dimension are not permitted.  
  
 measure_name  
 The name of the measure in the session cube.  
  
 source_cube_name.dimension_name  
 The fully qualified name of the source dimension being included in the session cube.  
  
 dimension_name  
 The name of the dimension in the session cube.  
  
 FROM \<dim from clause>  
 Valid specification for derived dimension definition only.  
  
 NOT_RELATED_TO_FACTS  
 Valid specification for derived dimension definition only.  
  
 \<level type>  
 Valid specification for derived dimension definition only.  
  
## Remarks  
 Unlike server and local cubes, a session cube is not persisted beyond the session that created the session cube. A session cube is defined in terms of the measures and definitions that define it. There are two types of dimensions.  
  
-   Source dimensions - These are dimensions that were part of one of more source cubes.  
  
-   Derived dimensions - These are dimensions that provide new analysis capabilities. A derived dimension can be a regular dimension defined based on a source dimension that is either sliced vertically or horizontally, or contains custom grouping of dimension members. A derived dimension can also be a data mining dimension based on a data mining model.  
  
> [!NOTE]  
>  The Dimension keyword can refer to either dimensions or hierarchies.  
  
 Session cubes are used primarily for dynamic grouping of attribute members into custom member groups by client applications, such as Microsoft Excel. In a session cube, you can perform the following tasks:  
  
-   Eliminate dimensions that exist in the source cube.  
  
-   Add or eliminate hierarchies from a dimension.  
  
-   Eliminate measure groups or specific measures.  
  
-   Add a new attribute, based on attribute binding, for purposes of creating groups against an existing attribute..  
  
> [!IMPORTANT]  
>  Security on session cube objects is inherited from the underlying source objects. Other objects, such as actions and calculation scripts, are also inherited by the session cube.  
  
 The CREATE SESSION CUBE statement follows these rules:  
  
-   You cannot perform grouping on parent-child hierarchies.  
  
-   You cannot perform grouping on ROLAP dimensions.  
  
-   You cannot perform grouping on linked dimensions.  
  
-   You cannot perform grouping on levels with custom rollups.  
  
-   You cannot perform grouping on discretized attribute hierarchies.  
  
-   You cannot perform grouping on unnatural hierarchies, which are hierarchies with many-to-many relationships between levels (such as age and gender).  
  
-   Explicit references to a cube name in MDX script are broken by grouping because the session cube has a different name. Use the CURRENTCUBE keyword instead.  
  
-   You cannot perform grouping on dimensions with explicit default members.  
  
-   When performing grouping, session-scoped calculated members on the original server cube are dropped.  
  
-   When performing grouping on a cube dimension in a server cube, the grouping affects all cube dimensions based on the same dimension.  
  
## Example  
 The following example demonstrates creating a session-scoped version of the Adventure Works cube that contains the Reseller Sales Amount measure, the Reseller dimension, the Product dimension, the Geography dimension, and the Date dimension. Within this session cube, two groups are created; one group contains countries/regions in Europe and one group contains groups in North America. This sample is a simplified version of a CREATE SESSION CUBE statement issued by Microsoft Excel when a user creates a custom grouping of members.  
  
```  
CREATE SESSION CUBE [Adventure Works_XL_GROUPING1]   
   FROM [Adventure Works]   
   ( MEASURE [Adventure Works].[Internet Sales Amount]  
   ,MEASURE [Adventure Works].[Reseller Sales Amount]  
   ,DIMENSION [Adventure Works].[Date].[Calendar]  
   ,DIMENSION [Adventure Works].[Date].[Calendar Year]  
   ,DIMENSION [Adventure Works].[Date].[Calendar Semester]  
   ,DIMENSION [Adventure Works].[Date].[Calendar Quarter]  
   ,DIMENSION [Adventure Works].[Date].[Month Name]  
   ,DIMENSION [Adventure Works].[Date].[Date]  
   ,DIMENSION [Adventure Works].[Geography].[Country]   
      HIDDEN AS _XL_GROUPING81  
   ,DIMENSION [Adventure Works].[Geography].[State-Province]  
   ,DIMENSION [Adventure Works].[Geography].[City]  
   ,DIMENSION [Adventure Works].[Geography].[Postal Code]  
   ,DIMENSION [Adventure Works].[Geography].[Geography]  
   ,DIMENSION [Adventure Works].[Product].[Product Categories]  
   ,DIMENSION [Adventure Works].[Product].[Category]  
   ,DIMENSION [Adventure Works].[Product].[Subcategory]  
   ,DIMENSION [Adventure Works].[Product].[Product]  
   ,DIMENSION [Adventure Works].[Product].[Product Key]  
   ,DIMENSION [Adventure Works].[Reseller].[Reseller]  
   ,DIMENSION [Adventure Works].[Reseller].[Geography Key]  
   ,DIMENSION [Geography].[Country]   
      NOT_RELATED_TO_FACTS FROM _XL_GROUPING81   
          ( LEVEL [(All)]  
         ,LEVEL [Country1] GROUPING  
         ,LEVEL [Country]  
            ,GROUP [Country1].[CountryXl_Grp_1]   
                ( MEMBER [Geography].[Country].&[Canada]  
                  ,MEMBER [Geography].[Country].&[United States] )  
            ,GROUP [Country1].[CountryXl_Grp_2]   
                ( MEMBER [Geography].[Country].&[France]  
                  ,MEMBER [Geography].[Country].&[Germany]  
                  ,MEMBER [Geography].[Country].&[United Kingdom] )   
            )   
   )  
```  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)   
 [CREATE GLOBAL CUBE Statement  &#40;MDX&#41;](../mdx/mdx-data-definition-create-global-cube.md)  
  
  
