---
title: "Properties Element (XMLA) | Microsoft Docs"
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
# Properties Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
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
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
