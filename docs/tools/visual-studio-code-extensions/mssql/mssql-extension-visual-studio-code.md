---
title: Overview
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn about the MSSQL extension for Visual Studio Code, which provides tools for connecting to databases, managing schemas, and executing queries.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: yoleichen
ms.date: 08/17/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ms.custom:
  - sfi-image-nochange
  - ignite-2025
ai-usage: ai-assisted
---

# MSSQL extension for Visual Studio Code

The [MSSQL extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) supports developers building applications that use Azure SQL (including Azure SQL Database, Azure SQL Managed Instance, and SQL Server on Azure Virtual Machines), SQL database in Microsoft Fabric, and SQL Server. It provides tools for connecting to databases, managing and designing schemas, exploring database objects, executing Transact-SQL (T-SQL) queries, and viewing query execution plans within Visual Studio Code.

The extension includes IntelliSense, T-SQL script execution, and customizable options for local and cloud-based databases.

<a id="install-the-mssql-extension-in-visual-studio-code"></a>

## Install the MSSQL extension for Visual Studio Code

To get started with SQL development in Visual Studio Code, install the **MSSQL extension**:

1. Open **Visual Studio Code**.
1. Select the **Extensions** icon in the Activity Bar (**Cmd**+**Shift**+**X** on macOS, or **Ctrl**+**Shift**+**X** on Windows and Linux).
1. In the **search bar**, type `mssql`.
1. Find **SQL Server (mssql)** in the results and select it.
1. Select the **Install** button.

:::image type="content" source="media/mssql-extension-visual-studio-code/mssql-extension-vscode.png" alt-text="Screenshot of the MSSQL extension for Visual Studio Code." lightbox="media/mssql-extension-visual-studio-code/mssql-extension-vscode.png":::

> [!TIP]  
> You know the extension is installed correctly when the **MSSQL** icon appears in the Activity Bar and the **Connections** view becomes available.

## Features

The MSSQL extension for Visual Studio Code supports SQL Server, Azure SQL, and SQL database in Fabric.

The following table provides an overview of the features available in the MSSQL extension, their release status, and links to detailed documentation.

