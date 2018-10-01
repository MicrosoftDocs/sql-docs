---
title: "Name Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Table::PutName"
  - "_Table::GetName"
  - "_Key::Name"
  - "_Key::get_Name"
  - "_Column::GetName"
  - "_Index::Name"
  - "_Index::put_Name"
  - "_Column::PutName"
  - "_Key::put_Name"
  - "_Table::put_Name"
  - "_User25::PutName"
  - "_Index::get_Name"
  - "_Column::get_Name"
  - "_Group25::Name"
  - "_Group25::get_Name"
  - "_Column::Name"
  - "_User25::get_Name"
  - "_Table::Name"
  - "_Group25::GetName"
  - "_Index::PutName"
  - "_Column::put_Name"
  - "_Key::GetName"
  - "_Table::get_Name"
  - "_User25::Name"
  - "_User25::put_Name"
  - "_Index::GetName"
  - "_User25::GetName"
helpviewer_keywords: 
  - "Name property [ADOX]"
ms.assetid: 81b92baf-b6b9-4f4e-9f33-4503795518cd
author: MightyPen
ms.author: genemi
manager: craigg
---
# Name Property (ADOX)
Indicates the name of the object.  
  
## Settings and Return Values  
 Sets or returns a **String** value.  
  
## Remarks  
 Names do not have to be unique within a collection.  
  
 The **Name** property is read/write on [Column](../../../ado/reference/adox-api/column-object-adox.md), [Group](../../../ado/reference/adox-api/group-object-adox.md), [Key](../../../ado/reference/adox-api/key-object-adox.md), [Index](../../../ado/reference/adox-api/index-object-adox.md), [Table](../../../ado/reference/adox-api/table-object-adox.md), and [User](../../../ado/reference/adox-api/user-object-adox.md) objects. The **Name** property is read-only on [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md), [Procedure](../../../ado/reference/adox-api/procedure-object-adox.md), and [View](../../../ado/reference/adox-api/view-object-adox.md) objects.  
  
 For read/write objects (**Column**, **Group**, **Key**, **Index**, **Table** and **User** objects), the default value is an empty string ("").  
  
> [!NOTE]
>  For keys, this property is read-only on **Key** objects already appended to a collection. For tables, this property is read-only for **Table** objects already appended to a collection.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Column Object (ADOX)](../../../ado/reference/adox-api/column-object-adox.md)|[Group Object (ADOX)](../../../ado/reference/adox-api/group-object-adox.md)|[Index Object (ADOX)](../../../ado/reference/adox-api/index-object-adox.md)|  
|[Key Object (ADOX)](../../../ado/reference/adox-api/key-object-adox.md)|[Procedure Object (ADOX)](../../../ado/reference/adox-api/procedure-object-adox.md)|[Property Object (ADO)](../../../ado/reference/ado-api/property-object-ado.md)|  
|[Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)|[User Object (ADOX)](../../../ado/reference/adox-api/user-object-adox.md)|[View Object (ADOX)](../../../ado/reference/adox-api/view-object-adox.md)|  
  
## See Also  
 [Columns and Tables Append Methods, Name Property Example (VB)](../../../ado/reference/adox-api/columns-and-tables-append-methods-name-property-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](../../../ado/reference/adox-api/keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [ParentCatalog Property Example (VB)](../../../ado/reference/adox-api/parentcatalog-property-example-vb.md)
