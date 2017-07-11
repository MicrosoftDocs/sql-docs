---
title: "Translations Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Translations Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.translations"
  - "urn:schemas-microsoft-com:xml-analysis#Translations"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Translations"
helpviewer_keywords: 
  - "Translations element"
ms.assetid: 86fd2119-9bea-4306-829e-cc439da05566
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Translations Element (XMLA)
  Contains a collection of [Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md) elements used to identify the member keys of the attribute member represented by the parent [Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md) element.  
  
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
|Parent elements|[Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md)|  
|Child elements|[Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  