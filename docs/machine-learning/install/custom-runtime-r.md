---
title: Install R custom runtime
description: Learn how to install an R custom runtime for SQL Server using Language Extensions. The Python custom runtime can run machine learning scripts.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 11/09/2022
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
- contperf-fy21q3
- intro-installation
- event-tier1-build-2022
zone_pivot_groups: sqlml-platforms
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# Install an R custom runtime for SQL Server

[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

Learn how to install an R custom runtime for running external R scripts with SQL Server on:

+ Windows
+ Ubuntu Linux
+ Red Hat Enterprise Linux (RHEL)
+ SUSE Linux Enterprise Server (SLES) version 12

The custom runtime can run machine learning scripts and uses the [SQL Server Language Extensions](../../language-extensions/language-extensions-overview.md).

Use your own version of the R runtime with SQL Server, instead of the default runtime version installed with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], runtimes for R, Python, and Java, are no longer installed with SQL Setup. Instead, install your desired R custom runtime(s) and packages. For more information, see [Install SQL Server 2022 Machine Learning Services (Python and R) on Windows](sql-machine-learning-services-windows-install-sql-2022.md) or [Install SQL Server Machine Learning Services (Python and R) on Linux](../../linux/sql-server-linux-setup-machine-learning.md).

::: zone pivot="platform-windows"
[!INCLUDE [R custom runtime - Windows](includes/custom-runtime-r-windows.md)]
::: zone-end

::: zone pivot="platform-linux-ubuntu"
[!INCLUDE [R custom runtime - Linux - Prerequisites](includes/custom-runtime-r-linux-prerequisites.md)]

[!INCLUDE [R custom runtime - Linux - Ubuntu specific steps](includes/custom-runtime-r-linux-ubuntu.md)]

[!INCLUDE [R custom runtime on Linux - Common steps](includes/custom-runtime-r-linux-common.md)]
::: zone-end

::: zone pivot="platform-linux-rhel"
[!INCLUDE [R custom runtime - Linux - Prerequisites](includes/custom-runtime-r-linux-prerequisites.md)]

[!INCLUDE [R custom runtime - Linux - RHEL specific steps](includes/custom-runtime-r-linux-rhel.md)]

[!INCLUDE [R custom runtime on Linux - Common steps](includes/custom-runtime-r-linux-common.md)]
::: zone-end

::: zone pivot="platform-linux-sles"
[!INCLUDE [R custom runtime - Linux - Prerequisites](includes/custom-runtime-r-linux-prerequisites.md)]

[!INCLUDE [R custom runtime - Linux - SLES specific steps](includes/custom-runtime-r-linux-sles.md)]

[!INCLUDE [R custom runtime on Linux - Common steps](includes/custom-runtime-r-linux-common.md)]
::: zone-end

## Enable external script

You can execute an R external script with the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

To enable external scripts, use [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) to execute the statement below.

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```

## Verify installation

Use the following SQL script to verify the installation and functionality of the R custom runtime. In the below sample script, `myR` is used as the language name because the default language name `R` cannot be provided for a custom runtime.

```sql
EXEC sp_execute_external_script
    @language =N'myR',
    @script=N'
print(R.home());
print(file.path(R.home("bin"), "R"));
print(R.version);
print("Hello RExtension!");'
```

::: zone pivot="platform-windows"
[!INCLUDE [R custom runtime - Windows - known issues](includes/custom-runtime-r-windows-known-issues.md)]
::: zone-end

::: zone pivot="platform-linux-rhel"
[!INCLUDE [R custom runtime - Linux - RHEL known issues](includes/custom-runtime-r-linux-known-issues-rhel.md)]
::: zone-end

## Next steps

+ [Install a Python custom runtime for SQL Server](custom-runtime-python.md)
+ [Extensibility framework in SQL Server](../concepts/extensibility-framework.md)
+ [Language Extensions Overview](../../language-extensions/language-extensions-overview.md)
