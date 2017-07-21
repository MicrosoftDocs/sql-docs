---
title: "SQL Server Integration Services (SSIS) Scale Out Master | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 1
author: "haoqian"
ms.author: "haoqian"
manager: "jhubbard"
---
# Integration Services (SSIS) Scale Out Master
Scale Out Master manages the Scale Out system through the SSISDB Catalog and the Scale Out Master service. 

The SSISDB Catalog stores all the information for Scale Out Workers, packages and executions. It provides the interface to enable a Scale Out Worker and execute packages in Scale Out. For more information, see [Walkthrough: Set up Integration Services Scale Out](walkthrough-set-up-integration-services-scale-out.md), [Run Packages in Integration Services](run-packages-in-integration-services-ssis-scale-out.md).

Scale Out Master service is a Windows service that is responsible for the communication with Scale Out Workers. It exchanges the status of package executions with Scale Out Workers through HTTPS and operates on the data in SSISDB. 

## Scale Out related SQL views and stored procedures in SSISDB

#### Views:
[[catalog].[master_properties]](../../integration-services/system-views/catalog-master-properties-ssisdb-database.md), [[catalog].[worker_agents]](../../integration-services/system-views/catalog-worker-agents-ssisdb-database.md).

#### Stored procedures:

- For Scale Out Worker management:  
 [[catalog].[disable_worker_agent]](../../integration-services/system-stored-procedures/catalog-disable-worker-agent-ssisdb-database.md), [[catalog].[enable_worker_agent]](../../integration-services/system-stored-procedures/catalog-enable-worker-agent-ssisdb-database.md).
- For executing packages in Scale Out:   
[[catalog].[create_execution]](../../integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database.md), [[catalog].[add_execution_worker]](../../integration-services/system-stored-procedures/catalog-add-execution-worker-ssisdb-database.md), [[catalog].[start_execution]](../../integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database.md).   

## Configure SQL Server Integration Services Scale Out Master service
Scale Out Master service can be configured using the \<driver\>:\Program Files\Microsoft SQL Server\140\DTS\Binn\MasterSettings.config file. The service must be restarted after updating the configuration file.


Configuration  |Description  |Default Value  
---------|---------|---------
PortNumber|The network port number used to communicate with a Scale Out Worker.|8391         
SSLCertThumbprint|The thumbprint of the SSL certificate used to protect  the communication with a Scale Out Worker.|The thumbprint of the SSL certificate specified during the Scale Out Master installation         
SqlServerName|The name of the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] that contains the SSISDB catalog. E.g. ServerName\\\\InstanceName.|The name of the SQL Server that is installed with the Scale Out Master.         
CleanupCompletedJobsIntervalInMs|The interval for cleaning up completed execution jobs, in milliseconds.|43200000         
DealWithExpiredTasksIntervalInMs|The interval for dealing with expired execution jobs, in milliseconds.|300000
MasterHeartbeatIntervalInMs|The interval for the Scale Out Master heartbeat, in milliseconds. This specifies the interval that Scale Out Master updates it's online status in the SSISDB catalog.|30000
SqlConnectionTimeoutInSecs|The SQL connection timeout in seconds when connecting to SSISDB.|15        

## View Scale Out Master service log
The Scale Out Master service log file is located in the \<driver\>:\Users\\*[account]*\AppData\Local\SSIS\Cluster\Master folder path. 

The *[account]* folder refers to the account running Scale Out Master service. By default, this account is SSISScaleOutMaster140.
