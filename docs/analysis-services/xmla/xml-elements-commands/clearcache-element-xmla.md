---
title: "ClearCache Element (XMLA) | Microsoft Docs"
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
# ClearCache Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Clears the memory cache for the specified object on a Analysis Services instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <ClearCache>  
      <Object>...</Object>  
   </ClearCache>  
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
|Child elements|[Object](../../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md)|  
  
## Remarks  
 The **ClearCache** command flushes the cache for a specified database, dimension, cube, measure group, or partition on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. If an object other than a database, dimension, cube, measure group, or partition is specified in the **Object** element, an error occurs.  
  
## See also
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