| Feature | Status | Description |
| --- | --- | --- |
| [Connection dialog](#connection-dialog) | GA | Connect using parameters, connection strings, or Azure browse |
| [Object Explorer](#object-explorer-filtering) | GA | Browse and filter database objects with type-aware search |
| [Query results pane](#query-results-pane) | GA | View, sort, copy, and export query results |
| [Query plan visualizer](#query-plan-visualizer) | GA | Analyze execution plans with interactive node navigation |
| [Table designer](#table-designer) | GA | Create and manage tables with a visual interface |
| [Schema designer](mssql-schema-designer.md) | GA | Visual schema modeling with drag-and-drop and auto-layout |
| [Schema compare](mssql-schema-compare.md) | GA | Compare and synchronize schemas between databases or DACPACs |
| [GitHub Copilot integration](../github-copilot/overview.md) | GA | AI-assisted SQL development with natural language chat and agent mode |
| [Local SQL Server containers](mssql-local-container.md) | GA | Create and manage SQL Server containers locally |
| [View and edit data](#view-and-edit-data) | GA | Browse and modify table data inline without writing DML |
| [Data-tier Application (DACPAC and BACPAC)](mssql-data-tier-application.md) | GA | Deploy, extract, import, and export DACPAC and BACPAC files |
| [Fabric integration](mssql-fabric-integration.md) | GA | Browse Fabric workspaces and provision SQL databases |
| [Database management](mssql-database-operations.md) | GA | Create, rename, and drop databases from Object Explorer |
| [Backup and restore](mssql-database-operations.md#backup-database) | GA | Back up databases to disk or URL and restore from backups |
| [Database object search](mssql-database-operations.md#database-object-search) | GA | Find tables, views, functions, and stored procedures with type-aware search |
| [Import flat file](mssql-database-operations.md#import-flat-file) | GA | Import `.csv` and `.txt` files into new SQL Server tables |
| [Query profiler](mssql-query-profiler.md) | GA | Real-time database activity monitoring with Extended Events |
| [Schema designer with GitHub Copilot](mssql-schema-designer-copilot.md) | GA | Natural language schema design with visual change tracking and ORM script generation |
| [Data API builder](mssql-data-api-builder.md) | GA | Create REST, GraphQL, and MCP endpoints for SQL databases |
| [SQL notebooks](mssql-sql-notebooks.md) | GA | Jupyter-based SQL notebooks with rich results and multi-kernel support |
| [SQL Formatter](mssql-sql-formatter.md) | Preview | Format T-SQL on demand or on save with configurable formatting options |
| [Azure integration](mssql-azure-integration.md) | GA | Provision Azure SQL databases directly from Visual Studio Code, starting with the free tier |
| [Shortcuts configuration](#shortcuts-configuration) | GA | Manage keyboard shortcuts for frequently used queries and other editor commands |

### Connection dialog

The Connection dialog provides an intuitive interface for connecting to databases hosted in Azure SQL (including Azure SQL Database, Azure SQL Managed Instance, and SQL Server on Azure VMs), SQL database in Fabric, or SQL Server. It offers multiple input options to cater to different scenarios:

- **Parameters**: Enter individual connection details such as server name, database name, username, and password.

- **Connection String**: Directly input a complete connection string for more advanced configurations.

- **Browse Azure**: Browse available database instances and databases in your Azure account, with options to filter by subscription, resource group, and location.

- **Connection Groups**: Organize environments by grouping connections into folders and assigning colors for quick visual identification. Easily assign or change a group when creating or editing a connection.

The connection dialog includes **Saved Connections** and **Recent Connections** panels to simplify reconnecting to previously used servers. The layout supports editing and saving connection details and makes it easy to switch between servers or databases.

:::image type="content" source="media/mssql-extension-visual-studio-code/mssql-connection-dialog-parameters.png" alt-text="Screenshot of the connection dialog feature." lightbox="media/mssql-extension-visual-studio-code/mssql-connection-dialog-parameters.png":::

### Database operations

The MSSQL extension provides built-in tools for common [database operations](mssql-database-operations.md), including:

- **Database management**: Create, rename, and drop databases directly from the **Object Explorer**.
- **Database object search**: Find tables, views, functions, and stored procedures with type-aware search and contextual actions.
- **Backup and restore**: Back up databases to disk or Azure Blob Storage, and restore from existing backups, backup files, or Azure Blob Storage.
- **Import flat file**: Import `.csv` and `.txt` files into new SQL Server tables with a guided wizard.

### Object Explorer (filtering)

The Object Explorer lets you explore your database objects, such as databases, tables, views, and programmability items. Filtering helps you find specific objects within large database hierarchies:

- **Apply Filters**: Filter database objects by properties like name, owner, or creation date. You can apply filters at multiple levels, including databases, tables, views, and programmability.

- **Edit Filters**: Refine or update existing filters to further narrow the object list.

- **Clear Filters**: Remove applied filters to view all objects within the hierarchy.

:::image type="content" source="media/mssql-extension-visual-studio-code/object-explorer-filtering.png" alt-text="Screenshot of the object explorer filter feature." lightbox="media/mssql-extension-visual-studio-code/object-explorer-filtering.png":::

### Table designer

To use this feature, right-click a table in Object Explorer and select **Modify Table Structure**.

The Table Designer provides a visual interface for creating and managing tables:

- **Columns**: Add new columns, set data types, define nullability, and specify default values. You can also designate a column as a primary key or identity column directly within the interface.

- **Primary Key**: Define one or more columns as the primary key for your table, ensuring each row is uniquely identifiable.

- **Indexes**: Create and manage indexes to improve query performance by adding extra columns as indexes for faster data retrieval.

- **Foreign Keys**: Define relationships between tables by adding foreign keys referencing primary keys in other tables, ensuring data integrity across tables.

- **Check Constraints**: Set up rules to enforce specific conditions on the data being entered, such as value ranges or patterns.

- **Advanced Options**: Configure more sophisticated properties and behaviors, such as system versioning and memory-optimized tables.

Within the designer, the **Script As Create** panel provides an automatically generated T-SQL script that reflects your table design. You have the following options:

- **Publish**: Apply your changes directly to the database by selecting **Publish**. This action uses DacFX (Data-tier Application Framework) to deploy your schema updates.

- **Copy script**: Copy the generated T-SQL script from the preview panel for manual execution or open it directly in the editor for further adjustments and modifications as needed.

:::image type="content" source="media/mssql-extension-visual-studio-code/table-designer.png" alt-text="Screenshot of the table designer feature." lightbox="media/mssql-extension-visual-studio-code/table-designer.png":::

### View and edit data

To use this feature, double-click a table in Object Explorer or right-click a table to open the context menu and select **Edit Table Data**.

The table data opens in a data grid within a new editor tab. You see the contents in a familiar, spreadsheet-like layout with paging controls based on the configured rows per page.

Key capabilities include:

- **Inline editing**: Update cell values directly within the grid. Your edits are validated in real time and return an error message for incorrect inputs, such as invalid data types or violations of a constraint. The grid highlights the cell with the incorrect input in red.

- **Add and delete rows**: Insert new rows or delete existing ones, so you can quickly adjust data during development and testing.

- **Pagination**: Navigate large datasets using built-in paging controls.

- **Save Changes**: All edits remain in a pending state until you select **Save Changes**, so you have complete control over when updates are committed to the database.

- **Show Script**: This pane displays a read-only DML script that reflects all actions performed in the data grid in real time. Use the script to review the underlying DML operations before saving changes.

:::image type="content" source="media/mssql-extension-visual-studio-code/edit-data.png" alt-text="Screenshot of the Edit Data screen." lightbox="media/mssql-extension-visual-studio-code/edit-data.png":::

### Query results pane

Query results display within the bottom panel of Visual Studio Code, alongside the integrated terminal, output, debug console, and other tools.

> [!TIP]  
> You can open query results in a new tab for an expanded view, similar to the previous experience.

Key features of the Query Results pane include:

- **Grid View**: Displays query results in a familiar grid format, so you can easily inspect the data. You can display results in a new tab for a clearer, more organized view.

- **Copy Options**: Right-click within the results grid to access options like *Select All, Copy, Copy with Headers, and Copy Headers*, making it convenient to transfer data for other uses.

- **Save Query Results**: Includes the ability to save query results to multiple formats such as JSON, Excel, and CSV, so you can work with the data outside of Visual Studio Code.

- **Inline Sorting**: You can sort the data by selecting the column headers directly in the query results view. Sorting can be done in ascending or descending order to make it easier to analyze specific subsets of the data.

- **Estimated Plan**: The **Estimated Plan** button appears as a flowchart icon, located in the query toolbar next to the **Run Query** button. It generates an estimated execution plan without executing the query, so you can review how the query optimizer processes the query.

- **Enable Actual Plan**: The **Enable Actual Plan** button, located after the **Estimated Plan** button, shows the actual query plan for executed queries. Use this option to identify bottlenecks and inefficiencies.

:::image type="content" source="media/mssql-extension-visual-studio-code/query-results-vscode.png" alt-text="Screenshot of the query results feature." lightbox="media/mssql-extension-visual-studio-code/query-results-vscode.png":::

> [!TIP]  
> You can customize the query results behavior using the `mssql.openQueryResultsInTabByDefault` setting. When set to `true`, query results open in a new tab by default, helping declutter your workspace.

The new Results Grid experience is available in preview and includes improvements in state management, performance, and column customization, such as freezing, showing, or hiding columns. It's enabled by default. To change this setting, modify `mssql.preview.betaResultsGrid` in Visual Studio Code settings.

### Query Plan Visualizer

The Query Plan Visualizer displays execution plans for SQL queries. It shows how the query optimizer processes each operation, so you can identify bottlenecks and optimize your queries.

Key features and capabilities include:

- **Node Navigation**: Each step in the execution plan appears as a node. You can interact with the plan in different ways. Select nodes to view tooltips or detailed information about specific operations. Collapse or expand node trees to simplify the view and focus on key areas of the query plan.
- **Zoom Controls**: The visualizer offers flexible zoom options to help you analyze the plan in detail. You can zoom in or out to adjust the level of detail. Use the *zoom to fit* feature to resize the view and fit the entire plan on your screen. Set custom zoom levels to examine specific elements precisely.
- **Metrics and Highlighting**: The metrics toolbar helps you analyze key performance indicators and highlight expensive operations. Select metrics such as **Actual Elapsed Time**, **Cost**, **Subtree Cost**, or **Number of Rows Read** from the dropdown list to identify bottlenecks. Use these metrics to search for specific nodes within the query plan for deeper analysis.

The right-hand sidebar provides quick access to more actions:

- **Save Plan**: Save the current execution plan for future reference.
- **Open XML**: Open the XML representation of the query plan to inspect details at the code level.
- **Open Query**: View the query that generated the execution plan directly from the toolbar.
- **Toggle Tooltips**: Enable or disable tooltips for more details on each node.
- **Properties**: View the properties of each node in the execution plan, with options to sort by importance or alphabetically.

:::image type="content" source="media/mssql-extension-visual-studio-code/query-plan-visualizer-vscode.png" alt-text="Screenshot of the query plan visualizer feature." lightbox="media/mssql-extension-visual-studio-code/query-plan-visualizer-vscode.png":::

### Shortcuts Configuration

Use Shortcuts Configuration to manage and run common commands.

To use the feature, select **Open Shortcuts Configuration** from the MSSQL extension toolbar.

:::image type="content" source="media/mssql-extension-visual-studio-code/shortcuts-configuration-entry.png" alt-text="Screenshot of the MSSQL extension toolbar showing the Open Shortcuts Configuration button." lightbox="media/mssql-extension-visual-studio-code/shortcuts-configuration-entry.png":::

It includes two tabs:

- **Quick Queries**: Save and run SQL snippets with keyboard shortcuts.
- **Extension Shortcuts**: Configure MSSQL editor and result-view shortcuts.

In the Quick Queries tab, you have the following options:

- **Customizable SQL queries**: Configure and save multiple SQL query entries.
- **Auto-execute control**: Choose whether a shortcut runs the query immediately or opens it in the editor first.
- **Keybinding integration**: Customize keybindings with the Visual Studio Code Keyboard Shortcuts editor.

:::image type="content" source="media/mssql-extension-visual-studio-code/shortcuts-configuration-queries.png" alt-text="Screenshot of the Shortcuts Configuration page showing the Quick Queries tab with query slots, auto-execute options, and keybinding controls." lightbox="media/mssql-extension-visual-studio-code/shortcuts-configuration-queries.png":::

For detailed guidance on **Extension Shortcuts** and advanced shortcut configuration, see [Customize keyboard shortcuts](mssql-keyboard-shortcuts.md).

## Supported operating systems

Currently, this extension supports the following operating systems:

- Windows 10 and 11 (x64, Arm64)
- macOS (Intel and Apple Silicon)
- Linux (x64, Arm64) - including Ubuntu, Debian, RHEL, Fedora, and other major distributions

## Offline installation

The extension can download and install a required `SqlToolsService` package during activation. You can still use the extension on machines with no Internet access by choosing the **Install from VSIX...** option in the Extension view and installing a bundled release from the [Releases page](https://github.com/microsoft/vscode-mssql/releases). Each operating system has a `.vsix` file with the required service included. Pick the file for your OS, download, and install it to get started. Choose a full release and ignore any alpha or beta releases, as these versions are daily builds used in testing.

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [Customize keyboard shortcuts](mssql-keyboard-shortcuts.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Database operations](mssql-database-operations.md)
- [Schema Designer](mssql-schema-designer.md)
- [GitHub Copilot integration in Schema Designer](mssql-schema-designer-copilot.md)
- [Data API builder](mssql-data-api-builder.md)
- [SQL Notebooks](mssql-sql-notebooks.md)
- [Create an Azure SQL Database](mssql-azure-integration.md)
- [Schema Compare](mssql-schema-compare.md)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
