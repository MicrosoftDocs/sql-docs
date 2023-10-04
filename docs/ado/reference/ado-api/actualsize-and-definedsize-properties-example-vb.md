---
title: "ActualSize and DefinedSize Properties Example (VB)"
description: "ActualSize and DefinedSize Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "DefinedSize property [ADO], Visual Basic example"
  - "ActualSize property [ADO], Visual Basic example"
dev_langs:
  - "VB"
---
# ActualSize and DefinedSize Properties Example (VB)
This example uses the [ActualSize](./actualsize-property-ado.md) and [DefinedSize](./definedsize-property.md) properties to display the defined size and actual size of a field.  
  
```vb
'BeginActualSizeVB  
  
    'To integrate this code  
    'replace the data source and initial catalog values  
    'in the connection string  
  
Public Sub Main()  
    On Error GoTo ErrorHandler  
  
    'recordset and connection variables  
    Dim rstStores As ADODB.Recordset  
    Dim SQLStores As String  
    Dim strCnxn As String  
     'record variables  
    Dim strMessage As String  
  
    ' Open a recordset for the Stores table  
    strCnxn = "Provider='sqloledb';Data Source='MySqlServer';" & _  
        "Initial Catalog='Northwind';Integrated Security='SSPI';"  
    Set rstStores = New ADODB.Recordset  
  
    SQLStores = "Suppliers"  
    rstStores.Open SQLStores, strCnxn, adOpenForwardOnly, adLockReadOnly, adCmdTable  
    'the above two lines of code are identical as the default values for  
    'CursorType and LockType arguments match those indicated  
  
    ' Loop through the recordset displaying the contents  
    ' of the store_name field, the field's defined size,  
    ' and its actual size.  
    rstStores.MoveFirst  
  
    Do Until rstStores.EOF  
        strMessage = "Company name: " & rstStores!CompanyName & _  
        vbCrLf & "Defined size: " & _  
        rstStores!CompanyName.DefinedSize & _  
        vbCrLf & "Actual size: " & _  
        rstStores!CompanyName.ActualSize & vbCrLf  
  
        MsgBox strMessage, vbOKCancel, "ADO ActualSize Property (Visual Basic)"  
        rstStores.MoveNext  
    Loop  
  
    ' clean up  
    rstStores.Close  
    Set rstStores = Nothing  
    Exit Sub  
  
ErrorHandler:  
    ' clean up  
    If Not rstStores Is Nothing Then  
        If rstStores.State = adStateOpen Then rstStores.Close  
    End If  
    Set rstStores = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
'EndActualSizeVB  
```  
  
## See Also  
 [ActualSize Property (ADO)](./actualsize-property-ado.md)   
 [DefinedSize Property](./definedsize-property.md)   
 [Field Object](./field-object.md)