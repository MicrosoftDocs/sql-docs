---
title: "Command and CommandText Properties Example (VB)"
description: "Command and CommandText Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "CommandText property [ADOX], Visual Basic example"
  - "Command property [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# Command and CommandText Properties Example (VB)
The following code demonstrates how to use the [Command](./command-property-adox.md) property to update the text of a procedure.  
  
```  
' BeginProcedureTextVB  
Sub Main()  
    On Error GoTo ProcedureTextError  
  
    Dim cnn As New ADODB.Connection  
    Dim cat As New ADOX.Catalog  
    Dim cmd As New ADODB.Command  
  
    ' Open the connection.  
    cnn.Open "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Open the catalog.  
    Set cat.ActiveConnection = cnn  
  
    ' Get the command.  
    Set cmd = cat.Procedures("CustomerById").Command  
  
    ' Update the CommandText.  
    cmd.CommandText = "Select CustomerId, CompanyName, ContactName " & _  
        "From Customers " & _  
        "Where CustomerId = [CustId]"  
  
    ' Update the procedure.  
    Set cat.Procedures("CustomerById").Command = cmd  
  
    'Clean up.  
    cnn.Close  
    Set cat = Nothing  
    Set cmd = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
ProcedureTextError:  
    Set cat = Nothing  
    Set cmd = Nothing  
  
    If Not cnn Is Nothing Then  
        If cnn.State = adStateOpen Then cnn.Close  
    End If  
    Set cnn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndProcedureTextVB  
```  
  
## See Also  
 [ActiveConnection Property (ADOX)](./activeconnection-property-adox.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Command Property (ADOX)](./command-property-adox.md)   
 [Procedure Object (ADOX)](./procedure-object-adox.md)   
 [Procedures Collection (ADOX)](./procedures-collection-adox.md)