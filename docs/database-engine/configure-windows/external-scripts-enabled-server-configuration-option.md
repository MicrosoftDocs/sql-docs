---
title: "External Scripts Enabled server configuration option | Microsoft Docs"
ms.date: "11/13/2017"
ms.prod: sql
ms.technology: configuration
ms.reviewer: ""
ms.topic: language-reference
f1_keywords: 
  - "external scripts enabled"
  - "external_scripts_enabled_TSQL"
helpviewer_keywords: 
  - "external scripts enabled option"
ms.assetid: 9d0ce165-8719-4007-9ae8-00f85cab3a0d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# External Scripts Enabled server configuration option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
**Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)] [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] and [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

Use the **external scripts enabled** option to enable the execution of scripts with certain remote language extensions. This property is OFF by default. When **Advanced Analytics Services** is installed, setup can optionally set this property to true.

## Remarks

You must enable the external script enabled option before you can execute an external script using the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) procedure. Use **sp_execute_external_script** to execute scripts written in a supported language such as R or Python. 

+ For [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]

    [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] includes support for the R language in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], and a set of R workstation tools and connectivity libraries.

    Install the **Advanced Analytics Extensions** feature during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable the execution of R scripts. The R language is installed by default.

+ For [[!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]

    [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)] uses the same architecture as in SQL Server 2016, but provides support for the Python language.

    Install the **Advanced Analytics Extensions** feature during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable the execution of external scripts. Be sure to select at least one language during initial setup: either R or Python, or both. 

## Additional requirements

After setup, to enable external scripts, execute the following script:

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```

You must restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to make this change effective.

For more information, see [Set up SQL Server Machine Learning](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md).

## See also

[sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)

[RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)

[sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)

[SQL Server Machine Learning Services](../../advanced-analytics/r/sql-server-r-services.md)
