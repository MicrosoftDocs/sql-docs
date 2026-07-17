---
title: Start from an Existing Database
description: Create a SQL project with objects from an existing database.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 05/12/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: tutorial
ms.collection:
  - data-tools
ms.custom:
  - ignite-2024
f1_keywords:
  - "sql.data.tools.dbprojectwizard.importschema"
  - "sql.data.tools.SqlProjectImportDatabaseDialog.dialog"
zone_pivot_groups: sq1-sql-projects-tools
---

# Tutorial: start from an existing database

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

SQL projects contain declarative (`CREATE` statement) files for all the objects in a database, such as tables, views, and stored procedures. You can use these files to create new databases, update existing databases, or track the database in source control. Often, you start with a SQL project when you have an existing database and want to create objects in the SQL project that match the database with minimal effort.

Some SQL project tools include a single step for creating a new SQL project from an existing database. Other tools require a few steps to create a new SQL project and then import objects from an existing database. Except for the Visual Studio (SQL Server Data Tools) instructions, this guide focuses on SDK-style SQL projects.

With [option 1](#option-1-create-a-new-sql-project-from-an-existing-database) in this tutorial, you:

- **Step 1:** Create a new SQL project from an existing database
- **Step 2:** Build the SQL project

With [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) in this tutorial, you:

- **Step 1:** Create a new empty SQL project
- **Step 2:** Import objects from an existing database
- **Step 3:** Build the SQL project

## Prerequisites

::: zone pivot="sq1-visual-studio"

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [Install SQL Server Data Tools (SSDT) for Visual Studio](../../../ssdt/download-sql-server-data-tools-ssdt.md)

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools, SDK-style (preview)](../../../ssdt/sql-server-data-tools-sdk-style.md)
- [SqlPackage CLI](../../sqlpackage/sqlpackage-download.md)

```bash
# install SqlPackage CLI
dotnet tool install -g Microsoft.SqlPackage
```

::: zone-end

::: zone pivot="sq1-visual-studio-code"

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio Code](https://code.visualstudio.com/Download)
- [SQL Database Projects extension](../../visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md)

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server Management Studio (SSMS)](/ssms/install/install)
- [Database DevOps workload installed in SSMS](/ssms/install/modify)

::: zone-end

::: zone pivot="sq1-command-line"

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
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

From the **SQL Server Object Explorer** in Visual Studio, right-click the database you want to create a project from and select **Create New Project...**.

:::image type="content" source="media/start-from-existing-database/ssdt-import-database.png" alt-text="Screenshot of Import Database dialog in Visual Studio.":::

In the **Create New Project** dialog, enter a project name. The project name doesn't need to match a database name. Verify and modify the project location as needed. The default import settings import the objects into folders by schema, then object type. You can modify the import settings to change the folder structure or to include permissions in the objects being imported. **Start** the import.

The **Import Database** dialog displays the import progress as messages. When the import finishes, you can see the imported objects in the **Solution Explorer**. The process stores the logs in a file in the project directory under `Import Schema Logs`. Select **Finish**.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Option 1 isn't available for SDK-style SQL projects in Visual Studio. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the database object explorer view in Visual Studio Code, select a database you want to create a project from. Right-click the database and select **Create Project from Database**.

:::image type="content" source="media/start-from-existing-database/ads-new-project-from-database.png" alt-text="Screenshot of Create project from database dialog in Visual Studio Code.":::

In Visual Studio Code, the **Create project from database** dialog requires the project name and location. The default import settings import the objects into folders by schema, then object type. You can select a different folder structure or choose to include permissions in the objects being imported before selecting **Create**.

Open the **Database Projects** view to see the new project and imported object definitions.

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

From the **Object Explorer** in SQL Server Management Studio, right-click the database you want to create a project from and select **Create Project from Database...** under the **Tasks** menu.

:::image type="content" source="media/start-from-existing-database/ssms-new-project-from-database.png" alt-text="Screenshot of New SQL Project from Database dialog in SQL Server Management Studio.":::

In the **Create SQL Project from Database** dialog, verify the selected database and modify the project name and location as needed. The new project will be created with the objects from the selected database and arranged in folders by schema, then object type.

A solution file is automatically created in the folder above the project folder, and opens in the **Solution Explorer** after creation. If you want to add your new project to an existing solution, select **Add to solution** and browse to the solution file. Select **Create** to create the project.

