---
title: "What&#39;s New in Integration Services in SQL Server 2017 | Microsoft Docs"
ms.custom: ""
ms.date: "09/28/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: e26d7884-e772-46fa-bfdc-38567fe976a1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# What&#39;s New in Integration Services in SQL Server 2017
This topic describes the features that have been added or updated in [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].

> [!NOTE]
> SQL Server 2017 also includes the features of SQL Server 2016 and the features added in SQL Server 2016 updates. For info about the new SSIS features in SQL Server 2016, see [What's New in Integration Services in SQL Server 2016](../integration-services/what-s-new-in-integration-services-in-sql-server-2016.md).

## Highlights of this release

Here are the most important new features of Integration Services in SQL Server 2017.

-   **Scale Out**. Distribute SSIS package execution more easily across multiple worker computers, and manage executions and workers from a single master computer. For more info, see [Integration Services Scale Out](../integration-services/scale-out/integration-services-ssis-scale-out.md).

-   **Integration Services on Linux**. Run SSIS packages on Linux computers. For more info, see [Extract, transform, and load data on Linux with SSIS](../linux/sql-server-linux-migrate-ssis.md).

-   **Connectivity improvements**. Connect to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online with the updated OData components. 

## New in Azure Data Factory

With the public preview of Azure Data Factory version 2 in September 2017, you can now do the following things:
-   Deploy packages to the SSIS Catalog database (SSISDB) on Azure SQL Database.
-   Run packages deployed to Azure on the Azure-SSIS Integration Runtime, a component of Azure Data Factory version 2.

For more info, see [Lift and shift SQL Server Integration Services workloads to the cloud](lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).

These new capabilities require SQL Server Data Tools (SSDT) version 17.2 or later, but do not require SQL Server 2017 or SQL Server 2016. When you deploy packages to Azure, the Package Deployment Wizard always upgrades the packages to the latest package format.

## New in the Azure Feature Pack

In addition to the connectivity improvements in SQL Server, the Integration Services Feature Pack for Azure has added support for Azure Data Lake Store. For more info, see the blog post [New Azure Feature Pack Release Strengthening ADLS Connectivity](https://blogs.msdn.microsoft.com/ssis/2017/08/29/new-azure-feature-pack-release-strengthening-adls-connectivity/). Also see [Azure Feature Pack for Integration Services (SSIS)](azure-feature-pack-for-integration-services-ssis.md).

## New in SQL Server Data Tools (SSDT)

You can now develop SSIS projects and packages that target SQL Server versions 2012 through 2017 in Visual Studio 2017 or in Visual Studio 2015. For more info, see [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md).

## New in SSIS in SQL Server 2017 RC1

### New and changed features in Scale Out for SSIS

-   Scale Out Master now supports high availability. You can enable Always On for SSISDB and set up Windows Server failover clustering for the server that hosts the Scale Out Master service. By applying this change to Scale Out Master, you avoid a single point of failure and provide high availability for the entire Scale Out deployment.
-   The failover handling of the execution logs from Scale Out Workers is improved. The execution logs are persisted to local disk in case the Scale Out Worker stops unexpectedly. Later, when the worker restarts, it reloads the persisted logs and continues saving them to SSISDB.
-   The parameter *runincluster* of the stored procedure **[catalog].[create_execution]** is renamed to *runinscaleout* for consistency and readability. This change of parameter name has the following impact:
    -   If you have existing scripts to run packages in Scale Out, you have to change the parameter name from *runincluster* to *runinscaleout* to make the scripts work in RC1.
    -   SQL Server Management Studio (SSMS) 17.1 and earlier versions can't trigger package execution in Scale Out in RC1. The error message is: "*@runincluster* is not a parameter for procedure **create_execution**." This issue is fixed in the next release of SSMS, version 17.2. Version 17.2 and later of SSMS support the new parameter name and package execution in Scale Out. Until SSMS version 17.2 is available, as a workaround, you can use your existing version of SSMS to generate the package execution script, then change the name of the *runincluster* parameter to *runinscaleout* in the script, and run the script.
-   The SSIS Catalog has a new global property to specify the default mode for executing SSIS packages. This new property applies when you call the **[catalog].[create_execution]** stored procedure with the *runinscaleout* parameter set to null. This mode also applies to SSIS SQL Agent jobs. You can set the new global property in the Properties dialog box for the SSISDB node in SSMS, or with the following command:
    ```sql
    EXEC [catalog].[configure_catalog] @property_name=N'DEFAULT_EXECUTION_MODE', @property_value=1
    ```

## New in SSIS in SQL Server 2017 CTP 2.1

### New and changed features in Scale Out for SSIS

-   You can now use the **Use32BitRuntime** parameter when you trigger execution in Scale Out.
-   The performance of logging to SSISDB for package executions in Scale Out has been improved. The Event Message and Message Context logs are now written to SSISDB in batch mode instead of one by one. Here are some additional notes about this improvement:        
    - Some reports in the current version of SQL Server Management Studio (SSMS) don't currently display these logs for executions in Scale Out. We anticipate that they will be supported in the next release of SSMS. The affected reports include the *All Connections* report, the *Error Context* report, and the *Connection Information* section in the Integration Service Dashboard.
    - A new column **event_message_guid** has been added. Use this column to join the [catalog].[event_message_context] view and the [catalog].[event_messages] view instead of using **event_message_id** when you query these logs of executions in Scale Out.
-   To get the management application for SSIS Scale Out, [download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) 17.1 or later.

## New in SSIS in SQL Server 2017 CTP 2.0

There are no new SSIS features in SQL Server 2017 CTP 2.0.

## New in SSIS in SQL Server 2017 CTP 1.4

There are no new SSIS features in SQL Server 2017 CTP 1.4.

## New in SSIS in SQL Server 2017 CTP 1.3

There are no new SSIS features in SQL Server 2017 CTP 1.3.

## New in SSIS in SQL Server 2017 CTP 1.2

There are no new SSIS features in SQL Server 2017 CTP 1.2.

## New in SSIS in SQL Server 2017 CTP 1.1

There are no new SSIS features in SQL Server 2017 CTP 1.1.

## New in SSIS in SQL Server 2017 CTP 1.0

### Scale Out for SSIS

The Scale Out feature makes it much easier to run [!INCLUDE[ssIS_md](../includes/ssis-md.md)] on multiple machines. 
   
After installing the Scale Out Master and Workers, the package can be distributed to execute on different Workers automatically. If the execution is terminated unexpectedly, the execution is retried automatically. Also, all the executions and Workers can be centrally managed using the Master.
   
For more information, see [Integration Services Scale Out](../integration-services/scale-out/integration-services-ssis-scale-out.md).
   
### Support for Microsoft Dynamics Online Resources

The OData Source and OData Connection Manager now support connecting to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online.

