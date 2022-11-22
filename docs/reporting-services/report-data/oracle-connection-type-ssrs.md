---
title: "Oracle Connection Type (SSRS & Power BI Report Server)"
description: Use the information in this article about the Oracle connection type to learn how to build a data source.
ms.date: 08/09/2022
ms.service: reporting-services
ms.subservice: report-data
ms.topic: conceptual
ms.assetid: 9db86dd2-beda-42d8-8af7-2629d58a8e3d
author: maggiesMSFT
ms.author: maggies
---
# Oracle Connection Type (SSRS & Power BI Report Server)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

To use data from an Oracle database in your report, you must have a dataset that's based on a report data source of type Oracle. This built-in data source type uses the Oracle Data Provider directly and requires an Oracle client software component. This article explains how to download and install drivers for Reporting Services, Power BI Report Server, Report Builder, and Power BI Desktop.


Use the information in this article to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md). 

> [!IMPORTANT]
> The following commands that use Oracle's OraProvCfg.exe tool to register Oracle's Managed and Unmanaged ODP.NET drivers are provided as examples for use with the above Microsoft products. For the configuration of the ODP.NET drivers specific to your environment, you may need to contact Oracle support or reference Oracle's documentation for [Configuring Oracle Data Provider for .NET](https://docs.oracle.com/en/database/oracle/oracle-database/19/odpnt/InstallConfig.html#GUID-1F689B90-2CC4-4907-B8FE-A5F4EE36F673).

## 64-bit drivers for the report servers

