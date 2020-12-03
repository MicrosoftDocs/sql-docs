---
description: "Append Method (ADOX Groups)"
title: "Append Method (ADOX Groups) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
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
author: rothja
ms.author: jroth
---
# Append Method (ADOX Groups)
Adds a new [Group](./group-object-adox.md) object to the [Groups](./groups-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Groups.Append Group  
```  
  
#### Parameters  
 *Group*  
 The **Group** object to append or the name of the group to create and append.  
  
## Remarks  
 The **Groups** collection of a [Catalog](./catalog-object-adox.md) represents all of the catalog's group accounts. The **Groups** collection for a [User](./user-object-adox.md) represents only the group to which the user belongs.  
  
 An error will occur if the provider does not support creating groups.  
  
> [!NOTE]
>  Before appending a **Group** object to the **Groups** collection of a **User** object, a **Group** object with the same [Name](./name-property-adox.md) as the one to be appended must already exist in the **Groups** collection of the **Catalog**.  
  
## Applies To  
 [Groups Collection (ADOX)](./groups-collection-adox.md)  
  
## See Also  
 [Groups and Users Append, ChangePassword Methods Example (VB)](./groups-and-users-append-changepassword-methods-example-vb.md)   
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Indexes)](./append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](./append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](./append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](./append-method-adox-tables.md)   
 [Append Method (ADOX Users)](./append-method-adox-users.md)   
 [Append Method (ADOX Views)](./append-method-adox-views.md)