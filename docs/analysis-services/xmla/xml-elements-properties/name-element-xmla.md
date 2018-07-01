---
title: "Name Element (XMLA) | Microsoft Docs"
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
# Name Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains the name of an attribute member for the parent [Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md) or [Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Attribute> <!-- or Translation -->  
   ...  
   <Name>...</Name>  
   ...  
</Attribute>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|See the table below.|  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md)|1-1: Required element that occurs once and only once.|  
|[Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md)|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md), [Translation](../../../analysis-services/xmla/xml-elements-properties/translation-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For **Attribute** elements, the **Name** element contains the name of the attribute member to be inserted or updated during, respectively, the **Insert** or **Update** command.  
  
 For **Translation** elements, the **Name** element contains the caption of the attribute member, in the language specified by the **Language** element of the parent **Translation** object. If the **Name** element is not specified or contains an empty string, the value of the **Name** element for the **Attribute** element that contains the **Translation** element is used.  
  
## See also
 [Insert Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md)   
 [Language Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/language-element-xmla.md)   
 [Update Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
