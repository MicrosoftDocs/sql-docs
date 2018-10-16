---
title: "UName Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "UName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#UName"
  - "urn:schemas-microsoft-com:xml-analysis#UName"
  - "microsoft.xml.analysis.uname"
helpviewer_keywords: 
  - "UName element"
ms.assetid: b4916d44-cf77-4d4c-b4e5-a0a98192d057
author: minewiskan
ms.author: owend
manager: craigg
---
# UName Element (XMLA)
  Contains the unique name of the parent [HierarchyInfo](hierarchyinfo-element-xmla.md) or [Member](member-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<HierarchyInfo> <!-- or Member -->  
   ...  
   <UName>...</UName>  
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
 For `HierarchyInfo` elements, the `UName` element contains the name of the property that provides the unique member names of the hierarchy. The value is equivalent to the MEMBER_UNIQUE_NAME property defined for axis rowsets in the OLE DB for OLAP specification.  
  
 For `Member` elements, the `UName` element contains the unique name of the parent `Member` element.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
