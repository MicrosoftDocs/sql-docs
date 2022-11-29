---
title: "Implementing Assemblies"
description: Learn how to work with assemblies hosted on SQL Server, including how to create/modify assemblies, drop or enable/disable assemblies, and manage versions.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "assemblies [CLR integration], implementing"
ms.assetid: c228d7bf-a906-4f37-a057-5d464d962ff8
---
# Assemblies - Implementing
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic provides information about the following areas to help you implement and work with assemblies in the database:  
  
-   Creating assemblies  
  
-   Modifying assemblies  
  
-   Dropping, disabling, and enabling assemblies  
  
-   Managing assembly versions  
  
## Creating Assemblies  
 Assemblies are created in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE ASSEMBLY statement, or in the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by using the Assembly Assisted Editor. Additionally, deploying a SQL Server Project in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] registers an assembly in the database that was specified for the project. For more information, see [Deploying CLR Database Objects](../../relational-databases/clr-integration/deploying-clr-database-objects.md).  
  
 **To create an assembly by using Transact-SQL**  
  
-   [CREATE ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/create-assembly-transact-sql.md)  
  
 **To create an assembly by using SQL Server Management Studio**  
  
-   [Assembly Properties &#40;General Page&#41;](../../relational-databases/clr-integration/assemblies-properties.md)  
  
## Modifying Assemblies  
 Assemblies are modified in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] ALTER ASSEMBLY statement or in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by using the Assembly Assisted Editor. You can modify an assembly when you want to do the following:  
  
-   Change the implementation of the assembly by uploading a newer version of the binaries of the assembly. For more information, see [Managing Assembly Versions](#_managing) later in this topic.  
  
-   Change the permission set of the assembly. For more information, see [Designing Assemblies](../../relational-databases/clr-integration/assemblies-designing.md).  
  
-   Change the visibility of the assembly. Visible assemblies are available for referencing in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Nonvisible assemblies are not available, even if they have been uploaded in the database. By default, assemblies uploaded to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are visible.  
  
-   Add or drop a debug or source file associated with the assembly.  
  
 **To modify an assembly by using Transact-SQL**  
  
-   [ALTER ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-assembly-transact-sql.md)  
  
 **To modify an assembly by using SQL Server Management Studio**  
  
-   [Assembly Properties &#40;General Page&#41;](../../relational-databases/clr-integration/assemblies-properties.md)  
  
## Dropping, Disabling, and Enabling Assemblies  
 Assemblies are dropped by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] DROP ASSEMBLY statement or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **To drop an assembly by using Transact-SQL**  
  
-   [DROP ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-assembly-transact-sql.md)  
  
 **To drop an assembly by using SQL Server Management Studio**  
  
-   [Delete Objects](../../ssms/object/delete-objects.md)  
  
 By default, all assemblies that are created in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are disabled from executing. You can use the **clr enabled** option of the **sp_configure** system stored procedure to disable or enable the execution of all assemblies that are uploaded in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Disabling assembly execution prevents common language runtime (CLR) functions, stored procedures, triggers, aggregates, and user-defined types from executing, and stops those that are currently executing. Disabling assembly execution does not disable the ability to create, alter, or drop assemblies. For more information, see [clr enabled Server Configuration Option](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md).  
  
 **To disable and enable assembly execution**  
  
-   [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
##  <a name="_managing"></a> Managing Assembly Versions  
 When an assembly is uploaded to an instance [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the assembly is stored and managed within the database system catalogs. Any changes made to the definition of the assembly in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] should be propagated to the assembly that is stored in the database catalog.  
  
 When you have to modify an assembly, you must issue an ALTER ASSEMBLY statement to update the assembly in the database. This will update the assembly to the latest copy of [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] modules holding its implementation.  
  
 The WITH UNCHECKED DATA clause of the ALTER ASSEMBLY statement instructs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to refresh even those assemblies upon which persisted data in the database is dependent. Specifically, you must specify WITH UNCHECKED DATA if any of the following exist:  
  
-   Persisted computed columns that reference methods in the assembly, either directly, or indirectly, through [!INCLUDE[tsql](../../includes/tsql-md.md)] functions or methods.  
  
-   Columns of a CLR user-defined type that depend on the assembly, and the type implements a **UserDefined** (non-**Native**) serialization format.  
  
> [!CAUTION]  
>  If WITH UNCHECKED DATA is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to prevent ALTER ASSEMBLY from executing if the new assembly version affects existing data in tables, indexes, or other persistent sites. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not guarantee that computed columns, indexes, indexed views, or expressions will be consistent with the underlying routines and types when the CLR assembly is updated. Be careful when you execute ALTER ASSEMBLY to make sure that there is no mismatch between the result of an expression and a value that is based on that expression stored in the assembly.  
  
 Only members of the **db_owner** and **db_ddlowner** fixed database role can execute run ALTER ASSEMBLY by using the WITH UNCHECKED DATA clause.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] posts a message to the Windows application event log that the assembly has been modified with unchecked data in the tables. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] then marks any tables that contain data dependent on the assembly as having unchecked data. The **has_unchecked_assembly_data** column of the **sys.tables** catalog view contains the value 1 for tables that contain unchecked data, and 0 for tables without unchecked data.  
  
 To resolve the integrity of unchecked data, run DBCC CHECKDB WITH EXTENDED_LOGICAL_CHECKS against each table that has unchecked data. If DBCC CHECKDB WITH EXTENDED_LOGICAL_CHECKS fails, you must either delete the table rows that are not valid or modify the assembly code to address problems, and then issue additional ALTER ASSEMBLY statements.  
  
 ALTER ASSEMBLY changes the assembly version. The culture and public key token of the assembly remain the same.SQL Server does not allow registering different versions of an assembly with the same name, culture and public key.  
  
### Interactions with Computer-wide Policy for Version Binding  
 If references to assemblies stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are redirected to specific versions by using publisher policy or computer-wide administrator policy, you must do either of the following:  
  
-   Make sure the new version to which this redirection is made is in the database.  
  
-   Modify any statements to the external policy files of the computer or publisher policy to make sure that they reference the specific version that is in the database.  
  
 Otherwise, an attempt to load a new assembly version to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will fail.  
  
 **To update the version of an assembly**  
  
-   [ALTER ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-assembly-transact-sql.md)  
  
## See Also  
 [Assemblies &#40;Database Engine&#41;](../../relational-databases/clr-integration/assemblies-database-engine.md)   
 [Getting Information About Assemblies](../../relational-databases/clr-integration/assemblies-getting-information.md)  
  
  
