---
title: "Append Method (ADOX Columns)"
description: "Append Method (ADOX Columns)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Columns::raw_Append"
  - "Columns::Append"
helpviewer_keywords:
  - "Append method [ADOX]"
apitype: "COM"
---
# Append Method (ADOX Columns)
Adds a new [Column](./column-object-adox.md) object to the [Columns](./columns-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Columns.Append Column [,Type] [,DefinedSize]  
```  
  
#### Parameters  
 *Column*  
 The **Column** object to append or the name of the column to create and append.  
  
 *Type*  
 Optional. A **Long** value that specifies the data type of the column. The *Type* parameter corresponds to the [Type](./type-property-column-adox.md) property of a **Column** object.  
  
 *DefinedSize*  
 Optional. A **Long** value that specifies the size of the column. The *DefinedSize* parameter corresponds to the [DefinedSize](./definedsize-property-adox.md) property of a **Column** object.  
  
> [!NOTE]
>  An error will occur when appending a **Column** to the **Columns** collection of an [Index](./index-object-adox.md) if the **Column** does not exist in a [Table](./table-object-adox.md) that is already appended to the [Tables](./tables-collection-adox.md) collection.  
  
## Applies To  
 [Columns Collection (ADOX)](./columns-collection-adox.md)  
  
## See Also  
 [Columns and Tables Append Methods, Name Property Example (VB)](./columns-and-tables-append-methods-name-property-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [ParentCatalog Property Example (VB)](./parentcatalog-property-example-vb.md)   
 [Append Method (ADOX Groups)](./append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](./append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](./append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](./append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](./append-method-adox-tables.md)   
 [Append Method (ADOX Users)](./append-method-adox-users.md)   
 [Append Method (ADOX Views)](./append-method-adox-views.md)