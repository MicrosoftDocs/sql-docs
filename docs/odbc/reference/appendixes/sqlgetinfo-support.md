---
description: "SQLGetInfo Support"
title: "SQLGetInfo Support | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "compatibility [ODBC], SQLGetInfo"
  - "backward compatibility [ODBC], SQLGetInfo"
  - "SQLGetInfo function [ODBC], support"
ms.assetid: 57326f57-daba-46b6-b0be-6c97213b9ef1
author: David-Engel
ms.author: v-davidengel
---
# SQLGetInfo Support
When an ODBC 2.*x* application calls **SQLGetInfo** to an ODBC 3*.x* driver, the *InfoType* arguments in the following table must be supported.  
  
|*InfoType*|Returns|  
|----------------|-------------|  
|SQL_ALTER_TABLE (ODBC 2.0) **Note:**  This information type is not deprecated; the bitmasks in the column to the right are deprecated.|An SQLINTEGER bitmask enumerating the clauses in the **ALTER TABLE** statement supported by the data source.<br /><br /> The following bitmasks are used to determine which clauses are supported:<br /><br /> SQL_AT_DROP_COLUMN = The ability to drop columns is supported. Whether this results in cascade or restrict behavior is driver-defined. (ODBC 2.0)<br /><br /> SQL_AT_ADD_COLUMN = The ability to add multiple columns in a single ALTER TABLE statement is supported. This bit does not combine with other SQL_AT_ADD_COLUMN_XXX bits or SQL_AT_CONSTRAINT_XXX bits. (ODBC 2.0)|  
|SQL_FETCH_DIRECTION (ODBC 1.0)<br /><br /> The information type was introduced in ODBC 1.0; each bitmask is labeled with the version in which it was introduced.|An SQLINTEGER bitmask enumerating the supported fetch direction options.<br /><br /> The following bitmasks are used in conjunction with the flag to determine which options are supported:<br /><br /> SQL_FD_FETCH_NEXT          (ODBC 1.0) SQL_FD_FETCH_FIRST         (ODBC 1.0) SQL_FD_FETCH_LAST          (ODBC 1.0) SQL_FD_FETCH_PRIOR         (ODBC 1.0) SQL_FD_FETCH_ABSOLUTE   (ODBC 1.0) SQL_FD_FETCH_RELATIVE    (ODBC 1.0) SQL_FD_FETCH_BOOKMARK (ODBC 2.0)|  
|SQL_LOCK_TYPES (ODBC 2.0)|An SQLINTEGER bitmask enumerating the supported lock types for the *fLock* argument in **SQLSetPos**.<br /><br /> The following bitmasks are used in conjunction with the flag to determine which lock types are supported:<br /><br /> SQL_LCK_NO_CHANGE SQL_LCK_EXCLUSIVE SQL_LCK_UNLOCK|  
|SQL_ODBC_API_CONFORMANCE (ODBC 1.0)|An SQLSMALLINT value indicating the level of ODBC conformance.<br /><br /> SQL_OAC_NONE = None<br /><br /> SQL_OAC_LEVEL1 = Level 1 supported<br /><br /> SQL_OAC_LEVEL2 = Level 2 supported|  
|SQL_ODBC_SQL_CONFORMANCE (ODBC 1.0)|An SQLSMALLINT value indicating SQL grammar supported by the driver. See [Appendix C: SQL Grammar](../../../odbc/reference/appendixes/appendix-c-sql-grammar.md) for a definition of SQL conformance levels.<br /><br /> SQL_OSC_MINIMUM = Minimum grammar supported<br /><br /> SQL_OSC_CORE = Core grammar supported<br /><br /> SQL_OSC_EXTENDED = Extended grammar supported|  
|SQL_POS_OPERATIONS (ODBC 2.0)|An SQLINTEGER bitmask enumerating the supported operations in **SQLSetPos**.<br /><br /> The following bitmasks are used to in conjunction with the flag to determine which options are supported:<br /><br /> SQL_POS_POSITION (ODBC 2.0) SQL_POS_REFRESH   (ODBC 2.0) SQL_POS_UPDATE     (ODBC 2.0) SQL_POS_DELETE     (ODBC 2.0) SQL_POS_ADD          (ODBC 2.0)|  
|SQL_POSITIONED_STATEMENTS (ODBC 2.0)|An SQLINTEGER bitmask enumerating the supported positioned SQL statements.<br /><br /> The following bitmasks are used to determine which statements are supported:<br /><br /> SQL_PS_POSITIONED_DELETE SQL_PS_POSITIONED_UPDATE SQL_PS_SELECT_FOR_UPDATE|  
|SQL_SCROLL_CONCURRENCY (ODBC 1.0)|An SQLINTEGER bitmask enumerating the concurrency control options supported for the cursor.<br /><br /> The following bitmasks are used to determine which options are supported:<br /><br /> SQL_SCCO_READ_ONLY = Cursor is read-only. No updates are allowed.<br /><br /> SQL_SCCO_LOCK = Cursor uses the lowest level of locking sufficient to ensure that the row can be updated.<br /><br /> SQL_SCCO_OPT_ROWVER = Cursor uses optimistic concurrency control, comparing row versions, such as SQLBase ROWID or Sybase TIMESTAMP.<br /><br /> SQL_SCCO_OPT_VALUES = Cursor uses optimistic concurrency control, comparing values.|  
|SQL_STATIC_SENSITIVITY (ODBC 2.0)|An SQLINTEGER bitmask enumerating whether changes made by an application to a static or keyset-driven cursor through **SQLSetPos** or positioned update or delete statements can be detected by that application.<br /><br /> SQL_SS_ADDITIONS = Added rows are visible to the cursor; the cursor can scroll to these rows. Where these rows are added to the cursor is driver-dependent.<br /><br /> SQL_SS_DELETIONS = Deleted rows are no longer available to the cursor and do not leave a "hole" in the result set; after the cursor scrolls from a deleted row, it cannot return to that row.<br /><br /> SQL_SS_UPDATES = Updates to rows are visible to the cursor; if the cursor scrolls from and returns to an updated row, the data returned by the cursor is the updated data, not the original data. This option applies only to static cursors or updates on keyset-driven cursors that do not update the key. This option does not apply for a dynamic cursor or in the case in which a key is changed in a mixed cursor.<br /><br /> Whether an application can detect changes made to the result set by other users, including other cursors in the same application, depends on the cursor type.|  
  
 An ODBC 3*.x* application working with an ODBC 3*.x* driver should not call **SQLGetInfo** with the *InfoType* arguments described in the preceding table but should use the ODBC 3*.x* *InfoType* arguments listed in the following paragraph. There is not a one-to-one correspondence between *InfoType* arguments used in ODBC 2.*x* and those used in ODBC 3*.x*. An ODBC 3*.x* application working with an ODBC 2.*x* driver, on the other hand, should use the *InfoType* arguments described previously.  
  
 Some of the information types in the previous table are deprecated in favor of the cursor attributes information types. These deprecated information types are SQL_FETCH_DIRECTION, SQL_LOCK_TYPES, SQL_POS_OPERATIONS, SQL_POSITIONED_STATEMENTS, SQL_SCROLL_CONCURRENCY, and SQL_STATIC_SENSITIVITY. The new cursor attributes types are SQL_XXX_CURSOR_ATTRIBUTES1and SQL_XXX_CURSOR_ATTRIBUTES2, where XXX equals DYNAMIC, FORWARD_ONLY, KEYSET_DRIVEN, or STATIC. Each of the new types indicates the driver capabilities for a single cursor type. For more information about these options, see the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md) function description.
