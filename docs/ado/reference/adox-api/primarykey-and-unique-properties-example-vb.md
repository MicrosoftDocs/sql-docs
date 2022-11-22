---
title: "PrimaryKey and Unique Properties Example (VB)"
description: "PrimaryKey and Unique Properties Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Unique property [ADOX], Visual Basic example"
  - "PrimaryKey property [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# PrimaryKey and Unique Properties Example (VB)
This example demonstrates the [PrimaryKey](./primarykey-property-adox.md) and [Unique](./unique-property-adox.md) properties of an [Index](./index-object-adox.md). The code creates a new table with two columns. The **PrimaryKey** and **Unique** properties are used to make one column the primary key for which duplicate values are not allowed.  
  
```  
' BeginPrimaryKeyVB  
Sub Main()  
    On Error GoTo PrimaryKeyXError  
  
    Dim catNorthwind As New ADOX.Catalog  
    Dim tblNew As New ADOX.Table  
    Dim idxNew As New ADOX.Index  
    Dim idxLoop As New ADOX.Index  
    Dim colLoop As New ADOX.Column  
  
    ' Connect the catalog  
    catNorthwind.ActiveConnection = "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Name new table  
    tblNew.Name = "NewTable"  
  
    ' Append a numeric and a text field to new table.  
    tblNew.Columns.Append "NumField", adInteger, 20  
    tblNew.Columns.Append "TextField", adVarWChar, 20  
  
    ' Append new Primary Key index on NumField column  
    ' to new table  
    idxNew.Name = "NumIndex"  
    idxNew.Columns.Append "NumField"  
    idxNew.PrimaryKey = True  
    idxNew.Unique = True  
    tblNew.Indexes.Append idxNew  
  
    ' Append an index on Textfield to new table.  
    ' Note the different technique: Specifying index and  
    ' column name as parameters of the Append method  
    tblNew.Indexes.Append "TextIndex", "TextField"  
  
    ' Append the new table  
    catNorthwind.Tables.Append tblNew  
  
    With tblNew  
        Debug.Print tblNew.Indexes.Count & " Indexes in " & _  
            tblNew.Name & " Table"  
  
        ' Enumerate Indexes collection.  
        For Each idxLoop In .Indexes  
            With idxLoop  
                Debug.Print "Index " & .Name  
                Debug.Print "   Primary key = " & .PrimaryKey  
                Debug.Print "   Unique = " & .Unique  
  
                ' Enumerate Columns collection of each Index  
                ' object.  
                Debug.Print "    Columns"  
                For Each colLoop In .Columns  
                    Debug.Print "       " & colLoop.Name  
                Next colLoop  
            End With  
        Next idxLoop  
  
    End With  
  
    ' Delete new table as this is a demonstration.  
    catNorthwind.Tables.Delete tblNew.Name  
  
    'Clean up  
    Set catNorthwind.ActiveConnection = Nothing  
    Set catNorthwind = Nothing  
    Set tblNew = Nothing  
    Set idxNew = Nothing  
    Set idxLoop = Nothing  
    Set colLoop = Nothing  
    Exit Sub  
  
PrimaryKeyXError:  
  
    Set catNorthwind = Nothing  
    Set tblNew = Nothing  
    Set idxNew = Nothing  
    Set idxLoop = Nothing  
    Set colLoop = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndPrimaryKeyVB  
```  
  
## See Also  
 [Index Object (ADOX)](./index-object-adox.md)   
 [PrimaryKey Property (ADOX)](./primarykey-property-adox.md)   
 [Unique Property (ADOX)](./unique-property-adox.md)