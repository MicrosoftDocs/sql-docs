---
title: "Create Method Example (VB)"
description: "Create Method Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Create method [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# Create Method Example (VB)
The following code shows how to create a new Microsoft Jet database with the [Create](./create-method-adox.md) method.  
  
```  
Attribute VB_Name = "Create"  
Option Explicit  
  
' BeginCreateDatabaseVB  
Sub CreateDatabase()  
    On Error GoTo CreateDatabaseError  
  
    Dim cat As New ADOX.Catalog  
    cat.Create "Provider='Microsoft.Jet.OLEDB.4.0';Data Source='new.mdb'"  
  
    'Clean up  
    Set cat = Nothing  
    Exit Sub  
  
CreateDatabaseError:  
    Set cat = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndCreateDatabaseVB  
```  
  
## See Also  
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Create Method (ADOX)](./create-method-adox.md)