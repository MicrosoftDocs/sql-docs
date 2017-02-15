---
title: "Automated Administration Across an Enterprise | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "enterprise automatic administration [SQL Server]"
  - "multiserver administration [SQL Server]"
  - "SQL Server Agent jobs, multiserver administration"
  - "jobs [SQL Server Agent], multiserver administration"
  - "master servers [SQL Server], about master servers"
  - "target servers [SQL Server], about target servers"
  - "master servers [SQL Server]"
  - "multiple instances of SQL Server"
  - "target servers [SQL Server]"
ms.assetid: 44d8365b-42bd-4955-b5b2-74a8a9f4a75f
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Automated Administration Across an Enterprise
Automating administration across multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] is called *multiserver administration*. Use multiserver administration to do the following:  
  
-   Manage two or more servers.  
  
-   Schedule information flows between enterprise servers for data warehousing.  
  
To take advantage of multiserver administration, you must have at least one master server and at least one target server. A master server distributes jobs to, and receives events from, target servers. A master server also stores the central copy of job definitions for jobs that are run on target servers. Target servers connect periodically to the master server to update their schedule of jobs. If a new job exists on the master server, the target server downloads the job. After the target server completes the job, it reconnects to the master server and reports the status of the job. Note that your job definition must be same when performing any database related activities.  
  
The following illustration shows the relationship between master and target servers:  
  
![Multiserver administration configuration](../../ssms/agent/media/multisvr.gif "Multiserver administration configuration")  
  
If you administer departmental servers across a large corporation, you can define the following:  
  
-   One backup job with job steps.  
  
-   Operators to notify in case of backup failure.  
  
-   An execution schedule for the backup job.  
  
Write this backup job one time on the master server and then enlist each departmental server as a target server. From the time of their enlistment, all the departmental servers run the same backup job, yet you defined the job only once.  
  
> [!NOTE]  
> Multiserver administration features are intended for members of the sysadmin role. However, a member of the sysadmin role on the target server cannot edit the operations that are performed on the target server by the master server. This security measure prevents job steps from being accidentally deleted and operations on the target server from being interrupted.  
  
## In This Section  
[Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md)  
Contains information about how to create and manage master and target servers.  
  
[Choose the Right SQL Server Agent Service Account for Multiserver Environments](../../ssms/agent/choose-the-right-sql-server-agent-service-account-for-multiserver-environments.md)  
Contains information about how using nonadministrative Windows accounts or the Local System account for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent service can affect multiserver environments.  
  
[Set Encryption Options on Target Servers](../../ssms/agent/set-encryption-options-on-target-servers.md)  
Contains information about setting the MsxEncryptChannelOptions[!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent registry subkey on target servers.  
  
[Manage Jobs Across an Enterprise](../../ssms/agent/manage-jobs-across-an-enterprise.md)  
Contains information about checking job status, changing target servers for jobs, synchronizing target server clocks, and polling master servers for their current job status.  
  
[Troubleshoot Multiserver Jobs That Use Proxies](../../ssms/agent/troubleshoot-multiserver-jobs-that-use-proxies.md)  
Contains information about troubleshooting multiserver jobs that use proxies which fail.  
  
[Poll Servers](../../ssms/agent/poll-servers.md)  
Contains information about how to implicitly and explicitly make target servers poll master servers to synchronize jobs information.  
  
[Manage Events](../../ssms/agent/manage-events.md)  
Contains information about event forwarding from target servers to master servers.  
  
[Tune Automated Administration Across an Enterprise](../../ssms/agent/tune-automated-administration-across-an-enterprise.md)  
Contains information about how automated administration in a multiserver environment takes advantage of the self-tuning features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
## See Also  
[Backward Compatibility Topics for Installing the SQL Server Database Engine](http://msdn.microsoft.com/en-us/10de5ec6-d3cf-42ef-aa62-1bdf3fbde841)  
[Registering Servers](http://msdn.microsoft.com/en-us/c2a2513e-fa09-419c-99e7-a12d57c5a0db)  
[sp_add_targetservergroup](http://msdn.microsoft.com/en-us/acb69343-d766-46ff-b771-0c7655c5231a)  
[sp_delete_targetserver](http://msdn.microsoft.com/en-us/cc438701-ad91-419d-9f23-ebc4c548c700)  
[sp_delete_targetservergroup](http://msdn.microsoft.com/en-us/d8dd838e-64aa-419f-9ccb-ff04908cf3e4)  
[sp_help_downloadlist](http://msdn.microsoft.com/en-us/745b265b-86e8-4399-b928-c6969ca1a2c8)  
[sp_help_jobserver](http://msdn.microsoft.com/en-us/57971787-f9f5-4199-9f64-c2b61a308906)  
[sp_help_targetservergroup](http://msdn.microsoft.com/en-us/ec3a4a68-b591-431c-9518-053ede522d0c)  
[sp_resync_targetserver](http://msdn.microsoft.com/en-us/40e44df7-d3e3-44ee-b149-08aba629a21f)  
[sp_update_targetservergroup](http://msdn.microsoft.com/en-us/4ac65ed6-e07e-40e4-a282-13bfd92dfa41)  
[sysjobservers](http://msdn.microsoft.com/en-us/9abcc20f-a421-4591-affb-62674d04575e)  
[syslogins](http://msdn.microsoft.com/en-us/4cb34f17-a4bb-469f-a218-71f074e6308f)  
[systargetservers](http://msdn.microsoft.com/en-us/479d1314-be37-4d19-ac9c-419fc9110e53)  
  
