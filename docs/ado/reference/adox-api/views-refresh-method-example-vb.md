---
title: "Views Refresh Method Example (VB)"
description: "Views Refresh Method Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Refresh method [ADOX]"
dev_langs:
  - "VB"
---
# Views Refresh Method Example (VB)
The following code shows how to refresh the [Views](./views-collection-adox.md) collection of a [Catalog](./catalog-object-adox.md). This is required before [View](./view-object-adox.md) objects from the **Catalog** can be accessed.  
  
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
 [Refresh Method (ADO)](../ado-api/refresh-method-ado.md)   
 [Views Collection (ADOX)](./views-collection-adox.md)