On the Oracle download site, install the [Oracle 64-bit ODAC Oracle Universal Installer (OUI)](https://www.oracle.com/database/technologies/dotnet-odacdev-downloads.html). You only need the following steps when using Oracle ODAC drivers 12.2 and later. Otherwise, they install by default to a non-machine-wide configuration for a new Oracle home installation. These steps assume you've installed the ODAC 18.x files to the c:\oracle64 folder.


### Paginated (RDL) Reports use Managed ODP.NET

Power BI Report Server and SQL Server Reporting Services 2016 and later all use **Managed ODP.NET** for paginated (RDL) reports. Follow these steps to register Managed ODP.NET:

1. Register ODP.NET Managed Client to GAC:

   ```
   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\managed\common\Oracle.ManagedDataAccess.dll
   ```

2. Add ODP.NET Managed Client entries to machine.config:

   ```
   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odpm /frameworkversion:v4.0.30319 /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\managed\common\Oracle.ManagedDataAccess.dll
   ```

### Power BI Reports use Unmanaged ODP.NET

Power BI Report Server uses **Unmanaged ODP.NET** for Power BI reports. Follow these steps to register Unmanaged ODP.NET:

1. Register ODP.NET Unmanaged Client to GAC:

   ```
   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
   ```
2. Add ODP.NET Unmanaged Client entries to machine.config:

   ```
   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odp /frameworkversion:v4.0.30319 /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
   ```

## 32-bit drivers for Microsoft Report Builder

[Microsoft Report Builder](https://www.microsoft.com/download/details.aspx?id=53613) uses **Managed ODP.NET** for authoring paginated (RDL) reports. You only need the following steps when using Oracle ODAC drivers 12.2 and later. Otherwise, they install by default to a non-machine-wide configuration for a new Oracle home installation. These steps assume you've installed the ODAC 18.x files to the c:\oracle32 folder where Microsoft Report Builder is installed. Follow these steps to register Managed ODP.NET:

1. On the Oracle download site, install the [Oracle "ODAC with Oracle Developer Tools for Visual Studio - OUI" (32-bit)](https://www.oracle.com/database/technologies/dotnet-odacdev-downloads.html).

2. Register ODP.NET Managed Client to GAC:

   ```
   C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle32\product\18.0.0\client_1\odp.net\managed\common\Oracle.ManagedDataAccess.dll
   ```

3. Add ODP.NET Managed Client entries to machine.config:

   ```
   C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odpm /frameworkversion:v4.0.30319 /providerpath:C:\oracle32\product\18.0.0\client_1\odp.net\managed\common\Oracle.ManagedDataAccess.dll
   ```

## 64-bit drivers for Power BI Report Builder

> [!NOTE]
> The following instructions apply to Power BI Report Builder version 15.7.01678.0001 and later. For versions before 15.7.01678.0001, follow the **32-bit drivers for Microsoft Report Builder** instructions above.

[Power BI Report Builder](https://www.microsoft.com/download/details.aspx?id=58158) uses **Managed ODP.NET** for authoring paginated (RDL) reports. You only need the following steps when using Oracle ODAC drivers 12.2 and later. Otherwise, they install by default to a non-machine-wide configuration for a new Oracle home installation. These steps assume you've installed the ODAC 18.x files to the c:\oracle64 folder where Power BI Report Builder is installed. Follow these steps to register Managed ODP.NET:

1. On the Oracle download site, install the [Oracle 64-bit ODAC Oracle Universal Installer (OUI)](https://www.oracle.com/database/technologies/dotnet-odacdev-downloads.html).

2. Register ODP.NET Managed Client to GAC:

   ```
   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\managed\common\Oracle.ManagedDataAccess.dll
   ```

3. Add ODP.NET Managed Client entries to machine.config:

   ```
   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odpm /frameworkversion:v4.0.30319 /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\managed\common\Oracle.ManagedDataAccess.dll
   ```

## 64-bit and 32-bit drivers for Power BI Desktop

Power BI Desktop uses **Unmanaged ODP.NET** for authoring Power BI reports. You only need the following steps when using Oracle ODAC drivers 12.2 and later. Otherwise, they install by default to a non-machine-wide configuration for a new Oracle home installation. These steps assume you've installed the ODAC 18.x files to the c:\oracle64 folder for 64-bit Power BI Desktop or the c:\oracle32 folder for 32-bit Power BI Desktop. Follow these steps to register Unmanaged ODP.NET:

### 64-bit Power BI Desktop

1. On the Oracle download site, install the [Oracle 64-bit ODAC Oracle Universal Installer (OUI)](https://www.oracle.com/database/technologies/dotnet-odacdev-downloads.html).
2. Register ODP.NET Unmanaged Client to GAC:

   ```
   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
   ```

3. Add ODP.NET Unmanaged Client entries to machine.config:

   ```
   C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odp /frameworkversion:v4.0.30319 /providerpath:C:\oracle64\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
   ```

### 32-bit Power BI Desktop

1. On the Oracle download site, install the [Oracle "ODAC with Oracle Developer Tools for Visual Studio - OUI" (32-bit)](https://www.oracle.com/database/technologies/dotnet-odacdev-downloads.html).

2. Register ODP.NET Unmanaged Client to GAC:

   ```
   C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:gac /providerpath:C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
   ```

3. Add ODP.NET Unmanaged Client entries to machine.config:

   ```
   C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\OraProvCfg.exe /action:config /force /product:odp /frameworkversion:v4.0.30319 /providerpath:C:\oracle32\product\18.0.0\client_1\odp.net\bin\4\Oracle.DataAccess.dll
   ```

##  <a name="Connection"></a> Connection String  

Contact your database administrator for connection information and for the credentials to use to connect to the data source. The following connection string example specifies an Oracle database on the server named "Oracle18" using Unicode. The server name must match what is defined in the Tnsnames.ora configuration file as the Oracle server instance name.  
  
```  
Data Source="Oracle18"; Unicode="True"  
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
  
- [How to use Reporting Services to configure and to access an Oracle data source](/archive/blogs/dataaccesstechnologies/configure-oracle-data-source-for-sql-server-reporting-services-ssdt-and-report-server)  
- [How to add permissions for the NETWORK SERVICE security principal](https://mskb.pkisolutions.com/kb/870668)  
  
### Alternate Data Extensions 

You can also retrieve data from an Oracle database by using an OLE DB data source type. For more information, see [OLE DB Connection Type &#40;SSRS&#41;](../../reporting-services/report-data/ole-db-connection-type-ssrs.md).  

::: moniker range=">=sql-server-2017"

### Report Models

 You can also create models based on an Oracle database.  
::: moniker-end

### Platform and Version Information  

For more information about platform and version support, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  

## See Also

[Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   

[Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   

[Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)
