---
title: "DrilledDown Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "DrilledDown"
  - "Member::DrilledDown"
helpviewer_keywords: 
  - "DrilledDown property [ADO MD]"
ms.assetid: bf39dd36-fc7a-4f6e-86c0-fa71430c0d86
author: MightyPen
ms.author: genemi
manager: craigg
---
# DrilledDown Property (ADO MD)
Indicates whether children immediately follow the [member](../../../ado/reference/ado-md-api/member-object-ado-md.md) on the axis.  
  
## Return Values  
 Returns a **Boolean** value and is read-only. **DrilledDown** returns **True** if there are no child members of the current member on the axis. **DrilledDown** returns **False** if the current member has one or more child members on the axis.  
  
## Remarks  
 Use the **DrilledDown** property to determine whether there is at least one child of this member on the axis immediately following this member. This information is useful when displaying the member.  
  
 This property is only supported on [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md) objects belonging to a [Position](../../../ado/reference/ado-md-api/position-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Level](../../../ado/reference/ado-md-api/level-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](../../../ado/reference/ado-md-api/member-object-ado-md.md)  
  
## See Also  
 [ParentSameAsPrev Property (ADO MD)](../../../ado/reference/ado-md-api/parentsameasprev-property-ado-md.md)
