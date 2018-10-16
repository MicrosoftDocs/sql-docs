---
title: "CellData Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CellData Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CellData"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CellData"
  - "urn:schemas-microsoft-com:xml-analysis#CellData"
  - "microsoft.xml.analysis.celldata"
helpviewer_keywords: 
  - "CellData element"
ms.assetid: 0ebfb5e1-a674-4b9b-bd8c-c529da105f61
author: minewiskan
ms.author: owend
manager: craigg
---
# CellData Element (XMLA)
  Contains a collection of Cell elements that represent the cell data contained by a [root](root-element-xmla.md) element that uses the [MDDataSet](../xml-data-types/mddataset-data-type-xmla.md) data type.  
  
## Syntax  
  
```xml  
  
<root xmlns="urn:schemas-microsoft-com:xml-analysis:mddataset">  
   ...  
   <CellData>  
      <Cell>...</Cell>  
   </CellData>  
</root>  
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
|Parent elements|[root](root-element-xmla.md)|  
|Child elements|[Cell](cell-element-mddataset-xmla.md)|  
  
## Remarks  
 In the parent root element, the `Axes` element is followed by the `CellData` element, a collection of `Cell` elements that contain the cell property values for each cell returned in the multidimensional dataset.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
