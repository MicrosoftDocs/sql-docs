---
title: "Create a New Foreign Key Relationship between Tables Example (VB) | Microsoft Docs"
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
  - "Key Type property [ADOX], Visual Basic example"
  - "RelatedTable property [ADOX], Visual Basic example"
  - "Keys Append method [ADOX], Visual Basic example"
  - "UpdateRule property [ADOX], Visual Basic example"
  - "RelatedColumn property [ADOX], Visual Basic example"
ms.assetid: 13b5b1c3-6af6-439e-bb65-976578ba6bc2
author: MightyPen
ms.author: genemi
manager: craigg
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
 [Append Method (ADOX Columns)](../../../ado/reference/adox-api/append-method-adox-columns.md)   
 [Append Method (ADOX Keys)](../../../ado/reference/adox-api/append-method-adox-keys.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Column Object (ADOX)](../../../ado/reference/adox-api/column-object-adox.md)   
 [Columns Collection (ADOX)](../../../ado/reference/adox-api/columns-collection-adox.md)   
 [Key Object (ADOX)](../../../ado/reference/adox-api/key-object-adox.md)   
 [Keys Collection (ADOX)](../../../ado/reference/adox-api/keys-collection-adox.md)   
 [Name Property (ADOX)](../../../ado/reference/adox-api/name-property-adox.md)   
 [RelatedColumn Property (ADOX)](../../../ado/reference/adox-api/relatedcolumn-property-adox.md)   
 [RelatedTable Property (ADOX)](../../../ado/reference/adox-api/relatedtable-property-adox.md)   
 [Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)   
 [Tables Collection (ADOX)](../../../ado/reference/adox-api/tables-collection-adox.md)   
 [Type Property (Key) (ADOX)](../../../ado/reference/adox-api/type-property-key-adox.md)   
 [UpdateRule Property (ADOX)](../../../ado/reference/adox-api/updaterule-property-adox.md)
