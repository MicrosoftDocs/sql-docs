---
title: "Create a User-Defined Data Type Alias | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.userdefineddatatype.general.f1"
  - "sql13.swb.new.datatype.properties.general.f1"
helpviewer_keywords: 
  - "alias data types [SQL Server], creating"
ms.assetid: b1dd8413-0cd0-411b-a79b-1bb043ccc62d
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create a User-Defined Data Type Alias
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic describes how to create a new user-defined data type alias in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a user-defined data type alias, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The name of a user-defined data type alias must comply with the rules for identifiers.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires CREATE TYPE permission in the current database and ALTER permission on *schema_name*. If *schema_name* is not specified, the default name resolution rules for determining the schema for the current user apply.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a user-defined data type  
  
1.  In Object Explorer, expand **Databases**, expand a database, expand **Programmability**, expand **Types**, right-click **User-Defined Data Types**, and then click **New User-Defined Data Type**.  
  
     **Allow NULLs**  
     Specify whether the user-defined data type can accept NULL values. The nullability of an existing user-defined data type is not editable.  
  
     **Data type**  
     Select the base data type from the list box. The list box displays all data types except for the **geography**, **geometry**, **hierarchyid**, **sysname**, **timestamp** , and **xml** data types. The data type of an existing user-defined data type is not editable.  
  
     **Default**  
     Optionally select a default to bind to the user-defined data type alias.  
  
     **Length/Precision**  
     Displays the length or precision of the data type as applicable. **Length** applies to character-based user-defined data types; **Precision** applies only to numeric-based user-defined data types. The label changes depending on the data type selected earlier. This box is not editable if the length or precision of the selected data type is fixed.  
  
     Length is not displayed for **nvarchar(max)**, **varchar(max)**, or **varbinary(max)** data types.  
  
     **Name**  
     If you are creating a new user-defined data type alias, type a unique name to be used across the database to represent the user-defined data type. The maximum number of characters must match the system **sysname** data type. The name of an existing user-defined data type alias is not editable.  
  
     **Rule**  
     Optionally select a rule to bind to the user-defined data type alias.  
  
     **Scale**  
     Specify the maximum number of decimal digits that can be stored to the right of the decimal point.  
  
     **Schema**  
     Select a schema from a list of all schemas available to the current user. The default selection is the default schema for the current user.  
  
     **Storage**  
     Displays the maximum storage size for the user-defined data type alias. Maximum storage sizes vary, based on precision.  
  
    |||  
    |-|-|  
    |1 - 9|5|  
    |10 - 19|9|  
    |20 - 28|13|  
    |29 - 38|17|  
  
     For **nchar** and **nvarchar** data types, the storage value is always two times the value in **Length**.  
  
     Storage is not displayed for **nvarchar(max)**, **varchar(max)**, or **varbinary(max)** data types.  
  
2.  In the **New User-defined Data Type** dialog box, in the **Schema** box, type the schema to own this data type alias, or use the browse button to select the schema.  
  
3.  In the **Name** box, type a name for the new data type alias.  
  
4.  In the **Data type** box, select the data type that the new data type alias will be based on.  
  
5.  Complete the **Length**, **Precision**, and **Scale** boxes if appropriate for that data type.  
  
6.  Check **Allow NULLs** if the new data type alias can permit NULL values.  
  
7.  In the **Binding** area, complete the **Default** or **Rule** boxes if you want to bind a default or rule to the new data type alias. Defaults and rules cannot be created in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Use [!INCLUDE[tsql](../../includes/tsql-md.md)]. Example code for creating defaults and rules are available in Template Explorer.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a user-defined data type alias  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example creates a data type alias based on the system-supplied `varchar` data type. The `ssn` data type alias is used for columns holding 11-digit social security numbers (999-99-9999). The column cannot be NULL.  
  
```sql  
CREATE TYPE ssn  
FROM varchar(11) NOT NULL ;  
```  
  
## See Also  
 [Database Identifiers](../../relational-databases/databases/database-identifiers.md)   
 [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md)  
  
  
