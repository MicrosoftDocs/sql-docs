---
title: "Append Method (ADOX Groups) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Groups::raw_Append"
  - "Groups::Append"
helpviewer_keywords: 
  - "Append method [ADOX]"
ms.assetid: 56b94fc6-7ef0-4e4a-82a3-033b94c46036
author: MightyPen
ms.author: genemi
manager: craigg
---
# Append Method (ADOX Groups)
Adds a new [Group](../../../ado/reference/adox-api/group-object-adox.md) object to the [Groups](../../../ado/reference/adox-api/groups-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Groups.Append Group  
```  
  
#### Parameters  
 *Group*  
 The **Group** object to append or the name of the group to create and append.  
  
## Remarks  
 The **Groups** collection of a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md) represents all of the catalog's group accounts. The **Groups** collection for a [User](../../../ado/reference/adox-api/user-object-adox.md) represents only the group to which the user belongs.  
  
 An error will occur if the provider does not support creating groups.  
  
> [!NOTE]
>  Before appending a **Group** object to the **Groups** collection of a **User** object, a **Group** object with the same [Name](../../../ado/reference/adox-api/name-property-adox.md) as the one to be appended must already exist in the **Groups** collection of the **Catalog**.  
  
## Applies To  
 [Groups Collection (ADOX)](../../../ado/reference/adox-api/groups-collection-adox.md)  
  
## See Also  
 [Groups and Users Append, ChangePassword Methods Example (VB)](../../../ado/reference/adox-api/groups-and-users-append-changepassword-methods-example-vb.md)   
 [Append Method (ADOX Columns)](../../../ado/reference/adox-api/append-method-adox-columns.md)   
 [Append Method (ADOX Indexes)](../../../ado/reference/adox-api/append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](../../../ado/reference/adox-api/append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](../../../ado/reference/adox-api/append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](../../../ado/reference/adox-api/append-method-adox-tables.md)   
 [Append Method (ADOX Users)](../../../ado/reference/adox-api/append-method-adox-users.md)   
 [Append Method (ADOX Views)](../../../ado/reference/adox-api/append-method-adox-views.md)
