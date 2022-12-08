---
title: "Group Object (ADOX)"
description: "Group Object (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "group object [ADOX]"
apitype: "COM"
---
# Group Object (ADOX)

Represents a group account that has access permissions within a secured database.  
  
## Remarks  

The [Groups](./groups-collection-adox.md) collection of a [Catalog](./catalog-object-adox.md) represents all the catalog's group accounts. The **Groups** collection for a [User](./user-object-adox.md) represents only the group to which the user belongs.  
  
 With the properties, collections, and methods of a **Group** object, you can:  
  
-   Identify the group with the [Name](./name-property-adox.md) property.  
  
-   Determine whether a group has read, write, or delete permissions with the [GetPermissions](./getpermissions-method-adox.md) and [SetPermissions](./setpermissions-method-adox.md) methods.  
  
-   Access the user accounts that have memberships in the group with the [Users](./users-collection-adox.md) collection.  
  
-   Access provider-specific properties with the [Properties](../ado-api/properties-collection-ado.md) collection.  
  
 This section contains the following topic.  
  
-   [Group Object Properties, Methods, and Events](./group-object-properties-methods-and-events.md)  
  
## See Also  
 [Groups Collection (ADOX)](./groups-collection-adox.md)   
 [Users Collection (ADOX)](./users-collection-adox.md)