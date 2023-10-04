---
title: "Clustered Property Example (VB)"
description: "Clustered Property Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Clustered property [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# Clustered Property Example (VB)
This example demonstrates the [Clustered](./clustered-property-adox.md) property of an [Index](./index-object-adox.md). Note that Microsoft Jet databases do not support clustered indexes, so this example will return **False** for the **Clustered** property of all indexes in the **Northwind** database.  
  
```  
' BeginClusteredVB  
Sub Main()  
    On Error GoTo ClusteredXError  
  
    Dim cnn As New ADODB.Connection  
    Dim cat As New ADOX.Catalog  
    Dim tblLoop As ADOX.Table  
    Dim idxLoop As ADOX.Index  
    Dim strCnn As String  
  
    strCnn = "Provider='SQLOLEDB';Data Source='MySqlServer';Initial Catalog='pubs';" & _  
        "Integrated Security='SSPI';"  
    ' Connect to the catalog.  
    cnn.Open strCnn  
    cat.ActiveConnection = cnn  
  
    ' Enumerate the tables.  
    For Each tblLoop In cat.Tables  
        'Enumerate the indexes.  
        For Each idxLoop In tblLoop.Indexes  
            Debug.Print tblLoop.Name & " " & _  
                idxLoop.Name & " " & idxLoop.Clustered  
        Next idxLoop  
    Next tblLoop  
  
    'Clean up.  
    cnn.Close  
    Set cat = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
ClusteredXError:  
  
    Set cat = Nothing  
  
    If Not cnn Is Nothing Then  
        If cnn.State = adStateOpen Then cnn.Close  
    End If  
    Set cnn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndClusteredVB  
```  
  
## See Also  
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Clustered Property (ADOX)](./clustered-property-adox.md)   
 [Index Object (ADOX)](./index-object-adox.md)   
 [Table Object (ADOX)](./table-object-adox.md)