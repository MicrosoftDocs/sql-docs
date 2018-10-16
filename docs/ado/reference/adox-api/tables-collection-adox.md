---
title: "Tables Collection (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Catalog::Tables"
  - "Tables"
helpviewer_keywords: 
  - "Tables collection [ADOX]"
ms.assetid: 38d750e7-f3fb-426e-b4b4-55eea4f1a654
author: MightyPen
ms.author: genemi
manager: craigg
---
# Tables Collection (ADOX)
Contains all [Table](../../../ado/reference/adox-api/table-object-adox.md) objects of a catalog.  
  
## Remarks  
 The [Append](../../../ado/reference/adox-api/append-method-adox-tables.md) method for a **Tables** collection is unique for ADOX. You can:  
  
-   Add a new table to the collection with the **Append** method.  
  
 The remaining properties and methods are standard to ADO collections. You can:  
  
-   Access a table in the collection with the [Item](../../../ado/reference/ado-api/item-property-ado.md) property.  
  
-   Return the number of tables contained in the collection with the [Count](../../../ado/reference/ado-api/count-property-ado.md) property.  
  
-   Remove a table from the collection with the [Delete](../../../ado/reference/adox-api/delete-method-adox-collections.md) method.  
  
-   Update the objects in the collection to reflect the current database schema with the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method.  
  
 Some providers may return other schema objects, such as a view, in the **Tables** collection. Therefore, some ADOX collections may contain multiple references to the same object. Should you delete the object from one collection, the change will not be visible in another collection that references the deleted object until the **Refresh** method is called on the collection. For example, with the OLE DB Provider for Microsoft Jet, views are returned with the **Tables** collection. If you drop a view, you must refresh the **Tables** collection before the collection will reflect the change.  
  
 This section contains the following topic.  
  
-   [Tables Collection Properties, Methods, and Events](../../../ado/reference/adox-api/tables-collection-properties-methods-and-events.md)  
  
## See Also  
 [Catalog ActiveConnection Property Example (VB)](../../../ado/reference/adox-api/catalog-activeconnection-property-example-vb.md)   
 [Columns and Tables Append Methods, Name Property Example (VB)](../../../ado/reference/adox-api/columns-and-tables-append-methods-name-property-example-vb.md)   
 [Connection Close Method, Table Type Property Example (VB)](../../../ado/reference/adox-api/connection-close-method-table-type-property-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](../../../ado/reference/adox-api/keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [Catalog Object (ADOX)](../../../ado/reference/adox-api/catalog-object-adox.md)   
 [Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)
