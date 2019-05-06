---
title: "SQL Server Parallel Data Warehouse Connection Type (SSRS) | Microsoft Docs"
ms.date: 05/30/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
ms.assetid: 3925fd3d-2aa1-4768-96ad-cfc2c0ba9283
author: maggiesMSFT
ms.author: maggies
---

# SQL Server Parallel Data Warehouse Connection Type (SSRS)

  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDWCurrentFull](../../includes/ssdwcurrentfull-md.md)] is a scalable data warehouse appliance that delivers performance and scalability through massively parallel processing. [!INCLUDE[ssDW](../../includes/ssdw-md.md)] uses SQL Server databases for distributed processing and data storage.  
  
 The appliance partitions large database tables across multiple physical nodes, with each node running its own instance of SQL Server. When a report connects to [!INCLUDE[ssDW](../../includes/ssdw-md.md)] to retrieve report data, it connects to the control node, which manages query processing, in the [!INCLUDE[ssDW](../../includes/ssdw-md.md)] appliance. After the connection is made, there are no differences between working with an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is and is not within a [!INCLUDE[ssDW](../../includes/ssdw-md.md)] environment.  
  
 To include data from [!INCLUDE[ssDW](../../includes/ssdw-md.md)] in your report, you must have a dataset that is based on a report data source of type [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Parallel Data Warehouse. This built-in data source type is based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Parallel Data Warehouse data extension. Use this data source type to connect to and retrieve data from [!INCLUDE[ssDW](../../includes/ssdw-md.md)].  
  
 This data extension supports multivalued parameters, server aggregates, and credentials managed separately from the connection string.  
  
 For more information, see the Web site [SQL Server 2008 R2 Parallel Data Warehouse](https://go.microsoft.com/fwlink/?LinkId=150895).  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
##  <a name="Connection"></a> Connection String  
 When you connect to [!INCLUDE[ssDW](../../includes/ssdw-md.md)], you are connecting to a database object within a [!INCLUDE[ssDW](../../includes/ssdw-md.md)] appliance. You specify the database object to use in the query designer. If you do not specify a database in the connection string, you connect to the default database that the administrator assigned to you. Contact your database administrator for connection information and for the credentials to use to connect to the data source. The following connection string example specifies the sample database, **CustomerSales**, in the [!INCLUDE[ssDW](../../includes/ssdw-md.md)] appliance:  
  
```  
HOST=<IP address>; database= CustomerSales; port=<port>  
```  
  
 In addition, you use the **Data Sources Properties** dialog box to provide credentials such as user name and password, The `User Id` and `Password` options are automatically appended to the connection string, you do not need to type them as part of the connection string. The user interface also provides options to specify the IP address of the control node in the [!INCLUDE[ssDW](../../includes/ssdw-md.md)] appliance and the port number. By default, the port is 17000. The port is configurable by an administrator and your connection string might use a different port number.  
  
 For more information about connection string examples, see [Data Connections, Data Sources, and Connection Strings in Report Builder](https://msdn.microsoft.com/library/7e103637-4371-43d7-821c-d269c2cc1b34).  
  
##  <a name="Credentials"></a> Credentials  
 [!INCLUDE[ssDW](../../includes/ssdw-md.md)] provides its own security technology to implement and store user names and passwords. You cannot use Windows Authentication. If you attempt to connect to [!INCLUDE[ssDW](../../includes/ssdw-md.md)] using Windows Authentication an error occurs.  
  
 Credentials must be sufficient to access the database. Depending on your query, you might need other permissions, such as sufficient permissions to access tables and views. The owner of the external data source must configure credentials that are sufficient to provide read-only access to the database objects that you need.  
  
 From a report authoring client, the following options are available to specify credentials:  
  
-   Use a stored user name and password. To negotiate the double hop that occurs when the database that contains the report data is different than the report server, select options to use credentials as Windows credentials. You can also chose to impersonate the authenticated user after connecting to the data source.  
  
-   No credentials are required. To use this option, you must have the unattended execution account configured on the report server. For more information, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md) in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) in on msdn.microsoft.com.  
  
 For more information, see [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md) or [Specify Credentials in Report Builder](https://msdn.microsoft.com/library/7412ce68-aece-41c0-8c37-76a0e54b6b53).  
  
  
##  <a name="Query"></a> Queries  
 A query specifies which data to retrieve for a report dataset.  
  
 The columns in the result set for a query populate the field collection for a dataset. If the query returns multiple result sets, the report processes only the first result set that a query retrieves. By default, if you create a new query or open an existing query that can be represented in the graphical query designer, the relational query designer is available. You can specify a query in the following ways:  
  
-   Build a query interactively. Use the relational query designer that displays a hierarchical view of tables, views, and other database items, organized by database schema. Select columns from tables or views. Limit the number of rows of data to retrieve by specifying filter criteria, grouping, and aggregates. Customize the filter when the report runs by setting the parameter option.  
  
-   Type or paste a query. Use the text-based query designer to enter [!INCLUDE[DWsql](../../includes/dwsql-md.md)] text directly, to paste query text from another source, to enter complex queries that cannot be built by using the relational query designer, or to enter query-based expressions.  
  
-   Import an existing query from a file or report. Use the **Import** query button from either query designer to browse to a .sql file or .rdl file and import a query.  
  
 For more information, see [Relational Query Designer User Interface &#40;Report Builder&#41;](../../reporting-services/report-data/relational-query-designer-user-interface-report-builder.md) and [Text-based Query Designer User Interface &#40;Report Builder&#41;](../../reporting-services/report-data/text-based-query-designer-user-interface-report-builder.md).  
  
 The text-based query designer supports the [Text](#QueryText) mode in which you type [!INCLUDE[DWsql](../../includes/dwsql-md.md)] commands that select data from the data source.  
  
-   [Text](#QueryText)  
  
 You use [!INCLUDE[DWsql](../../includes/dwsql-md.md)] with [!INCLUDE[ssDW](../../includes/ssdw-md.md)] and [!INCLUDE[tsql](../../includes/tsql-md.md)] with SQL Server. The two dialects of the SQL language are very similar. Queries written for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source connection type can typically be used for the [!INCLUDE[ssDWCurrentFull](../../includes/ssdwcurrentfull-md.md)] data source connection type.  
  
 A query that retrieves report data from a large database, including a data warehouse such as [!INCLUDE[ssDW](../../includes/ssdw-md.md)], might generate a result set that has a very large number of rows unless you aggregate and summarize data to reduce the number of rows that the query returns. You can write queries that include aggregates and grouping by using either the graphical or text-based query designer.  
  
 [!INCLUDE[DWsql](../../includes/dwsql-md.md)] support the clause, keyword, and aggregates that the query designer provides to summarize data.  
  
 The graphical query designer used by [!INCLUDE[ssDW](../../includes/ssdw-md.md)] provides built-in support for grouping and aggregates to help you write queries that retrieve only summary data. The [!INCLUDE[DWsql](../../includes/dwsql-md.md)] language features are: the GROUP BY clause, DISTINCT keyword, and aggregates such as SUM and COUNT. The text-based query designer provides full support for the [!INCLUDE[DWsql](../../includes/dwsql-md.md)] language, including grouping and aggregates.  
  
 For more information about [!INCLUDE[tsql](../../includes/tsql-md.md)], see [Transact-SQL Reference &#40;Database Engine&#41;](../../t-sql/transact-sql-reference-database-engine.md)in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?LinkId=141687) on msdn.microsoft.com.  
  
###  <a name="QueryText"></a> Using Query Type Text  
 In the text-based query designer, you type [!INCLUDE[DWsql](../../includes/dwsql-md.md)] commands to define the data in a dataset. The queries that you use to retrieve data from [!INCLUDE[ssDW](../../includes/ssdw-md.md)] are the same as ones you use to retrieve data from instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are not running within a [!INCLUDE[ssDW](../../includes/ssdw-md.md)] application. For example, the following [!INCLUDE[DWsql](../../includes/dwsql-md.md)] query selects the names of all employees who are marketing assistants:  
  
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
  
  
##  <a name="Parameters"></a> Parameters  
 When query text contains query variables or stored procedures that have input parameters, the corresponding query parameters for the dataset and report parameters for the report are automatically generated. The query text must not include the DECLARE statement for each query variable.  
  
 For example, the following SQL query creates a report parameter named **EmpID**:  
  
```  
SELECT FirstName, LastName FROM HumanResources.Employee E INNER JOIN  
       Person.Contact C ON  E.ContactID=C.ContactID   
WHERE EmployeeID = (@EmpID)  
```  
  
 By default, each report parameter has data type Text and an automatically created dataset to provide a drop-down list of available values. After the report parameters are created, you might have to change default values. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
  
##  <a name="Remarks"></a> Remarks  
  
###### Platform and Version Information  
 For more information about platform and version support, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md) in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
  
  
##  <a name="HowTo"></a> How-To Topics  
 This section contains step-by-step instructions for working with data connections, data sources, and datasets.  
  
 [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)  
  
 [Add a Filter to a Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-a-filter-to-a-dataset-report-builder-and-ssrs.md)  
  
  
##  <a name="Related"></a> Related Sections  
 These sections of the documentation provide in-depth conceptual information about report data, and procedural information about how to define, customize, and use parts of a report that are related to data.  
  
 [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)  
 Provides an overview of accessing data for your report.  
  
 [Data Connections, Data Sources, and Connection Strings in Report Builder](https://msdn.microsoft.com/library/7e103637-4371-43d7-821c-d269c2cc1b34)  
 Provides information about data connections and data sources.  
  
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
 Provides information about embedded and shared datasets.  
  
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md)  
 Provides information about the dataset field collection generated by the query.  
  
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md) in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
 Provides in-depth information about platform and version support for each data extension.  

## Next steps

[Report Parameters](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   
[Filter, Group, and Sort Data](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
[Expressions](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
