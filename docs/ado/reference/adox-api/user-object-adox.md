---
description: "User Object (ADOX)"
title: "User Object (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "User"
helpviewer_keywords: 
  - "User object [ADOX]"
ms.assetid: f68e32ce-ef7c-407d-bdb5-d280947ae0e2
author: rothja
ms.author: jroth
---
# User Object (ADOX)
Represents a user account that has access permissions within a secured database.  
  
## Remarks  
 The [Users](./users-collection-adox.md) collection of a [Catalog](./catalog-object-adox.md) represents all the catalog's users. The **Users** collection for a [Group](./group-object-adox.md) represents only the users of the specific group.  
  
 With the properties, collections, and methods of a **User** object, you can:  
  
-   Identify the user with the [Name](./name-property-adox.md) property.  
  
-   Change the password for a user with the [ChangePassword](./changepassword-method-adox.md) method.  
  
-   Determine whether a user has read, write, or delete permissions with the [GetPermissions](./getpermissions-method-adox.md) and [SetPermissions](./setpermissions-method-adox.md) methods.  
  
-   Access the groups to which a user belongs with the [Groups](./groups-collection-adox.md) collection.  
  
-   Access provider-specific properties with the [Properties](../ado-api/properties-collection-ado.md) collection.  
  
-   Determine the [ParentCatalog](./parentcatalog-property-adox.md) for a user.  
  
 This section contains the following topic.  
  
-   [User Object Properties, Methods, and Events](./user-object-properties-methods-and-events.md)  
  
## See Also  
 [GetPermissions and SetPermissions Methods Example (VB)](./getpermissions-and-setpermissions-methods-example-vb.md)   
 [Groups Collection (ADOX)](./groups-collection-adox.md)   
 [Users Collection (ADOX)](./users-collection-adox.md)