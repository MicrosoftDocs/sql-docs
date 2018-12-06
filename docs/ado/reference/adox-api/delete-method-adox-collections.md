---
title: "Delete Method (ADOX Collections) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Views::Delete"
  - "Groups::Delete"
  - "Indexes::raw_Delete"
  - "Columns::raw_Delete"
  - "Tables::Delete"
  - "Keys::Delete"
  - "Users::Delete"
  - "Users::raw_Delete"
  - "Keys::raw_Delete"
  - "Procedures::raw_Delete"
  - "Views::raw_Delete"
  - "Indexes::Delete"
  - "Procedures::Delete"
  - "Groups::raw_Delete"
  - "Tables::raw_Delete"
  - "Columns::Delete"
helpviewer_keywords: 
  - "delete method [ADOX]"
ms.assetid: e6b6e3a4-8952-4d79-81f4-51019c338374
author: MightyPen
ms.author: genemi
manager: craigg
---
# Delete Method (ADOX Collections)
Removes an object from a collection.  
  
## Syntax  
  
```  
  
Collection.Delete Name  
```  
  
#### Parameters  
 *Name*  
 A **Variant** that specifies the name or ordinal position (index) of the object to delete.  
  
## Remarks  
 An error will occur if the *Name* does not exist in the collection.  
  
 For [Tables](../../../ado/reference/adox-api/tables-collection-adox.md) and [Users](../../../ado/reference/adox-api/users-collection-adox.md) collections, an error will occur if the provider does not support deleting tables or users, respectively. For [Procedures](../../../ado/reference/adox-api/procedures-collection-adox.md) and [Views](../../../ado/reference/adox-api/views-collection-adox.md) collections, **Delete** will fail if the provider does not support persisting commands.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Columns Collection (ADOX)](../../../ado/reference/adox-api/columns-collection-adox.md)|[Groups Collection (ADOX)](../../../ado/reference/adox-api/groups-collection-adox.md)|[Indexes Collection (ADOX)](../../../ado/reference/adox-api/indexes-collection-adox.md)|  
|[Keys Collection (ADOX)](../../../ado/reference/adox-api/keys-collection-adox.md)|[Procedures Collection (ADOX)](../../../ado/reference/adox-api/procedures-collection-adox.md)|[Tables Collection (ADOX)](../../../ado/reference/adox-api/tables-collection-adox.md)|  
|[Users Collection (ADOX)](../../../ado/reference/adox-api/users-collection-adox.md)|[Views Collection (ADOX)](../../../ado/reference/adox-api/views-collection-adox.md)||  
  
## See Also  
 [Procedures Delete Method Example (VB)](../../../ado/reference/adox-api/procedures-delete-method-example-vb.md)   
 [Views Delete Method Example (VB)](../../../ado/reference/adox-api/views-delete-method-example-vb.md)
