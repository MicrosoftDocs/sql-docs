---
title: "Children Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Member::Children"
  - "Children"
helpviewer_keywords: 
  - "Children property [ADO MD]"
ms.assetid: 61d36468-1ccd-467a-9cb5-17d0bfacc766
author: MightyPen
ms.author: genemi
manager: craigg
---
# Children Property (ADO MD)
Returns a [Members](../../../ado/reference/ado-md-api/members-collection-ado-md.md) collection for which the current [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md) is the parent in the hierarchy.  
  
## Return Values  
 Returns a **Members** collection and is read-only.  
  
## Remarks  
 The **Children** property contains a **Members** collection for which the current **Member** is the hierarchical parent. Leaf level **Member** objects have no child members in the **Members** collection. This property is only supported on **Member** objects belonging to a [Level](../../../ado/reference/ado-md-api/level-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Position](../../../ado/reference/ado-md-api/position-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](../../../ado/reference/ado-md-api/member-object-ado-md.md)  
  
## See Also  
 [ChildCount Property (ADO MD)](../../../ado/reference/ado-md-api/childcount-property-ado-md.md)
