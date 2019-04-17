---
title: "Oracle Connection Type (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 9db86dd2-beda-42d8-8af7-2629d58a8e3d
author: markingmyname
ms.author: maghan
manager: kfile
---
# Oracle Connection Type (SSRS)
  To use data from an Oracle database in your report, you must you must have a dataset that is based on a report data source of type Oracle. This built-in data source type is based on the .NET Framework Managed Provider for Oracle and requires an Oracle client software component.  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
##  <a name="Connection"></a> Connection String  
 Contact your database administrator for connection information and for the credentials to use to connect to the data source. The following connection string example specifies an Oracle database on the server named "Oracle9" using Unicode. The server name must match what is defined in the Tnsnames.ora configuration file as the Oracle server instance name.  
  
```  
Data Source="Oracle9"; Unicode="True"  
```  
  
 For more information about connection string examples, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md).  
  
##  <a name="Credentials"></a> Credentials  
 Credentials are required to run queries, to preview the report locally, and to preview the report from the report server.  
  
 After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid.  
  
 For more information, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md) or [Specify Credentials in Report Builder](../specify-credentials-in-report-builder.md).  
  

  
##  <a name="Query"></a> Queries  
 To create a dataset, you can either select a stored procedure from a drop-down list or create an SQL query. To build a query, you must use the text-based query designer. For more information, see [Text-based Query Designer User Interface &#40;Report Builder&#41;](text-based-query-designer-user-interface-report-builder.md).  
  
 You can specify stored procedures that return only one result set. Using cursor-based queries are not supported.  
  
##  <a name="Parameters"></a> Parameters  
 If the query includes query variables, corresponding report parameters are automatically generated. Named parameters are supported by this extension. For Oracle version 9 or later, multivalue parameters are supported.  
  
 Report parameters are created with default property values that you might need to modify. For example, each report parameter is data type **Text**. After the report parameters are created, you might have to change default values. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../report-design/report-parameters-report-builder-and-report-designer.md).  
  

  
##  <a name="Remarks"></a> Remarks  
 Before you can connect an Oracle data source, the system administrator must have installed the version of the .NET Data Provider for Oracle that supports retrieving data from the Oracle database. This data provider must be installed on the same computer as Report Builder and also on the report server.  
  
 For more information, see the following:  
  
-   [Using the .NET Framework Data Provider for Oracle](https://go.microsoft.com/fwlink/?LinkId=112314) on msdn.microsoft.com  
  
-   [How to use Reporting Services to configure and to access an Oracle data source](https://support.microsoft.com/kb/834305)  
  
-   [How to add permissions for the NETWORK SERVICE security principal](https://support.microsoft.com/kb/870668)  
  
###### Alternate Data Extensions  
 You can also retrieve data from an Oracle database by using an OLE DB data source type. For more information, see [OLE DB Connection Type &#40;SSRS&#41;](ole-db-connection-type-ssrs.md).  
  
###### Report Models  
 You can also create models based on an Oracle database.  
  
###### Platform and Version Information  
 For more information about platform and version support, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../create-deploy-and-manage-mobile-and-paginated-reports.md) in the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
  

  
##  <a name="HowTo"></a> How-To Topics  
 This section contains step-by-step instructions for working with data connections, data sources, and datasets.  
  
 [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)  
  
 [Add a Filter to a Dataset &#40;Report Builder and SSRS&#41;](add-a-filter-to-a-dataset-report-builder-and-ssrs.md)  
  
 
  
##  <a name="Related"></a> Related Sections  
 These sections of the documentation provide in-depth conceptual information about report data, as well as procedural information about how to define, customize, and use parts of a report that are related to data.  
  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-datasets-ssrs.md)  
 Provides an overview of accessing data for your report.  
  
 [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md)  
 Provides information about data connections and data sources.  
  
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
 Provides information about embedded and shared datasets.  
  
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](dataset-fields-collection-report-builder-and-ssrs.md)  
 Provides information about the dataset field collection generated by the query.  
  
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../create-deploy-and-manage-mobile-and-paginated-reports.md) in the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?linkid=121312).  
 Provides in-depth information about platform and version support for each data extension.  
  

  
## See Also  
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../report-design/report-parameters-report-builder-and-report-designer.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../report-design/expressions-report-builder-and-ssrs.md)  
  
  
