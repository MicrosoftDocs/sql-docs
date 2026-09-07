---
title: Schema Comparison Overview
description: Visualize the difference in database models with schema compare.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, tsiddique
ms.date: 06/09/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: overview
ms.collection:
  - data-tools
ms.custom:
  - ignite-2024
f1_keywords:
  - "sql.data.tools.schemacompare.SchemaCompareOptionsDialog"
  - "sql.data.tools.schemacompare.watermark.f1"
  - "sql.data.tools.schemacompare.f1"
  - "sql.data.tools.schemacompare.connectiondialog.f1"
  - "sql.data.tools.schemacompare.connectiondialog.error.f1"
zone_pivot_groups: sq1-sql-projects-tools
---

# Schema comparison overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The schema comparison tooling enables you to compare two database definitions. You can use any combination of connected database, SQL database project, or `.dacpac` file as the source and target of the comparison. When the comparison finishes, you see the results as a set of actions that make the target the same as the source. Differences between the database models appear in a similar manner as a source control diff. If the schema compare target is a SQL project or a database, you can update the target directly from the schema compare interface, or generate an update script that has the same effect.

:::image type="content" source="media/schema-comparison/schema-compare-concept.png" alt-text="Screenshot of differences between a package and database as a concept." lightbox="media/schema-comparison/schema-compare-concept.png":::

Schema Compare provides the following features:

- Compare schemas between two `.dacpac` files, databases, or SQL projects.
- View results as a set of actions to match a target against the source.
- Selectively exclude actions listed in results.
- Set options that control the scope of the comparison.
- Apply changes directly to the target, or generate a script to apply changes at a later time.
- Save the comparison.

## Functionality

The differences between source and target appear in a grid for easy review. You can compare in either direction between a database model derived from any of the following options:

- connected database
- SQL database project
- `.dacpac` file

In schema compare, you can drill into and review each difference in the results grid or in script form where details of the changes are available at a per-line level. You can also selectively exclude specific differences before updating the target. The schema compare tooling is available in Visual Studio, Visual Studio Code, and the command line.

### Schema comparison options

The schema comparison options are based on the [deployment options](/dotnet/api/microsoft.sqlserver.dac.compare.schemacomparison.options) available in the DacFx .NET library. These options include:

- Ignore whitespace
- Ignore partition schemes
- Ignore column order
- Drop indexes not in source
- Block on possible data loss

You can also configure the object types that are included in the comparison. These objects include tables, stored procedures, indexes, permissions, user-defined types, and more.

### Schema compare files

You can save the comparison definition for schema compare as an `.scmp` file, known as a *schema compare file*. This file stores information about the schema comparison in XML and includes:

- Source and target connection information
- Comparison options
- Excluded object types

You can open an `.scmp` file in Visual Studio to run the same comparison again later, or to share the comparison with others.

## Launch and use schema compare

::: zone pivot="sq1-visual-studio"

1. On the **Tools** menu in Visual Studio, select **SQL Server**, and then select **New Schema Comparison**.

   Alternatively, right-click the **TradeDev** project in **Solution Explorer**, and select **Schema Compare**.

   The **Schema Compare** window opens, and Visual Studio automatically assigns it a name such as `SqlSchemaCompare1`.

   Two dropdown lists with a green arrow in between them appear just below the **Schema Compare** window toolbar. These menus allow you to select database definitions for your comparison source and target.

2. In the **Select Source** dropdown list, choose **Select Source** and the **Select Source Schema** dialog opens.

   If you open the **Schema Compare** window by right-clicking the project name, the source schema is already populated and you can proceed to step 4.

   :::image type="content" source="media/schema-comparison/vs-schema-compare-source.png" alt-text="Screenshot of Schema comparison source select dialog in Visual Studio.":::

3. Complete the selections for a schema comparison source by choosing a **Project**, **Database** connection, or **.dacpac** file. The source is the database definition that you want to use as the basis for changes to the target.

