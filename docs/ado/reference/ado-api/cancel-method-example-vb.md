---
title: "Cancel Method Example (VB) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Cancel method [ADO], Visual Basic example"
ms.assetid: 5c0530ad-68d0-4cba-b1af-9386d566c7c5
author: MightyPen
ms.author: genemi
manager: craigg
---
# Cancel Method Example (VB)
This example uses the [Cancel](../../../ado/reference/ado-api/cancel-method-ado.md) method to cancel a command executing on a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object if the connection is busy.  
  
```  
'BeginCancelVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    'recordset and connection variables  
    Dim Cnxn As ADODB.Connection  
    Dim strCnxn As String  
    Dim strCmdChange As String  
    Dim strCmdRestore As String  
     'record variables  
    Dim blnChanged As Boolean  
  
    ' Open a connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
    ' Define command strings  
    strCmdChange = "UPDATE titles SET type = 'self_help' WHERE type = 'psychology'"  
    strCmdRestore = "UPDATE titles SET type = 'psychology' " & _  
                     "WHERE type = 'self_help'"  
  
    ' Begin a transaction, then execute a command asynchronously  
    Cnxn.BeginTrans  
    Cnxn.Execute strCmdChange, , adAsyncExecute  
    ' do something else for a little while -  
    ' use i = 1 to 32000 to allow completion  
    Dim i As Integer  
    For i = 1 To 1000  
        i = i + i  
        Debug.Print i  
    Next i  
  
    ' If the command has NOT completed, cancel the execute and  
    ' roll back the transaction; otherwise, commit the transaction  
    If CBool(Cnxn.State And adStateExecuting) Then  
        Cnxn.Cancel  
        Cnxn.RollbackTrans  
        blnChanged = False  
        MsgBox "Update canceled."  
    Else  
        Cnxn.CommitTrans  
        blnChanged = True  
        MsgBox "Update complete."  
    End If  
  
    ' If the change was made, restore the data  
    ' because this is only a demo  
    If blnChanged Then  
        Cnxn.Execute strCmdRestore  
        MsgBox "Data restored."  
    End If  
  
    ' clean up  
    Cnxn.Close  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
    If Not Cnxn Is Nothing Then  
        If Cnxn.State = adStateOpen Then Cnxn.Close  
    End If  
    Set Cnxn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndCancelVB  
```  
  
## See Also  
 [Cancel Method (ADO)](../../../ado/reference/ado-api/cancel-method-ado.md)   
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)
