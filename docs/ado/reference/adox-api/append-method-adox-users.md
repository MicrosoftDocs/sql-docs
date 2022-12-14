---
title: "Append Method (ADOX Users)"
description: "Append Method (ADOX Users)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Users::raw_Append"
  - "Users::Append"
helpviewer_keywords:
  - "Append method [ADOX]"
apitype: "COM"
---
# Append Method (ADOX Users)
Adds a new [User](./user-object-adox.md) object to the [Users](./users-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Users.Append User[,Password]  
```  
  
#### Parameters  
 *User*  
 A **Variant** value that contains the **User** object to append or the name of the user to create and append.  
  
 *Password*  
 Optional. A **String** value that contains the password for the user. The *Password* parameter corresponds to the value specified by the [ChangePassword](./changepassword-method-adox.md) method of a **User** object.  
  
## Remarks  
 The **Users** collection of a [Catalog](./catalog-object-adox.md) represents all the catalog's users. The **Users** collection for a [Group](./group-object-adox.md) represents only the users that have a membership in the specific group.  
  
 An error will occur if the provider does not support creating users.  
  
> [!NOTE]
>  Before appending a **User** object to the **Users** collection of a **Group** object, a **User** object with the same [Name](./name-property-adox.md) as the one to be appended must already exist in the **Users** collection of the **Catalog**.  
  
## Applies To  
 [Users Collection (ADOX)](./users-collection-adox.md)  
  
## See Also  
 [Groups and Users Append, ChangePassword Methods Example (VB)](./groups-and-users-append-changepassword-methods-example-vb.md)   
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](./append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](./append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](./append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](./append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](./append-method-adox-tables.md)   
 [Append Method (ADOX Views)](./append-method-adox-views.md)