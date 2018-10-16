---
title: "AxesInfo Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AxesInfo Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#AxesInfo"
  - "microsoft.xml.analysis.axesinfo"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#AxesInfo"
helpviewer_keywords: 
  - "AxesInfo element"
ms.assetid: 15cfa67d-5acd-4737-8a81-2df34b334d3f
author: minewiskan
ms.author: owend
manager: craigg
---
# AxesInfo Element (XMLA)
  Contains a collection of [AxisInfo](axisinfo-element-xmla.md) elements, representing the axis metadata contained by the parent [OlapInfo](olapinfo-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<OlapInfo>  
   ...  
   <AxesInfo>  
      <AxisInfo>...</AxisInfo>  
   </AxesInfo>  
   ...  
</OlapInfo>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[OlapInfo](olapinfo-element-xmla.md)|  
|Child elements|[AxisInfo](axisinfo-element-xmla.md)|  
  
## Remarks  
 The `AxesInfo` element contains one `AxisInfo` element for each axis within the multidimensional dataset returned by a `root` element using the `MDDataSet` data type.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
