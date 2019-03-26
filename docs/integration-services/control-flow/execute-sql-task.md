---
title: "Execute SQL Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords:
- "sql13.dts.designer.executesqltask.f1"
- "sql13.dts.designer.executesqltask.general.f1"
- "sql13.dts.designer.executesqltask.parametermapping.f1"
- "sql13.dts.designer.executesqltask.resultset.f1"
helpviewer_keywords: 
  - "Transact-SQL statements, SSIS"
  - "statements [Integration Services]"
  - "batches [Integration Services]"
  - "Execute SQL task [Integration Services]"
ms.assetid: bebb2e8c-0410-43b2-ac2f-6fc80c8f2e9e
author: janinezhang
ms.author: janinez
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
  
## Connect to a data source  
 The Execute SQL task can use different types of connection managers to connect to the data source where it runs the SQL statement or stored procedure. The task can use the connection types listed in the following table.  
  
|Connection type|Connection manager|  
|---------------------|------------------------|  
|EXCEL|[Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md)|  
|OLE DB|[OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md)|  
|ODBC|[ODBC Connection Manager](../../integration-services/connection-manager/odbc-connection-manager.md)|  
|ADO|[ADO Connection Manager](../../integration-services/connection-manager/ado-connection-manager.md)|  
|ADO.NET|[ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md)|  
|SQLMOBILE|[SQL Server Compact Edition Connection Manager](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager.md)|  
  
