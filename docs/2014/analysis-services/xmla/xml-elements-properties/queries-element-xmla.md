---
title: "Queries Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Queries Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.queries"
  - "urn:schemas-microsoft-com:xml-analysis#Queries"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Queries"
helpviewer_keywords: 
  - "Queries element"
ms.assetid: e199412a-23f1-4d11-9e72-11f184ad9602
author: minewiskan
ms.author: owend
manager: craigg
---
# Queries Element (XMLA)
  Contains a collection of [Query](query-element-xmla.md) elements used by the [DesignAggregations](../xml-elements-commands/designaggregations-element-xmla.md) command during usage-based optimization.  
  
## Syntax  
  
```xml  
  
<DesignAggregations>  
   ...  
   <Queries>  
      <Query>...</Query>  
   </Queries>  
   ...  
</DesignAggregations>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DesignAggregations](../xml-elements-commands/designaggregations-element-xmla.md)|  
|Child elements|[Query](query-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
