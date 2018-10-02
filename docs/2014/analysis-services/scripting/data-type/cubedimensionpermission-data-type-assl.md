---
title: "CubeDimensionPermission Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CubeDimensionPermission Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubeDimensionPermission"
helpviewer_keywords: 
  - "CubeDimensionPermission data type"
ms.assetid: d9d39859-5f33-48bc-a402-0071755918de
author: minewiskan
ms.author: owend
manager: craigg
---
# CubeDimensionPermission Data Type (ASSL)
  Defines a primitive data type that represents the permissions for a single role on a specific dimension in a cube.  
  
## Syntax  
  
```xml  
  
<CubeDimensionPermission>  
   <CubeDimensionID>...</CubeDimensionID>  
   <Description>...</Description>  
   <Read>...</Read>  
   <Write>...</Write>  
   <AttributePermissions>...</AttributePermissions>  
   <Annotations>...</Annotations>  
</CubeDimensionPermission>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [AttributePermissions](../collections/attributepermissions-element-assl.md), [CubeDimensionID](../properties/id-element-assl.md), [Description](../properties/description-element-assl.md), [Read](../properties/read-element-assl.md), [Write](../properties/write-element-assl.md)|  
|Derived elements|[DimensionPermission](../objects/dimensionpermission-element-assl.md) ([DimensionPermissions](../collections/dimensionpermissions-element-assl.md) collection of [Dimension](../objects/dimension-element-assl.md) or [CubePermission](../objects/cubepermission-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeDimensionPermission>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
