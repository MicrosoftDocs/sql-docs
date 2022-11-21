---
title: Create Azure Functions with the SQL bindings extension through command palette for Visual Studio Code
description: Use the Visual Studio Code command palette to create Azure functions with SQL bindings.
ms.topic: conceptual
ms.service: sql
ms.subservice: tools-other
author: VasuBhog
ms.author: vabhog
ms.reviewer: drskwier
ms.date: 8/24/2022
---

# Create Azure Functions with the SQL bindings extension through the Object Explorer for Visual Studio Code

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

## Overview 
Microsoft SQL Bindings for VS Code enable users to develop Azure Functions with Azure SQL bindings, see further documentation [here](create-azure-function-with-mssql.md). [Install the VS Code extension here](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-bindings-vscode).

## From the Command Palette 
Run the `MS SQL: Create Azure Function with SQL Binding` command from the command palette to create a new function with a SQL binding. 

:::image type="content" alt-text="Screenshot of a VS Code command palette command `MS SQL: Create Azure Function with SQL Binding (preview)." source="./media/create-azure-function-with-mssql/create-azure-function-using-command-palette.png":::

The extension will then prompt you to select the object type to insert (`Input binding`) or upsert into (`Output binding`), either a `Table` or `View`.

:::image type="content" alt-text="Screenshot of a prompt to select the object type." source="./media/create-azure-function-with-mssql/select-object-type.png":::

Then the extension will prompt you to select a connection profile to use for the Azure Function or create a connection profile.

:::image type="content" alt-text="Screenshot of a prompt for connection profile." source="./media/create-azure-function-with-mssql/prompt-for-connection-profile.png":::

Once you either select a connection profile or create a new connection profile, the extension will prompt you to select the database from the selected connection to use for the Azure Function.

:::image type="content" alt-text="Screenshot of a prompt for database" source="./media/create-azure-function-with-mssql/select-database.png":::

Once you select a database, the extension will prompt you to select a table, or view to use or to enter a table or view to query or upsert into. This prompt is based on the object type you selected earlier.

> [!NOTE]
> Azure Function with SQL Binding from a `View` is only supported for `Input` bindings.

Prompt for Table:
:::image type="content" alt-text="Screenshot of a prompt for table." source="./media/create-azure-function-with-mssql/select-table.png":::

Prompt for View:
:::image type="content" alt-text="Screenshot of a prompt for view." source="./media/create-azure-function-with-mssql/select-view.png":::

The extension will then prompt you to enter the function name to be used for the Azure Function.

:::image type="content" alt-text="Screenshot of a prompt to enter function name." source="./media/create-azure-function-with-mssql/function-name-prompt.png":::

If you already have connection strings stored in the local.settings.json, then the extension will prompt you to select the connection string to use for the Azure Function or create a new connection string.

:::image type="content" alt-text="Screenshot of a prompt to select connection string setting." source="./media/create-azure-function-with-mssql/create-new-sql-connection-string-setting.png":::

If you select `Create new local app setting`, then the extension will prompt you to enter the connection string name and value.

:::image type="content" alt-text="Screenshot of a prompt to enter connection string." source="./media/create-azure-function-with-mssql/enter-connection-string-setting-name.png":::

If you're creating the `Azure Function with SQL Binding` to an existing Azure Function project, then the extension will prompt you whether you would like to include the password for the connection string in the local.settings.json file.

:::image type="content" alt-text="Screenshot of a prompt to save the password to the SQL connection string." source="./media/create-azure-function-with-mssql/password-prompt.png":::

If `Yes`, then the password will be saved to the local.settings.json file. If `No` then the extension will warn you that the password won't be saved to the local.settings.json file (shown below), and you'll need to manually add the password later to the local.settings.json file.

:::image type="content" alt-text="Screenshot of a warning to add password to SQL connection string later manually." source="./media/create-azure-function-with-mssql/do-not-save-password.png":::

