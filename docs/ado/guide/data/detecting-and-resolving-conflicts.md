---
title: "Detecting and Resolving Conflicts"
description: "Detecting and Resolving Conflicts"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "conflicts [ADO], detecting and resolving"
  - "ADO, detecting and resolving conflicts"
---
# Detecting and Resolving Conflicts
If you are dealing with your Recordset in immediate mode, there is much less chance for concurrency problems to occur. On the other hand, if your application uses batch mode updating, there may be a good chance that one user will change a record before changes that were made by another user editing the same record are saved. In such a case, you will want your application to gracefully handle the conflict. It may be your wish that the last person to send an update to the server "wins." Or you may want to let the most recent user to decide which update should take precedence by providing him with a choice between the two conflicting values.  
  
 Whatever the case, ADO provides the UnderlyingValue and OriginalValue properties of the Field object to handle these types of conflicts. Use these properties in combination with the Resync method and Filter property of the Recordset.  
  
## Remarks  
 When ADO encounters a conflict during a batch update, a warning will be added to the Errors collection. Therefore, you should always check for errors immediately after you call BatchUpdate, and if you find them, begin testing the assumption that you have encountered a conflict. The first step is to set the Filter property on the Recordset equal to adFilterConflictingRecords. This limits the view on your Recordset to only those records that are in conflict. If the RecordCount property is equal to zero after this step, you know the error was raised by something other than a conflict.  
  
 When you call BatchUpdate, ADO and the provider are generating SQL statements to perform updates on the data source. Remember that certain data sources have limitations on which types of columns can be used in a WHERE clause.  
  
 Next, call the Resync method on the Recordset with the AffectRecords argument set equal to adAffectGroup and the ResyncValues argument set equal to adResyncUnderlyingValues. The Resync method updates the data in the current Recordset object from the underlying database. By using adAffectGroup, you are ensuring that only the records visible with the current filter setting, that is, only the conflicting records, are resynchronized with the database. This could make a significant performance difference if you are dealing with a large Recordset. By setting the ResyncValues argument to adResyncUnderlyingValues when calling Resync, you ensure that the UnderlyingValue property will contain the (conflicting) value from the database, that the Value property will maintain the value entered by the user, and that the OriginalValue property will hold the original value for the field (the value it had before the last successful UpdateBatch call was made). You can then use these values to resolve the conflict programmatically or require the user to select the value that will be used.  
  
 This technique is shown in the following code example. The example artificially creates a conflict by using a separate Recordset to change a value in the underlying table before UpdateBatch is called.  
  
```  
'BeginConflicts  
    strConn = "Provider=SQLOLEDB;Initial Catalog=Northwind;" & _  
              "Data Source=MySQLServer;Integrated Security=SSPI;"  
  
    strSQL = "SELECT * FROM Shippers WHERE ShipperID = 2"  
  
    'Open Rs and change a value  
    Set objRs1 = New ADODB.Recordset  
    Set objRs2 = New ADODB.Recordset  
    objRs1.CursorLocation = adUseClient  
    objRs1.Open strSQL, strConn, adOpenStatic, adLockBatchOptimistic, adCmdText  
    objRs1("Phone") = "(111) 555-1111"  
  
    'Introduce a conflict at the db...  
    objRs2.Open strSQL, strConn, adOpenKeyset, adLockOptimistic, adCmdText  
    objRs2("Phone") = "(999) 555-9999"  
    objRs2.Update  
    objRs2.Close  
    Set objRs2 = Nothing  
  
    On Error Resume Next  
    objRs1.UpdateBatch  
  
    If objRs1.ActiveConnection.Errors.Count <> 0 Then  
        Dim intConflicts As Integer  
  
        intConflicts = 0  
  
        objRs1.Filter = adFilterConflictingRecords  
  
        intConflicts = objRs1.RecordCount  
  
        'Resync so we can see the UnderlyingValue and offer user a choice.  
        'This sample only displays all three values and resets to original.  
        objRs1.Resync adAffectGroup, adResyncUnderlyingValues  
  
        If intConflicts > 0 Then  
            strMsg = "A conflict occurred with updates for " & intConflicts & _  
                     " record(s)." & vbCrLf & "The values will be restored" & _  
                     " to their original values." & vbCrLf & vbCrLf  
  
            objRs1.MoveFirst  
            While Not objRs1.EOF  
              strMsg = strMsg & "SHIPPER = " & objRs1("CompanyName") & vbCrLf  
              strMsg = strMsg & "Value = " & objRs1("Phone").Value & vbCrLf  
              strMsg = strMsg & "UnderlyingValue = " & _  
                                 objRs1("Phone").UnderlyingValue & vbCrLf  
              strMsg = strMsg & "OriginalValue = " & _  
                                 objRs1("Phone").OriginalValue & vbCrLf  
              strMsg = strMsg & vbCrLf & "Original value has been restored."  
  
              MsgBox strMsg, vbOKOnly, _  
                    "Conflict " & objRs1.AbsolutePosition & _  
                    " of " & intConflicts  
  
              objRs1("Phone").Value = objRs1("Phone").OriginalValue  
              objRs1.MoveNext  
            Wend  
  
            objRs1.UpdateBatch adAffectGroup  
        Else  
            'Other error occurred. Minimal handling in this example.  
             strMsg = "Errors occurred during the update. " & _  
                        objRs1.ActiveConnection.Errors(0).Number & " " & _  
                        objRs1.ActiveConnection.Errors(0).Description  
        End If  
  
        On Error GoTo 0  
    End If  
  
    objRs1.MoveFirst  
    objRs1.Close  
    Set objRs1 = Nothing  
'EndConflicts  
```  
  
 You can use the Status property of the current Record or of a specific Field to determine what kind of a conflict has occurred.  
  
 For detailed information about error handling, see [Error Handling](./error-handling.md).  
  
## See Also  
 [Batch Mode](./batch-mode.md)