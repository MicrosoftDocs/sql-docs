---
description: "dBASE Indexes"
title: "dBASE Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "DBase indexes [ODBC]"
  - "DBase driver [ODBC], indexes"
ms.assetid: fdfa56f5-e324-4ec2-9267-fdf95ab99373
author: David-Engel
ms.author: v-davidengel
---
# dBASE Indexes
The ODBC dBASE driver automatically opens and updates dBASE IV index files. You must use the **Select Indexes** dialog box displayed through the ODBC Data Source Administrator to associate dBASE III .ndx files with dBASE files.  
  
 The following limitations apply to the creation of dBASE indexes:  
  
-   All column names must be valid.  
  
-   All columns must be in the same ascending or descending order.  
  
-   The length of any single text column must be less than 100 bytes.  
  
-   If more than one column exists, all of the columns must be text columns and the sum of the column sizes must be less than 100 bytes.  
  
-   Memo fields cannot be indexed.  
  
-   An index must not be specified for the current set of fields (that is, duplicate indexes are not allowed).  
  
-   The index name must match the dBASE index naming convention. dBASE III requires that each index be in a separate file, each having an .ndx extension. In dBASE IV, indexes are created as tag names that are stored in a single .mdx file. The .mdx file has the same base name as the database file (for example, Emp.mdx is the index file for the Emp.dbf database).  
  
-   dBASE defines a unique index as one where only one record from a set with identical key values is added to the index.