4. From the **Select Target** dropdown list in the **Schema Compare Window**, choose **Select Target**. The **Select Target Schema** dialog opens. Complete the selections for a schema comparison target by choosing a **Project**, **Database** connection, or **.dacpac** file. The target is the database definition that you want to evaluate and potentially apply changes to.

5. Select **Options** from the **Schema Compare Window** toolbar to specify which objects are compared, what types of differences are ignored, and other settings.

6. Select the **Compare** button in the **Schema Compare Window** toolbar to start the comparison process.

   When the comparison is complete, the structural differences between the project and the database appear in the **Results** pane in the upper part of the window. By default, the comparison results are grouped by action (such as Delete, Change, or Add). The **Results** pane displays a row for each database object that differs between the database definitions. Each row identifies the object in the source or target schema (or both) and the action that would be taken on the target schema to make the target object the same as the source object. If an object was refactored and either renamed or moved to a new schema, the source and target names are different, and the source name appears in bold font to highlight the difference.

   :::image type="content" source="media/schema-comparison/ssdt-schema-compare.png" alt-text="Screenshot of Schema comparison interface in Visual Studio comparing a database against a project." lightbox="media/schema-comparison/ssdt-schema-compare.png":::

   By default, the results list hides objects that are the same in both schemas or that aren't supported for update, such as built-in objects. Select the appropriate filter options in the toolbar to show these objects.

   To change the grouping preference, select the **Group Results** dropdown list in the toolbar. Select **Type** to group the results by object type (for example, by tables, views, or stored procedures).

7. By default all differences are included in the scope of the Update Target action. You can exclude differences that you don't want to synchronize. To do so, uncheck the **Action** column in the center of each row. Alternatively, right-click a row in the Schema pane, and select **Exclude**. The row is immediately grayed out. When schema compare is used to update the target database, this row isn't considered for any pending changes.

   You can also right-click on a group row and select **Exclude All** or **Include All**, which is equivalent to unchecking or checking all differences in that group. When you group results by schema, right-clicking on the group row is a useful way to include or exclude all changes to a specific schema.

   If the row being excluded has any dependent objects (for example, a **Table** row that is referenced by a **View** row), the excluded row is disabled but its checkbox isn't cleared. Once all rows that depend on it are unchecked, the disabled row is unchecked. In addition, if a row is refactored (renamed or moved to another schema), then the checkbox is disabled for that row and any of its dependent child rows.

   If you refresh the comparison, those differences that you chose to skip are ignored.

To update the schema of the target, you have two options. You can update the target directly from the **Schema Compare** window if the target is a database or project, or you can generate an update script if the target is a database or a database file. A generated script appears in the Transact-SQL Editor, from which you can inspect the script and execute it against a database.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

> [!NOTE]  
> Graphical schema comparison is partially available in the SDK-style SQL projects preview in Visual Studio. Schema comparisons are available for connected databases and `.dacpac` files, SQL database projects aren't yet available.

1. On the **Tools** menu in Visual Studio, select **SQL Server**, and then select **New Schema Comparison**.

   Alternatively, right-click the **TradeDev** project in **Solution Explorer**, and select **Schema Compare**.

   The **Schema Compare** window opens, and Visual Studio automatically assigns it a name such as `SqlSchemaCompare1`.

   Two dropdown lists with a green arrow in between them appear just below the **Schema Compare** window toolbar. These menus allow you to select database definitions for your comparison source and target.

2. In the **Select Source** dropdown list, choose **Select Source** and the **Select Source Schema** dialog opens.

   If you open the **Schema Compare** window by right-clicking the project name, the source schema is already populated and you can proceed to step 4.

   :::image type="content" source="media/schema-comparison/vs-schema-compare-source.png" alt-text="Screenshot of Schema comparison source select dialog in Visual Studio.":::

3. Complete the selections for a schema comparison source by choosing a **Project**, **Database** connection, or **.dacpac** file. The source is the database definition that you want to use as the basis for changes to the target.

4. From the **Select Target** dropdown list in the **Schema Compare Window**, choose **Select Target**. The **Select Target Schema** dialog opens. Complete the selections for a schema comparison target by choosing a **Project**, **Database** connection, or **.dacpac** file. The target is the database definition that you want to evaluate and potentially apply changes to.

