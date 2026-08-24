---
title: "Unpack a DACPAC File"
description: "Unpack a DACPAC file to review or examine the contents."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, maghan
ms.date: 02/06/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: how-to
ms.collection:
  - data-tools
helpviewer_keywords:
  - "wizard [DAC], unpack"
  - "data-tier application [SQL Server], unpack"
  - "How to [DAC], unpack"
  - "unpack DAC"
---
# Unpack a DACPAC file

A data-tier application (DAC) is a self-contained unit of the entire database model and is portable in an artifact known as a DAC package, or `.dacpac`. It's a good practice to review the contents of a `.dacpac` before deploying it in production, and to validate the upgrade actions before upgrading an existing DAC. Validation of the `.dacpac` contents is especially important when deploying packages that weren't developed in your organization. This article describes several ways to unpack the database model from a `.dacpac` for Windows, macOS, and Linux.

> [!WARNING]  
> Don't deploy a `.dacpac` from unknown or untrusted sources. Such DACs could contain malicious code that might execute unintended code or cause errors by modifying the schema. Before you use a DAC from an unknown or untrusted source, deploy it on an isolated test instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)], unpack the DAC and examine the code, such as stored procedures or other user-defined code.

Options for examining the content of a `.dacpac` include:

- importing the `.dacpac` to a SQL project in Visual Studio
- using the SqlPackage command-line utility to extract the `.dacpac`
- decompressing the file to view the XML contents
- deploying the `.dacpac` to a test instance

Unpacking a `.dacpac` immediately after it was extracted from a database to view the object definitions is more efficiently accomplished by using [Extract](../../../sqlpackage/sqlpackage-extract.md) in SqlPackage with the property `/p:ExtractTarget=File`. The result directly creates a single `.sql` file that contains the object definitions from the specified source database.

## Import the DACPAC to a SQL project in Visual Studio

Importing a `.dacpac` to a SQL project in Visual Studio results in the contents of the `.dacpac` being transformed into *.sql* files and organized into folders. Following the import, post-deployment scripts and predeployment scripts from the `.dacpac` are visible in the solution explorer.

1. Install [SQL Server Data Tools](../../../../ssdt/download-sql-server-data-tools-ssdt.md) as a part of Visual Studio and create a new SQL project.

1. In [Solution Explorer](/visualstudio/ide/use-solution-explorer) right-click the empty project and select **Import**, then **from a Data-tier application package**.

## Decompress the DACPAC to view XML contents

Decompressing the `.dacpac` file results in the raw XML contents being available for viewing in a text editor. When looking for a specific component within the `.dacpac`, reviewing the XML contents can be a quick method to access the information.

1. Change the file extension on the `.dacpac` file to `.zip`.

1. Unzip the .zip file using the utility provided by your OS. To unzip a file from the command line:

   ```bash
   unzip AdventureWorks.dacpac
   ```

1. The resulting contents include `DacMetadata.xml`, `Origin.xml`, and `model.xml`.

## Deploy the DACPAC to a test instance

Deploying the `.dacpac` to a test instance results in the contents of the `.dacpac` being published to a database where the objects can be browsed from various connected database tools.

> [!NOTE]  
> One option for creating a test instance locally is with [SQL Server in Docker](../../../../linux/quickstart-install-connect-docker.md#pullandrun2022).

### Deploy the DACPAC using Visual Studio Code

1. Install the [MSSQL extension for Visual Studio Code](../../../visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md), and follow the instructions to enable the [Data-tier Application (Preview)](../../../visual-studio-code-extensions/mssql/mssql-data-tier-application.md) experience.

1. Connect to the desired instance. Right-click on the server node and select **Data-tier application wizard** from the menu.

1. Select the **deploy** option from the wizard and set the **Target Database** option to **New Database**.

1. Following deployment, navigate to the database on the connected server in object explorer to browse the database objects.

### Deploy the DACPAC using SqlPackage

1. Install [SqlPackage](../../../sqlpackage/sqlpackage-download.md).

1. Use the SqlPackage CLI to publish the `.dacpac` file to the desired instance. For example commands to publish a `.dacpac` to a database, see [SqlPackage Publish examples](../../../sqlpackage/sqlpackage-publish.md#examples).

### Other tools with DACPAC deployment capabilities

Beyond Visual Studio Code and SqlPackage, many other tools can be used to deploy a `.dacpac` to a database. Some examples include:

- SQL Server Management Studio
- Visual Studio: SQL Server Data Tools

## Invoke the `Unpack()` method

The Microsoft.SqlServer.DacFx .NET API provides a [method to unpack](/dotnet/api/microsoft.sqlserver.dac.dacpackage.unpack) a `.dacpac` to a folder, which can be used to programmatically unpack a `.dacpac` to a folder as seen. The following example .NET application takes two arguments, the path to the `.dacpac` file and the path to the output folder, and the result is the contents of the `.dacpac` converted to 3 XML files and a single .sql file that contain all the database objects.

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

## Related content

- [Data-tier applications (DAC) overview](overview.md)
- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../../sqlpackage/sqlpackage-publish.md)
- [Data-tier Application (DACPAC and BACPAC) import and export](../../../visual-studio-code-extensions/mssql/mssql-data-tier-application.md)
- [Install SQL Server Data Tools (SSDT) for Visual Studio](../../../../ssdt/download-sql-server-data-tools-ssdt.md)
