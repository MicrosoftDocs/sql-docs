---
title: "SQL Server documentation include files"
description: Explanation of SQL Server includes files for versioning and applies-to.
ms.custom: ""
ms.date: 01/19/2019
ms.service: sql
ms.reviewer: ""
ms.subservice: release-landing
author: MashaMSFT
ms.author: mathoma
ms.topic: conceptual
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017"
---
# SQL Server include files for versioning and applies-to

References in the documentation can be easily modified without changing the actual text of individual articles by using include files in Markdown. There are three types of include files in the SQL content world: SQL version, applies-to, and referential text. **SQL Server version** include files are used to indicate the version of SQL being discussed, such as SQL Server 2016 or 2017. The **applies-to** include files indicate what SQL products and services the document applies to, such as SQL Server on Linux or Azure SQL Database. **Referential text** include files don't fall in the other two categories, such as the "Get Help" include, a list of links that customers can use to get help with SQL Server.

This article is meant to be used as a reference point for just the first two types of include files. You can browse the complete list of include files in the [sql-docs repository](https://github.com/MicrosoftDocs/sql-docs/tree/live/docs/includes).

## SQL Server version include files

SQL Content writers frequently need to include the name of the product and version of SQL Server. This way, if something changes in the name, the include file is updated instead of manually updating the value in every single article. These include files are used as placeholders for product names but have not been consistently used in all SQL documentation. SQL Server vNext refers to a future release of SQL that does not yet have a version number and is the exception to this.  

|SQL Version| File Name| Markdown Example |Text|
| :------------  | :-------------| :----------| :-------------------|
| SQL Server | ssnoversion-md.md | `[!INCLUDE[ssSQL11](../includes/ssnoversion-md.md)]` | SQL Server |
| SQL Server 2000 | ssversion2000-md.md | `[!INCLUDE[ssSQL11](../includes/ssversion2000-md.md)]` | [!INCLUDE [ssversion2000-md](../includes/ssversion2000-md.md)] |
| SQL Server 2005 | ssversion2005-md.md | `[!INCLUDE[ssSQL11](../includes/ssversion2005-md.md)]` | [!INCLUDE [ssversion2005-md](../includes/ssversion2005-md.md)] |
| SQL Server 2008 | sql2008-md.md | `[!INCLUDE [sql2008-md](../includes/sql2008-md.md)]` | [!INCLUDE [sql2008-md](../includes/sql2008-md.md)] |
| SQL Server 2008 R2 | sql2008r2-md.md | `[!INCLUDE [sql2008r2-md](../includes/sql2008r2-md.md)]` | [!INCLUDE [sql2008r2-md](../includes/sql2008r2-md.md)] |
| SQL Server 2012 | sssql11-md.md | `[!INCLUDE[ssSQL11](../includes/sssql11-md.md)]` | SQL Server 2012 (11.x) |
| SQL Server 2012 SP1 | sssql11sp1-md.md | `[!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)]` | [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] |
| SQL Server 2014 | sssql14-md.md | `[!INCLUDE[ssSQL14](../includes/sssql14-md.md)]` | [!INCLUDE [sssql14-md](../includes/sssql14-md.md)] |
| SQL Server 2016 | sssql16-md.md | `[!INCLUDE[sssql15-md](../includes/sssql16-md.md)]` | [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] |
| SQL Server 2017 | sssql17-md.md | `[!INCLUDE[sssql17-md](../includes/sssql17-md.md)]` | [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] |
| SQL Server 2019 | sssql19-md.md | `[!INCLUDE[sssql19-md](../includes/sssql19-md.md)]` | [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] |
| SQL Server vNext | sssql22-md.md | `[!INCLUDE[sssql22-md](../includes/sssql22-md.md)]` | [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] |

## SQL Server Applies-to (non-version-specific)

These applies-to include files omit the version of SQL Server.

| File Name| Markdown Example |Image|
| :-------------| :----------| :-------------------|
| appliesto-ss-asdb-asdw-xxx-md.md | `[!INCLUDE[SQL Server ASDB, ASDBMI, ASDW](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]` | [!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)] |
| appliesto-ss-asdb-xxxx-pdw-md.md | `[!INCLUDE[appliesto-ss-asdb-xxxx-pdw-md.md](../includes/appliesto-ss-asdb-xxxx-pdw-md.md)]` | [!INCLUDE[appliesto-ss-asdb-xxxx-pdw-md.md](../includes/appliesto-ss-asdb-xxxx-pdw-md.md)] |
| appliesto-ss-asdb-xxxx-xxx-md.md | `[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md.md](../includes/applies-to-version/sql-asdb.md)]` | [!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md.md](../includes/applies-to-version/sql-asdb.md)] |
| applies-to-version/sql-asdbmi.md | `[!INCLUDE[applies-to-version/sql-asdbmi.md](../includes/applies-to-version/sql-asdbmi.md)]` | [!INCLUDE[applies-to-version/sql-asdbmi.md](../includes/applies-to-version/sql-asdbmi.md)] |
| appliesto-ss-xxxx-asdw-pdw-md.md | `[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md.md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]` | [!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md.md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)] |
| appliesto-ss-xxxx-xxxx-pdw-md.md | `[!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md.md](../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)]` | [!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md.md](../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)] |
| appliesto-ss-xxxx-xxxx-xxx-md-winonly.md | `[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]` | [!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)] |
| applies-to-version/_ssnoversion.md | `[!INCLUDE[applies-to-version/_ssnoversion.md](../includes/applies-to-version/sqlserver.md)]` | [!INCLUDE[applies-to-version/_ssnoversion.md](../includes/applies-to-version/sqlserver.md)] |
| appliesto-xx-asdb-asdw-xxx-md.md | `[!INCLUDE[appliesto-xx-asdb-asdw-xxx-md.md](../includes/appliesto-xx-asdb-asdw-xxx-md.md)]` | [!INCLUDE[appliesto-xx-asdb-asdw-xxx-md.md](../includes/appliesto-xx-asdb-asdw-xxx-md.md)] |
| appliesto-xx-asdb-xxxx-xxx-md.md | `[!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md.md](../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]` | [!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md.md](../includes/appliesto-xx-asdb-xxxx-xxx-md.md)] |
| appliesto-xx-xxxx-asdw-pdw-md.md | `[!INCLUDE[appliesto-xx-xxxx-asdw-pdw-md.md](../includes/appliesto-xx-xxxx-asdw-pdw-md.md)]` | [!INCLUDE[appliesto-xx-xxxx-asdw-pdw-md.md](../includes/appliesto-xx-xxxx-asdw-pdw-md.md)] |
| appliesto-xx-xxxx-asdw-xxx-md.md | `[!INCLUDE[appliesto-xx-xxxx-asdw-xxx-md.md](../includes/appliesto-xx-xxxx-asdw-xxx-md.md)]` | [!INCLUDE[appliesto-xx-xxxx-asdw-xxx-md.md](../includes/appliesto-xx-xxxx-asdw-xxx-md.md)] |
 
