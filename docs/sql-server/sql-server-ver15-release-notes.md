---
title: "SQL Server 2019 Release Notes | Microsoft Docs"
ms.custom: ""
ms.date: "09/24/2018"
ms.prod: "sql-server-2018"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 13942af8-5a40-4cef-80f5-918386767a47
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "craigg"
monikerRange: "= sql-server-ver15 || = sqlallproducts-allversions"
---
# SQL Server 2019 Release Notes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes limitations and known issues for the [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] Community Technology Preview (CTP) releases. For related information, see:
- [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md)

**Try [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] !**
- [![Download from Evaluation Center](../includes/media/download2.png)](http://go.microsoft.com/fwlink/?LinkID=829477) [Download [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] to install on Windows](http://go.microsoft.com/fwlink/?LinkID=829477)
- Install on Linux for [Red Hat Enterprise Server](../linux/quickstart-install-connect-red-hat.md), [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md), and [Ubuntu](../linux/quickstart-install-connect-ubuntu.md).
- [Run on SQL Server 2019 on Docker](../linux/quickstart-install-connect-docker.md).

## CTP 2.0 (September 2018)

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 2.0 is the first public release of [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)].

### SQL Graph

**Issue and customer impact**: Tools which have dependency on DacFx like import-export will not work for the new graph features - Edge Constraints or Merge DML. Scripting in SSMS may not work.

**Workaround**: Writing T-SQL scripts and running them against the server using SSMS or SQLCMD will work. Exporting or Importing database objects that create Edge constraints, have the new merge DML syntax or create derived tables/views on graph objects will not work. Users will have to manually create such objects in their database using t-sql scripts. 

**Applies to**:  [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 2.0.

### UTF-8 collations

**Issue and customer impact**: UTF-8 enabled collations cannot be used with some other SQL Server features. UTF-8 is not supported when the following SQL Server features are in use:

- SQL Server Replication
- Linked Server
- In-memory OLTP
- External Table for Polybase

  Also note there is currently no UI support to choose UTF-8 enabled collations in Azure Data Studio or SSDT. The latest SSMS version supports choice of UTF-8 enabled collations in the UI.

**Workaround**: No workaround for SQL Server 2019 CTP 2.0.

**Applies to**: SQL Server 2019 CTP 2.0

[!INCLUDE[get-help-options-msft-only](../includes/paragraph-content/get-help-options.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)