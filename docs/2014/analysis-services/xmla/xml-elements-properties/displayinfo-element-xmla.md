---
title: "DisplayInfo Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DisplayInfo Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.displayinfo"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DisplayInfo"
  - "urn:schemas-microsoft-com:xml-analysis#DisplayInfo"
helpviewer_keywords: 
  - "DisplayInfo element"
ms.assetid: 059ce038-38de-4c7d-a72f-4f919cfc7c4a
author: minewiskan
ms.author: owend
manager: craigg
---
# DisplayInfo Element (XMLA)
  Contains display information for the parent [HierarchyInfo](hierarchyinfo-element-xmla.md) or [Member](member-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<HierarchyInfo> <!-- or Member -->  
   ...  
   <DisplayInfo>...</DisplayInfo>  
   ...  
</HierarchyInfo>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|unsignedInt|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[HierarchyInfo](hierarchyinfo-element-xmla.md), [Member](member-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `DisplayInfo` element contains various items of information that help a client application render the parent `HierarchyInfo` or `Member` element. The value is equivalent to the DISPLAY_INFO property defined for axis rowsets in the OLE DB for OLAP specification.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
