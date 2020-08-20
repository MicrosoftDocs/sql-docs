---
title: "Oracle Connection Type (SSRS & Power BI Report Server)"
description: Use the information in this article about the Oracle connection type to learn how to build a data source.
ms.date: 03/12/2020
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
ms.assetid: 9db86dd2-beda-42d8-8af7-2629d58a8e3d
author: maggiesMSFT
ms.author: maggies
---
# Oracle Connection Type (SSRS & Power BI Report Server)

To use data from an Oracle database in your report, you must have a dataset that's based on a report data source of type Oracle. This built-in data source type uses the Oracle Data Provider directly and requires an Oracle client software component. This article explains how to download and install drivers for Reporting Services, Power BI Report Server, Report Builder, and Power BI Desktop.


Use the information in this article to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md). 


## 64-bit drivers for the report servers

Power BI Report Server and SQL Server Reporting Services 2016 and later all use **Managed ODP.NET** for paginated reports. The following steps are only needed  when using Oracle ODAC drivers 12.2 and later as they install by default to a non-machine-wide configuration for a new Oracle home installation. These steps assume you've installed the ODAC 18.x files to c:\oracle64 and the file version of your Oracle.ManagedDataAccess.dll and Oracle.DataAccess.dll is 4.122.18.3.

1. On the Oracle download site, install the [Oracle 64-bit ODAC Oracle Universal Installer (OUI)](https://www.oracle.com/technetwork/topics/dotnet/downloads/odacdeploy-4242173.html). 
2. Register ODP.NET Managed Client to GAC:

    C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\managed\common\Oracle.ManagedDataAccess.dll
3. Add ODP.NET Managed Client entries to machine.config:

    C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odpm /frameworkversion:v4.0.30319 /productversion:4.122.18.3

### Power BI Reports use Unmanaged ODP.NET

Power BI Reports use **Unmanaged ODP.NET**. Follow these steps to register Unmanaged ODP.NET:

1. Register ODP.NET Unmanaged Client to GAC:

   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
2. Add ODP.NET Unmanaged Client entries to machine.config:

   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odp /frameworkversion:v4.0.30319 /productversion:4.122.18.3
 
## 32-bit drivers for Report Builder

Report Builder uses **Managed ODP.NET** for authoring paginated reports. The following steps are only needed  when using Oracle ODAC drivers 12.2 and later as they install by default to a non-machine-wide configuration for a new Oracle home installation. These steps assume you've installed the ODAC 18.x files to c:\oracle32 and the file version of your Oracle.ManagedDataAccess.dll is 4.122.18.3.

1. On the Oracle download site, install the [Oracle 32-bit ODAC Oracle Universal Installer (OUI)](https://www.oracle.com/technetwork/topics/dotnet/downloads/odacdev-4242174.html).
2. Register ODP.NET Managed Client to GAC:

    C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle32\product\18.0.0\client_1\odp.net\managed\common\Oracle.ManagedDataAccess.dll
3. Add ODP.NET Managed Client entries to machine.config:

    C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odpm /frameworkversion:v4.0.30319 /productversion:4.122.18.3

## 64-bit and 32-bit drivers for Power BI Desktop

Power BI Reports use **Unmanaged ODP.NET**. The following steps are only needed  when using Oracle ODAC drivers 12.2 and later as they install by default to a non-machine-wide configuration for a new Oracle home installation. These steps assume you've installed the ODAC 18.x files to c:\oracle64 for 64-bit and c:\oracle32 for 32-bit and the file version of your Oracle.DataAccess.dll is 4.122.18.3. Follow these steps to register Unmanaged ODP.NET:

### 64-bit Power BI Desktop

1. Register ODP.NET Unmanaged Client to GAC:

   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
2. Add ODP.NET Unmanaged Client entries to machine.config:

   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odp /frameworkversion:v4.0.30319 /productversion:4.122.18.3

### 32-bit Power BI Desktop

1. Register ODP.NET Unmanaged Client to GAC:

   C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
2. Add ODP.NET Unmanaged Client entries to machine.config:

   C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odp /frameworkversion:v4.0.30319 /productversion:4.122.18.3
  
##  <a name="Connection"></a> Connection String  
 Contact your database administrator for connection information and for the credentials to use to connect to the data source. The following connection string example specifies an Oracle database on the server named "Oracle18" using Unicode. The server name must match what is defined in the Tnsnames.ora configuration file as the Oracle server instance name.  
  
```  
Data Source="Oracle"; Unicode="True"  
```  
  
 For more connection string examples, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
##  <a name="Credentials"></a> Credentials  
 Credentials are required to run queries, to preview the report locally, and to preview the report from the report server.  
  
 After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid.  
  
 For more information, see [Specify Credential and Connection Information for Report Data Sources](specify-credential-and-connection-information-for-report-data-sources.md).  
  
  
##  <a name="Query"></a> Queries  
 To create a dataset, you can either select a stored procedure from a drop-down list or create an SQL query. To build a query, you must use the text-based query designer. For more information, see [Text-based Query Designer User Interface &#40;Report Builder&#41;](../../reporting-services/report-data/text-based-query-designer-user-interface-report-builder.md).  
  
 You can specify stored procedures that return only one result set. Using cursor-based queries aren't supported.  
  
##  <a name="Parameters"></a> Parameters  
 If the query includes query variables, corresponding report parameters are automatically generated. Named parameters are supported by this extension. For Oracle version 9 or later, multivalue parameters are supported.  
  
 Report parameters are created with default property values that you might need to modify. For example, each report parameter is data type **Text**. After the report parameters are created, you might have to change default values. For more information, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
  
##  <a name="Remarks"></a> Remarks  
 Before you can connect an Oracle data source, the system administrator must have installed the version of the .NET Data Provider for Oracle that supports retrieving data from the Oracle database. This data provider must be installed on the same computer as Report Builder and also on the report server.  
  
 For more information, see the following articles:  
  
-   [How to use Reporting Services to configure and to access an Oracle data source](https://docs.microsoft.com/archive/blogs/dataaccesstechnologies/configure-oracle-data-source-for-sql-server-reporting-services-ssdt-and-report-server)  
-   [How to add permissions for the NETWORK SERVICE security principal](https://support.microsoft.com/kb/870668)  
  
### Alternate Data Extensions 
 
 You can also retrieve data from an Oracle database by using an OLE DB data source type. For more information, see [OLE DB Connection Type &#40;SSRS&#41;](../../reporting-services/report-data/ole-db-connection-type-ssrs.md).  

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions" 
### Report Models  

 You can also create models based on an Oracle database.  
::: moniker-end
   
### Platform and Version Information  

 For more information about platform and version support, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  

  
## See Also

 [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)  
  
  
