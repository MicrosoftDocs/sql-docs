---
title: "User Object (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
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
author: MightyPen
ms.author: genemi
manager: craigg
---
# User Object (ADOX)
Represents a user account that has access permissions within a secured database.  
  
## Remarks  
 The [Users](../../../ado/reference/adox-api/users-collection-adox.md) collection of a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md) represents all the catalog's users. The **Users** collection for a [Group](../../../ado/reference/adox-api/group-object-adox.md) represents only the users of the specific group.  
  
 With the properties, collections, and methods of a **User** object, you can:  
  
-   Identify the user with the [Name](../../../ado/reference/adox-api/name-property-adox.md) property.  
  
-   Change the password for a user with the [ChangePassword](../../../ado/reference/adox-api/changepassword-method-adox.md) method.  
  
-   Determine whether a user has read, write, or delete permissions with the [GetPermissions](../../../ado/reference/adox-api/getpermissions-method-adox.md) and [SetPermissions](../../../ado/reference/adox-api/setpermissions-method-adox.md) methods.  
  
-   Access the groups to which a user belongs with the [Groups](../../../ado/reference/adox-api/groups-collection-adox.md) collection.  
  
-   Access provider-specific properties with the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.  
  
-   Determine the [ParentCatalog](../../../ado/reference/adox-api/parentcatalog-property-adox.md) for a user.  
  
 This section contains the following topic.  
  
-   [User Object Properties, Methods, and Events](../../../ado/reference/adox-api/user-object-properties-methods-and-events.md)  
  
## See Also  
 [GetPermissions and SetPermissions Methods Example (VB)](../../../ado/reference/adox-api/getpermissions-and-setpermissions-methods-example-vb.md)   
 [Groups Collection (ADOX)](../../../ado/reference/adox-api/groups-collection-adox.md)   
 [Users Collection (ADOX)](../../../ado/reference/adox-api/users-collection-adox.md)
