---
title: "External Scripts Enabled server configuration option"
description: Learn about the external scripts enabled option in SQL Server. After turning it on, you can execute external scripts in supported languages such as R or Python.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "06/30/2020"
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: language-reference
f1_keywords:
  - "external scripts enabled"
  - "external_scripts_enabled_TSQL"
helpviewer_keywords:
  - "external scripts enabled option"
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# External Scripts Enabled server configuration option
[!INCLUDE [sqlserver2016-asdbmi](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

Use the **external scripts enabled** option to enable the execution of scripts with certain remote language extensions. This property is OFF by default. When **Machine Learning Services** is installed, setup can optionally set this property to true.

## Remarks

You must enable the external script enabled option before you can execute an external script using the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) procedure. Use **sp_execute_external_script** to execute scripts written in a supported language such as R or Python. 

+ For [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]

    [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] includes support for the R language in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], and a set of R workstation tools and connectivity libraries.

    Install the **R Services** feature during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable the execution of R scripts.

+ For [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] and later

    [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)] has support for both the R and Python languages.

    Install the **Machine Learning Services** feature during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable the execution of external scripts. Be sure to select at least one language during initial setup: either R or Python, or both.
    
+ For [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later[!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)] has support for all R, Python, Java and other third party languages.

Install the Machine Learning Services and Language Extensions feature during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable the execution of external scripts for any supported language.

## Additional requirements

After setup, to enable external scripts, execute the following script:

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```

For more information, see [Install SQL Server Machine Learning Services (Python and R) on Windows](../../machine-learning/install/sql-machine-learning-services-windows-install.md) or [Linux](../../linux/sql-server-linux-setup-machine-learning-docker.md?toc=/sql/machine-learning/toc.json).

## See also

+ [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
+ [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)
+ [sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)
+ [SQL machine learning documentation](../../machine-learning/index.yml)
