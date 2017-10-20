---
title: "SQL Server 2017 Release Notes | Microsoft Docs"
ms.custom: ""
ms.date: "10/20/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 13942af8-5a40-4cef-80f5-918386767a47
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# SQL Server 2017 Release Notes
This topic describes limitations and issues with SQL Server 2017. For related information, see:
- [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md)
- [SQL Server on Linux release notes](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-release-notes)
- [SQL Server 2017 Cumulative updates](http://aka.ms/sql2017cu) for information about the latest cumulative update (CU) release

**Try SQL Server!**
- [![Download from Evaluation Center](../includes/media/download2.png)](http://go.microsoft.com/fwlink/?LinkID=829477) [Download SQL Server 2017](http://go.microsoft.com/fwlink/?LinkID=829477)
- [![Create Virtual Machine](../includes/media/azure-vm.png)](https://azure.microsoft.com/en-us/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm) [Spin up a Virtual Machine with SQL Server 2017](https://azure.microsoft.com/en-us/services/virtual-machines/sql-server/?wt.mc_id=sqL16_vm)

## SQL Server 2017 - general availability release (October 2017)
### Database Engine

- **Issue and customer impact:** After upgrade, the existing FILESTREAM network share may be no longer available.

- **Workaround:** First, reboot the computer and check if the FILESTREAM network share is available. If the share is still not available, do the following:

    1. In SQL Server Configuration Manager, right click the SQL Server instance, and click **Properties**. 
    2. In the **FILESTREAM** tab clear **Enable FILESTREAM for file I/O streaming access** , then click **Apply**.
    3. Check **Enable FILESTREAM for file I/O streaming access** again with the original share name and click **Apply**.

### Master Data Services (MDS)
- **Issue and customer impact:** 
On the user permissions page, when granting permission to the root level in the entity tree view, you see the following error:
`"The model permission cannot be saved. The object guid is not valid"`

- **Workarounds:** 
  - Grant permission on the sub nodes in the tree view instead of the root level.
  - or
  - Run the script described in this MDS team blog [error applying permission on entity level](http://sqlblog.com/blogs/mds_team/archive/2017/09/05/sql-server-2016-sp1-cu4-regression-error-while-applying-permission-on-entity-level-quick-workaround.aspx)

### Analysis Services
- **Issue and customer impact:** For tabular models at the 1400 compatibility level, when using Get Data, data connectors for some data sources such as  Amazon Redshift, IBM Netezza, and Impala, are not yet available.
- **Workaround:** None.   

- **Issue and customer impact:** Direct Query models at the 1400 compatibility level with perspectives can fail on querying or discovering metadata.
- **Workaround:** Remove perspectives and re-deploy.

### Tools
- **Issue and customer impact:** Running *DReplay* fails with the following message: "Error DReplay Unexpected error occurred!".
- **Workaround:** None.

![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 Release Candidate (RC2 - August 2017)
There are no SQL Server on Windows release notes for this release. See [SQL Server on Linux Release notes](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-release-notes).


![horizontal_bar](../sql-server/media/horizontal-bar.png)
## SQL Server 2017 Release Candidate (RC1 - July 2017)
### SQL Server Integration Services (SSIS) (RC1 - July 2017)
- **Issue and customer impact:** The parameter *runincluster* of the stored procedure **[catalog].[create_execution]** is renamed to *runinscaleout* for consistency and readability.
- **Workaround:** If you have existing scripts to run packages in Scale Out, you have to change the parameter name from *runincluster* to *runinscaleout* to make the scripts work in RC1.

- **Issue and customer impact:** SQL Server Management Studio (SSMS) 17.1 and earlier versions can't trigger package execution in Scale Out in RC1. The error message is: "*@runincluster* is not a parameter for procedure **create_execution**." This issue is fixed in the next release of SSMS, version 17.2. Versions 17.2 and later of SSMS support the new parameter name and package execution in Scale Out. 
- **Workaround:** Until SSMS version 17.2 is available:
  1. Use your existing version of SSMS to generate the package execution script.
  2. Change the name of the *runincluster* parameter to *runinscaleout* in the script.
  3. Run the script.

## More information
- [SQL Server Reporting Services release notes](../reporting-services/reporting-services-release-notes.md).
- [Known Issues for Machine Learning Services](../advanced-analytics/known-issues-for-sql-server-machine-learning-services.md)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)