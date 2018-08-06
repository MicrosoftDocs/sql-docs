---
title: "Order (MDX) | Microsoft Docs"
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
# Order (MDX)


  Arranges members of a specified set, optionally preserving or breaking the hierarchy.  
  
## Syntax  
  
```  
  
Numeric expression syntax  
Order(Set_Expression, Numeric_Expression   
[ , { ASC | DESC | BASC | BDESC } ] )  
  
String expression syntax  
Order(Set_Expression, String_Expression   
[ , { ASC | DESC | BASC | BDESC } ] )  
  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
 *String_Expression*  
 A valid string expression that is typically a valid Multidimensional Expressions (MDX) expression of cell coordinates that return a number expressed as a string.  
  
## Remarks  
 The **Order** function can either be hierarchical (as specified by using the **ASC** or **DESC** flag) or nonhierarchical (as specified by using the **BASC** or **BDESC** flag; the **B** stands for "break hierarchy"). If **ASC** or **DESC** is specified, the **Order** function first arranges the members according to their position in the hierarchy, and then orders each level. If **BASC** or **BDESC** is specified, the **Order** function arranges members in the set without regard to the hierarchy. In no flag is specified, **ASC** is the default.  
  
 If the **Order** function is used with a set where two or more hierarchies are crossjoined, and the **DESC** flag is used, only the members of the last hierarchy in the set are ordered. This is a change from Analysis Services 2000 where all hierarchies in the set were ordered.  
  
## Examples  
 The following example returns, from the **Adventure Works** cube, the number of reseller orders for all Calendar Quarters from the Calendar hierarchy on the Date dimension.The **Order** function reorders the set for the ROWS axis. The **Order** function orders the set by `[Reseller Order Count]` in descending hierarchical order as determined by the `[Calendar]` hierarchy.  
  
 `SELECT`  
  
 `Measures.[Reseller Order Count] ON COLUMNS,`  
  
 `Order(`  
  
 `[Date].[Calendar].[Calendar Quarter].MEMBERS`  
  
 `,Measures.[Reseller Order Count]`  
  
 `,DESC`  
  
 `) ON ROWS`  
  
 `FROM [Adventure Works]`  
  
 Notice how in this example, when the **DESC** flag is changed to **BDESC**, the hierarchy is broken and the list of Calendar Quarters is returned with no regard for the hierarchy:  
  
 `SELECT`  
  
 `Measures.[Reseller Order Count] ON COLUMNS,`  
  
 `Order(`  
  
 `[Date].[Calendar].[Calendar Quarter].MEMBERS`  
  
 `,Measures.[Reseller Order Count]`  
  
 `,BDESC`  
  
 `) ON ROWS`  
  
 `FROM [Adventure Works]`  
  
 The following example returns the Reseller Sales Measure for the top five selling subcategories of products, irrespective of hierarchy, based on Reseller Gross Profit. The **Subset** function is used to return only the first 5 tuples in the set after the result is ordered using the **Order** function.  
  
 `SELECT Subset`  
  
 `(Order`  
  
 `([Product].[Product Categories].[SubCategory].members`  
  
 `,[Measures].[Reseller Gross Profit]`  
  
 `,BDESC`  
  
 `)`  
  
 `,0`  
  
 `,5`  
  
 `) ON 0`  
  
 `FROM [Adventure Works]`  
  
 The following example uses the **Rank** function to rank the members of the City hierarchy, based on the Reseller Sales Amount measure, and then displays them in ranked order. By using the **Order** function to first order the set of members of the City hierarchy, the sorting is done only once and then followed by a linear scan before being presented in sorted order.  
  
```  
WITH   
SET OrderedCities AS Order  
   ([Geography].[City].[City].members  
   , [Measures].[Reseller Sales Amount], BDESC  
   )  
MEMBER [Measures].[City Rank] AS Rank  
   ([Geography].[City].CurrentMember, OrderedCities)  
SELECT {[Measures].[City Rank],[Measures].[Reseller Sales Amount]}  ON 0   
,Order  
   ([Geography].[City].[City].MEMBERS  
   ,[City Rank], ASC)  
    ON 1  
FROM [Adventure Works]  
```  
  
 The following example returns the number of products in the set that are unique, using the **Order** function to order the non-empty tuples before utilizing the **Filter** function. The **CurrentOrdinal** function is used to compare and eliminate ties.  
  
```  
WITH MEMBER [Measures].[PrdTies] AS Count  
   (Filter  
      (Order  
        (NonEmpty  
          ([Product].[Product].[Product].Members  
          , {[Measures].[Reseller Order Quantity]}  
          )  
       , [Measures].[Reseller Order Quantity]  
       , BDESC  
       ) AS OrdPrds  
    , (OrdPrds.CurrentOrdinal < OrdPrds.Count   
       AND [Measures].[Reseller Order Quantity] =   
          ( [Measures].[Reseller Order Quantity]  
            , OrdPrds.Item  
               (OrdPrds.CurrentOrdinal  
               )  
            )  
         )  
         OR (OrdPrds.CurrentOrdinal > 1   
            AND [Measures].[Reseller Order Quantity] =   
               ([Measures].[Reseller Order Quantity]  
               , OrdPrds.Item  
                  (OrdPrds.CurrentOrdinal-2)  
                )  
             )  
          )  
       )  
SELECT {[Measures].[PrdTies]} ON 0  
FROM [Adventure Works]  
```  
  
 To understand how the **DESC** flag works with sets of tuples, first consider the results of the following query:  
  
```  
  
SELECT  
{[Measures].[Tax Amount]} ON 0,  
ORDER(  
[Sales Territory].[Sales Territory].[Group].MEMBERS  
,[Measures].[Tax Amount], DESC)  
ON 1  
FROM [Adventure Works]  
  
```  
  
 On the Rows axis you can see that the Sales Territory Groups have been ordered in descending order by Tax Amount, as follows: North America, Europe, Pacific, NA. Now see what happens if we crossjoin the set of Sales Territory Groups with the set of Product Subcategories and apply the **Order** function in the same way, as follows:  
  
```  
  
SELECT  
{[Measures].[Tax Amount]} ON 0,  
ORDER(  
[Sales Territory].[Sales Territory].[Group].MEMBERS  
*  
{[Product].[Product Categories].[subCategory].Members}  
,[Measures].[Tax Amount], DESC)  
ON 1  
FROM [Adventure Works]  
  
```  
  
 While the set of Product Subcategories has been ordered in descending, hierarchical order, the Sales Territory Groups are now not sorted and appear in the order they appear on the hierarchy: Europe, NA, North America and Pacific. This is because only the last hierarchy in the set of tuples, Product Subcategories, is sorted. To reproduce the behavior of Analysis Services 2000, use a series of nested **Generate** functions to sort each set before it is crossjoined, for example:  
  
```  
  
SELECT  
{[Measures].[Tax Amount]} ON 0,  
GENERATE(  
ORDER(  
[Sales Territory].[Sales Territory].[Group].MEMBERS  
,[Measures].[Tax Amount], DESC)  
,  
ORDER(  
[Sales Territory].[Sales Territory].CURRENTMEMBER  
*  
{[Product].[Product Categories].[subCategory].Members}  
,[Measures].[Tax Amount], DESC))  
ON 1  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
