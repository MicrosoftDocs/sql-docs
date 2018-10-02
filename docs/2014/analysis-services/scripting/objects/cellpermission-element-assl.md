---
title: "CellPermission Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CellPermission Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CellPermission"
helpviewer_keywords: 
  - "CellPermission element"
ms.assetid: 54a6afc0-1fcb-4b24-851a-6d81e1fe0efc
author: minewiskan
ms.author: owend
manager: craigg
---
# CellPermission Element (ASSL)
  Describes the permissions that members of a [Role](role-element-assl.md) element have on individual cells within a [Cube](cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CellPermissions>  
   <CellPermission>  
      <Access>...</Access>  
            <Description>...</Description>  
      <Expression>...</Expression>  
      <Annotations>...</Annotations>  
   </CellPermission>  
</CellPermissions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CellPermissions](../collections/cellpermissions-element-assl.md)|  
|Child elements|[Access](../properties/access-element-assl.md), [Annotations](../collections/annotations-element-assl.md), [Description](../properties/description-element-assl.md), [Expression](../properties/expression-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CellPermission>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
