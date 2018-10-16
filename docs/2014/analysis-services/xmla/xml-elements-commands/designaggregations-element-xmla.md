---
title: "DesignAggregations Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DesignAggregations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#DesignAggregations"
  - "microsoft.xml.analysis.designaggregations"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DesignAggregations"
helpviewer_keywords: 
  - "DesignAggregations command"
ms.assetid: 4c419dbc-7405-40aa-8ddd-6b46685a297d
author: minewiskan
ms.author: owend
manager: craigg
---
# DesignAggregations Element (XMLA)
  Creates aggregations for an aggregation design on a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <DesignAggregations>  
      <Object>...</Object>  
      <Time>...</Time>  
      <Steps>...</Steps>  
      <Optimization>...</Optimization>  
      <Storage>...</Storage>  
      <Materialize>...</Materialize>  
      <Queries>...</Queries>  
   </DesignAggregations>  
</Command>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Materialize](../xml-elements-properties/materialize-element-xmla.md), [Object](../xml-elements-properties/object-element-xmla.md), [Optimization](../xml-elements-properties/optimization-element-xmla.md), [Queries](../xml-elements-properties/queries-element-xmla.md), [Steps](../xml-elements-properties/steps-element-xmla.md), [Storage](../xml-elements-properties/storage-element-xmla.md), [Time](../xml-elements-properties/time-element-xmla.md)|  
  
## Remarks  
 The `DesignAggregations` command is used to generate aggregation definitions stored by an aggregation design. An aggregation design can then be used to materialize aggregations for a partition and can be reused between partitions.  
  
## See Also  
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
