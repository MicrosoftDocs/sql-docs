---
title: "MDDataSet Data Type (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MDDataSet Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#MDDataSet"
  - "MDDataSet"
  - "urn:schemas-microsoft-com:xml-analysis#MDDataSet"
helpviewer_keywords: 
  - "MDDataSet data type"
ms.assetid: 1a7e0092-f9f0-4ae5-ba27-ad1d8ebe8cb9
author: minewiskan
ms.author: owend
manager: craigg
---
# MDDataSet Data Type (XMLA)
  Defines a derived data type that represents multidimensional data returned by the [Execute](../xml-elements-methods-execute.md) method.  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis:mddataset  
  
## Syntax  
  
```xml  
  
<root xmlns="urn:schemas-microsoft-com:xml-analysis:rowset">  
   <!-- The following elements extend Resultset -->  
   <!-- Optional schema elements -->  
   <OlapInfo>...</OlapInfo>  
   <Axes>...</Axes>  
   <CellData>...</CellData>  
</root>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Resultset](resultset-data-type-xmla.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Axes](../xml-elements-properties/axes-element-xmla.md), [CellData](../xml-elements-properties/celldata-element-xmla.md), [OlapInfo](../xml-elements-properties/olapinfo-element-xmla.md)|  
|Derived elements|None|  
  
## Remarks  
 The `MDDataSet` data type provides the OLAP-oriented rowset (or dataset) required to represent OLAP data in XML. The contents of this rowset can vary depending on the values of the `Content` and `Format` properties provided in the [Properties](../xml-elements-properties/properties-element-xmla.md) collection of the `Execute` method. For more information about the `Content` and `Format` properties, see [Supported XMLA Properties &#40;XMLA&#41;](../xml-elements-properties/propertylist-element-supported-xmla-properties.md).  
  
 For basic information about the OLE DB for OLAP dataset structures, refer to "MDDataSet Data Type Mapping to OLE DB" in the XML for Analysis 1.1 specification. For a full XML Schema definition language (XSD) sample of the `MDDataSet` data type, refer to "Appendix D: MDDataSet Example" of the XML for Analysis 1.1 specification.  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](xml-data-types-xmla.md)  
  
  
