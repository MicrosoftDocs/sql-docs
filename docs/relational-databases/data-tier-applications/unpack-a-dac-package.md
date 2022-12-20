---
description: "Unpack a DAC Package"
title: "Unpack a DAC Package"
ms.custom: ""
ms.date: "12/20/2022"
ms.service: sql
ms.subservice:
ms.topic: conceptual
helpviewer_keywords: 
  - "wizard [DAC], unpack"
  - "data-tier application [SQL Server], unpack"
  - "How to [DAC], unpack"
  - "unpack DAC"
ms.assetid: 697b69b3-f157-4e22-ac4e-f65c5fc2d0ad
author: dzsquared
ms.author: drskwier
---
# Unpack a DAC Package

A DAC is a self-contained unit of the entire database model and is portable in an artifact known as a DAC package, or .dacpac.  This article describes several ways to unpack the database model from a .dacpac for Windows, macOS, and Linux.

> [!CAUTION]
> We recommend that you do not deploy a DAC package from unknown or untrusted sources. Such DACs could contain malicious code that might execute unintended code or cause errors by modifying the schema. Before you use a DAC from an unknown or untrusted source, deploy it on an isolated test instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], unpack the DAC and examine the code, such as stored procedures or other user-defined code.


Use the Unpack Data-tier Application dialog box to unzip the scripts and files from a data-tier application (DAC) package. The scripts and files are placed in a folder where they can be reviewed before the package is used to deploy the DAC into a production system. The contents of one DAC can also be compared with the contents of another package unpacked to another folder.  

Options for examining the content of a dacpac include:
- importing the .dacpac to a SQL project in Visual Studio
- decompressing the file to view the xml structures
- deploying the .dacpac to a test instance

## Import the .dacpac to a SQL project in Visual Studio

Importing a .dacpac to a SQL project in Visual Studio results in the contents of the .dacpac being transformed into *.sql* files and organized into folders. Following the import, post-deployment scripts and pre-deployment scripts from the .dacpac are visible in the solution explorer.

1. Install [SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md) as a part of Visual Studio and create a new SQL project.

2. In [Solution Explorer](/visualstudio/ide/use-solution-explorer) right-click the empty project and select **Import**, then **from a Data-tier application package**.


## Decompress the .dacpac to view xml

Decompressing the .dacpac file results in the raw xml contents being visible in a text editor.  When looking for a specific component within the .dacpac this can be a quick method to access the contents.

1. Change the file extension on the .dacpac file to `.zip`.

2. Unzip the file using the built-in utility from your OS. To unzip a file from the command line:
  ```bash
  unzip AdventureWorks.dacpac
  ```

3. The resulting contents include `DacMetadata.xml`, `Origin.xml`, and `model.xml`.


## Deploy the .dacpac to a test instance

Deploying the .dacpac to a test instance results in the contents of the .dacpac being published to a database where the objects can be browsed from various connected database tools.

> [!NOTE]
> One option for creating a test instance locally is with [SQL Server in Docker](../../linux/quickstart-install-connect-docker.md#pullandrun2022).

### Deploy the .dacpac using Azure Data Studio

1. Install the **SQL Server dacpac extension** in [Azure Data Studio](../../azure-data-studio/extensions/sql-server-dacpac-extension.md).

2. Connect to the desired instance. Right-click on the server node and select **Data-tier application wizard** from the menu.

3.  Select the **deploy** option from the wizard and set the **Target Database** option to **New Database**.

4.  Following deployment, navigate to the database on the connected server in object explorer to browse the database objects.

### Deploy the .dacpac using SqlPackage

1. Install [SqlPackage](../../tools/sqlpackage/sqlpackage-download.md).

2. Use the SqlPackage CLI to publish the .dacpac file to the desired instance.  For example commands to publish a .dacpac to a database, please see [SqlPackage Publish examples](../../tools/sqlpackage/sqlpackage-publish.md#examples).


### Additional tools with .dacpac deployment capabilities

- SQL Server Management Studio
- Visual Studio: SQL Server Data Tools
- [PowerShell](deploy-a-data-tier-application.md#using-powershell)


## See Also
- [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)
- [SqlPackage Publish](../../tools/sqlpackage/sqlpackage-publish.md)
- [SQL Server dacpac extension in Azure Data Studio](../../azure-data-studio/extensions/sql-server-dacpac-extension.md)
- [SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md)

