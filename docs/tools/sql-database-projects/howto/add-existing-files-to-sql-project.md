---
title: Add existing files to a SQL project
description: "How to add the contents of a dacpac or SQL scripts to a project."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.service: sql
ms.topic: how-to 
ms.date: 09/10/2024
f1_keywords:
  - "SQL.DATA.TOOLS.SQLPROJECTIMPORTDATABASESUMMARYDIALOG.DIALOG"
  - "SQL.DATA.TOOLS.IMPORTSCRIPTWIZARD.SUMMARY"
  - "sql.data.tools.importscriptwizard.welcome"
  - "sql.data.tools.importscriptwizard.fileselection"

---

# Add existing files to a SQL project

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

As a concept, once a SQL project is created, objects can be added to it one at a time or in bulk. Adding objects one at a time is straightforward, while adding objects in bulk can be done from the contents of a `.dacpac` file or one or more SQL scripts. This article explains how to add the contents of a dacpac or SQL scripts to a SQL project and the tradeoffs between the sources.

## Import from a `.dacpac` file

A `.dacpac` file is a compiled database model and requires specific tooling to read and apply the file. A `.dacpac` file can be generated as a SQL project build artifact or from an existing database, and you may be provided with one with no access to the source. In addition to the many tools that can apply a `.dacpac` to a database, SQL Server Data Tools (SSDT) in Visual Studio can import the contents of a `.dacpac` file directly into a project. The ability to import schema from a database or a .dacpac file is only available if there are no schema objects already defined in the project.

:::image type="content" source="media/add-existing-files-to-sql-project/vs-import-menu.png" alt-text="Screenshot of the import menu on a SQL project in Visual Studio SSDT.":::

On import, object definitions are scripted into project files using SSDT's organizational defaults for new objects: new files for top level objects, hierarchical children defined in the same file as the parent, table/column constraints defined inline where possible. For more targeted visibility and control for each object, use Schema Compare instead of Import. If the import source contains Pre- and Post-Deployment Scripts, RefactorLogs, or SQLCMD variable definitions, they are imported into the project. If the project already contains any one of these artifacts, the imported files are added to an Ignored on Import folder in the project.

If Visual Studio and SQL Server Data Tools isn't available, you can either:

- Use the Schema Compare extension in Azure Data Studio to compare the contents of a `.dacpac` file to a project, then selectively apply the changes to the project.
- Use the [SqlPackage](../../sqlpackage/sqlpackage.md) command-line utility to import the contents of a `.dacpac` file into a database, then [create a project from the database](../tutorials/start-from-existing-database.md).

## Import from SQL scripts

T-SQL scripts can be imported into a SQL project in two ways: by adding the script files to the project directory or by processing the contents of the scripts in Visual Studio. The method you choose depends on the project type and the desired level of control over the import process. In both cases, the syntax of the script files must be valid.

### Add files to a project

With SDK-style SQL projects, you can add existing SQL scripts to a project by placing them in the project directory because Microsoft.Build.Sql automatically includes any `*.sql` files in the project. If you're using a non-SDK-style project, you must import existing SQL scripts into the project by [utilizing the script processing in Visual Studio](#process-contents-of-files). The `*.sql` files automatically included in the project are included in the database model build as SQL objects.

A file added to the project folder containing a duplicate object definition to an object already present in the project causes the project build to fail. You need to manually resolve the conflict by removing the duplicate object or renaming one of the objects.

To add a file to a project as a pre/post-deployment script, in addition to adding the file to the project directory, you must also include the file in the project file. For example, to add a file named `Pre-DeploymentScript.sql` as a pre-deployment script, add the following to the project file:

```xml
<ItemGroup>
  <PreDeploy Include="Pre-DeploymentScript.sql" />
</ItemGroup>
```

More information on [pre/post deployment scripts](../concepts/pre-post-deployment-scripts.md#sql-project-file-sample-and-syntax) can be found in the SQL projects documentation.

### Process contents of files

SQL Server Data Tools (SSDT) in Visual Studio also has the capability to process the contents of SQL scripts while adding them to an original-style project. During this processing, if a script contains an object already defined in the project, the object's definition are updated to match the script. If the script contains an object not already defined in the project, a new file is created for the object.

There are known issues where the script processing may result in duplicate constraint and encryption key statements. If you encounter these issues, utilize the build output window to identify the source of the duplicates and manually remove them from the project.

The Import from Script process doesn't incorporate Pre/Post-Deployment scripts, SQLCMD variables, or RefactorLog files. These and any other unsupported constructs that are detected on import are placed in a ScriptsIgnoredOnImport.sql file in a Scripts folder in your project.

## Related content

- [Create a project from a database](../tutorials/start-from-existing-database.md)
- [Schema compare overview](../concepts/schema-comparison.md)
