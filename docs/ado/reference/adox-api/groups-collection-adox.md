---
title: "Groups Collection (ADOX)"
description: "Groups Collection (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Groups"
  - "User::Groups"
  - "Catalog::Groups"
helpviewer_keywords:
  - "Groups collection [ADOX]"
apitype: "COM"
---
# Groups Collection (ADOX)
Contains all stored [Group](./group-object-adox.md) objects of a catalog or user.  
  
## Remarks  
 The **Groups** collection of a [Catalog](./catalog-object-adox.md) represents all of the catalog's group accounts. The **Groups** collection for a [User](./user-object-adox.md) represents only the group to which the user belongs.  
  
 The [Append](./append-method-adox-groups.md) method for a **Groups** collection is unique for ADOX. You can:  
  
-   Add a new security group to the collection with the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a group in the collection with the [Item](../ado-api/item-property-ado.md) property.  
  
-   Return the number of groups contained in the collection with the [Count](../ado-api/count-property-ado.md) property.  
  
-   Remove a group from the collection with the [Delete](./delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database schema with the [Refresh](../ado-api/refresh-method-ado.md) method.  
  
> [!NOTE]
>  Before appending a **Group** object to the **Groups** collection of a **User** object, a **Group** object with the same [Name](./name-property-adox.md) as the one to be appended must already exist in the **Groups** collection of the **Catalog**.  
  
 This section contains the following topic.  
  
-   [Groups Collection Properties, Methods, and Events](./groups-collection-properties-methods-and-events.md)  
  
## See Also  
 [Catalog Object (ADOX)](./catalog-object-adox.md)   
 [Group Object (ADOX)](./group-object-adox.md)