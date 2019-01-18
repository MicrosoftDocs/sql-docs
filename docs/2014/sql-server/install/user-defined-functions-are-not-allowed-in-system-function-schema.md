---
title: "User-defined functions are not allowed in system_function_schema | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "system functions [SQL Server]"
  - "user-defined functions [SQL Server], system"
ms.assetid: 3cb54053-ef65-4558-ae96-8686b6b22f4f
author: mashamsft
ms.author: mathoma
manager: craigg
---
# User-defined functions are not allowed in system_function_schema
  The Upgrade Advisor detected user-defined functions that are owned by the undocumented user **system_function_schema**. You cannot create a user-defined system function by specifying this user. The **system_function_schema** user name does not exist, and the user ID that is associated with this name (UID = 4) is reserved for the **sys** schema and is restricted to internal use only.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 System object storage and access has changed in the following ways:  
  
-   System objects are stored in the read-only **Resource** database, and direct system object updates are not permitted.  
  
     System objects logically appear in the **sys** schema of every database. This maintains the ability to invoke system functions from any database by specifying a one-part function name. For example, the statement `SELECT * FROM fn_helpcollations()` can be run from any database.  
  
-   The undocumented user **system_function_schema** has been removed.  
  
-   The user ID associated with **system_function_schema** (UID = 4) is reserved for the **sys** schema and is restricted to internal use only.  
  
 These changes have the following effect on user-defined system functions:  
  
-   Data Definition Language (DDL) statements that reference **system_function_schema** will fail. For example, the statement `CREATE FUNCTION system`_`function`\_`schema.fn`\_`MySystemFunction` ... will not succeed.  
  
-   After you upgrade to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], existing objects that are owned by **system_function_schema** are contained only in the **sys** schema of the **master** database. Because system objects cannot be modified, these functions can never be changed or dropped from the **master** database. Additionally, these functions cannot be invoked from other databases by specifying only a one-part function name.  
  
## Corrective Action  
 Before you upgrade , complete the following tasks:  
  
1.  Change the ownership of existing user-defined functions to **dbo** by using the **sp_changeobjectowner** system stored procedure.  
  
2.  Consider renaming the function so that is does not use the prefix 'fn_'. This will avoid potential name conflicts with current or future system functions.  
  
3.  Add a copy of the modified functions in every database that uses them.  
  
4.  Replace references to **system_function_schema** with **dbo** in all scripts that contain user-defined function DDL statements.  
  
5.  Modify scripts that invoke these functions to use either the two-part name dbo**.**_function_name_, or the three-part name _database_name_**.**dbo.*function_name*.  
  
 For more information, see the following topics in SQL Server Books Online:  
  
-   "sp_changeobjectowner"  
  
-   "User-schema Separation"  
  
-   "Resource Database"  
  
## See Also  
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)   
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [Remove statements that modify system objects](../../../2014/sql-server/install/remove-statements-that-modify-system-objects.md)   
 [Remove statements that drop system objects](../../../2014/sql-server/install/remove-statements-that-drop-system-objects.md)  
  
  
