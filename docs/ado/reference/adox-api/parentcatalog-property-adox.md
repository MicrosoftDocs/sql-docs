---
title: "ParentCatalog Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
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
ms.assetid: a0bb2ed8-d4cb-4f92-8de7-769bbe0e6273
author: MightyPen
ms.author: genemi
manager: craigg
---
# ParentCatalog Property (ADOX)
Specifies the parent catalog of a Table, User, or Column object to provide access to provider-specific properties.  
  
## Settings and Return Values  
 Sets and returns a [Catalog](../../../ado/reference/adox-api/catalog-object-adox.md) object. Setting **ParentCatalog** to an open **Catalog** allows access to provider-specific properties prior to appending a table or column to a **Catalog** collection.  
  
## Remarks  
 Some data providers allow provider-specific property values to be written only at creation: that is, when a table or column is appended to its **Catalog** collection. To access these properties before appending these objects to a **Catalog**, specify the **Catalog** in the **ParentCatalog** property first.  
  
 An error occurs when the table or column is appended to a different **Catalog** than the **ParentCatalog**.  
  
## Applies To  
  
|||  
|-|-|  
|[Column Object (ADOX)](../../../ado/reference/adox-api/column-object-adox.md)|[Table Object (ADOX)](../../../ado/reference/adox-api/table-object-adox.md)|  
|[User Object (ADOX)](../../../ado/reference/adox-api/user-object-adox.md)||  
  
## See Also  
 [ParentCatalog Property Example (VB)](../../../ado/reference/adox-api/parentcatalog-property-example-vb.md)
