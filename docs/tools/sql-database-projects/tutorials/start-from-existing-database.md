---
title: Start from an existing database
description: "Create a SQL project with objects from an existing database."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: tutorial
zone_pivot_groups: sq1-sql-projects-tools
f1_keywords:
  - "sql.data.tools.dbprojectwizard.importschema"
  - "sql.data.tools.SqlProjectImportDatabaseDialog.dialog"
---

# Tutorial: start from an existing database

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

SQL projects contain declarative (CREATE statement) files for all the objects in a database, such as tables, views, and stored procedures. These files can be used to create new databases, update existing databases, or even just to track the database in source control. Often we are starting with a SQL project when we have an existing database and want to create objects in the SQL project that match the database with minimal effort.

Some SQL projects tools include a single step for creating a new SQL project from an existing database. Other tools require a few steps to create a new SQL project and then import objects from an existing database. Except for the Visual Studio (SQL Server Data Tools) instructions, the guide focuses on SDK-style SQL projects.

With [option 1](#option-1-create-a-new-sql-project-from-an-existing-database) in this tutorial, you:

**Step 1:** create a new SQL project from an existing database
**Step 2:** build the SQL project

With [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) in this tutorial, you:

- **Step 1:** create a new empty SQL project
- **Step 2:** import objects from an existing database
- **Step 3:** build the SQL project

## Prerequisites

::: zone pivot="sq1-visual-studio"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools (SSDT) installed in Visual Studio 2022](../../../ssdt/download-sql-server-data-tools-ssdt.md)

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools, SDK-style (preview) installed in Visual Studio 2022](../../sql/ssdt/download-sql-server-data-tools-ssdt-sdk.md)
- [SqlPackage CLI](../../sqlpackage/sqlpackage-download.md)

```bash
# install SqlPackage CLI
dotnet tool install -g Microsoft.SqlPackage
```

::: zone-end

