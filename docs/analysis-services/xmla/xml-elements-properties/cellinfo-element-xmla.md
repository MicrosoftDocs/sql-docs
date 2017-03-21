---
title: "CellInfo Element (XMLA) | Microsoft Docs"
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
  - "CellInfo Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.cellinfo"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CellInfo"
  - "urn:schemas-microsoft-com:xml-analysis#CellInfo"
helpviewer_keywords: 
  - "CellInfo element"
ms.assetid: 8b6420f1-e9a7-4975-b580-1439fa11f5ca
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# CellInfo Element (XMLA)
  Represents the cell metadata contained by the parent [OlapInfo](../../../analysis-services/xmla/xml-elements-properties/olapinfo-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<OlapInfo>  
   ...  
   <CellInfo>  
      <!-- One or more cell property definitions -->  
   </CellInfo>  
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
|Parent elements|[OlapInfo](../../../analysis-services/xmla/xml-elements-properties/olapinfo-element-xmla.md)|  
|Child elements|One or more cell property definitions|  
  
## Remarks  
 The **CellInfo** element contains a collection of cell properties for the cells included within the multidimensional dataset returned by a **root** element using the **MDDataSet** data type. Each cell property in the **CellInfo** element is defined by a separate XML element, each with a **name** attribute and a **type** attribute. The **name** attribute of the cell property corresponds to the name of the OLE DB for the OLAP cell property represented by the XML element, and the **type** attribute represents the XML data type of the cell property. The name of the XML element is used to identify the value of the cell property for cells contained in the **CellData** element of the **root** element.  
  
 The following syntax describes a cell property definition:  
  
```  
<CellPropertyDefinition name="string" type"string" />  
```  
  
 The available properties and their values can be obtained by using the DISCOVER_PROPERTIES request type with the **Discover** method. There is no required order for the properties listed in the **PropertyList** element.  
  
 A provider can optionally specify default values for individual member or cell properties in the **AxisInfo** or **CellInfo** section. Default values can provide a smaller result if the property always or almost always has the same value. To indicate a default value for a property, the**Default** element can optionally be specified as a child element of one of the cell property definition elements. Therefore, the absence of a member or cell property in the result indicates that the stated default is the value for the cell property.  
  
## Example  
 The following example demonstrates how the VALUE, FORMATTED_VALUE, and FORMAT_STRING cell properties are represented in the **CellInfo** element.  
  
```  
<OlapInfo>  
   ...  
      <CellInfo>  
         <Value name="VALUE"></Value>  
         <FmtValue name="FORMATTED_VALUE"></FmtValue>  
         <FormatString name="FORMAT_STRING"></FormatString>  
      </CellInfo>  
</OlapInfo>  
```  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  