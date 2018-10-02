---
title: "Caption Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Caption Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Caption"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Caption"
  - "microsoft.xml.analysis.caption"
helpviewer_keywords: 
  - "Caption element"
ms.assetid: 3d10ee68-98ab-4da0-a409-800dea2f1c32
author: minewiskan
ms.author: owend
manager: craigg
---
# Caption Element (XMLA)
  Contains information about the caption of the parent [HierarchyInfo](hierarchyinfo-element-xmla.md) or [Member](member-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<HierarchyInfo> <!-- or Member -->  
   ...  
   <Caption>...</Caption>  
   ...  
</HierarchyInfo>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[HierarchyInfo](hierarchyinfo-element-xmla.md), [Member](member-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For `HierarchyInfo` elements, the `Caption` element contains the name of the property that provides the member captions of the hierarchy. The value is equivalent to the MEMBER_CAPTION property defined for axis rowsets in the OLE DB for OLAP specification.  
  
 For `Member` elements, the `Caption` element contains the caption of the parent `Member` element in the language specified for the XML for Analysis (XMLA) session. If no caption is available, this element contains the unique name of the member.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
