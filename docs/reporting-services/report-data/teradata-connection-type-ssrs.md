---
title: "Teradata Connection Type"
description: Use the information in this article about the Teradata connection type to learn how to build a data source.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-data
ms.topic: concept-article
ms.custom:
  - updatefrequency5
---
# Teradata Connection Type (SSRS)
  To include data from Teradata in your report, you must have a dataset that is based on a report data source of type Teradata. This built-in data source type is based on the .NET Managed Provider for Teradata data processing extension.  
  
 Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
##  <a name="Connection"></a> Connection String  
 Contact your database administrator for connection information and for the credentials to use to connect to the data source. The following connection string example specifies a Teradata data source on the server specified with an IP address:  
  
```  
data source=<IP Address>  
```  
  
 For more connection string examples, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
##  <a name="Credentials"></a> Credentials  
 Credentials are required to run queries, to preview the report locally, and to preview the report from the report server.  
  
 After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid.  
  
 For more information, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md) or [Specify Credential and Connection Information for Report Data Sources](specify-credential-and-connection-information-for-report-data-sources.md).  
  
  
##  <a name="Remarks"></a> Remarks  
 Before you can connect a Teradata data source, the system administrator must have installed the version of the .NET Data Provider for Teradata that supports retrieving data from Teradata. This data provider must be installed on the same computer as Report Builder and also on the report server.  
  
 Not all report delivery modes are supported by this data provider. Delivering reports through data-driven subscriptions is not supported for this data processing extension. For more information, see [Use an External Data Source for Subscriber Data &#40;Data-Driven Subscription&#41;](../../reporting-services/subscriptions/use-an-external-data-source-for-subscriber-data-data-driven-subscription.md).  
  
  
##  <a name="Models"></a> Report Models  
 To create a dataset from a report model that is based on a Teradata data source, the model must be designed in Model Designer in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and published on a report server.  
  
  
## Related content

- [Report Datasets (SSRS)](report-datasets-ssrs.md)
- [Create data connection strings in Report Builder](data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)
- [Report Embedded Datasets and Shared Datasets (Report Builder and SSRS)](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)
- [Dataset Fields Collection (Report Builder and SSRS)](dataset-fields-collection-report-builder-and-ssrs.md)
- [Data Sources Supported by Reporting Services (SSRS)](data-sources-supported-by-reporting-services-ssrs.md)
- [Paginated report parameters in Report Builder](../report-design/report-parameters-report-builder-and-report-designer.md)
- [Filter, group, and sort data in Report Builder paginated reports](../report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)
- [Expressions in a paginated report (Report Builder)](../report-design/expressions-report-builder-and-ssrs.md)
