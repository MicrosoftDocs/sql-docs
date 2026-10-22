---
title: "Use Visual Studio with .NET and C# to Query"
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: "Use Visual Studio to create a C# app that connects to a database in Azure SQL Database or Azure SQL Managed Instance and runs queries."
author: cwebb-msft
ms.author: chweb
ms.reviewer: wiassaf
ms.date: 01/14/2025
ms.service: azure-sql
ms.subservice: connect
ms.topic: quickstart
ms.custom:
  - devx-track-csharp
  - sqldbrb=2
  - mode-ui
ms.devlang: csharp
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---
# Quickstart: Connect to and query a database with .NET and C# in Visual Studio

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi-asa.md)]

This quickstart shows how to use the [.NET](https://dotnet.microsoft.com) and C# code in Visual Studio to query a database in Azure SQL or Synapse SQL with Transact-SQL statements.

## Prerequisites

To complete this quickstart, you need:

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- [Visual Studio 2022](https://www.visualstudio.com/downloads/) Community, Professional, or Enterprise edition.
- A database where you can run a query.

  [!INCLUDE [create-configure-database](../includes/create-configure-database.md)]

## Create code to query the database in Azure SQL Database

1. In Visual Studio, create a new project.

1. In the **New Project** dialog, select the **C# Console App**.

1. Enter *sqltest* for the project name, and then select **Next**.

1. Select a *(Long-term support)* Framework option, such as *.NET 8.0*, and then select **Create**. The new project is created.

1. Select **Project** > **Manage NuGet Packages**.

1. In **NuGet Package Manager**, select the **Browse** tab, then search for and select **Microsoft.Data.SqlClient**.

1. On the **Microsoft.Data.SqlClient** page, select **Install**.
   - If prompted, select **OK** to continue with the installation.
   - If a **License Acceptance** window appears, select **I Accept**.

1. When the install completes, you can close **NuGet Package Manager**.

1. In the code editor, replace the **Program.cs** contents with the following code. Replace your values for `<your_server>`, `<your_username>`, `<password>`, and `<your_database>`.

   ```csharp
   using System;
   using Microsoft.Data.SqlClient;
   using System.Text;

   namespace sqltest
   {
       class Program
       {
           static void Main(string[] args)
           {
               try
               {
                   SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                   builder.DataSource = "<your_server>.database.windows.net";
                   builder.UserID = "<your_username>";
                   builder.Password = "<password>";
                   builder.InitialCatalog = "<your_database>";

                   using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                   {
                       Console.WriteLine("\nQuery data example:");
                       Console.WriteLine("=========================================\n");

                       String sql = "SELECT name, collation_name FROM sys.databases";

                       using (SqlCommand command = new SqlCommand(sql, connection))
                       {
                           connection.Open();
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
               Console.ReadLine();
           }
       }
   }
   ```

## Run the code

1. To run the app, select **Debug** > **Start Debugging**, or select **Start** on the toolbar, or press **F5**.
1. Verify that the database names and collations are returned, and then close the app window.

## Related content

- [Quickstart: Use .NET (C#) to query a database](connect-query-dotnet-core.md)
- [Getting started with .NET on Windows/Linux/macOS using VS Code](/dotnet/core/tutorials/with-visual-studio-code)
- [Developing with .NET and SQL](/sql/connect/ado-net/sql)
- [Tutorial: Design a relational database in Azure SQL Database](design-first-database-tutorial.md)
- [.NET documentation](/dotnet/)
- [Connect resiliently to Azure SQL with ADO.NET](/sql/connect/ado-net/step-4-connect-resiliently-sql-ado-net)
