---
title: "Catalog ActiveConnection Property Example (VB) | Microsoft Docs"
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
  - "ActiveConnection property [ADOX], Visual Basic example"
ms.assetid: bb3274b1-764d-43a7-a49f-ef55680ecd26
author: MightyPen
ms.author: genemi
manager: craigg
---
# Catalog ActiveConnection Property Example (VB)
Setting the [ActiveConnection](../../../ado/reference/adox-api/activeconnection-property-adox.md) property to a valid, open connection "opens" the catalog. From an open catalog, you can access the schema objects contained within that catalog.  
  
```  
' BeginOpenConnectionVB  
Sub Main()  
    On Error GoTo OpenConnectionError  
  
    Dim cnn As New ADODB.Connection  
    Dim cat As New ADOX.Catalog  
  
    cnn.Open "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source= 'Northwind.mdb';"  
    Set cat.ActiveConnection = cnn  
    Debug.Print cat.Tables(0).Type  
  
    'Clean up  
    cnn.Close  
    Set cat = Nothing  
    Set cnn = Nothing  
    Exit Sub  
  
OpenConnectionError:  
  
    Set cat = Nothing  
  
    If Not cnn Is Nothing Then  
        If cnn.State = adStateOpen Then cnn.Close  
    End If  
    Set cnn = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndOpenConnectionVB  
```  
  
 Setting the **ActiveConnection** property to a valid connection string also "opens" the catalog.  
  
```  
Attribute VB_Name = "Catalog"  
```  
  
## See Also  
 [ActiveConnection Property (ADOX)](../../../ado/reference/adox-api/activeconnection-property-adox.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)   
 [Tables Collection (ADOX)](../../../ado/reference/adox-api/tables-collection-adox.md)   
 [Type Property (Table) (ADOX)](../../../ado/reference/adox-api/type-property-table-adox.md)
