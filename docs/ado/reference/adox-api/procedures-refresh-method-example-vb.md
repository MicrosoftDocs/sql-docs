---
title: "Procedures Refresh Method Example (VB) | Microsoft Docs"
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
  - "Refresh method [ADOX], Visual Basic example"
ms.assetid: 499679bd-287b-487d-bdfb-3803abffec1c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Procedures Refresh Method Example (VB)
The following code shows how to refresh the [Procedures](../../../ado/reference/adox-api/procedures-collection-adox.md) collection of a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md). This is required before [Procedure](../../../ado/reference/adox-api/procedure-object-adox.md) objects from the **Catalog** can be accessed.  
  
```  
' BeginProceduresRefreshVB  
Sub Main()  
    On Error GoTo ProcedureRefreshError  
  
    Dim cat As New ADOX.Catalog  
  
    ' Open the Catalog  
    cat.ActiveConnection = _  
        "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Refresh the Procedures collection  
    cat.Procedures.Refresh  
  
    'Clean up  
    Set cat.ActiveConnection = Nothing  
    Set cat = Nothing  
    Exit Sub  
  
ProcedureRefreshError:  
    Set cat = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndProceduresRefreshVB  
```  
  
## See Also  
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Procedures Collection (ADOX)](../../../ado/reference/adox-api/procedures-collection-adox.md)   
 [Refresh Method (ADO)](../../../ado/reference/ado-api/refresh-method-ado.md)
