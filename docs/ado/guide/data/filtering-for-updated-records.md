---
title: "Filtering for Updated Records"
description: "Filtering for Updated Records"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "filtering for updated records [ADO]"
---
# Filtering for Updated Records
Before you call UpdateBatch, you can use the Recordset Filter property to view only those records that have been changed since the Recordset was opened or the last call to UpdateBatch. To do this, set Filter equal to adFilterPendingRecords to determine how many records will be updated, as shown in the code example in the next section.  
  
## Remarks  
 This example extends the previous UpdateBatch example by filtering the Recordset just before it calls the UpdateBatch, showing the user which records will change and allowing her to cancel the update (using the CancelBatch method).  
  
```  
'BeginFilterPend  
    '...  
    objRs1.Open strSQL, strConn, adOpenStatic, adLockBatchOptimistic, adCmdText  
  
    ' Change value of Phone field for first record in Recordset, saving value  
    ' for later restoration.  
    intId = objRs1("ShipperId")  
    sPhone = objRs1("Phone")  
  
    objRs1("Phone") = "(111) 555-1111"  
  
    'Add two new records  
    For i = 0 To 1  
        objRs1.AddNew  
        objRs1(1) = "New Shipper #" & CStr((i + 1))  
        objRs1(2) = "(nnn) 555-" & i & i & i & i  
    Next i  
  
    objRs1.Filter = adFilterPendingRecords  
  
    MsgBox "There are " & objRs1.RecordCount & " records pending.", _  
            , "Filter Pending"  
  
    ' Cancel the updates  
    objRs1.CancelBatch  
'EndFilterPend  
```  
  
## See Also  
 [Batch Mode](./batch-mode.md)