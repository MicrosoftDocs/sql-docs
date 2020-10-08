---
description: "Delete Method (ADOX Collections)"
title: "Delete Method (ADOX Collections) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
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
author: rothja
ms.author: jroth
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
  
 For [Tables](./tables-collection-adox.md) and [Users](./users-collection-adox.md) collections, an error will occur if the provider does not support deleting tables or users, respectively. For [Procedures](./procedures-collection-adox.md) and [Views](./views-collection-adox.md) collections, **Delete** will fail if the provider does not support persisting commands.  
  
## Applies To  

:::row:::
    :::column:::
        [Columns Collection (ADOX)](./columns-collection-adox.md)  
        [Groups Collection (ADOX)](./groups-collection-adox.md)  
        [Indexes Collection (ADOX)](./indexes-collection-adox.md)  
    :::column-end:::
    :::column:::
        [Keys Collection (ADOX)](./keys-collection-adox.md)  
        [Procedures Collection (ADOX)](./procedures-collection-adox.md)  
        [Tables Collection (ADOX)](./tables-collection-adox.md)  
    :::column-end:::
    :::column:::
        [Users Collection (ADOX)](./users-collection-adox.md)  
        [Views Collection (ADOX)](./views-collection-adox.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Procedures Delete Method Example (VB)](./procedures-delete-method-example-vb.md)   
 [Views Delete Method Example (VB)](./views-delete-method-example-vb.md)