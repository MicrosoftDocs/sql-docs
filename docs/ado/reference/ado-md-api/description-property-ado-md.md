---
title: "Description Property (ADO MD)"
description: "Description Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Member::Description"
  - "Level::Description"
  - "CubeDef::Description"
  - "Hierarchy::Description"
  - "Description"
  - "Dimension::Description"
helpviewer_keywords:
  - "Description property [ADO MD]"
apitype: "COM"
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