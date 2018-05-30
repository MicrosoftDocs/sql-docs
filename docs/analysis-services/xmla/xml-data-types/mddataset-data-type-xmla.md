---
title: "MDDataSet Data Type (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDDataSet Data Type (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Defines a derived data type that represents multidimensional data returned by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
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
  
## Data type characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Resultset](../../../analysis-services/xmla/xml-data-types/resultset-data-type-xmla.md)|  
|Derived data types|None|  
  
## Data type relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Axes](../../../analysis-services/xmla/xml-elements-properties/axes-element-xmla.md), [CellData](../../../analysis-services/xmla/xml-elements-properties/celldata-element-xmla.md), [OlapInfo](../../../analysis-services/xmla/xml-elements-properties/olapinfo-element-xmla.md)|  
|Derived elements|None|  
  
## Remarks  
 The **MDDataSet** data type provides the OLAP-oriented rowset (or dataset) required to represent OLAP data in XML. The contents of this rowset can vary depending on the values of the **Content** and **Format** properties provided in the [Properties](../../../analysis-services/xmla/xml-elements-properties/properties-element-xmla.md) collection of the **Execute** method. For more information about the **Content** and **Format** properties, see [Supported XMLA Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties.md).  
  
 For basic information about the OLE DB for OLAP dataset structures, refer to "MDDataSet Data Type Mapping to OLE DB" in the XML for Analysis 1.1 specification. For a full XML Schema definition language (XSD) sample of the **MDDataSet** data type, refer to "Appendix D: MDDataSet Example" of the XML for Analysis 1.1 specification.  
  
## See also
 [XML Data Types &#40;XMLA&#41;](../../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)  
  
  
