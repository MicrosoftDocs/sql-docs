---
title: "Tabular model object-level security | Microsoft Docs"
ms.custom: ""
ms.date: "06/20/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
ms.assetid:
caps.latest.revision: 
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Object-level security

[!INCLUDE[ssas-appliesto-sql2017-later-aas](../../includes/ssas-appliesto-sql2017-later-aas.md)]

Data model security starts with effectively implementing [roles](../../analysis-services/tabular-models/roles-ssas-tabular.md) and row-level filters to define user permissions on data model objects and data. Beginning with tabular 1400 models, you can also define object-level security, which collectively includes table-level security and column-level security in the [Roles object](../../analysis-services/tabular-models-scripting-language-objects/roles-object-tmsl.md).

## Table-level security

With table-level security, you can not only restrict access to table data, but also sensitive table names, helping prevent malicious users from discovering if a table exists. 

> [!IMPORTANT]  
> A relationship chain cannot pass through a secured table. For example, if there is a relationship between tables A and B, and B and C, you cannot secure table B. If table B is secured, a query on table A could not transit the relationship between table A and B, and table B and C.

 Table-level security is set in the JSON-based metadata in the Model.bim, [Tabular Model Scripting Language (TMSL)](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md), or [Tabular Object Model (TOM)](../../analysis-services/tabular-model-programming-compatibility-level-1200/introduction-to-the-tabular-object-model-tom-in-analysis-services-amo.md). Set the **metadataPermission** property of the **tablePermissions** class in the [Roles object](../../analysis-services/tabular-models-scripting-language-objects/roles-object-tmsl.md) to **none**.

In this example, the metadataPermission property of the tablePermissions class for the Product table is set to none:

```
"roles": [
  {
    "name": "Users",
    "description": "All allowed users to query the model",
    "modelPermission": "read",
    "tablePermissions": [
      {
        "name": "Product",
        "metadataPermission": "none"
      }
    ]
  }
```

## Column-level security

Similar to table-level security, with column-level security you can not only restrict access to column data, but also sensitive column names,  helping prevent malicious users from discovering a column.

 Column-level security is set in the JSON-based metadata in the Model.bim, [Tabular Model Scripting Language (TMSL)](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md), or [Tabular Object Model (TOM)](../../analysis-services/tabular-model-programming-compatibility-level-1200/introduction-to-the-tabular-object-model-tom-in-analysis-services-amo.md). Set the **metadataPermission** property of the **columnPermissions** class in the [Roles object](../../analysis-services/tabular-models-scripting-language-objects/roles-object-tmsl.md) to **none**.

In this example, the metadataPermission property of the columnPermissions class for the Base Rate column in the Employees table is set to none:

```
"roles": [
  {
    "name": "Users",
    "description": "All allowed users to query the model",
    "modelPermission": "read",
    "tablePermissions": [
      {
        "name": "Employee",
        "columnPermissions": [
          {
            "name": "Base Rate",
            "metadataPermission": "none"
          }
        ]
      }
    ]
  }
```




## See Also  
[Roles](../../analysis-services/tabular-models/roles-ssas-tabular.md)  
[Roles object (TMSL)](../../analysis-services/tabular-models-scripting-language-objects/roles-object-tmsl.md)  
[Tabular Model Scripting Language (TMSL)](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)  
[Tabular Object Model (TOM)](../../analysis-services/tabular-model-programming-compatibility-level-1200/introduction-to-the-tabular-object-model-tom-in-analysis-services-amo.md).

  