---
title: "Cube Space | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Cube Space
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Cube space is the product of the members of a cube's attribute hierarchies with the cube's measures. Therefore, the cube space is determined by the combinatorial product of all attribute hierarchy members in the cube and the cube's measures and defines the maximum size of the cube. It is important to note that this space includes all possible combinations of attribute hierarchy members; even combinations that might be deem as impossible in the real world i.e. combinations where the city is Paris and the countries are England or Spain or Japan or India or elsewhere.  
  
## Autoexists and cube space  
 The concept of *autoexists* limits this cube space to those cells that actually exist. Members of an attribute hierarchy in a dimension may not exist with members of another attribute hierarchy in the same dimension.  
  
 For example, if you have a cube that has a City attribute hierarchy, a Country attribute hierarchy, and an Internet Sales Amount measure, the space of this cube only includes those members that exist with each other. For example, if the City attribute hierarchy includes the cities New York, London, Paris, Tokyo, and Melbourne; and the Country attribute hierarchy includes the countries United States, United Kingdom, France, Japan, and Australia; then the space of the cube does not include the space (cell) at the intersection of Paris and United States.  
  
 When querying cells that do not exist, non-existing cells return nulls; that is, they cannot contain calculations and you cannot define a calculation that writes to this space. For example, the following statement includes cells that do not exist.  
  
```  
SELECT [Customer].[Gender].[Gender].Members ON COLUMNS,  
{[Customer].[Customer].[Aaron A. Allen]  
   ,[Customer].[Customer].[Abigail Clark]} ON ROWS   
FROM [Adventure Works]  
WHERE Measures.[Internet Sales Amount]  
```  
  
> [!NOTE]  
>  This query uses the [Members (Set) (MDX)](../../../mdx/members-set-mdx.md) function to return the set of members of the Gender attribute hierarchy on the column axis, and crosses this set with the specified set of members from the Customer attribute hierarchy on the row axis.  
  
 When you execute the previous query, the cell at the intersection of Aaron A. Allen and Female displays a null. Similarly, the cell at the intersection of Abigail Clark and Male displays a null. These cells do not exist and cannot contain a value, but cells that do not exist can appear in the result returned by a query.  
  
 When you use the [Crossjoin (MDX)](../../../mdx/crossjoin-mdx.md) function to return the cross-product of attribute hierarchy members from attribute hierarchies in the same dimension, auto-exists limits those tuples being returned to the set of tuples that actually exist, rather than returning a full Cartesian product. For example, run and then examine the results from the execution of the following query.  
  
```  
SELECT CROSSJOIN  
   (  
      {[Customer].[Country].[United States]},  
         [Customer].[State-Province].Members  
  ) ON 0   
FROM [Adventure Works]  
WHERE Measures.[Internet Sales Amount]  
```  
  
> [!NOTE]  
>  Notice that 0 is used to designate the column axis, which is shorthand for axis(0) - which is the column axis.  
  
 The previous query only returns cells for members from each attribute hierarchy in the query that exist with each other. The previous query can also be written using the new * variant of the [* (Crossjoin) (MDX)](../../../mdx/crossjoin-mdx-operator-reference.md) function.  
  
```  
SELECT   
   [Customer].[Country].[United States] *   
      [Customer].[State-Province].Members  
ON 0   
FROM [Adventure Works]  
WHERE Measures.[Internet Sales Amount]  
```  
  
 The previous query could also be written in the following manner:  
  
