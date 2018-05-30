---
title: "CubeInfo Element (XMLA) | Microsoft Docs"
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
# CubeInfo Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains the cube metadata contained by the parent [OlapInfo](../../../analysis-services/xmla/xml-elements-properties/olapinfo-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<OlapInfo>  
   ...  
   <CubeInfo>  
      <Cube>...</Cube>  
   </CubeInfo>  
   ...  
</OlapInfo>  
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
|Parent elements|[OlapInfo](../../../analysis-services/xmla/xml-elements-properties/olapinfo-element-xmla.md)|  
|Child elements|[Cube](../../../analysis-services/xmla/xml-elements-properties/cube-element-olapinfo-xmla.md)|  
  
## Remarks  
 The **CubeInfo** element contains one **Cube** element for each cube referenced in the multidimensional dataset.  
  
> [!NOTE]  
>  Analysis Services returns only a single **Cube** element in this collection because [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] does not support statements that reference multiple cubes in the FROM clause of the Multidimensional Expressions (MDX) language.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
