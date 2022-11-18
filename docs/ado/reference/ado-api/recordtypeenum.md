---
title: "RecordTypeEnum"
description: "RecordTypeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "RecordTypeEnum"
helpviewer_keywords:
  - "RecordTypeEnum enumeration [ADO]"
apitype: "COM"
---
# RecordTypeEnum
Specifies the type of [Record](./record-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adSimpleRecord**|0|Indicates a *simple* record (does not contain child nodes).|  
|**adCollectionRecord**|1|Indicates a *collection* record (contains child nodes).|  
|**adRecordUnknown**|-1|Indicates that the type of this **Record** is unknown.|  
|**adStructDoc**|2|Indicates a special kind of *collection* record that represents COM structured documents.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [RecordType Property (ADO)](./recordtype-property-ado.md)