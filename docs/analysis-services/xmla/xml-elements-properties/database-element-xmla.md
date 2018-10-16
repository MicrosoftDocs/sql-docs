---
title: "Database Element (XMLA) | Microsoft Docs"
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
# Database Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Identifies the database that contains the dimension represented by the parent [Object](../../../analysis-services/xmla/xml-elements-properties/object-element-dimension-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Object>  
   ...  
   <Database>...</Database>  
   ...  
</Object>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Object](../../../analysis-services/xmla/xml-elements-properties/object-element-dimension-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Database** element is an object identifier that contains the name of the Analysis Services database that contains the dimension represented by the **Object** element.  
  
## See also
 [Cube Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/cube-element-xmla.md)   
 [Dimension Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/dimension-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
