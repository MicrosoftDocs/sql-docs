---
title: Create Azure Functions with the SQL bindings extension through the Object Explorer for Visual Studio Code 
description: Use the mssql object explorer to create Azure functions with SQL bindings in Visual Studio Code.
ms.topic: conceptual
ms.service: sql
ms.subservice: tools-other
author: VasuBhog
ms.author: vabhog
ms.reviewer: drskwier
ms.date: 8/24/2022
---

# Create Azure Functions with the SQL bindings extension through the Object Explorer

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

## Overview 
Microsoft SQL Bindings for VS Code enable users to develop Azure Functions with Azure SQL bindings, see further documentation [here](create-azure-function-with-mssql.md). [Install the VS Code extension here](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-bindings-vscode).

## From the Object Explorer 
To create an Azure Function from a specific `Table` or `View` in object explorer (OE), right-click on a table or view from a connected server in SQL Server object explorer and select `Create Azure Function with SQL Binding.` 

**Table OE Command**:
:::image type="content" alt-text="Screenshot of object explorer context menu to add a SQL binding from Table." source="./media/create-azure-function-with-mssql/create-function-table-object-explorer.png":::

**View OE Command**:
:::image type="content" alt-text="Screenshot of object explorer context menu to add a SQL binding from View." source="./media/create-azure-function-with-mssql/create-function-view-object-explorer.png":::

If you haven't yet created the Azure Function project, a VS Code prompt will appear to aid in creating a new Azure Function project.

:::image type="content" alt-text="Screenshot of VS Code notification to create a new Azure Function project since none were found in folder." source="./media/create-azure-function-with-mssql/create-azure-function-project.png":::

The extension will then ask you to select the folder where you want to create the Azure Function.

:::image type="content" alt-text="Screenshot of a prompt to choose folder to create Azure Function with SQL binding to." source="./media/create-azure-function-with-mssql/select-folder-to-use-for-function.png":::

If you're creating an Azure Function with SQL binding from a table, the extension will prompt you to select the binding type to use, either an `Input` (Retrieves data from a database) or `Output` (Save data to a database) binding.

> [!NOTE]
> Azure Function with SQL Binding from a `View` is only supported for `Input` bindings.

:::image type="content" alt-text="Screenshot of a prompt to select binding type." source="./media/create-azure-function-with-mssql/binding-type-prompt.png":::

The extension will then prompt you to enter the function name to be used for the Azure Function.

:::image type="content" alt-text="Screenshot of a prompt to enter function name." source="./media/create-azure-function-with-mssql/function-name-prompt.png":::

If you already have connection strings stored in the local.settings.json, then the extension will prompt you to select the connection string to use for the Azure Function or create a new connection string.

:::image type="content" alt-text="Screenshot of a prompt to select connection string setting." source="./media/create-azure-function-with-mssql/create-new-sql-connection-string-setting.png":::

If you select `Create new local app setting`, then the extension will prompt you to enter the connection string name and value.

:::image type="content" alt-text="Screenshot of a pPrompt to enter connection string." source="./media/create-azure-function-with-mssql/enter-connection-string-setting-name.png":::

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

## Next steps

- [Create Azure Function with SQL binding overview](create-azure-function-with-mssql.md).
- [Learn more about SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql).
- [Create Azure Function with SQL binding through the command palette tutorial](create-azure-function-with-mssql-command-palette.md).
- [Use the mssql extension to query a SQL instance](mssql-extensions.md).