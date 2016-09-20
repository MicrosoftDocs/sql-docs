---
title: "Breaking Changes (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2b9e800d-790b-47a4-ba5b-adf406a89ecb
caps.latest.revision: 79
author: BarbKess
---
# Breaking Changes (Analytics Platform System)
These are breaking changes that apply when upgrading to the Analytics Platform System Appliance Updates.  
  
## Contents  
  
-   [Appliance Update 3](#au3)  
  
-   [Appliance Update 2](#au2)  
  
-   [Appliance Update 1](#au1)  
  
## <a name="au3"></a>Appliance Update 3  
  
### Existing Queries on External Tables Might Return Different Results  
In AU2, by default a PolyBase query on an external table folder returned data stored in the root folder only. In AU3, by default a query on an external table now returns data from the root folder and all of the subfolders. The root folder is specified by the LOCATION parameter in CREATE EXTERNAL TABLE.  
  
To avoid unwanted results, verify that subfolders in external file folders do not contain extra data that should not be returned in query results.  
  
For backwards compatibility, your appliance administrator can change the default setting to read data from only the root folder of the external file location. To do this, set <polybase.recursive.traversal> to 'false' in the appliance configuration file called core-site.xml.  
  
## <a name="au2"></a>Appliance Update 2  
These breaking changes apply when upgrading to Appliance Update 2.  
  
### Default Schemas May Change Behavior  
PDW now supports user-schemas and default schemas for users. Previous scripts that assumed all objects were created in the **dbo** schema, might have unexpected results if the users default schema is not **dbo**.  
  
## <a name="au1"></a>Appliance Update 1  
These breaking changes apply when upgrading to Appliance Update 1.  
  
### Changes to SQL Server Data Tools  
Update SQL Server Data Tools to the January 2014 (or later) release of SQL [Server Data Tools for Visual Studio 2012](http://msdn.microsoft.com/en-us/data/hh297027). SQL Server Data Tools is no longer supported for Visual Studio 2010.  
  
For more information, see [Install SQL Server database tooling  for Visual Studio &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/install-sql-server-database-tooling-for-visual-studio-sql-server-pdw.md)  
  
### Changes to input date formats for loading data with dwloader  
Loading date and time data can have difference results in Appliance Udpate 1 than the previous release. dwloader enforces only the order of month, day, and year when loading datetime data from the input file; it allows, but no longer enforces custom date formats.  
  
Data that will load in Appliance Update 1, but would be rejected in previous release:  
  
-   For the custom date format 'yyyy-mm-dd', dwloader only enforces the order ymd. dwloader will load the incoming data '99-2-1' whereas this would be rejected in the previous release.  
  
-   For empty strings or strings that contain only spaces, dwloader will load the value 1900-01-01 12:00:00AM whereas this would be rejected in the previous release.  
  
-   For dates that do not contain separators, dwloader will load them whereas they would be rejected in the previous release. For example, '19991231' will be loaded as '1999-12-31'.  
  
-   For missing date values, dwloader will insert 1900-01-01 whereas this would be rejected in the previous release.  
  
-   For missing time values, dwloader will insert 00:00:00 whereas this would be rejected in the previous release.  
  
-   For time data that does not specify AM or PM, dwloader will load this by using a 24-hour clock. For example, 1999/01/06 20:00:00 will be loaded as 20:00:00 (8:00 PM), whereas in the previous release, this would be loaded as 8:00:00 (8:00 AM).  
  
-   For input data formatted as HH:mm:ssZ, dwloader will load the value into time, datetime2, or datetimeoffset data, whereas this would be rejected in the previous release.  
  
-   For input data that contains only the year, dwloader will load the value January 1. For example, 1999 will load as 1999-01-01. Previously, this would be rejected.  
  
-   When loading into a smalldatetime column, dwloader will round up seconds and fractional seconds, whereas this would be rejected in the previous release. For example, 1999-01-05 20:10:35.123 will load as 01-05 20:11. Previously, this would be rejected.  
  
-   When loading into a time column, dwloader will ignore date dataFor example, 1999-01-01 12:12 will be inserted into the time column as 12:12. Previously, this would be rejected.  
  
-   For input data with leading or trailing spaces, dwloader will ignore the spaces and load the data. Previously, this would be rejected.  
  
-   Input file rows can have the EOL characters '\r\n', '\r', or '\n'. Previously, input file rows that ended in only '\r', or '\n' would be rejected.  
  
Data that will *not* load in Appliance Update 1, but passed in previous release:  
  
-   Data ordered as ydm is supported only for loading into columns of datetime and smalldatetime. Ydm data cannot be loaded into columns of datetime2, date, and datetimeoffset, whereas previously this would pass.  
  
### Update scripts that use CREATE EXTERNAL TABLE before upgrading to the next appliance update.  
The [CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-external-table-sql-server-pdw.md)and [CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-external-table-as-select-sql-server-pdw.md) syntax has changed to use data source and file format objects. The previous syntax is deprecated in Appliance Update 1 and will be removed in the next appliance update.  
  
Update scripts that use the former syntax to use the new syntax so that future upgrades will not break. When upgrading to Appliance Update 1, SQL Server PDW will re-create all existing external tables to use the new syntax and objects.  
  
Similarly, you can continue to use the former syntax in Appliance Update 1. In this case, SQL Server PDW will convert the DDL to the new syntax and will auto-create the data source and file format objects.  
  
The auto-created objects follow the naming convention DataSource_*GUID* and FileFormat_*GUID* for data sources and file formats respectively.  
  
### Update scripts that use the sys.pdw_external tables system view  
The system view sys.pdw_external_tables has been removed from Appliance Update 1. The information is now contained across these three system views:  
  
-   sys.external_tables  
  
-   sys.external_file_formats  
  
-   sys.external_data_sources  
  
### Java will be removed and upgraded automatically  
You no longer need to manually install Java onto the appliance. Upgrading SQL Server 2012 PDW to Appliance Update 1 will install Oracle Java SE Development Kit 7 on the SQL Server PDW nodes for use with PolyBase. Note that the upgrade will remove any existing non-Oracle Java versions from the appliance.  
  
### SET ROWCOUNT does not affect some statements  
SET ROWCOUNT does not affect these statements: INSERT, UPDATE, DELETE, CREATE TABLE AS SELECT, CREATE REMOTE TABLE AS SELECT, and CREATE EXTERNAL TABLE AS SELECT. Modify applications that use this. For a similar behavior, use the [TOP &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/top-sql-server-pdw.md) statement.  
  
This was deprecated in the previous release, and is now in effect in Appliance Update 1.  
  
