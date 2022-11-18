---
title: "Parent Property (ADO MD)"
description: "Parent Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Parent"
  - "Member::Parent"
helpviewer_keywords:
  - "Parent property [ADO MD]"
apitype: "COM"
---
# Parent Property (ADO MD)
Indicates the member that is the parent of the current [member](./member-object-ado-md.md) in a hierarchy.  
  
## Return Values  
 Returns a [Member](./member-object-ado-md.md) object and is read-only.  
  
## Remarks  
 A member that is at the top level of a hierarchy (the root) has no parent. This property is supported only on **Member** objects belonging to a [Level](./level-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Position](./position-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](./member-object-ado-md.md)  
  
## See Also  
 [Children Property (ADO MD)](./children-property-ado-md.md)