## Create SQL statements  
 The source of the SQL statements used by this task can be a task property that contains a statement, a connection to a file that contains one or multiple statements, or the name of a variable that contains a statement. The SQL statements must be written in the dialect of the source database management system (DBMS). For more information, see [Integration Services &#40;SSIS&#41; Queries](../../integration-services/integration-services-ssis-queries.md).  
  
 If the SQL statements are stored in a file, the task uses a File connection manager to connect to the file. For more information, see [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md).  
  
 In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, you can use the **Execute SQL Task Editor** dialog box to type SQL statements, or use **Query Builder**, a graphical user interface for creating SQL queries. 
  
> [!NOTE]  
>  Valid SQL statements written outside the Execute SQL task may not be parsed successfully by the Execute SQL task.  
  
> [!NOTE]  
>  The Execute SQL Task uses the **RecognizeAll** ParseMode enumeration value. For more information, see [ManagedBatchParser Namespace](https://go.microsoft.com/fwlink/?LinkId=223617).  
  
## Send multiple statements in a batch  
 If you include multiple statements in an Execute SQL task, you can group them and run them as a batch. To signal the end of a batch, use the GO command. All the SQL statements between two GO commands are sent in a batch to the OLE DB provider to be run. The SQL command can include multiple batches separated by GO commands.  
  
 There are restrictions on the kinds of SQL statements that you can group in a batch. For more information, see [Batches of Statements](../../relational-databases/native-client-odbc-queries/executing-statements/batches-of-statements.md).  
  
 If the Execute SQL task runs a batch of SQL statements, the following rules apply to the batch:  
  
-   Only one statement can return a result set and it must be the first statement in the batch.  
  
-   If the result set uses result bindings, the queries must return the same number of columns. If the queries return a different number of columns, the task fails. However, even if the task fails, the queries that it runs, such as DELETE or INSERT queries, may succeed.  
  
-   If the result bindings use column names, the query must return columns that have the same names as the result set names that are used in the task. If the columns are missing, the task fails.  
  
-   If the task uses parameter binding, all the queries in the batch must have the same number and types of parameters.  
  
## Run parameterized SQL commands  
 SQL statements and stored procedures frequently use input parameters, output parameters, and return codes. The Execute SQL task supports the **Input**, **Output**, and **ReturnValue** parameter types. You use the **Input** type for input parameters, **Output** for output parameters, and **ReturnValue** for return codes.  
  
> [!NOTE]  
>  You can use parameters in an Execute SQL task only if the data provider supports them.  
  
## Specify a result set type  
 Depending on the type of SQL command, a result set may or may not be returned to the Execute SQL task. For example, a SELECT statement typically returns a result set, but an INSERT statement does not. The result set from a SELECT statement can contain zero rows, one row, or many rows. Stored procedures can also return an integer value, called a return code, that indicates the execution status of the procedure. In that case, the result set consists of a single row.  
  
## Configure the Execute SQL task  
 You can configure the Execute SQL task in the following ways:  
  
-   Specify the type of connection manager to use to connect to a database.  
  
-   Specify the type of result set that the SQL statement returns.  
  
-   Specify a time-out for the SQL statements.  
  
-   Specify the source of the SQL statement.  
  
-   Indicate whether the task skips the prepare phase for the SQL statement.  
  
-   If you use the ADO connection type, you must indicate whether the SQL statement is a stored procedure. For other connection types, this property is read-only and its value is always **false**.  
  
 You can set properties programmatically or through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  

## General Page - Execute SQL Task Editor
 Use the **General** page of the **Execute SQL Task Editor** dialog box to configure the Execute SQL task and provide the SQL statement that the task runs.  

To learn more about the Transact-SQL query language, see [Transact-SQL Reference &#40;Database Engine&#41;](../../t-sql/transact-sql-reference-database-engine.md).  
  
### Static Options  
 **Name**  
 Provide a unique name for the Execute SQL task in the workflow. The name that is provided will be displayed within [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 **Description**  
 Describe the Execute SQL task. As a best practice, to make packages self-documenting and easier to maintain, describe the task in terms of its purpose.  
  
 **TimeOut**  
 Specify the maximum number of seconds the task will run before timing out. A value of 0 indicates an infinite time. The default is 0.  
  
> [!NOTE]  
>  Stored procedures do not time out if they emulate sleep functionality by providing time for connections to be made and transactions to complete that is greater than the number of seconds specified by **TimeOut**. However, stored procedures that execute queries are always subject to the time restriction specified by **TimeOut**.  
  
 **CodePage**  
 Specify the code page to use when translating Unicode values in variables. The default value is the code page of the local computer.  
  
> [!NOTE]  
>  When the Execute SQL task uses an ADO or ODBC connection manager, the **CodePage** property is not available. If your solution requires the use of a code page, use an OLE DB or an ADO.NET connection manager with the Execute SQL task.  
  
 **TypeConversionMode**  
 When you set this property to **Allowed**, the Execute SQL Task will attempt to convert output parameter and query results to the data type of the variable the results are assigned to. This applies to the **Single row** result set type.  
  
 **ResultSet**  
 Specify the result type expected by the SQL statement being run. Choose among **Single row**, **Full result set**, **XML**, or **None**.  
  
 **ConnectionType**  
 Choose the type of connection manager to use to connect to the data source. Available connection types include **OLE DB**, **ODBC**, **ADO**, **ADO.NET** and **SQLMOBILE**.  
  
 **Related Topics:** [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md), [ODBC Connection Manager](../../integration-services/connection-manager/odbc-connection-manager.md), [ADO Connection Manager](../../integration-services/connection-manager/ado-connection-manager.md), [ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md), [SQL Server Compact Edition Connection Manager](../../integration-services/connection-manager/sql-server-compact-edition-connection-manager.md)  
  
 **Connection**  
 Choose the connection from a list of defined connection managers. To create a new connection, select \<**New connection...**>.  
  
 **SQLSourceType**  
 Select the source type of the SQL statement that the task runs.  
  
 Depending on the connection manager type that Execute SQL task uses, you must use specific parameter markers in parameterized SQL statements.    
  
 This property has the options listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Direct input**|Set the source to a Transact-SQL statement. Selecting this value displays the dynamic option, **SQLStatement**.|  
|**File connection**|Select a file that contains a Transact-SQL statement. Setting this option displays the dynamic option, **FileConnection**.|  
|**Variable**|Set the source to a variable that defines the Transact-SQL statement. Selecting this value displays the dynamic option, **SourceVariable**.|  
  
 **QueryIsStoredProcedure**  
 Indicates whether the specified SQL statement to be run is a stored procedure. This property is read/write only if the task uses the ADO connection manager. Otherwise the property is read-only and its value is **false**.  
  
 **BypassPrepare**  
 Indicate whether the SQL statement is prepared.  **true** skips preparation; **false** prepares the SQL statement before running it. This option is available only with OLE DB connections that support preparation.  
  
 **Related Topics:**  [Prepared Execution](../../relational-databases/native-client-odbc-queries/executing-statements/prepared-execution.md)  
  
 **Browse**  
 Locate a file that contains a SQL statement by using the **Open** dialog box. Select a file to copy the contents of the file as a SQL statement into the **SQLStatement** property.  
  
 **Build Query**  
 Create an SQL statement using the **Query Builder** dialog box, a graphical tool used to create queries. This option is available when the **SQLSourceType** option is set to **Direct input**.  
  
 **Parse Query**  
 Validate the syntax of the SQL statement.  
  
### SQLSourceType Dynamic Options  
  
#### SQLSourceType = Direct input  
 **SQLStatement**  
 Type the SQL statement to execute in the option box, or click the browse button (...) to type the SQL statement in the **Enter SQL Query** dialog box, or click **Build Query** to compose the statement using the **Query Builder** dialog box.  
  
 **Related Topics:** [Query Builder](https://msdn.microsoft.com/library/780752c9-6e3c-4f44-aaff-4f4d5e5a45c5)  
  
#### SQLSourceType = File connection  
 **FileConnection**  
 Select an existing File connection manager, or click \<**New connection...**> to create a new connection manager.  
  
 **Related Topics:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)  
  
#### SQLSourceType = Variable  
 **SourceVariable**  
 Select an existing variable, or click \<**New variable...**> to create a new variable.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](https://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)  
 
## Parameter Mapping Page - Execute SQL Task Editor
Use the **Parameter Mapping** page of the **Execute SQL Task Editor** dialog box to map variables to parameters in the SQL statement.  
  
### Options  
 **Variable Name**  
 After you have added a parameter mapping by clicking **Add**, select a system or user-defined variable from the list or click \<**New variable...**> to add a new variable by using the **Add Variable** dialog box.  
  
 **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md)  
  
 **Direction**  
 Select the direction of the parameter. Map each variable to an input parameter, output parameter, or a return code.  
  
 **Data Type**  
 Select the data type of the parameter. The list of available data types is specific to the provider selected in the connection manager used by the task.  
  
 **Parameter Name**  
 Provide a parameter name.  
  
 Depending on the connection manager type that the task uses, you must use numbers or parameter names. Some connection manager types require that the first character of the parameter name is the \@ sign , specific names like \@Param1, or column names as parameter names.  
  
 **Parameter Size**  
 Provide the size of parameters that have variable length, such as strings and binary fields.  
  
 This setting ensures that the provider allocates sufficient space for variable-length parameter values.  
  
 **Add**  
 Click to add a parameter mapping.  
  
 **Remove**  
 Select a parameter mapping in the list and then click **Remove**.  
 
## Result Set Page - Execute SQL Task Editor
Use the **Result Set** page of the **Execute SQL Task Editor** dialog to map the result of the SQL statement to new or existing variables. The options in this dialog box are disabled if **ResultSet** on the General page is set to **None**.  
  
### Options  
 **Result Name**  
 After you have added a result set mapping set by clicking **Add**, provide a name for the result. Depending on the result set type, you must use specific result names.  
  
 If the result set type is **Single row**, you can use either the name of a column returned by the query or the number that represents the position of a column in the column list of a column returned by the query.  
  
 If the result set type is **Full result set** or **XML**, you must use 0 as the result set name.  
 
  
 **Variable Name**  
 Map the result set to a variable by selecting a variable or click \<**New variable...**> to add a new variable by using the **Add Variable** dialog box.  
  
 **Add**  
 Click to add a result set mapping.  
  
 **Remove**  
 Select a result set mapping in the list and then click **Remove**.  
 
## Parameters in the Execute SQL Task
SQL statements and stored procedures frequently use **input** parameters, **output** parameters, and return codes. In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the Execute SQL task supports the **Input**, **Output**, and **ReturnValue** parameter types. You use the **Input** type for input parameters, **Output** for output parameters, and **ReturnValue** for return codes.  
  
> [!NOTE]  
>  You can use parameters in an Execute SQL task only if the data provider supports them.  
  
 Parameters in SQL commands, including queries and stored procedures, are mapped to user-defined variables that are created within the scope of the Execute SQL task, a parent container, or within the scope of the package. The values of variables can be set at design time or populated dynamically at run time. You can also map parameters to system variables. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [System Variables](../../integration-services/system-variables.md).  
  
 However, working with parameters and return codes in an Execute SQL task is more than just knowing what parameter types the task supports and how these parameters will be mapped. There are additional usage requirements and guidelines to successfully use parameters and return codes in the Execute SQL task. The remainder of this topic covers these usage requirements and guidelines:  
  
-   [Using parameters names and markers](#Parameter_names_and_markers)  
  
-   [Using parameters with date and time data types](#Date_and_time_data_types)  
  
-   [Using parameters in WHERE clauses](#WHERE_clauses)  
  
-   [Using parameters with stored procedures](#Stored_procedures)  
  
-   [Getting values of return codes](#Return_codes)    
  
###  <a name="Parameter_names_and_markers"></a> Parameter names and markers  
 Depending on the connection type that the Execute SQL task uses, the syntax of the SQL command uses different parameter markers. For example, the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager type requires that the SQL command uses a parameter marker in the format **\@varParameter**, whereas OLE DB connection type requires the question mark (?) parameter marker.  
  
 The names that you can use as parameter names in the mappings between variables and parameters also vary by connection manager type. For example, the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager type uses a user-defined name with a \@ prefix, whereas the OLE DB connection manager type requires that you use the numeric value of a 0-based ordinal as the parameter name.  
  
 The following table summarizes the requirements for SQL commands for the connection manager types that the Execute SQL task can use.  
  
|Connection type|Parameter marker|Parameter name|Example SQL command|  
|---------------------|----------------------|--------------------|-------------------------|  
|ADO|?|Param1, Param2, ...|SELECT FirstName, LastName, Title FROM Person.Contact WHERE ContactID = ?|  
|[!INCLUDE[vstecado](../../includes/vstecado-md.md)]|\@\<parameter name>|\@\<parameter name>|SELECT FirstName, LastName, Title FROM Person.Contact WHERE ContactID = \@parmContactID|  
|ODBC|?|1, 2, 3, ...|SELECT FirstName, LastName, Title FROM Person.Contact WHERE ContactID = ?|  
|EXCEL and OLE DB|?|0, 1, 2, 3, ...|SELECT FirstName, LastName, Title FROM Person.Contact WHERE ContactID = ?|  
  
#### Use parameters with ADO.NET and ADO Connection Managers  
 [!INCLUDE[vstecado](../../includes/vstecado-md.md)] and ADO connection managers have specific requirements for SQL commands that use parameters:  
  
-   [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection managers require that the SQL command use parameter names as parameter markers. This means that variables can be mapped directly to parameters. For example, the variable `@varName` is mapped to the parameter named `@parName` and provides a value to the parameter `@parName`.  
  
-   ADO connection managers require that the SQL command use question marks (?) as parameter markers. However, you can use any user-defined name, except for integer values, as parameter names.  
  
 To provide values to parameters, variables are mapped to parameter names. Then, the Execute SQL task uses the ordinal value of the parameter name in the parameter list to load values from variables to parameters.  
  
#### Use parameters with EXCEL, ODBC, and OLE DB Connection Managers  
 EXCEL, ODBC, and OLE DB connection managers require that the SQL command use question marks (?) as parameter markers and 0-based or 1-based numeric values as parameter names. If the Execute SQL task uses the ODBC connection manager, the parameter name that maps to the first parameter in the query is named 1; otherwise, the parameter is named 0. For subsequent parameters, the numeric value of the parameter name indicates the parameter in the SQL command that the parameter name maps to. For example, the parameter named 3 maps to the third parameter, which is represented by the third question mark (?) in the SQL command.  
  
 To provide values to parameters, variables are mapped to parameter names and the Execute SQL task uses the ordinal value of the parameter name to load values from variables to parameters.  
  
 Depending on the provider that the connection manager uses, some OLE DB data types may not be supported. For example, the Excel driver recognizes only a limited set of data types. For more information about the behavior of the Jet provider with the Excel driver, see [Excel Source](../../integration-services/data-flow/excel-source.md).  
  
#### Use parameters with OLE DB Connection Managers  
 When the Execute SQL task uses the OLE DB connection manager, the BypassPrepare property of the task is available. You should set this property to **true** if the Execute SQL task uses SQL statements with parameters.  
  
 When you use an OLE DB connection manager, you cannot use parameterized subqueries because the Execute SQL Task cannot derive parameter information through the OLE DB provider. However, you can use an expression to concatenate the parameter values into the query string and to set the SqlStatementSource property of the task.  
  
###  <a name="Date_and_time_data_types"></a> Use parameters with date and time data types  
  
#### Use date and time parameters with ADO.NET and ADO Connection Managers  
 When reading data of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] types, **time** and **datetimeoffset**, an Execute SQL task that uses either an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] or ADO connection manager has the following additional requirements:  
  
-   For **time** data, an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager requires this data to be stored in a parameter whose parameter type is **Input** or **Output**, and whose data type is **string**.  
  
-   For **datetimeoffset** data, an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager requires this data to be stored in one of the following parameters:  
  
    -   A parameter whose parameter type is **Input** and whose data type is **string**.  
  
    -   A parameter whose parameter type is **Output** or **ReturnValue**, and whose data type is **datetimeoffset**, **string**, or **datetime2**. If you select a parameter whose data type is either **string** or **datetime2**, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] converts the data to either string or datetime2.  
  
-   An ADO connection manager requires that either **time** or **datetimeoffset** data be stored in a parameter whose parameter type is **Input** or **Output**, and whose data type is **adVarWchar**.  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types and how they map to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md) and [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
#### Use date and time parameters with OLE DB Connection Managers  
 When using an OLE DB connection manager, an Execute SQL task has specific storage requirements for data of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, **date**, **time**, **datetime**, **datetime2**, and **datetimeoffset**. You must store this data in one of the following parameter types:  
  
-   An input parameter of the NVARCHAR data type.  
  
-   An output parameter of with the appropriate data type, as listed in the following table.  
  
    |**Output** parameter type|Date data type|  
    |-------------------------------|--------------------|  
    |DBDATE|**date**|  
    |DBTIME2|**time**|  
    |DBTIMESTAMP|**datetime**, **datetime2**|  
    |DBTIMESTAMPOFFSET|**datetimeoffset**|  
  
 If the data is not stored in the appropriate input or output parameter, the package fails.  
  
#### Use date and time parameters with ODBC Connection Managers  
 When using an ODBC connection manager, an Execute SQL task has specific storage requirements for data with one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, **date**, **time**, **datetime**, **datetime2**, or **datetimeoffset**. You must store this data in one of the following parameter types:  
  
-   An **input** parameter of the SQL_WVARCHAR data type  
  
-   An **output** parameter with the appropriate data type, as listed in the following table.  
  
    |**Output** parameter type|Date data type|  
    |-------------------------------|--------------------|  
    |SQL_DATE|**date**|  
    |SQL_SS_TIME2|**time**|  
    |SQL_TYPE_TIMESTAMP<br /><br /> -or-<br /><br /> SQL_TIMESTAMP|**datetime**, **datetime2**|  
    |SQL_SS_TIMESTAMPOFFSET|**datetimeoffset**|  
  
 If the data is not stored in the appropriate input or output parameter, the package fails.  
  
###  <a name="WHERE_clauses"></a> Use parameters in WHERE clauses  
 SELECT, INSERT, UPDATE, and DELETE commands frequently include WHERE clauses to specify filters that define the conditions each row in the source tables must meet to qualify for an SQL command. Parameters provide the filter values in the WHERE clauses.  
  
 You can use parameter markers to dynamically provide parameter values. The rules for which parameter markers and parameter names can be used in the SQL statement depend on the type of connection manager that the Execute SQL uses.  
  
 The following table lists examples of the SELECT command by connection manager type. The INSERT, UPDATE, and DELETE statements are similar. The examples use SELECT to return products from the **Product** table in [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)] that have a **ProductID** greater than and less than the values specified by two parameters.  
  
|Connection type|SELECT syntax|  
|---------------------|-------------------|  
|EXCEL, ODBC, and OLEDB|`SELECT* FROM Production.Product WHERE ProductId > ? AND ProductID < ?`|  
|ADO|`SELECT* FROM Production.Product WHERE ProductId > ? AND ProductID < ?`|  
|[!INCLUDE[vstecado](../../includes/vstecado-md.md)]|`SELECT* FROM Production.Product WHERE ProductId > @parmMinProductID AND ProductID < @parmMaxProductID`|  
  
 The examples would require parameters that have the following names:  
  
-   The EXCEL and OLED DB connection managers use the parameter names 0 and 1. The ODBC connection type uses 1 and 2.  
  
-   The ADO connection type could use any two parameter names, such as Param1 and Param2, but the parameters must be mapped by their ordinal position in the parameter list.  
  
-   The [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection type uses the parameter names \@parmMinProductID and \@parmMaxProductID.  
  
###  <a name="Stored_procedures"></a> Use parameters with stored procedures  
 SQL commands that run stored procedures can also use parameter mapping. The rules for how to use parameter markers and parameter names depends on the type of connection manager that the Execute SQL uses, just like the rules for parameterized queries.  
  
 The following table lists examples of the EXEC command by connection manager type. The examples run the **uspGetBillOfMaterials** stored procedure in [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)]. The stored procedure uses the `@StartProductID` and `@CheckDate` **input** parameters.  
  
|Connection type|EXEC syntax|  
|---------------------|-----------------|  
|EXCEL and OLEDB|`EXEC uspGetBillOfMaterials ?, ?`|  
|ODBC|`{call uspGetBillOfMaterials(?, ?)}`<br /><br /> For more information about ODBC call syntax, see the topic, [Procedure Parameters](https://go.microsoft.com/fwlink/?LinkId=89462), in the ODBC Programmer's Reference in the  MSDN Library.|  
|ADO|If IsQueryStoredProcedure is set to **False**, `EXEC uspGetBillOfMaterials ?, ?`<br /><br /> If IsQueryStoredProcedure is set to **True**, `uspGetBillOfMaterials`|  
|[!INCLUDE[vstecado](../../includes/vstecado-md.md)]|If IsQueryStoredProcedure is set to **False**, `EXEC uspGetBillOfMaterials @StartProductID, @CheckDate`<br /><br /> If IsQueryStoredProcedure is set to **True**, `uspGetBillOfMaterials`|  
  
 To use output parameters, the syntax requires that the OUTPUT keyword follow each parameter marker. For example, the following output parameter syntax is correct: `EXEC myStoredProcedure ? OUTPUT`.  
  
 For more information about using input and output parameters with Transact-SQL stored procedures, see [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md).  
 
### Map query parameters to variables
This section describes how to use a parameterized SQL statement in the Execute SQL task and create mappings between variables and the parameters in the SQL statement.  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package you want to work with.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  If the package does not already include an Execute SQL task, add one to the control flow of the package. For more information, see [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
5.  Double-click the Execute SQL task.  
  
6.  Provide a parameterized SQL command in one of the following ways:  
  
    -   Use direct input and type the SQL command in the SQLStatement property.  
  
    -   Use direct input, click **Build Query**, and then create an SQL command using the graphical tools that the Query Builder provides.  
  
    -   Use a file connection and then reference the file that contains the SQL command.  
  
    -   Use a variable and then reference the variable that contains the SQL command.  
  
     The parameter markers that you use in parameterized SQL statements depend on the connection type that the Execute SQL task uses.  
  
    |Connection type|Parameter marker|  
    |---------------------|----------------------|  
    |ADO|?|  
    |ADO.NET and SQLMOBILE|\@\<parameter name>|  
    |ODBC|?|  
    |EXCEL and OLE DB|?|  
  
     The following table lists examples of the SELECT command by connection manager type. Parameters provide the filter values in the WHERE clauses. The examples use SELECT to return products from the **Product** table in [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)] that have a **ProductID** greater than and less than the values specified by two parameters.  
  
    |Connection type|SELECT syntax|  
    |---------------------|-------------------|  
    |EXCEL, ODBC, and OLEDB|`SELECT* FROM Production.Product WHERE ProductId > ? AND ProductID < ?`|  
    |ADO|`SELECT* FROM Production.Product WHERE ProductId > ? AND ProductID < ?`|  
    |[!INCLUDE[vstecado](../../includes/vstecado-md.md)]|`SELECT* FROM Production.Product WHERE ProductId > @parmMinProductID AND ProductID < @parmMaxProductID`|  
   
7.  Click **Parameter Mapping**.  
  
8.  To add a parameter mapping, click **Add**.  
  
9. Provide a name in the **Parameter Name** box.  
  
     The parameter names that you use depend on the connection type that the Execute SQL task uses.  
  
    |Connection type|Parameter name|  
    |---------------------|--------------------|  
    |ADO|Param1, Param2, ...|  
    |ADO.NET and SQLMOBILE|\@\<parameter name>|  
    |ODBC|1, 2, 3, ...|  
    |EXCEL and OLE DB|0, 1, 2, 3, ...|  
  
10. From the **Variable Name** list, select a variable. For more information, see [Add, Delete, Change Scope of User-Defined Variable in a Package](https://msdn.microsoft.com/library/cbf40c7f-3c8a-48cd-aefa-8b37faf8b40e).  
  
11. In the **Direction** list, specify if the parameter is an input, an output, or a return value.  
  
12. In the **Data Type** list, set the data type of the parameter.  
  
    > [!IMPORTANT]  
    >  The data type of the parameter must be compatible with the data type of the variable.  
  
13. Repeat steps 8 through 11 for each parameter in the SQL statement.  
  
    > [!IMPORTANT]  
    >  The order of parameter mappings must be the same as the order in which the parameters appear in the SQL statement.  
  
14. Click **OK**.  

##  <a name="Return_codes"></a> Get the values of return codes  
 A stored procedure can return an integer value, called a return code, to indicate the execution status of a procedure. To implement return codes in the Execute SQL task, you use parameters of the **ReturnValue** type.  
  
 The following table lists by connection type some examples of EXEC commands that implement return codes. All examples use an **input** parameter. The rules for how to use parameter markers and parameter names are the same for all parameter types-**Input**, **Output**, and **ReturnValue**.  
  
 Some syntax does not support parameter literals. In that case, you must provide the parameter value by using a variable.  
  
|Connection type|EXEC syntax|  
|---------------------|-----------------|  
|EXCEL and OLEDB|`EXEC ? = myStoredProcedure 1`|  
|ODBC|`{? = call myStoredProcedure(1)}`<br /><br /> For more information about ODBC call syntax, see the topic, [Procedure Parameters](https://go.microsoft.com/fwlink/?LinkId=89462), in the ODBC Programmer's Reference in the  MSDN Library.|  
|ADO|If IsQueryStoreProcedure is set to **False**, `EXEC ? = myStoredProcedure 1`<br /><br /> If IsQueryStoreProcedure is set to **True**, `myStoredProcedure`|  
|[!INCLUDE[vstecado](../../includes/vstecado-md.md)]|Set IsQueryStoreProcedure is set to **True**.<br /><br /> `myStoredProcedure`|  
  
 In the syntax shown in the previous table, the Execute SQL task uses the **Direct Input** source type to run the stored procedure. The Execute SQL task can also use the **File Connection** source type to run a stored procedure. Regardless of whether the Execute SQL task uses the **Direct Input** or **File Connection** source type, use a parameter of the **ReturnValue** type to implement the return code.
  
 For more information about using return codes with Transact-SQL stored procedures, see [RETURN &#40;Transact-SQL&#41;](../../t-sql/language-elements/return-transact-sql.md).  
  
## Result Sets in the Execute SQL Task
 In an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package, whether a result set is returned to the Execute SQL task depends on the type of SQL command that the task uses. For example, a SELECT statement typically returns a result set, but an INSERT statement does not.  
  
 What the result set contains also varies by SQL command. For example, the result set from a SELECT statement can contain zero rows, one row, or many rows. However, the result set from a SELECT statement that returns a count or a sum contains only a single row.  
  
 Working with result sets in an Execute SQL task is more than just knowing whether the SQL command returns a result set and what that result set contains. There are additional usage requirements and guidelines to successfully use result sets in the Execute SQL task. The remainder of this topic covers these usage requirements and guidelines:  
  
-   [Specifying a Result Set Type](#Result_set_type)  
  
-   [Populating a variable with a result set](#Populate_variable_with_result_set)  
  
###  <a name="Result_set_type"></a> Specify a result set type  
 The Execute SQL task supports the following types of result sets:  
  
-   The **None** result set is used when the query returns no results. For example, this result set is used for queries that add, change, and delete records in a table.  
  
-   The **Single row** result set is used when the query returns only one row. For example, this result set is used for a SELECT statement that returns a count or a sum.  
  
-   The **Full result set** result set is used when the query returns multiple rows. For example, this result set is used for a SELECT statement that retrieves all the rows in a table.  
  
-   The **XML** result set is used when the query returns a result set in an XML format. For example, this result set is used for a SELECT statement that includes a FOR XML clause.  
  
 If the Execute SQL task uses the **Full result set** result set and the query returns multiple rowsets, the task returns only the first rowset. If this rowset generates an error, the task reports the error. If other rowsets generate errors, the task does not report them.  
  
###  <a name="Populate_variable_with_result_set"></a> Populate a variable with a result set  
 You can bind the result set that a query returns to a user-defined variable, if the result set type is a single row, a rowset, or XML.  
  
 If the result set type is **Single row**, you can bind a column in the return result to a variable by using the column name as the result set name, or you can use the ordinal position of the column in the column list as the result set name. For example, the result set name for the query `SELECT Color FROM Production.Product WHERE ProductID = ?` could be **Color** or **0**. If the query returns multiple columns and you want to access the values in all columns, you must bind each column to a different variable. If you map columns to variables using numbers as result set names, the numbers reflect the order in which the columns appear in the column list of the query. For example, in the query `SELECT Color, ListPrice, FROM Production.Product WHERE ProductID = ?`, you use 0 for the **Color** column and 1 for the **ListPrice** column. The ability to use a column name as the name of a result set depends on the provider that the task is configured to use. Not all providers make column names available.  
  
 Some queries that return a single value may not include column names. For example, the statement `SELECT COUNT (*) FROM Production.Product` returns no column name. You can access the return result using the ordinal position, 0, as the result name. To access the return result by column name, the query must include an AS \<alias name> clause to provide a column name. The statement `SELECT COUNT (*)AS CountOfProduct FROM Production.Product`, provides the **CountOfProduct** column. You can then access the return result column using the **CountOfProduct** column name or the ordinal position, 0.  
  
 If the result set type is **Full result set** or **XML**, you must use 0 as the result set name.  
  
 When you map a variable to a result set with the **Single row** result set type, the variable must have a data type that is compatible with the data type of the column that the result set contains. For example, a result set that contains a column with a **String** data type cannot map to a variable with a numeric data type. When you set the **TypeConversionMode** property to **Allowed**, the Execute SQL Task will attempt to convert output parameter and query results to the data type of the variable the results are assigned to.  
  
 An XML result set can map only to a variable with the **String** or **Object** data type. If the variable has the **String** data type, the Execute SQL task returns a string and the XML source can consume the XML data. If the variable has the **Object** data type, the Execute SQL task returns a Document Object Model (DOM) object.  
  
 A **Full result set** must map to a variable of the **Object** data type. The return result is a rowset object. You can use a Foreach Loop container to extract the table row values that are stored in the Object variable into package variables, and then use a Script Task to write the data stored in packages variables to a file. For a demonstration on how to do this using a Foreach Loop container and a Script Task, see the CodePlex sample, [Execute SQL Parameters and Result Sets](https://go.microsoft.com/fwlink/?LinkId=157863), on msftisprodsamples.codeplex.com.  
  
 The following table summarizes the data types of variables that can be mapped to result sets.  
  
|Result set type|Data type of variable|Type of object|  
|---------------------|---------------------------|--------------------|  
|Single row|Any type that is compatible with the type column in the result set.|Not applicable|  
|Full result set|**Object**|If the task uses a native connection manager, including the ADO, OLE DB, Excel, and ODBC connection managers, the returned object is an ADO **Recordset**.<br /><br /> If the task uses a managed connection manager, such as the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager, then the returned object is a **System.Data.DataSet**.<br /><br /> You can use a Script task to access the **System.Data.DataSet** object, as shown in the following example.<br /><br /> `Dim dt As Data.DataTable`<br /><br /> `Dim ds As Data.DataSet = CType(Dts.Variables("Recordset").Value, DataSet) dt = ds.Tables(0)`|  
|XML|**String**|**String**|  
|XML|**Object**|If the task uses a native connection manager, including the ADO, OLE DB, Excel, and ODBC connection managers, the returned object is an **MSXML6.IXMLDOMDocument**.<br /><br /> If the task uses a managed connection manager, such as the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager, the returned object is a **System.Xml.XmlDocument**.|  
  
 The variable can be defined in the scope of the Execute SQL task or the package. If the variable has package scope, the result set is available to other tasks and containers within the package, and is available to any packages run by the Execute Package or Execute DTS 2000 Package tasks.  
  
 When you map a variable to a **Single row** result set, non-string values that the SQL statement returns are converted to strings when the following conditions are met:  
  
-   The **TypeConversionMode** property is set to true. You set the property value in the Properties window or by using the **Execute SQL Task Editor**.  
  
-   The conversion will not result in data truncation.  
  
## Map result sets to variables in an Execute SQL Task
This section describes how to create a mapping between a result set and a variable in an Execute SQL task. Mapping a result set to a variable makes the result set available to other elements in the package. For example, a script in a Script task can read the variable and then use the values from the result set or an XML source can consume the result set stored in a variable. If the result set is generated by a parent package, the result set can be made available to a child package called by an Execute Package task by mapping the result set to a variable in the parent package, and then creating a parent package variable configuration in the child package to store the parent variable value.  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In **Solution Explorer**, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  If the package does not already include an Execute SQL task, add one to the control flow of the package. For more information, see [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
5.  Double-click the Execute SQL task.  
  
6.  In the **Execute SQL Task Editor** dialog box, on the **General** page, select the **Single row**, **Full result set**, or **XML** result set type.  

7.  Click **Result Set**.  
  
8.  To add a result set mapping, click **Add**.  
  
9. From the **Variables Name** list, select a variable or create a new variable. For more information, see [Add, Delete, Change Scope of User-Defined Variable in a Package](https://msdn.microsoft.com/library/cbf40c7f-3c8a-48cd-aefa-8b37faf8b40e).  
  
10. In the **Result Name** list, optionally, modify the name of the result set.  
  
     In general, you can use the column name as the result set name, or you can use the ordinal position of the column in the column list as the result set. The ability to use a column name as the result set name depends on the provider that the task is configured to use. Not all providers make column names available.  
  
11. Click **OK**.  

## Troubleshoot the Execute SQL task  
 You can log the calls that the Execute SQL task makes to external data providers. You can use this logging capability to troubleshoot the SQL commands that the Execute SQL task runs. To log the calls that the Execute SQL task makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
 Sometimes a SQL command or stored procedure returns multiple result sets. These result sets include not only rowsets that are the result of **SELECT** queries, but single values that are the result of errors of **RAISERROR** or **PRINT** statements. Whether the task ignores errors in result sets that occur after the first result set depends on the type of connection manager that is used:  
  
-   When you use OLE DB and ADO connection managers, the task ignores the result sets that occur after the first result set. Therefore, with these connection managers, the task ignores an error returned by an SQL command or a stored procedure when the error is not part of the first result set.  
  
-   When you use ODBC and ADO.NET connection managers, the task does not ignore result sets that occur after the first result set. With these connection managers, the task will fail with an error when a result set other than the first result set contains an error.  
  
### Custom Log Entries  
 The following table describes the custom log entry for the Execute SQL task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**ExecuteSQLExecutingQuery**|Provides information about the execution phases of the SQL statement. Log entries are written when the task acquires connection to the database, when the task starts to prepare the SQL statement, and after the execution of the SQL statement is completed. The log entry for the prepare phase includes the SQL statement that the task uses.|  

