---
title: "Subselects in Queries | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 9e361798-688e-4b11-9eef-31fc793e8ba4
author: minewiskan
ms.author: owend
manager: craigg
---
# Subselects in Queries
  Subselect expressions are nested SELECT expressions that are used to restrict the space of the cube from where the outer external SELECT is being evaluated. Subselects allow you to define a new space over which all calculations are evaluated.  
  
## Subselects by example  
 Let's begin with an example of how subselects can help to produce the results we want to show. Assume that you are requested to produce a table that shows the sales behavior, over years, for the top 10 products.  
  
 The result should look like the following table:  
  
|||||  
|-|-|-|-|  
||Sum of Years|Year 1|...|  
|Sum of Top 10 Products||||  
|Product A||||  
|...||||  
  
 To do something like the above one could write the following MDX expression:  
  
```  
SELECT [Date].[Calendar Year].MEMBERS on 0  
     , TOPCOUNT( [Product].[Product].MEMBERS  
               , 10  
               , [Measures].[Sales Amount]  
               ) ON 1  
  FROM [Adventure Works]  
```  
  
 That returns the following results:  
  
|||||||  
|-|-|-|-|-|-|  
||All Periods|CY 2005|CY 20062|CY 2007|CY 2008|  
|All Products|$80,450,596.98|$8,065,435.31|$24,144,429.65|$32,202,669.43|$16,038,062.60|  
|Mountain-200 Black, 38|$1,634,647.94|(null)|(null)|$894,207.97|$740,439.97|  
|Mountain-200 Black, 42|$1,285,524.65|(null)|(null)|$722,137.65|$563,387.00|  
|Mountain-200 Silver, 38|$1,181,945.82|(null)|(null)|$634,600.78|$547,345.03|  
|Mountain-200 Black, 46|$995,927.43|(null)|(null)|$514,995.76|$480,931.68|  
|Mountain-200 Silver, 42|$1,005,111.77|(null)|(null)|$529,543.29|$475,568.49|  
|Mountain-200 Silver, 46|$975,932.56|(null)|(null)|$526,759.30|$449,173.26|  
|Road-150 Red, 56|$792,228.98|$382,159.24|$410,069.74|(null)|(null)|  
|Mountain-200 Black, 38|$1,471,078.72|(null)|$789,958.49|$681,120.23|(null)|  
|Road-350-W Yellow, 48|$1,380,253.88|(null)|(null)|$744,988.37|$635,265.50|  
  
 Which is very close to what we are looking for; except that the query returned 9 not 10 products and the All Products total reflects the sum of all products not the sum of the returned Top 9 (in this case). Another attempt to solve the problem is presented in the following MDX query:  
  
```  
SELECT [Date].[Calendar Year].MEMBERS on 0  
     , TOPCOUNT( [Product].[Product].CHILDREN, 10, [Measures].[Sales Amount]) ON 1  
  FROM [Adventure Works]  
```  
  
 That returns the following results:  
  
|||||||  
|-|-|-|-|-|-|  
||All Periods|CY 2005|CY 2006|CY 2007|CY 2008|  
|Mountain-200 Black, 38|$1,634,647.94|(null)|(null)|$894,207.97|$740,439.97|  
|Mountain-200 Black, 42|$1,285,524.65|(null)|(null)|$722,137.65|$563,387.00|  
|Mountain-200 Silver, 38|$1,181,945.82|(null)|(null)|$634,600.78|$547,345.03|  
|Mountain-200 Black, 46|$995,927.43|(null)|(null)|$514,995.76|$480,931.68|  
|Mountain-200 Silver, 42|$1,005,111.77|(null)|(null)|$529,543.29|$475,568.49|  
|Mountain-200 Silver, 46|$975,932.56|(null)|(null)|$526,759.30|$449,173.26|  
|Road-150 Red, 56|$792,228.98|$382,159.24|$410,069.74|(null)|(null)|  
|Mountain-200 Black, 38|$1,471,078.72|(null)|$789,958.49|$681,120.23|(null)|  
|Road-350-W Yellow, 48|$1,380,253.88|(null)|(null)|$744,988.37|$635,265.50|  
|Road-150 Red, 62|$566,797.97|$234,018.86|$332,779.11|(null)|(null)|  
  
 That was very close to the desired result because it only missed the sum of the products. At this point one can start tweaking the above MDX expression to add the missing line; however, that task could prove to be a cumbersome one.  
  
 Another approach to solve the problem, would be to start thinking in terms of redefining the cube space over which the MDX expression is resolved. What if the 'new' cube contains only data from the Top 10 products? That cube then will have the All member adjusted to the Top 10 products only and now a simple query would resolve our needs.  
  
 The following MDX expression uses a subselect statement to redefine the cube space to the Top 10 products and produce the desired results:  
  
