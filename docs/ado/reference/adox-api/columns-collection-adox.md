---
title: Columns Collection (ADOX)
description: "Columns Collection (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Columns_Collection_ADOX"
helpviewer_keywords:
  - "Columns collection [ADOX]"
dev_langs:
  - "TSQL"
apitype: "COM"
---

# Columns Collection (ADOX)

Contains all [Column](./column-object-adox.md) objects of a table, index, or key.  
  
## Remarks

The [Append](./append-method-adox-columns.md) method for a **Columns** collection is unique for ADOX. You can:  
  
- Add a new column to the collection with the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
- Access a column in the collection with the [Item](../ado-api/item-property-ado.md) property.  
  
- Return the number of columns contained in the collection with the [Count](../ado-api/count-property-ado.md) property.  
  
- Remove a column from the collection with the [Delete](./delete-method-adox-collections.md) method.  
  
- Update the objects in the collection to reflect the current database's schema with the [Refresh](../ado-api/refresh-method-ado.md) method.  
  
> [!NOTE]
> An error will occur when appending a **Column** to the **Columns** collection of an [Index](./index-object-adox.md) if the **Column** does not exist in a [Table](./table-object-adox.md) that is already appended to the [Tables](./tables-collection-adox.md) collection.  
  
 This section contains the following topic.  
  
- [Columns Collection Properties, Methods, and Events](./columns-collection-properties-methods-and-events.md)  
  
## See Also

- [Columns and Tables Append Methods, Name Property Example (VB)](./columns-and-tables-append-methods-name-property-example-vb.md)
- [Connection Close Method, Table Type Property Example (VB)](./connection-close-method-table-type-property-example-vb.md)
- [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)
- [ParentCatalog Property Example (VB)](./parentcatalog-property-example-vb.md)
- [SortOrder Property Example (VB)](./sortorder-property-example-vb.md)
- [Column Object (ADOX)](./column-object-adox.md)