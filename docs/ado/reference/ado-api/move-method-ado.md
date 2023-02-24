---
title: "Move Method (ADO)"
description: "Move Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::Move"
  - "Recordset15::raw_Move"
helpviewer_keywords:
  - "Move method [ADO]"
apitype: "COM"
---
# Move Method (ADO)
Moves the position of the current record in a [Recordset](./recordset-object-ado.md) object.  
  
## Syntax  
  
```  
  
recordset.Move NumRecords, Start  
```  
  
#### Parameters  
 *NumRecords*  
 A signed **Long** expression that specifies the number of records that the current record position moves.  
  
 *Start*  
 Optional. A **String** value or **Variant** that evaluates to a bookmark. You can also use a [BookmarkEnum](./bookmarkenum.md) value.  
  
## Remarks  
 The **Move** method is supported on all **Recordset** objects.  
  
 If the *NumRecords* argument is greater than zero, the current record position moves forward (toward the end of the **Recordset**). If *NumRecords* is less than zero, the current record position moves backward (toward the beginning of the **Recordset**).  
  
 If the **Move** call would move the current record position to a point before the first record, ADO sets the current record to the position before the first record in the recordset ([BOF](./bof-eof-properties-ado.md) is **True**). An attempt to move backward when the **BOF** property is already **True** generates an error.  
  
 If the **Move** call would move the current record position to a point after the last record, ADO sets the current record to the position after the last record in the recordset ([EOF](./bof-eof-properties-ado.md) is **True**). An attempt to move forward when the **EOF** property is already **True** generates an error.  
  
 Calling the **Move** method from an empty **Recordset** object generates an error.  
  
 If you pass the *Start* argument, the move is relative to the record with this bookmark, assuming the **Recordset** object supports bookmarks. If not specified, the move is relative to the current record.  
  
 If you are using the [CacheSize](./cachesize-property-ado.md) property to locally cache records from the provider, passing a *NumRecords* argument that moves the current record position outside the current group of cached records forces ADO to retrieve a new group of records, starting from the destination record. The **CacheSize** property determines the size of the newly retrieved group, and the destination record is the first record retrieved.  
  
 If the **Recordset** object is forward only, a user can still pass a *NumRecords* argument less than zero, provided the destination is within the current set of cached records. If the **Move** call would move the current record position to a record before the first cached record, an error will occur. Thus, you can use a record cache that supports full scrolling over a provider that supports only forward scrolling. Because cached records are loaded into memory, you should avoid caching more records than are necessary. Even if a forward-only **Recordset** object supports backward moves in this way, calling the [MovePrevious](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md) method on any forward-only **Recordset** object will still generate an error.  
  
> [!NOTE]
>  Support for moving backwards in a forward-only **Recordset** is not predictable, depending upon your provider. If the current record has been positioned after the last record in the **Recordset**, **Move** backwards may not result in the correct current position.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [Move Method Example (VB)](./move-method-example-vb.md)   
 [Move Method Example (VBScript)](./move-method-example-vbscript.md)   
 [Move Method Example (VC++)](./move-method-example-vc.md)   
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (ADO)](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md)   
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (RDS)](../rds-api/movefirst-movelast-movenext-and-moveprevious-methods-rds.md)   
 [MoveRecord Method (ADO)](./moverecord-method-ado.md)