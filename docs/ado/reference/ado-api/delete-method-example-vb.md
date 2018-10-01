---
title: "Delete Method Example (VB) | Microsoft Docs"
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
  - "Delete method [ADO], Visual Basic example"
ms.assetid: 0c80e71b-9e3f-4d05-ab2a-9e78798dad88
author: MightyPen
ms.author: genemi
manager: craigg
---
# Delete Method Example (VB)
This example uses the [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md) method to remove a specified record from a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
```  
'BeginDeleteVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    Dim rstRoySched As ADODB.Recordset  
    Dim Cnxn As ADODB.Connection  
    Dim strCnxn As String  
    Dim strSQLRoySched As String  
  
    Dim strMsg As String  
    Dim strTitleID As String  
    Dim intLoRange As Integer  
    Dim intHiRange As Integer  
    Dim intRoyalty As Integer  
  
     ' open connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
    ' open RoySched table with cursor client-side  
    Set rstRoySched = New ADODB.Recordset  
    rstRoySched.CursorLocation = adUseClient  
    rstRoySched.CursorType = adOpenStatic  
    rstRoySched.LockType = adLockBatchOptimistic  
    rstRoySched.Open "SELECT * FROM roysched WHERE royalty = 20", strCnxn, , , adCmdText  
  
    ' Prompt for a record to delete  
    strMsg = "Before delete there are " & rstRoySched.RecordCount & _  
       " titles with 20 percent royalty:" & vbCr & vbCr  
  
    Do While Not rstRoySched.EOF  
       strMsg = strMsg & rstRoySched!title_id & vbCr  
       rstRoySched.MoveNext  
    Loop  
  
    strMsg = strMsg & vbCr & vbCr & "Enter the ID of a record to delete:"  
    strTitleID = UCase(InputBox(strMsg))  
  
    If strTitleID = "" Then  
        Err.Raise 1, , "You didn't enter any value for the record ID."  
    End If  
  
    ' Move to the record and save data so it can be restored  
    rstRoySched.Filter = "title_id = '" & strTitleID & "'"  
  
    If rstRoySched.RecordCount < 1 Then  
        Err.Raise 1, , "There is no record for the record ID you entered."  
    End If  
  
    intLoRange = rstRoySched!lorange  
    intHiRange = rstRoySched!hirange  
    intRoyalty = rstRoySched!royalty  
  
    ' Delete the record  
    rstRoySched.Delete  
    rstRoySched.UpdateBatch  
  
    ' Show the results  
    rstRoySched.Filter = adFilterNone  
    rstRoySched.Requery  
    strMsg = ""  
    strMsg = "After delete there are " & rstRoySched.RecordCount & _  
       " titles with 20 percent royalty:" & vbCr & vbCr  
    Do While Not rstRoySched.EOF  
        strMsg = strMsg & rstRoySched!title_id & vbCr  
        rstRoySched.MoveNext  
    Loop  
    MsgBox strMsg  
  
    ' Restore the data because this is a demonstration  
    rstRoySched.AddNew  
    rstRoySched!title_id = strTitleID  
    rstRoySched!lorange = intLoRange  
    rstRoySched!hirange = intHiRange  
    rstRoySched!royalty = intRoyalty  
    rstRoySched.UpdateBatch  
  
    ' clean up  
    rstRoySched.Close  
    Set rstRoySched = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not rstRoySched Is Nothing Then  
        If rstRoySched.State = adStateOpen Then rstRoySched.Close  
    End If  
    Set rstRoySched = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndDeleteVB  
```  
  
## See Also  
 [Delete Method (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
