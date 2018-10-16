---
title: "OlapInfo Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "OlapInfo Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#OlapInfo"
  - "microsoft.xml.analysis.olapinfo"
  - "urn:schemas-microsoft-com:xml-analysis#OlapInfo"
  - "OLAPInfo"
helpviewer_keywords: 
  - "OlapInfo element"
ms.assetid: 8828fdd7-c0f7-48ce-a0d0-ab4bc1a995cf
author: minewiskan
ms.author: owend
manager: craigg
---
# OlapInfo Element (XMLA)
  Contains the axis and cell metadata contained by a [root](root-element-xmla.md) element that uses the [MDDataSet](../xml-data-types/mddataset-data-type-xmla.md) data type.  
  
## Syntax  
  
```xml  
  
<root xmlns="urn:schemas-microsoft-com:xml-analysis:mddataset">  
   ...  
   <OlapInfo>  
      <CubeInfo>...</CubeInfo>  
      <AxesInfo>...</AxesInfo>  
      <CellInfo>...</CellInfo>  
   </OlapInfo>  
   ...  
</root>  
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
|Parent elements|[root](root-element-xmla.md)|  
|Child elements|[AxesInfo](axesinfo-element-xmla.md), [CellInfo](cellinfo-element-xmla.md), [CubeInfo](cubeinfo-element-xmla.md)|  
  
## Remarks  
 The `OLAPInfo` section of a `root` element using the `MDDataSet` data type provides metadata about the cube, the axes of the multidimensional result, and the properties of the cells included the result.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
