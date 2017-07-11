---
title: "LName Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "LName Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#LName"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#LName"
  - "microsoft.xml.analysis.lname"
helpviewer_keywords: 
  - "LName element"
ms.assetid: 2c8c2fa9-cb2d-44ea-b253-5e6ff61f1b66
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# LName Element (XMLA)
  Contains information about unique level names for the parent [HierarchyInfo](../../../analysis-services/xmla/xml-elements-properties/hierarchyinfo-element-xmla.md) or [Member](../../../analysis-services/xmla/xml-elements-properties/member-element-xmla.md) element.  
  
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
|Parent elements|[HierarchyInfo](../../../analysis-services/xmla/xml-elements-properties/hierarchyinfo-element-xmla.md), [Member](../../../analysis-services/xmla/xml-elements-properties/member-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For **HierarchyInfo** elements, this element contains the name of the property that provides the unique level names of the hierarchy. The value is equivalent to the LEVEL_UNIQUE_NAME property defined for axis rowsets in the OLE DB for OLAP specification.  
  
 For **Member** elements, this element contains the unique name of the level in the hierarchy that contains the member represented by the parent **Member** element.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  