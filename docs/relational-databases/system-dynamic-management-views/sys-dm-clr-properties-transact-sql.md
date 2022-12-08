---
title: "sys.dm_clr_properties (Transact-SQL)"
description: sys.dm_clr_properties (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_clr_properties"
  - "sys.dm_clr_properties_TSQL"
  - "dm_clr_properties_TSQL"
  - "dm_clr_properties"
helpviewer_keywords:
  - "sys.dm_clr_properties dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 220d062f-d117-46e7-a448-06fe48db8163
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_clr_properties (Transact-SQL)
[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  Returns a row for each property related to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] common language runtime (CLR) integration, including the version and state of the hosted CLR. The hosted CLR is initialized by running the [CREATE ASSEMBLY](../../t-sql/statements/create-assembly-transact-sql.md), [ALTER ASSEMBLY](../../t-sql/statements/alter-assembly-transact-sql.md), or [DROP ASSEMBLY](../../t-sql/statements/drop-assembly-transact-sql.md) statements, or by executing any CLR routine, type, or trigger. The **sys.dm_clr_properties** view does not specify whether execution of user CLR code has been enabled on the server. Execution of user CLR code is enabled by using the [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) stored procedure with the [clr enabled](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md) option set to 1.  
  
 The **sys.dm_clr_properties** view contains the **name** and **value** columns. Each row in this view provides details about a property of the hosted CLR. Use this view to gather information about the hosted CLR, such as the CLR install directory, the CLR version, and the current state of the hosted CLR. This view can help you determine if the CLR integration code is not working because of problems with the CLR installation on the server computer.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**nvarchar(128)**|The name of the property.|  
|**value**|**nvarchar(128)**|Value of the property.|  
  
## Properties  
 The **directory** property indicates the directory that the .NET Framework was installed to on the server. There could be multiple installations of .NET Framework on the server computer and the value of this property identifies which installation [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is using.  
  
 The **version** property indicates the version of the .NET Framework and hosted CLR on the server.  
  
 The **sys.dm_clr_properties** dynamic managed view can return six different values for the **state** property, which reflects the state of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] hosted CLR. They are:  
  
-   Mscoree is not loaded.  
  
-   Mscoree is loaded.  
  
-   Locked CLR version with mscoree.  
  
-   CLR is initialized.  
  
-   CLR initialization permanently failed.  
  
-   CLR is stopped.  
  
 The **Mscoree is not loaded** and **Mscoree is loaded** states show the progression of the hosted CLR initialization on server startup, and are not likely to be seen.  
  
 The **Locked CLR version with mscoree** state may be seen where the hosted CLR is not being used and, thus, it has not yet been initialized. The hosted CLR is initialized the first time a  DDL statement (such as [CREATE ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/create-assembly-transact-sql.md)) or a managed database object is executed.  
  
 The **CLR is initialized** state indicates that the hosted CLR was successfully initialized. Note that this does not indicate whether execution of user CLR code was enabled. If the execution of user CLR code is first enabled and then disabled using the [!INCLUDE[tsql](../../includes/tsql-md.md)] [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) stored procedure, the state value will still be **CLR is initialized**.  
  
 The **CLR initialization permanently failed** state indicates that hosted CLR initialization failed. Memory pressure is a likely cause, or it could also be the result of a failure in the hosting handshake between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the CLR. Error message 6512 or 6513 will be thrown in such a case.  
  
 The **CLR is stopped state** is only seen when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in the process of shutting down.  
  
## Remarks  
 The properties and values of this view might change in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] due to enhancements of the CLR integration functionality.  
  
## Permissions  
  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Examples  
 The following example retrieves information about the hosted CLR:  
  
```  
SELECT name, value   
FROM sys.dm_clr_properties;  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Common Language Runtime Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/common-language-runtime-related-dynamic-management-views-transact-sql.md)  
  
