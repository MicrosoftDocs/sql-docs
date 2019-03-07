---
title: "Using Cell Properties (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "intrinsic cell properties [MDX]"
  - "cells [MDX]"
  - "cell properties [MDX]"
  - "CELL PROPERTIES keyword"
ms.assetid: a593c74d-8c5e-485e-bd92-08f9d22451d4
author: minewiskan
ms.author: owend
manager: craigg
---
# Using Cell Properties (MDX)
  Cell properties in Multidimensional Expressions (MDX) contain information about the content and format of cells in a multidimensional data source, such as a cube.  
  
 MDX supports the CELL PROPERTIES keyword in an MDX SELECT statement to retrieve intrinsic cell properties. Intrinsic cell properties are most commonly used to assist in the visual presentation of cell data.  
  
## CELL PROPERTIES Keyword Syntax  
 Use the following syntax for the `CELL PROPERTIES` keyword of the MDX `SELECT` statement:  
  
```  
SELECT [<axis_specification>  
       [, <axis_specification>...]]  
  FROM [<cube_specification>]  
[WHERE [<slicer_specification>]]  
[<cell_props>]  
```  
  
 The following syntax shows the format of the `<cell_props>` value and how this value uses the `CELL PROPERTIES` keyword along with one or more intrinsic cell properties:  
  
```  
<cell_props> ::= CELL PROPERTIES <property> [, <property>...]  
```  
  
## Supported Intrinsic Cell Properties  
 The following table lists the supported intrinsic cell properties that are used in the `<property>` value.  
  
