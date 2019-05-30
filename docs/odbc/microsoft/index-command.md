---
title: "INDEX Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "index command [ODBC]"
ms.assetid: 694e8cf5-2f69-4001-9c1e-b735a4da3aff
author: MightyPen
ms.author: genemi
manager: craigg
---
# INDEX Command
Creates an index file to display and access table records in a logical order.  
  
## Syntax  
  
```  
  
INDEX ON eExpression TO IDXFileName | TAG TagName [OF CDXFileName]  
   [FOR lExpression]  
   [COMPACT]  
   [ASCENDING | DESCENDING]  
   [UNIQUE | CANDIDATE]  
   [ADDITIVE]  
```  
  
## Arguments  
 *eExpression*  
 Specifies an index expression that can include the name of a field or fields from the current table. An index key based on the index expression is created in the index file for each record in the table. Visual FoxPro uses these keys to display and access records in the table.  
  
> [!NOTE]  
>  Although not recommended, *eExpression* can also be a memory variable, an array element, or a field or field expression from a table in another work area. Memo fields cannot be used alone in index file expressions; they must be combined with other character expressions. If you access an index that contains a variable or field that no longer exists or cannot be located, Visual FoxPro generates an error message.  
  
 If you attempt to build an index with a key that varies in length, the key will be padded with spaces. Variable-length index keys aren't supported in Visual FoxPro.  
  
 It is possible to create an index key with zero length. For example, a zero length index key is created when the index expression is a substring of an empty memo field. A zero length index key generates an error message. When Visual FoxPro creates an index, it evaluates fields in the first record in the table. If a field is empty, it might be necessary to enter some temporary data in the field in the first record to prevent a 0-length index key.  
  
 TO *IDXFileName*  
 Creates an .idx index file. The index file is given the default extension .idx.  
  
 TAG *TagName*[OF *CDXFileName*]  
 Creates a compound index file. A compound index file is a single index file that consists of any number of separate tags (index entries). Each tag is identified by its unique tag name. Tag names must begin with a letter or an underscore and can consist of any combination of up to 10 letters, digits, or underscores. The number of tags in a compound index file is limited only by available memory and disk space.  
  
 Multiple-entry compound index files are always compact. It isn't necessary to include COMPACT when creating a compound index file. Names of compound index files are given a .cdx extension.  
  
 Two types of compound index files can be created: structural and nonstructural.  
  
 **Structural Compound Index Files** You can create a structural compound index file with TAG *TagName* by excluding the optional OF *CDXFileName* clause. A structural compound index file always has the same base name as the table and is automatically opened when the table is opened.  
  
 **Nonstructural Compound Index Files** You can create a nonstructural compound index file by including OF *CDXFileName* after TAG *TagName*. Unlike a structural compound index file, a nonstructural compound index file must be explicitly opened with the INDEX clause in USE.  
  
 If a compound index file has already been created and opened, issuing INDEX with TAG *TagName* adds a tag to the compound index file.  
  
 FOR *lExpression*  
 Specifies a condition whereby only records that satisfy the filter expression *lExpression* are available for display and access; index keys are created in the index file for just those records matching the filter expression.  
  
 Visual FoxPro Rushmore technology optimizes an INDEX ... FOR *lExpression* command if *lExpression* is an optimizable expression. For best performance, use an optimizable expression in the FOR clause.  
  
 COMPACT  
 Creates a compact .idx file.  
  
 ASCENDING  
 Specifies an ascending order for the .cdx file. By default, .cdx tags are created in ascending order. (You can include ASCENDING as a reminder of the index file's order.) A table can be indexed in reverse order by including DESCENDING.  
  
 DESCENDING  
 Specifies a descending order for the .cdx file. You can't include DESCENDING when creating .idx index files.  
  
 UNIQUE  
 Specifies that only the first record encountered with a particular index key value is included in an .idx file or a .cdx tag. UNIQUE can be used to prevent the display of or access to duplicate records. All records added with duplicate index keys are excluded from the index file. Using the UNIQUE option of INDEX is identical to executing SET UNIQUE ON before issuing INDEX or REINDEX.  
  
 When a UNIQUE index or index tag is active and a duplicate record is changed in a manner that changes its index key, the index or index tag is updated. However, the next duplicate record with the original index key cannot be accessed or displayed until you reindex the file using REINDEX.  
  
 CANDIDATE  
 Creates a candidate structural index tag. The CANDIDATE keyword can be included only when creating a structural index tag; otherwise, Visual FoxPro generates an error message.  
  
 A candidate index tag prevents duplicate values in the field or combination of fields specified in the index expression *eExpression*. The term *candidate* refers to the type of index; because candidate indexes prevent duplicate values, they qualify as a "candidate" to be a primary index.  
  
 Visual FoxPro generates an error if you create a candidate index tag for a field or combination of fields that already contains duplicate values.  
  
 ADDITIVE  
 Keeps open any previously opened index files. If you omit the ADDITIVE clause when you create an index file or files for a table with INDEX, any previously opened index files (except the structural compound index) are closed.  
  
## Remarks  
 Records in a table that has an index file are displayed and accessed in the order specified by the index expression. The physical order of the records in the table isn't changed by an index file.  
  
## Index Types  
 Visual FoxPro lets you create two types of index files:  
  
-   Compound .cdx index files containing multiple index entries called tags  
  
-   .idx index files containing one index entry  
  
 You can also create a structural compound index file, which is automatically opened with the table.  
  
> [!NOTE]  
>  Because structural compound index files are automatically opened when the table is opened, they are the preferred index type.  
  
 Include COMPACT to create compact .idx index files. Compound index files are always compact.  
  
## Index Order and Updating  
 Only one index file (the master index file) or tag (the master tag) controls the order in which the table is displayed or accessed. Certain commands (SEEK, for example) use the master index file or tag to search for records. However, all open .idx and .cdx index files are updated as changes are made to the table.  
  
## User-Defined Functions  
 Although an index expression can contain a user-defined function, you should not use user-defined functions in an index expression. User-defined functions in an index expression increase the time it takes to create or update the index. Also, index updates might not occur when a user-defined function is used for an index expression.  
  
 If you use a user-defined function in an index expression, Visual FoxPro must be able to locate the user-defined function. When Visual FoxPro creates an index, the index expression is saved in the index file but only a reference to the user-defined function is included in the index expression.  
  
## See Also  
 [ALTER TABLE - SQL Command](../../odbc/microsoft/alter-table-sql-command.md)   
 [DELETE TAG Command](../../odbc/microsoft/delete-tag-command.md)   
 [SET COLLATE Command](../../odbc/microsoft/set-collate-command.md)   
 [SET UNIQUE Command](../../odbc/microsoft/set-unique-command.md)
