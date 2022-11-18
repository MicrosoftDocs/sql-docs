---
title: "UpdateBatch and CancelBatch Methods Example (VB)"
description: "UpdateBatch and CancelBatch Methods Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "UpdateBatch method [ADO]"
  - "CancelBatch method [ADO]"
dev_langs:
  - "VB"
---
# UpdateBatch and CancelBatch Methods Example (VB)
This example demonstrates the [UpdateBatch](./updatebatch-method.md) method in conjunction with the [CancelBatch](./cancelbatch-method-ado.md) method.  
  
```  
'BeginUpdateBatchVB  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
    'connection and recordset variables  
    Dim rstTitles As ADODB.Recordset  
    Dim Cnxn As ADODB.Connection  
    Dim strCnxn As String  
    Dim strSQLTitles As String  
     'record variables  
    Dim strTitle As String  
    Dim strMessage As String  
  
    ' Open connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
     ' open recordset for batch uodate  
    Set rstTitles = New ADODB.Recordset  
    strSQLTitles = "titles"  
    rstTitles.Open strSQLTitles, Cnxn, adOpenKeyset, adLockBatchOptimistic, adCmdTable  
  
    rstTitles.MoveFirst  
    ' Loop through recordset and ask user if she wants  
    ' to change the type for a specified title.  
    Do Until rstTitles.EOF  
        If Trim(rstTitles!Type) = "psychology" Then  
            strTitle = rstTitles!Title  
            strMessage = "Title: " & strTitle & vbCr & _  
               "Change type to self help?"  
  
            If MsgBox(strMessage, vbYesNo) = vbYes Then  
                rstTitles!Type = "self_help"  
            End If  
        End If  
  
        rstTitles.MoveNext  
    Loop  
  
    ' Ask the user if she wants to commit to all the  
    ' changes made above.  
    If MsgBox("Save all changes?", vbYesNo) = vbYes Then  
        rstTitles.UpdateBatch  
    Else  
        rstTitles.CancelBatch  
    End If  
  
    ' Print current data in recordset.  
    rstTitles.Requery  
    rstTitles.MoveFirst  
    Do While Not rstTitles.EOF  
        Debug.Print rstTitles!Title & " - " & rstTitles!Type  
        rstTitles.MoveNext  
    Loop  
  
    ' Restore original values because this is a demonstration.  
    rstTitles.MoveFirst  
    Do Until rstTitles.EOF  
        If Trim(rstTitles!Type) = "self_help" Then  
            rstTitles!Type = "psychology"  
        End If  
        rstTitles.MoveNext  
    Loop  
    rstTitles.UpdateBatch  
  
    ' clean up  
    rstTitles.Close  
    Cnxn.Close  
    Set rstTitles = Nothing  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not rstTitles Is Nothing Then  
        If rstTitles.State = adStateOpen Then rstTitles.Close  
    End If  
    Set rstTitles = Nothing  
  
    If Not Cnxn Is Nothing Then  
        If Cnxn.State = adStateOpen Then Cnxn.Close  
    End If  
    Set Cnxn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndUpdateBatchVB  
```  
  
## See Also  
 [CancelBatch Method (ADO)](./cancelbatch-method-ado.md)   
 [UpdateBatch Method](./updatebatch-method.md)