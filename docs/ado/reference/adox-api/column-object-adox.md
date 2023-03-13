---
title: "Column Object (ADOX)"
description: "Column Object (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Column"
helpviewer_keywords:
  - "Column object [ADOX]"
apitype: "COM"
---
# Column Object (ADOX)
Represents a column from a table, index, or key.  
  
## Remarks  
 The following code creates a new **Column**:  
  
 `Dim obj As New Column`  
  
 With the properties and collections of a **Column** object, you can:  
  
-   Identify the column with the [Name Property (ADOX)](./name-property-adox.md) property.  
  
-   Specify the data type of the column with the [Type Property (Key) (ADOX)](./type-property-key-adox.md) property.  
  
-   Determine if the column is fixed-length, or if it can contain null values with the [Attributes Property (ADOX)](./attributes-property-adox.md) property.  
  
-   Specify the maximum size of the column with the [DefinedSize Property (ADOX)](./definedsize-property-adox.md) property.  
  
-   For numeric data values, specify the scale with the [NumericScale Property (ADOX)](./numericscale-property-adox.md) property.  
  
-   For numeric data value, specify the maximum precision with the [Precision Property (ADOX)](./precision-property-adox.md) property.  
  
-   Specify the [Catalog Object (ADOX)](./catalog-object-adox.md) that owns the column with the [ParentCatalog Property (ADOX)](./parentcatalog-property-adox.md) property.  
  
-   For key columns, specify the name of the related column in the related table with the [RelatedColumn Property (ADOX)](./relatedcolumn-property-adox.md) property.  
  
-   For index columns, specify whether the sort order is ascending or descending with the [SortOrder Property (ADOX)](./sortorder-property-adox.md) property.  
  
-   Access provider-specific properties with the [Properties Collection (ADO)](../ado-api/properties-collection-ado.md) collection.  
  
> [!NOTE]
>  Not all properties of **Column** objects may be supported by your data provider. An error will occur if you have set a value for a property that the provider does not support. For new **Column** objects, the error will occur when the object is appended to the collection. For existing objects, the error will occur when setting the property.  
>   
>  When creating **Column** objects, the existence of an appropriate default value for an optional property does not guarantee that your provider supports the property. For more information about which properties your provider supports, see your provider documentation.  
  
 This section contains the following topic.  
  
-   [Column Object Properties, Methods, and Events](./column-object-properties-methods-and-events.md)  
  
## See Also  
 [Columns and Tables Append Methods, Name Property Example (VB)](./columns-and-tables-append-methods-name-property-example-vb.md)   
 [Connection Close Method, Table Type Property Example (VB)](./connection-close-method-table-type-property-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [ADOX Code Example: NumericScale and Precision Properties Example (VB)](./adox-code-example-numericscale-and-precision-properties-example-vb.md)   
 [ParentCatalog Property Example (VB)](./parentcatalog-property-example-vb.md)   
 [SortOrder Property Example (VB)](./sortorder-property-example-vb.md)   
 [Columns Collection (ADOX)](./columns-collection-adox.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)