```  
SELECT [Date].[Calendar Year].MEMBERS on 0  
     , [Product].[Product].MEMBERS on 1  
  FROM (SELECT TOPCOUNT( [Product].[Product].CHILDREN  
                       , 10  
                       , [Measures].[Sales Amount]  
                       ) ON 0  
          FROM [Adventure Works]  
        )  
 WHERE [Measures].[Sales Amount]  
```  
  
 The above expression returns the following results:  
  
|||||||  
|-|-|-|-|-|-|  
||All Periods|CY 2005|CY 2006|CY 2007|CY 2008|  
|All Products|$19,997,183.30|$1,696,815.63|$2,816,611.28|$7,930,797.72|$7,552,958.66|  
|Mountain-200 Silver, 38|$2,160,981.60|(null)|(null)|$1,024,359.10|$1,136,622.49|  
|Mountain-200 Silver, 42|$1,914,547.85|(null)|(null)|$903,061.68|$1,011,486.18|  
|Mountain-200 Silver, 46|$1,906,248.55|(null)|(null)|$877,077.79|$1,029,170.76|  
|Mountain-200 Black, 38|$1,811,229.02|(null)|$896,511.60|$914,717.43|(null)|  
|Mountain-200 Black, 38|$2,589,363.78|(null)|(null)|$1,261,406.37|$1,327,957.41|  
|Mountain-200 Black, 42|$2,265,485.38|(null)|(null)|$1,126,055.89|$1,139,429.49|  
|Mountain-200 Black, 46|$1,957,528.24|(null)|(null)|$946,453.88|$1,011,074.37|  
|Road-150 Red, 62|$1,769,096.69|$828,011.68|$941,085.01|(null)|(null)|  
|Road-150 Red, 56|$1,847,818.63|$868,803.96|$979,014.67|(null)|(null)|  
|Road-350-W Yellow, 48|$1,774,883.56|(null)|(null)|$877,665.59|$897,217.96|  
  
 The results above are exactly what we were looking for.  
  
 Let's review what exactly did the subselect do for us. The subselect returned for us a new cube that contained all other dimensions from product as they are; but, in the product dimension it filtered all members to match the top 10 products that we were interested in. It was as if we had removed all data that did not meet the Top 10 criteria and had rebuilt the cube. The other important concept to understand in this example is the fact that the Top 10 products were calculated over the All member of all the other dimensions in the cube; the former is true because no other filtering restrictions were imposed in the subselect.  
  
 Subselects can be as complex as one would like; the following example will illustrate how to produce a similar table as the mentioned above but filtered on France on the Sales Territory dimension and on Internet for the Sales Channel dimension.  
  
```  
SELECT [Date].[Calendar Year].MEMBERS on 0  
     , [Product].[Product].MEMBERS on 1  
  FROM (SELECT TOPCOUNT( [Product].[Product].CHILDREN  
                       , 10  
                       , [Measures].[Sales Amount]  
                       ) ON 0  
             , [Sales Territory].[Sales Territory].[Region].[France] on 1  
             ,  [Sales Channel].[Sales Channel].[Internet] on 2  
          FROM [Adventure Works]  
        )  
 WHERE [Measures].[Sales Amount]  
```  
  
 Produced the following results:  
  
|||||||  
|-|-|-|-|-|-|  
||All Periods|CY 2005|CY 2006|CY 2007|CY 2008|  
|All Products|$748,682.49|$32,204.43|$73,125.18|$269,506.56|$373,846.32|  
|Mountain-200 Silver, 38|$90,479.61|(null)|(null)|$41,759.82|$48,719.79|  
|Mountain-200 Silver, 42|$97,439.58|(null)|(null)|$39,439.83|$57,999.75|  
|Mountain-200 Silver, 46|$102,079.56|(null)|(null)|$27,839.88|$74,239.68|  
|Mountain-200 Black, 38|$26,638.28|(null)|$12,294.59|$14,343.69|(null)|  
|Mountain-200 Black, 38|$96,389.58|(null)|(null)|$41,309.82|$55,079.76|  
|Mountain-200 Black, 42|$80,324.65|(null)|(null)|$43,604.81|$36,719.84|  
|Mountain-200 Black, 46|$107,864.53|(null)|(null)|$45,899.80|$61,964.73|  
|Road-150 Red, 62|$46,517.51|$14,313.08|$32,204.43|(null)|(null)|  
|Road-150 Red, 56|$46,517.51|$17,891.35|$28,626.16|(null)|(null)|  
|Road-350-W Yellow, 48|$54,431.68|(null)|(null)|$15,308.91|$39,122.77|  
  
 The above results are the Top 10 products sold in France through the Internet channel.  
  
