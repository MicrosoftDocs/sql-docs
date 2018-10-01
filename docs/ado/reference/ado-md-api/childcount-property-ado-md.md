---
title: "ChildCount Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ChildCount"
  - "Member::ChildCount"
helpviewer_keywords: 
  - "ChildCount property [ADO MD]"
ms.assetid: 5463be22-ca50-43ea-9c92-468fc8eda280
author: MightyPen
ms.author: genemi
manager: craigg
---
# ChildCount Property (ADO MD)
Indicates the number of members for which the current [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md) object is the parent in a hierarchy.  
  
## Return Values  
 Returns a **Long** integer and is read-only.  
  
## Remarks  
 Use the **ChildCount** property to return an estimate of how many children a **Member** has. The actual children of a **Member** can be returned by the [Children](../../../ado/reference/ado-md-api/children-property-ado-md.md) property.  
  
 For **Member** objects from a [Position](../../../ado/reference/ado-md-api/position-object-ado-md.md) object, the maximum number returned is 65536. If the actual number of children exceeds 65536, the value returned will still be 65536. Therefore, the application should interpret a **ChildCount** of 65536 as equal to or greater than 65536 children.  
  
 For **Member** objects from a [Level](../../../ado/reference/ado-md-api/level-object-ado-md.md) object, use the ADO collection [Count](../../../ado/reference/ado-api/count-property-ado.md) property on the **Children** collection to determine the exact number of children. Determining the exact number of children may be slow if the number of children in the collection is large.  
  
## Applies To  
 [Member Object (ADO MD)](../../../ado/reference/ado-md-api/member-object-ado-md.md)  
  
## See Also  
 [Children Property (ADO MD)](../../../ado/reference/ado-md-api/children-property-ado-md.md)   
 [Count Property (ADO)](../../../ado/reference/ado-api/count-property-ado.md)   
 [Members Collection (ADO MD)](../../../ado/reference/ado-md-api/members-collection-ado-md.md)
