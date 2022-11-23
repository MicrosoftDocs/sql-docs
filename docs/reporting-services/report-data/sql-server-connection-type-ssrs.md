---
title: "SQL Server Connection Type | Microsoft Docs"
description: Learn about the SQL Server connection type and including data from a SQL Server database in your report.
ms.date: 08/17/2018
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
ms.assetid: 957e7091-e08f-48d2-9506-872227ae8b20
author: maggiesMSFT
ms.author: maggies
---
# SQL Server Connection Type (SSRS)
  To include data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database in your report, you must have a dataset that is based on a report data source of type [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This built-in data source type is based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data extension. Use this data source type to connect to and retrieve data from the current version and earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
 This data extension supports multivalue parameters, server aggregates, and credentials managed separately from the connection string.  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
##  <a name="Connection"></a> Connection String  
 When you connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, you are connecting to the database object in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a server. The database might have multiple schemas that have multiple tables, views, and stored procedures. You specify the database object to use in the query designer. If you do not specify a database in the connection string, you connect to the default database that the database administrator assigned to you.  
  
 Contact your database administrator for connection information and for the credentials to use to connect to the data source. The following connection string example specifies a sample database on the local client:  
  
```  
Data Source=<server>;Initial Catalog=AdventureWorks  
```  
  
 For more information about connection string examples, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
##  <a name="Credentials"></a> Credentials  
 Credentials are required to run queries, to preview the report locally, and to preview the report from the report server.  
  
 After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid.  
  
 From a report authoring client, the following options are available to specify credentials:  
  
-   Current Windows user (also known as integrated security).  
  
-   Use a stored user name and password.  
  
-   Prompt the user for credentials. This option supports Windows integrated security only.  
  
-   No credentials are required. To use this option, you must have the unattended execution account configured on the report server. For more information, see [Configure the Unattended Execution Account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md). 
  
 For more information, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md) or [Specify Credential and Connection Information for Report Data Sources](specify-credential-and-connection-information-for-report-data-sources.md).  
  
  
##  <a name="Query"></a> Queries  
 A query specifies which data to retrieve for a report dataset. The columns in the result set for a query populate the field collection for a dataset. A report processes only the first result set that a query retrieves.  
  
 By default, if you create a new query or open an existing query that can be represented in the graphical query designer, the relational query designer is available. You can specify a query in the following ways:  
  
-   Build a query interactively. Use the relational query designer that displays a hierarchical view of tables, views, stored procedures, and other database items, organized by database schema. Select columns from tables or views, or specify stored procedures or table-valued functions. Limit the number of rows of data to retrieve by specifying filter criteria. Customize the filter when the report runs by setting the parameter option.  
  
-   Type or paste a query. Use the text-based query designer to enter [!INCLUDE[tsql](../../includes/tsql-md.md)] text directly, to paste query text from another source, to enter complex queries that cannot be built by using the relational query designer, or to enter query-based expressions.  
  
