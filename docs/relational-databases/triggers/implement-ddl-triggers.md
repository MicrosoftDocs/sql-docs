---
description: "Implement DDL Triggers"
title: "Implement DDL Triggers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice:
ms.topic: conceptual
helpviewer_keywords: 
  - "DDL triggers, implementing"
ms.assetid: f44e5340-1d18-40e9-828e-0ffcca091ae3
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Implement DDL Triggers
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  This topic provides information to help you create DDL triggers, modify DDL triggers, and disable or drop DDL triggers.  
  
## Creating DDL Triggers  
 DDL triggers are created by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE TRIGGER statement for DDL triggers.  
  
 **To create a DDL trigger**  
  
-   [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)  
  
> [!IMPORTANT]  
>  The ability to return result sets from triggers will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Triggers that return result sets may cause unexpected behavior in applications that are not designed to work with them. Avoid returning result sets from triggers in new development work, and plan to modify applications that currently do this. To prevent triggers from returning result sets in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], set the [disallow results from triggers Option](../../database-engine/configure-windows/disallow-results-from-triggers-server-configuration-option.md) to 1. The default setting of this option will be 1 in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Modifying DDL Triggers  
 If you have to modify the definition of a DDL trigger, you can either drop and re-create the trigger or redefine the existing trigger in a single step.  
  
 If you change the name of an object that is referenced by a DDL trigger, you must modify the trigger so that its text reflects the new name. Therefore, before renaming an object, display the dependencies of the object first to determine whether any triggers are affected by the proposed change.  
  
 A trigger can also be modified to encrypt its definition.  
  
 **To modify a trigger**  
  
-   [ALTER TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-trigger-transact-sql.md)  
  
 **To view the dependencies of a trigger**  
  
-   [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)  
  
-   [sys.dm_sql_referenced_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md)  
  
-   [sys.dm_sql_referencing_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md)  
  
## Disabling and Dropping DDL Triggers  
 When a DDL trigger is no longer needed, you can disable it or delete it.  
  
 Disabling a DDL trigger does not drop it. The trigger still exists as an object in the current database. However, the trigger will not fire when any [!INCLUDE[tsql](../../includes/tsql-md.md)] statements on which it was programmed are run. DDL triggers that are disabled can be reenabled. Enabling a DDL trigger causes it to fire in the same way the trigger did when it was originally created. When DDL triggers are created, they are enabled by default.  
  
 When a DDL trigger is deleted, it is dropped from the current database. Any objects or data upon which the DDL trigger is scoped are not affected.  
  
 **To disable a DDL trigger**  
  
-   [DISABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/disable-trigger-transact-sql.md)  
  
-   [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
 **To enable a DDL trigger**  
  
-   [ENABLE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/enable-trigger-transact-sql.md)  
  
-   [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
 **To delete a DDL trigger**  
  
-   [DROP TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-trigger-transact-sql.md)  
  
  
