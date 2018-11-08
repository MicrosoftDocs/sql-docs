---
title: "Append Method (ADOX Users) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Users::raw_Append"
  - "Users::Append"
helpviewer_keywords: 
  - "Append method [ADOX]"
ms.assetid: b80bc5d5-78ca-4f75-956b-2ac658029cc7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Append Method (ADOX Users)
Adds a new [User](../../../ado/reference/adox-api/user-object-adox.md) object to the [Users](../../../ado/reference/adox-api/users-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Users.Append User[,Password]  
```  
  
#### Parameters  
 *User*  
 A **Variant** value that contains the **User** object to append or the name of the user to create and append.  
  
 *Password*  
 Optional. A **String** value that contains the password for the user. The *Password* parameter corresponds to the value specified by the [ChangePassword](../../../ado/reference/adox-api/changepassword-method-adox.md) method of a **User** object.  
  
## Remarks  
 The **Users** collection of a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md) represents all the catalog's users. The **Users** collection for a [Group](../../../ado/reference/adox-api/group-object-adox.md) represents only the users that have a membership in the specific group.  
  
 An error will occur if the provider does not support creating users.  
  
> [!NOTE]
>  Before appending a **User** object to the **Users** collection of a **Group** object, a **User** object with the same [Name](../../../ado/reference/adox-api/name-property-adox.md) as the one to be appended must already exist in the **Users** collection of the **Catalog**.  
  
## Applies To  
 [Users Collection (ADOX)](../../../ado/reference/adox-api/users-collection-adox.md)  
  
## See Also  
 [Groups and Users Append, ChangePassword Methods Example (VB)](../../../ado/reference/adox-api/groups-and-users-append-changepassword-methods-example-vb.md)   
 [Append Method (ADOX Columns)](../../../ado/reference/adox-api/append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](../../../ado/reference/adox-api/append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](../../../ado/reference/adox-api/append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](../../../ado/reference/adox-api/append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](../../../ado/reference/adox-api/append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](../../../ado/reference/adox-api/append-method-adox-tables.md)   
 [Append Method (ADOX Views)](../../../ado/reference/adox-api/append-method-adox-views.md)
