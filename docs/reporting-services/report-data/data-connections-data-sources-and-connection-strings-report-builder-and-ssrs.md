---
title: Create data connection strings in Report Builder
description: Learn how to create data connection strings and learn important information related to data source credentials.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/01/2024
ms.service: reporting-services
ms.subservice: report-data
ms.topic: conceptual
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want to learn how to create data strings so that I can connect Report Builder to my data sources.
---
# Create data connection strings in Report Builder

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

To include data in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated reports, you must first create a connection string to your data source. This article explains how to create data connection strings and provides important information related to data source credentials. A data source includes the data source type, connection information, and the type of credentials needed. For more information, see [Intro to report data in SQL Server Reporting Services (SSRS)](report-data-ssrs.md).

## <a name="bkmk_DataConnections"></a> Built-in data extensions

Default data extensions in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] include Microsoft SQL Server, Microsoft Azure SQL Database, and Microsoft SQL Server Analysis Services. For a full list of data sources and versions [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports, see [Data sources supported by Reporting Services (SSRS)](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).

## <a name="bkmk_connection_examples"></a> Common connection string examples

Connection strings are the text representation of connection properties for a data provider. The following table lists examples of connections strings for various data connection types.

> [!NOTE]
> [Connectionstrings.com](https://www.connectionstrings.com/) is another resource to get examples for connection strings.

|**Data source**|**Example**|**Description**|
|---------------------|-----------------|---------------------|
|SQL Server database on the local server|`Data Source="(local)"; Initial Catalog=AdventureWorks`|Set data source type to **Microsoft SQL Server**. For more information, see [SQL Server connection type (SSRS)](../../reporting-services/report-data/sql-server-connection-type-ssrs.md).|
|SQL Server named instance|`Data Source=<host>\MSSQL13.<InstanceName>; Initial Catalog=AdventureWorks`|Set data source type to **Microsoft SQL Server**. For more information, see [SQL Server connection type (SSRS)](../../reporting-services/report-data/sql-server-connection-type-ssrs.md).|
|Azure SQL Database|`Data Source=<host>.database.windows.net; Initial Catalog=AdventureWorks; Encrypt=True`|Set data source type to **Microsoft Azure SQL Database**. For more information, see [Azure SQL connection type (SSRS)](../../reporting-services/report-data/sql-azure-connection-type-ssrs.md).|
|SQL Server Parallel Data Warehouse|`HOST=<IP address>; database=AdventureWorks; port=<port>`|Set data source type to **Microsoft SQL Server Parallel Data Warehouse**. For more information, see [SQL Server Parallel Data Warehouse connection type (SSRS)](../../reporting-services/report-data/sql-server-parallel-data-warehouse-connection-type-ssrs.md).|
|Analysis Services database on the local server|`Data Source=localhost; Initial Catalog=Adventure Works DW`|Set data source type to **Microsoft SQL Server Analysis Services**. For more information, see [Analysis Services connection type for MDX (SSRS)](../../reporting-services/report-data/analysis-services-connection-type-for-mdx-ssrs.md) or [Analysis Services connection type for DMX (SSRS)](../../reporting-services/report-data/analysis-services-connection-type-for-dmx-ssrs.md).|
|Analysis Services tabular model database with Sales perspective|`Data Source=<servername>; Initial Catalog=Adventure Works DW; cube='Sales'`|Set data source type to **Microsoft SQL Server Analysis Services**. Specify perspective name in `cube=` setting. For more information, see [Perspectives in tabular models](/analysis-services/tabular-models/perspectives-ssas-tabular).|
|Azure Analysis Services|`Data Source=asazure://aspaaseastus2.asazure.windows.net/<server name>; Initial Catalog=AdventureWorks`|Set data source type to **Microsoft SQL Server Analysis Services**. For more information, see [Microsoft SQL Server Analysis Services data processing extension](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md#AnalysisServices).|
|Oracle server|`Data Source=<host>`|Set the data source type to **Oracle**. The Oracle client tools must be installed on the Report Designer computer and on the report server. For more information, see [Oracle Connection Type (SSRS & Power BI Report Server)](../../reporting-services/report-data/oracle-connection-type-ssrs.md).|
|SAP NetWeaver BI data source|`Data Source=https://mySAPNetWeaverBIServer:8000/sap/bw/xml/soap/xmla`|Set the data source type to **SAP NetWeaver BI**. For more information, see [SAP NetWeaver BI connection type (SSRS)](../../reporting-services/report-data/sap-netweaver-bi-connection-type-ssrs.md).|
|Hyperion Essbase data source|`Data Source=https://localhost:13080/aps/XMLA; Initial Catalog=Sample`|Set the data source type to **Hyperion Essbase**. For more information, see [Hyperion Essbase connection type (SSRS)](../../reporting-services/report-data/hyperion-essbase-connection-type-ssrs.md).|
|Teradata data source|`Data Source=<NNN>.<NNN>.<NNN>.<NNN>;`|Set the data source type to **Teradata**. The connection string is an IP address in the form of four fields, where each field can be from one to three digits. For more information, see [Teradata connection type (SSRS)](../../reporting-services/report-data/teradata-connection-type-ssrs.md).|
|Teradata data source|`Database=<database name>; Data Source=<NNN>.<NNN>.<NNN>.<NNN>; Use X Views=False; Restrict to Default Database=True`|Set the data source type to **Teradata**, similar to the previous example. Only use the default database that is specified in the `Database` tag, and don't automatically discover data relationships.|
|XML data source, Web service|`data source=https://adventure-works.com/results.aspx`|Set the data source type to **XML**. The connection string is a URL for a web service that supports Web Services Definition Language (WSDL). For more information, see [XML connection type (SSRS)](../../reporting-services/report-data/xml-connection-type-ssrs.md).|
|XML data source, XML document|`https://localhost/XML/Customers.xml`|Set the data source type to **XML**. The connection string is a URL to the XML document.|
|XML data source, embedded XML document|*Empty*|Set the data source type to **XML**. The XML data is embedded in the report definition.|
|SharePoint List|`Data Source=https://MySharePointWeb/MySharePointSite/`|Set data source type to **SharePoint List**.|
| Power BI Premium dataset (Starting with Reporting Services 2019 and Power BI Report Server January 2020) | `Data Source=powerbi://api.powerbi.com/v1.0/myorg/<workspacename>;  Initial Catalog=<datasetname>` | Set data source type to **Microsoft SQL Server Analysis Services**. |

If you can't connect to a report server using **localhost**, make sure that the network protocol for the TCP/IP protocol is enabled. For more information, see [Configure client protocols](../../database-engine/configure-windows/configure-client-protocols.md).

For more information about the configurations needed to connect to these data source types, see the specific data connection article under [Add data from external data sources (SSRS)](../../reporting-services/report-data/add-data-from-external-data-sources-ssrs.md) or [Data sources supported by Reporting Services (SSRS)](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).

## <a name="bkmk_special_password_characters"></a> Special characters in a password

If you configure your ODBC or SQL data source with a password, you might encounter errors with special characters. If a user enters the password with special characters like punctuation marks, some underlying data source drivers can't validate those special characters. When you process the report, the message "Not a valid password" might indicate this problem. If changing the password is impractical, you can work with your database administrator to store the appropriate credentials on the server as part of a system ODBC data source name (DSN). For more information, see [OdbcConnection.ConnectionString](/dotnet/api/system.data.odbc.odbcconnection.connectionstring) in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] documentation.

## <a name="bkmk_Expressions_in_connection_strings"></a> Expression-based connection strings

Expression-based connection strings are evaluated at run time. For example, you can specify the data source as a parameter, include the parameter reference in the connection string, and allow the user to choose a data source for the report. For example, suppose a multinational firm has data servers in several countries/regions. With an expression-based connection string, a user running a sales report can select a data source for a particular country/region before running the report.

The following example illustrates the use of a data source expression in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection string. The example assumes you created a report parameter named `ServerName`:

```  basic
="Data Source=" & Parameters!ServerName.Value & "; Initial Catalog=AdventureWorks"  
```

Data source expressions are processed at run time or when a report is previewed. The expression must be written in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)]. Use the following guidelines when defining a data source expression:

- Design the report using a static connection string. A static connection string is a connection string that isn't set through an expression. For example, when you follow the steps for creating a report-specific or shared data source, you're defining a static connection string. Using a static connection string allows you to connect to the data source in Report Designer so you can get the query results you need to create the report.

- When defining the data source connection, don't use a shared data source. You can't use a data source expression in a shared data source. You must define an embedded data source for the report.

- Specify credentials separately from the connection string. You can use stored credentials, prompted credentials, or integrated security.

- Add a report parameter to specify a data source. For parameter values, you can either provide a static list of available values or define a query that retrieves a list of data sources at run time. In this case, the static list of available values should be data sources you can use with the report.

- Be sure that the list of data sources shares the same database schema. All report design begins with schema information. If there's a mismatch between the schema used to define the report and the actual schema used by the report at run time, the report might not run.

- Before publishing the report, replace the static connection string with an expression. Wait until you finish designing the report before you replace the static connection string with an expression. Once you use an expression, you can't execute the query in Report Designer. Furthermore, the field list in the **Report Data** pane and the **Parameters** list doesn't update automatically.

## Related content

- [Intro to report data in SQL Server Reporting Services (SSRS)](report-data-ssrs.md)
- [Create and modify shared data sources](../../reporting-services/report-data/create-modify-and-delete-shared-data-sources-ssrs.md)
- [Create and modify embedded data sources](../../reporting-services/report-data/create-and-modify-embedded-data-sources.md)

More questions? Try asking the [Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
