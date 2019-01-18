---
title: "ResyncEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ResyncEnum"
helpviewer_keywords: 
  - "ResyncEnum enumeration [ADO]"
ms.assetid: d3df2c90-e570-4c40-a79a-25b3448a009c
author: MightyPen
ms.author: genemi
manager: craigg
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
