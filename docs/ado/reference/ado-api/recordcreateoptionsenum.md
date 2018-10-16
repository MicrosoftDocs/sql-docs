---
title: "RecordCreateOptionsEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "RecordCreateOptionsEnum"
helpviewer_keywords: 
  - "RecordCreateOptionsEnum enumeration [ADO]"
ms.assetid: 6d746670-0850-4065-9cd4-168dea1d3ea9
author: MightyPen
ms.author: genemi
manager: craigg
---
# RecordCreateOptionsEnum
Specifies whether an existing **Record** should be opened or a new **Record** created for the [Record](../../../ado/reference/ado-api/record-object-ado.md) object [Open](../../../ado/reference/ado-api/open-method-ado-record.md) method. The values can be combined with an AND operator.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adCreateCollection**|0x2000|Creates a new **Record** at the node specified by *Source* parameter, instead of opening an existing **Record**. If the source points to an existing node, then a run-time error occurs, unless **adCreateCollection** is combined with **adOpenIfExists** or **adCreateOverwrite**.|  
|**adCreateNonCollection**|0|Creates a new **Record** of type [adSimpleRecord](../../../ado/reference/ado-api/recordtypeenum.md).|  
|**adCreateOverwrite**|0x4000000|Modifies the creation flags **adCreateCollection**, **adCreateNonCollection**, and **adCreateStructDoc**. When OR is used with this value and one of the creation flag values, if the source URL points to an existing node or **Record**, then the existing **Record** is overwritten and a new one is created in its place. This value cannot be used together with **adOpenIfExists**.|  
|**adCreateStructDoc**|0x80000000|Creates a new **Record** of type [adStructDoc](../../../ado/reference/ado-api/recordtypeenum.md), instead of opening an existing **Record**.|  
|**adFailIfNotExists**|-1|Default. Results in a run-time error if *Source* points to a non-existent node.|  
|**adOpenIfExists**|0x2000000|Modifies the creation flags **adCreateCollection**, **adCreateNonCollection**, and **adCreateStructDoc**. When OR is used with this value and one of the creation flag values, if the source URL points to an existing node or **Record** object, then the provider must open the existing **Record** instead of creating a new one. This value cannot be used together with **adCreateOverwrite**.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [Open Method (ADO Record)](../../../ado/reference/ado-api/open-method-ado-record.md)
