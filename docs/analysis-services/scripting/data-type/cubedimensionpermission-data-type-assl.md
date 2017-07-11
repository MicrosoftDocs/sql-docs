---
title: "CubeDimensionPermission Data Type (ASSL) | Microsoft Docs"
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
  - "CubeDimensionPermission Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "CubeDimensionPermission"
helpviewer_keywords: 
  - "CubeDimensionPermission data type"
ms.assetid: d9d39859-5f33-48bc-a402-0071755918de
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [AttributePermissions](../../../analysis-services/scripting/collections/attributepermissions-element-assl.md), [CubeDimensionID](../../../analysis-services/scripting/properties/cubedimensionid-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [Read](../../../analysis-services/scripting/properties/read-element-assl.md), [Write](../../../analysis-services/scripting/properties/write-element-assl.md)|  
|Derived elements|[DimensionPermission](../../../analysis-services/scripting/objects/dimensionpermission-element-assl.md) ([DimensionPermissions](../../../analysis-services/scripting/collections/dimensionpermissions-element-assl.md) collection of [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md) or [CubePermission](../../../analysis-services/scripting/objects/cubepermission-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeDimensionPermission>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  