|Property|Description|  
|--------------|-----------------|  
|`ACTION_TYPE`|A bitmask that indicates which types of actions exist on the cell. This property can have one of the following values:<br /><br /> **MDACTION_TYPE_URL**<br /><br /> **MDACTION_TYPE_HTML**<br /><br /> **MDACTION_TYPE_STATEMENT**<br /><br /> **MDACTION_TYPE_DATASET**<br /><br /> **MDACTION_TYPE_ROWSET**<br /><br /> **MDACTION_TYPE_COMMANDLINE**<br /><br /> **MDACTION_TYPE_PROPRIETARY**<br /><br /> **MDACTION_TYPE_REPORT**<br /><br /> **MDACTION_TYPE_DRILLTHROUGH**<br /><br /> <br /><br /> Note: Drillthrough actions are not included for queries containing a set in the where clause.|  
|**BACK_COLOR**|The background color for displaying the `VALUE` or `FORMATTED_VALUE` property. For more information, see [FORE_COLOR and BACK_COLOR Contents &#40;MDX&#41;](mdx-cell-properties-fore-color-and-back-color-contents.md).|  
|`CELL_ORDINAL`|The ordinal number of the cell in the dataset.|  
|**FONT_FLAGS**|The bitmask detailing effects on the font. For example, the value 5 represents the combination of bold (`MDFF_BOLD`) and underline (`MDFF_UNDERLINE`) font effects. The value is the result of a bitwise OR operation of one or more of the following constants:<br /><br /> `MDFF_BOLD` = 1<br /><br /> `MDFF_ITALIC` = 2<br /><br /> `MDFF_UNDERLINE` = 4<br /><br /> `MDFF_STRIKEOUT` = 8|  
|**FONT_NAME**|The font to be used to display the `VALUE` or `FORMATTED_VALUE` property.|  
|**FONT_SIZE**|Font size to be used to display the `VALUE` or `FORMATTED_VALUE` property.|  
|**FORE_COLOR**|The foreground color for displaying the `VALUE` or `FORMATTED_VALUE` property. For more information, see [FORE_COLOR and BACK_COLOR Contents &#40;MDX&#41;](mdx-cell-properties-fore-color-and-back-color-contents.md).|  
|`FORMAT`|Same as `FORMAT_STRING`.|  
|`FORMAT_STRING`|The format string used to create the `FORMATTED_VALUE` property value. For more information, see [FORMAT_STRING Contents &#40;MDX&#41;](mdx-cell-properties-format-string-contents.md).|  
|`FORMATTED_VALUE`|The character string that represents a formatted display of the `VALUE` property.|  
|`LANGUAGE`|The locale where the `FORMAT_STRING` will be applied. `LANGUAGE` is usually used for currency conversion.|  
|`UPDATEABLE`|A value that indicates whether the cell can be updated. This property can have one of the following values:<br /><br /> `MD_MASK_ENABLED` (0x00000000)   The cell can be updated.<br /><br /> `MD_MASK_NOT_ENABLED` (0x10000000)   The cell cannot be updated.<br /><br /> `CELL_UPDATE_ENABLED` (0x00000001)   Cell can be updated in the cellset.<br /><br /> `CELL_UPDATE_ENABLED_WITH_UPDATE` (0x00000002)   The cell can be updated with an update statement. The update may fail if a leaf cell is updated that is not write-enabled.<br /><br /> `CELL_UPDATE_NOT_ENABLED_FORMULA` (0x10000001)   The cell cannot be updated because the cell has a calculated member among its coordinates; the cell was retrieved with a set in the where clause. A cell can be updated even though a formula affects, or a calculated cell is on, the value of a cell (is somewhere along the aggregation path). In this scenario, the final value of the cell may not be the updated value, because the calculation will affect the result<br /><br /> `CELL_UPDATE_NOT_ENABLED_NONSUM_MEASURE` (0x10000002)   The cell cannot be updated because non-sum measures (count, min, max, distinct count, semi-additive) can not be updated.<br /><br /> `CELL_UPDATE_NOT_ENABLED_NACELL_VIRTUALCUBE` (0x10000003)   The cell cannot be updated because the cell does not exist as it is at the intersection of a measure and a dimension member unrelated to the measure's measure group.<br /><br /> `CELL_UPDATE_NOT_ENABLED_SECURE` (0x10000005)    The cell cannot be updated because the cell is secured.<br /><br /> `CELL_UPDATE_NOT_ENABLED_CALCLEVEL` (0x10000006)   Reserved for future use.<br /><br /> `CELL_UPDATE_NOT_ENABLED_CANNOTUPDATE` (0x10000007)   The cell cannot be updated because of internal reasons.<br /><br /> `CELL_UPDATE_NOT_ENABLED_INVALIDDIMENSIONTYPE` (0x10000009)   The cell cannot be updated because update is not supported in mining model, indirect, or data mining dimensions.|  
|`VALUE`|The unformatted value of the cell.|  
  
 Only the `CELL_ORDINAL`, `FORMATTED_VALUE`, and `VALUE` cell properties are required. All cell properties, intrinsic or provider-specific, are defined in the `PROPERTIES` schema rowset, including their data types and provider support. For more information about the `PROPERTIES` schema rowset, see [MDSCHEMA_PROPERTIES Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/ole-db-olap/mdschema-properties-rowset).  
  
 By default, if the `CELL PROPERTIES` keyword is not used, the cell properties returned are `VALUE`, `FORMATTED_VALUE`, and `CELL_ORDINAL` (in that order). If the `CELL PROPERTIES` keyword is used, only those cell properties explicitly stated with the keyword are returned.  
  
 The following example demonstrates the use of the `CELL PROPERTIES` keyword in an MDX query:  
  
```  
SELECT  
   {[Measures].[Reseller Gross Profit]} ON COLUMNS,  
   {[Reseller].[Reseller Type].[Reseller Name].Members} ON ROWS  
FROM [Adventure Works]  
CELL PROPERTIES VALUE, FORMATTED_VALUE, FORMAT_STRING, FORE_COLOR, BACK_COLOR  
```  
  
 Cell properties are not returned for MDX queries that return flattened rowsets; in this case, each cell is represented as if only the `FORMATTED_VALUE` cell property were returned.  
  
## Setting Cell Properties  
 Cell properties can be set in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] in various places. For example, the Format String property can be set for regular measures on the Cube Structure tab of the Cube Editor in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)]; the same property can be set for calculated measures defined on the cube on the Calculations tab of the Cube Editor; calculated measures defined in the WITH clause of a query have their format string defined there too.The following query demonstrates how cell properties can be set on a calculated measure::  
  
```  
WITH MEMBER MEASURES.CELLPROPERTYDEMO AS [Measures].[Internet Sales Amount]  
, FORE_COLOR=RGB(0,0,255)  
, BACK_COLOR=IIF([Measures].[Internet Sales Amount]>7000000, RGB(255,0,0), RGB(0,255,0))  
, FONT_SIZE=10  
, FORMAT_STRING='#,#.000'  
SELECT MEASURES.CELLPROPERTYDEMO ON 0,  
[Date].[Calendar Year].[Calendar Year].MEMBERS ON 1  
FROM [Adventure Works]  
CELL PROPERTIES VALUE, FORMATTED_VALUE, FORE_COLOR, BACK_COLOR, FONT_SIZE  
```  
  
## See Also  
 [MDX Query Fundamentals &#40;Analysis Services&#41;](mdx-query-fundamentals-analysis-services.md)  
  
  
