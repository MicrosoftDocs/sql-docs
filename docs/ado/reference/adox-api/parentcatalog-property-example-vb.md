---
title: "ParentCatalog Property Example (VB)"
description: "ParentCatalog Property Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ParentCatalog property [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# ParentCatalog Property Example (VB)
The following code demonstrates how to use the [ParentCatalog](./parentcatalog-property-adox.md) property to access a provider-specific property prior to appending a table to a catalog. The property is **AutoIncrement**, which creates an AutoIncrement field in a Microsoft Jet database.  
  
```  
' BeginCreateAutoIncrColumnVB  
Sub Main()  
    On Error GoTo CreateAutoIncrColumnError  
  
    Dim cnn As New ADODB.Connection  
    Dim cat As New ADOX.Catalog  
    Dim tbl As New ADOX.Table  
  
    cnn.Open "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
    Set cat.ActiveConnection = cnn  
  
    With tbl  
        .Name = "MyContacts"  
        Set .ParentCatalog = cat  
        ' Create fields and append them to the new Table object.  
        .Columns.Append "ContactId", adInteger  
        ' Make the ContactId column and auto incrementing column  
        .Columns("ContactId").Properties("AutoIncrement") = True  
        .Columns.Append "CustomerID", adVarWChar  
        .Columns.Append "FirstName", adVarWChar  
        .Columns.Append "LastName", adVarWChar  
        .Columns.Append "Phone", adVarWChar, 20  
        .Columns.Append "Notes", adLongVarWChar  
    End With  
  
    cat.Tables.Append tbl  
    Debug.Print "Table 'MyContacts' is added."  
  
    ' Delete the table as this is a demonstration.  
    cat.Tables.Delete tbl.Name  
    Debug.Print "Table 'MyContacts' is delete."  
  
    'Clean up  
    cnn.Close  
    Set cat = Nothing  
    Set tbl = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
CreateAutoIncrColumnError:  
  
    Set cat = Nothing  
    Set tbl = Nothing  
  
    If Not cnn Is Nothing Then  
        If cnn.State = adStateOpen Then cnn.Close  
    End If  
    Set cnn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
  
End Sub  
' EndCreateAutoIncrColumnVB  
```  
  
## See Also  
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Tables)](./append-method-adox-tables.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Column Object (ADOX)](./column-object-adox.md)   
 [Columns Collection (ADOX)](./columns-collection-adox.md)   
 [Name Property (ADOX)](./name-property-adox.md)   
 [ParentCatalog Property (ADOX)](./parentcatalog-property-adox.md)   
 [Table Object (ADOX)](./table-object-adox.md)   
 [Type Property (Column) (ADOX)](./type-property-column-adox.md)