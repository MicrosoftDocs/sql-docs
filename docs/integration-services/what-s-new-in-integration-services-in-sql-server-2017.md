---
title: "What&#39;s New in Integration Services in SQL Server 2017 | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: e26d7884-e772-46fa-bfdc-38567fe976a1
caps.latest.revision: 4
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# What&#39;s New in Integration Services in SQL Server 2017
This topic describes the features that have been added or updated in [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].

>   [!NOTE]
> SQL Server 2017 also includes the features of SQL Server 2016 and the features added in SQL Server 2016 updates. For info about the new SSIS features in SQL Server 2016, see [What's New in Integration Services in SQL Server 2016](../integration-services/what-s-new-in-integration-services-in-sql-server-2016.md).

## New in SSIS in SQL Server 2017 CTP 2.1

### New features in Scale Out for SSIS

1.	You can now use the **Use32BitRuntime** parameter when you trigger execution in Scale Out.
2.	The performance of logging to SSISDB for package executions in Scale Out has been improved. The Event Message and Message Context logs are now written to SSISDB in batch mode instead of one by one. Here are some additional notes about this improvement:        
    - Some reports in the current version of SQL Server Management Studio (SSMS) don’t currently display these logs for executions in Scale Out. We anticipate that they will be supported in the next release of SSMS. The affected reports include the *All Connections* report, the *Error Context* report, and the *Connection Information* section in the Integration Service Dashboard.
    - A new column **event_message_guid** has been added. Use this column to join the [catalog].[event_message_context] view and the [catalog].[event_messages] view instead of using **event_message_id** when you query these logs of executions in Scale Out.

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

