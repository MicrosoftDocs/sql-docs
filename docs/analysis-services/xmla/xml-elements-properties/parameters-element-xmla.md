---
title: "Parameters Element (XMLA) | Microsoft Docs"
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
# Parameters Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains a collection of [Parameter](../../../analysis-services/xmla/xml-elements-properties/parameter-element-xmla.md) elements used by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
 **Namespace:** `urn:schemas-microsoft-com:xml-analysis`  
  
## Syntax  
  
```  
  
<Execute>  
...  
   <Parameters>  
      <Parameter>...</Parameter>  
   </Parameters>  
...  
</Execute>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md)|  
|Child elements|[Parameter](../../../analysis-services/xmla/xml-elements-properties/parameter-element-xmla.md)|  
  
## Remarks  
 Some XML for Analysis (XMLA) commands, such as the [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) command, can require additional information. The **Parameters** element provides a mechanism for providing additional information, including chunked information, for an XMLA command.  
  
 If the XMLA command does not use the **Parameters** element, the element can be omitted when calling the **Execute** method.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
