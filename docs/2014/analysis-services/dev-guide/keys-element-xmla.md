---
title: "Keys Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Keys Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Keys"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Keys"
  - "microsoft.xml.analysis.keys"
helpviewer_keywords: 
  - "Keys element"
ms.assetid: 67291791-0032-412a-9a4f-74f68533e83d
caps.latest.revision: 11
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Keys Element (XMLA)
  Contains a collection of [Key](../../../2014/analysis-services/dev-guide/key-element-xmla.md) elements used to identify the member keys of the attribute member represented by the parent [Attribute](../../../2014/analysis-services/dev-guide/attribute-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute>  
   ...  
   <Keys>  
      <Key>...</Key>  
   </Keys>  
   ...  
</Attribute>  
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
|Parent elements|[Attribute](../../../2014/analysis-services/dev-guide/attribute-element-xmla.md)|  
|Child elements|[Key](../../../2014/analysis-services/dev-guide/key-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Drop Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/drop-element-xmla.md)   
 [Insert Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  