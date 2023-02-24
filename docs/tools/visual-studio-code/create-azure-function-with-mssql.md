---
title: Create Azure Functions with the SQL bindings extension for Visual Studio Code
description: Use the SQL bindings extension for Visual Studio Code to create Azure functions with SQL bindings.
ms.topic: conceptual
ms.service: sql
ms.subservice: tools-other
author: VasuBhog
ms.author: vabhog
ms.reviewer: drskwier
ms.date: 8/24/2022
---

# Create Azure Functions with the SQL bindings extension for Visual Studio Code

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Azure Functions support for [SQL bindings](/azure/azure-functions/functions-bindings-azure-sql) is available in preview for input and output bindings, making connecting to an Azure SQL database or SQL Server database to Azure Functions easier. The SQL bindings extension for Visual Studio Code (VS Code) facilitates the process of developing Azure Functions with SQL bindings and is automatically installed with the [mssql extension for VS Code](https://aka.ms/mssql-marketplace) extension pack.  This article shows how the [SQL bindings extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-bindings-vscode) for Visual Studio Code can be used to create Azure Functions with SQL bindings.

> [!NOTE]
> Currently, the SQL bindings extension only supports C# Azure Functions. JavaScript and Python Azure Functions support SQL bindings but are not supported by the SQL bindings extension at this time.

## From the Object Explorer 
To create an Azure Function from a specific `Table` or `View` in object explorer (OE), right-click on a table or view from a connected server in SQL Server object explorer and select `Create Azure Function with SQL Binding.` 

**Table OE Command**:
:::image type="content" alt-text="Screenshot of object explorer context menu to add a SQL binding from Table." source="./media/create-azure-function-with-mssql/create-function-table-object-explorer.png":::

**View OE Command**:
:::image type="content" alt-text="Screenshot of object explorer context menu to add a SQL binding from View." source="./media/create-azure-function-with-mssql/create-function-view-object-explorer.png":::

See further documentation to create an Azure function with SQL bindings from the SQL Server object explorer [here](create-azure-function-with-mssql-object-explorer.md).

## From the Command Palette 
Run the `MS SQL: Create Azure Function with SQL Binding` command from the command palette to create a new function with a SQL binding. 

:::image type="content" alt-text="VS Code command palette command `MS SQL: Create Azure Function with SQL Binding (preview)." source="./media/create-azure-function-with-mssql/create-azure-function-using-command-palette.png":::

See further documentation to create an Azure function with SQL bindings from the command palette [here](create-azure-function-with-mssql-command-palette.md).

### In an existing Azure Function

Open the C# Azure Function in an editor and then run the `MS SQL: Add SQL Binding` command from the command palette to add a SQL binding to an existing function.

:::image type="content" alt-text="VS Code command palette command `MS SQL: Add SQL Binding (preview)." source="./media/create-azure-function-with-mssql/add-sql-binding-command-palette.png":::

See further documentation [here](create-azure-function-with-mssql-command-palette.md).

## Generated Code for Azure Functions with SQL Bindings

**The code generated for the Azure function with SQL Input Binding:**
    
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

**The code generated for the Azure function with SQL Output Binding:**

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

## Next steps

- [Install the VS Code extension here](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-bindings-vscode).
- [Use the mssql extension to query a SQL instance](mssql-extensions.md).
- [Learn more about SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql).
- [Create Azure Function with SQL binding through the object explorer tutorial](create-azure-function-with-mssql-object-explorer.md).
- [Create Azure Function with SQL binding through the command palette tutorial](create-azure-function-with-mssql-command-palette.md).