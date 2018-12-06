---
title: "SQL Server Integration Services (SSIS) Scale Out Master | Microsoft Docs"
description: "This article describes the Scale Out Master component of SSIS Scale Out"
ms.custom: performance
ms.date: "12/19/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
author: "haoqian"
ms.author: "haoqian"
manager: craigg
---
# Integration Services (SSIS) Scale Out Master
The Scale Out Master manages the Scale Out system through the SSISDB Catalog and the Scale Out Master service. 

The SSISDB Catalog stores all the information for Scale Out Workers, packages, and executions. It provides the interface to enable a Scale Out Worker and execute packages in Scale Out. For more information, see [Walkthrough: Set up Integration Services Scale Out](walkthrough-set-up-integration-services-scale-out.md) and [Run Packages in Integration Services](run-packages-in-integration-services-ssis-scale-out.md).

The Scale Out Master service is a Windows service that is responsible for the communication with Scale Out Workers. It returns the status of package executions on Scale Out Workers over HTTPS and operates on the data in SSISDB. 

## Scale Out views and stored procedures in SSISDB

### Views:
-   [[catalog].[master_properties]](../../integration-services/system-views/catalog-master-properties-ssisdb-database.md)
-   [[catalog].[worker_agents]](../../integration-services/system-views/catalog-worker-agents-ssisdb-database.md).

### Stored procedures:

-   For managing Scale Out Workers:  
    -   [[catalog].[disable_worker_agent]](../../integration-services/system-stored-procedures/catalog-disable-worker-agent-ssisdb-database.md)
    -   [[catalog].[enable_worker_agent]](../../integration-services/system-stored-procedures/catalog-enable-worker-agent-ssisdb-database.md).

- For running packages in Scale Out:   
    -   [[catalog].[create_execution]](../../integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database.md)
    -   [[catalog].[add_execution_worker]](../../integration-services/system-stored-procedures/catalog-add-execution-worker-ssisdb-database.md)
    -   [[catalog].[start_execution]](../../integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database.md).   

## Configure the Scale Out Master service
You configure the Scale Out Master service by using the `\<drive\>:\Program Files\Microsoft SQL Server\140\DTS\Binn\MasterSettings.config` file. You have to restart the service after updating the configuration file.


Configuration  |Description  |Default Value  
---------|---------|---------
PortNumber|The network port number used to communicate with a Scale Out Worker.|8391         
SSLCertThumbprint|The thumbprint of the SSL certificate used to protect  the communication with a Scale Out Worker.|The thumbprint of the SSL certificate specified during the Scale Out Master installation         
SqlServerName|The name of the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] that contains the SSISDB catalog. For example, ServerName\\\\InstanceName.|The name of the SQL Server that is installed with the Scale Out Master.         
CleanupCompletedJobsIntervalInMs|The interval for cleaning up completed execution jobs, in milliseconds.|43200000         
DealWithExpiredTasksIntervalInMs|The interval for dealing with expired execution jobs, in milliseconds.|300000
MasterHeartbeatIntervalInMs|The interval for the Scale Out Master heartbeat, in milliseconds. This property specifies the interval at which Scale Out Master updates its online status in the SSISDB catalog.|30000
SqlConnectionTimeoutInSecs|The SQL connection timeout in seconds when connecting to SSISDB.|15    
||||    

## View the Scale Out Master service log
The Scale Out Master service log file is located in the `\<drive\>:\Users\\[account]\AppData\Local\SSIS\ScaleOut\Master` folder. 

The *[account]* parameter refers to the account running the Scale Out Master service. By default, this account is `SSISScaleOutMaster140`.

## Next steps
[Integration Services (SSIS) Scale Out Worker](integration-services-ssis-scale-out-worker.md)
