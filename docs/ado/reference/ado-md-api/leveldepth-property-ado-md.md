---
title: "LevelDepth Property (ADO MD)"
description: "LevelDepth Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "LevelDepth"
  - "Member::LevelDepth"
helpviewer_keywords:
  - "LevelDepth property [ADO MD]"
apitype: "COM"
---
# LevelDepth Property (ADO MD)
Indicates the number of levels between the root of the hierarchy and a [member](./member-object-ado-md.md).  
  
## Return Values  
 Returns a **Long** integer, and is read-only.  
  
## Remarks  
 Use the **LevelDepth** property to determine the distance of the [Member](./member-object-ado-md.md)object from the root level of the hierarchy. The **LevelDepth**of a member at the root level is 0. This corresponds to the [Depth](./depth-property-ado-md.md) property of a [Level](./level-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](./member-object-ado-md.md)  
  
## See Also  
 [Depth Property (ADO MD)](./depth-property-ado-md.md)   
 [Level Object (ADO MD)](./level-object-ado-md.md)