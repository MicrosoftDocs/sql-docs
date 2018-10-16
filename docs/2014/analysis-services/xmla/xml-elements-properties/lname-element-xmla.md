---
title: "LName Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "LName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#LName"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#LName"
  - "microsoft.xml.analysis.lname"
helpviewer_keywords: 
  - "LName element"
ms.assetid: 2c8c2fa9-cb2d-44ea-b253-5e6ff61f1b66
author: minewiskan
ms.author: owend
manager: craigg
---
# LName Element (XMLA)
  Contains information about unique level names for the parent [HierarchyInfo](hierarchyinfo-element-xmla.md) or [Member](member-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<HierarchyInfo> <!-- or Member -->  
   ...  
   <LName>...</LName>  
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
 For `HierarchyInfo` elements, this element contains the name of the property that provides the unique level names of the hierarchy. The value is equivalent to the LEVEL_UNIQUE_NAME property defined for axis rowsets in the OLE DB for OLAP specification.  
  
 For `Member` elements, this element contains the unique name of the level in the hierarchy that contains the member represented by the parent `Member` element.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