::: zone pivot="sq1-visual-studio-code"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [VS Code](https://code.visualstudio.com/Download)
- [SQL Database Projects extension for Azure Data Studio](/azure-data-studio/extensions/sql-database-project-extension) or [SQL Database Projects extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-database-projects-vscode)

::: zone-end

::: zone pivot="sq1-command-line"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SqlPackage CLI](../../sqlpackage/sqlpackage-download.md)
- [Microsoft.Build.Sql.Templates .NET templates](https://www.nuget.org/packages/Microsoft.Build.Sql.Templates/)

```bash
# install SqlPackage CLI
dotnet tool install -g Microsoft.SqlPackage

# install Microsoft.Build.Sql.Templates
dotnet new install Microsoft.Build.Sql.Templates
```

::: zone-end

> [!NOTE]  
> To complete the tutorial, you need access to an Azure SQL or SQL Server instance. You can develop locally for free with [SQL Server developer edition](https://www.microsoft.com/sql-server/sql-server-downloads) on Windows or in [containers](../../../linux/quickstart-install-connect-docker.md).

## Option 1: Create a new SQL project from an existing database

### Step 1: Create a new SQL project from an existing database

::: zone pivot="sq1-visual-studio"

From the **SQL Server object explorer** in Visual Studio, right-click the database you want to create a project from and select **Create New Project...**.

:::image type="content" source="media/start-from-existing-database/ssdt-import-database.png" alt-text="Screenshot of Import Database dialog in Visual Studio.":::

In the **Create New Project** dialog, provide a project name, which doesn't need to match a database name. Verify and modify the project location as needed. The default import settings import the objects into folders by schema, then object type. You can modify the import settings to change the folder structure or to include permissions in the objects being imported. **Start** the import.

While the import proceeds, progress is displayed as messages in the **Import Database** dialog. When the import is complete, the imported objects are visible in the **Solution Explorer** and the logs are stored in a file in the project directory under `Import Schema Logs`. Select **Finish**.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Option 1 isn't available for the command line. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the database object explorer view in VS Code or Azure Data Studio, select a database you want to create a project from. Right-click the database and select **Create Project from Database**.

:::image type="content" source="media/start-from-existing-database/ads-new-project-from-database.png" alt-text="Screenshot of Create project from database dialog in Azure Data Studio.":::

In Azure Data Studio, the **Create project from database** dialog requires the project name and location to be selected. The default import settings import the objects into folders by schema, then object type. You can select a different folder structure or to include permissions in the objects being imported before selecting **Create**.

In VS Code, the command prompts ask for a project name and location. The default import settings import the objects into folders by schema, then object type. You can select a different folder structure or to include permissions in the objects being imported before the import begins.

Open the **Database Projects** view to see the new project and imported object definitions.

::: zone-end

::: zone pivot="sq1-command-line"

Option 1 isn't available for the command line. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

### Step 2: Build the SQL project

The build process validates the relationships between objects and the syntax against the target platform specified in the project file. The artifact output from the build process is a `.dacpac` file, which can be used to deploy the project to a target database and contains the compiled model of the database schema.

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Option 1 isn't available for the command line. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin/Debug/projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-command-line"

Option 1 isn't available for the command line. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

## Option 2: Create a new empty SQL project and import objects from an existing database

Alternatively, the project creation and object import steps can be done separately.

### Step 1: Create a new empty SQL project

We start our project by creating a new SQL database project before importing our objects to it.

::: zone pivot="sq1-visual-studio"

Select **File**, **New**, then **Project**.

In the **New Project** dialog box, use the term **SQL Server** in the search box. The top result should be **SQL Server Database Project**.

:::image type="content" source="media/start-from-existing-database/new-project-dialog.png" alt-text="Screenshot of New project dialog." lightbox="media/start-from-existing-database/new-project-dialog.png":::

Select **Next** to proceed to the next step. Provide a project name, which doesn't need to match a database name. Verify and modify the project location as needed.

Select **Create** to create the project. The empty project is opened and visible in the **Solution Explorer** for editing.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Select **File**, **New**, then **Project**.

In the **New Project** dialog box, use the term **SQL Server** in the search box. The top result should be **SQL Server Database Project, SDK-style (preview)**.

:::image type="content" source="media/start-from-existing-database/vs-sdk-new-project-dialog.png" alt-text="Screenshot of New project dialog." lightbox="media/start-from-existing-database/vs-sdk-new-project-dialog.png":::

Select **Next** to proceed to the next step. Provide a project name, which doesn't need to match a database name. Verify and modify the project location as needed.

Select **Create** to create the project. The empty project is opened and visible in the **Solution Explorer** for editing.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, select the **New Project** button.

:::image type="content" source="media/start-from-existing-database/projects-viewlet.png" alt-text="Screenshot of New viewlet.":::

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

### Step 2: Import objects from an existing database

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Import -> Database...**. If this option is disabled, your database project might have objects created in it. You can delete the objects or create a new project.

In the **Import Database** dialog, select the connection to the database you want to import objects from. If you've connected to the database in SQL Server object explorer, it's present in the **history** list.

:::image type="content" source="media/start-from-existing-database/ssdt-import-database.png" alt-text="Screenshot of Import Database dialog in Visual Studio.":::

The default import settings import the objects into folders by schema, then object type. You can modify the import settings to change the folder structure or to include permissions in the objects being imported. **Start** the import.

While the import proceeds, progress is displayed as messages in the **Import Database** dialog. When the import is complete, the imported objects are visible in the **Solution Explorer** and the logs are stored in a file in the project directory under `Import Schema Logs`. Select **Finish** to return to the project.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

We will use the SqlPackage CLI to import objects from an existing database to the new SQL database project we created in Visual Studio in step 1. The following SqlPackage command imports the schema of a database to a folder `MyDatabaseProject` organized by nested schema and object type folders.

```bash
sqlpackage /a:Extract /ssn:localhost /sdn:MyDatabase /tf:MyDatabaseProject /p:ExtractTarget=SchemaObjectType
```

When these folders are placed in an SDK-style SQL database project folder, they're automatically included in the project without the need to import them or modify the SQL project file.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

> [!NOTE]  
> The SQL Database Projects extension in VS Code doesn't support importing objects from a database into a project. Use the SQL Database Projects extension in Azure Data Studio to import objects from a database or follow [option 1](#option-1-create-a-new-sql-project-from-an-existing-database) to create a new project from an existing database in VS Code.

In the SQL Database Projects extension in Azure Data Studio, open the **Database Projects** view. Right-click the project node and select **Update project from database**.

:::image type="content" source="media/start-from-existing-database/ads-update-project.png" alt-text="Screenshot of Update Database dialog in Azure Data Studio.":::

In the **Update Database** dialog, select the connection to the database you want to import objects from. If you've connected to the database in the **Connections** view, it's present in the **history** list.

Select either **View changes in schema compare** to review and choose a subset of objects to import or **Apply all changes** to import all objects.

::: zone-end

::: zone pivot="sq1-command-line"

The SqlPackage CLI can be used to extract the schema of an existing database to a `.dacpac` file or individual `.sql` files. The following SqlPackage command extracts the schema of a database to a `.sql` files organized by nested schema and object type folders.

```bash
sqlpackage /a:Extract /ssn:localhost /sdn:MyDatabase /tf:MyDatabaseProject /p:ExtractTarget=SchemaObjectType
```

When these folders are placed in an SDK-style SQL database project folder, they're automatically included in the project without the need to import them or modify the SQL project file.

::: zone-end

### Step 3: Build the SQL project

The build process validates the relationships between objects and the syntax against the target platform specified in the project file. The artifact output from the build process is a `.dacpac` file, which can be used to deploy the project to a target database and contains the compiled model of the database schema.

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

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

## Related content

- [Get started with SQL database projects](../get-started.md)
- [Tutorial: Create and deploy a SQL project](create-deploy-sql-project.md)
- [Compare a database and a project](../howto/compare-database-project.md)
