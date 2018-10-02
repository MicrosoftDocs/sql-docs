---
title: "PropertyList Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "PropertyList Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#PropertyList"
  - "microsoft.xml.analysis.propertylist"
  - "urn:schemas-microsoft-com:xml-analysis#PropertyList"
helpviewer_keywords: 
  - "PropertyList element"
ms.assetid: 58e63bd9-8aac-438d-adba-1868b4d123f5
author: minewiskan
ms.author: owend
manager: craigg
---
# PropertyList Element (XMLA)
  Contains a collection of XML for Analysis (XMLA) properties used by the [Discover](../xml-elements-methods-discover.md) and [Execute](../xml-elements-methods-execute.md) methods.  
  
## Syntax  
  
```  
  
<Properties>  
   <PropertyList>  
      <!-- Zero or more XMLA properties -->  
   </PropertyList>  
</Properties>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Properties](properties-element-xmla.md)|  
|Child elements|XMLA properties (see Remarks)|  
  
## Remarks  
 The `PropertyList` element contains a collection of XMLA properties. Each property allows the user to control some aspect of the `Discover` or `Execute` method, such as defining the information required to connect to the data source, specifying the return format of the result set, or specifying the locale in which the data should be formatted. Each XMLA property in the `PropertyList` element is defined by a separate XML element. The value of the XMLA property is the data contained by the XML element, and the name of the XMLA property corresponds to the name of the XML element.  
  
 The available properties and their values can be obtained by using the DISCOVER_PROPERTIES request type with the `Discover` method. There is no required order for the properties listed in the `PropertyList` element.  
  
 For more information regarding the XMLA properties supported by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], see [Supported XMLA Properties &#40;XMLA&#41;](propertylist-element-supported-xmla-properties.md).  
  
## Example  
  
```  
<Properties>  
   <PropertyList>  
      <DataSourceInfo>  
         Provider=MSOLAP;Data Source=local;  
      </DataSourceInfo>  
      <Catalog>  
         Foodmart 2000  
      </Catalog>  
      <Format>  
         Multidimensional  
      </Format>  
   </PropertyList>  
</Properties>  
```  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
