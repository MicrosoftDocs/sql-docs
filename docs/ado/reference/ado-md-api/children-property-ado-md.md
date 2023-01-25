---
title: "Children Property (ADO MD)"
description: "Children Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Member::Children"
  - "Children"
helpviewer_keywords:
  - "Children property [ADO MD]"
apitype: "COM"
---
# Children Property (ADO MD)
Returns a [Members](./members-collection-ado-md.md) collection for which the current [Member](./member-object-ado-md.md) is the parent in the hierarchy.  
  
## Return Values  
 Returns a **Members** collection and is read-only.  
  
## Remarks  
 The **Children** property contains a **Members** collection for which the current **Member** is the hierarchical parent. Leaf level **Member** objects have no child members in the **Members** collection. This property is only supported on **Member** objects belonging to a [Level](./level-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Position](./position-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](./member-object-ado-md.md)  
  
## See Also  
 [ChildCount Property (ADO MD)](./childcount-property-ado-md.md)