---
title: "Parallel Element (XMLA) | Microsoft Docs"
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
# Parallel Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Specifies how many processing jobs can run in parallel using the parent [Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Batch>  
   ....  
   <Parallel maxParallel="Integer">  
      <!-- An XMLA process command -->  
   </Parallel>  
   ....  
</Batch>  
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
|Parent elements|[Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md)|  
|Child elements|[Process Element](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|maxParallel|Optional **Integer** attribute. Indicates the maximum number of threads on which to run commands in parallel. If not specified or set to 0, the instance of Analysis Services determines an optimal number of threads based on the number of processors available on the computer.|  
  
## Remarks  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
