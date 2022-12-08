---
title: "OLE DB Connection Type | Microsoft Docs"
description: Use the information in this article about the OLE DB connection type to learn how to build a data source.
ms.date: 03/17/2017
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
ms.assetid: d00cb13b-e1c2-4300-a195-3da1430a2df1
author: maggiesMSFT
ms.author: maggies
---
# OLE DB Connection Type (SSRS)
  To include data from an OLE DB data provider, you must have a dataset that is based on a report data source of type OLE DB. This built-in data source type is based on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] OLE DB data processing extension.  
  
 OLE DB is a data access technology that enables clients to connect to a variety of data providers. After you select the data source type OLE DB, you must select a specific data provider. Support for features such as parameters and credentials are dependent on the data provider that you select.  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
##  <a name="Connection"></a> Connection String  
 The connection string for the OLE DB data processing extension depends on the data provider that you want. A typical connection string contains name/value pairs that are supported by the data provider. For example, the following connection string specifies the OLE DB provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client and the AdventureWorks database:  
  
```  
Provider=SQLNCLI10.1;Data Source=server; Initial Catalog=AdventureWorks  
```  
  
 The connection string that you use depends on the external data source that you are connecting to. To set connection string properties specific to a data provider, from the **General** page of the **Data Source Properties** dialog box, click the **Build** button to open the **Connection Properties** dialog box. Set extended data source properties through the **Data Link Properties** dialog box.  
  
 For examples of connection strings, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
  
##  <a name="Credentials"></a> Credentials  
 Credentials are required to run queries, to preview the report locally, and to preview the report from the report server.  
  
 After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid.  
  
 For more information, see [Specify Credential and Connection Information for Report Data Sources](specify-credential-and-connection-information-for-report-data-sources.md).  
  
###### Special Characters in a Password  
 If you configure the OLE DB data source to prompt for a password or to include the password in the connection string, and a user enters the password with special characters such as punctuation marks, some underlying data source drivers cannot validate the special characters. When you process the report, the message "Not a valid password" may indicate this problem.  
  
> [!NOTE]  
>  It is recommended that you do not add login information such as passwords to the connection string. Report Builder provides a separate tab on the **Data Source** dialog box that you can use to enter credentials.  
  
  
##  <a name="Parameters"></a> Parameters  
 Some OLE DB providers support unnamed parameters and not named parameters. Parameters are passed by position by using a placeholder in the query. The placeholder character is determined by the syntax that is supported by the data provider.  
  
  
##  <a name="Remarks"></a> Remarks  
 OLEDB is a native technology for building data providers for specific data sources. OLEDB is based on COM (Component Object Model) interfaces. OLEDB is a later technology than ODBC and earlier than ADO.NET data providers. OLEDB data providers are registered with the operating system like any other COM component. OLEDB data providers are available from Microsoft and third party vendors. Microsoft also provides MSDASQL, an OLEDB data provider that bridges communication to ODBC drivers. For more information, see [ODBC Connection Type &#40;SSRS&#41;](../../reporting-services/report-data/odbc-connection-type-ssrs.md).  
  
 To successfully retrieve the data that you want, you must provide query syntax that is supported by the data provider. Parameter support varies by data provider. For more information, see topics that are specific to the data provider that you select. For example:  
  
-   [Analysis Services client libraries](/analysis-services/client-libraries)  
   
  
-   [SQL Server Native Client &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)  
  
 For more information about specific OLE DB data providers, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  
  
  
##  <a name="HowTo"></a> How-To Topics  
 This section contains step-by-step instructions for working with data connections, data sources, and datasets.  
  
 [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)  
  
 [Add a Filter to a Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-a-filter-to-a-dataset-report-builder-and-ssrs.md)  
  
  
##  <a name="Related"></a> Related Sections  
 These sections of the documentation provide in-depth conceptual information about report data, as well as procedural information about how to define, customize, and use parts of a report that are related to data.  
  
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
  
