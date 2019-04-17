---
title: "RecordTypeEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "RecordTypeEnum"
helpviewer_keywords: 
  - "RecordTypeEnum enumeration [ADO]"
ms.assetid: f557e537-015d-4ba7-8a41-a6f00b366a91
author: MightyPen
ms.author: genemi
manager: craigg
---
# RecordTypeEnum
Specifies the type of [Record](../../../ado/reference/ado-api/record-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adSimpleRecord**|0|Indicates a *simple* record (does not contain child nodes).|  
|**adCollectionRecord**|1|Indicates a *collection* record (contains child nodes).|  
|**adRecordUnknown**|-1|Indicates that the type of this **Record** is unknown.|  
|**adStructDoc**|2|Indicates a special kind of *collection* record that represents COM structured documents.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [RecordType Property (ADO)](../../../ado/reference/ado-api/recordtype-property-ado.md)
