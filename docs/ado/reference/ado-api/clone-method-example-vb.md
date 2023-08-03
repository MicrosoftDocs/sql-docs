---
title: "Clone Method Example (VB)"
description: "Clone Method Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Clone method [ADO], Visual Basic example"
dev_langs:
  - "VB"
---
# Clone Method Example (VB)
This example uses the [Clone](./clone-method-ado.md) method to create copies of a [Recordset](./recordset-object-ado.md) and then lets the user position the record pointer of each copy independently.  
  
```  
'BeginCloneVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    'recordset array and connection variables  
    Dim arstStores(1 To 3) As ADODB.Recordset  
    Dim Cnxn As ADODB.Connection  
    Dim strSQLStore As String  
    Dim strCnxn As String  
     'record variables  
    Dim intLoop As Integer  
    Dim strMessage As String  
    Dim strFind As String  
  
    ' Open a connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
    ' Open recordset as a static cursor type recordset  
    Set arstStores(1) = New ADODB.Recordset  
    strSQLStore = "SELECT stor_name FROM Stores ORDER BY stor_name"  
    arstStores(1).Open strSQLStore, strCnxn, adOpenStatic, adLockBatchOptimistic, adCmdText  
  
    ' Create two clones of the original Recordset  
    Set arstStores(2) = arstStores(1).Clone  
    Set arstStores(3) = arstStores(1).Clone  
  
    ' Loop through the array so that on each pass the user  
    ' is searching a different copy of the same Recordset  
    Do  
        For intLoop = 1 To 3  
             ' Ask for search string while showing where  
             ' the current record pointer is for each Recordset  
            strMessage = _  
                "Recordsets from stores table:" & vbCr & _  
                "  1 - Original - Record pointer at " & arstStores(1)!stor_name & vbCr & _  
                "  2 - Clone - Record pointer at " & arstStores(2)!stor_name & vbCr & _  
                "  3 - Clone - Record pointer at " & arstStores(3)!stor_name & vbCr & _  
                "Enter search string for #" & intLoop & ":"  
  
            strFind = Trim(InputBox(strMessage))  
             ' make sure something was entered, if not then EXIT loop  
            If strFind = "" Then Exit Do  
  
             ' otherwise locate the record from the entered string  
            arstStores(intLoop).Filter = "stor_name = '" & strFind & "'"  
  
             'if there's no match, jump to the last record  
            If arstStores(intLoop).EOF Then  
                arstStores(intLoop).Filter = adFilterNone  
                arstStores(intLoop).MoveLast  
            Else  
                MsgBox "Found " & strFind  
            End If  
        Next intLoop  
    Loop  
  
    ' clean up  
    arstStores(1).Close  
    arstStores(2).Close  
    arstStores(3).Close  
    Cnxn.Close  
    Set arstStores(1) = Nothing  
    Set arstStores(2) = Nothing  
    Set arstStores(3) = Nothing  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not arstStores(1) Is Nothing Then  
        If arstStores(1).State = adStateOpen Then arstStores(1).Close  
    End If  
    Set arstStores(1) = Nothing  
    If Not arstStores(2) Is Nothing Then  
        If arstStores(2).State = adStateOpen Then arstStores(2).Close  
    End If  
    Set arstStores(2) = Nothing  
    If Not arstStores(3) Is Nothing Then  
        If arstStores(3).State = adStateOpen Then arstStores(3).Close  
    End If  
    Set arstStores(3) = Nothing  
  
    If Not Cnxn Is Nothing Then  
        If Cnxn.State = adStateOpen Then Cnxn.Close  
    End If  
    Set Cnxn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndCloneVB  
```  
  
## See Also  
 [Clone Method (ADO)](./clone-method-ado.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)