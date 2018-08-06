---
title: "CREATE SUBCUBE Statement (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Data Definition - CREATE SUBCUBE


  Redefines the cube space of a specified cube or subcube to a specified subcube. This statement changes the apparent cube space for subsequent operations.  
  
## Syntax  
  
```  
  
CREATE SUBCUBE Cube_Name AS Select_Statement  
                                                  | NON VISUAL ( Select_Statement )  
```  
  
## Arguments  
 *Cube_Name*  
 The valid string expression that provides the name of the cube or perspective that is being restricted, which becomes the name of the subcube.  
  
 *Select_Statement*  
 A valid Multidimensional Expressions (MDX) SELECT expression that does not contain WITH, NON EMPTY, or HAVING clauses, and does not request dimension or cell properties.  
  
 See [SELECT Statement &#40;MDX&#41;](../mdx/mdx-data-manipulation-select.md) for a detailed syntax explanation on Select statements and the **NON VISUAL** clause.  
  
## Remarks  
 When default members are excluded in the definition of a subcube, coordinates will correspondingly change. For attributes that can be aggregated, the default member is moved to the [All] member. For attributes that cannot be aggregated, the default member is moved to a member that exists in the subcube. The following table contains example subcube and default member combinations.  
  
|Original default member|Can be aggregated|Subselect|Revised default member|  
|-----------------------------|-----------------------|---------------|----------------------------|  
|Time.Year.All|Yes|{Time.Year.2003}|No change|  
|Time.Year.[1997]|Yes|{Time.Year.2003}|Time.Year.All|  
|Time.Year.[1997]|No|{Time.Year.2003}|Time.Year.[2003]|  
|Time.Year.[1997]|Yes|{Time.Year.2003, Time.Year.2004}|Time.Year.All|  
|Time.Year.[1997]|No|{Time.Year.2003, Time.Year.2004}|Either Time.Year.[2003] or<br /><br /> Time.Year.[2004]|  
  
 [All] members will always exist in a subcube.  
  
 Session objects created in the context of a subcube are dropped when the subcube is dropped.  
  
 For more information about subcubes, see [Building Subcubes in MDX &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/building-subcubes-in-mdx-mdx.md).  
  
## Example  
 The following example creates a subcube that restricts the apparent cube space to members that exist with the country of Canada. It then uses the **MEMBERS** function to return all members of the Country level of the Geography user-defined hierarchy - returning only the country of Canada.  
  
```  
CREATE SUBCUBE [Adventure Works] AS  
   SELECT [Geography].[Country].&[Canada] ON 0  
   FROM [Adventure Works]  
  
SELECT [Geography].[Country].[Country].MEMBERS ON 0  
   FROM [Adventure Works]  
  
```  
  
 The following example creates a subcube that restricts the apparent cube space to {Accessories, Clothing} members in Products.Category and {[Value Added Reseller], [Warehouse]} in Resellers.[Business Type].  
  
 `CREATE SUBCUBE [Adventure Works] AS`  
  
 `Select {[Category].Accessories, [Category].Clothing} on 0,`  
  
 `{[Business Type].[Value Added Reseller], [Business Type].[Warehouse]} on 1`  
  
 `from [Adventure Works]`  
  
 Querying the subcube for all members in Products.Category and Resellers.[Business Type] with the following MDX:  
  
 `select [Category].members on 0,`  
  
 `[Business Type].members on 1`  
  
 `from [Adventure Works]`  
  
 `where [Measures].[Reseller Sales Amount]`  
  
 Yields the following results:  
  
|||||  
|-|-|-|-|  
||All Products|Accessories|Clothing|  
|All Resellers|$2,031,079.39|$506,172.45|$1,524,906.93|  
|Value Added Reseller|$767,388.52|$175,002.81|$592,385.71|  
|Warehouse|$1,263,690.86|$331,169.64|$932,521.23|  
  
 Dropping and recreating the subcube using the NON VISUAL clause will create a subcube that keeps the true totals for all members in Products.Category and Resellers.[Business Type], whether they are visible or not in the subcube.  
  
 `CREATE SUBCUBE [Adventure Works] AS`  
  
 `NON VISUAL (Select {[Category].Accessories, [Category].Clothing} on 0,`  
  
 `{[Business Type].[Value Added Reseller], [Business Type].[Warehouse]} on 1`  
  
 `from [Adventure Works])`  
  
 Issuing the same MDX query from above:  
  
 `select [Category].members on 0,`  
  
 `[Business Type].members on 1`  
  
 `from [Adventure Works]`  
  
 `where [Measures].[Reseller Sales Amount]`  
  
 Yields the following different results:  
  
|||||  
|-|-|-|-|  
||All Products|Accessories|Clothing|  
|All Resellers|$80,450,596.98|$571,297.93|$1,777,840.84|  
|Value Added Reseller|$34,967,517.33|$175,002.81|$592,385.71|  
|Warehouse|$38,726,913.48|$331,169.64|$932,521.23|  
  
 The [All Products] and [All Resellers], column and row respectively, contains totals for all members not only those visible ones.  
  
## See Also  
 [Key Concepts in MDX &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/mdx/key-concepts-in-mdx-analysis-services.md)   
 [MDX Scripting Statements &#40;MDX&#41;](../mdx/mdx-scripting-statements-mdx.md)   
 [DROP SUBCUBE Statement &#40;MDX&#41;](../mdx/mdx-data-definition-drop-subcube.md)   
 [SELECT Statement &#40;MDX&#41;](../mdx/mdx-data-manipulation-select.md)  
  
  
