---
title: "Query Options Execution (ANSI Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.query.ansi.f1"
ms.assetid: c90d7cdf-3309-46f4-b900-220521bb9552
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Query Options Execution (ANSI Page)
  Use this page to specify that [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] will run the queries using all or a portion of the settings specified in the ISO (ANSI) standard.  
  
## UIElement List  
 **SET ANSI_DEFAULTS**  
 Select all of the default ISO settings. This box is unavailable by default, because only some of the ISO settings are configured.  
  
 **SET QUOTED_IDENTIFIER**  
 Surround object identifiers with quotation marks. This option is selected by default.  
  
 **SET ANSI_NULL_DFLT_ON**  
 Allow null values for all user-defined data types or columns that are not explicitly defined as NOTNULL during a CREATE TABLE or ALTER TABLE statement (the default state). This option is selected by default.  
  
 **SET IMPLICIT_TRANSACTIONS**  
 This option is not selected by default.  
  
 **SET CURSOR_CLOSE_ON_COMMIT**  
 Close any open cursors automatically (in compliance with ISO) when a transaction is committed. When cleared (set to OFF), cursors remain open across transaction boundaries, closing only when the connection is closed or when they are explicitly closed. This option is not selected by default.  
  
 **SET ANSI_PADDING**  
 Controls the way the column stores values shorter than the defined size of the column, and the way the column stores values that have trailing blanks in **char**, **varchar**, **binary**, and **varbinary** data. This setting affects only the definition of new columns. After the column is created, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] stores the values based on the setting when the column was created. Existing columns are not affected by a later change to this setting. This check box is selected by default.  
  
 **SET ANSI_WARNINGS**  
 Specifies ISO standard behavior for several error conditions:  
  
-   When this check box is selected, if null values appear in aggregate functions (such as SUM, AVG, MAX, MIN, STDEV, STDEVP, VAR, VARP, or COUNT), a warning message is generated. When **OFF**, no warning is issued.  
  
-   When this check box is cleared, divide-by-zero and arithmetic overflow errors cause the statement to be rolled back and an error message is generated. When OFF, divide-by-zero and arithmetic overflow errors cause null values to be returned. The behavior in which a divide-by-zero or arithmetic overflow error causes null values to be returned occurs if an INSERTor UPDATEoperation is attempted on a character, Unicode, or binary column in which the length of a new value exceeds the maximum size of the column. If **SET ANSI_WARNINGS** is ON, the INSERT or UPDATE operation is canceled as specified by the ISO standard. Trailing blanks are ignored for character columns, and trailing nulls are ignored for binary columns. When OFF, data is truncated to the size of the column and the statement succeeds.  
  
 This option is selected by default.  
  
 **SET ANSI_NULLS**  
 Specifies ISO compliant behavior of the Equal (`=`) and Not Equal to (`<>`) comparison operators when used with null values. When **SET ANSI_NULLS** is selected, all comparisons against a null value evaluate to UNKNOWN, the ISO compliant behavior. When **SET ANSI_NULLS** is not selected, comparisons of all data against a null value evaluate to TRUE if the data value is NULL. This option is selected by default.  
  
 **Reset to Default**  
 Resets all values on this page to the original default values.  
  
  