::: zone-end

::: zone pivot="sq1-command-line"

Option 1 isn't available for the command line. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

### Step 2: Build the SQL project

The build process validates the relationships between objects and the syntax against the target platform specified in the project file. The artifact output from the build process is a `.dacpac` file, which you can use to deploy the project to a target database. This file contains the compiled model of the database schema.

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, the output window displays them. On a successful build, the build artifact (`.dacpac` file) is created and its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Option 1 isn't available for SDK-style SQL projects in Visual Studio. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of Visual Studio Code, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, the output window displays them. On a successful build, the build artifact (`.dacpac` file) is created and its location is included in the build output (default is `bin/Debug/projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

Option 1 isn't available for SQL Server Management Studio. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

::: zone pivot="sq1-command-line"

Option 1 isn't available for the command line. Use [option 2](#option-2-create-a-new-empty-sql-project-and-import-objects-from-an-existing-database) instead.

::: zone-end

## Option 2: Create a new empty SQL project and import objects from an existing database

Alternatively, you can separate the project creation and object import steps.

### Step 1: Create a new empty SQL project

Start your project by creating a new SQL database project before importing your objects to it.

::: zone pivot="sq1-visual-studio"

Select **File**, **New**, and then **Project**.

In the **New Project** dialog box, use the term **SQL Server** in the search box. The top result is **SQL Server Database Project**.

:::image type="content" source="media/start-from-existing-database/new-project-dialog.png" alt-text="Screenshot of New project dialog." lightbox="media/start-from-existing-database/new-project-dialog.png":::

Select **Next** to proceed to the next step. Enter a project name, which doesn't need to match a database name. Verify and modify the project location as needed.

Select **Create** to create the project. The empty project opens and is visible in the **Solution Explorer** for editing.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Select **File**, **New**, and then **Project**.

In the **New Project** dialog box, use the term **SQL Server** in the search box. The top result is **SQL Server Database Project, SDK-style (preview)**.

:::image type="content" source="media/start-from-existing-database/vs-sdk-new-project-dialog.png" alt-text="Screenshot of New project dialog." lightbox="media/start-from-existing-database/vs-sdk-new-project-dialog.png":::

Select **Next** to proceed to the next step. Enter a project name, which doesn't need to match a database name. Verify and modify the project location as needed.

Select **Create** to create the project. The empty project opens and is visible in the **Solution Explorer** for editing.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of Visual Studio Code, select the **New Project** button.

:::image type="content" source="media/start-from-existing-database/projects-viewlet.png" alt-text="Screenshot of New viewlet.":::

The first prompt determines which project template to use, primarily based on whether the target platform is SQL Server or Azure SQL. If prompted to select a specific version of SQL, choose the version that matches the target database. If you don't know the target database version, choose the latest version as the value can be modified later.

Enter a project name in the text input that appears, which doesn't need to match a database name.

In the **Select a Folder** dialog that appears, select a directory for the project's folder, `.sqlproj` file, and other contents to reside in.

When prompted whether to create an SDK-style project, select **Yes**.

When completed, the empty project opens and is visible in the **Database Projects** view for editing.

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

Select **File**, **New**, and then **Project**.

In the **New Project** dialog box, select **SQL Database Project**.

:::image type="content" source="media/create-deploy-sql-project/new-project-dialog-ssms.png" alt-text="Screenshot of New project dialog." lightbox="media/create-deploy-sql-project/new-project-dialog-ssms.png":::

Select **Next** to proceed to the next step. Enter a project name, which doesn't need to match a database name. Verify and modify the project location as needed.

Select **Create** to create the project. The empty project opens and is visible in the **Solution Explorer** for editing.

::: zone-end

::: zone pivot="sq1-command-line"

When you install the .NET templates for Microsoft.Build.Sql projects, you can create a new SQL database project from the command line. The `-n` option specifies the name of the project, and the `-tp` option specifies the project target platform.

Use the `-h` option to see all available options.

```bash
# create a new SQL database project
dotnet new sqlproj -n MyDatabaseProject
```

::: zone-end

### Step 2: Import objects from an existing database

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Import -> Database...**. If this option is disabled, your database project might have objects created in it. You can delete the objects or create a new project.

In the **Import Database** dialog, select the connection to the database you want to import objects from. If you connected to the database in SQL Server Object Explorer, it appears in the **history** list.