```  
SELECT [Customer].[State-Province].Members  
ON 0   
FROM [Adventure Works]  
WHERE (Measures.[Internet Sales Amount],  
   [Customer].[Country].[United States])  
```  
  
 The cells values returned will be identical, although the metadata in the result set will be different. For example, with the previous query, the Country hierarchy was moved to the slicer axis (in the WHERE clause) and therefore does not appear explicitly in the result set.  
  
 Each of these three previous queries demonstrates the effect of the auto-exists behavior in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## User-Defined Hierarchies and Cube Space  
 The previous examples in this topic define positions in cube space by using attribute hierarchies. However, you can also define a position in cube space by using user-defined hierarchies that have been defined based on attribute hierarchies in a dimension. A user-defined hierarchy is a hierarchy of attribute hierarchies designed to facilitate browsing of cube data by users.  
  
 For example, the **CROSSJOIN** query in the previous section could also have been written as follows:  
  
```  
SELECT CROSSJOIN  
   (  
      {[Customer].[Country].[United States]},  
         [Customer].[Customer Geography].[State-Province].Members  
   )   
ON 0   
FROM [Adventure Works]  
WHERE Measures.[Internet Sales Amount]  
```  
  
 In the previous query, the Customer Geography user-defined hierarchy within the Customer dimension is used to define the position in cube space that was previously defined by using an attribute hierarchy. The identical position in cube space can be defined by using either attribute hierarchies or user-defined hierarchies.  
  
##  <a name="AttribRelationships"></a> Attribute Relationships and Cube Space  
 Defining attribute relationships between related attributes improves query performance (by facilitating the creation of appropriate aggregations) and affects the member of a related attribute hierarchy that appears with an attribute hierarchy member. For example, when you define a tuple that includes a member from the City attribute hierarchy and the tuple does not explicitly define the Country attribute hierarchy member, you might expect that the default Country attribute hierarchy member would be the related member of the Country attribute hierarchy. However, this is only true if an attribute relationship is defined between the City attribute hierarchy and the Country attribute hierarchy.  
  
 The following example returns the member of a related attribute hierarchy that is not included explicitly in the query.  
  
```  
WITH MEMBER Measures.x AS   
   Customer.Country.CurrentMember.Name  
SELECT Measures.x ON 0,  
Customer.City.Members ON 1  
FROM [Adventure Works]  
```  
  
> [!NOTE]  
>  Notice that the **WITH** keyword is used with the [CurrentMember (MDX)](../../../mdx/currentmember-mdx.md) and [Name (MDX)](../../../mdx/name-mdx.md) functions to create a calculated member for use in the query. For more information, see [The Basic MDX Query &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-the-basic-query.md).  
  
 In the previous query, the name of the member of the Country attribute hierarchy that is associated with each member of the State attribute hierarchy is returned. The expected Country member appears (because an attribute relationship is defined between the City and Country attributes). However, if no attribute relationship were defined between attribute hierarchies in the same dimension, the (All) member would be returned, as illustrated in the following query.  
  
```  
WITH MEMBER Measures.x AS   
   Customer.Education.Currentmember.Name  
SELECT Measures.x  ON 0,   
Customer.City.Members ON 1  
FROM [Adventure Works]  
```  
  
 In the previous query, the (All) member ("All Customers") is returned, because there is no relationship between Education and City. Therefore, the (All) member of the Education attribute hierarchy would be the default member of the Education attribute hierarchy used in any tuple involving the City attribute hierarchy where an Education member is not explicitly provided.  
  
## Calculation Context  
  
## See Also  
 [Key Concepts in MDX &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/key-concepts-in-mdx-analysis-services.md)   
 [Tuples](../../../analysis-services/multidimensional-models/mdx/tuples.md)   
 [Autoexists](../../../analysis-services/multidimensional-models/mdx/autoexists.md)   
 [Working with Members, Tuples, and Sets &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/working-with-members-tuples-and-sets-mdx.md)   
 [Visual Totals and Non Visual Totals](../../../analysis-services/multidimensional-models/mdx/visual-totals-and-non-visual-totals.md)   
 [MDX Language Reference &#40;MDX&#41;](../../../mdx/mdx-language-reference-mdx.md)   
 [Multidimensional Expressions &#40;MDX&#41; Reference](../../../mdx/multidimensional-expressions-mdx-reference.md)  
  
  
