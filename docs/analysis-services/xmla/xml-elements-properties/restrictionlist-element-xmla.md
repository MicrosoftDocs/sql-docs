---
title: "RestrictionList Element (XMLA) | Microsoft Docs"
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
# RestrictionList Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains a collection of restriction columns and values used by the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method.  
  
## Syntax  
  
```  
  
<Restrictions>  
   <RestrictionList>  
      <!-- Zero or more restriction columns and values -->  
   </RestrictionList>  
</Restrictions>  
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
|Parent elements|[Restrictions](../../../analysis-services/xmla/xml-elements-properties/restrictions-element-xmla.md)|  
|Child elements|Restriction columns and values (see Remarks).|  
  
## Remarks  
 The **RestrictionList** element contains a collection of restriction columns on which the data returned by the **Discover** method can be filtered. Each restriction column in the **RestrictionList** element is defined by a separate XML element. The value of the restriction column is the data contained by the XML element, and the name of the restriction column corresponds to the name of the XML element.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
