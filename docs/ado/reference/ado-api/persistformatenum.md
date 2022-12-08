---
title: "PersistFormatEnum"
description: "PersistFormatEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "PersistFormatEnum"
helpviewer_keywords:
  - "PersistFormatEnum enumeration [ADO]"
apitype: "COM"
---
# PersistFormatEnum
Specifies the format in which to save a [Recordset](./recordset-object-ado.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adPersistADTG**|0|Indicates Microsoft Advanced Data TableGram (ADTG) format.|  
|**adPersistADO**|1|Indicates that ADO's own Extensible Markup Language (XML) format will be used. This value is the same as adPersistXML and is included for backwards compatibility.|  
|**adPersistXML**|1|Indicates Extensible Markup Language (XML) format.|  
|**adPersistProviderSpecific**|2|Indicates that the provider will persist the **Recordset** using its own format.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.PersistFormat.ADTG|  
|AdoEnums.PersistFormat.XML|  
  
## Applies To  
 [Save Method](./save-method.md)