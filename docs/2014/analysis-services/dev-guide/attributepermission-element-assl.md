---
title: "AttributePermission Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "AttributePermission Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AttributePermission"
helpviewer_keywords: 
  - "AttributePermission element"
ms.assetid: efc8aa63-3959-4b2e-98f8-2a9c424298c2
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AttributePermission Element (ASSL)
  Defines the permissions that members of a [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element have on the attributes of an individual dimension in a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AttributePermissions>  
   <AttributePermission>  
      <AttributeID>...</AttributeID>  
      <Description>...</Description>  
      <DefaultMember>...</DefaultMember>  
            <VisualTotals>...</VisualTotals>  
      <AllowedSet>...</AllowedSet>  
            <DeniedSet>...</DeniedSet>  
      <Annotations>...</Annotations>  
   </AttributePermission>  
</AttributePermissions>  
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
|Parent elements|[AttributePermissions](../../../2014/analysis-services/dev-guide/attributepermissions-element-assl.md)|  
|Child elements|[AllowedSet](../../../2014/analysis-services/dev-guide/allowedset-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [AttributeID](../../../2014/analysis-services/dev-guide/attributeid-element-assl.md), [DefaultMember](../../../2014/analysis-services/dev-guide/defaultmember-element-assl.md), [DeniedSet](../../../2014/analysis-services/dev-guide/deniedset-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [VisualTotals](../../../2014/analysis-services/dev-guide/visualtotals-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributePermission>.  
  
## See Also  
 [CubeDimensionPermission Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/cubedimensionpermission-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  