---
title: "Views and Fields Collections Example (VB)"
description: "Views and Fields Collections Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Views collection [ADOX], Visual Basic example"
  - "Fields collection [ADOX]"
dev_langs:
  - "VB"
---
# Views and Fields Collections Example (VB)
The following code demonstrates how to use the [Command](./command-property-adox.md) property and the [Recordset](../ado-api/recordset-object-ado.md) object to retrieve field information for the view.  
  
```  
' BeginViewFieldsVB  
Sub ViewFields()  
    On Error GoTo ViewFieldsError  
  
    Dim cnn As New ADODB.Connection  
    Dim rst As New ADODB.Recordset  
    Dim fld As ADODB.Field  
    Dim cat As New ADOX.Catalog  
  
    ' Open the Connection  
    cnn.Open _  
        "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Open the catalog  
    Set cat.ActiveConnection = cnn  
  
    ' Set the Source for the Recordset  
    Set rst.Source = cat.Views("AllCustomers").Command  
  
    ' Retrieve Field information  
    rst.Fields.Refresh  
    For Each fld In rst.Fields  
        Debug.Print fld.Name & ":" & fld.Type  
    Next  
  
    'Clean up  
    cnn.Close  
    Set cat = Nothing  
    Set rst = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
ViewFieldsError:  
  
    If Not cnn Is Nothing Then  
        If cnn.State = adStateOpen Then cnn.Close  
    End If  
  
    Set cat = Nothing  
    Set rst = Nothing  
    Set cnn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
  
End Sub  
' EndViewFieldsVB  
```  
  
## See Also  
 [ActiveConnection Property (ADOX)](./activeconnection-property-adox.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Command Property (ADOX)](./command-property-adox.md)   
 [View Object (ADOX)](./view-object-adox.md)   
 [Views Collection (ADOX)](./views-collection-adox.md)