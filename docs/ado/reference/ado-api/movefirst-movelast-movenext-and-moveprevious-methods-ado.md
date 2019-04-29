---
title: "MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::MoveLast"
  - "Recordset15::MoveNext"
  - "Recordset15::raw_MoveNext"
  - "Recordset15::raw_MovePrevious"
  - "Recordset15::MoveFirst"
  - "Recordset15::raw_MoveLast"
  - "Recordset15::MovePrevious"
  - "Recordset15::raw_MoveFirst"
helpviewer_keywords: 
  - "MoveNext method [ADO]"
  - "MoveLast method [ADO]"
  - "MoveFirst method [ADO]"
  - "MovePrevious method [ADO]"
ms.assetid: a61a01a7-5b33-4150-9126-21dfa63654cb
author: MightyPen
ms.author: genemi
manager: craigg
---
# MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (ADO)
Moves to the first, last, next, or previous record in a specified [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object and makes that record the current record.  
  
## Syntax  
  
```  
  
recordset.{MoveFirst | MoveLast | MoveNext | MovePrevious}  
```  
  
## Remarks  
 Use the **MoveFirst** method to move the current record position to the first record in the **Recordset**.  
  
 Use the **MoveLast** method to move the current record position to the last record in the **Recordset**. The **Recordset** object must support bookmarks or backward cursor movement; otherwise, the method call will generate an error.  
  
 A call to either **MoveFirst** or **MoveLast** when the **Recordset** is empty (both **BOF** and **EOF** are True) generates an error.  
  
 Use the **MoveNext** method to move the current record position one record forward (toward the bottom of the **Recordset**). If the last record is the current record and you call the **MoveNext** method, ADO sets the current record to the position after the last record in the **Recordset** ([EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) is **True**). An attempt to move forward when the **EOF** property is already **True** generates an error.  
  
 In ADO 2.5 and later, when the **Recordset** has been filtered or sorted and the data of the current record is changed, calling the **MoveNext** method moves the cursor two records forward from the current record. This is because when the current record is changed, the next record becomes the new current record. Calling **MoveNext** after the change moves the cursor one record forward from the new current record. This is different from the behavior in ADO 2.1 and earlier. In these earlier versions, changing the data of a current record in the sorted or filtered **Recordset** does not change the position of the current record, and **MoveNext** moves the cursor to the next record immediately after the current record.  
  
 Use the **MovePrevious** method to move the current record position one record backward (toward the top of the **Recordset**). The **Recordset** object must support bookmarks or backward cursor movement; otherwise, the method call will generate an error. If the first record is the current record and you call the **MovePrevious** method, ADO sets the current record to the position before the first record in the **Recordset** ([BOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) is **True**). An attempt to move backward when the **BOF** property is already **True** generates an error. If the **Recordset** object does not support either bookmarks or backward cursor movement, the **MovePrevious** method will generate an error.  
  
 If the **Recordset** is forward only and you want to support both forward and backward scrolling, you can use the [CacheSize](../../../ado/reference/ado-api/cachesize-property-ado.md) property to create a record cache that will support backward cursor movement through the [Move](../../../ado/reference/ado-api/move-method-ado.md) method. Because cached records are loaded into memory, you should avoid caching more records than is necessary. You can call the **MoveFirst** method in a forward-only **Recordset** object; doing so may cause the provider to re-execute the command that generated the **Recordset** object.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods Example (VB)](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-example-vb.md)   
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods Example (VBScript)](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-example-vbscript.md)   
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods Example (VC++)](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-example-vc.md)   
 [Move Method (ADO)](../../../ado/reference/ado-api/move-method-ado.md)   
 [MoveFirst, MoveLast, MoveNext, and MovePrevious Methods (RDS)](../../../ado/reference/rds-api/movefirst-movelast-movenext-and-moveprevious-methods-rds.md)   
 [MoveRecord Method (ADO)](../../../ado/reference/ado-api/moverecord-method-ado.md)
