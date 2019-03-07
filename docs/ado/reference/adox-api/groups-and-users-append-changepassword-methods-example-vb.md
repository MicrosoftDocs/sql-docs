---
title: "Groups and Users Append, ChangePassword Methods Example (VB) | Microsoft Docs"
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
  - "ChangePassword method [ADOX], Visual Basic example"
  - "Append method [ADOX], Visual Basic example"
ms.assetid: c9426757-9cdd-4a95-b506-d3d011569109
author: MightyPen
ms.author: genemi
manager: craigg
---
# Groups and Users Append, ChangePassword Methods Example (VB)
This example demonstrates the [Append](../../../ado/reference/adox-api/append-method-adox-groups.md) method of [Groups](../../../ado/reference/adox-api/groups-collection-adox.md), as well as the [Append](../../../ado/reference/adox-api/append-method-adox-users.md) method of [Users](../../../ado/reference/adox-api/users-collection-adox.md) by adding a new [Group](../../../ado/reference/adox-api/group-object-adox.md) and a new [User](../../../ado/reference/adox-api/user-object-adox.md) to the system. The new **Group** is appended to the **Groups** collection of the new **User**. Consequently, the new **User** is added to the **Group**. Also, the [ChangePassword](../../../ado/reference/adox-api/changepassword-method-adox.md) method is used to specify the **User** password.  
  
> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.  
  
```  
' BeginGroupVB  
Sub Main()  
    On Error GoTo GroupXError  
  
    Dim cat As ADOX.Catalog  
    Dim usrNew As ADOX.User  
    Dim usrLoop As ADOX.User  
    Dim grpLoop As ADOX.Group  
  
    Set cat = New ADOX.Catalog  
  
    cat.ActiveConnection = "Provider='Microsoft.Jet.OLEDB.4.0';" & _  
        "Data Source='Northwind.mdb';" & _  
        "jet oledb:system database=" & _  
        "'system.mdw'"  
  
    With cat  
        'Create and append new group with a string.  
        .Groups.Append "Accounting"  
  
        ' Create and append new user with an object.  
        Set usrNew = New ADOX.User  
        usrNew.Name = "Pat Smith"  
        usrNew.ChangePassword "", "Password1"  
        .Users.Append usrNew  
  
        ' Make the user Pat Smith a member of the  
        ' Accounting group by creating and adding the  
        ' appropriate Group object to the user's Groups  
        ' collection. The same is accomplished if a User  
        ' object representing Pat Smith is created and  
        ' appended to the Accounting group Users collection  
        usrNew.Groups.Append "Accounting"  
  
        ' Enumerate all User objects in the  
        ' catalog's Users collection.  
        For Each usrLoop In .Users  
            Debug.Print "  " & usrLoop.Name  
            Debug.Print "    Belongs to these groups:"  
            ' Enumerate all Group objects in each User  
            ' object's Groups collection.  
            If usrLoop.Groups.Count <> 0 Then  
                For Each grpLoop In usrLoop.Groups  
                    Debug.Print "    " & grpLoop.Name  
                Next grpLoop  
            Else  
                Debug.Print "    [None]"  
            End If  
        Next usrLoop  
  
        ' Enumerate all Group objects in the default  
        ' workspace's Groups collection.  
        For Each grpLoop In .Groups  
            Debug.Print "  " & grpLoop.Name  
            Debug.Print "    Has as its members:"  
            ' Enumerate all User objects in each Group  
            ' object's Users collection.  
            If grpLoop.Users.Count <> 0 Then  
                For Each usrLoop In grpLoop.Users  
                    Debug.Print "    " & usrLoop.Name  
                Next usrLoop  
            Else  
                Debug.Print "    [None]"  
            End If  
        Next grpLoop  
  
        ' Delete new User and Group objects because this  
        ' is only a demonstration.  
        ' These two line are commented out because the sub "OwnersX" uses  
        ' the group "Accounting".  
'        .Users.Delete "Pat Smith"  
'        .Groups.Delete "Accounting"  
  
    End With  
  
    'Clean up  
    Set cat.ActiveConnection = Nothing  
    Set cat = Nothing  
    Set usrNew = Nothing  
    Exit Sub  
  
GroupXError:  
  
    Set cat = Nothing  
    Set usrNew = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
' EndGroupVB  
```  
  
## See Also  
 [Append Method (ADOX Groups)](../../../ado/reference/adox-api/append-method-adox-groups.md)   
 [Append Method (ADOX Users)](../../../ado/reference/adox-api/append-method-adox-users.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [ChangePassword Method (ADOX)](../../../ado/reference/adox-api/changepassword-method-adox.md)   
 [Group Object (ADOX)](../../../ado/reference/adox-api/group-object-adox.md)   
 [Groups Collection (ADOX)](../../../ado/reference/adox-api/groups-collection-adox.md)   
 [User Object (ADOX)](../../../ado/reference/adox-api/user-object-adox.md)   
 [Users Collection (ADOX)](../../../ado/reference/adox-api/users-collection-adox.md)
