---
title: "AggregateFunction Element (ASSL) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# AggregateFunction Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Defines the type of aggregate function used by a [Measure](../../../analysis-services/scripting/objects/measure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Measure>  
   ...  
   <AggregateFunction>...</AggregateFunction>  
   ...  
</Measure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Sum*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Measure](../../../analysis-services/scripting/objects/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Sum*|The measure is aggregated using the **Sum** function.|  
|*Count*|The measure is aggregated using the **Count** function.|  
|*Min*|The measure is aggregated using the **Min** function.|  
|*Max*|The measure is aggregated using the **Max** function.|  
|*DistinctCount*|The measure is aggregated using the **DistinctCount** function.|  
|*None*|The measure is not aggregated.|  
|*ByAccount*|The measure is aggregated by account.|  
|*AverageOfChildren*|The measure is aggregated by returning the average of its children.|  
|*FirstChild*|The measure is aggregated by returning its first child member.|  
|*LastChild*|The measure is aggregated by returning its last child member.|  
|*FirstNonEmpty*|The measure is aggregated by returning its first nonempty member.|  
|*LastNonEmpty*|The measure is aggregated by returning its last nonempty member.|  
  
 The enumeration that corresponds to the allowed values for **AggregateFunction** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationFunction>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
