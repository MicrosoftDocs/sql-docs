---
title: "Views Collection, CommandText Property Example (VB) | Microsoft Docs"
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
  - "CommandText property [ADOX]"
  - "Views collection [ADOX], Visual Basic example"
ms.assetid: a05a0190-352d-44ff-9488-0c94e9fb656e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Views Collection, CommandText Property Example (VB)
The following code demonstrates how to use the [Command](../../../ado/reference/adox-api/command-property-adox.md) property to update the text of a view.  
  
```  
' BeginViewsCollectionVB  
Sub Main()  
    On Error GoTo ViewTextError  
  
    Dim cnn As New ADODB.Connection  
    Dim cat As New ADOX.Catalog  
    Dim cmd As New ADODB.Command  
  
    ' Open the connection.  
    cnn.Open _  
        "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Open the catalog.  
    Set cat.ActiveConnection = cnn  
  
    ' Get the command.  
    Set cmd = cat.Views("AllCustomers").Command  
  
    ' Update the CommandText of the command.  
    cmd.CommandText = _  
    "Select CustomerId, CompanyName, ContactName From Customers"  
  
    ' Update the view.  
    Set cat.Views("AllCustomers").Command = cmd  
  
    'Clean up.  
    cnn.Close  
    Set cat = Nothing  
    Set cmd = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
ViewTextError:  
  
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
' EndViewsCollectionVB  
```  
  
## See Also  
 [ActiveConnection Property (ADOX)](../../../ado/reference/adox-api/activeconnection-property-adox.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Command Property (ADOX)](../../../ado/reference/adox-api/command-property-adox.md)   
 [View Object (ADOX)](../../../ado/reference/adox-api/view-object-adox.md)   
 [Views Collection (ADOX)](../../../ado/reference/adox-api/views-collection-adox.md)
