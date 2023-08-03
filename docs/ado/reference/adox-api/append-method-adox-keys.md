---
title: "Append Method (ADOX Keys)"
description: "Append Method (ADOX Keys)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Keys::Append"
  - "Keys::raw_Append"
helpviewer_keywords:
  - "Append method [ADOX]"
apitype: "COM"
---
# Append Method (ADOX Keys)
Adds a new [Key](./key-object-adox.md) object to the [Keys](./keys-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Keys.Append Key [,KeyType] [,Column] [,RelatedTable] [,RelatedColumn]  
```  
  
#### Parameters  
 *Key*  
 The **Key** object to append or the name of the key to create and append.  
  
 *KeyType*  
 Optional. A **Long** value that specifies the type of key. The *Key* parameter corresponds to the [Type](./type-property-key-adox.md) property of a **Key** object.  
  
 *Column*  
 Optional. A **String** value that specifies the name of the column to be indexed. The *Columns* parameter corresponds to the value of the [Name](./name-property-adox.md) property of a [Column](./column-object-adox.md) object.  
  
 *RelatedTable*  
 Optional. A **String** value that specifies the name of the related table. The *RelatedTable* parameter corresponds to the value of the **Name** property of a [Table](./table-object-adox.md) object.  
  
 *RelatedColumn*  
 Optional. A **String** value that specifies the name of the related column for a foreign key. The *RelatedColumn* parameter corresponds to the value of the **Name** property of a [Column](./column-object-adox.md) object.  
  
## Remarks  
 The *Columns* parameter can take either the name of a column or an array of column names.  
  
## Applies To  
 [Keys Collection (ADOX)](./keys-collection-adox.md)  
  
## See Also  
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [Append Method (ADOX Columns)](./append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](./append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](./append-method-adox-indexes.md)   
 [Append Method (ADOX Procedures)](./append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](./append-method-adox-tables.md)   
 [Append Method (ADOX Users)](./append-method-adox-users.md)   
 [Append Method (ADOX Views)](./append-method-adox-views.md)