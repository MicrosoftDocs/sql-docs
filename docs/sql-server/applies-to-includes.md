---
title: "SQL Server Applies | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.component: "sqlcmd"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
caps.latest.revision: 155
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || = sqlallproducts-allversions"
---

# SQL 'Applies to' and 'Includes'
Includes in markdown allow us to have reference text in our documentation that can be easily modified without modifying the actual text in the documentation. There are three types of includes in the SQL world - SQL Versions, Applies to, and referential text. SQL Versions are used to indicate the version of SQL being discussed (SQL 2016 vs 2017), and the applies to indicate what version of SQL the document applies to (SQL on WIndows vs Azure SQL DB). Referential text is anything that doesn't fall in the other two categories, such as the Get Help include - a list of links that customers can use to get help with SQL. 

This article is meant to be used as a reference point for the first two type of includes. 

## SQL Server Version includes

As SQL Content writers, we frequently need to include the name of the product we're discussing, along with the version number. The benefit of using an include is that if the SQL Server version ever changes, instead of changing this value manually in every single topic, we just change the include, and then it updates across the entire documentation set. These were used more frequently in the past, but as a content team we have moved away from it from the most part with the exception of new versions of SQL - such as SQL vNext. Since we don't know what version SQL vNext is going to be, it makes sense to refer to it via an include so that, when the name is made public, we can just modify the include, instead of modifying the text everywhere. 

|SQL Version| File Name| Markdown (may need to modify path) |Text|
| :------------  | :-------------| :----------| :-------------------|
|	SQL 2012	|	sssql11-md.md	|	`[!INCLUDE[ssSQL11](../includes/sssql11-md.md)]`	|	SQL Server 2012 (11.x)	|
|	SQL 2012	|	sssql11_md.md	|	`[!INCLUDE[ssSQL11](../includes/sssql11_md.md)]`	|	SQL Server 2012 (11.x)	|
|	SQL 2012 SP1	|	sssql11sp1-md.md	|	`[!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)]`	|	SQL Server 2012 SP1 (11.0.3x)	|
|	SQL 2014	|	sssql14-md.md	|	`[!INCLUDE[ssSQL14](../includes/sssql14-md.md)]`	|	SQL Server 2014 (12.x)	|
|	SQL 2016	|	sssql15-md.md	|	`[!INCLUDE[sssql15-md](../includes/sssql15-md.md)]`	|	SQL Server 2016 (13.x)	|
|	SQL 2017	|	sssql17-md.md	|	`[!INCLUDE[sssql17-md](../includes/sssql17-md.md)]`	|	SQL Server 2017 (14.x)	|
|	SQL 2017	|	sssqlv14-md.md	|	`[!INCLUDE[sssqlv14](../includes/sssqlv14-md.md)]`	|	SQL Server 2017 (14.x)	|
|	SQL vNext	|	sssqlv15-md.md	|	Idk can't find an example of this, maybe `[!INCLUDE[sssqlv15-md](../includes/sssqlv14-md.md)]` ?	|	SQL Server vNext	|
| &nbsp; | &nbsp; | &nbsp; | &nbsp; |  




## SQL Server Non-Version Specific applies-to

| File Name| Markdown Example |Image|
| :-------------| :----------| :-------------------|
| appliesto-ss-asdb-asdw-xxx-md.md	|	`[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md.md](../includes/appliesto-ss-asdb-asdw-xxx-md.md)]`	| [!INCLUDE[appliesto-ss-asdb-asdw-xxx-md.md](../includes/appliesto-ss-asdb-asdw-xxx-md.md)]	|
|	appliesto-ss-asdb-asdw-pdw-asdbmi-md.md	|	`[!INCLUDE[appliesto-ss-asdb-asdw-pdw-asdbmi-md.md](../includes/appliesto-ss-asdb-asdw-pdw-asdbmi-md.md)]`	| [!INCLUDE[appliesto-ss-asdb-asdw-pdw-asdbmi-md.md](../includes/appliesto-ss-asdb-asdw-pdw-asdbmi-md.md)]	|
|	appliesto-ss-asdb-asdw-pdw-md.md	|	`[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md.md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]`	| [!INCLUDE[appliesto-ss-asdb-asdw-pdw-md.md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]	|
|	appliesto-ss-asdb-xxxx-pdw-md.md	|	`[!INCLUDE[appliesto-ss-asdb-xxxx-pdw-md.md](../includes/appliesto-ss-asdb-xxxx-pdw-md.md)]`	| [!INCLUDE[appliesto-ss-asdb-xxxx-pdw-md.md](../includes/appliesto-ss-asdb-xxxx-pdw-md.md)]	|
|	appliesto-ss-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md.md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md.md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]	|
|	appliesto-ss-asdbmi-xxxx-xxx-md.md	|	`[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md.md](../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]`	| [!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md.md](../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]	|
|	appliesto-ss-xxxx-asdw-pdw-md.md	|	`[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md.md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]`	| [!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md.md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]	|
|	appliesto-ss-xxxx-xxxx-pdw-md.md	|	`[!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md.md](../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)]`	| [!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md.md](../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)]	|
|	appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md	|	`[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]`	| [!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]	|
|	appliesto-ss-xxxx-xxxx-xxx-md-winonly.md	|	`[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]`	| [!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]	|
|	appliesto-ss-xxxx-xxxx-xxx-md.md	|	`[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]`	| [!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md.md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]	|
|	appliesto-xx-asdb-asdw-xxx-md.md	|	`[!INCLUDE[appliesto-xx-asdb-asdw-xxx-md.md](../includes/appliesto-xx-asdb-asdw-xxx-md.md)]`	| [!INCLUDE[appliesto-xx-asdb-asdw-xxx-md.md](../includes/appliesto-xx-asdb-asdw-xxx-md.md)]	|
|	appliesto-xx-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md.md](../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md.md](../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]	|
|	appliesto-xx-xxxx-asdw-pdw-md.md	|	`[!INCLUDE[appliesto-xx-xxxx-asdw-pdw-md.md](../includes/appliesto-xx-xxxx-asdw-pdw-md.md)]`	| [!INCLUDE[appliesto-xx-xxxx-asdw-pdw-md.md](../includes/appliesto-xx-xxxx-asdw-pdw-md.md)]	|
|	appliesto-xx-xxxx-asdw-xxx-md.md	|	`[!INCLUDE[appliesto-xx-xxxx-asdw-xxx-md.md](../includes/appliesto-xx-xxxx-asdw-xxx-md.md)]`	| [!INCLUDE[appliesto-xx-xxxx-asdw-xxx-md.md](../includes/appliesto-xx-xxxx-asdw-xxx-md.md)]	|
|&nbsp; | &nbsp; | &nbsp; |  
 
