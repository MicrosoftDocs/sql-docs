---
title: "MemberTypeEnum | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
apitype: "COM"
f1_keywords: 
  - "MemberTypeEnum"
helpviewer_keywords: 
  - "MemberTypeEnum enumeration [ADO MD]"
ms.assetid: 5d8132c0-7ca2-4f86-8336-1b34213869ad
caps.latest.revision: 9
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# MemberTypeEnum
Specifies the setting for the [Type](../../../ado/reference/ado-md-api/type-property-ado-md.md) property of a [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adMemberAll**|4|Indicates that the **Member** object represents all members of the level.|  
|**adMemberFormula**|3|Indicates that the **Member** object is calculated using a formula expression.|  
|**adMemberMeasure**|2|Indicates that the **Member** object belongs to the Measures dimension and represents a quantitative attribute.|  
|**adMemberRegular**|1|Default. Indicates that the **Member** object represents an instance of a business entity.|  
|**adMemberUnknown**|0|Cannot determine the type of the member.|