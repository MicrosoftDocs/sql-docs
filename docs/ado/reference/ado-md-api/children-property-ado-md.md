---
description: "Children Property (ADO MD)"
title: "Children Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
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
author: rothja
ms.author: jroth
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