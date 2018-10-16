---
title: "LevelDepth Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "LevelDepth"
  - "Member::LevelDepth"
helpviewer_keywords: 
  - "LevelDepth property [ADO MD]"
ms.assetid: 8a1cfe2c-f207-4445-b152-ade090f64608
author: MightyPen
ms.author: genemi
manager: craigg
---
# LevelDepth Property (ADO MD)
Indicates the number of levels between the root of the hierarchy and a [member](../../../ado/reference/ado-md-api/member-object-ado-md.md).  
  
## Return Values  
 Returns a **Long** integer, and is read-only.  
  
## Remarks  
 Use the **LevelDepth** property to determine the distance of the [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md)object from the root level of the hierarchy. The **LevelDepth**of a member at the root level is 0. This corresponds to the [Depth](../../../ado/reference/ado-md-api/depth-property-ado-md.md) property of a [Level](../../../ado/reference/ado-md-api/level-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](../../../ado/reference/ado-md-api/member-object-ado-md.md)  
  
## See Also  
 [Depth Property (ADO MD)](../../../ado/reference/ado-md-api/depth-property-ado-md.md)   
 [Level Object (ADO MD)](../../../ado/reference/ado-md-api/level-object-ado-md.md)
