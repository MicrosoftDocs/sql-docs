---
title: "DrilledDown Property (ADO MD)"
description: "DrilledDown Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "DrilledDown"
  - "Member::DrilledDown"
helpviewer_keywords:
  - "DrilledDown property [ADO MD]"
apitype: "COM"
---
# DrilledDown Property (ADO MD)
Indicates whether children immediately follow the [member](./member-object-ado-md.md) on the axis.  
  
## Return Values  
 Returns a **Boolean** value and is read-only. **DrilledDown** returns **True** if there are no child members of the current member on the axis. **DrilledDown** returns **False** if the current member has one or more child members on the axis.  
  
## Remarks  
 Use the **DrilledDown** property to determine whether there is at least one child of this member on the axis immediately following this member. This information is useful when displaying the member.  
  
 This property is only supported on [Member](./member-object-ado-md.md) objects belonging to a [Position](./position-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Level](./level-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](./member-object-ado-md.md)  
  
## See Also  
 [ParentSameAsPrev Property (ADO MD)](./parentsameasprev-property-ado-md.md)