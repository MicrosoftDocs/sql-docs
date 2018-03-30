---
title: "HierarchyInfo Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "HierarchyInfo Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#HierarchyInfo"
  - "microsoft.xml.analysis.hierarchyinfo"
  - "urn:schemas-microsoft-com:xml-analysis#HierarchyInfo"
helpviewer_keywords: 
  - "HierarchyInfo element"
ms.assetid: b4472251-1f1d-4233-a8e6-407397862ab4
caps.latest.revision: 11
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# HierarchyInfo Element (XMLA)
  Represents a single hierarchy contained by a parent [AxisInfo](../../../2014/analysis-services/dev-guide/axisinfo-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<AxisInfo>  
   ...  
   <HierarchyInfo name="string">  
      <UName>...</UName>  
      <Caption>...</Caption>  
      <LName>...</LName>  
      <LNum>...</LNum>  
      <DisplayInfo>...</DisplayInfo>  
   </HierarchyInfo>  
   ...  
</AxisInfo>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AxisInfo](../../../2014/analysis-services/dev-guide/axisinfo-element-xmla.md)|  
|Child elements|[Caption](../../../2014/analysis-services/dev-guide/caption-element-xmla.md), [DisplayInfo](../../../2014/analysis-services/dev-guide/displayinfo-element-xmla.md), [LName](../../../2014/analysis-services/dev-guide/lname-element-xmla.md), [LNum](../../../2014/analysis-services/dev-guide/lnum-element-xmla.md), [UName](../../../2014/analysis-services/dev-guide/uname-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|Name|Required `String` attribute. The name of the hierarchy.|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  