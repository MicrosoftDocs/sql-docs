---
title: "More Ways to Move in a Recordset"
description: "More Ways to Move in a Recordset"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "MoveNext method [ADO]"
  - "MoveLast method [ADO]"
  - "MoveFirst method [ADO]"
  - "Recordset object [ADO], moving"
  - "MovePrevious method [ADO]"
---
# More Ways to Move in a Recordset
The following four methods are used to move around, or scroll, in the **Recordset**: [MoveFirst, MoveLast, MoveNext, and MovePrevious](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md). (Some of these methods are unavailable on forward-only cursors.)  
  
 **MoveFirst** changes the current record position to the first record in the **Recordset**. **MoveLast** changes the current record position to the last record in the **Recordset**. To use **MoveFirst** or **MoveLast**, the **Recordset** object must support bookmarks or backward cursor movement; otherwise, the method call will generate an error.  
  
 **MoveNext** moves the current record position one place forward. If you are on the last record when you call **MoveNext**, **EOF** will be set to **True**. **MovePrevious** moves the current record position one place backward. If you are on the first record when you call **MovePrevious**, **BOF** will be set to **True**. It is wise to check the **EOF** and **BOF** properties when using these methods and to move the cursor back to a valid current record position if you move off either end of the **Recordset**, as shown here:  
  
```  
. . .  
oRs.MoveNext  
If oRs.EOF Then oRs.MoveLast  
. . .   
```  
  
 Or, in the case of the **MovePrevious** method:  
  
```  
. . .   
oRs.MovePrevious  
If oRs.BOF Then oRs.MoveFirst  
. . .  
```  
  
 In cases where the **Recordset** has been filtered or sorted and the current record's data is changed, the position may also change. In such cases the **MoveNext** method works normally, but be aware that the position is moved one record forward from the new position, not the old position. For example, changing the data in the current record, such that the record is moved to the end of the sorted **Recordset**, would mean that calling **MoveNext** results in ADO setting the current record to the position after the last record in the **Recordset** (**EOF** = **True**).  
  
 The behavior of the various Move methods of the **Recordset** object depends, to some extent, on the data within the **Recordset**. New records added to the **Recordset** are initially added in a particular order, which is defined by the data source and may be dependent implicitly or explicitly on the data in the new record. For example, if a sort or a join is done within the query that populates the **Recordset**, the new record will be inserted in the appropriate place within the **Recordset**. If ordering is not explicitly specified when creating the **Recordset**, changes in the data source implementation may cause the ordering of the returned rows to change inadvertently. In addition, the sorting, filtering, and editing functions of the **Recordset** can affect the order and possibly which rows in the recordset will be visible.  
  
 Therefore, **MoveNext**, **MovePrevious**, **MoveFirst**, **MoveLast**, and **Move** are all sensitive to other operations performed on the same **Recordset**. ADO will always try to maintain your current position until you explicitly move it, but sometimes, intervening changes make it difficult to understand the effects of a subsequent move. For example, if you call **MoveFirst** to position on the first row of a sorted **Recordset** and you change the sort from ascending order to descending order, you are still on the same row - but now it is the last row in the **Recordset**. **MoveFirst** will take you to a different row (the new first row).  
  
 As another example, if you are positioned on a particular row in the middle of a **Recordset** and you call **Delete** and then call **MoveNext**, you are now on the record immediately following the deleted record. But calling **MovePrevious** makes the record preceding the one you deleted the current record, because the deleted record is no longer counted in the active membership of the **Recordset**.  
  
 It is particularly difficult to define consistent move semantics across all providers for methods that move relative to the current record - **MovePrevious**, **MoveNext**, and **Move** - in the face of changing data in the current record. For example, if you are working with a sorted, filtered **Recordset**, and you change the data in the current record so that it would precede all other records, but your changed data also no longer matches the filter, it is not clear where a **MoveNext** operation should take you. The safest conclusion is that relative movement within a **Recordset** is riskier than absolute movement (such as using **MoveFirst** or **MoveLast**) when the data is changing while records are being edited, added, or deleted. Sorting and filtering should be based on a primary key or ID, because this type of value should not change.