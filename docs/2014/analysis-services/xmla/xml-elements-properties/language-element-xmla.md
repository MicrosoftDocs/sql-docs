---
title: "Language Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Language Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Language"
  - "microsoft.xml.analysis.language"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Language"
helpviewer_keywords: 
  - "Language element"
ms.assetid: cd998202-e43f-4c6c-8727-a15a76a520ea
author: minewiskan
ms.author: owend
manager: craigg
---
# Language Element (XMLA)
  Contains the locale identifier (LCID) for the parent [Translation](translation-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Translation>  
   ...  
   <Language>...</Language>  
   ...  
</Translation>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Translation](translation-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `Language` element specifies the LCID used by the parent `Translation` element to assign the `Name` element of the parent `Translation` element to an attribute member, for the specified language, during an `Insert` or `Update` command.  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../xml-elements-commands/insert-element-xmla.md)   
 [Name Element &#40;XMLA&#41;](name-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
