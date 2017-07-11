---
title: "Query Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Query Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Query"
  - "microsoft.xml.analysis.query"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Query"
helpviewer_keywords: 
  - "Query element"
ms.assetid: 5a4544e4-012f-4a47-942c-23596400ea16
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Query Element (XMLA)
  Contains a query within the [Queries](../../../analysis-services/xmla/xml-elements-properties/queries-element-xmla.md) collection used by the [DesignAggregations](../../../analysis-services/xmla/xml-elements-commands/designaggregations-element-xmla.md) command during usage-based optimization.  
  
## Syntax  
  
```xml  
  
<Queries>  
   ...  
   <Query>...</Query>  
   ...  
</Queries>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Queries](../../../analysis-services/xmla/xml-elements-properties/queries-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **DesignAggregations** command supports usage-based optimization by including one or more **Query** elements in the **Queries** collection of the command. Each **Query** element represents a goal query that the design process uses to define aggregations that target the most frequently used queries. You can either specify your own goal queries, or you can use the information stored by an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] in the query log to retrieve information about the most frequently used queries.  
  
 If you are iteratively designing aggregations, you only have to pass goal queries in the first **DesignAggregations** command because the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance stores these goal queries and uses these queries during subsequent **DesignAggregations** commands. After you pass goal queries in the first **DesignAggregations** command of an iterative process, any subsequent **DesignAggregations** command that contains goal queries in the **Queries** property generates an error.  
  
 The **Query** element contains a comma-delimited value that contains the following arguments:  
  
 *Frequency*,*Dataset*[,*Dataset*...]  
  
 *Frequency*  
 A weighting factor that corresponds to the number of times that the query has previously been executed. If the **Query** element represents a new query, the *Frequency* value represents the weighting factor used by the design process to evaluate the query. As the frequency value becomes larger, the weight that is put on the query during the design process increases.  
  
 *Dataset*  
 A numeric string that specifies which attributes from a dimension are to be included in the query. This string must have the same number of characters as the number of attributes in the dimension. Zero (0) indicates that the attribute in the specified ordinal position is not included in the query for the specified dimension, while one (1) indicates that the attribute in the specified ordinal position is included in the query for the specified dimension.  
  
 For example, the string "011" would refer to a query involving a dimension with three attributes, from which the second and third attributes are included in the query.  
  
> [!NOTE]  
>  Some attributes are excluded from consideration in the dataset. For more information about excluded attributes, see [Properties (XMLA)](../../../analysis-services/xmla/xml-elements-properties/query-element-xmla.md).  
  
 Each dimension in the measure group that contains the aggregation design is represented by a *Dataset* value in the **Query** element. The order of *Dataset* values must match the order of dimensions included in the measure group.  
  
## See Also  
 [Designing Aggregations &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/designing-aggregations-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  