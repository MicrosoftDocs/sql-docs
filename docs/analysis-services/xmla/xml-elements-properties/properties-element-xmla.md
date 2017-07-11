---
title: "Properties Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Properties Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Properties"
  - "microsoft.xml.analysis.properties"
  - "urn:schemas-microsoft-com:xml-analysis#Properties"
  - "Properties"
helpviewer_keywords: 
  - "Properties element"
ms.assetid: 0b5468e5-bf23-4d22-862f-72e27a9fff2f
caps.latest.revision: 30
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Properties Element (XMLA)
  Contains XML for Analysis (XMAL) properties used by the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) and [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) methods.  
  
## Syntax  
  
```  
  
<Discover> <!-- or Execute -->  
...  
   <Properties>  
      <PropertyList>...</PropertyList>  
   </Properties>  
...  
</Discover>  
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
|Parent elements|[Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md), [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md)|  
|Child elements|[PropertyList](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-xmla.md)|  
  
## Remarks  
 The **Properties** element represents XMLA properties used to control aspects of the **Discover** and **Execute** methods, such as defining the information required to connect to the data source, specifying the return format of the result set, or specifying the locale in which the data should be formatted.  
  
 The available properties and their values can be obtained by using the DISCOVER_PROPERTIES request type with the **Discover** method.  
  
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
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  