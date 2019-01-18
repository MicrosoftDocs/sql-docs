---
title: "SQL Server Integration Services (SSIS) Scale Out Worker | Microsoft Docs"
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
# Integration Services (SSIS) Scale Out Worker

The Scale Out Worker runs the Scale Out Worker service to pull execution tasks from Scale Out Master. Then the worker runs the packages locally with `ISServerExec.exe`.

## Configure the Scale Out Worker service
You can configure the Scale Out Worker service by using the` \<drive\>:\Program Files\Microsoft SQL Server\140\DTS\Binn\WorkerSettings.config` file. You have to restart the service after updating the configuration file.

Configuration  |Description  |Default value  
---------|---------|---------
DisplayName|The display name of the Scale Out Worker. **NOT in use in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] 2017.**|Machine name         
Description|The description of the Scale Out Worker. **NOT in use in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] 2017.**|Empty         
MasterEndpoint|The endpoint to connect to Scale Out Master.|The endpoint set during the Scale Out Worker installation         
MasterHttpsCertThumbprint|The thumbprint of the client SSL certificate used to authenticate Scale Out Master|The thumbprint of the client certificate specified during the Scale Out Worker installation.          
WorkerHttpsCertThumbprint|The thumbprint of the certificate for Scale Out Master used to authenticate the Scale Out Worker.|The thumbprint of a certificate created and installed automatically during the Scale Out Worker installation          
StoreLocation|The store location of worker certificate.|LocalMachine       
StoreName|The store name that worker certificate is in.|My         
AgentHeartbeatInterval|The interval of the Scale Out Worker heartbeat.|00:01:00         
TaskHeartbeatInterval|The interval of the Scale Out Worker reporting task state.|00:00:10         
HeartbeatErrorTollerance|After this time period from last successful task heartbeat, the task is terminated if error response of heartbeat is received.|00:10:00      
TaskRequestMaxCPU|The upper limit of CPU for Scale Out Worker to request tasks.|70.0         
TaskRequestMinMemory|The lower limit of memory in MB for Scale Out Worker to request tasks.|100.0         
MaxTaskCount|The max number of tasks the Scale Out Worker can hold.|10         
LeaseInternval|The lease interval of a task holding by the Scale Out Worker.|00:01:00         
TasksRootFolder|The folder of task logs. If the value is empty, the `\<drive\>:\Users\[account]\AppData\Local\SSIS\Cluster\Tasks` folder path is used. [account] is the account running Scale Out Worker service. By default, the account is SSISScaleOutWorker140.|Empty         
TaskLogLevel|The task log level of the Scale Out Worker. (Verbose 0x01, Information 0x02, Warning 0x04, Error 0x08, Progress 0x10, CriticalError 0x20, Audit 0x40)|126 (Information, Warning, Error, Progress, CriticalError, Audit)     
TaskLogSegment|The time span of a task log file.|00:00:00         
TaskLogEnabled|Specifies whether the task log is enabled.|true         
ExecutionLogCacheFolder|The folder used to cache package execution log. If the value is empty, the` \<drive\>:\Users\[account]\AppData\Local\SSIS\Cluster\Agent\ELogCache` folder path is used. [account] is the account running Scale Out Worker service. By default, the account is SSISScaleOutWorker140.|Empty         
ExecutionLogMaxBufferLogCount|The max number of execution logs cached, in one execution log buffer in memory.|10000        
ExecutionLogMaxInMemoryBufferCount|The max number of execution log buffers in memory for execution logs.|10         
ExecutionLogRetryCount|The retry count if execution logging fails.|3
ExecutionLogRetryTimeout|The retry timeout if execution logging fails. i\If ExecutionLogRetryTimeout is reached, ExecutionLogRetryCount is ignored. |7.00:00:00 (7 days)        
AgentId|Worker agent ID of the Scale Out Worker|Generated automatically    
||||    

## View the Scale Out Worker log
The log file of the Scale Out Worker service is in the `\<drive\>:\Users\\[account]\AppData\Local\SSIS\ScaleOut\Agent` folder.

The log location of each individual task is configured in the `WorkerSettings.config` file in the `TasksRootFolder`. If a value is not specified, the log is in the `\<drive\>:\Users\\[account]\AppData\Local\SSIS\ScaleOut\Tasks` folder. 

The *[account]* parameter is the account running the Scale Out Worker service. By default, the account is `SSISScaleOutWorker140`.

## Next steps
[Integration Services (SSIS) Scale Out Master](integration-services-ssis-scale-out-master.md)
