---
title: "Append Method (ADOX Keys) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Keys::Append"
  - "Keys::raw_Append"
helpviewer_keywords: 
  - "Append method [ADOX]"
ms.assetid: 215a5391-f422-42ec-99ea-4e6fbb5d3d64
author: MightyPen
ms.author: genemi
manager: craigg
---
# Append Method (ADOX Keys)
Adds a new [Key](../../../ado/reference/adox-api/key-object-adox.md) object to the [Keys](../../../ado/reference/adox-api/keys-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Keys.Append Key [,KeyType] [,Column] [,RelatedTable] [,RelatedColumn]  
```  
  
#### Parameters  
 *Key*  
 The **Key** object to append or the name of the key to create and append.  
  
 *KeyType*  
 Optional. A **Long** value that specifies the type of key. The *Key* parameter corresponds to the [Type](../../../ado/reference/adox-api/type-property-key-adox.md) property of a **Key** object.  
  
 *Column*  
 Optional. A **String** value that specifies the name of the column to be indexed. The *Columns* parameter corresponds to the value of the [Name](../../../ado/reference/adox-api/name-property-adox.md) property of a [Column](../../../ado/reference/adox-api/column-object-adox.md) object.  
  
 *RelatedTable*  
 Optional. A **String** value that specifies the name of the related table. The *RelatedTable* parameter corresponds to the value of the **Name** property of a [Table](../../../ado/reference/adox-api/table-object-adox.md) object.  
  
 *RelatedColumn*  
 Optional. A **String** value that specifies the name of the related column for a foreign key. The *RelatedColumn* parameter corresponds to the value of the **Name** property of a [Column](../../../ado/reference/adox-api/column-object-adox.md) object.  
  
## Remarks  
 The *Columns* parameter can take either the name of a column or an array of column names.  
  
## Applies To  
 [Keys Collection (ADOX)](../../../ado/reference/adox-api/keys-collection-adox.md)  
  
## See Also  
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](../../../ado/reference/adox-api/keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [Append Method (ADOX Columns)](../../../ado/reference/adox-api/append-method-adox-columns.md)   
 [Append Method (ADOX Groups)](../../../ado/reference/adox-api/append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](../../../ado/reference/adox-api/append-method-adox-indexes.md)   
 [Append Method (ADOX Procedures)](../../../ado/reference/adox-api/append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](../../../ado/reference/adox-api/append-method-adox-tables.md)   
 [Append Method (ADOX Users)](../../../ado/reference/adox-api/append-method-adox-users.md)   
 [Append Method (ADOX Views)](../../../ado/reference/adox-api/append-method-adox-views.md)
