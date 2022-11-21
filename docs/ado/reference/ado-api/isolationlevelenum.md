---
title: "IsolationLevelEnum"
description: "IsolationLevelEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "IsolationLevelEnum"
helpviewer_keywords:
  - "IsolationLevelEnum enumeration [ADO]"
apitype: "COM"
---
# IsolationLevelEnum
Specifies the level of transaction isolation for a [Connection](./connection-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adXactUnspecified**|-1|Indicates that the provider is using a different isolation level than specified, but that the level cannot be determined.|  
|**adXactChaos**|16|Indicates that pending changes from more highly isolated transactions cannot be overwritten.|  
|**adXactBrowse**|256|Indicates that from one transaction you can view uncommitted changes in other transactions.|  
|**adXactReadUncommitted**|256|Same as **adXactBrowse**.|  
|**adXactCursorStability**|4096|Indicates that from one transaction you can view changes in other transactions only after they have been committed.|  
|**adXactReadCommitted**|4096|Same as **adXactCursorStability**.|  
|**adXactRepeatableRead**|65536|Indicates that from one transaction you cannot see changes made in other transactions, but that requerying can retrieve new **Recordset** objects.|  
|**adXactIsolated**|1048576|Indicates that transactions are conducted in isolation of other transactions.|  
|**adXactSerializable**|1048576|Same as **adXactIsolated**.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.IsolationLevel.UNSPECIFIED|  
|AdoEnums.IsolationLevel.CHAOS|  
|AdoEnums.IsolationLevel.BROWSE|  
|AdoEnums.IsolationLevel.READUNCOMMITTED|  
|AdoEnums.IsolationLevel.CURSORSTABILITY|  
|AdoEnums.IsolationLevel.READCOMMITTED|  
|AdoEnums.IsolationLevel.REPEATABLEREAD|  
|AdoEnums.IsolationLevel.ISOLATED|  
|AdoEnums.IsolationLevel.SERIALIZABLE|  
  
## Applies To  
 [IsolationLevel Property](./isolationlevel-property.md)