## Subselect statement  
 The BNF for the Subselect is:  
  
```  
[WITH [<calc-clause> ...]]  
SELECT [<axis-spec> [, <axis-spec> ...]]  
FROM [<identifier> | (< sub-select-statement >)]  
[WHERE <slicer>]  
[[CELL] PROPERTIES <cellprop> [, <cellprop> ...]]  
  
< sub-select-statement > :=  
   SELECT [<axis-spec> [, <axis-spec> ...]]  
   FROM [<identifier> | (< sub-select-statement >)]  
   [WHERE <slicer>]  
  
```  
  
 The subselect is another Select statement where the axis specifications and the slicer specification filter the space of the cube over which the outer select is evaluated.  
  
 When a member is specified in one of the axis or slicer clause then that member with its ascendants and descendants are included in the sub cube space for the subselect; all non mentioned sibling members, in the axis or slicer clause, and their descendants are filtered from the subspace. This way, the space of the outer select has been limited to the existing members in the axis clause or slicer clause, with their ascendants and descendants as mentioned before.  
  
 Because the All member of all non mentioned dimensions in axis or slicer clauses belongs to the space of the select; then, all descendants to the All member on those dimensions are, also, part of the subselect space.  
  
 The All member, in all dimensions, in the subcube space, is re-evaluated to reflect the impact of the constrains of the new space.  
  
 The following example will show what has been said above; the first MDX expression helps to display the unfiltered values in the cube, the second MDX illustrates the effect of filtering in the Subselect clause:  
  
```  
SELECT { [Customer].[Customer Geography].[All Customers]  
       , [Customer].[Customer Geography].[Country].&[United States]  
       , [Customer].[Customer Geography].[State-Province].&[OR]&[US]  
       , [Customer].[Customer Geography].[City].&[Portland]&[OR]  
       , [Customer].[Customer Geography].[State-Province].&[WA]&[US]  
       , [Customer].[Customer Geography].[City].&[Seattle]&[WA]  
       } ON 1  
     ,  {[Measures].[Internet Sales Amount], [Measures].[Reseller Sales Amount]} ON 0  
  FROM [Adventure Works]  
```  
  
 Returns the following values:  
  
||||  
|-|-|-|  
||Internet Sales Amount|Reseller Sales Amount|  
|All Customers|$29,358,677.22|$80,450,596.98|  
|United States|$9,389,789.51|$80,450,596.98|  
|Oregon|$1,170,991.54|$80,450,596.98|  
|Portland|$110,649.54|$80,450,596.98|  
|Washington|$2,467,248.34|$80,450,596.98|  
|Seattle|$75,164.86|$80,450,596.98|  
  
 In the above example Seattle is a child of Washington, Portland is a child of Oregon, Oregon and Washington are children of United States and United States is a child of [Customer Geography].[All Customers]. All of the shown members in this example have other siblings that contribute to the parent aggregate value; i.e. Spokane, Tacoma and Everett are sibling cities of Seattle and they all contribute to Washington Internet Sales Amount. Reseller Sales Amount value is independent of Customer Geography attribute; hence the All value is displayed in the results. The next MDX expression illustrates the filtering effect of the subselect clause:  
  
```  
SELECT { [Customer].[Customer Geography].[All Customers]  
       , [Customer].[Customer Geography].[Country].&[United States]  
       , [Customer].[Customer Geography].[State-Province].&[OR]&[US]  
       , [Customer].[Customer Geography].[City].&[Portland]&[OR]  
       , [Customer].[Customer Geography].[State-Province].&[WA]&[US]  
       , [Customer].[Customer Geography].[City].&[Seattle]&[WA]  
       } ON 1  
     ,  {[Measures].[Internet Sales Amount], [Measures].[Reseller Sales Amount]} ON 0  
  FROM ( SELECT [Customer].[State-Province].&[WA]&[US] ON 0  
           FROM [Adventure Works]  
        )  
```  
  
 Returns the following values:  
  
