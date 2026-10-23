---
title: Work with CLR Database Objects
description: "Learn how to use .NET Framework languages to retrieve and update data. See how to create and use a Common Language Run (CLR) database object in a project."
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "sql.data.tools.allowsqlclrdebugging"
---

# How to: Work with CLR database objects

In addition to the Transact-SQL programming language, you can use .NET Framework languages to create database objects that retrieve and update data. Database objects that are written in managed code are called SQL Server Common Language Run (CLR) database objects. For an explanation of the advantages of using CLR database objects hosted in SQL Server, and how to choose between Transact-SQL and CLR, see [CLR integration overview](../relational-databases/clr-integration/clr-integration-overview.md) and [Advantages of Using Managed Code to Create Database Objects](/previous-versions/visualstudio/visual-studio-2010/k2e1fb36(v=vs.100)).

To create a CLR database object using SQL Server Data Tools, you create a database project and then add a CLR database object to it. Unlike in previous versions of Visual Studio, you don't need to create a separate CLR project and then add a reference to it from the database project. When you build and publish the database project, you automatically publish the CLR objects in the project at the same time. After you publish these CLR objects, they can be called and executed like any other database objects.

The CLR and CLR Build property pages contain many settings for using CLR database objects in your project. Specifically, the CLR property page has a permission level setting to set permissions on the CLR assembly. It also has the "Generate DDL" setting to control whether DDL for the CLR database objects added to the project is generated. The CLR Build property page contains all the compiler options that you can set to configure the compilation of CLR code in the project. These property pages can be accessed by right-clicking your project in **Solution Explorer** and select **Properties**.

To enable debugging of CLR database objects, open **SQL Server Object Explorer**. Right-click the server containing the CLR database artifacts you want to debug, and choose **Allow SQL/CLR Debugging**. A message box appears with the warning:

```output
During debugging, all managed threads on this server stop. Do you wish to enable SQL CLR debugging on this server?
```

When you're debugging CLR database objects, breaking execution breaks all threads on the server, affecting other users. For this reason, you shouldn't debug applications for CLR database objects on a production server. You should also note that once you have started debugging, it's too late to change settings in **SQL Server Object Explorer**. Changes made in **SQL Server Object Explorer** don't take effect until the start of the next debugging session.

For more information on the requirements of building CLR database objects, see [Build database objects with common language runtime (CLR) integration](../relational-databases/clr-integration/database-objects/building-database-objects-with-common-language-runtime-clr-integration.md).

> [!WARNING]  
> The following procedure uses entities created in previous procedures in the [SQL database projects](../tools/sql-database-projects/sql-database-projects.md) sections.

## Add a CLR database object to your project

1. Right-click the `TradeDev` database project in **Solution Explorer**, select **Add**, then **New Item**.

1. Select the **C# SQL CLR** template, then **SQL CLR User-Defined Function**. Accept the default name and select **Add**.

1. Add the following code to class body. This function validates a U.S. phone number. It must consist of three numeric characters, optionally enclosed in parentheses, followed by a set of three numeric characters and then a set of four numeric characters. Examples of supported formats are (425) 555-0123, 425-555-0123, 425 555 0123 and 1-425-555-0123.

   ```csharp
   [SqlFunction(IsDeterministic = true, IsPrecise = true)]
   public static SqlBoolean validatePhone(SqlString phone)
   {
       string aNorthAmericanPhoneNumberPattern = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
       if (!phone.IsNull)
       {
          Regex regex = new Regex(aNorthAmericanPhoneNumberPattern);
          return regex.IsMatch(phone.Value);
       }
       return true;
    }
   ```

1. `Regex` is underlined in red. Right-click `Regex` and select **Resolve**, then **using System.Text.RegularExpressions**.

1. If you're developing against a Microsoft SQL Server 2012 server instance, you can skip this step. Otherwise, SQL Server 2005 and SQL Server 2008 only support database projects that are built with the 2.0, 3.0, or 3.5 version of the .NET Framework. To make sure that the .NET target platform is correctly set, right-click the `TradeDev` database project in **Solution Explorer**, select **Properties**. In the **SQLCLR** property page, change the **Target platform** to **.NET Framework 3.5** or below. Select **Yes** in the final screen to close and reopen the project.

1. Right-click the `TradeDev` project and select **Build** to build the project.

1. Double-click Suppliers.sql and select **View Designer** to open the Suppliers table in Table Designer.

1. Select the empty row in the Columns Grid to add a new column to the table. Enter **phone** for the **Name** field, **nvarchar (128)** for **Data Type** and leave the **Allow Nulls** field checked.

1. Right-click the **Check Constraints** node in the Context pane and select **Add New Check Constraint**.

1. Replace the default definition of the constraint in the Script pane with the following.

   ```sql
   CONSTRAINT [CK_Suppliers_CheckPhone] CHECK (dbo.validatePhone(phone)=1),
   ```

   This ensures that any input to the new phone field is checked using the CLR UDF we added previously.

1. Press **F5** to build and deploy the project to the local database.

### Use CLR database objects

1. In **SQL Server Object Explorer**, navigate to the local database where you deploy your project to.

1. By default, CLR integration is turned off in SQL Server. To use CLR database objects, you must enable CLR integration. To do this, use the "clr enabled" option of the `sp_configure` stored procedure. For more information, see the [clr enabled Option article](../relational-databases/clr-integration/clr-integration-enabling.md).

   Right-click the database and select **New Query**. In the query pane, paste the following code and press the **Execute Query** button.

   ```sql
   EXECUTE sp_configure 'clr enabled', 1;
   GO

   RECONFIGURE;
   GO
   ```

1. Right-click the Suppliers table and select **View Data**.

1. Enter `5` for **id**, **Contoso** for **name**, leave the **Address** field empty, and `425 3122 1222` for **phone**. Tab away from the **phone** field, and notice that a message pops up, indicating that the `INSERT` statement conflicts with your existing check constraint, which checks the input of the **phone** field using a predefined phone pattern.

1. Change your input to `425 312 1222` and tab away. The input is accepted this time.

## Related content

- [CLR integration overview](../relational-databases/clr-integration/clr-integration-overview.md)
- [Advantages of Using Managed Code to Create Database Objects](/previous-versions/visualstudio/visual-studio-2010/k2e1fb36(v=vs.100))
- [Build database objects with common language runtime (CLR) integration](../relational-databases/clr-integration/database-objects/building-database-objects-with-common-language-runtime-clr-integration.md)
