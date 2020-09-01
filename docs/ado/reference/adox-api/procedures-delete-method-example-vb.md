---
description: "Procedures Delete Method Example (VB)"
title: "Procedures Delete Method Example (VB) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Delete method [ADOX], Visual Basic example"
ms.assetid: 94f1ac93-e778-4a40-a85e-94bce5316ac7
author: rothja
ms.author: jroth
---
# Procedures Delete Method Example (VB)
The following code demonstrates how to delete a procedure by using the [Delete](./delete-method-adox-collections.md) method of the [Procedures](./procedures-collection-adox.md) collection.  
  
```  
' BeginDeleteProcedureVB  
Sub Main()  
    On Error GoTo DeleteProcedureError  
  
    Dim cat As New ADOX.Catalog  
  
    ' Open the catalog.  
    cat.ActiveConnection = _  
        "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';"  
  
    ' Delete the procedure.  
    cat.Procedures.Delete "CustomerById"  
  
    'Clean up.  
    Set cat.ActiveConnection = Nothing  
    Set cat = Nothing  
    Exit Sub  
  
DeleteProcedureError:  
    Set cat = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndDeleteProcedureVB  
```  
  
## See Also  
 [ActiveConnection Property (ADOX)](./activeconnection-property-adox.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Delete Method (ADOX Collections)](./delete-method-adox-collections.md)   
 [Procedure Object (ADOX)](./procedure-object-adox.md)   
 [Procedures Collection (ADOX)](./procedures-collection-adox.md)