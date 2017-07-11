---
title: "ResyncEnum | Microsoft Docs"
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
  - "ResyncEnum"
helpviewer_keywords: 
  - "ResyncEnum enumeration [ADO]"
ms.assetid: d3df2c90-e570-4c40-a79a-25b3448a009c
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# ResyncEnum
Specifies whether underlying values are overwritten by a call to [Resync](../../../ado/reference/ado-api/resync-method.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adResyncAllValues**|2|Default. Overwrites data, and pending updates are canceled.|  
|**adResyncUnderlyingValues**|1|Does not overwrite data, and pending updates are not canceled.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.Resync.ALLVALUES|  
|AdoEnums.Resync.UNDERLYINGVALUES|  
  
## Applies To  
 [Resync Method](../../../ado/reference/ado-api/resync-method.md)