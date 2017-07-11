---
title: "Name Element (XMLA) | Microsoft Docs"
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
  - "Name Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Name"
  - "urn:schemas-microsoft-com:xml-analysis#Name"
  - "microsoft.xml.analysis.name"
helpviewer_keywords: 
  - "Name element"
ms.assetid: cc1a93df-0b1b-4c38-9183-4f11c26fea6a
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Name Element (XMLA)
  Contains the name of an attribute member for the parent [Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md) or [Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute> <!-- or Translation -->  
   ...  
   <Name>...</Name>  
   ...  
</Attribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|See the table below.|  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md)|1-1: Required element that occurs once and only once.|  
|[Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md)|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md), [Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For **Attribute** elements, the **Name** element contains the name of the attribute member to be inserted or updated during, respectively, the **Insert** or **Update** command.  
  
 For **Translation** elements, the **Name** element contains the caption of the attribute member, in the language specified by the **Language** element of the parent **Translation** object. If the **Name** element is not specified or contains an empty string, the value of the **Name** element for the **Attribute** element that contains the **Translation** element is used.  
  
## See Also  
 [Insert Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md)   
 [Language Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/language-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  