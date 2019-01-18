---
title: "Catalog Object (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Catalog"
helpviewer_keywords: 
  - "Catalog object [ADOX]"
ms.assetid: bb651639-a488-4e38-b6de-0ed99fa4dd92
author: MightyPen
ms.author: genemi
manager: craigg
---
# Catalog Object (ADOX)
Contains collections ([Tables](../../../ado/reference/adox-api/tables-collection-adox.md), [Views](../../../ado/reference/adox-api/views-collection-adox.md), [Users](../../../ado/reference/adox-api/users-collection-adox.md), [Groups](../../../ado/reference/adox-api/groups-collection-adox.md), and [Procedures](../../../ado/reference/adox-api/procedures-collection-adox.md)) that describe the schema catalog of a data source.  
  
## Remarks  
 You can modify the **Catalog** object by adding or removing objects or by modifying existing objects. Some providers may not support all of the **Catalog** objects or may support only viewing schema information.  
  
 With the properties and methods of a **Catalog** object, you can:  
  
-   Open the catalog by setting the [ActiveConnection](../../../ado/reference/adox-api/activeconnection-property-adox.md) property to an ADO [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object or a valid connection string.  
  
-   Create a new catalog with the [Create](../../../ado/reference/adox-api/create-method-adox.md) method.  
  
-   Determine the owners of the objects in a **Catalog** with the [GetObjectOwner](../../../ado/reference/adox-api/getobjectowner-method-adox.md) and [SetObjectOwner](../../../ado/reference/adox-api/setobjectowner-method.md) methods.  
  
 This section contains the following topic.  
  
-   [Catalog Object Properties, Methods, and Events](../../../ado/reference/adox-api/catalog-object-properties-methods-and-events.md)  
  
## See Also  
 [Catalog ActiveConnection Property Example (VB)](../../../ado/reference/adox-api/catalog-activeconnection-property-example-vb.md)   
 [Command and CommandText Properties Example (VB)](../../../ado/reference/adox-api/command-and-commandtext-properties-example-vb.md)   
 [Connection Close Method, Table Type Property Example (VB)](../../../ado/reference/adox-api/connection-close-method-table-type-property-example-vb.md)   
 [Create Method Example (VB)](../../../ado/reference/adox-api/create-method-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](../../../ado/reference/adox-api/keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [Parameters Collection, Command Property Example (VB)](../../../ado/reference/adox-api/parameters-collection-command-property-example-vb.md)   
 [ParentCatalog Property Example (VB)](../../../ado/reference/adox-api/parentcatalog-property-example-vb.md)   
 [Procedures Append Method Example (VB)](../../../ado/reference/adox-api/procedures-append-method-example-vb.md)   
 [Procedures Delete Method Example (VB)](../../../ado/reference/adox-api/procedures-delete-method-example-vb.md)   
 [Procedures Refresh Method Example (VB)](../../../ado/reference/adox-api/procedures-refresh-method-example-vb.md)   
 [Views and Fields Collections Example (VB)](../../../ado/reference/adox-api/views-and-fields-collections-example-vb.md)   
 [Views Append Method Example (VB)](../../../ado/reference/adox-api/views-append-method-example-vb.md)   
 [Views Collection, CommandText Property Example (VB)](../../../ado/reference/adox-api/views-collection-commandtext-property-example-vb.md)   
 [Views Delete Method Example (VB)](../../../ado/reference/adox-api/views-delete-method-example-vb.md)   
 [Views Refresh Method Example (VB)](../../../ado/reference/adox-api/views-refresh-method-example-vb.md)   
 [Groups Collection (ADOX)](../../../ado/reference/adox-api/groups-collection-adox.md)   
 [Procedures Collection (ADOX)](../../../ado/reference/adox-api/procedures-collection-adox.md)   
 [Tables Collection (ADOX)](../../../ado/reference/adox-api/tables-collection-adox.md)   
 [Users Collection (ADOX)](../../../ado/reference/adox-api/users-collection-adox.md)   
 [Views Collection (ADOX)](../../../ado/reference/adox-api/views-collection-adox.md)
