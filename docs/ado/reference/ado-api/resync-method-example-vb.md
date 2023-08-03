---
title: "Resync Method Example (VB)"
description: "Resync Method Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Resync method [ADO], Visual Basic example"
dev_langs:
  - "VB"
---
# Resync Method Example (VB)
This example demonstrates using the [Resync](./resync-method.md) method to refresh data in a static recordset.  
  
```  
'BeginResyncVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection strings  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    'connection and recordset variables  
    Dim Cnxn As ADODB.Connection  
    Dim rstTitles As ADODB.Recordset  
    Dim strCnxn As String  
    Dim strSQLTitles As String  
  
    ' Open connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
    ' Open recordset using object refs to set properties  
    ' that allow for updates to the database  
    Set rstTitles = New ADODB.Recordset  
    Set rstTitles.ActiveConnection = Cnxn  
    rstTitles.CursorType = adOpenKeyset  
    rstTitles.LockType = adLockOptimistic  
  
    strSQLTitles = "titles"  
    rstTitles.Open strSQLTitles  
  
    'rstTitles.Open strSQLTitles, Cnxn, adOpenKeyset, adLockPessimistic, adCmdTable  
    'the above line of code passes the same refs as the object refs listed above  
  
    ' Change the type of the first title in the recordset  
    rstTitles!Type = "database"  
  
    ' Display the results of the change  
    MsgBox "Before resync: " & vbCr & vbCr & _  
        "Title - " & rstTitles!Title & vbCr & _  
        "Type - " & rstTitles!Type  
  
    ' Resync with database and redisplay results  
    rstTitles.Resync  
    MsgBox "After resync: " & vbCr & vbCr & _  
        "Title - " & rstTitles!Title & vbCr & _  
        "Type - " & rstTitles!Type  
  
    ' clean up  
    rstTitles.CancelBatch  
    rstTitles.Close  
    Cnxn.Close  
    Set rstTitles = Nothing  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not rstTitles Is Nothing Then  
        If rstTitles.State = adStateOpen Then  
            rstTitles.CancelBatch  
            rstTitles.Close  
        End If  
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
'EndResyncVB  
```  
  
## See Also  
 [Recordset Object (ADO)](./recordset-object-ado.md)   
 [Resync Method](./resync-method.md)