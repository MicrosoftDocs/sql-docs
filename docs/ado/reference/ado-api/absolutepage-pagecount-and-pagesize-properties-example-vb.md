---
title: "AbsolutePage, PageCount, and PageSize Properties Example (VB)"
description: "AbsolutePage, PageCount, and PageSize Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "PageCount property [ADO], Visual Basic example"
  - "AbsolutePage property [ADO], Visual Basic example"
  - "PageSize property [ADO], Visual Basic example"
dev_langs:
  - "VB"
---
# AbsolutePage, PageCount, and PageSize Properties Example (VB)
```  
'BeginAbsolutePageVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    'recordset and connection variables  
    Dim rstEmployees As ADODB.Recordset  
    Dim Cnxn As ADODB.Connection  
    Dim strCnxn As String  
    Dim strSQL As String  
        'record variables  
    Dim strMessage As String  
    Dim intPage As Integer  
    Dim intPageCount As Integer  
    Dim intRecord As Integer  
  
    'Open connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Pubs';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
    ' Open employee recordset  
    ' Use client cursor to enable AbsolutePosition property  
    Set rstEmployees = New ADODB.Recordset  
    strSQL = "employee"  
    rstEmployees.Open strSQL, strCnxn, adUseClient, adLockReadOnly, adCmdTable  
  
    ' Display names and hire dates, five records at a time  
    rstEmployees.PageSize = 5  
    intPageCount = rstEmployees.PageCount  
    For intPage = 1 To intPageCount  
        rstEmployees.AbsolutePage = intPage  
        strMessage = ""  
        For intRecord = 1 To rstEmployees.PageSize  
            strMessage = strMessage & _  
                rstEmployees!fname & " " & _  
                rstEmployees!lname & " " & _  
                rstEmployees!hire_date & vbCr  
            rstEmployees.MoveNext  
            If rstEmployees.EOF Then Exit For  
        Next intRecord  
        MsgBox strMessage  
    Next intPage  
  
    ' clean up  
    rstEmployees.Close  
    Cnxn.Close  
    Set rstEmployees = Nothing  
    Set Cnxn = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not rstEmployees Is Nothing Then  
        If rstEmployees.State = adStateOpen Then rstEmployees.Close  
    End If  
    Set rstEmployees = Nothing  
  
    If Not Cnxn Is Nothing Then  
        If Cnxn.State = adStateOpen Then Cnxn.Close  
    End If  
    Set Cnxn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndAbsolutePageVB  
```  
  
## See Also  
 [AbsolutePage Property (ADO)](./absolutepage-property-ado.md)   
 [PageCount Property (ADO)](./pagecount-property-ado.md)   
 [PageSize Property (ADO)](./pagesize-property-ado.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)