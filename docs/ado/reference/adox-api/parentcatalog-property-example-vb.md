---
title: "ParentCatalog Property Example (VB) | Microsoft Docs"
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
  - "ParentCatalog property [ADOX], Visual Basic example"
ms.assetid: 448bc850-7584-4c5f-89f3-5f4fee88b259
author: MightyPen
ms.author: genemi
manager: craigg
---
# ParentCatalog Property Example (VB)
The following code demonstrates how to use the [ParentCatalog](../../../ado/reference/adox-api/parentcatalog-property-adox.md) property to access a provider-specific property prior to appending a table to a catalog. The property is **AutoIncrement**, which creates an AutoIncrement field in a Microsoft Jet database.  
  
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
 [Append Method (ADOX Columns)](../../../ado/reference/adox-api/append-method-adox-columns.md)   
 [Append Method (ADOX Tables)](../../../ado/reference/adox-api/append-method-adox-tables.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Column Object (ADOX)](../../../ado/reference/adox-api/column-object-adox.md)   
 [Columns Collection (ADOX)](../../../ado/reference/adox-api/columns-collection-adox.md)   
 [Name Property (ADOX)](../../../ado/reference/adox-api/name-property-adox.md)   
 [ParentCatalog Property (ADOX)](../../../ado/reference/adox-api/parentcatalog-property-adox.md)   
 [Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)   
 [Type Property (Column) (ADOX)](../../../ado/reference/adox-api/type-property-column-adox.md)
