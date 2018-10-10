---
title: "Attributes Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Attributes Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Attributes"
  - "microsoft.xml.analysis.attributes"
  - "urn:schemas-microsoft-com:xml-analysis#Attributes"
helpviewer_keywords: 
  - "Attributes element"
ms.assetid: c0393de8-44e8-46de-af78-1fd66c218521
author: minewiskan
ms.author: owend
manager: craigg
---
# Attributes Element (XMLA)
  Contains a collection of [Attribute](attribute-element-xmla.md) elements used by the parent [Insert](../xml-elements-commands/insert-element-xmla.md) or [Update](../xml-elements-commands/update-element-xmla.md) command, or by the parent [Where](where-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Insert > <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Attributes>  
      <Attribute>...</Attribute>  
   </Attributes>  
   ...  
</Insert>  
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
|Parent elements|[Insert](../xml-elements-commands/insert-element-xmla.md), [Update](../xml-elements-commands/update-element-xmla.md), [Where](where-element-xmla.md)|  
|Child elements|[Attribute](attribute-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
