---
title: "OLE DB Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.oledbsource.f1"
  - "sql13.dts.designer.oledbsourceadapter.connection.f1"
  - "sql13.dts.designer.oledbsourceadapter.columns.f1"
  - "sql13.dts.designer.oledbsourceadapter.errorhandling.f1"
helpviewer_keywords: 
  - "sources [Integration Services], OLE DB"
  - "OLE DB source [Integration Services]"
ms.assetid: f87cc5f6-b078-40f3-9d87-7a65e13e4c86
author: janinezhang
ms.author: janinez
manager: craigg
---
# OLE DB Source

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The OLE DB source extracts data from a variety of OLE DB-compliant relational databases by using a database table, a view, or an SQL command. For example, the OLE DB source can extract data from tables in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Access or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
> [!NOTE]  
>  If the data source is [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Excel 2007, the data source requires a different connection manager than earlier versions of Excel. For more information, see [Connect to an Excel Workbook](../../integration-services/connection-manager/connect-to-an-excel-workbook.md).  
  
 The OLE DB source provides four different data access modes for extracting data:  
  
-   A table or view.  
  
-   A table or view specified in a variable.  
  
-   The results of an SQL statement. The query can be a parameterized query.  
  
-   The results of an SQL statement stored in a variable.  
  
> [!NOTE]  
>  When you use an SQL statement to invoke a stored procedure that returns results from a temporary table, use the WITH RESULT SETS option to define metadata for the result set.  
  
 If you use a parameterized query, you can map variables to parameters to specify the values for individual parameters in the SQL statements.  
  
 This source uses an OLE DB connection manager to connect to a data source, and the connection manager specifies the OLE DB provider to use. For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
 An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project also provides the data source object from which you can create an OLE DB connection manager, making data sources and data source views available to the OLE DB source.  
  
 Depending on the OLE DB provider, some limitations apply to the OLE DB source:  
  
-   The [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB provider for Oracle does not support the Oracle data types BLOB, CLOB, NCLOB, BFILE, OR UROWID, and the OLE DB source cannot extract data from tables that contain columns with these data types.  
  
-   The IBM OLE DB DB2 provider and [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB DB2 provider do not support using an SQL command that calls a stored procedure. When this kind of command is used, the OLE DB source cannot create the column metadata and, as a result, the data flow components that follow the OLE DB source in the data flow have no column data available and the execution of the data flow fails.  
  
 The OLE DB source has one regular output and one error output.  
  
## Using Parameterized SQL Statements  
 The OLE DB source can use an SQL statement to extract data. The statement can be a SELECT or an EXEC statement.  
  
 The OLE DB source uses an OLE DB connection manager to connect to the data source from which it extracts data. Depending on the provider that the OLE DB connection manager uses and the Relational Database Management System (RDBMS) that the connection manager connects to, different rules apply to the naming and listing of parameters. If the parameter names are returned from the RDBMS, you can use parameter names to map parameters in a parameter list to parameters in an SQL statement; otherwise, the parameters are mapped to the parameter in the SQL statement by their ordinal position in the parameter list. The types of parameter names that are supported vary by provider. For example, some providers require that you use the variable or column names, whereas some providers require that you use symbolic names such as 0 or Param0. You should see the provider-specific documentation for information about the parameter names to use in SQL statements.  
  
 When you are use an OLE DB connection manager, you cannot use parameterized subqueries, because the OLE DB source cannot derive parameter information through the OLE DB provider. However, you can use an expression to concatenate the parameter values into the query string and to set the SqlCommand property of the source.In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, you configure an OLE DB source by using the **OLE DB Source Editor** dialog box and map the parameters to variables in the **Set Query Parameter** dialog box.  
  
### Specifying Parameters by Using Ordinal Positions  
 If no parameter names are returned, the order in which the parameters are listed in the **Parameters** list in the **Set Query Parameter** dialog box governs which parameter marker they are mapped to at run time. The first parameter in the list maps to the first ? in the SQL statement, the second to the second ?, and so on.  
  
 The following SQL statement selects rows from the **Product** table in the [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)] database. The first parameter in the **Mappings** list maps to the first parameter to the **Color** column, the second parameter to the **Size** column.  
  
 `SELECT * FROM Production.Product WHERE Color = ? AND Size = ?`  
  
 The parameter names have no effect. For example, if a parameter is named the same as the column to which it applies, but not put in the correct ordinal position in the **Parameters** list, the parameter mapping that occurs at run time will use the ordinal position of the parameter, not the parameter name.  
  
 The EXEC command typically requires that you use the names of the variables that provide parameter values in the procedure as parameter names.  
  
### Specifying Parameters by Using Names  
 If the actual parameter names are returned from the RDBMS, the parameters used by a SELECT and EXEC statement are mapped by name. The parameter names must match the names that the stored procedure, run by the SELECT statement or the EXEC statement, expects.  
  
 The following SQL statement runs the **uspGetWhereUsedProductID** stored procedure, available in the [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)] database.  
  
 `EXEC uspGetWhereUsedProductID ?, ?`  
  
 The stored procedure expects the variables, `@StartProductID` and `@CheckDate`, to provide parameter values. The order in which the parameters appear in the **Mappings** list is irrelevant. The only requirement is that the parameter names match the variable names in the stored procedure, including the \@ sign.  
  
### Mapping Parameters to Variables  
 The parameters are mapped to variables that provide the parameter values at run time. The variables are typically user-defined variables, although you can also use the system variables that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides. If you use user-defined variables, make sure that you set the data type to a type that is compatible with the data type of the column that the mapped parameter references. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md).  
  
