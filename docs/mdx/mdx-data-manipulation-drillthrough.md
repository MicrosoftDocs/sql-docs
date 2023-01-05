---
description: "MDX Data Manipulation - DRILLTHROUGH"
title: "DRILLTHROUGH Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Manipulation - DRILLTHROUGH


  Retrieves the underlying table rows that were used to create a specified cell in a cube.  
  
## Syntax  
  
```  
  
DRILLTHROUGH[MAXROWSUnsigned_Integer]   
      <MDX SELECT statement>   
      [RETURNSet_of_Attributes_and_Measures   
            [,Set_of_Attributes_and_Measures ...]  
      ]  
```  
  
## Arguments  
 *Unsigned_Integer*  
 A positive integer value.  
  
 *MDX SELECT statement*  
 Any valid Multidimensional Expressions (MDX) expressions SELECT statement.  
  
 *Set_of_Attributes_and_Measures*  
 A comma-separated list of dimension attributes and measures.  
  
## Remarks  
 Drillthrough is an operation in which an end user selects a single cell from a cube and retrieves a result set from the source data for that cell in order to get more detailed information. By default, a drillthrough result set is derived from the table rows that were evaluated to calculate the value of the selected cube cell. For end users to drill through, their client applications must support this capability. In [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the results are retrieved directly from MOLAP storage, unless ROLAP partitions or dimensions are queried.  
  
> [!IMPORTANT]  
>  Drillthrough security is based on the general security options defined on the cube. If a user cannot get some data by using MDX, drillthrough will also restrict the user in the exactly the same manner.  
  
 An MDX statement specifies the subject cell. The value specified by the **MAXROWS** argument indicates the maximum number of rows that should be returned by the resulting rowset.  
  
 By default, the maximum number of rows that are returned is 10,000 rows. This means that if you leave **MAXROWS** unspecified, you will get 10,000 rows or less. If this value is too low for your scenario, you can set **MAXROWS** to a higher number, such as `MAXROWS 20000`. If it is too low overall, you can increase the default by changing the **OLAP\Query\DefaultDrillthroughMaxRows** server property. For more information about changing this property, see [Server Properties in Analysis Services](/analysis-services/server-properties/server-properties-in-analysis-services).  
  
 Unless otherwise specified, the columns returned include all granularity attributes for all dimensions related to the measure group of the specified measure, other than many-to-many dimensions. Cube dimensions are preceded by $ to distinguish between dimensions and measure groups. The **RETURN** clause is used to specify the columns returned by the drillthrough query. The following functions can be applied to a single attribute or measure by the **RETURN** clause.  
  
 Name(attribute_name)  
 Returns the name of the specified attribute member.  
  
 UniqueName(attribute_name)  
 Returns the unique name of the specified attribute member.  
  
 Key(attribute_name[, N])  
 Returns the key of the specified attribute member, where N specifies column in the composite key (if any). The default value for N is 1.  
  
 Caption(attribute_name)  
 Returns the caption of the specified attribute member.  
  
 MemberValue(attribute_name)  
 Returns the member value of the specified attribute member.  
  
 CustomRollup(attribute_name)  
 Returns the custom rollup expression of the specified attribute member.  
  
 CustomRollupProperties(attribute_name)  
 Returns the custom rollup properties of the specified attribute member.  
  
 UnaryOperator(attribute_name)  
 Returns the unary operator of the specified attribute member.  
  
## Example  
 The following example specifies cell for the month of July, 2007 for the reseller sales amount measure (the default measure) for Australia. The RETURN clause specifies that the date of each sale, the product model name, the employee name, the sales amount, the tax amount and the product cost values that underlie this cell be returned.  
  
```  
DRILLTHROUGH  
SELECT  
   ([Date].[Calendar].[Month].[July 2007])  
ON 0   
FROM [Adventure Works]  
WHERE [Geography].[Country].[Australia]  
RETURN   
  [$Date].[Date]  
  ,KEY([$Product].[Model Name])  
  ,NAME([$Employee].[Employee])  
  ,[Reseller Sales].[Reseller Sales Amount]  
  ,[Reseller Sales].[Reseller Tax Amount]  
  ,[Reseller Sales].[Reseller Standard Product Cost]  
```  
  
## See Also  
 [MDX Data Manipulation Statements &#40;MDX&#41;](../mdx/mdx-data-manipulation-statements-mdx.md)  
  
