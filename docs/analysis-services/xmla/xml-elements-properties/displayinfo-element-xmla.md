---
title: "DisplayInfo Element (XMLA) | Microsoft Docs"
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
# DisplayInfo Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains display information for the parent [HierarchyInfo](../../../analysis-services/xmla/xml-elements-properties/hierarchyinfo-element-xmla.md) or [Member](../../../analysis-services/xmla/xml-elements-properties/member-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<HierarchyInfo> <!-- or Member -->  
   ...  
   <DisplayInfo>...</DisplayInfo>  
   ...  
</HierarchyInfo>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|unsignedInt|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[HierarchyInfo](../../../analysis-services/xmla/xml-elements-properties/hierarchyinfo-element-xmla.md), [Member](../../../analysis-services/xmla/xml-elements-properties/member-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **DisplayInfo** element contains various items of information that help a client application render the parent **HierarchyInfo** or **Member** element. The value is equivalent to the DISPLAY_INFO property defined for axis rowsets in the OLE DB for OLAP specification.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
