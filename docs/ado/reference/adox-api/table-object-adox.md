---
title: "Table Object (ADOX)"
description: "Table Object (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Table object [ADOX]"
apitype: "COM"
---
# Table Object (ADOX)
Represents a database table including columns, indexes, and keys.  
  
## Remarks  
 The following code creates a new **Table**:  
  
```  
Dim obj As New Table  
```  
  
 With the properties and collections of a **Table** object, you can:  
  
-   Identify the table with the [Name Property (ADOX)](./name-property-adox.md) property.  
  
-   Determine the type of table with the [Type Property (Table) (ADOX)](./type-property-table-adox.md) property.  
  
-   Access the database columns of the table with the [Columns Collection (ADOX)](./columns-collection-adox.md) collection.  
  
-   Access the indexes of the table with the [Indexes Collection (ADOX)](./indexes-collection-adox.md).  
  
-   Access the keys of the table with the [Keys Collection (ADOX)](./keys-collection-adox.md).  
  
-   Specify the Catalog that owns the table with the [ParentCatalog Property (ADOX)](./parentcatalog-property-adox.md) property.  
  
-   Return date information with the [DateCreated Property (ADOX)](./datecreated-property-adox.md) and [DateModified Property (ADOX)](./datemodified-property-adox.md) properties.  
  
-   Access provider-specific table properties with the [Properties Collection (ADO)](../ado-api/properties-collection-ado.md) collection.  
  
> [!NOTE]
>  Your data provider may not support all properties of **Table** objects. An error will occur if you have set a value for a property that the provider does not support. For new **Table** objects, the error will occur when the object is appended to the collection. For existing objects, the error will occur when setting the property.  
>   
>  When creating **Table** objects, the existence of an appropriate default value for an optional property does not guarantee that your provider supports the property. For more information about which properties your provider supports, see your provider documentation.  
  
 This section contains the following topic.  
  
-   [Table Object Properties, Methods, and Events](./table-object-properties-methods-and-events.md)  
  
## See Also  
 [Catalog ActiveConnection Property Example (VB)](./catalog-activeconnection-property-example-vb.md)   
 [Columns and Tables Append Methods, Name Property Example (VB)](./columns-and-tables-append-methods-name-property-example-vb.md)   
 [Connection Close Method, Table Type Property Example (VB)](./connection-close-method-table-type-property-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [ParentCatalog Property Example (VB)](./parentcatalog-property-example-vb.md)   
 [Columns Collection (ADOX)](./columns-collection-adox.md)   
 [Indexes Collection (ADOX)](./indexes-collection-adox.md)   
 [Keys Collection (ADOX)](./keys-collection-adox.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)   
 [Tables Collection (ADOX)](./tables-collection-adox.md)