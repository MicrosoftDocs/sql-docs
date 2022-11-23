---
title: "Sending the Updates: UpdateBatch Method"
description: "Sending the Updates: UpdateBatch Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
---
# Sending the Updates: UpdateBatch Method
The following code opens a Recordset in batch mode by setting the LockType property to adLockBatchOptimistic and the CursorLocation to adUseClient. It adds two new records and changes the value of a field in an existing record, saving the original values, and then calls UpdateBatch to send the changes back to the data source.  
  
## Remarks  
  
```  
'BeginBatchUpdate  
    strConn = "Provider=SQLOLEDB;Initial Catalog=Northwind;" & _  
              "Data Source=MySQLServer;Integrated Security=SSPI;"  
  
    strSQL = "SELECT ShipperId, CompanyName, Phone FROM Shippers"  
  
    Set objRs1 = New ADODB.Recordset  
    objRs1.CursorLocation = adUseClient  
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
  
    ' Send the updates  
    objRs1.UpdateBatch  
'EndBatchUpdate  
```  
  
 If you are editing the current record or adding a new record when you call the UpdateBatch method, ADO will automatically call the Update method to save any pending changes to the current record before transmitting the batched changes to the provider.  
  
## See Also  
 [Batch Mode](../../../ado/guide/data/batch-mode.md)
