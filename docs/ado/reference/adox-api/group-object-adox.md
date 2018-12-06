---
title: "Group Object (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Group"
helpviewer_keywords: 
  - "group object [ADOX]"
ms.assetid: 55ef0ade-68ea-4da5-8aa5-4cd27d1f6d1e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Group Object (ADOX)
Represents a group account that has access permissions within a secured database.  
  
## Remarks  
 The [Groups](../../../ado/reference/adox-api/groups-collection-adox.md) collection of a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md) represents all the catalog's group accounts. The **Groups** collection for a [User](../../../ado/reference/adox-api/user-object-adox.md) represents only the group to which the user belongs.  
  
 With the properties, collections, and methods of a **Group** object, you can:  
  
-   Identify the group with the [Name](../../../ado/reference/adox-api/name-property-adox.md) property.  
  
-   Determine whether a group has read, write, or delete permissions with the [GetPermissions](../../../ado/reference/adox-api/getpermissions-method-adox.md) and [SetPermissions](../../../ado/reference/adox-api/setpermissions-method-adox.md) methods.  
  
-   Access the user accounts that have memberships in the group with the [Users](../../../ado/reference/adox-api/users-collection-adox.md) collection.  
  
-   Access provider-specific properties with the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.  
  
 This section contains the following topic.  
  
-   [Group Object Properties, Methods, and Events](../../../ado/reference/adox-api/group-object-properties-methods-and-events.md)  
  
## See Also  
 [Groups Collection (ADOX)](../../../ado/reference/adox-api/groups-collection-adox.md)   
 [Users Collection (ADOX)](../../../ado/reference/adox-api/users-collection-adox.md)
