---
title: "ErrorCode Element (XMLA) | Microsoft Docs"
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
# ErrorCode Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains the numeric return code of the parent [Error](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Error>  
   ...  
   <ErrorCode>...</ErrorCode>  
   ...  
</Error>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|UnsignedInt|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Error](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
