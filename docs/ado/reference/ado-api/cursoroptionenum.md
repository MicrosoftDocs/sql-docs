---
title: "CursorOptionEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "CursorOptionEnum"
helpviewer_keywords: 
  - "CursorOptionEnum enumeration [ADO]"
ms.assetid: 4e10cda7-ce81-4466-94c2-844d38191cf1
author: MightyPen
ms.author: genemi
manager: craigg
---
# CursorOptionEnum
Specifies what functionality the [Supports](../../../ado/reference/ado-api/supports-method.md) method should test for.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adAddNew**|0x1000400|Supports the [AddNew](../../../ado/reference/ado-api/addnew-method-ado.md) method to add new records.|  
|**adApproxPosition**|0x4000|Supports the [AbsolutePosition](../../../ado/reference/ado-api/absoluteposition-property-ado.md) and [AbsolutePage](../../../ado/reference/ado-api/absolutepage-property-ado.md) properties.|  
|**adBookmark**|0x2000|Supports the [Bookmark](../../../ado/reference/ado-api/bookmark-property-ado.md) property to gain access to specific records.|  
|**adDelete**|0x1000800|Supports the [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md) method to delete records.|  
|**adFind**|0x80000|Supports the [Find](../../../ado/reference/ado-api/find-method-ado.md) method to locate a row in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).|  
|**adHoldRecords**|0x100|Retrieves more records or changes the next position without committing all pending changes.|  
|**adIndex**|0x100000|Supports the [Index](../../../ado/reference/ado-api/index-property.md) property to name an index.|  
|**adMovePrevious**|0x200|Supports the [MoveFirst](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md) and [MovePrevious](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md) methods, and [Move](../../../ado/reference/ado-api/move-method-ado.md) or [GetRows](../../../ado/reference/ado-api/getrows-method-ado.md) methods to move the current record position backward without requiring bookmarks.|  
|**adNotify**|0x40000|Indicates that the underlying data provider supports notifications (which determines whether **Recordset** events are supported).|  
|**adResync**|0x20000|Supports the [Resync](../../../ado/reference/ado-api/resync-method.md) method to update the cursor with the data that is visible in the underlying database.|  
|**adSeek**|0x200000|Supports the [Seek](../../../ado/reference/ado-api/seek-method.md) method to locate a row in a **Recordset**.|  
|**adUpdate**|0x1008000|Supports the [Update](../../../ado/reference/ado-api/update-method.md) method to modify existing data.|  
|**adUpdateBatch**|0x10000|Supports batch updating ([UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) and [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) methods) to transmit groups of changes to the provider.|  
  
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
 [Supports Method](../../../ado/reference/ado-api/supports-method.md)
