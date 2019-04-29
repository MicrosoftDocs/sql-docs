---
title: "Index Object (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Index"
helpviewer_keywords: 
  - "Index object [ADOX]"
ms.assetid: 6b9578c0-bc94-46b9-b801-c18e14b04b31
author: MightyPen
ms.author: genemi
manager: craigg
---
# Index Object (ADOX)
Represents an index from a database table.  
  
## Remarks  
 The following code creates a new **Index**:  
  
```  
Dim obj As New Index  
```  
  
 With the properties and collections of an **Index** object, you can:  
  
-   Identify the index with the [Name](../../../ado/reference/adox-api/name-property-adox.md) property.  
  
-   Access the database columns of the index with the [Columns](../../../ado/reference/adox-api/columns-collection-adox.md) collection.  
  
-   Specify whether the index keys must be unique with the [Unique](../../../ado/reference/adox-api/unique-property-adox.md) property.  
  
-   Specify whether the index is the primary key for a table with the [PrimaryKey](../../../ado/reference/adox-api/primarykey-property-adox.md) property.  
  
-   Specify whether records that have null values in their index fields have index entries with the [IndexNulls](../../../ado/reference/adox-api/indexnulls-property-adox.md) property.  
  
-   Specify whether the index is clustered with the [Clustered](../../../ado/reference/adox-api/clustered-property-adox.md) property.  
  
-   Access provider-specific index properties with the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection.  
  
> [!NOTE]
>  An error will occur when appending a [Column](../../../ado/reference/adox-api/column-object-adox.md) to the **Columns** collection of an **Index** if the **Column** does not exist in a [Table](../../../ado/reference/adox-api/table-object-adox.md) object already appended to the [Tables](../../../ado/reference/adox-api/tables-collection-adox.md) collection.  
  
> [!NOTE]
>  Your data provider may not support all properties of **Index** objects. An error will occur if you have set a value for a property that is not supported by the provider. For new **Index** objects, the error will occur when the object is appended to the collection. For existing objects, the error will occur when setting the property.  
  
> [!NOTE]
>  When creating **Index** objects, the existence of an appropriate default value for an optional property does not guarantee that your provider supports the property. For more information about which properties your provider supports, see your provider documentation.  
  
 This section contains the following topic.  
  
-   [Index Object Properties, Methods, and Events](../../../ado/reference/adox-api/index-object-properties-methods-and-events.md)  
  
## See Also  
 [Indexes Append Method Example (VB)](../../../ado/reference/adox-api/indexes-append-method-example-vb.md)   
 [IndexNulls Property Example (VB)](../../../ado/reference/adox-api/indexnulls-property-example-vb.md)   
 [PrimaryKey and Unique Properties Example (VB)](../../../ado/reference/adox-api/primarykey-and-unique-properties-example-vb.md)   
 [SortOrder Property Example (VB)](../../../ado/reference/adox-api/sortorder-property-example-vb.md)   
 [Columns Collection (ADOX)](../../../ado/reference/adox-api/columns-collection-adox.md)   
 [Indexes Collection (ADOX)](../../../ado/reference/adox-api/indexes-collection-adox.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
