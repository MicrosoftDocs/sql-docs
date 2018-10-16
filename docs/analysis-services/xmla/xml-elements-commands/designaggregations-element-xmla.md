---
title: "DesignAggregations Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DesignAggregations Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Creates aggregations for an aggregation design on a Analysis Services instance.  
  
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Materialize](../../../analysis-services/xmla/xml-elements-properties/materialize-element-xmla.md), [Object](../../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md), [Optimization](../../../analysis-services/xmla/xml-elements-properties/optimization-element-xmla.md), [Queries](../../../analysis-services/xmla/xml-elements-properties/queries-element-xmla.md), [Steps](../../../analysis-services/xmla/xml-elements-properties/steps-element-xmla.md), [Storage](../../../analysis-services/xmla/xml-elements-properties/storage-element-xmla.md), [Time](../../../analysis-services/xmla/xml-elements-properties/time-element-xmla.md)|  
  
## Remarks  
 The **DesignAggregations** command is used to generate aggregation definitions stored by an aggregation design. An aggregation design can then be used to materialize aggregations for a partition and can be reused between partitions.  
  
## See also
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
