---
title: "Seek Method and Index Property Example (VB)"
description: "Seek Method and Index Property Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Seek method [ADO], Visual Basic example"
  - "index property [ADO]"
dev_langs:
  - "VB"
---
# Seek Method and Index Property Example (VB)
This example uses the [Recordset](./recordset-object-ado.md) object's [Seek](./seek-method.md) method and [Index](./index-property.md) property in conjunction with a given ***Employee ID***, to locate the employee's name in the ***Employees*** table of the Nwind.mdb database.  
  
``` VB
'BeginSeekVB  
Public Sub SeekX()  
    On Error GoTo ErrorHandler  
  
    ' To integrate this code replace the data source  
    ' in the connection string  
  
     'recordset and connection variables  
    Dim rstEmployees As ADODB.Recordset  
    Dim Cnxn As ADODB.Connection  
    Dim strCnxn As String  
    Dim strSQLEmployees As String  
  
    Dim strID As String  
    Dim strPrompt As String  
    strPrompt = "Enter an EmployeeID (e.g., 1 to 9)"  
  
    ' Open connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
                "Data Source='C:\Program Files\Microsoft Office XP\Office10\Samples\northwind.mdb';"  
    Cnxn.Open strCnxn  
  
     ' open recordset server-side for indexing  
    Set rstEmployees = New ADODB.Recordset  
    rstEmployees.CursorLocation = adUseServer  
    strSQLEmployees = "employees"  
    rstEmployees.Open strSQLEmployees, strCnxn, adOpenKeyset, adLockReadOnly, adCmdTableDirect  
  
    ' Does this provider support Seek and Index?  
    If rstEmployees.Supports(adIndex) And rstEmployees.Supports(adSeek) Then  
        rstEmployees.Index = "PrimaryKey"  
        ' Display all the employees  
            rstEmployees.MoveFirst  
            Do While rstEmployees.EOF = False  
                Debug.Print rstEmployees!EmployeeId; ": "; rstEmployees!FirstName; " "; _  
                    rstEmployees!LastName  
                rstEmployees.MoveNext  
            Loop  
  
    ' Prompt the user for an EmployeeID between 1 and 9  
          rstEmployees.MoveFirst  
          Do  
             strID = LCase(Trim(InputBox(strPrompt, "Seek Example")))  
             ' Quit if strID is a zero-length string (CANCEL, null, etc.)  
             If Len(strID) = 0 Then Exit Do  
             If Len(strID) = 1 And strID >= "1" And strID <= "9" Then  
                rstEmployees.Seek Array(strID), adSeekFirstEQ  
                If rstEmployees.EOF Then  
                   Debug.Print "Employee not found.", , "Seek Example"  
                   MsgBox "Employee not found."  
                Else  
                   Debug.Print strID; ": Employee='"; rstEmployees!FirstName; " "; _  
                      rstEmployees!LastName; "'"  
                   MsgBox "EmployeeID is: " + strID + "; Employee Name is: '" + rstEmployees!FirstName + " " + _  
                      rstEmployees!LastName + "'", , "Seek Example"  
                End If  
             Else  
                MsgBox "You entered a wrong EmployeeID, please enter a new ID."  
             End If  
          Loop  
    End If  
  
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
'EndSeekVB  
```  
  
## See Also  
 [Index Property](./index-property.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)   
 [Seek Method](./seek-method.md)
