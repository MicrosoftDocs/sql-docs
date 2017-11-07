---
title: "DesignAggregations Element (XMLA) | Microsoft Docs"
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
  - "DesignAggregations Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#DesignAggregations"
  - "microsoft.xml.analysis.designaggregations"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DesignAggregations"
helpviewer_keywords: 
  - "DesignAggregations command"
ms.assetid: 4c419dbc-7405-40aa-8ddd-6b46685a297d
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
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
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Materialize](../../../analysis-services/xmla/xml-elements-properties/materialize-element-xmla.md), [Object](../../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md), [Optimization](../../../analysis-services/xmla/xml-elements-properties/optimization-element-xmla.md), [Queries](../../../analysis-services/xmla/xml-elements-properties/queries-element-xmla.md), [Steps](../../../analysis-services/xmla/xml-elements-properties/steps-element-xmla.md), [Storage](../../../analysis-services/xmla/xml-elements-properties/storage-element-xmla.md), [Time](../../../analysis-services/xmla/xml-elements-properties/time-element-xmla.md)|  
  
## Remarks  
 The **DesignAggregations** command is used to generate aggregation definitions stored by an aggregation design. An aggregation design can then be used to materialize aggregations for a partition and can be reused between partitions.  
  
## See Also  
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  