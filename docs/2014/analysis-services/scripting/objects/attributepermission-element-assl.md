---
title: "AttributePermission Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributePermission Element (ASSL)
  Defines the permissions that members of a [Role](role-element-assl.md) element have on the attributes of an individual dimension in a [Cube](cube-element-assl.md) element.  
  
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
|Parent elements|[AttributePermissions](../collections/attributepermissions-element-assl.md)|  
|Child elements|[AllowedSet](../properties/allowedset-element-assl.md), [Annotations](../collections/annotations-element-assl.md), [AttributeID](../properties/id-element-assl.md), [DefaultMember](member-element-assl.md), [DeniedSet](../properties/deniedset-element-assl.md), [Description](../properties/description-element-assl.md), [VisualTotals](../properties/visualtotals-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributePermission>.  
  
## See Also  
 [CubeDimensionPermission Data Type &#40;ASSL&#41;](../data-type/permission-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