-   Import an existing query from a file or report. Use the **Import** query button from either query designer to browse to a .sql file or .rdl file and import a query.  
  
 For more information, see [Relational Query Designer User Interface &#40;Report Builder&#41;](../../reporting-services/report-data/relational-query-designer-user-interface-report-builder.md) and [Text-based Query Designer User Interface &#40;Report Builder&#41;](../../reporting-services/report-data/text-based-query-designer-user-interface-report-builder.md).  
  
 The following query modes are supported:  
  
-   [Text](#QueryText) Type in [!INCLUDE[tsql](../../includes/tsql-md.md)] commands.  
  
-   [Stored Procedure](#QueryStoredProcedure) Choose from a list of stored procedures.  
  
###  <a name="QueryText"></a> Using Query Type Text  
 In the text-based query designer, you can type [!INCLUDE[tsql](../../includes/tsql-md.md)] commands to define the data in a dataset. For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] query selects the names of all employees who are marketing assistants:  
  
```  
SELECT  
  HumanResources.Employee.BusinessEntityID  
  ,HumanResources.Employee.JobTitle  
  ,Person.Person.FirstName  
  ,Person.Person.LastName  
FROM  
  Person.Person  
  INNER JOIN HumanResources.Employee  
    ON Person.Person.BusinessEntityID = HumanResources.Employee.BusinessEntityID  
WHERE HumanResources.Employee.JobTitle = 'Marketing Assistant'   
```  
  
 Click the **Run** button (**!**) on the toolbar to run the query and display a result set.  
  
 To parameterize this query, add a query parameter. For example, change the WHERE clause to the following:  
  
 `WHERE HumanResources.Employee.JobTitle = (@JobTitle)`  
  
 When you run the query, report parameters that correspond to query parameters are automatically created. For more information, see [Query Parameters](#Parameters) later in this topic.  
  
  
###  <a name="QueryStoredProcedure"></a> Using Query Type StoredProcedure  
 You can specify a stored procedure for a dataset query in one of the following ways:  
  
-   In the **Dataset Properties** dialog box, set the **Stored Procedure** option. Choose from the drop-down list of stored procedures and table-valued functions.  
  
-   In the relational query designer, in the Database view pane, select a stored procedure or table-valued function.  
  
-   In the text-based query designer, select **StoredProcedure** from the toolbar.  
  
 After you select a stored procedure or table-valued function, you can run the query. You will be prompted for input parameter values. When you run the query, report parameters that correspond to input parameters are automatically created. For more information, see [Query Parameters](#Parameters) later in this topic.  
  
 Only the first result set that is retrieved for a stored procedure is supported. If a stored procedure returns multiple result sets, only the first one is used.  
  
 If a stored procedure has a parameter that has a default value, you can access that value by using the DEFAULT keyword as a value for the parameter. If the query parameter is linked to a report parameter, the user can type or select the word DEFAULT in the input box for the report parameter.  
  
 For more information, see [Stored Procedures (Database Engine](../../relational-databases/stored-procedures/stored-procedures-database-engine.md)).  
  
  
##  <a name="Parameters"></a> Parameters  
 When query text contains query variables or stored procedures that have input parameters, the corresponding query parameters for the dataset and report parameters for the report are automatically generated. The query text must not include the DECLARE statement for each query variable.  
  
 For example, the following SQL query creates a report parameter named **EmpID**:  
  
```  
SELECT FirstName, LastName FROM HumanResources.Employee E INNER JOIN  
       Person.Contact C ON  E.ContactID=C.ContactID   
WHERE EmployeeID = (@EmpID)  
```  
  
 Report parameters are created with default property values that you might need to modify. For example:  
  
-   By default, each report parameter is data type **Text**. If the underlying data is a different data type, you must change the parameter data type.  
  
-   If you select the option for multivalued parameters, you must manually change the query to test whether values are part of a set by using the **IN** operator, for example, `WHERE EmployeeID IN (@EmpID)`.  
  
 For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
  
##  <a name="Remarks"></a> Remarks  
 You can also retrieve data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using an OLE DB or ODBC data source type. For more information, see [OLE DB Connection Type &#40;SSRS&#41;](../../reporting-services/report-data/ole-db-connection-type-ssrs.md) or [ODBC Connection Type &#40;SSRS&#41;](../../reporting-services/report-data/odbc-connection-type-ssrs.md).  
  
###### Platform and Version Information  
 For more information about platform and version support, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  
  
  
##  <a name="HowTo"></a> How-To Topics  
 This section contains step-by-step instructions for working with data connections, data sources, and datasets.  
  
 [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)  
  
 [Add a Filter to a Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-a-filter-to-a-dataset-report-builder-and-ssrs.md)  
  
  
##  <a name="Related"></a> Related Sections  
 These sections of the documentation provide in-depth conceptual information about report data, and procedural information about how to define, customize, and use parts of a report that are related to data.  
  
 [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)  
 Provides an overview of accessing data for your report.  
  
 [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)  
 Provides information about data connections and data sources.  
  
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
 Provides information about embedded and shared datasets.  
  
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md)  
 Provides information about the dataset field collection generated by the query.  
  
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  
 Provides in-depth information about platform and version support for each data extension.  
  
  
## See Also  
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)  
  
  
