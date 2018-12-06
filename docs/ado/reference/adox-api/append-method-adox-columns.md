---
title: "Append Method (ADOX Columns) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Columns::raw_Append"
  - "Columns::Append"
helpviewer_keywords: 
  - "Append method [ADOX]"
ms.assetid: 7a46d23c-efef-4ec7-815d-cd3ac86787dd
author: MightyPen
ms.author: genemi
manager: craigg
---
# Append Method (ADOX Columns)
Adds a new [Column](../../../ado/reference/adox-api/column-object-adox.md) object to the [Columns](../../../ado/reference/adox-api/columns-collection-adox.md) collection.  
  
## Syntax  
  
```  
  
Columns.Append Column [,Type] [,DefinedSize]  
```  
  
#### Parameters  
 *Column*  
 The **Column** object to append or the name of the column to create and append.  
  
 *Type*  
 Optional. A **Long** value that specifies the data type of the column. The *Type* parameter corresponds to the [Type](../../../ado/reference/adox-api/type-property-column-adox.md) property of a **Column** object.  
  
 *DefinedSize*  
 Optional. A **Long** value that specifies the size of the column. The *DefinedSize* parameter corresponds to the [DefinedSize](../../../ado/reference/adox-api/definedsize-property-adox.md) property of a **Column** object.  
  
> [!NOTE]
>  An error will occur when appending a **Column** to the **Columns** collection of an [Index](../../../ado/reference/adox-api/index-object-adox.md) if the **Column** does not exist in a [Table](../../../ado/reference/adox-api/table-object-adox.md) that is already appended to the [Tables](../../../ado/reference/adox-api/tables-collection-adox.md) collection.  
  
## Applies To  
 [Columns Collection (ADOX)](../../../ado/reference/adox-api/columns-collection-adox.md)  
  
## See Also  
 [Columns and Tables Append Methods, Name Property Example (VB)](../../../ado/reference/adox-api/columns-and-tables-append-methods-name-property-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](../../../ado/reference/adox-api/keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [ParentCatalog Property Example (VB)](../../../ado/reference/adox-api/parentcatalog-property-example-vb.md)   
 [Append Method (ADOX Groups)](../../../ado/reference/adox-api/append-method-adox-groups.md)   
 [Append Method (ADOX Indexes)](../../../ado/reference/adox-api/append-method-adox-indexes.md)   
 [Append Method (ADOX Keys)](../../../ado/reference/adox-api/append-method-adox-keys.md)   
 [Append Method (ADOX Procedures)](../../../ado/reference/adox-api/append-method-adox-procedures.md)   
 [Append Method (ADOX Tables)](../../../ado/reference/adox-api/append-method-adox-tables.md)   
 [Append Method (ADOX Users)](../../../ado/reference/adox-api/append-method-adox-users.md)   
 [Append Method (ADOX Views)](../../../ado/reference/adox-api/append-method-adox-views.md)
