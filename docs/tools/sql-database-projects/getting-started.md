---
title: "Get started with SQL database projects"
description: "[Article description]."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: get-started
zone_pivot_groups: sq1-sql-projects-tools
---

# Get started with SQL database projects

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

A SQL database project is a local representation of SQL objects that comprise the schema for a single database, such as tables, stored procedures, or functions. The development cycle of a SQL database project enables database development to be integrated into a continuous integration and continuous deployment (CI/CD) workflows familiar as a development best practice.

This article steps through creating a new SQL project, adding objects to the project, and building and deploying the project. Except for the Visual Studio (SQL Server Data Tools) instructions, the guide focuses on SDK-style SQL projects.

## Prerequisites

::: zone pivot="sq1-visual-studio"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools (SSDT) installed in Visual Studio 2022](../../ssdt/download-sql-server-data-tools-ssdt.md)

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools, SDK-style (preview) installed in Visual Studio 2022](../../ssdt/download-sql-server-data-tools-ssdt-sdk.md)

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [VS Code](https://code.visualstudio.com/Download)
- [SQL Database Projects extension for Azure Data Studio](/azure-data-studio/extensions/sql-database-project-extension) or [SQL Database Projects extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-database-projects-vscode)

::: zone-end

::: zone pivot="sq1-command-line"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SqlPackage CLI](../sqlpackage/sqlpackage-download.md)
- [Microsoft.Build.Sql.Templates .NET templates](https://www.nuget.org/packages/Microsoft.Build.Sql.Templates/)

```bash
# install SqlPackage CLI
dotnet tool install -g Microsoft.SqlPackage

# install Microsoft.Build.Sql.Templates
dotnet new install Microsoft.Build.Sql.Templates
```

::: zone-end

> [!NOTE]  
> To complete the deployment of a SQL database project, you need access to an Azure SQL or SQL Server instance. You can develop locally for free with [SQL Server developer edition](https://www.microsoft.com/sql-server/sql-server-downloads) on Windows or in [containers](../../linux/quickstart-install-connect-docker.md).

## Create a new project

We start our project by creating a new SQL database project before manually adding objects to it. There are other ways to create a project that enable immediately populating the project with objects from an existing database, such as using the [schema comparison tools](howto/compare-a-database-and-a-project.md).

::: zone pivot="sq1-visual-studio"

Select **File**, **New**, then **Project**.

In the **New Project** dialog box, use the term **SQL Server** in the search box. The top result should be **SQL Server Database Project**.

:::image type="content" source="media/getting-started/new-project-dialog.png" alt-text="Screenshot of New project dialog." lightbox="media/getting-started/new-project-dialog.png":::

Select **Next** to proceed to the next step. Provide a project name, which doesn't need to match a database name. Verify and modify the project location as needed.

Select **Create** to create the project. The empty project is opened and visible in the **Solution Explorer** for editing.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, select the **New Project** button.

:::image type="content" source="media/getting-started/projects-viewlet.png" alt-text="Screenshot of New viewlet.":::

The first prompt determines which project template to use, primarily based on whether the target platform is SQL Server or Azure SQL. If prompted to select a specific version of SQL, choose the version that matches the target database but if the target database version is unknown, choose the latest version as the value can be modified later.

Enter a project name in the text input that appears, which doesn't need to match a database name.

In the "Select a Folder" dialog that appears, select a directory for the project's folder, `.sqlproj` file, and other contents to reside in.

When prompted whether to create an SDK-style project (preview), select **Yes**.

Once completed, the empty project is opened and visible in the **Database Projects** view for editing.

::: zone-end

::: zone pivot="sq1-command-line"

With the .NET templates for Microsoft.Build.Sql projects installed, you can create a new SQL database project from the command line. The `-n` option specifies the name of the project, and the `-tp` option specifies the project target platform.

Use the `-h` option to see all available options.

```bash
# install Microsoft.Build.Sql.Templates
dotnet new sqlproject -n MyDatabaseProject
```

::: zone-end

## Add objects to the project

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Add**, then **Table**. The **Add New Item** dialog appears, where you can specify the table name. Select **Add** to create the table in the SQL project.

The table is opened in the Visual Studio table designer with the template table definition, where you can add columns, indexes, and other table properties. Save the file when you're done making the initial edits.

More database objects can be added through the **Add New Item** dialog, such as views, stored procedures, and functions. Access the dialog by right-clicking the project node in **Solution Explorer** and selecting **Add**, then the desired object type. Files in the project can be organized into folders through the **New Folder** option under **Add**.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project node and select **Add Table**. In the dialog that appears, specify the table name.

The table is opened in the text editor with the template table definition, where you can add columns, indexes, and other table properties. Save the file when you're done making the initial edits.

More database objects can be added through the context menu on the project node, such as views, stored procedures, and functions. Access the dialog by right-clicking the project node in **Database Projects** view of VS Code or Azure Data Studio, then the desired object type. Files in the project can be organized into folders through the **New Folder** option under **Add**.

::: zone-end

::: zone pivot="sq1-command-line"

Files can be added to the project by creating them in the project directory or nested folders. The file extension should be `.sql` and organization by object type or schema and object type is recommended.

The base template for a table can be used as a starting point for creating a new table object in the project:

```sql
CREATE TABLE [dbo].[Table1]
(
  [Id] INT NOT NULL PRIMARY KEY
)
```

::: zone-end

## Build the project

The build process validates the relationships between objects and the syntax against the target platform specified in the project file. The artifact output from the build process is a `.dacpac` file, which can be used to deploy the project to a target database and contains the compiled model of the database schema.

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin/Debug/projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-command-line"

SQL database projects can be built from the command line using the `dotnet build` command.

```bash
dotnet build

# optionally specify the project file
dotnet build MyDatabaseProject.sqlproj
```

The build output includes any errors or warnings and the specific files and line numbers where they occur. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin/Debug/projectname.dacpac`).

::: zone-end

## Deploy the project

The compiled model of a database schema in a `.dacpac` file can be deployed to a target database using the `SqlPackage` command-line tool or other deployment tools. The deployment process determines the necessary steps to update the target database to match the schema defined in the `.dacpac`, creating or altering objects as needed based on the objects already existing in the database. As a result, the deployment process is idempotent, meaning it can be run multiple times without causing issues and you can deploy the same `.dacpac` to multiple databases without needing to predetermine their status.

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Publish...**.

The publish dialog opens, where you establish the **target database connection**. If you don't have an existing SQL instance for deployment, LocalDB (`(localdb)\MSSQLLocalDB`) is installed with Visual Studio and can be used for testing and development.

Specify a database name and select **Publish** to deploy the project to the target database or **Generate Script** to generate a script to review before executing.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project node and select **Publish**.

> [!TIP]  
> If you don't have an available SQL instance for deployment, the SQL Database Projects extension can create a local SQL Server instance for you in a new container. With a container runtime such as Docker Desktop running, select **Publish to a new SQL server local development container** from the dropdown list.

If you have an existing SQL instance for deployment, select **Publish to an existing SQL server** and then **Don't use profile** if prompted for a publish profile.

If you haven't configured a connection to a target database, you're prompted to create a new connection. The new connection inputs ask for the server name, authentication method, and database name.

After the connection is configured, the deployment process will begin. You can choose to automatically execute the deployment (publish) or generate a script to review before executing (generate script).

::: zone-end

::: zone pivot="sq1-command-line"

The SqlPackage CLI is used to deploy a `.dacpac` file to a target database with the [publish action](../sqlpackage/sqlpackage-publish.md).

For example, to deploy a `.dacpac` file to a target database based on a connection string:

```bash
sqlpackage /Action:Publish /SourceFile:bin/Debug/projectname.dacpac /TargetConnectionString:{yourconnectionstring}
```

::: zone-end

## Get help

- [DacFx GitHub repository](https://github.com/microsoft/dacfx)

## Related content

- [Compare a database and a project](howto/compare-a-database-and-a-project.md)
- [Convert an original SQL project to an SDK-style project](howto/convert-an-original-sql-project.md)
- [Analyze T-SQL code to find defects](howto/analyze-t-sql-code-to-find-defects.md)
