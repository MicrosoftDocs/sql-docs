---
title: "AxisInfo Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AxisInfo Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#AxisInfo"
  - "microsoft.xml.analysis.axisinfo"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#AxisInfo"
helpviewer_keywords: 
  - "AxisInfo element"
ms.assetid: 060741db-b2ec-4174-9277-58d996440a88
author: minewiskan
ms.author: owend
manager: craigg
---
# AxisInfo Element (XMLA)
  Represents the metadata of a single axis contained by the parent [AxesInfo](axesinfo-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<AxesInfo>  
   ...  
   <AxisInfo name="string">  
      <HierarchyInfo>...</HierarchyInfo>  
   </AxisInfo>  
   ...  
</AxesInfo>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AxesInfo](axesinfo-element-xmla.md)|  
|Child elements|[HierarchyInfo](hierarchyinfo-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|Name|Required `String` attribute. The name of the axis.|  
  
## Remarks  
 In a `root` element that uses the `MDDataSet` object, an `AxisInfo` element contains a collection of `HierarchyInfo` elements that, combined with the value of the `name` attribute, represents the definition of a single axis returned in the multidimensional dataset.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