## Troubleshooting the OLE DB Source  
 You can log the calls that the OLE DB source makes to external data providers. You can use this logging capability to troubleshoot the loading of data from external data sources that the OLE DB source performs. To log the calls that the OLE DB source makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
## Configuring the OLE DB Source  
 You can set properties programmatically or through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [OLE DB Custom Properties](../../integration-services/data-flow/ole-db-custom-properties.md)  
  
## Related Tasks  
  
-   [Extract Data by Using the OLE DB Source](../../integration-services/data-flow/extract-data-by-using-the-ole-db-source.md)  
  
-   [Map Query Parameters to Variables in a Data Flow Component](../../integration-services/data-flow/map-query-parameters-to-variables-in-a-data-flow-component.md)  
  
-   [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
-   [Sort Data for the Merge and Merge Join Transformations](../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md)  
  
## Related Content  
 Wiki article, [SSIS with Oracle Connectors](https://go.microsoft.com/fwlink/?LinkId=220670), on social.technet.microsoft.com.  
  
## OLE DB Source Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **OLE DB Source Editor** dialog box to select the OLE DB connection manager for the source. This page also lets you select a table or view from the database.  
  
> [!NOTE]  
>  To load data from a data source that uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Excel 2007, use an OLE DB source. You cannot use an Excel source to load data from an Excel 2007 data source. For more information, see [Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md).  
>   
>  To load data from a data source that uses [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Excel 2003 or earlier, use an Excel source. For more information, see [Excel Source Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/excel-source-editor-connection-manager-page.md).  
  
> [!NOTE]  
>  The **CommandTimeout** property of the OLE DB source is not available in the **OLE DB Source Editor**, but can be set by using the **Advanced Editor**. For more information on this property, see the Excel Source section of [OLE DB Custom Properties](../../integration-services/data-flow/ole-db-custom-properties.md).  
  
### Open the OLE DB Source Editor (Connection Manager Page)  
  
1.  Add the OLE DB source to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the source component and when click **Edit**.  
  
3.  Click **Connection Manager**.  
  
### Static Options  
 **OLE DB connection manager**  
 Select an existing connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Data access mode**  
 Specify the method for selecting data from the source.  
  
|Option|Description|  
|------------|-----------------|  
|Table or view|Retrieve data from a table or view in the OLE DB data source.|  
|Table name or view name variable|Specify the table or view name in a variable.<br /><br /> **Related information:** [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787)|  
|SQL command|Retrieve data from the OLE DB data source by using a SQL query.|  
|SQL command from variable|Specify the SQL query text in a variable.|  
  
 **Preview**  
 Preview results by using the **Data View** dialog box. **Preview** can display up to 200 rows.  
  
> [!NOTE]  
>  When you preview data, columns with a CLR user-defined type do not contain data. Instead the values \<value too big to display> or System.Byte[] display. The former displays when the data source is accessed using the SQL OLE DB provider, the latter when using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client provider.  
  
### Data Access Mode Dynamic Options  
  
#### Data access mode = Table or view  
 **Name of the table or the view**  
 Select the name of the table or view from a list of those available in the data source.  
  
#### Data access mode = Table name or view name variable  
 **Variable name**  
 Select the variable that contains the name of the table or view.  
  
#### Data access mode = SQL command  
 **SQL command text**  
 Enter the text of a SQL query, build the query by clicking **Build Query**, or locate the file that contains the query text by clicking **Browse**.  
  
 **Parameters**  
 If you have entered a parameterized query by using ? as a parameter placeholder in the query text, use the **Set Query Parameters** dialog box to map query input parameters to package variables.  
  
 **Build query**  
 Use the **Query Builder** dialog box to construct the SQL query visually.  
  
 **Browse**  
 Use the **Open** dialog box to locate the file that contains the text of the SQL query.  
  
 **Parse query**  
 Verify the syntax of the query text.  
  
#### Data access mode = SQL command from variable  
 **Variable name**  
 Select the variable that contains the text of the SQL query.  
  
## OLE DB Source Editor (Columns Page)
  Use the **Columns** page of the **OLE DB Source Editor** dialog box to map an output column to each external (source) column.  
  
### Options  
 **Available External Columns**  
 View the list of available external columns in the data source. You cannot use this table to add or delete columns.  
  
 **External Column**  
 View external (source) columns in the order in which you will see them when configuring components that consume data from this source. You can change this order by first clearing the selected columns in the table, and then selecting external columns from the list in a different order.  
  
 **Output Column**  
 Provide a unique name for each output column. The default is the name of the selected external (source) column; however, you can choose any unique, descriptive name. The name provided will be displayed within the SSIS Designer.  
  
## OLE DB Source Editor (Error Output Page)
  Use the **Error Output** page of the **OLE DB Source Editor** dialog box to select error handling options and to set properties on error output columns.  
  
### Options  
 **Input/Output**  
 View the name of the data source.  
  
 **Column**  
 View the external (source) columns that you selected on the **Connection Manager** page of the **OLE DB Source Editor**dialog box.  
  
 **Error**  
 Specify what should happen when an error occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Related Topics:** [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
 **Truncation**  
 Specify what should happen when a truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Description**  
 View the description of the error.  
  
 **Set this value to selected cells**  
 Specify what should happen to all the selected cells when an error or truncation occurs: ignore the failure, redirect the row, or fail the component.  
  
 **Apply**  
 Apply the error handling option to the selected cells.  
  
## See Also  
 [OLE DB Destination](../../integration-services/data-flow/ole-db-destination.md)   
 [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md)   
 [Data Flow](../../integration-services/data-flow/data-flow.md)  
  
  
