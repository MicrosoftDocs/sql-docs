---
description: "Keys Collection (ADOX)"
title: "Keys Collection (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Table::Keys"
  - "Keys"
helpviewer_keywords: 
  - "Keys collection [ADOX]"
ms.assetid: cdb31c76-e559-475c-b33a-aac24f73e70e
author: rothja
ms.author: jroth
---
# Keys Collection (ADOX)
Contains all [Key](./key-object-adox.md) objects of a [table](./table-object-adox.md).  
  
## Remarks  
 The [Append](./append-method-adox-keys.md) method for a [Keys collection]() is unique for ADOX. You can:  
  
-   Add a new key to the collection with the [Append](./append-method-adox-keys.md) method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a key in the collection with the [Item](../ado-api/item-property-ado.md) property.  
  
-   Return the number of keys contained in the collection with the [Count](../ado-api/count-property-ado.md) property.  
  
-   Remove a key from the collection with the [Delete](./delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database's schema with the [Refresh](../ado-api/refresh-method-ado.md) method.  
  
 This section contains the following topic.  
  
-   [Indexes Collection Properties, Methods, and Events](./indexes-collection-properties-methods-and-events.md)  
  
## See Also  
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [Keys Collection Properties, Methods, and Events](./keys-collection-properties-methods-and-events.md)   
 [Key Object (ADOX)](./key-object-adox.md)