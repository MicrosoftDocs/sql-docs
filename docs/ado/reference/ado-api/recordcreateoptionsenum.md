---
title: "RecordCreateOptionsEnum"
description: "RecordCreateOptionsEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "RecordCreateOptionsEnum"
helpviewer_keywords:
  - "RecordCreateOptionsEnum enumeration [ADO]"
apitype: "COM"
---
# RecordCreateOptionsEnum
Specifies whether an existing **Record** should be opened or a new **Record** created for the [Record](./record-object-ado.md) object [Open](./open-method-ado-record.md) method. The values can be combined with an AND operator.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adCreateCollection**|0x2000|Creates a new **Record** at the node specified by *Source* parameter, instead of opening an existing **Record**. If the source points to an existing node, then a run-time error occurs, unless **adCreateCollection** is combined with **adOpenIfExists** or **adCreateOverwrite**.|  
|**adCreateNonCollection**|0|Creates a new **Record** of type [adSimpleRecord](./recordtypeenum.md).|  
|**adCreateOverwrite**|0x4000000|Modifies the creation flags **adCreateCollection**, **adCreateNonCollection**, and **adCreateStructDoc**. When OR is used with this value and one of the creation flag values, if the source URL points to an existing node or **Record**, then the existing **Record** is overwritten and a new one is created in its place. This value cannot be used together with **adOpenIfExists**.|  
|**adCreateStructDoc**|0x80000000|Creates a new **Record** of type [adStructDoc](./recordtypeenum.md), instead of opening an existing **Record**.|  
|**adFailIfNotExists**|-1|Default. Results in a run-time error if *Source* points to a non-existent node.|  
|**adOpenIfExists**|0x2000000|Modifies the creation flags **adCreateCollection**, **adCreateNonCollection**, and **adCreateStructDoc**. When OR is used with this value and one of the creation flag values, if the source URL points to an existing node or **Record** object, then the provider must open the existing **Record** instead of creating a new one. This value cannot be used together with **adCreateOverwrite**.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [Open Method (ADO Record)](./open-method-ado-record.md)