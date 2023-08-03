---
title: "Create a New Foreign Key Relationship between Tables Example (VB)"
description: "Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Key Type property [ADOX], Visual Basic example"
  - "RelatedTable property [ADOX], Visual Basic example"
  - "Keys Append method [ADOX], Visual Basic example"
  - "UpdateRule property [ADOX], Visual Basic example"
  - "RelatedColumn property [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)
The following code demonstrates how to create a new foreign key relationship between two existing tables named **Customers** and **Orders**.  
  
```  
' BeginCreateKeyVB  
Sub Main()  
    On Error GoTo CreateKeyError  
  
    Dim kyForeign As New ADOX.Key  
    Dim cat As New ADOX.Catalog  
  
    ' Connect to the catalog.  
    cat.ActiveConnection = "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Define the foreign key.  
    kyForeign.Name = "CustOrder"  
    kyForeign.Type = adKeyForeign  
    kyForeign.RelatedTable = "Customers"  
    kyForeign.Columns.Append "CustomerId"  
    kyForeign.Columns("CustomerId").RelatedColumn = "CustomerId"  
    kyForeign.UpdateRule = adRICascade  
  
    ' Append the foreign key to the keys collection.  
    cat.Tables("Orders").Keys.Append kyForeign  
  
    'Delete the key t demonstrate the Delete method.  
    cat.Tables("Orders").Keys.Delete kyForeign.Name  
  
    'Clean up.  
    Set cat.ActiveConnection = Nothing  
    Set cat = Nothing  
    Set kyForeign = Nothing  
    Exit Sub  
  
CreateKeyError:  
    Set cat = Nothing  
    Set kyForeign = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
  
End Sub  
' EndCreateKeyVB  
```  
  
## See Also  
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Keys)](./append-method-adox-keys.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Column Object (ADOX)](./column-object-adox.md)   
 [Columns Collection (ADOX)](./columns-collection-adox.md)   
 [Key Object (ADOX)](./key-object-adox.md)   
 [Keys Collection (ADOX)](./keys-collection-adox.md)   
 [Name Property (ADOX)](./name-property-adox.md)   
 [RelatedColumn Property (ADOX)](./relatedcolumn-property-adox.md)   
 [RelatedTable Property (ADOX)](./relatedtable-property-adox.md)   
 [Table Object (ADOX)](./table-object-adox.md)   
 [Tables Collection (ADOX)](./tables-collection-adox.md)   
 [Type Property (Key) (ADOX)](./type-property-key-adox.md)   
 [UpdateRule Property (ADOX)](./updaterule-property-adox.md)