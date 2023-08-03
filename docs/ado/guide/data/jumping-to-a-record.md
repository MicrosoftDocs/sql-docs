---
title: "Jumping to a Record"
description: "Jumping to a Record"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "record jumping [ADO]"
  - "jumping to record [ADO]"
---
# Jumping to a Record
The [Move](../../reference/ado-api/move-method-ado.md) method allows you to move forward or backward in the **Recordset** a specified number of records by using the following syntax:  
  
```  
oRs.Move NumRecords, Start  
```  
  
## Remarks  
 The **Move** method is supported on all **Recordset** objects.  
  
 If the *NumRecords* argument is greater than zero, the current record position moves forward (toward the end of the **Recordset**). If *NumRecords* is less than zero, the current record position moves backward (toward the beginning of the **Recordset**).  
  
 If the **Move** call would move the current record position to a point before the first record, ADO sets the current record to the position before the first record in the **Recordset** (**BOF** is **True**). An attempt to move backward when the **BOF** property is already **True** generates an error.  
  
 If the **Move** call would move the current record position to a point after the last record, ADO sets the current record to the position after the last record in the **Recordset** (**EOF** is **True**). An attempt to move forward when the **EOF** property is already **True** generates an error.  
  
 Calling the **Move** method from an empty **Recordset** object generates an error.  
  
 If you pass a bookmark in the *Start* argument, the move is relative to the record with this bookmark, assuming the **Recordset** object supports bookmarks. A bookmark is obtained by using the [Bookmark](../../reference/ado-api/bookmark-property-ado.md) property. If not specified, the move is relative to the current record.  
  
 If you are using the **CacheSize** property to locally cache records from the provider, passing a *NumRecords* argument that moves the current record position outside the current group of cached records forces ADO to retrieve a new group of records, starting from the destination record. The **CacheSize** property determines the size of the newly retrieved group, and the destination record is the first record retrieved.