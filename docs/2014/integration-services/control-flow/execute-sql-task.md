---
title: "Execute SQL Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.executesqltask.f1"
helpviewer_keywords: 
  - "Transact-SQL statements, SSIS"
  - "statements [Integration Services]"
  - "batches [Integration Services]"
  - "Execute SQL task [Integration Services]"
ms.assetid: bebb2e8c-0410-43b2-ac2f-6fc80c8f2e9e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Execute SQL Task
  The Execute SQL task runs SQL statements or stored procedures from a package. The task can contain either a single SQL statement or multiple SQL statements that run sequentially. You can use the Execute SQL task for the following purposes:  
  
-   Truncate a table or view in preparation for inserting data.  
  
-   Create, alter, and drop database objects such as tables and views.  
  
-   Re-create fact and dimension tables before loading data into them.  
  
-   Run stored procedures. If the SQL statement invokes a stored procedure that returns results from a temporary table, use the WITH RESULT SETS option to define metadata for the result set.  
  
-   Save the rowset returned from a query into a variable.  
  
 The Execute SQL task can be used in combination with the Foreach Loop and For Loop containers to run multiple SQL statements. These containers implement repeating control flows in a package and they can run the Execute SQL task repeatedly. For example, using the Foreach Loop container, a package can enumerate files in a folder and run an Execute SQL task repeatedly to execute the SQL statement stored in each file.  
  
## Connecting to a Data Source  
 The Execute SQL task can use different types of connection managers to connect to the data source where it runs the SQL statement or stored procedure. The task can use the connection types listed in the following table.  
  
|Connection type|Connection manager|  
|---------------------|------------------------|  
|EXCEL|[Excel Connection Manager](../connection-manager/excel-connection-manager.md)|  
|OLE DB|[OLE DB Connection Manager](../connection-manager/ole-db-connection-manager.md)|  
|ODBC|[ODBC Connection Manager](../connection-manager/odbc-connection-manager.md)|  
|ADO|[ADO Connection Manager](../connection-manager/ado-connection-manager.md)|  
|ADO.NET|[ADO.NET Connection Manager](../connection-manager/ado-net-connection-manager.md)|  
|SQLMOBILE|[SQL Server Compact Edition Connection Manager](../connection-manager/sql-server-compact-edition-connection-manager.md)|  
  