:::image type="content" source="media/start-from-existing-database/ssdt-import-database.png" alt-text="Screenshot of Import Database dialog in Visual Studio.":::

The default import settings import the objects into folders by schema, then object type. You can modify the import settings to change the folder structure or to include permissions in the objects being imported. **Start** the import.

While the import proceeds, progress is displayed as messages in the **Import Database** dialog. When the import is complete, the imported objects are visible in the **Solution Explorer** and the logs are stored in a file in the project directory under `Import Schema Logs`. Select **Finish** to return to the project.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Use the SqlPackage CLI to import objects from an existing database to the new SQL database project you created in Visual Studio in step 1. The following SqlPackage command imports the schema of a database to a folder `MyDatabaseProject` organized by nested schema and object type folders.

```bash
sqlpackage /a:Extract /ssn:localhost /sdn:MyDatabase /tf:MyDatabaseProject /p:ExtractTarget=SchemaObjectType
```

When you place these folders in an SDK-style SQL database project folder, they're automatically included in the project without the need to import them or modify the SQL project file.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the SQL Database Projects extension in Visual Studio Code, open the **Database Projects** view. Right-click the project node and select **Update project from database**.

:::image type="content" source="media/start-from-existing-database/ads-update-project.png" alt-text="Screenshot of Update Database dialog in Visual Studio Code.":::

In the **Update Database** dialog, select the connection to the database you want to import objects from. If you connected to the database in the **Connections** view, it appears in the **history** list.

Select either **View changes in schema compare** to review and choose a subset of objects to import or **Apply all changes** to import all objects.

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

In **Solution Explorer**, right-click the project node and select **Import From Database...**.

In the **Import Database** dialog, select the connection to the database you want to import objects from.

:::image type="content" source="media/start-from-existing-database/ssms-import-database.png" alt-text="Screenshot of Import Database dialog in SSMS.":::

The default import settings import all objects from the database into the project and arrange them in folders by schema and object type, but cancel the import if any objects in the project are overwritten. You can modify the import to update the project with objects from the database by selecting **Overwrite existing objects in the project**. Select **Import** to begin the import.

When the import completes, **Solution Explorer** is updated with the imported objects.

::: zone-end

::: zone pivot="sq1-command-line"

Use the SqlPackage CLI to extract the schema of an existing database to a `.dacpac` file or individual `.sql` files. The following SqlPackage command extracts the schema of a database to `.sql` files organized by nested schema and object type folders.

```bash
sqlpackage /a:Extract /ssn:localhost /sdn:MyDatabase /tf:MyDatabaseProject /p:ExtractTarget=SchemaObjectType
```

When you place these folders in an SDK-style SQL database project folder, they're automatically included in the project without the need to import them or modify the SQL project file.

::: zone-end

### Step 3: Build the SQL project

The build process validates the relationships between objects and the syntax against the target platform specified in the project file. The artifact output from the build process is a `.dacpac` file, which you can use to deploy the project to a target database. This file contains the compiled model of the database schema.

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, the output window displays them. On a successful build, the build artifact (`.dacpac` file) is created and its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, the output window displays them. On a successful build, the build artifact (`.dacpac` file) is created and its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of Visual Studio Code, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, the output window displays them. On a successful build, the build artifact (`.dacpac` file) is created and its location is included in the build output (default is `bin/Debug/projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

In **Solution Explorer**, right-click the project node and select **Build**.

:::image type="content" source="media/create-deploy-sql-project/ssms-solution-explorer.png" alt-text="Screenshot of Solution Explorer in SQL Server Management Studio with the Build option available.":::

The output window automatically opens to display the build process. If there are errors or warnings, the output window displays them. On a successful build, the build artifact (`.dacpac` file) is created and its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-command-line"

You can build SQL database projects from the command line using the `dotnet build` command.

```bash
dotnet build

# optionally specify the project file
dotnet build MyDatabaseProject.sqlproj
```

The build output includes any errors or warnings and the specific files and line numbers where they occur. On a successful build, the build artifact (`.dacpac` file) is created and its location is included in the build output (default is `bin/Debug/projectname.dacpac`).

::: zone-end

## Related content

- [Get started with SQL database projects](../get-started.md)
- [Tutorial: Create and deploy a SQL project](create-deploy-sql-project.md)
- [Compare a database and a project](../howto/compare-database-project.md)
