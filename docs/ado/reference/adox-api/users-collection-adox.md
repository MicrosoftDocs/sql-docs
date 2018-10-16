---
title: "Users Collection (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Group::Users"
  - "Users"
  - "Catalog::Users"
helpviewer_keywords: 
  - "Users collection [ADOX]"
ms.assetid: 0a30fa74-6f10-4410-bd70-882e7c43cd46
author: MightyPen
ms.author: genemi
manager: craigg
---
# Users Collection (ADOX)
Contains all stored [User](../../../ado/reference/adox-api/user-object-adox.md) objects of a [catalog](../../../ado/reference/adox-api/catalog-object-adox.md) or [group](../../../ado/reference/adox-api/group-object-adox.md).  
  
## Remarks  
 The **Users** collection of a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md) represents all the catalog's users. The **Users** collection for a [Group](../../../ado/reference/adox-api/group-object-adox.md) represents only the users that have a membership in the specific group.  
  
 The [Append](../../../ado/reference/adox-api/append-method-adox-users.md) method for a **Users** collection is unique for ADOX. You can:  
  
-   Add a new user to the collection by using the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a user in the collection with the [Item](../../../ado/reference/ado-api/item-property-ado.md) property.  
  
-   Return the number of users contained in the collection with the [Count](../../../ado/reference/ado-api/count-property-ado.md) property.  
  
-   Remove a user from the collection with the [Delete](../../../ado/reference/adox-api/delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database's schema with the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method.  
  
> [!NOTE]
>  Before appending a **User** object to the **Users** collection of a **Group** object, a **User** object with the same [Name](../../../ado/reference/adox-api/name-property-adox.md) as the one to be appended must already exist in the **Users** collection of the **Catalog**.  
  
 This section contains the following topic.  
  
-   [Users Collection Properties, Methods, and Events](../../../ado/reference/adox-api/users-collection-properties-methods-and-events.md)  
  
## See Also  
 [GetPermissions and SetPermissions Methods Example (VB)](../../../ado/reference/adox-api/getpermissions-and-setpermissions-methods-example-vb.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [User Object (ADOX)](../../../ado/reference/adox-api/user-object-adox.md)
