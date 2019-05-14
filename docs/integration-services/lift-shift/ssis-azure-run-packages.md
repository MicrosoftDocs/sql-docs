---
title: "Run SSIS packages in Azure | Microsoft Docs"
description: "Provides an overview of the available methods for running SSIS packages deployed to Azure SQL Database."
ms.date: "05/29/2018"
ms.topic: conceptual
ms.prod: sql
ms.prod_service: "integration-services"
ms.custom: ""
ms.technology: integration-services
author: swinarko
ms.author: sawinark
ms.reviewer: douglasl
manager: craigg
---
# Run SQL Server Integration Services (SSIS) packages deployed in Azure

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



You can run SSIS packages deployed to the SSISDB Catalog on an Azure SQL Database server by choosing one of the methods described in this article. You can run a package directly, or run a package as part of an Azure Data Factory pipeline. For an overview about SSIS on Azure, see [Deploy and run SSIS packages in Azure](ssis-azure-lift-shift-ssis-packages-overview.md).

- Run a package directly

  - [Run with SSMS](#ssms)

  - [Run with stored procedures](#sproc)

  - [Run with script or code](#script)

- Run a package as part of an Azure Data Factory pipeline

  - [Run with the Execute SSIS Package activity](#exec_activity)

  - [Run with the Stored Procedure activity](#sproc_activity)

> [!NOTE]
> Running a package with `dtexec.exe` has not been tested with packages deployed to Azure.

## <a name="ssms"></a> Run a package with SSMS

In SQL Server Management Studio (SSMS), you can right-click on a package deployed to the SSIS Catalog database, SSISDB, and select **Execute** to open the **Execute Package** dialog box. For more info, see [Run an SSIS package with SQL Server Management Studio (SSMS)](../ssis-quickstart-run-ssms.md).

## <a name="sproc"></a> Run a package with stored procedures

In any environment from which you can connect to Azure SQL Database and run Transact-SQL code, you can run a package by calling the following stored procedures:

1. **[catalog].[create_execution]**. For more info, see [catalog.create_execution](../system-stored-procedures/catalog-create-execution-ssisdb-database.md).

2. **[catalog].[set_execution_parameter_value]**. For more info, see [catalog.set_execution_parameter_value](../system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md).

3. **[catalog].[start_execution]**. For more info, see [catalog.start_execution](../system-stored-procedures/catalog-start-execution-ssisdb-database.md).

For more info, see the following examples:

- [Run an SSIS package from SSMS with Transact-SQL](../ssis-quickstart-run-tsql-ssms.md)

- [Run an SSIS package from Visual Studio Code with Transact-SQL](../ssis-quickstart-run-tsql-vscode.md)

## <a name="script"></a> Run a package with script or code

In any development environment from which you can call a managed API, you can run a package by calling the `Execute` method of the `Package` object in the `Microsoft.SQLServer.Management.IntegrationServices` namespace.

For more info, see the following examples:

- [Run an SSIS package with PowerShell](../ssis-quickstart-run-powershell.md)

- [Run an SSIS package with C# code in a .NET app](../ssis-quickstart-run-dotnet.md)

## <a name="exec_activity"></a> Run a package with the Execute SSIS Package activity

For more info, see [Run an SSIS package using the Execute SSIS Package Activity in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-ssis-activity).

## <a name="sproc_activity"></a> Run a package with the Stored Procedure activity

For more info, see [Run an SSIS package using stored procedure activity in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-stored-procedure-activity).

## Next steps

Learn about options for scheduling SSIS packages deployed to Azure. For more info, see [Schedule SSIS packages in Azure](ssis-azure-schedule-packages.md).
