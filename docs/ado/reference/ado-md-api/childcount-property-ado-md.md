---
title: "ChildCount Property (ADO MD)"
description: "ChildCount Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ChildCount"
  - "Member::ChildCount"
helpviewer_keywords:
  - "ChildCount property [ADO MD]"
apitype: "COM"
---
# ChildCount Property (ADO MD)
Indicates the number of members for which the current [Member](./member-object-ado-md.md) object is the parent in a hierarchy.  
  
## Return Values  
 Returns a **Long** integer and is read-only.  
  
## Remarks  
 Use the **ChildCount** property to return an estimate of how many children a **Member** has. The actual children of a **Member** can be returned by the [Children](./children-property-ado-md.md) property.  
  
 For **Member** objects from a [Position](./position-object-ado-md.md) object, the maximum number returned is 65536. If the actual number of children exceeds 65536, the value returned will still be 65536. Therefore, the application should interpret a **ChildCount** of 65536 as equal to or greater than 65536 children.  
  
 For **Member** objects from a [Level](./level-object-ado-md.md) object, use the ADO collection [Count](../ado-api/count-property-ado.md) property on the **Children** collection to determine the exact number of children. Determining the exact number of children may be slow if the number of children in the collection is large.  
  
## Applies To  
 [Member Object (ADO MD)](./member-object-ado-md.md)  
  
## See Also  
 [Children Property (ADO MD)](./children-property-ado-md.md)   
 [Count Property (ADO)](../ado-api/count-property-ado.md)   
 [Members Collection (ADO MD)](./members-collection-ado-md.md)