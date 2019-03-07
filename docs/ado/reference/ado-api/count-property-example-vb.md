---
title: "Count Property Example (VB) | Microsoft Docs"
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
  - "Count property [ADO], Visual Basic example"
ms.assetid: 35033910-623b-449a-a57d-baff3ed5ab8f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Count Property Example (VB)
This example demonstrates the [Count](../../../ado/reference/ado-api/count-property-ado.md) property with two collections in the ***Employee*** database. The property obtains the number of objects in each collection, and sets the upper limit for loops that enumerate these collections. Another way to enumerate these collections without using the **Count** property would be to use `For Each...Next` statements.  
  
```  
'BeginCountVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    ' recordset and connection variables  
    Dim rstEmployees As ADODB.Recordset  
    Dim Cnxn As ADODB.Connection  
    Dim strSQLEmployees As String  
    Dim strCnxn As String  
  
    Dim intLoop As Integer  
  
    ' Open a connection  
    Set Cnxn = New ADODB.Connection  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Northwind';Integrated Security='SSPI';"  
    Cnxn.Open strCnxn  
  
    ' Open recordset with data from Employee table  
    Set rstEmployees = New ADODB.Recordset  
    strSQLEmployees = "Employee"  
    'rstEmployees.Open strSQLEmployee, Cnxn, , , adCmdTable  
    rstEmployees.Open strSQLEmployees, Cnxn, adOpenForwardOnly, adLockReadOnly, adCmdTable  
    'the above two lines opening the recordset are identical as  
    'the default values for CursorType and LockType arguments match those specified  
  
    ' Print information about Fields collection  
    Debug.Print rstEmployees.Fields.Count & " Fields in Employee"  
  
    For intLoop = 0 To rstEmployees.Fields.Count - 1  
        Debug.Print "   " & rstEmployees.Fields(intLoop).Name  
    Next intLoop  
  
    ' Print information about Properties collection  
    Debug.Print rstEmployees.Properties.Count & " Properties in Employee"  
  
    For intLoop = 0 To rstEmployees.Properties.Count - 1  
        Debug.Print "   " & rstEmployees.Properties(intLoop).Name  
    Next intLoop  
  
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
'EndCountVB  
```  
  
## See Also  
 [Count Property (ADO)](../../../ado/reference/ado-api/count-property-ado.md)