5. Select **Options** from the **Schema Compare Window** toolbar to specify which objects are compared, what types of differences are ignored, and other settings.

6. Select the **Compare** button in the **Schema Compare Window** toolbar to start the comparison process.

   When the comparison is complete, the structural differences between the project and the database appear in the **Results** pane in the upper part of the window. By default, the comparison results are grouped by action (such as Delete, Change, or Add). The **Results** pane displays a row for each database object that differs between the database definitions. Each row identifies the object in the source or target schema (or both) and the action that would be taken on the target schema to make the target object the same as the source object. If an object was refactored and either renamed or moved to a new schema, the source and target names are different, and the source name appears in bold font to highlight the difference.

   :::image type="content" source="media/schema-comparison/ssdt-schema-compare.png" alt-text="Screenshot of Schema comparison interface in Visual Studio comparing a database against a project." lightbox="media/schema-comparison/ssdt-schema-compare.png":::

   By default, the results list hides objects that are the same in both schemas or that aren't supported for update, such as built-in objects. Select the appropriate filter options in the toolbar to show these objects.

   To change the grouping preference, select the **Group Results** dropdown list in the toolbar. Select **Type** to group the results by object type (for example, by tables, views, or stored procedures).

7. By default all differences are included in the scope of the Update Target action. You can exclude differences that you don't want to synchronize. To do so, uncheck the **Action** column in the center of each row. Alternatively, right-click a row in the Schema pane, and select **Exclude**. The row is immediately grayed out. When schema compare is used to update the target database, this row isn't considered for any pending changes.

   You can also right-click on a group row and select **Exclude All** or **Include All**, which is equivalent to unchecking or checking all differences in that group. When you group results by schema, right-clicking on the group row is a useful way to include or exclude all changes to a specific schema.

   If the row being excluded has any dependent objects (for example, a **Table** row that is referenced by a **View** row), the excluded row is disabled but its checkbox isn't cleared. Once all rows that depend on it are unchecked, the disabled row is unchecked. In addition, if a row is refactored (renamed or moved to another schema), then the checkbox is disabled for that row and any of its dependent child rows.

   If you refresh the comparison, those differences that you have chosen to skip are ignored.

To update the schema of the target, you have two options. You can update the target directly from the **Schema Compare** window if the target is a database or project, or you can generate an update script if the target is a database or a database file. A generated script appears in the Transact-SQL Editor, from which you can inspect the script and execute it against a database.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

More in-depth information about schema comparison in Visual Studio Code is available in the article [Schema Compare](../../visual-studio-code-extensions/mssql/mssql-schema-compare.md)

1. In Visual Studio Code in the command palette (`ctrl/cmd+shift+P`), search for and select **MSSQL: Schema Compare**.

   Alternatively, right-click a database project in the **Database Projects** view or a database in **Object Explorer**, and select **Schema Compare**.

   :::image type="content" source="media/schema-comparison/vs-code-schema-compare-launch.png" alt-text="Screenshot of Schema comparison menu item in Visual Studio Code object explorer.":::

   The **Schema Compare** window opens, and a source or target might be preset based on the launch point.

   Two ellipsis buttons with an arrow in between them appear just below the **Schema Compare** window toolbar. These menus allow you to select database definitions for your comparison source and target.

2. Selecting the ellipsis button for the source or target opens a dialog where each can be updated. Complete the selections for a schema comparison source by choosing a **Project**, **Database** connection, or **.dacpac** file. The source is the database definition that you want to use as the basis for changes to the target. The target is the database definition that you want to evaluate and potentially apply changes to.

   :::image type="content" source="media/schema-comparison/vs-code-schema-compare-source.png" alt-text="Screenshot of Schema comparison source select dialog in Visual Studio Code.":::

   Once the selections are completed, select **OK** to close the dialog and return to the **Schema Compare** window.

