---
description: "CREATE INDEX for Paradox"
title: "CREATE INDEX for Paradox | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "CREATE INDEX [ODBC]"
  - "Paradox driver [ODBC], create index"
ms.assetid: 6472bd69-b931-4bc2-a9bf-f1873ed4cdfe
author: David-Engel
ms.author: v-davidengel
---
# CREATE INDEX for Paradox
The syntax of the CREATE INDEX statement for the ODBC Paradox driver is:  
  
 **CREATE** [**UNIQUE**] **INDEX** *index-name*  
  
 **ON** *table-name*  
  
 **(** *column-identifier* [**ASC**]  
  
 [**,** *column-identifier* [**ASC**]...]**)**  
  
 The ODBC Paradox driver does not support the **DESC** keyword in the ODBC SQL grammar for the CREATE INDEX statement. The *table-name* argument can specify the full path of the table.  
  
 If the keyword **UNIQUE** is specified, the ODBC Paradox driver will create a unique index. The first unique index is created as a primary index. This is a Paradox primary key file named *table-name*.PX. Primary indexes are subject to the following restrictions:  
  
-   The primary index must be created before any rows are added to the table.  
  
-   A primary index must be defined upon the first "n" columns in a table.  
  
-   Only one primary index is allowed per table.  
  
-   A table cannot be updated by the Paradox driver if a primary index is not defined on the table. (Note that this is not true for an empty table, which can be updated even if a unique index is not defined on the table.)  
  
-   The *index-name* argument for a primary index must be the same as the base name of the table, as required by Paradox.  
  
 If the keyword **UNIQUE** is omitted, the ODBC Paradox driver will create a non-unique index. This consists of two Paradox secondary index files named *table-name*.X*nn* and *table-name*.Y*nn*, where *nn* is the number of the column in the table. Non-unique indexes are subject to the following restrictions:  
  
-   Before a non-unique index can be created for a table, a primary index must exist for that table.  
  
-   For Paradox 3.*x*, the *index-name* argument for any index other than a primary index (unique or non-unique) must be the same as the column name. For Paradox 4.*x* and 5.*x*, the name of such an index can be, but doesn't have to be, the same as the column name.  
  
-   Only one column can be specified for a non-unique index.  
  
 Columns cannot be added once an index has been defined on a table. If the first column of the argument list of a CREATE TABLE statement creates an index, a second column cannot be included in the argument list.  
  
 For example, to use the sales order number and line number columns as the unique index on the SO_LINES table, use the statement:  
  
```  
CREATE UNIQUE INDEX SO_LINES  
 ON SO_LINES (SONum, LineNum)  
```  
  
 To use the part number column as a non-unique index on the SO_LINES table, use the statement:  
  
```  
CREATE INDEX PartNum  
 ON SO_LINES (PartNum)  
```  
  
 Note that when two CREATE INDEX statements are performed, the first statement will always create a primary index with the same name as the table and the second statement will always create a non-unique index with the same name as the column. These indexes will be named this way even if different names are entered in the CREATE INDEX statements and even if the index is labeled UNIQUE in the second CREATE INDEX statement.  
  
> [!NOTE]  
>  When you use the Paradox driver without implementing the Borland Database Engine, only read and append statements are allowed.