||||  
|-|-|-|  
||Internet Sales Amount|Reseller Sales Amount|  
|All Customers|$2,467,248.34|$80,450,596.98|  
|United States|$2,467,248.34|$80,450,596.98|  
|Washington|$2,467,248.34|$80,450,596.98|  
|Seattle|$75,164.86|$80,450,596.98|  
  
 The results above show that only ascendants and descendants of Washington State are part of the subspace where the outer select statement was evaluated; Oregon and Portland have been removed from the subcube because Oregon and all the other sibling states were not mentioned in the subselect when Washington was mentioned.  
  
 The All member was adjusted to reflect the filtering on Washington; not only was adjusted in the [Customer Geography] dimension but in all other dimensions that crossed with [Customer Geography]. All dimensions that do not cross with [Customer Geography] remain in the subcube unaltered.  
  
 The following two MDX statements illustrate how the All member in other dimensions is adjusted to meet the filtering effect of the subselect. The first query displays the unaltered results and the second shows the impact of the filtering:  
  
```  
SELECT { [Customer].[Customer Geography].[All Customers]  
       , [Customer].[Customer Geography].[Country].&[United States]  
       , [Customer].[Customer Geography].[State-Province].&[OR]&[US]  
       , [Customer].[Customer Geography].[City].&[Portland]&[OR]  
       , [Customer].[Customer Geography].[State-Province].&[WA]&[US]  
       , [Customer].[Customer Geography].[City].&[Seattle]&[WA]  
       } ON 1  
     ,   [Product].[Product Line].MEMBERS ON 0  
  FROM [Adventure Works]  
 WHERE [Measures].[Internet Sales Amount]  
```  
  
||||||||  
|-|-|-|-|-|-|-|  
||All Products|Accessory|Components|Mountain|Road|Touring|  
|All Customers|$29,358,677.22|$604,053.30|(null)|$10,251,183.52|$14,624,108.58|$3,879,331.82|  
|United States|$9,389,789.51|$217,168.79|(null)|$3,547,956.78|$4,322,438.41|$1,302,225.54|  
|Oregon|$1,170,991.54|$30,513.17|(null)|$443,607.98|$565,372.10|$131,498.29|  
|Portland|$110,649.54|$2,834.17|(null)|$47,099.91|$53,917.17|$6,798.29|  
|Washington|$2,467,248.34|$62,662.92|(null)|$945,219.38|$1,155,880.07|$303,485.97|  
|Seattle|$75,164.86|$2,695.74|(null)|$19,914.53|$44,820.06|$7,734.54|  
  
```  
SELECT { [Customer].[Customer Geography].[All Customers]  
       , [Customer].[Customer Geography].[Country].&[United States]  
       , [Customer].[Customer Geography].[State-Province].&[OR]&[US]  
       , [Customer].[Customer Geography].[City].&[Portland]&[OR]  
       , [Customer].[Customer Geography].[State-Province].&[WA]&[US]  
       , [Customer].[Customer Geography].[City].&[Seattle]&[WA]  
       } ON 1  
     ,   [Product].[Product Line].MEMBERS ON 0  
  FROM ( SELECT [Customer].[State-Province].&[WA]&[US] ON 0  
           FROM [Adventure Works]  
        )  
 WHERE [Measures].[Internet Sales Amount]  
```  
  
||||||||  
|-|-|-|-|-|-|-|  
||All Products|Accessory|Components|Mountain|Road|Touring|  
|All Customers|$2,467,248.34|$62,662.92|(null)|$945,219.38|$1,155,880.07|$303,485.97|  
|United States|$2,467,248.34|$62,662.92|(null)|$945,219.38|$1,155,880.07|$303,485.97|  
|Washington|$2,467,248.34|$62,662.92|(null)|$945,219.38|$1,155,880.07|$303,485.97|  
|Seattle|$75,164.86|$2,695.74|(null)|$19,914.53|$44,820.06|$7,734.54|  
  
 The above results show that All Products values have been adjusted to only values from Washington State, as expected.  
  
 Subselects can be nested with no limit to how deep you can nest subselects, except available memory. The inner most subselect defines the starting subspace over which filtering is applied and passed onto the next outer select. An important thing to notice is the fact that nesting is not a commutative operation; so the order in which the nesting is set my produce different results. The following examples should show the difference when choosing a nesting order.  
  
