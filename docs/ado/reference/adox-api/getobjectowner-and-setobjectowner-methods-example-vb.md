---
title: "GetObjectOwner and SetObjectOwner Methods Example (VB)"
description: "GetObjectOwner and SetObjectOwner Methods Example (VB)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "SetObjectOwner method [ADOX], Visual Basic example"
  - "GetObjectOwner method [ADOX], Visual Basic example"
dev_langs:
  - "VB"
---
# GetObjectOwner and SetObjectOwner Methods Example (VB)
This example demonstrates the [GetObjectOwner](./getobjectowner-method-adox.md) and [SetObjectOwner](./setobjectowner-method.md) methods. This code assumes the existence of the group Accounting (see the [Groups and Users Append, ChangePassword Methods Example (VB)](./groups-and-users-append-changepassword-methods-example-vb.md) to see how to add this group to the system). The owner of the Categories table is set to Accounting.  
  
```  
' BeginOwnersVB  
Sub OwnersX()  
  
    Dim tblLoop As New ADOX.Table  
    Dim cat As New ADOX.Catalog  
    Dim strOwner As String  
  
    ' Open the Catalog.  
    cat.ActiveConnection = "Provider=Microsoft.Jet.OLEDB.4.0;" & _  
        "Data Source=c:\Program Files\" & _  
        "Microsoft Office\Office\Samples\Northwind.mdb;" & _  
        "jet oledb:system database=" & _  
        "c:\Program Files\Microsoft Office\Office\system.mdw"  
  
    ' Print the original owner of Categories  
    strOwner = cat.GetObjectOwner("Categories", adPermObjTable)  
    Debug.Print "Owner of Categories: " & strOwner  
  
    ' Set the owner of Categories to Accounting  
    cat.SetObjectOwner "Categories", adPermObjTable, "Accounting"  
  
    ' List the owners of all tables and columns in the catalog.  
    For Each tblLoop In cat.Tables  
        Debug.Print "Table: " & tblLoop.Name  
        Debug.Print "   Owner: " & _  
            cat.GetObjectOwner(tblLoop.Name, adPermObjTable)  
    Next tblLoop  
  
    ' Restore the original owner of Categories  
    cat.SetObjectOwner "Categories", adPermObjTable, strOwner  
  
End Sub  
' EndOwnersVB  
```  
  
## See Also  
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [GetObjectOwner Method (ADOX)](./getobjectowner-method-adox.md)   
 [SetObjectOwner Method](./setobjectowner-method.md)