## Creating SQL Statements  
 The source of the SQL statements used by this task can be a task property that contains a statement, a connection to a file that contains one or multiple statements, or the name of a variable that contains a statement. The SQL statements must be written in the dialect of the source database management system (DBMS). For more information, see [Integration Services &#40;SSIS&#41; Queries](../integration-services-ssis-queries.md).  
  
 If the SQL statements are stored in a file, the task uses a File connection manager to connect to the file. For more information, see [File Connection Manager](../connection-manager/file-connection-manager.md).  
  
 In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, you can use the **Execute SQL Task Editor** dialog box to type SQL statements, or use **Query Builder**, a graphical user interface for creating SQL queries. For more information, see [Execute SQL Task Editor &#40;General Page&#41;](../execute-sql-task-editor-general-page.md) and [Query Builder](../query-builder.md).  
  
> [!NOTE]  
>  Valid SQL statements written outside the Execute SQL task may not be parsed successfully by the Execute SQL task.  
  
> [!NOTE]  
>  The Execute SQL Task uses the `RecognizeAll` ParseMode enumeration value. For more information, see [ManagedBatchParser Namespace](https://go.microsoft.com/fwlink/?LinkId=223617).  
  
## Sending Multiple Statements in a Batch  
 If you include multiple statements in an Execute SQL task, you can group them and run them as a batch. To signal the end of a batch, use the GO command. All the SQL statements between two GO commands are sent in a batch to the OLE DB provider to be run. The SQL command can include multiple batches separated by GO commands.  
  
 There are restrictions on the kinds of SQL statements that you can group in a batch. For more information, see [Batches of Statements](../../relational-databases/native-client-odbc-queries/executing-statements/batches-of-statements.md).  
  
 If the Execute SQL task runs a batch of SQL statements, the following rules apply to the batch:  
  
-   Only one statement can return a result set and it must be the first statement in the batch.  
  
-   If the result set uses result bindings, the queries must return the same number of columns. If the queries return a different number of columns, the task fails. However, even if the task fails, the queries that it runs, such as DELETE or INSERT queries, may succeed.  
  
-   If the result bindings use column names, the query must return columns that have the same names as the result set names that are used in the task. If the columns are missing, the task fails.  
  
-   If the task uses parameter binding, all the queries in the batch must have the same number and types of parameters.  
  
## Running Parameterized SQL Commands  
 SQL statements and stored procedures frequently use input parameters, output parameters, and return codes. The Execute SQL task supports the `Input`, `Output`, and `ReturnValue` parameter types. You use the `Input` type for input parameters, `Output` for output parameters, and `ReturnValue` for return codes.  
  
> [!NOTE]  
>  You can use parameters in an Execute SQL task only if the data provider supports them.  
  
 For information on using parameters and return codes in the Execute SQL task, see [Parameters and Return Codes in the Execute SQL Task](execute-sql-task.md).  
  
## Specifying a Result Set Type  
 Depending on the type of SQL command, a result set may or may not be returned to the Execute SQL task. For example, a SELECT statement typically returns a result set, but an INSERT statement does not. The result set from a SELECT statement can contain zero rows, one row, or many rows. Stored procedures can also return an integer value, called a return code, that indicates the execution status of the procedure. In that case, the result set consists of a single row.  
  
 For information on retrieving result sets from SQL commands in the Execute SQL task, see [Result Sets in the Execute SQL Task](../result-sets-in-the-execute-sql-task.md).  
  
## Troubleshooting the Execute SQL Task  
 You can log the calls that the Execute SQL task makes to external data providers. You can use this logging capability to troubleshoot the SQL commands that the Execute SQL task runs. To log the calls that the Execute SQL task makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
 Sometimes a SQL command or stored procedure returns multiple result sets. These result sets include not only rowsets that are the result of `SELECT` queries, but single values that are the result of errors of `RAISERROR` or `PRINT` statements. Whether the task ignores errors in result sets that occur after the first result set depends on the type of connection manager that is used:  
  
-   When you use OLE DB and ADO connection managers, the task ignores the result sets that occur after the first result set. Therefore, with these connection managers, the task ignores an error returned by an SQL command or a stored procedure when the error is not part of the first result set.  
  
-   When you use ODBC and ADO.NET connection managers, the task does not ignore result sets that occur after the first result set. With these connection managers, the task will fail with an error when a result set other than the first result set contains an error.  
  
### Custom Log Entries  
 The following table describes the custom log entry for the Execute SQL task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../performance/integration-services-ssis-logging.md) and [Custom Messages for Logging](../custom-messages-for-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|`ExecuteSQLExecutingQuery`|Provides information about the execution phases of the SQL statement. Log entries are written when the task acquires connection to the database, when the task starts to prepare the SQL statement, and after the execution of the SQL statement is completed. The log entry for the prepare phase includes the SQL statement that the task uses.|  
  
## Configuring the Execute SQL Task  
 You can configure the Execute SQL task in the following ways:  
  
-   Specify the type of connection manager to use to connect to a database.  
  
-   Specify the type of result set that the SQL statement returns.  
  
-   Specify a time-out for the SQL statements.  
  
-   Specify the source of the SQL statement.  
  
-   Indicate whether the task skips the prepare phase for the SQL statement.  
  
-   If you use the ADO connection type, you must indicate whether the SQL statement is a stored procedure. For other connection types, this property is read-only and its value is always `false`.  
  
 You can set properties programmatically or through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Execute SQL Task Editor &#40;General Page&#41;](../execute-sql-task-editor-general-page.md)  
  
-   [Execute SQL Task Editor &#40;Parameter Mapping Page&#41;](../execute-sql-task-editor-parameter-mapping-page.md)  
  
-   [Execute SQL Task Editor &#40;Result Set Page&#41;](../execute-sql-task-editor-result-set-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## Configuring the Execute SQL Task Programmatically  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask.ExecuteSQLTask>  
  
## Related Tasks  
  
-   [Map Query Parameters to Variables in an Execute SQL Task](../map-query-parameters-to-variables-in-an-execute-sql-task.md)  
  
-   [Map Result Sets to Variables in an Execute SQL Task](../map-result-sets-to-variables-in-an-execute-sql-task.md)  
  
## Related Content  
  
-   [Parameters and Return Codes in the Execute SQL Task](execute-sql-task.md)  
  
-   [Result Sets in the Execute SQL Task](../result-sets-in-the-execute-sql-task.md)  
  
-   [Transact-SQL Reference &#40;Database Engine&#41;](/sql/t-sql/language-reference)  
  
-   Blog entry, [New Date and Time Functions in SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=239783), on mssqltips.com  
  
  
