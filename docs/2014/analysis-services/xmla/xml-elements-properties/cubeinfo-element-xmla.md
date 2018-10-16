---
title: "CubeInfo Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CubeInfo Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CubeInfo"
  - "microsoft.xml.analysis.cubeinfo"
  - "urn:schemas-microsoft-com:xml-analysis#CubeInfo"
helpviewer_keywords: 
  - "CubeInfo element"
ms.assetid: a504bac5-4bf2-4f78-a288-e74a34eaa97e
author: minewiskan
ms.author: owend
manager: craigg
---
# CubeInfo Element (XMLA)
  Contains the cube metadata contained by the parent [OlapInfo](olapinfo-element-xmla.md) element.  
  
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
|Child elements|[Cube](cube-element-olapinfo-xmla.md)|  
  
## Remarks  
 The `CubeInfo` element contains one `Cube` element for each cube referenced in the multidimensional dataset.  
  
> [!NOTE]  
>  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] returns only a single `Cube` element in this collection because [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] does not support statements that reference multiple cubes in the FROM clause of the Multidimensional Expressions (MDX) language.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
