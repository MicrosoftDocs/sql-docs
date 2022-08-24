---
title: Create Azure Functions with the SQL bindings extension for Visual Studio Code
description: Use the mssql extension for Visual Studio Code to edit and run Transact-SQL scripts for SQL Server on Linux.
ms.topic: conceptual
ms.prod: sql
ms.technology: tools-other
author: VasuBhog
ms.author: vabhog
ms.reviewer: drskwier
ms.date: 8/24/2022
---

# Create Azure Functions with the SQL bindings extension for Visual Studio Code

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Azure Functions support for [SQL bindings](https://aka.ms/sqlbindings) is available in preview for input and output bindings, making connecting an Azure SQL database or SQL Server database to Azure Functions easier. The SQL bindings extension for Visual Studio Code (VS Code) facilitates the process of developing Azure Functions with SQL bindings and is automatically installed with the [mssql extension for VS Code](https://aka.ms/mssql-marketplace) extension pack.  This article shows how the [SQL bindings extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-bindings-vscode) for Visual Studio Code can be used to create Azure Functions with SQL bindings.

## Getting Started with SQL Bindings

> [!NOTE]
> Currently, the SQL bindings extension only supports C# Azure Functions. JavaScript and Python Azure Functions support SQL bindings but are not supported by the SQL bindings extension at this time.

### From the Object Explorer 
To create an Azure Function from a specific `Table` or `View` in object explorer (OE), right-click on a table or view from a connected server in SQL Serve object explorer and select `Create Azure Function with SQL Binding.` 

Table OE Command:
:::image type="content" alt-text="Screenshot of object explorer context menu to add a SQL binding from Table." source="./media/create-azure-function-with-mssql/create-function-table-object-explorer.png":::

View OE Command:
:::image type="content" alt-text="Screenshot of object explorer context menu to add a SQL binding from View." source="./media/create-azure-function-with-mssql/create-function-view-object-explorer.png":::

If you haven't yet created the Azure Function project, a VS Code prompt will appear to aid in creating a new Azure Function project.

:::image type="content" alt-text="VS Code notification to create a new azure function project since none were found in folder" source="./media/create-azure-function-with-mssql/create-azure-function-project.png":::

The extension will then ask you to select the folder where you want to create the Azure Function.

:::image type="content" alt-text="Prompt to choose folder to create azure function with SQL binding to" source="./media/create-azure-function-with-mssql/select-folder-to-use-for-function.png":::

If you're creating an Azure Function with SQL binding from a table, the extension will prompt you to select the binding type to use, either an `Input` (Retrieves data from a database) or `Output` (Save data to a database) binding.

> [!NOTE]
> Azure Function with SQL Binding from a `View` is only supported for `Input` bindings.

:::image type="content" alt-text="Prompt to select binding type" source="./media/create-azure-function-with-mssql/binding-type-prompt.png":::

The extension will then prompt you to enter the function name to be used for the Azure Function.

:::image type="content" alt-text="Prompt to enter function name" source="./media/create-azure-function-with-mssql/function-name-prompt.png":::

If you already have connection strings stored in the local.settings.json, then the extension will prompt you to select the connection string to use for the Azure Function or create a new connection string.

:::image type="content" alt-text="Prompt to select connection string setting" source="./media/create-azure-function-with-mssql/create-new-sql-connection-string-setting.png":::

If you `Create new local app setting` then the extension will prompt you to enter the connection string name and value.

:::image type="content" alt-text="Prompt to enter connection string" source="./media/create-azure-function-with-mssql/enter-connection-string-setting-name.png":::

If you're creating the `Azure Function with SQL Binding` to an existing Azure Function project, then the extension will prompt you whether you would like to include the password for the connection string in the local.settings.json file.

:::image type="content" alt-text="Prompt to save the password to the SQL connection string" source="./media/create-azure-function-with-mssql/password-prompt.png":::

## <a name="passwordprompt"/></a>
If `Yes`, then the password will be saved to the local.settings.json file. If `No` then the extension will warn you that the password won't be saved to the local.settings.json file, and you'll need to manually add the password later to the local.settings.json file.

:::image type="content" alt-text="Warning to add password to SQL connection string later manually" source="./media/create-azure-function-with-mssql/do-not-save-password.png":::

The extension will then prompt you to provide the namespace for the Azure Function. 
:::image type="content" alt-text="Warning to add password to SQL connection string later manually" source="./media/create-azure-function-with-mssql/namespace-for-function.png":::

A progress notification will appear to indicate that the Azure Function is being created.

If you're creating a brand new Azure function project with SQL binding, then the extension will prompt whether you would like to include the password for the connection string in the local.settings.json file (discussed [here](#passwordprompt)).

### From the Command Palette 
Run the `MS SQL: Create Azure Function with SQL Binding` command from the command palette to create a new function with a SQL binding. 

:::image type="content" alt-text="VS Code notification to create a new azure function project since none were found in folder" source="./media/create-azure-function-with-mssql/create-azure-function-using-command-palette.png":::

The extension will then prompt you to select the object type to insert (`Input binding`) or upsert into (`Output binding`), either a `Table` or `View`.

:::image type="content" alt-text="Prompt to select the object type" source="./media/create-azure-function-with-mssql/select-object-type.png":::

Then the extension will prompt you to select a connection profile to use for the Azure Function or create a connection profile.

:::image type="content" alt-text="Prompt for connection profile" source="./media/create-azure-function-with-mssql/prompt-for-connection-profile.png":::

Once you either select a connection profile or create a new connection profile, the extension will prompt you to select the database from the selected connection to use for the Azure Function.

:::image type="content" alt-text="Prompt for connection profile" source="./media/create-azure-function-with-mssql/select-database.png":::

Once you select a database, the extension will prompt you to select a table, or view to use or to enter a table or view to query or upsert into. This prompt is based on the object type you selected earlier.

:::image type="content" alt-text="Prompt for table" source="./media/create-azure-function-with-mssql/select-table.png":::

:::image type="content" alt-text="Prompt for view" source="./media/create-azure-function-with-mssql/select-view.png":::

The extension will then prompt you to enter the function name to be used for the Azure Function.

:::image type="content" alt-text="Prompt to enter function name" source="./media/create-azure-function-with-mssql/function-name-prompt.png":::

If you already have connection strings stored in the local.settings.json, then the extension will prompt you to select the connection string to use for the Azure Function or create a new connection string.

:::image type="content" alt-text="Prompt to select connection string setting" source="./media/create-azure-function-with-mssql/create-new-sql-connection-string-setting.png":::

If you `Create new local app setting` then the extension will prompt you to enter the connection string name and value.

:::image type="content" alt-text="Prompt to enter connection string" source="./media/create-azure-function-with-mssql/enter-connection-string-setting-name.png":::

If you're creating the `Azure Function with SQL Binding` to an existing Azure Function project, then the extension will prompt you whether you would like to include the password for the connection string in the local.settings.json file.

:::image type="content" alt-text="Prompt to save the password to the SQL connection string" source="./media/create-azure-function-with-mssql/password-prompt.png":::

## <a name="passwordprompt2"/></a>
If `Yes`, then the password will be saved to the local.settings.json file. If `No` then the extension will warn you that the password won't be saved to the local.settings.json file, and you'll need to manually add the password later to the local.settings.json file.

:::image type="content" alt-text="Warning to add password to SQL connection string later manually" source="./media/create-azure-function-with-mssql/do-not-save-password.png":::

The extension will then prompt you to provide the namespace for the Azure Function. 
:::image type="content" alt-text="Warning to add password to SQL connection string later manually" source="./media/create-azure-function-with-mssql/namespace-for-function.png":::

A progress notification will appear to indicate that the Azure Function is being created.

If you're creating a brand new Azure function project with SQL binding, then the extension will prompt whether you would like to include the password for the connection string in the local.settings.json file (discussed [here](#passwordprompt2)).


### In an existing Azure Function
Open the C# Azure Function in an editor and then run the `MS SQL: Add SQL Binding` command from the command palette to add a SQL binding to an existing function.
:::image type="content" alt-text="VS Code notification to create a new azure function project since none were found in folder" source="./media/create-azure-function-with-mssql/add-sql-binding-command-palette.png":::

The extension will then prompt you to select Azure function in the current file to add the SQL binding to.
:::image type="content" alt-text="VS Code notification to create a new azure function project since none were found in folder" source="./media/create-azure-function-with-mssql/select-azure-functions-in-file.png":::

If you're creating an Azure Function with SQL binding from a table, the extension will prompt you to select the binding type to use, either an `Input` (Retrieves data from a database) or `Output` (Save data to a database) binding.

# TO DO

## Generate Code for Azure Functions with SQL Bindings

**The code generated for SQL Input Binding:**
    
```csharp
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class dboEmployees
    {
        // Visit https://aka.ms/sqlbindingsinput to learn how to use this input binding
    [FunctionName("dboEmployees")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [Sql("SELECT * FROM [dbo].[Employees]",
            CommandType = System.Data.CommandType.Text,
            ConnectionStringSetting = "SqlConnectionString")] IEnumerable<Object> result,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger with SQL Input Binding function processed a request.");

            return new OkObjectResult(result);
        }
    }
}
```

**The code generated for SQL Output Binding:**

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class dboEmployees
    {
        // Visit [https://aka.ms/sqlbindingsoutput] to learn how to use this output binding
        [FunctionName("dboEmployees")]
        public static CreatedResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "addtodoitem")] HttpRequest req,
            [Sql("[dbo].[Test2]", ConnectionStringSetting = "NewSQLConnectionString")] out ToDoItem output,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger with SQL Output Binding function processed a request.");

            output = new ToDoItem
            {
                Id = "1",
                Priority = 1,
                Description = "Hello World"
            };

            return new CreatedResult($"/api/addtodoitem", output);
        }
    }

    public class ToDoItem
    {
        public string Id { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
    }
}
```
