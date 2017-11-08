---
title: Code snippets in SQL Operations Studio | Microsoft Docs
description: Learn how to use SQL code snippets in SQL Operations Studio
keywords: 
ms.custom: "tools|sos"
ms.date: "11/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# Use code snippets to quickly create SQL scripts in [!INCLUDE[name-sos](../includes/name-sos-short.md)]

Code snippets in [!INCLUDE[name-sos](../includes/name-sos-short.md)] are templates that make it easier to create databases and database objects. 

## Using built-in SQL code snippets

[!INCLUDE[name-sos](../includes/name-sos-short.md)] provides several SQL snippets to assist you with quickly generating the proper syntax. 

1. To access the available snippets, type *sql* in the query editor to open the list:

   ![sql snippets](media/code-snippets/sql-snippets.png)

1. Select the snippet you want to use, and it generates the SQL script. For example, select *sqlCreateTable*:

   ![sql snippets](media/code-snippets/create-table.png)

1. Update the highlighted fields with your specific values. For example, replace *TableName* and *Schema* with the values for your database:

   ![replace template field](media/code-snippets/table-from-snippet.png)

   If the field you want to change is no longer highlighted (this happens when moving the cursor around the editor), right-click the word you want to change, and select **Change all occurrences**:

   ![replace template field](media/code-snippets/change-all.png)

1. Update or add any additional SQL you need for the selected snippet. For example, update *Column1*, *Column2*, and add more columns.


## Next steps

[Code editor tutorial](tutorial-sql-editor.md)