```  
SELECT [Sales Territory].[Sales Territory Region].MEMBERS on 0  
     , [Product].[Product].MEMBERS on 1  
  FROM (SELECT TOPCOUNT( [Product].[Product].CHILDREN, 5, [Measures].[Sales Amount]) ON 0  
          FROM (SELECT TOPCOUNT( [Sales Territory].[Sales Territory Region].CHILDREN, 5, [Measures].[Sales Amount]) on 0  
                  FROM [Adventure Works]  
               )  
        )  
 WHERE [Measures].[Sales Amount]  
```  
  
 Returned the following results.  
  
||||||||  
|-|-|-|-|-|-|-|  
||All Sales Territories|Australia|Canada|Central|Northwest|Southwest|  
|All Products|$7,591,495.49|$1,281,059.99|$1,547,298.12|$600,205.79|$1,924,763.50|$2,238,168.08|  
|Mountain-200 Silver, 38|$1,449,576.15|$248,702.93|$275,052.45|$141,103.65|$349,487.01|$435,230.12|  
|Mountain-200 Black, 38|$1,722,896.50|$218,024.05|$418,726.43|$123,929.46|$486,694.63|$475,521.93|  
|Mountain-200 Black, 42|$1,573,655.14|$239,137.96|$319,921.61|$130,102.75|$420,445.84|$464,046.98|  
|Mountain-200 Black, 46|$1,420,500.58|$192,320.16|$230,875.99|$117,044.49|$424,813.66|$455,446.27|  
|Road-150 Red, 56|$1,424,867.11|$382,874.89|$302,721.64|$88,025.44|$243,322.36|$407,922.78|  
  
```  
SELECT [Sales Territory].[Sales Territory Region].MEMBERS on 0  
     , [Product].[Product].MEMBERS on 1  
  FROM (SELECT TOPCOUNT( [Sales Territory].[Sales Territory Region].CHILDREN, 5, [Measures].[Sales Amount]) ON 0  
          FROM (SELECT TOPCOUNT( [Product].[Product].CHILDREN, 5, [Measures].[Sales Amount]) on 0  
                  FROM [Adventure Works]  
               )  
        )  
 WHERE [Measures].[Sales Amount]  
```  
  
 Returned the following results.  
  
||||||||  
|-|-|-|-|-|-|-|  
||All Sales Territories|Australia|Canada|Northwest|Southwest|United Kingdom|  
|All Products|$7,938,218.56|$1,096,312.24|$1,474,255.49|$2,042,674.72|$2,238,099.55|$1,086,876.56|  
|Mountain-200 Silver, 38|$1,520,958.53|$248,702.93|$275,052.45|$349,487.01|$435,230.12|$212,486.03|  
|Mountain-200 Silver, 42|$1,392,237.14|$198,127.15|$229,679.01|$361,233.58|$407,854.24|$195,343.16|  
|Mountain-200 Black, 38|$1,861,703.23|$218,024.05|$418,726.43|$486,694.63|$475,521.93|$262,736.19|  
|Mountain-200 Black, 42|$1,702,427.25|$239,137.96|$319,921.61|$420,445.84|$464,046.98|$258,874.87|  
|Mountain-200 Black, 46|$1,460,892.41|$192,320.16|$230,875.99|$424,813.66|$455,446.27|$157,436.31|  
  
 As you can see there are differences in the results between both sets. The first query answered the question of which are the best selling products in the top 5 selling regions, the second query answered the question of where are the largest sales of the top 5 selling products.  
  
### Remarks  
 Subselects have the following restrictions and limitations:  
  
-   The WHERE clause does not filter the subspace.  
  
-   The WHERE clause changes the default member in the sub cube only.  
  
-   The NON EMPTY clause is not allowed in an axis clause; use a [NonEmpty &#40;MDX&#41;](/sql/mdx/nonempty-mdx) function expression instead.  
  
-   The HAVING clause is not allowed in an axis clause; use a [Filter &#40;MDX&#41;](/sql/mdx/filter-mdx) function expression instead.  
  
-   By default calculated members are not allowed in subselects; however, this restriction can be changed, in a per session basis, by assigning a value to the `SubQueries` connection string property in <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A> or `DBPROP_MSMD_SUBQUERIES` property in [Supported XMLA Properties &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties). See [Calculated Members in Subselects and Subcubes](calculated-members-in-subselects-and-subcubes.md) for a detailed explanation of the behavior of calculated members depending on the values of `SubQueries` or `DBPROP_MSMD_SUBQUERIES`.  
  
  
