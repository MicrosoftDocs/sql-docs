---
title: "Views Refresh Method Example (VB) | Microsoft Docs"
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
  - "Refresh method [ADOX]"
ms.assetid: cdad2d66-6ade-40dc-9e74-e40cfa9bc127
author: MightyPen
ms.author: genemi
manager: craigg
---
# Views Refresh Method Example (VB)
The following code shows how to refresh the [Views](../../../ado/reference/adox-api/views-collection-adox.md) collection of a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md). This is required before [View](../../../ado/reference/adox-api/view-object-adox.md) objects from the **Catalog** can be accessed.  
  
```  
' BeginViewsRefreshVB  
Sub Main()  
    On Error GoTo ProcedureViewsRefreshError  
  
    Dim cat As New ADOX.Catalog  
  
    ' Open the catalog.  
    cat.ActiveConnection = "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Refresh the Procedures collection.  
    cat.Views.Refresh  
  
    'Clean up  
    Set cat = Nothing  
    Exit Sub  
  
ProcedureViewsRefreshError:  
  
    If Not cat Is Nothing Then  
        Set cat = Nothing  
    End If  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndViewsRefreshVB  
```  
  
## See Also  
 [Refresh Method (ADO)](../../../ado/reference/ado-api/refresh-method-ado.md)   
 [Views Collection (ADOX)](../../../ado/reference/adox-api/views-collection-adox.md)
