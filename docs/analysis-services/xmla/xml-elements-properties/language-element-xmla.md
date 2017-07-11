---
title: "Language Element (XMLA) | Microsoft Docs"
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
  - "Language Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Language"
  - "microsoft.xml.analysis.language"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Language"
helpviewer_keywords: 
  - "Language element"
ms.assetid: cd998202-e43f-4c6c-8727-a15a76a520ea
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Language Element (XMLA)
  Contains the locale identifier (LCID) for the parent [Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md) element.  
  
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
|Parent elements|[Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Language** element specifies the LCID used by the parent **Translation** element to assign the **Name** element of the parent **Translation** element to an attribute member, for the specified language, during an **Insert** or **Update** command.  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md)   
 [Name Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/name-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  