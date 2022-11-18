---
title: "CursorOptionEnum"
description: "CursorOptionEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "CursorOptionEnum"
helpviewer_keywords:
  - "CursorOptionEnum enumeration [ADO]"
apitype: "COM"
---
# CursorOptionEnum
Specifies what functionality the [Supports](./supports-method.md) method should test for.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adAddNew**|0x1000400|Supports the [AddNew](./addnew-method-ado.md) method to add new records.|  
|**adApproxPosition**|0x4000|Supports the [AbsolutePosition](./absoluteposition-property-ado.md) and [AbsolutePage](./absolutepage-property-ado.md) properties.|  
|**adBookmark**|0x2000|Supports the [Bookmark](./bookmark-property-ado.md) property to gain access to specific records.|  
|**adDelete**|0x1000800|Supports the [Delete](./delete-method-ado-recordset.md) method to delete records.|  
|**adFind**|0x80000|Supports the [Find](./find-method-ado.md) method to locate a row in a [Recordset](./recordset-object-ado.md).|  
|**adHoldRecords**|0x100|Retrieves more records or changes the next position without committing all pending changes.|  
|**adIndex**|0x100000|Supports the [Index](./index-property.md) property to name an index.|  
|**adMovePrevious**|0x200|Supports the [MoveFirst](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md) and [MovePrevious](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md) methods, and [Move](./move-method-ado.md) or [GetRows](./getrows-method-ado.md) methods to move the current record position backward without requiring bookmarks.|  
|**adNotify**|0x40000|Indicates that the underlying data provider supports notifications (which determines whether **Recordset** events are supported).|  
|**adResync**|0x20000|Supports the [Resync](./resync-method.md) method to update the cursor with the data that is visible in the underlying database.|  
|**adSeek**|0x200000|Supports the [Seek](./seek-method.md) method to locate a row in a **Recordset**.|  
|**adUpdate**|0x1008000|Supports the [Update](./update-method.md) method to modify existing data.|  
|**adUpdateBatch**|0x10000|Supports batch updating ([UpdateBatch](./updatebatch-method.md) and [CancelBatch](./cancelbatch-method-ado.md) methods) to transmit groups of changes to the provider.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.CursorOption.ADDNEW|  
|AdoEnums.CursorOption.APPROXPOSITION|  
|AdoEnums.CursorOption.BOOKMARK|  
|AdoEnums.CursorOption.DELETE|  
|AdoEnums.CursorOption.FIND|  
|AdoEnums.CursorOption.HOLDRECORDS|  
|AdoEnums.CursorOption.INDEX|  
|AdoEnums.CursorOption.MOVEPREVIOUS|  
|AdoEnums.CursorOption.NOTIFY|  
|AdoEnums.CursorOption.RESYNC|  
|AdoEnums.CursorOption.SEEK|  
|AdoEnums.CursorOption.UPDATE|  
|AdoEnums.CursorOption.UPDATEBATCH|  
  
## Applies To  
 [Supports Method](./supports-method.md)