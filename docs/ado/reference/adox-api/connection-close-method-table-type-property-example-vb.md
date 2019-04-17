---
title: "Connection Close Method, Table Type Property Example (VB) | Microsoft Docs"
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
  - "Close method [ADOX], Visual Basic example"
  - "Type property [ADOX], Visual Basic example"
ms.assetid: f88e7a3b-19ed-46e2-b2ce-3b611d9b8166
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connection Close Method, Table Type Property Example (VB)
Setting the [ActiveConnection](../../../ado/reference/adox-api/activeconnection-property-adox.md) property to **Nothing** should close the connection to the catalog. Associated collections will be empty. Any objects that were created from schema objects in the catalog will be orphaned. Any properties on those objects that have been cached will still be available, but an attempt to read properties that requires a call to the provider will fail.  
  
```  
' BeginCloseConnectionVB  
Sub Main()  
    On Error GoTo CloseConnectionByNothingError  
  
    Dim cnn As New ADODB.Connection  
    Dim cat As New ADOX.Catalog  
    Dim tbl As ADOX.Table  
  
    cnn.Open "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source= 'Northwind.mdb';"  
    Set cat.ActiveConnection = cnn  
    Set tbl = cat.Tables(0)  
    Debug.Print tbl.Type    ' Cache tbl.Type info  
    Set cat.ActiveConnection = Nothing  
    Debug.Print tbl.Type    ' tbl is orphaned  
    ' Previous line will succeed if this info was cached.  
    Debug.Print tbl.Columns(0).DefinedSize  
    ' Previous line will fail if this info has not been cached.  
  
    'Clean up.  
    cnn.Close  
    Set cat = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
CloseConnectionByNothingError:  
    Set cat = Nothing  
  
    If Not cnn Is Nothing Then  
        If cnn.State = adStateOpen Then cnn.Close  
    End If  
    Set cnn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndCloseConnectionVB  
```  
  
 Closing a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object that was used to open the catalog should have the same effect as setting the **ActiveConnection** property to **Nothing**.  
  
```  
Attribute VB_Name = "Connection"  
```  
  
## See Also  
 [ActiveConnection Property (ADOX)](../../../ado/reference/adox-api/activeconnection-property-adox.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Column Object (ADOX)](../../../ado/reference/adox-api/column-object-adox.md)   
 [Columns Collection (ADOX)](../../../ado/reference/adox-api/columns-collection-adox.md)   
 [Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)   
 [Tables Collection (ADOX)](../../../ado/reference/adox-api/tables-collection-adox.md)   
 [Type Property (Table) (ADOX)](../../../ado/reference/adox-api/type-property-table-adox.md)