3. You can also select the **Options** button in the **Schema Compare Window** toolbar to specify which objects are compared, what types of differences are ignored, and other settings.

4. Select the **Compare** button in the **Schema Compare Window** toolbar to start the comparison process.

   When the comparison is complete, the structural differences between the project and the database appear in the **Results** pane in the upper part of the window. By default, the comparison results are grouped by action (such as Delete, Change, or Add). The **Results** pane displays a row for each database object that differs between the database definitions. Each row identifies the object in the source or target schema (or both) and the action that would be taken on the target schema to make the target object the same as the source object. If an object was refactored and either renamed or moved to a new schema, the source and target names are different, and the source name appears in bold font to highlight the difference.

   :::image type="content" source="media/schema-comparison/vs-code-schema-compare.png" alt-text="Screenshot of Schema comparison interface in Visual Studio Code comparing a database against a project." lightbox="media/schema-comparison/vs-code-schema-compare.png":::

5. By default all differences are included in the scope of the Update Target action. You can exclude differences that you don't want to synchronize. To do so, uncheck the **Action** column in the center of each row. When schema compare is used to update the target database, this row isn't considered for any pending changes.

   If the row being excluded has any dependent objects (for example, a **Table** row that is referenced by a **View** row), the excluded row is disabled but its checkbox isn't cleared. Once all rows that depend on it are unchecked, the disabled row is unchecked. In addition, if a row is refactored (renamed or moved to another schema), then the checkbox is disabled for that row and any of its dependent child rows.

   If you refresh the comparison, those differences that you have chosen to skip are ignored.

To update the schema of the target, you have two options. You can update the target directly from the **Schema Compare** window with the **Apply** button if the target is a database or project, or you can generate an update script if the target is a database with the **Generate script** button. A generated script appears in the Transact-SQL Editor, from which you can inspect the script and execute it against a database.

::: zone-end

:::zone pivot="sq1-sql-server-management-studio"

In SQL Server Management Studio, you can use the **Schema Compare** tool to compare databases, projects, or .dacpac files.

1. In SQL Server Management Studio, connect to the database you want to compare.

2. In the **Object Explorer**, right-click the database and select **Tasks** > **Schema Compare (preview)**.

3. In the **Schema Compare** window, complete the selections for a schema comparison target by choosing a **Project**, **Database** connection, or **.dacpac** file.

4. Select **Compare** to start the comparison.

   When the comparison is complete, the structural differences between the project and the database appear in the **Results** pane in the upper part of the window. By default, the comparison results are grouped by action (such as Delete, Change, or Add). The **Results** pane displays a row for each database object that differs between the database definitions. Each row identifies the object in the source or target schema (or both) and the action that would be taken on the target schema to make the target object the same as the source object. If an object was refactored and either renamed or moved to a new schema, the source and target names are different, and the source name appears in bold font to highlight the difference.

5. By default all differences are included in the scope of the Update Target action. You can exclude differences that you don't want to synchronize. To do so, uncheck the **Include** column of each row. When schema compare is used to update the target database, this row isn't considered for any pending changes.

   If the row being excluded has any dependent objects (for example, a **Table** row that is referenced by a **View** row), the excluded row is disabled but its checkbox isn't cleared. Once all rows that depend on it are unchecked, the disabled row is unchecked.

   If you refresh the comparison, those differences that you have chosen to skip are ignored.

To update the schema of the target, you have two options. You can update the target directly from the **Schema Compare** window with the **Apply** button if the target is a database or project, or you can generate an update script if the target is a database with the **Generate script** button. A generated script appears in the Transact-SQL Editor, from which you can inspect the script and execute it against a database.

:::zone-end

::: zone pivot="sq1-command-line"

[!INCLUDE [schema-compare-where-found](../includes/schema-compare-where-found.md)]

::: zone-end

## Related content

- [Compare a database and a project](../howto/compare-database-project.md)
- [Tutorial: start from an existing database](../tutorials/start-from-existing-database.md)
- [`SqlServer.Dac.Compare` namespace](/dotnet/api/microsoft.sqlserver.dac.compare)
