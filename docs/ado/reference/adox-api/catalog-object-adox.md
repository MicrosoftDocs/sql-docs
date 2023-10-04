---
title: "Catalog Object (ADOX)"
description: "Catalog Object (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Catalog object [ADOX]"
helpviewer_keywords:
  - "Catalog object [ADOX]"
apitype: "COM"
---

# Catalog Object (ADOX)

Contains collections ([Tables](./tables-collection-adox.md), [Views](./views-collection-adox.md), [Users](./users-collection-adox.md), [Groups](./groups-collection-adox.md), and [Procedures](./procedures-collection-adox.md)) that describe the schema catalog of a data source.

## Remarks

You can modify the **Catalog** object by adding or removing objects or by modifying existing objects. Some providers may not support all of the **Catalog** objects or may support only viewing schema information.

With the properties and methods of a **Catalog** object, you can:

- Open the catalog by setting the [ActiveConnection](./activeconnection-property-adox.md) property to an ADO [Connection](../ado-api/connection-object-ado.md) object or a valid connection string.

- Create a new catalog with the [Create](./create-method-adox.md) method.

- Determine the owners of the objects in a **Catalog** with the [GetObjectOwner](./getobjectowner-method-adox.md) and [SetObjectOwner](./setobjectowner-method.md) methods.

This section contains the following topic.

- [Catalog Object Properties, Methods, and Events](./catalog-object-properties-methods-and-events.md)

## See Also
- [Catalog ActiveConnection Property Example (VB)](./catalog-activeconnection-property-example-vb.md) 
- [Command and CommandText Properties Example (VB)](./command-and-commandtext-properties-example-vb.md) 
- [Connection Close Method, Table Type Property Example (VB)](./connection-close-method-table-type-property-example-vb.md) 
- [Create Method Example (VB)](./create-method-example-vb.md) 
- [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md) 
- [Parameters Collection, Command Property Example (VB)](./parameters-collection-command-property-example-vb.md) 
- [ParentCatalog Property Example (VB)](./parentcatalog-property-example-vb.md) 
- [Procedures Append Method Example (VB)](./procedures-append-method-example-vb.md) 
- [Procedures Delete Method Example (VB)](./procedures-delete-method-example-vb.md) 
- [Procedures Refresh Method Example (VB)](./procedures-refresh-method-example-vb.md) 
- [Views and Fields Collections Example (VB)](./views-and-fields-collections-example-vb.md) 
- [Views Append Method Example (VB)](./views-append-method-example-vb.md) 
- [Views Collection, CommandText Property Example (VB)](./views-collection-commandtext-property-example-vb.md) 
- [Views Delete Method Example (VB)](./views-delete-method-example-vb.md) 
- [Views Refresh Method Example (VB)](./views-refresh-method-example-vb.md) 
- [Groups Collection (ADOX)](./groups-collection-adox.md) 
- [Procedures Collection (ADOX)](./procedures-collection-adox.md) 
- [Tables Collection (ADOX)](./tables-collection-adox.md) 
- [Users Collection (ADOX)](./users-collection-adox.md) 
- [Views Collection (ADOX)](./views-collection-adox.md)