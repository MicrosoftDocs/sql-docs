---
title: "Columns and Tables Append Methods, Name Property Example (VB)"
description: "Columns and Tables Append Methods, Name Property Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Name property [ADOX], Visual Basic example"
  - "Append method [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# Columns and Tables Append Methods, Name Property Example (VB)
The following code demonstrates how to create a new table.  
  
```  
' BeginCreateTableVB  
Sub Main()  
    On Error GoTo CreateTableError  
  
    Dim tbl As New Table  
    Dim cat As New ADOX.Catalog  
  
    ' Open the Catalog.  
    cat.ActiveConnection = "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    tbl.Name = "MyTable"  
    tbl.Columns.Append "Column1", adInteger  
    tbl.Columns.Append "Column2", adInteger  
    tbl.Columns.Append "Column3", adVarWChar, 50  
    cat.Tables.Append tbl  
    Debug.Print "Table 'MyTable' is added."  
  
    'Delete the table as this is a demonstration.  
    cat.Tables.Delete tbl.Name  
    Debug.Print "Table 'MyTable' is deleted."  
  
    'Clean up  
    Set cat.ActiveConnection = Nothing  
    Set cat = Nothing  
    Set tbl = Nothing  
    Exit Sub  
  
CreateTableError:  
  
    Set cat = Nothing  
    Set tbl = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndCreateTableVB  
```  
  
## See Also  
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Tables)](./append-method-adox-tables.md)   
 [Column Object (ADOX)](./column-object-adox.md)   
 [Columns Collection (ADOX)](./columns-collection-adox.md)   
 [Name Property (ADOX)](./name-property-adox.md)   
 [Table Object (ADOX)](./table-object-adox.md)   
 [Tables Collection (ADOX)](./tables-collection-adox.md)