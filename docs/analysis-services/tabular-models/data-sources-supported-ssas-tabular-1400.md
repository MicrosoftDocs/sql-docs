---
title: "Data sources supported in SQL Server Analysis Services tabular 1400 models | Microsoft Docs"
ms.date: 02/12/2019
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data sources supported in SQL Server Analysis Services tabular 1400 models

[!INCLUDE[ssas-appliesto-sql2017](../../includes/ssas-appliesto-sql2017.md)]

This article describes the types of data sources that can be used with SQL Server Analysis Services (SSAS) tabular models at the 1400 compatibility level. 

For SSAS tabular models at the 1200 and lower compatibility levels, see [Data sources supported in SQL Server Analysis Services tabular 1200  models](data-sources-supported-ssas-tabular.md).

For Azure Analysis Services, see [Data sources supported in Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/analysis-services-datasource).


## Cloud data sources

|Datasource  |In-memory  |DirectQuery  |
|---------|---------|---------|
|Azure SQL Database<sup>[2](#azsqlmanaged)</sup>     |   Yes      |    Yes      |
|Azure SQL Data Warehouse     |   Yes      |   Yes       |
|Azure Blob Storage<sup>[1](#tab1400a)</sup>     |   Yes       |    No      |
|Azure Table Storage<sup>[1](#tab1400a)</sup>    |   Yes       |    No      |
|Azure Cosmos DB<sup>[1](#tab1400a)</sup>     |  Yes        |  No        |
|Azure Data Lake Store (Gen1)<sup>[1](#tab1400a)</sup>, <sup>[4](#gen2)</sup>      |   Yes       |    No      |
|Azure HDInsight HDFS<sup>[1](#tab1400a)</sup>     |     Yes     |   No       |
|Azure HDInsight Spark<sup>[1](#tab1400a)</sup>, <sup>[3](#databricks)</sup>     |   Yes       |   No       |
||||

<a name="tab1400a">1</a> - Tabular 1400 and higher models only.   
<a name="azsqlmanaged">2</a> - Azure SQL Database Managed Instance is supported. Because a managed instance runs within Azure VNet with a private IP address, an On-premises Data Gateway is required.   
<a name="databricks">3</a> - Azure Databricks using the Spark connector is currently not supported.   
<a name="gen2">4</a> - ADLS Gen2 is currently not supported.


**Provider**   
In-memory and DirectQuery models connecting to Azure data sources use .NET Framework Data Provider for SQL Server.

## On-premises data sources

### Supported by in-memory and DirectQuery models

|Datasource | In-memory provider | DirectQuery provider |
|  --- | --- | --- |
| SQL Server |SQL Server Native Client 11.0, Microsoft OLE DB Provider for SQL Server, .NET Framework Data Provider for SQL Server | .NET Framework Data Provider for SQL Server |
| SQL Server Data Warehouse |SQL Server Native Client 11.0, Microsoft OLE DB Provider for SQL Server, .NET Framework Data Provider for SQL Server | .NET Framework Data Provider for SQL Server |
| Oracle |Microsoft OLE DB Provider for Oracle, Oracle Data Provider for .NET |Oracle Data Provider for .NET | |
| Teradata |OLE DB Provider for Teradata, Teradata Data Provider for .NET |Teradata Data Provider for .NET | |
| | | |

> [!NOTE]
> For in-memory models, OLE DB providers can provide better performance for large-scale data. When choosing between different providers for the same data source, try the OLE DB provider first.  

### Supported by in-memory models only

|Database  |
|---------|---------|---------|
|Access Database     | 
|SQL Server Analysis Services     | 
|IBM Informix (beta) | 
|JSON document     | 
|Lines from binary     | 
|MySQL Database     | 
|PostgreSQL Database    | Yes | No
|SAP HANA   | Yes | No
|SAP Business Warehouse    | Yes | No
|Sybase Database     | Yes | No
|||

|File  |  
|---------|---------|
|Excel workbook     |
|Folder     | 
|JSON | 
|Text/CSV    | 
|XML table    | 
|||

|Online services  |  
|---------|---------|
|Dynamics 365      |
|Exhange Online     |
|Saleforce Objects    | 
|Salesforce Reports     |
|SharePoint Online List     |
|||

|Other  |  
|---------|---------|
|Active Directory      | 
|Exhange     |  
|OData Feed     | 
|ODBC query     | 
|OLE DB  | 
|SharePoint list | 
|||

## See also

[Data sources supported in SQL Server Analysis Services tabular 1200  models](data-sources-supported-ssas-tabular.md)

[Data sources supported in Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/analysis-services-datasource)   
