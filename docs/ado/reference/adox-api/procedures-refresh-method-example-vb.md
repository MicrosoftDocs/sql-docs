---
title: "Procedures Refresh Method Example (VB)"
description: "Procedures Refresh Method Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Refresh method [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# Procedures Refresh Method Example (VB)
The following code shows how to refresh the [Procedures](./procedures-collection-adox.md) collection of a [Catalog](./catalog-object-adox.md). This is required before [Procedure](./procedure-object-adox.md) objects from the **Catalog** can be accessed.  
  
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
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Procedures Collection (ADOX)](./procedures-collection-adox.md)   
 [Refresh Method (ADO)](../ado-api/refresh-method-ado.md)