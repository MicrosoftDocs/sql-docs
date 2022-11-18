---
title: "Name Property (ADOX)"
description: "Name Property (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
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
apitype: "COM"
---
# Name Property (ADOX)
Indicates the name of the object.  
  
## Settings and Return Values  
 Sets or returns a **String** value.  
  
## Remarks  
 Names do not have to be unique within a collection.  
  
 The **Name** property is read/write on [Column](./column-object-adox.md), [Group](./group-object-adox.md), [Key](./key-object-adox.md), [Index](./index-object-adox.md), [Table](./table-object-adox.md), and [User](./user-object-adox.md) objects. The **Name** property is read-only on [Catalog](./catalog-object-adox.md), [Procedure](./procedure-object-adox.md), and [View](./view-object-adox.md) objects.  
  
 For read/write objects (**Column**, **Group**, **Key**, **Index**, **Table** and **User** objects), the default value is an empty string ("").  
  
> [!NOTE]
>  For keys, this property is read-only on **Key** objects already appended to a collection. For tables, this property is read-only for **Table** objects already appended to a collection.  
  
## Applies To  

:::row:::
    :::column:::
        [Column Object (ADOX)](./column-object-adox.md)  
        [Group Object (ADOX)](./group-object-adox.md)  
        [Index Object (ADOX)](./index-object-adox.md)  
    :::column-end:::
    :::column:::
        [Key Object (ADOX)](./key-object-adox.md)  
        [Procedure Object (ADOX)](./procedure-object-adox.md)  
        [Property Object (ADO)](../ado-api/property-object-ado.md)  
    :::column-end:::
    :::column:::
        [Table Object (ADOX)](./table-object-adox.md)  
        [User Object (ADOX)](./user-object-adox.md)  
        [View Object (ADOX)](./view-object-adox.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Columns and Tables Append Methods, Name Property Example (VB)](./columns-and-tables-append-methods-name-property-example-vb.md)   
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [ParentCatalog Property Example (VB)](./parentcatalog-property-example-vb.md)