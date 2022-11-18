---
title: "Users Collection (ADOX)"
description: "Users Collection (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Group::Users"
  - "Users"
  - "Catalog::Users"
helpviewer_keywords:
  - "Users collection [ADOX]"
apitype: "COM"
---
# Users Collection (ADOX)
Contains all stored [User](./user-object-adox.md) objects of a [catalog](./catalog-object-adox.md) or [group](./group-object-adox.md).  
  
## Remarks  
 The **Users** collection of a [Catalog](./catalog-object-adox.md) represents all the catalog's users. The **Users** collection for a [Group](./group-object-adox.md) represents only the users that have a membership in the specific group.  
  
 The [Append](./append-method-adox-users.md) method for a **Users** collection is unique for ADOX. You can:  
  
-   Add a new user to the collection by using the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a user in the collection with the [Item](../ado-api/item-property-ado.md) property.  
  
-   Return the number of users contained in the collection with the [Count](../ado-api/count-property-ado.md) property.  
  
-   Remove a user from the collection with the [Delete](./delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database's schema with the [Refresh](../ado-api/refresh-method-ado.md) method.  
  
> [!NOTE]
>  Before appending a **User** object to the **Users** collection of a **Group** object, a **User** object with the same [Name](./name-property-adox.md) as the one to be appended must already exist in the **Users** collection of the **Catalog**.  
  
 This section contains the following topic.  
  
-   [Users Collection Properties, Methods, and Events](./users-collection-properties-methods-and-events.md)  
  
## See Also  
 [GetPermissions and SetPermissions Methods Example (VB)](./getpermissions-and-setpermissions-methods-example-vb.md)   
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [User Object (ADOX)](./user-object-adox.md)