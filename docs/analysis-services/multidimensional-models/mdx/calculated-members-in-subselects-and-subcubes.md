---
title: "Calculated Members in Subselects and Subcubes | Microsoft Docs"
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
# Calculated Members in Subselects and Subcubes
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  A calculated member is a dimension member whose value is calculated from an expression at run time, and can be used in subselects and subcubes to more precisely define the cubespace of a query.  
  
## Enabling calculated members in the subspace  
 The **SubQueries** connection string property in <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A> or the **DBPROPMSMDSUBQUERIES** property in [Supported XMLA Properties &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties) defines the behavior or allowance of calculated members or calculated sets on subselects or subcubes. In the context of this document, subselect refers to subselects and subcubes unless otherwise stated.  
  
 The SubQueries property allows the following values.  
  
|||  
|-|-|  
|Value|Description|  
|0|Calculated members are not allowed in subselects or subcubes.<br /><br /> An error is raised when evaluating the subselect or subcube if a calculated member is referenced.|  
|1|Calculated members are allowed in subselects or subcubes but no ascendant members are introduced in the returning subspace.|  
|2|Calculated members are allowed in subselects or subcubes and ascendant members are introduced in the returning subspace. Also, mixed granularity is allowed in the selection of calculated members.|  
  
 Using values of 1 or 2 in the SubQueries property allows calculated members to be used to filter the returning subspace of subselects.  
  
 An example will help clarify the concept; first a calculated member must be created and then a subselect query issued to show the above mentioned behavior.  
  
 The following sample creates a calculated member that adds [Seattle Metro] as a city to the [Geography].[Geography] hierarchy under Washington state.  
  
 To run the example, the connection string must contain the SubQueries property with a value of 1 and all MDX statements must be run in the same session.  
  
> [!IMPORTANT]  
>  If you're using Management Studio to test the queries, click the Options button on Connection Manager to access the additional connection string properties pane, where you can enter subqueries=1 or 2 to allow calculated members in the subspace.  
  
 First issue the following MDX expression:  
  
```  
  
CREATE MEMBER [Adventure Works].[Geography].[Geography].[State-Province].&[WA]&[US].[Seattle Metro Agg]   
   AS  AGGREGATE(   
                 {   
                   [Geography].[Geography].[City].&[Bellevue]&[WA]  
                 , [Geography].[Geography].[City].&[Issaquah]&[WA]  
                 , [Geography].[Geography].[City].&[Redmond]&[WA]  
                 , [Geography].[Geography].[City].&[Seattle]&[WA]  
                 }  
                )    
```  
  
 Then issue the following MDX query to see calculated members allowed in subselects.  
  
```  
Select [Date].[Calendar Year].members on 0,  
       [Geography].[Geography].allmembers on 1  
from (Select {[Geography].[Geography].[State-Province].&[WA]&[US].[Seattle Metro Agg]} on 0 from [Adventure Works])  
Where [Measures].[Reseller Sales Amount]  
```  
  
 The obtained results are:  
  
|||||||  
|-|-|-|-|-|-|  
||All Periods|CY 2011|CY 2012|CY 2013|CY 2014|  
|Seattle Metro Agg|$2,383,545.69|1$291,248.93|$763,557.02|$915,832.36|$412,907.37|  
  
 As said before, the ascendants of [Seattle Metro] do not exist in the returned subspace, when SubQueries=1, hence [Geography].[Geography].allmembers only contains the calculated member.  
  
 If the example is run using SubQueries=2, in the connection string, the obtained results are:  
  
|||||||  
|-|-|-|-|-|-|  
||All Periods|CY 2001|CY 2002|CY 2003|CY 2004|  
|All Geographies|(null)|(null)|(null)|(null)|(null)|  
|United States|(null)|(null)|(null)|(null)|(null)|  
|Washington|(null)|(null)|(null)|(null)|(null)|  
|Seattle Metro Agg|$2,383,545.69|$291,248.93|$763,557.02|$915,832.36|$412,907.37|  
  
 As said before, when using SubQueries=2, the ascendants of [Seattle Metro] exist in the returned subspace but no values exist for those members because there is no regular members to provide for the aggregations. Therefore, NULL values are provided for all ascendant members of the calculated member in this example.  
  
 To understand the above behavior, it helps to understand that calculated members do not contribute to the aggregations of their parents as regular members do; the former implies that filtering by calculated members alone will lead to empty ascendants because there are no regular members to contribute to the aggregated values of the resulting subspace. If you add regular members to the filtering expression then the aggregated values will come from those regular members. Continuing with the above example, if the cities of Portland, in Oregon, and the city of Spokane, in Washington, are added to the same axis where the calculated member appears; as shown in the next MDX expression:  
  
```  
Select [Date].[Calendar Year].members on 0,  
       [Geography].[Geography].allmembers on 1  
from (Select {  
               [Seattle Metro Agg]  
             , [Geography].[Geography].[City].&[Portland]&[OR]  
             , [Geography].[Geography].[City].&[Spokane]&[WA]  
             } on 0 from [Adventure Works]  
     )  
Where [Measures].[Reseller Sales Amount]  
```  
  
 The following results are obtained.  
  
|||||||  
|-|-|-|-|-|-|  
||All Periods|CY 2001|CY 2002|CY 2003|CY 2004|  
|All Geographies|$235,171.62|$419.46|$4,996.25|$131,788.82|$97,967.09|  
|United States|$235,171.62|$419.46|$4,996.25|$131,788.82|$97,967.09|  
|Oregon|$30,968.25|$419.46|$4,996.25|$17,442.97|$8,109.56|  
|Portland|$30,968.25|$419.46|$4,996.25|$17,442.97|$8,109.56|  
|97205|$30,968.25|$419.46|$4,996.25|$17,442.97|$8,109.56|  
|Washington|$204,203.37|(null)|(null)|$114,345.85|$89,857.52|  
|Spokane|$204,203.37|(null)|(null)|$114,345.85|$89,857.52|  
|99202|$204,203.37|(null)|(null)|$114,345.85|$89,857.52|  
|Seattle Metro Agg|$2,383,545.69|$291,248.93|$763,557.02|$915,832.36|$412,907.37|  
  
 In the above results the aggregated values for [All Geographies], [United States], [Oregon] and [Washington] come from aggregating over the descendants of &[Portland]&[OR] and &[Spokane]&[WA]. Nothing comes from the calculated member.  
  
### Remarks  
 Only global or session calculated members are allowed in the subselect or subcube expressions. Having query calculated members in the MDX expression will raise an error when the subselect or subcube expression is evaluated.  
  
## See Also  
 <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A>   
 [Subselects in Queries](../../../analysis-services/multidimensional-models/mdx/subselects-in-queries.md)   
 [Supported XMLA Properties &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties)  
  
  
