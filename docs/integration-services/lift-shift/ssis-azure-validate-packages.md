---
title: "Validate SSIS packages deployed to Azure"
description: Learn how the SSIS Package Deployment Wizard checks packages for known issues that may prevent the packages from running as expected in Azure.
author: swinarko
ms.author: sawinark
ms.reviewer: maghan, randolphwest
ms.date: 11/03/2022
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
---
# Validate SQL Server Integration Services (SSIS) packages deployed to Azure

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

When you deploy a SQL Server Integration Services (SSIS) project to the SSIS Catalog (SSISDB) on an Azure server, the Package Deployment Wizard adds an additional validation step after the **Review** page. This validation step checks the packages in the project for known issues that may prevent the packages from running as expected in the Azure SSIS Integration Runtime. Then the wizard displays any applicable warnings on the **Validate** page.

> [!IMPORTANT]  
> The validation described in this article occurs when you deploy a project with SQL Server Data Tools (SSDT) version 17.4 or later. To get the latest version of SSDT, see [Download SQL Server Data Tools (SSDT)](../../ssdt/download-sql-server-data-tools-ssdt.md).

For more info about the Package Deployment Wizard, see [Deploy Integration Services (SSIS) Projects and Packages](../packages/deploy-integration-services-ssis-projects-and-packages.md).

## Validate connection managers

The wizard checks certain connection managers for the following issues, which may cause the connection to fail:

- **Windows authentication**. If a connection string uses Windows authentication, validation raises a warning. Windows authentication requires additional configuration steps. For more info, see [Connect to data and file shares with Windows Authentication](/azure/data-factory/ssis-azure-connect-with-windows-auth).
- **File path**. If a connection string contains a hard-coded local file path like `C:\\...`, validation raises a warning. Packages that contain an absolute path may fail.
- **UNC path**. If a connection string contains a UNC path, validation raises a warning. Packages that contain a UNC path may fail, typically because a UNC path requires Windows authentication to access.
- **Host name**. If a server property contains host name instead of IP address, validation raises a warning. Packages that contain host name may fail, typically because the Azure virtual network requires the correct DNS configuration to support DNS name resolution.
- **Provider or driver**. If a provider or driver isn't supported, validation raises a warning. Only a few built-in providers and drivers are supported at this time.

The wizard does the following validation checks for the connection managers in the list.

| Connection manager | Windows authentication | File path | UNC path | Host name | Provider or driver |
| :--- | :---: | :---: | :---: | :---: | :---: |
| Ado | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| AdoNet | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| Cache | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | |
| Excel | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | |
| File | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | |
| FlatFile | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | |
| Ftp | | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | |
| MsOLAP100 | | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| MultiFile | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | |
| MultiFlatFile | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | |
| OData | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | |
| Odbc | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| OleDb | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: |
| SmoServer | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | |
| Smtp | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | |
| SqlMobile | | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | |
| Wmi | :::image type="content" source="../../includes/media/yes-icon.svg" border="false" alt-text="Yes"::: | | | | |

## Validate sources and destinations

The following third-party sources and destinations aren't supported:

- Oracle Source and Destination by Attunity
- Teradata Source and Destination by Attunity
- SAP BW Source and Destination

## Validate tasks and components

### Execute Process Task

Validation raises a warning if a command points to a local file with an absolute path, or to a file with a UNC path. These paths may cause execution on Azure to fail.

### Script Task and Script Component

Validation raises a warning if a package contains a script task or a script component, which may reference or call unsupported assemblies. These references or calls may cause execution to fail.

### Other components

The Orc format isn't supported in the HDFS Destination and the Azure Data Lake Store Destination.

## Next steps

- [Schedule SSIS packages in Azure](ssis-azure-schedule-packages.md).
