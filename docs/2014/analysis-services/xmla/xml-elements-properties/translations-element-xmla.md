---
title: "Translations Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Translations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.translations"
  - "urn:schemas-microsoft-com:xml-analysis#Translations"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Translations"
helpviewer_keywords: 
  - "Translations element"
ms.assetid: 86fd2119-9bea-4306-829e-cc439da05566
author: minewiskan
ms.author: owend
manager: craigg
---
# Translations Element (XMLA)
  Contains a collection of [Translation](translation-element-xmla.md) elements used to identify the member keys of the attribute member represented by the parent [Attribute](attribute-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute>  
   ...  
   <Translations>  
      <Translation>...</Translation>  
   </Translations>  
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
|Parent elements|[Attribute](attribute-element-xmla.md)|  
|Child elements|[Translation](translation-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
