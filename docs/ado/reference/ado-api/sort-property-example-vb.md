---
title: "Sort Property Example (VB) | Microsoft Docs"
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
  - "Sort property [ADO], Visual Basic example"
ms.assetid: fc2fd40b-65d6-4023-90a3-90c9a88ef6cf
author: MightyPen
ms.author: genemi
manager: craigg
---
# Sort Property Example (VB)
This example uses the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object's [Sort](../../../ado/reference/ado-api/sort-property.md) property to reorder the rows of a **Recordset** derived from the ***Authors*** table of the ***Pubs*** database. A secondary utility routine prints each row.  
  
```  
'BeginSortVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
     ' connection and recordset variables  
    Dim Cnxn As New ADODB.Connection  
    Dim rstAuthors As New ADODB.Recordset  
    Dim strCnxn As String  
    Dim strSQLAuthors As String  
  
    Dim strTitle As String  
  
    ' Open connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
    ' open client-side recordset to enable sort method  
    Set rstAuthors = New ADODB.Recordset  
    rstAuthors.CursorLocation = adUseClient  
    strSQLAuthors = "SELECT * FROM Authors"  
    rstAuthors.Open strSQLAuthors, Cnxn, adOpenStatic, adLockReadOnly, adCmdText  
  
     ' sort the recordset last name ascending  
    rstAuthors.Sort = "au_lname ASC, au_fname ASC"  
     ' show output  
    Debug.Print "Last Name Ascending:"  
    Debug.Print "First Name  Last Name" & vbCr  
  
    rstAuthors.MoveFirst  
    Do Until rstAuthors.EOF  
        Debug.Print rstAuthors!au_fname & " " & rstAuthors!au_lname  
        rstAuthors.MoveNext  
    Loop  
  
     ' sort the recordset last name descending  
    rstAuthors.Sort = "au_lname DESC, au_fname ASC"  
     ' show output  
    Debug.Print "Last Name Descending"  
    Debug.Print "First Name  Last Name" & vbCr  
  
    Do Until rstAuthors.EOF  
        Debug.Print rstAuthors!au_fname & " " & rstAuthors!au_lname  
        rstAuthors.MoveNext  
    Loop  
  
    ' clean up  
    rstAuthors.Close  
    Cnxn.Close  
    Set rstAuthors = Nothing  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not rstAuthors Is Nothing Then  
        If rstAuthors.State = adStateOpen Then rstAuthors.Close  
    End If  
    Set rstAuthors = Nothing  
  
    If Not Cnxn Is Nothing Then  
        If Cnxn.State = adStateOpen Then Cnxn.Close  
    End If  
    Set Cnxn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndSortVB  
```  
  
 This is the secondary utility routine that prints the given title, and the contents of the specified **Recordset**.  
  
```  
Attribute VB_Name = "Sort"  
```  
  
## See Also  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)   
 [Sort Property](../../../ado/reference/ado-api/sort-property.md)
