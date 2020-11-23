---
description: "Type Property (ADO MD)"
title: "Type Property (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Member::Type"
  - "Type"
helpviewer_keywords: 
  - "Type property [ADO MD]"
ms.assetid: 34698910-64b9-41d8-8531-9de12f2b1e32
author: rothja
ms.author: jroth
---
# Type Property (ADO MD)
Indicates the type of the current [member](./member-object-ado-md.md).  
  
## Return Values  
 Returns a [MemberTypeEnum](./membertypeenum.md) value and is read-only.  
  
## Remarks  
 This property is supported only on [Member](./member-object-ado-md.md) objects belonging to a [Level](./level-object-ado-md.md) object. An error occurs when this property is referenced from **Member** objects belonging to a [Position](./position-object-ado-md.md) object.  
  
## Applies To  
 [Member Object (ADO MD)](./member-object-ado-md.md)