---
title: "MarshalOptionsEnum | Microsoft Docs"
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
  - "MarshalOptionsEnum"
helpviewer_keywords: 
  - "MarshalOptionsEnum enumeration [ADO]"
ms.assetid: 4013075d-dbea-4bbc-a6f4-c345a55c5633
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# MarshalOptionsEnum
Specifies which records should be returned to the server.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adMarshalAll**|0|Default. Returns all rows to the server.|  
|**adMarshalModifiedOnly**|1|Returns only modified rows to the server.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.MarshalOptions.ALL|  
|AdoEnums.MarshalOptions.MODIFIEDONLY|  
  
## Applies To  
 [MarshalOptions Property (ADO)](../../../ado/reference/ado-api/marshaloptions-property-ado.md)