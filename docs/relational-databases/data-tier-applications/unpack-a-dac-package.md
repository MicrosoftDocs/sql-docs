---
title: "Unpack a DAC Package"
description: "Unpack a DAC Package"
author: dzsquared
ms.author: drskwier
ms.date: "5/9/2024"
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "wizard [DAC], unpack"
  - "data-tier application [SQL Server], unpack"
  - "How to [DAC], unpack"
  - "unpack DAC"
---
# Unpack a DAC Package

A DAC is a self-contained unit of the entire database model and is portable in an artifact known as a DAC package, or .dacpac.  This article describes several ways to unpack the database model from a .dacpac for Windows, macOS, and Linux.

> [!WARNING]
> We recommend that you do not deploy a DAC package from unknown or untrusted sources. Such DACs could contain malicious code that might execute unintended code or cause errors by modifying the schema. Before you use a DAC from an unknown or untrusted source, deploy it on an isolated test instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], unpack the DAC and examine the code, such as stored procedures or other user-defined code.

Options for examining the content of a dacpac include:

- importing the .dacpac to a SQL project in Visual Studio
- decompressing the file to view the XML contents
- deploying the .dacpac to a test instance
- invoking the `Unpack()` method from the Microsoft.SqlServer.DacFx .NET API

Unpacking a DAC package immediately after it has been extracted from a database to view the object definitions is more efficiently accomplished by using [Extract](../../tools/sqlpackage/sqlpackage-extract.md) in SqlPackage with the property `/p:ExtractTarget=File`. The result directly creates a single `.sql` file containing the object definitions from the specified source database.

## Import the .dacpac to a SQL project in Visual Studio

Importing a .dacpac to a SQL project in Visual Studio results in the contents of the .dacpac being transformed into *.sql* files and organized into folders. Following the import, post-deployment scripts and predeployment scripts from the .dacpac are visible in the solution explorer.

1. Install [SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md) as a part of Visual Studio and create a new SQL project.

2. In [Solution Explorer](/visualstudio/ide/use-solution-explorer) right-click the empty project and select **Import**, then **from a Data-tier application package**.

## Decompress the .dacpac to view XML contents

Decompressing the .dacpac file results in the raw XML contents being available for viewing in a text editor.  When looking for a specific component within the .dacpac this can be a quick method to access the contents.

1. Change the file extension on the .dacpac file to `.zip`.

2. Unzip the .zip file using the utility provided by your OS. To unzip a file from the command line:

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

3. Select the **deploy** option from the wizard and set the **Target Database** option to **New Database**.

4. Following deployment, navigate to the database on the connected server in object explorer to browse the database objects.

### Deploy the .dacpac using SqlPackage

1. Install [SqlPackage](../../tools/sqlpackage/sqlpackage-download.md).

2. Use the SqlPackage CLI to publish the .dacpac file to the desired instance.  For example commands to publish a .dacpac to a database, please see [SqlPackage Publish examples](../../tools/sqlpackage/sqlpackage-publish.md#examples).

### Additional tools with .dacpac deployment capabilities

Beyond Azure Data Studio and SqlPackage, many other tools can be used to deploy a .dacpac to a database.  Some examples include:

- SQL Server Management Studio
- Visual Studio: SQL Server Data Tools
- [PowerShell](deploy-a-data-tier-application.md#use-powershell)

## Invoke the `Unpack()` method

The Microsoft.SqlServer.DacFx .NET API provides a [method to unpack](/dotnet/api/microsoft.sqlserver.dac.dacpackage.unpack) a .dacpac to a folder, which can be used to programmatically unpack a .dacpac to a folder as seen. The example .NET application below takes two arguments, the path to the .dacpac file and the path to the output folder, and the result is the contents of the .dacpac being unpacked to 3 XML files and a single .sql file containing all the database objects.

```csharp
using Microsoft.SqlServer.Dac;

namespace DacUnpack
{
    class Program
    {
        static void Main(string[] args)
        {
            var dacpacPath = args[0];
            var outputPath = args[1];

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            Console.WriteLine("Unpacking {0} to {1}", dacpacPath, outputPath);
            using(DacPackage dacpac = DacPackage.Load(dacpacPath))
            {
                dacpac.Unpack(outputPath);
            }
        }
    }
}
```

## See Also

- [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)
- [SqlPackage Publish](../../tools/sqlpackage/sqlpackage-publish.md)
- [SQL Server dacpac extension in Azure Data Studio](../../azure-data-studio/extensions/sql-server-dacpac-extension.md)
- [SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md)