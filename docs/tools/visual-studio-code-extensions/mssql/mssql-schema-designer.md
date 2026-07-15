---
title: Schema Designer
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use the Schema Designer in the MSSQL extension for Visual Studio Code to visualize, design, and manage database schemas.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: yoleichen, roblescarlos
ms.date: 07/10/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# Schema Designer

The Schema Designer in the MSSQL extension for Visual Studio Code provides a graphical environment for visualizing, designing, and managing database schemas without writing Transact-SQL (T-SQL) statements.

:::image type="content" source="media/mssql-schema-designer/schema-designer-overview.png" alt-text="Screenshot of the schema designer overview showing a database schema diagram." lightbox="media/mssql-schema-designer/schema-designer-overview.png":::

## Features

Schema designer offers these features:

- Visualize database structure with interactive diagrams.
- Create or edit tables, foreign keys, primary keys, and constraints.
- Search, drag and drop, filter, zoom, use a mini-map, and autoarrange diagrams for efficient navigation and customization.
- Export schema diagrams for documentation or sharing.
- Automatically generate and view read-only T-SQL scripts representing your schema changes.
- Review and apply changes to the database with the Publish Changes feature.

## Open schema designer

Right-click the database in **Object Explorer** and select **Visualize and Design Schema** from the menu. This action opens the schema designer view, where you can see the visual database diagram.

:::image type="content" source="media/mssql-schema-designer/schema-designer-entry-point.png" alt-text="Screenshot of the entry point to open the schema designer in Visual Studio Code MSSQL extension.":::

Once inside the schema designer, you find a canvas with various navigation capabilities. Here's how to get around:

- **Pan and zoom**: Select and drag anywhere on the canvas to pan across the diagram. Use your mouse scroll wheel or trackpad gestures to zoom in and out for a closer or broader view.

- **Mini-map**: Use the built-in mini-map (located in the bottom right corner of the designer) for quick navigation across large or complex schemas.

  :::image type="content" source="media/mssql-schema-designer/schema-designer-mini-map.png" alt-text="Screenshot of the mini-map feature in the schema designer for quick navigation.":::

- **Drag and drop**: Rearrange tables and relationships by dragging elements on the canvas. This option helps you create a layout that makes sense to you or your team.

- **Search and filter**: Use the search box (**Ctrl**+**F** or **Cmd**+**F**) to locate specific tables or columns. Apply filters to focus on certain parts of the schema or hide irrelevant elements.

- **Auto-arrange**: The diagram is automatically arranged in a readable layout by default. If you manually reposition tables and want to reset the view, select the **Autoarrange** button to reorganize the tables.

## Understand table structure and relationships

When you enter the schema designer view, you see the visualization of your database tables. Each table displays its schema and table name, columns, data types, and primary keys shown as a key icon.

:::image type="content" source="media/mssql-schema-designer/schema-designer-tables.png" alt-text="Screenshot of a table structure showing columns, data types, and primary keys in the schema designer." lightbox="media/mssql-schema-designer/schema-designer-tables.png":::

Foreign key relationships are shown with connecting arrows between columns. For example, in the previous diagram, the `AddressID` column in the `CustomerAddress` table references the `AddressID` column in the `Address` table, visually representing the relationship between them.

## Add or edit tables

Select **Add Table** in the top toolbar to add a new table. Select the pencil icon on a table in the diagram to edit that table.

:::image type="content" source="media/mssql-schema-designer/schema-designer-edit-table.png" alt-text="Screenshot of the table editor panel for adding or modifying table details in the schema designer." lightbox="media/mssql-schema-designer/schema-designer-edit-table.png":::

This action opens the **Table** tab in the **Table Editor** in a side panel. You can:

- Select or change the schema
- Define the table name
- Add new columns with name, data type, default value, and constraints
- Mark one or more columns as primary keys
- Delete or update existing columns as needed

Select **Save** to apply your changes. The diagram updates to reflect your changes. For more advanced table editing capabilities, such as creating indexes or setting up constraint rules, use the [Table designer in the MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md#table-designer).

## Add or edit foreign key relationships

To manage foreign key relationships, select the ellipsis (**...**) on a table in the diagram and select **Manage Relationships**.

:::image type="content" source="media/mssql-schema-designer/schema-designer-manage-relationships.png" alt-text="Screenshot of relationships management entry point in the schema designer.":::

This option opens the **Foreign Keys** tab in the **Table Editor** side panel, where you can:

- Add new foreign key relationships by referencing primary keys in other tables
- Define the foreign key name
- Edit existing foreign keys to update or correct relationships

:::image type="content" source="media/mssql-schema-designer/schema-designer-foreign-key.png" alt-text="Screenshot of the foreign key relationships management panel in the schema designer." lightbox="media/mssql-schema-designer/schema-designer-foreign-key.png":::

Changes automatically appear in the visual diagram, with arrows showing the direction of each relationship.

:::image type="content" source="media/mssql-schema-designer/schema-designer-arrows.png" alt-text="Screenshot of arrows representing foreign key relationships between tables in the schema designer." lightbox="media/mssql-schema-designer/schema-designer-arrows.png":::

Alternatively, you can create a relationship by dragging an arrow from one column to another directly in the diagram. This method defines a one-to-one relationship between the selected columns.

## View schema definition in script pane

From the ribbon toolbar, select the **View Code** button to open the bottom pane. This pane shows the read-only T-SQL script that displays actions performed on the schema designer in real time.

:::image type="content" source="media/mssql-schema-designer/schema-designer-view-code.png" alt-text="Screenshot of the code view pane showing T-SQL scripts generated by the schema designer." lightbox="media/mssql-schema-designer/schema-designer-view-code.png":::

## Review and publish your changes

When you finish editing tables or relationships, select **Publish Changes** in the top toolbar. This action generates a change summary report that lists all pending modifications to your schema.

:::image type="content" source="media/mssql-schema-designer/schema-designer-publish.png" alt-text="Screenshot of the publish changes feature in the schema designer summarizing schema modifications." lightbox="media/mssql-schema-designer/schema-designer-publish.png":::

Review the report carefully. Check the confirmation box to acknowledge and accept any potential risks associated with applying the changes. This process uses DacFX (Data-tier Application Framework) to deploy your schema updates.

## GitHub Copilot integration

The Schema Designer supports GitHub Copilot for AI-assisted schema design. You can use natural language to create schemas, modify existing schemas, review changes through a diff view, and import external artifacts. Changes are reflected in the visual diagram and T-SQL script.

For detailed instructions on using GitHub Copilot within the Schema Designer, see [GitHub Copilot integration in Schema Designer](mssql-schema-designer-copilot.md).

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [GitHub Copilot integration in Schema Designer](mssql-schema-designer-copilot.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Schema Compare](mssql-schema-compare.md)
- [Data-tier Application (DACPAC and BACPAC) import and export](mssql-data-tier-application.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
