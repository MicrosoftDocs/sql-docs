---
title: "Views Append Method Example (VB)"
description: "Views Append Method Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Append method [ADOX]"
dev_langs:
  - "VB"
---
# Views Append Method Example (VB)
The following code demonstrates how to use a [Command](../ado-api/command-object-ado.md) object and the [Views](./views-collection-adox.md) collection [Append](./append-method-adox-views.md) method to create a new view in the underlying data source.  
  
```  
' BeginCreateViewVB  
Sub Main()  
    On Error GoTo CreateViewError  
  
    Dim cmd As New ADODB.Command  
    Dim cat As New ADOX.Catalog  
  
    ' Open the Catalog  
    cat.ActiveConnection = _  
        "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Create the command representing the view.  
    cmd.CommandText = "Select * From Customers"  
  
    ' Create the new View  
    cat.Views.Append "AllCustomers", cmd  
  
    'Clean up  
    Set cat.ActiveConnection = Nothing  
    Set cat = Nothing  
    Set cmd = Nothing  
    Exit Sub  
  
CreateViewError:  
  
    Set cat = Nothing  
    Set cmd = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndCreateViewVB  
```  
  
## See Also  
 [ActiveConnection Property (ADOX)](./activeconnection-property-adox.md)   
 [Append Method (ADOX Views)](./append-method-adox-views.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [View Object (ADOX)](./view-object-adox.md)   
 [Views Collection (ADOX)](./views-collection-adox.md)