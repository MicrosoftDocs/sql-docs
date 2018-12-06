---
title: "Description Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Member::Description"
  - "Level::Description"
  - "CubeDef::Description"
  - "Hierarchy::Description"
  - "Description"
  - "Dimension::Description"
helpviewer_keywords: 
  - "Description property [ADO MD]"
ms.assetid: 6d626d35-0bf3-4f24-9934-ad9c9c91273a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Description Property (ADO MD)
Returns a text explanation of the current object.  
  
## Return Values  
 Returns a **String** and is read-only.  
  
## Remarks  
 For [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md) objects, **Description** applies only to measure and formula members. **Description** returns an empty string ("") for all other types of members. For more information about the various types of members, see the [Type](../../../ado/reference/ado-md-api/type-property-ado-md.md) property.  
  
 This property is only supported on **Member** objects that belong to a [Level](../../../ado/reference/ado-md-api/level-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Position](../../../ado/reference/ado-md-api/position-object-ado-md.md) object.  
  
## Applies To  
  
||||  
|-|-|-|  
|[CubeDef Object (ADO MD)](../../../ado/reference/ado-md-api/cubedef-object-ado-md.md)|[Dimension Object (ADO MD)](../../../ado/reference/ado-md-api/dimension-object-ado-md.md)|[Hierarchy Object (ADO MD)](../../../ado/reference/ado-md-api/hierarchy-object-ado-md.md)|  
|[Level Object (ADO MD)](../../../ado/reference/ado-md-api/level-object-ado-md.md)|[Member Object (ADO MD)](../../../ado/reference/ado-md-api/member-object-ado-md.md)||
