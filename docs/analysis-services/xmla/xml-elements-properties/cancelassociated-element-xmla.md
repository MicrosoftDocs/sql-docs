---
title: "CancelAssociated Element (XMLA) | Microsoft Docs"
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
# CancelAssociated Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Indicates whether the parent [Cancel](../../../analysis-services/xmla/xml-elements-commands/cancel-element-xmla.md) element should cancel all associated commands.  
  
## Syntax  
  
```xml  
  
<Cancel>  
   ...  
   <ConnectionID>...</ConnectionID>  
   ...  
</Cancel>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cancel](../../../analysis-services/xmla/xml-elements-commands/cancel-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 If this element is specified and set to **True**, every corresponding connection, session, and command identified in the parent **Cancel** command is canceled.  
  
## See also
 [ConnectionID Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/connectionid-element-xmla.md)   
 [SessionID Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/sessionid-element-xmla.md)   
 [SPID Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/spid-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
