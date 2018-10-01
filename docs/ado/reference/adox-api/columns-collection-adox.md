---
title: "Columns Collection (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Index::Columns"
  - "Table::Columns"
  - "Key::Columns"
  - "Columns"
helpviewer_keywords: 
  - "Columns collection [ADOX]"
ms.assetid: 23b9fea8-4f76-4a51-95ce-1a6ce4560b34
author: MightyPen
ms.author: genemi
manager: craigg
---
# Columns Collection (ADOX)
Contains all [Column](../../../ado/reference/adox-api/column-object-adox.md) objects of a table, index, or key.  
  
## Remarks  
 The [Append](../../../ado/reference/adox-api/append-method-adox-columns.md) method for a **Columns** collection is unique for ADOX. You can:  
  
-   Add a new column to the collection with the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a column in the collection with the [Item](../../../ado/reference/ado-api/item-property-ado.md) property.  
  
-   Return the number of columns contained in the collection with the [Count](../../../ado/reference/ado-api/count-property-ado.md) property.  
  
-   Remove a column from the collection with the [Delete](../../../ado/reference/adox-api/delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database's schema with the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method.  
  
> [!NOTE]
>  An error will occur when appending a **Column** to the **Columns** collection of an [Index](../../../ado/reference/adox-api/index-object-adox.md) if the **Column** does not exist in a [Table](../../../ado/reference/adox-api/table-object-adox.md) that is already appended to the [Tables](../../../ado/reference/adox-api/tables-collection-adox.md) collection.  
  
 This section contains the following topic.  
  
-   [Columns Collection Properties, Methods, and Events](../../../ado/reference/adox-api/columns-collection-properties-methods-and-events.md)  
  
## See Also  
 [Columns and Tables Append Methods, Name Property Example (VB)](../../../ado/reference/adox-api/columns-and-tables-append-methods-name-property-example-vb.md)   
 [Connection Close Method, Table Type Property Example (VB)](../../../ado/reference/adox-api/connection-close-method-table-type-property-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](../../../ado/reference/adox-api/keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [ParentCatalog Property Example (VB)](../../../ado/reference/adox-api/parentcatalog-property-example-vb.md)   
 [SortOrder Property Example (VB)](../../../ado/reference/adox-api/sortorder-property-example-vb.md)   
 [Column Object (ADOX)](../../../ado/reference/adox-api/column-object-adox.md)