## SQL Server Applies-to (version-specific)

These applies-to include files specify which versions of SQL the documentation applies to.

| File Name| Markdown Example |Image|
| :-------------| :----------| :-------------------|
| appliesto-ss-asdb-asdw-xxx-md.md | `[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md.md](../includes/appliesto-ss-asdb-asdw-xxx-md.md)]` | [!INCLUDE[appliesto-ss-asdb-asdw-xxx-md.md](../includes/appliesto-ss-asdb-asdw-xxx-md.md)] |
| tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md | `[!INCLUDE[tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)]` | [!INCLUDE[tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)] |
| tsql-appliesto-ss2008-asdbmi-xxxx-pdw-md.md | `[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-pdw-md.md](../includes/tsql-appliesto-ss2008-asdbmi-xxxx-pdw-md.md)]` | [!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-pdw-md.md](../includes/tsql-appliesto-ss2008-asdbmi-xxxx-pdw-md.md)] |
| appliesto-ss-asdb-xxxx-pdw-md.md | `[!INCLUDE[appliesto-ss-asdb-xxxx-pdw-md.md](../includes/appliesto-ss-asdb-xxxx-pdw-md.md)]` | [!INCLUDE[appliesto-ss-asdb-xxxx-pdw-md.md](../includes/appliesto-ss-asdb-xxxx-pdw-md.md)] |
| appliesto-ss-asdb-xxxx-xxx-md.md | `[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md.md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]` | [!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md.md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)] |
| appliesto-ss-xxxx-asdw-pdw-md.md | `[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md.md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]` | [!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md.md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)] |
| tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md | `[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)]` | [!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)] |
| tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md | `[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md](../includes/applies-to-version/sqlserver.md)]` | [!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md](../includes/applies-to-version/sqlserver.md)] |
| appliesto-ss-xxxx-xxxx-pdw-md.md | `[!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md.md](../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)]` | [!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md.md](../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)] |
| appliesto-ss-xxxx-xxxx-xxx-md.md | `[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]` | [!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)] |
| tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md | `[!INCLUDE[tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md)]` | [!INCLUDE[tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md)] |
| tsql-appliesto-ss2016-all-md.md | `[!INCLUDE[tsql-appliesto-ss2016-all-md.md](tsql-appliesto-ss2016-all-md.md)]` | [!INCLUDE[tsql-appliesto-ss2016-all-md.md](../includes/tsql-appliesto-ss2016-all-md.md)] |
| tsql-appliesto-ss2016-asdb-asdw-xxx-md.md | `[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]` | [!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)] |
| tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md | `[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]` | [!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)] |
| tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md | `[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md](../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]` | [!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md](../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)] |
| tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md | `[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md)]` | [!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md)] |
| tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md | `[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]` | [!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)] |
| tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md | `[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md)]` | [!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md)] |
| tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md | `[!INCLUDE[tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md)]` | [!INCLUDE[tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md)] |
| tsql-appliesto-ss2017-all-md.md | `[!INCLUDE[tsql-appliesto-ss2017-all-md.md](tsql-appliesto-ss2017-all-md.md)]` | [!INCLUDE[tsql-appliesto-ss2017-all-md.md](../includes/tsql-appliesto-ss2017-all-md.md)] |
| tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md | `[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md](../includes/applies-to-version/sqlserver2017.md)]` | [!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md](../includes/applies-to-version/sqlserver2017.md)] |
| sqlserver2017-asdb.md | `[!INCLUDE[sqlserver2017-asdb.md](../includes/applies-to-version/sqlserver2017-asdb.md)]` | [!INCLUDE[sqlserver2017-asdb.md](../includes/applies-to-version/sqlserver2017-asdb.md)] |
| appliesto-xx-asdb-asdw-xxx-md.md | `[!INCLUDE[appliesto-xx-asdb-asdw-xxx-md.md](../includes/appliesto-xx-asdb-asdw-xxx-md.md)]` | [!INCLUDE[appliesto-xx-asdb-asdw-xxx-md.md](../includes/appliesto-xx-asdb-asdw-xxx-md.md)] |
| appliesto-xx-asdb-xxxx-xxx-md.md | `[!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md.md](../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]` | [!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md.md](../includes/appliesto-xx-asdb-xxxx-xxx-md.md)] |
| applies-to-version/asa-pdw.md | `[!INCLUDE[applies-to-version/asa-pdw.md](../includes/applies-to-version/asa-pdw.md)]` | [!INCLUDE[applies-to-version/asa-pdw.md](../includes/applies-to-version/asa-pdw.md)] |
| appliesto-xx-xxxx-asdw-xxx-md.md | `[!INCLUDE[appliesto-xx-xxxx-asdw-xxx-md.md](../includes/appliesto-xx-xxxx-asdw-xxx-md.md)]` | [!INCLUDE[appliesto-xx-xxxx-asdw-xxx-md.md](../includes/appliesto-xx-xxxx-asdw-xxx-md.md)] |
| tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md | `[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md](../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]` | [!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md](../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)] |

