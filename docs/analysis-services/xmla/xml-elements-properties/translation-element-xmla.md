---
title: "Translation Element (XMLA) | Microsoft Docs"
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
# Translation Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Defines a translation for an attribute member.  
  
## Syntax  
  
```xml  
  
<Translations>  
   ...  
   <Translation>  
      <Language>...</Language>  
      <Name>...</Name>  
   </Translation>  
   ...  
</Translations>  
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
|Parent elements|[Translations](../../../analysis-services/xmla/xml-elements-properties/translations-element-xmla.md)|  
|Child elements|[Language](../../../analysis-services/xmla/xml-elements-properties/language-element-xmla.md), [Name](../../../analysis-services/xmla/xml-elements-properties/name-element-xmla.md)|  
  
## Remarks  
 A **Translation** element defines the information needed to associate an attribute member to a translation defined for a given attribute during an [Insert](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md) or [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md) command.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
