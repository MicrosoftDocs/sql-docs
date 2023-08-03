---
title: "MemberTypeEnum"
description: "MemberTypeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "MemberTypeEnum"
helpviewer_keywords:
  - "MemberTypeEnum enumeration [ADO MD]"
apitype: "COM"
---
# MemberTypeEnum
Specifies the setting for the [Type](./type-property-ado-md.md) property of a [Member](./member-object-ado-md.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adMemberAll**|4|Indicates that the **Member** object represents all members of the level.|  
|**adMemberFormula**|3|Indicates that the **Member** object is calculated using a formula expression.|  
|**adMemberMeasure**|2|Indicates that the **Member** object belongs to the Measures dimension and represents a quantitative attribute.|  
|**adMemberRegular**|1|Default. Indicates that the **Member** object represents an instance of a business entity.|  
|**adMemberUnknown**|0|Cannot determine the type of the member.|