## Analysis Services Applies-to

These applies-to include files are used with Analysis Services documentation.

| File Name| Markdown Example |Image|
| :-------------| :----------| :-------------------|
| ssas-appliesto-sql2016.md | `[!INCLUDE[ssas-appliesto-sql2016.md](../includes/ssas-appliesto-sql2016.md)]` | [!INCLUDE[ssas-appliesto-sql2016.md](../includes/ssas-appliesto-sql2016.md)] |
| ssas-appliesto-sql2016-later.md | `[!INCLUDE[ssas-appliesto-sql2016-later.md](../includes/ssas-appliesto-sql2016-later.md)]` | [!INCLUDE[ssas-appliesto-sql2016-later.md](../includes/ssas-appliesto-sql2016-later.md)] |
| ssas-appliesto-sql2016-later-aas.md | `[!INCLUDE[ssas-appliesto-sql2016-later-aas.md](../includes/ssas-appliesto-sql2016-later-aas.md)]` | [!INCLUDE[ssas-appliesto-sql2016-later-aas.md](../includes/ssas-appliesto-sql2016-later-aas.md)] |
| ssas-appliesto-sql2017.md | `[!INCLUDE[ssas-appliesto-sql2017.md](../includes/ssas-appliesto-sql2017.md)]` | [!INCLUDE[ssas-appliesto-sql2017.md](../includes/ssas-appliesto-sql2017.md)] |
| ssas-appliesto-sql2017-later-aas.md | `[!INCLUDE[ssas-appliesto-sql2017-later-aas.md](../includes/ssas-appliesto-sql2017-later-aas.md)]` | [!INCLUDE[ssas-appliesto-sql2017-later-aas.md](../includes/ssas-appliesto-sql2017-later-aas.md)] |
| ssas.md | `[!INCLUDE[ssas.md](../includes/applies-to-version/ssas.md)]` | [!INCLUDE[ssas.md](../includes/applies-to-version/ssas.md)] |
| ssas-appliesto-sqlas-all-aas.md | `[!INCLUDE[ssas-aas.md](../includes/ssas-appliesto-sqlas-all-aas.md)]` | [!INCLUDE[ssas-appliesto-sqlas-all-aas.md](../includes/ssas-appliesto-sqlas-all-aas.md)] |
| ssas-appliesto-sqlas-all.md | `[!INCLUDE[ssas-all.md](../includes/ssas-appliesto-sqlas-all.md)]` | [!INCLUDE[ssas-appliesto-sqlas-all.md](../includes/ssas-appliesto-sqlas-all.md)] |
| ssas-appliesto-sqlas-all-aas.md | `[!INCLUDE[ssas-all-aas.md](../includes/ssas-appliesto-sqlas-all-aas.md)]` | [!INCLUDE[ssas-appliesto-sqlas-all-aas.md](../includes/ssas-appliesto-sqlas-all-aas.md)] |

