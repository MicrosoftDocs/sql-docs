---
title: "BOF, EOF, and Bookmark Properties Example (VB)"
description: "BOF, EOF, and Bookmark Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "BOF property [ADO], Visual Basic example"
  - "Bookmark property [ADO], Visual Basic example"
  - "EOF property [ADO], Visual Basic example"
dev_langs:
  - "VB"
---
# BOF, EOF, and Bookmark Properties Example (VB)
This example uses the [BOF](./bof-eof-properties-ado.md) and [EOF](./bof-eof-properties-ado.md) properties to display a message if a user tries to move past the first or last record of a [Recordset](./recordset-object-ado.md). It uses the [Bookmark](./bookmark-property-ado.md) property to let the user flag a record in a **Recordset** and return to it later.  
  
```  
'BeginBOFVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    'recordset and connection variables  
    Dim Cnxn As ADODB.Connection  
    Dim rstPublishers As ADODB.Recordset  
    Dim strCnxn As String  
    Dim strSQLPubs As String  
     'record variables  
    Dim strMessage As String  
    Dim intCommand As Integer  
    Dim varBookmark As Variant  
  
     ' open connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
     ' Open recordset and use client cursor  
     ' to enable AbsolutePosition property  
    Set rstPublishers = New ADODB.Recordset  
    strSQLPubs = "SELECT pub_id, pub_name FROM publishers ORDER BY pub_name"  
    rstPublishers.Open strSQLPubs, strCnxn, adUseClient, adOpenStatic, adCmdText  
  
    rstPublishers.MoveFirst  
    Do Until rstPublishers.EOF  
        ' Display information about current record  
        ' and get user input  
        strMessage = "Publisher: " & rstPublishers!pub_name & _  
            vbCr & "(record " & rstPublishers.AbsolutePosition & _  
            " of " & rstPublishers.RecordCount & ")" & vbCr & vbCr & _  
            "Enter command:" & vbCr & _  
            "[1 - next / 2 - previous /" & vbCr & _  
            "3 - set bookmark / 4 - go to bookmark]"  
        intCommand = Val(InputBox(strMessage))  
  
        ' Check user input  
        Select Case intCommand  
            Case 1  
                ' Move forward trapping for EOF  
                rstPublishers.MoveNext  
                If rstPublishers.EOF Then  
                    MsgBox "Moving past the last record." & _  
                        vbCr & "Try again."  
                    rstPublishers.MoveLast  
                End If  
            Case 2  
                ' Move backward trapping for BOF  
                rstPublishers.MovePrevious  
                If rstPublishers.BOF Then  
                    MsgBox "Moving past the first record." & _  
                        vbCr & "Try again."  
                    rstPublishers.MoveFirst  
                End If  
            Case 3  
                ' Store the bookmark of the current record  
                varBookmark = rstPublishers.Bookmark  
            Case 4  
                ' Go to the record indicated by the stored bookmark  
                If IsEmpty(varBookmark) Then  
                    MsgBox "No Bookmark set!"  
                Else  
                    rstPublishers.Bookmark = varBookmark  
                End If  
            Case Else  
                Exit Do  
        End Select  
    Loop  
  
    ' clean up  
    rstPublishers.Close  
    Cnxn.Close  
    Set rstPublishers = Nothing  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not rstPublishers Is Nothing Then  
        If rstPublishers.State = adStateOpen Then rstPublishers.Close  
    End If  
    Set rstPublishers = Nothing  
  
    If Not Cnxn Is Nothing Then  
        If Cnxn.State = adStateOpen Then Cnxn.Close  
    End If  
    Set Cnxn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndBOFVB  
```  
  
 This example uses the **Bookmark** and [Filter](./filter-property.md) properties to create a limited view of the **Recordset**. Only records referenced by the array of bookmarks are accessible.  
  
```  
Attribute VB_Name = "BOF"  
```  
  
## See Also  
 [BOF, EOF Properties (ADO)](./bof-eof-properties-ado.md)   
 [Bookmark Property (ADO)](./bookmark-property-ado.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)