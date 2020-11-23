---
description: "Description Property (ADO MD)"
title: "Description Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
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
author: rothja
ms.author: jroth
---
# Description Property (ADO MD)
Returns a text explanation of the current object.  
  
## Return Values  
 Returns a **String** and is read-only.  
  
## Remarks  
 For [Member](./member-object-ado-md.md) objects, **Description** applies only to measure and formula members. **Description** returns an empty string ("") for all other types of members. For more information about the various types of members, see the [Type](./type-property-ado-md.md) property.  
  
 This property is only supported on **Member** objects that belong to a [Level](./level-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Position](./position-object-ado-md.md) object.  
  
## Applies To  

:::row:::
    :::column:::
        [CubeDef Object (ADO MD)](./cubedef-object-ado-md.md)  
        [Dimension Object (ADO MD)](./dimension-object-ado-md.md)  
    :::column-end:::
    :::column:::
        [Hierarchy Object (ADO MD)](./hierarchy-object-ado-md.md)  
        [Level Object (ADO MD)](./level-object-ado-md.md)  
    :::column-end:::
    :::column:::
        [Member Object (ADO MD)](./member-object-ado-md.md)  
    :::column-end:::
:::row-end:::