## Reporting Services Applies-to

These applies-to include files are used with Reporting Services documentation.

| File Name| Markdown Example |Image|
| :-------------| :----------| :-------------------|
| ssrs-appliesto-2017-and-later.md | `[!INCLUDE[ssrs-appliesto-2017-and-later.md](../includes/ssrs-appliesto-2017-and-later.md)]` | [!INCLUDE[ssrs-appliesto-2017-and-later.md](../includes/ssrs-appliesto-2017-and-later.md)] |
| ssrs-appliesto-not-pbirs.md | `[!INCLUDE[ssrs-appliesto-not-pbirs.md](../includes/ssrs-appliesto-not-pbirs.md)]` | [!INCLUDE[ssrs-appliesto-not-pbirs.md](../includes/ssrs-appliesto-not-pbirs.md)] |
| ssrs-appliesto-pbirs.md | `[!INCLUDE[ssrs-appliesto-pbirs.md](../includes/ssrs-appliesto-pbirs.md)]` | [!INCLUDE[ssrs-appliesto-pbirs.md](../includes/ssrs-appliesto-pbirs.md)] |
| ssrs-appliesto-sharepoint-2013-2016.md | `[!INCLUDE[ssrs-appliesto-sharepoint-2013-2016.md](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]` | [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016.md](../includes/ssrs-appliesto-sharepoint-2013-2016.md)] |
| ssrs-appliesto-sql2016-preview.md | `[!INCLUDE[ssrs-appliesto-sql2016-preview.md](../includes/ssrs-appliesto-sql2016-preview.md)]` | [!INCLUDE[ssrs-appliesto-sql2016-preview.md](../includes/ssrs-appliesto-sql2016-preview.md)] |

## Next steps

For more information on how to use these include files, see [Applies-to includes](sql-server-docs-contribute.md#applies-to-includes).
