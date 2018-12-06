---
title: "Groups Collection (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Groups"
  - "User::Groups"
  - "Catalog::Groups"
helpviewer_keywords: 
  - "Groups collection [ADOX]"
ms.assetid: 09aa7b0a-69d5-4564-80a7-20ad8189670f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Groups Collection (ADOX)
Contains all stored [Group](../../../ado/reference/adox-api/group-object-adox.md) objects of a catalog or user.  
  
## Remarks  
 The **Groups** collection of a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md) represents all of the catalog's group accounts. The **Groups** collection for a [User](../../../ado/reference/adox-api/user-object-adox.md) represents only the group to which the user belongs.  
  
 The [Append](../../../ado/reference/adox-api/append-method-adox-groups.md) method for a **Groups** collection is unique for ADOX. You can:  
  
-   Add a new security group to the collection with the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a group in the collection with the [Item](../../../ado/reference/ado-api/item-property-ado.md) property.  
  
-   Return the number of groups contained in the collection with the [Count](../../../ado/reference/ado-api/count-property-ado.md) property.  
  
-   Remove a group from the collection with the [Delete](../../../ado/reference/adox-api/delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database schema with the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method.  
  
> [!NOTE]
>  Before appending a **Group** object to the **Groups** collection of a **User** object, a **Group** object with the same [Name](../../../ado/reference/adox-api/name-property-adox.md) as the one to be appended must already exist in the **Groups** collection of the **Catalog**.  
  
 This section contains the following topic.  
  
-   [Groups Collection Properties, Methods, and Events](../../../ado/reference/adox-api/groups-collection-properties-methods-and-events.md)  
  
## See Also  
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Group Object (ADOX)](../../../ado/reference/adox-api/group-object-adox.md)
