---
title: "SQL Server Integration Services (SSIS) Scale Out Worker | Microsoft Docs"
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
# Integration Services (SSIS) Scale Out Worker

Scale Out Worker runs a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion_md](../../includes/ssisnoversion-md.md)] Scale Out Worker service to pull execution tasks from Scale Out Master and executes the packages locally with ISServerExec.exe.

## Configure SQL Server Integration Services Scale Out Worker service
Scale Out Worker service can be configured using the \<driver\>:\Program Files\Microsoft SQL Server\140\DTS\Binn\WorkerSettings.config file. The service must be restarted after updating the configuration file.

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
TaskRequestMaxCPU|The upper limit of CPU for Scale Out Worker to request tasks. **NOT in use in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] 2017.**|70.0         
TaskRequestMinMemory|The lower limit of memory in MB for Scale Out Worker to request tasks. **NOT in use in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] 2017.**|100.0         
MaxTaskCount|The max number of tasks the Scale Out Worker can hold.|10         
LeaseInternval|The lease interval of a task holding by the Scale Out Worker.|00:01:00         
TasksRootFolder|The folder of task logs. The \<driver\>:\Users\\*[account]*\AppData\Local\SSIS\Cluster\Tasks folder path is used if the value is empty. [account] is the account running Scale Out Worker service. By default, the account is SSISScaleOutWorker140.|Empty         
TaskLogLevel|The task log level of the Scale Out Worker. (Verbose 0x01, Information 0x02, Warning 0x04, Error 0x08, Progress 0x10, CriticalError 0x20, Audit 0x40)|126 (Information,Warning,Error,Progress,CriticalError,Audit)     
TaskLogSegment|The time span of a task log file.|00:00:00         
TaskLogEnabled|Specifies whether the task log is enabled.|true         
ExecutionLogCacheFolder|The folder used to cache package execution log. The \<driver\>:\Users\\*[account]*\AppData\Local\SSIS\Cluster\Agent\ELogCache folder path is used if the value is empty. [account] is the account running Scale Out Worker service. By default, the account is SSISScaleOutWorker140.|Empty         
ExecutionLogMaxBufferLogCount|The max number of execution logs cached, in one execution log buffer in memory.|10000        
ExecutionLogMaxInMemoryBufferCount|The max number of execution log buffers in memory for execution logs.|10         
ExecutionLogRetryCount|The retry count if execution logging fails.|3
ExecutionLogRetryTimeout|The retry timeout if execution logging fails. ExecutionLogRetryCount is ignored if ExecutionLogRetryTimeout is reached.|7.00:00:00 (7 days)        
AgentId|Worker agent Id of the Scale Out Worker|Generated automatically        

## View Scale Out Worker log
The log file of Scale Out Worker service is in the \<driver\>:\Users\\*[account]*\AppData\Local\SSIS\ScaleOut\Agent folder path.

The log location of each individual task is configured in the WorkerSettings.config file by TasksRootFolder. If it is not specified, the log is in the \<driver\>:\Users\\*[account]*\AppData\Local\SSIS\ScaleOut\Tasks folder path. 

The *[account]* is the account running Scale Out Worker service. By default, the account is SSISScaleOutWorker140.