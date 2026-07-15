---
title: SQL Database Projects Refactor Overview
description: Learn what a refactor log is, why it matters, and how SQL projects and DacFx use it during deployment.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 07/14/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ms.collection:
  - data-tools
---

# Refactor objects in SQL database projects

Refactoring in a SQL database project includes operations such as renaming a table, column, or other database object, as well as moving objects between schemas. The refactor log file records refactoring operations. This XML file describes the intent of the changes. The SQL project includes the refactor log, and the build process packages it into the `.dacpac`.

When you deploy a SQL project, the dynamically generated deployment plan uses the refactor log to interpret changes as renames or moves instead of drop-and-create operations. It also checks the target database to determine which operations are already applied. The `[dbo].[__RefactorLog]` table tracks which operations are applied on a per-database basis.

Without a refactor log, deployment interprets a rename as:

- Drop old object
- Create new object

With a refactor log, deployment interprets the same change as:

- Rename existing object in place

## How refactor log processing works

At a high level, the lifecycle of a refactor log is as follows:

1. You include a `.refactorlog` file in the SQL project either from an IDE rename or refactor operation or by manually creating the file.
1. SQL project build packages the refactor log into the `.dacpac` as `refactor.xml`.
1. Deployment reads the refactor operations from the `.dacpac`.
1. If an operation isn't already applied, deployment emits rename or schema transfer operations.
1. Deployment records operations by key in `[dbo].[__RefactorLog]`.

## Rename operations

`sp_rename` is a SQL Server system stored procedure that renames a database object. SQL projects refactoring uses the same underlying logic as `sp_rename` to rename objects in the target database. The refactor log records the rename operation, and the deployment plan uses the refactor log to interpret the change as a rename instead of a drop-and-create operation.

### Rename objects in Visual Studio Code

SQL projects refactoring integrates with the Visual Studio Code [rename symbol](https://code.visualstudio.com/docs/editing/editingevolved#_rename-symbol) feature. You can rename a database object in the text editor, and the refactor log automatically updates with the rename operation.

Right-click a database object identifier in the text editor and select **Rename Symbol** to rename the object. The text area that appears provides the option to immediately rename the object or to preview the changes. If you choose to preview, a list of all references to the object is displayed and you can select which references to rename. The refactor log updates with the rename operation and the SQL project updates with the new object name.

### Rename objects in Visual Studio

SQL projects refactoring in Visual Studio is grouped under a context menu branch called **Refactor**. You can rename a database object in the text editor, and the refactor log automatically updates with the rename operation.

Right-click a database object identifier in the text editor and select **Refactor** > **Rename...** to rename the object. A dialog appears where you enter the new name. The dialog provides the option to immediately rename the object or to preview the changes. If you choose to preview, a list of all references to the object is displayed and you can select which references to rename. The refactor log updates with the rename operation and the SQL project updates with the new object name.

### Rename objects in SQL Server Management Studio

Rename operations in SQL Server Management Studio (SSMS) aren't currently integrated with SQL projects. You can modify database object definitions in a SQL project in SSMS and then manually create the entry in a refactor log file. The refactor log [reference section](#refactor-log-xml-reference) describes the XML structure and required nodes for a rename operation in the refactor log and SQL project file.

## Move schema operations

`ALTER SCHEMA ... TRANSFER` is a SQL Server statement that moves a database object to a different schema. SQL projects refactoring uses the same underlying logic as `ALTER SCHEMA ... TRANSFER` to move objects in the target database. The refactor log records the move operation, and the deployment plan uses the refactor log to interpret the change as a move instead of a drop-and-create operation.

### Move objects to a different schema in Visual Studio Code

SQL projects refactoring integrates with the Visual Studio Code [refactoring operations](https://code.visualstudio.com/docs/editing/refactoring) feature. You can move a database object to a different schema in the text editor, and the refactor log automatically updates with the move operation.

Right-click a database object identifier in the text editor and select **Refactor** > **Move to Schema** to move the object. A list of available schemas is displayed. When you select the target schema, the **Refactor Preview** panel shows the changes to be applied. If you choose to apply the changes, the object is moved to the new schema, all references to the object are updated, and the refactor log updates with the move operation.

### Move objects to a different schema in Visual Studio

SQL projects refactoring in Visual Studio is grouped under a context menu branch called **Refactor**. You can move a database object to a different schema in the text editor, and the refactor log automatically updates with the move operation.

Right-click a database object identifier in the text editor and select **Refactor** > **Move to Schema...** to move the object. A dialog appears where you select the target schema. The dialog provides the option to immediately move the object or to preview the changes. If you choose to preview, a list of all references to the object is displayed and you can select which references to update. The refactor log updates with the move operation and the SQL project updates with the new schema.

### Move objects to a different schema in SQL Server Management Studio

Schema move operations in SQL Server Management Studio (SSMS) don't currently integrate with SQL projects. You can modify database object definitions in a SQL project in SSMS and then manually create the entry in a refactor log file. The refactor log [reference section](#refactor-log-xml-reference) describes the XML structure and required nodes for a move operation in the refactor log and SQL project file.

## Refactor log XML reference

A refactor log is XML with an `Operations` root node and one or more `Operation` entries. The following example refactor log shows two operations: a rename of a table and a move of that table to a new schema.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Operations Version="1.0" xmlns="http://schemas.microsoft.com/sqlserver/dac/Serialization/2012/02">
  <Operation Name="Rename Refactor" Key="71b7b0db-6d06-49bd-9df5-1f653caa1023" ChangeDateTime="07/07/2026 18:08:35">
    <Property Name="ElementName" Value="[dbo].[Table1]" />
    <Property Name="ElementType" Value="SqlTable" />
    <Property Name="ParentElementName" Value="[dbo]" />
    <Property Name="ParentElementType" Value="SqlSchema" />
    <Property Name="NewName" Value="Table1Renamed" />
  </Operation>
  <Operation Name="Move Schema" Key="14c2cc0f-4744-473f-807e-8fbc323b325b" ChangeDateTime="07/07/2026 18:08:35">
    <Property Name="ElementName" Value="[dbo].[Table1Renamed]" />
    <Property Name="ElementType" Value="SqlTable" />
    <Property Name="NewSchema" Value="app" />
    <Property Name="IsNewSchemaExternal" Value="False" />
  </Operation>
</Operations>
```

Manually authoring a refactor log requires editing the `refactorlog.xml` file in a text editor and inserting a `<RefactorLog />` entry in the SQL project file.

### Author a refactor log

If you author XML manually, follow these practices:

- Use a new GUID for each distinct refactor operation key.
- Keep `ElementName` and `ElementType` aligned with the source model.
- Use correct SQL identifier formatting in names (for example, `[schema].[object]`).
- Keep operation order stable when multiple dependent renames exist.
- Validate XML structure before build.

If the XML is invalid or required nodes are missing, build or packaging can fail.

Key points:

- `Name` identifies the refactor operation type, such as `Rename Refactor`.
- `Key` is a unique operation identifier used for idempotency tracking.
- `Property` nodes describe the source object and the intended new name.

### Incorporate a refactor log into a SQL project

The build process needs the refactor log entry in the SQL project file to include the refactor log in the `.dacpac`. The following example shows how to include a refactor log in a SQL project file:

```xml
  <ItemGroup>
    <RefactorLog Include="AdventureWorks.refactorlog" />
  </ItemGroup>
```

## Related concepts

- [SQL projects properties](project-properties.md)
- [Tutorial: Create and deploy a SQL project](../tutorials/create-deploy-sql-project.md)
- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../sqlpackage/sqlpackage-publish.md)
