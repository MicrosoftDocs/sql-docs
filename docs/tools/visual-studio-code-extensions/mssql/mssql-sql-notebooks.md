---
title: SQL Notebooks
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn how to use SQL Notebooks in the MSSQL extension for Visual Studio Code to write and run SQL queries in Jupyter notebook format.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: tsiddique, roblescarlos
ms.date: 06/01/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# SQL Notebooks

SQL Notebooks in the MSSQL extension for Visual Studio Code provide notebook-based SQL development using native Visual Studio Code Jupyter notebooks. You can combine interactive SQL query execution with Markdown documentation cells to build runnable query collections, document database operations, and share reproducible analysis.

## Features

SQL Notebooks offers these capabilities:

- Execute Transact-SQL (T-SQL) queries interactively in notebook code cells with inline results displayed below each cell.

- Use the native Visual Studio Code Jupyter notebook format (`.ipynb` files) for full compatibility with existing notebook tooling.

- View query results in a rich data grid with sorting, filtering, cell selection, null value highlighting, and copy options (including copy with headers).

- Write SQL with IntelliSense, including table and column name suggestions based on your active database connection.

- Add Markdown text cells alongside SQL code cells to document queries, annotate results, and create narrative workflows.

- Connect each notebook to a SQL Server instance and switch between databases on the same server.

- Run cells individually or execute all cells sequentially.

- Use GitHub Copilot for inline query suggestions in code cells and chat-driven notebook authoring to generate complete notebooks from natural language descriptions.

- Combine SQL cells with other language kernels (such as Python) in the same notebook by installing the [Jupyter extension](https://marketplace.visualstudio.com/items?itemName=ms-toolsai.jupyter).

- Export and share notebooks as `.ipynb` files that others can open in Visual Studio Code or any Jupyter-compatible environment.

## Prerequisites

Before you use SQL Notebooks, ensure the following requirements are met:

- The MSSQL extension for Visual Studio Code is installed. For installation steps, see the [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md) overview.

- An active database connection is established through the MSSQL extension. For connection steps, see [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md).

- (Optional) The [Jupyter extension](https://marketplace.visualstudio.com/items?itemName=ms-toolsai.jupyter) for Visual Studio Code, if you want to use other kernels such as Python alongside SQL in the same notebook.

## Create a SQL notebook

You can create a new SQL notebook in several ways:

- Open the **Command Palette** (**Ctrl**+**Shift**+**P** on Windows and Linux, or **Cmd**+**Shift**+**P** on macOS), type `New Notebook`, and select the command.

- Go to the **File** menu and select **New File**, then choose the Jupyter Notebook type.

- Right-click on a database in the MSSQL extension's **Object Explorer** and select **New Notebook** from the context menu.

When you create a new notebook, select the **MSSQL** kernel to enable T-SQL execution in code cells.

## Connect to a database

Each SQL notebook needs an active database connection to run queries. When you open a notebook with the MSSQL kernel, the extension prompts you to select a connection profile or create a new one.

> [!IMPORTANT]  
> Each notebook supports a single server connection per kernel. You can connect to one SQL Server instance and switch between databases on that server, but you can't connect to multiple servers within the same notebook. To work with a different server, create a separate notebook or change the notebook's connection.

## Write and run SQL cells

SQL code cells let you write and execute T-SQL queries interactively within the notebook.

1. Select **+ Code** in the notebook toolbar to add a new code cell.

1. Enter your T-SQL query in the cell. The cell provides the same SQL editing experience as a standard query editor, including IntelliSense with table and column name suggestions from the connected database.

1. Select **Run Cell** (the play button to the left of the cell) to execute the query.

1. Results appear directly below the cell in a rich data grid.

To run all cells in the notebook sequentially, select **Run All** in the notebook toolbar.

> [!TIP]  
> Use **Ctrl**+**Enter** (Windows and Linux) or **Cmd**+**Enter** (macOS) to run the current cell and stay on it. Use **Shift**+**Enter** to run the current cell and advance to the next one.

### Query results

Query results in SQL Notebooks provide the same rich data grid experience available in the standard query editor:

- **Sorting**: Select a column header to sort results ascending or descending.
- **Filtering**: Use the filter icon on column headers to filter result data.
- **Cell selection**: Select individual cells or ranges of cells in the results grid.
- **Null highlighting**: `NULL` values are visually highlighted in the results grid for easy identification.
- **Copy options**: Right-click selected cells to access **Copy**, **Copy with Headers**, and **Select All** options.

## Add Markdown cells

You can use Markdown cells to add formatted text, headings, lists, and links alongside your SQL code cells. Use them to document your queries, explain business logic, or add notes about expected results.

1. Select **+ Markdown** in the notebook toolbar to add a new text cell.

1. Type your Markdown content. A preview is shown as you type.

1. Select outside the cell to render the Markdown text.

Select the cell again to return to edit mode.

## Use multiple kernels

The MSSQL extension provides the SQL kernel out of the box. You can extend your notebooks with more language kernels by installing the [Jupyter extension](https://marketplace.visualstudio.com/items?itemName=ms-toolsai.jupyter), which bundles support for Python and other kernels. You can combine SQL data queries with Python data processing and visualization cells in the same notebook.

## GitHub Copilot integration

GitHub Copilot works with SQL Notebooks to help you write queries and generate notebooks. When you install the GitHub Copilot extension, you get:

- **Inline suggestions**: As you type in SQL code cells, GitHub Copilot suggests query completions based on your database context and surrounding Markdown cells.

- **Chat-driven notebook authoring**: Use GitHub Copilot Chat to generate complete notebooks with alternating Markdown and SQL cells. Describe the analysis you want to perform, and GitHub Copilot creates the full notebook structure.

For example, you can open GitHub Copilot Chat and use a prompt like:

```copilot-prompt
I have a SQL Notebook open connected to AdventureWorks. Create cells for a sales
analysis: list all tables, find top 10 customers by revenue, show revenue by product
category, and demonstrate a safe data modification using BEGIN TRAN / ROLLBACK.
```

GitHub Copilot generates the Markdown documentation cells and T-SQL code cells, which you can then run individually or all at once using **Run All**.

## Switch databases

To switch to a different database within the same server connection:

1. Select the database name displayed in the notebook's connection status area.

1. Choose a different database from the dropdown list.

All subsequent cell executions use the newly selected database.

> [!NOTE]  
> To switch to a different server, you need to change the notebook's connection profile. Consider creating a separate notebook for each server you need to work with.

## Limitations

**Single server connection per notebook**: Currently, each notebook connects to one SQL Server instance. You can switch databases on that server but can't connect to a second server within the same notebook.

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Quickstart: Run your first query with the MSSQL extension for Visual Studio Code](mssql-run-first-query.md)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](../github-copilot/overview.md)
- [Transition from Azure Data Studio](mssql-azure-data-studio-transition.md)
- [MSSQL extension for Visual Studio Code](mssql-extension-visual-studio-code.md)
- [Visual Studio Code Jupyter notebooks documentation](https://code.visualstudio.com/docs/datascience/jupyter-notebooks)
- [Visual Studio Code documentation](https://code.visualstudio.com/docs)
- [MSSQL extension for Visual Studio Code repository on GitHub](https://github.com/Microsoft/vscode-mssql)
