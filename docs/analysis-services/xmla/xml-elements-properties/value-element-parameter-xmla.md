---
title: "Value Element (Parameter) (XMLA) | Microsoft Docs"
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
# Value Element (Parameter) (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains the value of a parameter represented by the [Parameter](../../../analysis-services/xmla/xml-elements-properties/parameter-element-xmla.md) element.  
  
## Syntax  
  
```  
  
<Parameter>  
   ...  
   <Value>...</Value>  
   ...  
</Parameter>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Any|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Parameter](../../../analysis-services/xmla/xml-elements-properties/parameter-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Value** element can store any simple XML type, as well as the XML for Analysis (XMLA) **Rowset** data type, for parameters used by XMLA commands in the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