## SQL Server Version specific applies-to
These applies to specifically indicate which versions of SQL the documentation applies to. 

 File Name| Markdown (may need to modify path) |Image|
| :-------------| :----------| :-------------------|
|	tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-2014sp2-asdb-xxxx-xxx-md.md)]	|
|	tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2008-all-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-all-md.md](../includes/tsql-appliesto-ss2008-all-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-all-md.md](../includes/tsql-appliesto-ss2008-all-md.md)]	|
|	tsql-appliesto-ss2008-asdb-asdw-pdw-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md.md](../includes/tsql-appliesto-ss2008-asdb-asdw-pdw-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md.md](../includes/tsql-appliesto-ss2008-asdb-asdw-pdw-md.md)]	|
|	tsql-appliesto-ss2008-asdb-asdw-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-ss2008-asdb-asdw-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-ss2008-asdb-asdw-xxx-md.md)]	|
|	tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2008-asdb-xxxx-pdw-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-pdw-md.md](../includes/tsql-appliesto-ss2008-asdb-xxxx-pdw-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-pdw-md.md](../includes/tsql-appliesto-ss2008-asdb-xxxx-pdw-md.md)]	|
|	tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md](../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md](../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]	|
|	tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)]	|
|	tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]	|
|	tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2012-all-md.md	|	`[!INCLUDE[tsql-appliesto-ss2012-all-md.md](tsql-appliesto-ss2012-all-md.md)]`	| [!INCLUDE[../includes/tsql-appliesto-ss2012-all-md.md](../includes/tsql-appliesto-ss2012-all-md.md)]	|
|	tsql-appliesto-ss2012-asdb-asdw-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2012-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-ss2012-asdb-asdw-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2012-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-ss2012-asdb-asdw-xxx-md.md)]	|
|	tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2012-xxxx-xxxx-pdw-md.md	|	`[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-pdw-md.md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-pdw-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-pdw-md.md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-pdw-md.md)]	|
|	tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2016-all-md.md	|	`[!INCLUDE[tsql-appliesto-ss2016-all-md.md](tsql-appliesto-ss2016-all-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2016-all-md.md](../includes/tsql-appliesto-ss2016-all-md.md)]	|
|	tsql-appliesto-ss2016-asdb-asdw-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]	|
|	tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md	|	`[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md](../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md](../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]	|
|	tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-ss2016-xxxx-asdw-xxx-md.md)]	|
|	tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md	|	`[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md)]`	| [!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md)]	|
|	tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]	|
|	tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md)]	|
|	t-sql-appliesto-ss-asdbmi-xxxx-pwd-md.md	|	`[!INCLUDE[t-sql-appliesto-ss-asdbmi-xxxx-pwd-md.md](../includes/t-sql-appliesto-ss-asdbmi-xxxx-pwd-md.md)]`	| [!INCLUDE[t-sql-appliesto-ss-asdbmi-xxxx-pwd-md.md](../includes/t-sql-appliesto-ss-asdbmi-xxxx-pwd-md.md)]	|
|	tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md](../includes/tsql-appliesto-xxxxxx-asdb-asdw-xxx-md.md)]	|
|	tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md](../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]	|
|	tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md	|	`[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md](../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]`	| [!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md](../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]	|
|	tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md	|	`[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]`	| [!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md](../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]	|
|	tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md	|	`[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md](../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]`	| [!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md](../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]	|
|&nbsp; | &nbsp; | &nbsp; |  

## Analysis Services applies-to

| File Name| Markdown Example |Image|
| :-------------| :----------| :-------------------|
|	ssas-appliesto-sql2016.md	|	`[!INCLUDE[ssas-appliesto-sql2016.md](../includes/ssas-appliesto-sql2016.md)]`	| [!INCLUDE[ssas-appliesto-sql2016.md](../includes/ssas-appliesto-sql2016.md)]	|
|	ssas-appliesto-sql2016-later.md	|	`[!INCLUDE[ssas-appliesto-sql2016-later.md](../includes/ssas-appliesto-sql2016-later.md)]`	| [!INCLUDE[ssas-appliesto-sql2016-later.md](../includes/ssas-appliesto-sql2016-later.md)]	|
|	ssas-appliesto-sql2016-later-aas.md	|	`[!INCLUDE[ssas-appliesto-sql2016-later-aas.md](../includes/ssas-appliesto-sql2016-later-aas.md)]`	| [!INCLUDE[ssas-appliesto-sql2016-later-aas.md](../includes/ssas-appliesto-sql2016-later-aas.md)]	|
|	ssas-appliesto-sql2017.md	|	`[!INCLUDE[ssas-appliesto-sql2017.md](../includes/ssas-appliesto-sql2017.md)]`	| [!INCLUDE[ssas-appliesto-sql2017.md](../includes/ssas-appliesto-sql2017.md)]	|
|	ssas-appliesto-sql2017-later-aas.md	|	`[!INCLUDE[ssas-appliesto-sql2017-later-aas.md](../includes/ssas-appliesto-sql2017-later-aas.md)]`	| [!INCLUDE[ssas-appliesto-sql2017-later-aas.md](../includes/ssas-appliesto-sql2017-later-aas.md)]	|
|	ssas-appliesto-sqlas.md	|	`[!INCLUDE[ssas-appliesto-sqlas.md](../includes/ssas-appliesto-sqlas.md)]`	| [!INCLUDE[ssas-appliesto-sqlas.md](../includes/ssas-appliesto-sqlas.md)]	|
|	ssas-appliesto-sqlas-aas.md	|	`[!INCLUDE[ssas-appliesto-sqlas-aas.md](../includes/ssas-appliesto-sqlas-aas.md)]`	| [!INCLUDE[ssas-appliesto-sqlas-aas.md](../includes/ssas-appliesto-sqlas-aas.md)]	|
|	ssas-appliesto-sqlas-all.md	|	`[!INCLUDE[ssas-appliesto-sqlas-all.md](../includes/ssas-appliesto-sqlas-all.md)]`	| [!INCLUDE[ssas-appliesto-sqlas-all.md](../includes/ssas-appliesto-sqlas-all.md)]	|
|	ssas-appliesto-sqlas-all-aas.md	|	`[!INCLUDE[ssas-appliesto-sqlas-all-aas.md](../includes/ssas-appliesto-sqlas-all-aas.md)]`	| [!INCLUDE[ssas-appliesto-sqlas-all-aas.md](../includes/ssas-appliesto-sqlas-all-aas.md)]	|
|&nbsp; | &nbsp; | &nbsp; |  

## Reporting Services applies-to

| File Name| Markdown Example |Image|
| :-------------| :----------| :-------------------|
|	ssrs-appliesto-2017-and-later.md	|	`[!INCLUDE[ssrs-appliesto-2017-and-later.md](../includes/ssrs-appliesto-2017-and-later.md)]`	| [!INCLUDE[ssrs-appliesto-2017-and-later.md](../includes/ssrs-appliesto-2017-and-later.md)]	|
|	ssrs-appliesto-not-pbirs.md	|	`[!INCLUDE[ssrs-appliesto-not-pbirs.md](../includes/ssrs-appliesto-not-pbirs.md)]`	| [!INCLUDE[ssrs-appliesto-not-pbirs.md](../includes/ssrs-appliesto-not-pbirs.md)]	|
|	ssrs-appliesto-pbirs.md	|	`[!INCLUDE[ssrs-appliesto-pbirs.md](../includes/ssrs-appliesto-pbirs.md)]`	| [!INCLUDE[ssrs-appliesto-pbirs.md](includes/ssrs-appliesto-pbirs.md)]	|
|	ssrs-appliesto-sharepoint-2013-2016.md	|	`[!INCLUDE[ssrs-appliesto-sharepoint-2013-2016.md](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]`	| [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016.md](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]	|
|	ssrs-appliesto-sql2016-preview.md	|	`[!INCLUDE[ssrs-appliesto-sql2016-preview.md](../includes/ssrs-appliesto-sql2016-preview.md)]`	| [!INCLUDE[ssrs-appliesto-sql2016-preview.md](../includes/ssrs-appliesto-sql2016-preview.md)]	|
|&nbsp; | &nbsp; | &nbsp; |  