The extension will then prompt you to provide the namespace for the Azure Function. 
:::image type="content" alt-text="Screenshot of a prompt for namespace for the Azure Function." source="./media/create-azure-function-with-mssql/namespace-for-function.png":::

If you're creating a brand new Azure Function project with SQL binding, then the extension will prompt whether you would like to include the password for the connection string in the local.settings.json file.

A progress notification will appear to indicate that the Azure Function has completed.

:::image type="content" alt-text="Screenshot of a information message indicating finished creating Azure Function Project." source="./media/create-azure-function-with-mssql/finished-creating-project.png":::

Once the Azure Function is created, the extension will generate the code either for an `Input` or `Output` binding shown [here](create-azure-function-with-mssql.md#generated-code-for-azure-functions-with-sql-bindings).



#### In an existing Azure Function
Open the C# Azure Function in an editor and then run the `MS SQL: Add SQL Binding` command from the command palette to add a SQL binding to an existing function.

:::image type="content" alt-text="Screenshot of a VS Code command palette command `MS SQL: Add SQL Binding (preview)." source="./media/create-azure-function-with-mssql/add-sql-binding-command-palette.png":::

The extension will then prompt you to select Azure function in the current file to add the SQL binding to.
:::image type="content" alt-text="Screenshot of Azure Functions found in project." source="./media/create-azure-function-with-mssql/select-azure-functions-in-file.png":::

If you're creating an Azure Function with SQL binding from a table, the extension will prompt you to select the binding type to use, either an `Input` (Retrieves data from a database) or `Output` (Save data to a database) binding.

If you already have connection strings stored in the local.settings.json, then the extension will prompt you to select the connection string to use for the Azure Function or create a new connection string.

:::image type="content" alt-text="Screenshot of a prompt to select or create a new connection string setting." source="./media/create-azure-function-with-mssql/create-new-sql-connection-string-setting.png":::

If you select `Create new local app setting`, then the extension will prompt you to enter the connection string name and value.

:::image type="content" alt-text="Screenshot of a prompt to enter connection string." source="./media/create-azure-function-with-mssql/enter-connection-string-setting-name.png":::

The extension will then prompt you to select a connection string method to select a connection profile or enter a connection string to use for the SQL binding.

:::image type="content" alt-text="Screenshot of a prompt to select connection string setting method." source="./media/create-azure-function-with-mssql/select-connection-string-method.png":::

If you decide to select a connection profile, the extension will prompt you to select the database from the selected connection to use for the Azure Function.

:::image type="content" alt-text="Screenshot of a prompt for database." source="./media/create-azure-function-with-mssql/select-database.png":::

Once you select a database, the extension will prompt you to select a table to use, or to enter a table or view to query or upsert into.

Prompt for Table:
:::image type="content" alt-text="Screenshot of a prompt for table." source="./media/create-azure-function-with-mssql/select-table.png":::

The extension will then prompt you whether you would like to include the password for the connection string in the local.settings.json file.

:::image type="content" alt-text="Screenshot of a prompt to save the password to the SQL connection string." source="./media/create-azure-function-with-mssql/password-prompt.png":::

If `Yes`, then the password will be saved to the local.settings.json file. If `No` then the extension will warn you that the password won't be saved to the local.settings.json file (shown below), and you'll need to manually add the password later to the local.settings.json file.

:::image type="content" alt-text="Screenshot of a warning to add password to SQL connection string later manually." source="./media/create-azure-function-with-mssql/do-not-save-password.png":::

Once the Azure Function is created, the extension will generate the code either for an `Input` or `Output` binding shown [here](create-azure-function-with-mssql.md#generated-code-for-azure-functions-with-sql-bindings).

## Next steps

- [Create Azure Function with SQL binding overview](create-azure-function-with-mssql.md).
- [Learn more about SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql).
- [Create Azure Function with SQL binding through the object explorer tutorial](create-azure-function-with-mssql-object-explorer.md).
- [Use the mssql extension to query a SQL instance](mssql-extensions.md).