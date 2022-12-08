---
title: "ParentCatalog Property (ADOX)"
description: "ParentCatalog Property (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_User::get_ParentCatalog"
  - "_Column::ParentCatalog"
  - "_Table::put_ParentCatalog"
  - "_Group::put_ParentCatalog"
  - "_Column::get_ParentCatalog"
  - "_Table::PutParentCatalog"
  - "_Group::putref_ParentCatalog"
  - "_Group::ParentCatalog"
  - "_Column::PutParentCatalog"
  - "_Column::put_ParentCatalog"
  - "_Group::get_ParentCatalog"
  - "_User::put_ParentCatalog"
  - "_User::putref_ParentCatalog"
  - "_Table::get_ParentCatalog"
  - "_Group::PutParentCatalog"
  - "_Table::ParentCatalog"
  - "_Column::GetParentCatalog"
  - "_Table::PutRefParentCatalog"
  - "_User::GetParentCatalog"
  - "_Table::GetParentCatalog"
  - "_Table::putref_ParentCatalog"
  - "_User::PutParentCatalog"
  - "_User::ParentCatalog"
  - "_User::PutRefParentCatalog"
  - "_Group::GetParentCatalog"
  - "_Group::PutRefParentCatalog"
helpviewer_keywords:
  - "ParentCatalog property [ADOX]"
apitype: "COM"
---
# ParentCatalog Property (ADOX)
Specifies the parent catalog of a Table, User, or Column object to provide access to provider-specific properties.  
  
## Settings and Return Values  
 Sets and returns a [Catalog](./catalog-object-adox.md) object. Setting **ParentCatalog** to an open **Catalog** allows access to provider-specific properties prior to appending a table or column to a **Catalog** collection.  
  
## Remarks  
 Some data providers allow provider-specific property values to be written only at creation: that is, when a table or column is appended to its **Catalog** collection. To access these properties before appending these objects to a **Catalog**, specify the **Catalog** in the **ParentCatalog** property first.  
  
 An error occurs when the table or column is appended to a different **Catalog** than the **ParentCatalog**.  
  
## Applies To  

:::row:::
    :::column:::
        [Column Object (ADOX)](./column-object-adox.md)  
    :::column-end:::
    :::column:::
        [Table Object (ADOX)](./table-object-adox.md)  
    :::column-end:::
    :::column:::
        [User Object (ADOX)](./user-object-adox.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [ParentCatalog Property Example (VB)](./parentcatalog-property-example-vb.md)