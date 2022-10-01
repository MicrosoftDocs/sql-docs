---
title: Use .NET to connect and query a database on Windows, Linux, or macOS
titleSuffix: Azure SQL Database & SQL Managed Instance
description: This topic shows you how to use .NET to create a program that connects to a database in Azure SQL Database, or Azure SQL Managed Instance, and queries it using Transact-SQL statements.
author: dzsquared
ms.author: drskwier
ms.reviewer: wiassaf, mathoma
ms.date: 08/17/2022
ms.service: sql-database
ms.subservice: connect
ms.topic: quickstart
ms.custom:
  - "sqldbrb=2"
  - "devx-track-csharp"
  - "mode-other"
ms.devlang: csharp
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Quickstart: Use .NET (C#) to query a database
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi-asa.md)]

In this quickstart, you'll use [.NET](https://dotnet.microsoft.com) and C# code to connect to a database. You'll then run a Transact-SQL statement to query data.  This quickstart is applicable to Windows, Linux, and macOS and leverages the unified .NET platform.

> [!TIP]
> This free Learn module shows you how to [Develop and configure an ASP.NET application that queries a database in Azure SQL Database](/training/modules/develop-app-that-queries-azure-sql/)

## Prerequisites

To complete this quickstart, you need:

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/?ref=microsoft.com&utm_source=microsoft.com&utm_medium=docs&utm_campaign=visualstudio).
- [.NET SDK for your operating system](https://dotnet.microsoft.com/download) installed.
- A database where you can run your query. 

  [!INCLUDE[create-configure-database](../includes/create-configure-database.md)]
  
## Create a new .NET project

1. Open a command prompt and create a folder named **sqltest**. Navigate to this folder and run this command.

    ```bash
    dotnet new console
    ```

    This command creates new app project files, including an initial C# code file (**Program.cs**), an XML configuration file (**sqltest.csproj**), and needed binaries.

2. At the command prompt used above, run this command.
    
    ```bash
    dotnet add package Microsoft.Data.SqlClient
    ```

    This command adds the `Microsoft.Data.SqlClient` package to the project.

## Insert code to query the database in Azure SQL Database

1. In a text editor such as [Visual Studio Code](https://code.visualstudio.com/), open **Program.cs**.

2. Replace the contents with the following code and add the appropriate values for your server, database, username, and password.

> [!NOTE]
> To use an ADO.NET connection string, replace the 4 lines in the code
> setting the server, database, username, and password with the line below. In
> the string, set your username and password.
>
>    `builder.ConnectionString="<your_ado_net_connection_string>";`

```csharp
using Microsoft.Data.SqlClient;

namespace sqltest
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "<your_server.database.windows.net>"; 
                builder.UserID = "<your_username>";            
                builder.Password = "<your_password>";     
                builder.InitialCatalog = "<your_database>";
         
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");
                    
                    connection.Open();       

                    String sql = "SELECT name, collation_name FROM sys.databases";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine(); 
        }
    }
}
```

## Run the code

1. At the prompt, run the following commands.

   ```bash
   dotnet restore
   dotnet run
   ```

2. Verify that the rows are returned, your output may include other values.

   ```text
   Query data example:
   =========================================

   master	SQL_Latin1_General_CP1_CI_AS
   tempdb	SQL_Latin1_General_CP1_CI_AS
   WideWorldImporters	Latin1_General_100_CI_AS

   Done. Press enter.
   ```

3. Choose **Enter** to close the application window.

## Next steps

- [Getting started with .NET on Windows/Linux/macOS using VS Code](/dotnet/core/tutorials/with-visual-studio-code).
- Learn how to [connect to Azure SQL Database using Azure Data Studio on Windows/Linux/macOS](/sql/azure-data-studio/quickstart-sql-database).
- Learn more about [developing with .NET and SQL](/sql/connect/ado-net/sql).
- Learn how to [connect and query Azure SQL Database or Azure SQL Managed Instance, by using .NET in Visual Studio](connect-query-dotnet-visual-studio.md).
- Learn how to [Design your first database with SSMS](design-first-database-tutorial.md).
- For more information about .NET, see [.NET documentation](/dotnet/).
