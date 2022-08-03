---
title: "Type Property (ADO MD)"
description: "Type Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.prod: sql
ms.technology: ado
ms.topic: reference
f1_keywords: 
helpviewer_keywords:
  - "Type property [ADO MD]"
apitype: "COM"
---
# Type Property (ADO MD)
Indicates the type of the current [member](./member-object-ado-md.md).  
  
## Return Values  
 Returns a [MemberTypeEnum](./membertypeenum.md) value and is read-only.  
  
## Remarks  
 This property is supported only on [Member](./member-object-ado-md.md) objects belonging to a [Level](./level-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Position](./position-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](./member-object-ado-md.md)