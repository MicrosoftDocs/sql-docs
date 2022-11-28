---
title: "RecordOpenOptionsEnum"
description: "RecordOpenOptionsEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "RecordOpenOptionsEnum"
helpviewer_keywords:
  - "RecordOpenOptionsEnum enumeration [ADO]"
apitype: "COM"
---
# RecordOpenOptionsEnum
Specifies options for opening a [Record](./record-object-ado.md). These values may be combined by using OR.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adDelayFetchFields**|0x8000|Indicates to the provider that the fields associated with the **Record** need not be retrieved initially, but can be retrieved at the first attempt to access the field. The default behavior, indicated by the absence of this flag, is to retrieve all the **Record** object fields.|  
|**adDelayFetchStream**|0x4000|Indicates to the provider that the default stream associated with the **Record** need not be retrieved initially. The default behavior, indicated by the absence of this flag, is to retrieve the default stream associated with the **Record** object.|  
|**adOpenAsync**|0x1000|Indicates that the **Record** object is opened in asynchronous mode.|  
|**adOpenExecuteCommand**|0x10000|Indicates that the Source string contains command text that should be executed. This value is equivalent to the **adCmdText** option on **Recordset.Open**.|  
|**adOpenRecordUnspecified**|-1|Default. Indicates no options are specified.|  
|**adOpenOutput**|0x800000|Indicates that if the source points to a node that contains an executable script (such as an .ASP page), then the opened **Record** will contain the results of the executed script. This value is only valid with non-collection records.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [Open Method (ADO Record)](./open-method-ado-record.md)