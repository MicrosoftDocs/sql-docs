---
title: Create a stored procedure
description: Learn how to create a Transact-SQL stored procedure by using SQL Server Management Studio and by using the Transact-SQL CREATE PROCEDURE statement.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 01/25/2024
ms.service: sql
ms.subservice: stored-procedures
ms.topic: quickstart
ms.custom: intro-quickstart
helpviewer_keywords:
  - "new stored procedures"
  - "stored procedures [SQL Server], creating"
  - "creating stored procedures"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create a stored procedure

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE PROCEDURE statement.
  
## Permissions
 Requires CREATE PROCEDURE permission in the database and ALTER permission on the schema in which the procedure is being created.
  
## Create a stored procedure
You can use the [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) user interface or [!INCLUDE[tsql](../../includes/tsql-md.md)] in an SSMS query window to create a stored procedure. Always use the latest version of SSMS.

>[!NOTE]
>The example stored procedure in this article uses the sample `AdventureWorksLT2022` ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]) or `AdventureWorksLT` ([!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)]) database. For instructions on how to get and use the `AdventureWorksLT` sample databases, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

### <a id="SSMSProcedure"></a> Use SQL Server Management Studio

To create a stored procedure in SSMS:

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)].

   For more information, see the following quickstarts:

   - [Connect and query a SQL Server instance using SSMS](../../ssms/quickstarts/ssms-connect-query-sql-server.md)
   - [Connect to and query Azure SQL Database or Azure SQL Managed Instance using SSMS](/azure/azure-sql/database/connect-query-ssms)

1. Expand the instance, and then expand **Databases**.

1. Expand the database that you want, and then expand **Programmability**.

1. Right-click **Stored Procedures**, and then select **New** > **Stored Procedure**. A new query window opens with a template for the stored procedure.

   The default stored procedure template has two parameters. If your stored procedure has fewer, more, or no parameters, add or remove parameter lines in the template as appropriate.

1. On the **Query** menu, select **Specify Values for Template Parameters**.

1. In the **Specify Values for Template Parameters** dialog box, provide the following information for the **Value** fields:

   - **Author**: Replace `Name` with your name.
   - **Create Date**: Enter today's date.
   - **Description**: Briefly describe what the procedure does.
   - **Procedure_Name**: Replace `ProcedureName` with the new stored procedure name.
   - **@Param1**: Replace `@p1` with your first parameter name, such as *@ColumnName1*.
   - **@Datatype_For_Param1**: As appropriate, replace `int` with your first parameter's datatype, such as *nvarchar(50)*.
   - **Default_Value_For_Param1**: As appropriate, replace `0` with your first parameter's default value, or *NULL*.
   - **@Param2**: Replace `@p2` with your second parameter name, such as *@ColumnName2*.
   - **@Datatype_For_Param2**: As appropriate, replace `int` with your second parameter's datatype, such as *nvarchar(50)*.
   - **Default_Value_For_Param2**: As appropriate, replace `0` with your second parameter's default value, or *NULL*.
  
   The following screenshot shows the completed dialog box for the example stored procedure:
   
   :::image type="content" source="media/create-a-stored-procedure/specify-values.png" alt-text="Screenshot that shows a completed Specify Values for Template Parameters dialog box.":::

1. Select **OK**.
  
1. In the **Query Editor**, replace the SELECT statement with the query for your procedure.

   The following code shows the completed CREATE PROCEDURE statement for the example stored procedure:
   
   ```sql
   -- =======================================================
   -- Create Stored Procedure Template for Azure SQL Database
   -- =======================================================
   SET ANSI_NULLS ON
   GO
   SET QUOTED_IDENTIFIER ON
   GO
   -- =============================================
   -- Author:      My Name
   -- Create Date: 01/23/2024
   -- Description: Returns the customer's company name.
   -- =============================================
   CREATE PROCEDURE SalesLT.uspGetCustomerCompany
   (
       -- Add the parameters for the stored procedure here
       @LastName nvarchar(50) = NULL,
       @FirstName nvarchar(50) = NULL
   )
   AS
   BEGIN
       -- SET NOCOUNT ON added to prevent extra result sets from
       -- interfering with SELECT statements.
       SET NOCOUNT ON
   
       -- Insert statements for procedure here
       SELECT FirstName, LastName, CompanyName
          FROM SalesLT.Customer
          WHERE FirstName = @FirstName AND LastName = @LastName;
   END
   GO
   ```

1. To test the syntax, on the **Query** menu, select **Parse**. Correct any errors.
  
1. Select **Execute** from the toolbar. The procedure is created as an object in the database.
  
1. To see the new procedure listed in **Object Explorer**, right-click **Stored Procedures** and select **Refresh**.
  
To run the procedure:

1. In **Object Explorer**, right-click the stored procedure name and select **Execute Stored Procedure**.
  
1. In the **Execute Procedure** window, enter values for all parameters, and then select **OK**. For detailed instructions, see [Execute a stored procedure](execute-a-stored-procedure.md#SSMSProcedure).

   For example, to run the `SalesLT.uspGetCustomerCompany` sample procedure, enter *Cannon* for the **@LastName** parameter and *Chris* for the **@FirstName** parameter, and then select **OK**. The stored procedure runs, and returns `FirstName` **Chris**, `LastName` **Cannon**, and `CompanyName` **Outdoor Sporting Goods**.
  
> [!IMPORTANT]  
> Validate all user input. Don't concatenate user input before you validate it. Never execute a command constructed from unvalidated user input.
  
### <a id="TsqlProcedure"></a> Use Transact-SQL

To create a procedure in the SSMS **Query Editor**:
  
1. In SSMS, connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)].
1. Select **New Query** from the toolbar.
  
1. Input the following code into the query window, replacing `<ProcedureName>`, the names and data types of any parameters, and the SELECT statement with your own values.

  
    ```sql 
   CREATE PROCEDURE <ProcedureName>
       @<ParameterName1> <data type>,
       @<ParameterName2> <data type>
   AS   
   
       SET NOCOUNT ON;
       SELECT <your SELECT statement>;
   GO
   ```

   For example, the following statement creates the same stored procedure in the `AdventureWorksLT` database as the previous example, with a slightly different procedure name.

   ```sql 
   CREATE PROCEDURE SalesLT.uspGetCustomerCompany1
       @LastName nvarchar(50),
       @FirstName nvarchar(50)
   AS   
   
       SET NOCOUNT ON;
       SELECT FirstName, LastName, CompanyName
       FROM SalesLT.Customer
       WHERE FirstName = @FirstName AND LastName = @LastName;
   GO
   ```
  
1. Select **Execute** from the toolbar to execute the query. The stored procedure is created.

1. To run the stored procedure, enter an EXECUTE statement in a new query window, providing values for any parameters, and then select **Execute**. For detailed instructions, see [Execute a stored procedure](execute-a-stored-procedure.md#TsqlProcedure).

## Related content

- [Stored Procedures (Database Engine)](stored-procedures-database-engine.md)
- [CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)
- [Execute a stored procedure](execute-a-stored-procedure.md)
- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [Specify parameters in a stored procedure](specify-parameters.md)
