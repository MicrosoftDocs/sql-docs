---
description: "Restart Packages by Using Checkpoints"
title: "Restart Packages by Using Checkpoints | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "checkpoints [Integration Services]"
  - "restarting packages"
  - "starting packages"
ms.assetid: 48f2fbb7-8964-484a-8311-5126cf594bfb
author: chugugrace
ms.author: chugu
---
# Restart Packages by Using Checkpoints

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] can restart failed packages from the point of failure, instead of rerunning the whole package. If a package is configured to use checkpoints, information about package execution is written to a checkpoint file. When the failed package is rerun, the checkpoint file is used to restart the package from the point of failure. If the package runs successfully, the checkpoint file is deleted, and then re-created the next time the package is run.  
  
 Using checkpoints in a package can provide the following benefits.  
  
-   Avoid repeating the downloading and uploading of large files. For example, a package that downloads multiple large files by using an FTP task for each download can be restarted after the downloading of a single file fails and then download only that file.  
  
-   Avoid repeating the loading of large amounts of data. For example, a package that performs bulk inserts into dimension tables in a data warehouse using a different Bulk Insert task for each dimension can be restarted if the insertion fails for one dimension table, and only that dimension will be reloaded.  
  
-   Avoid repeating the aggregation of values. For example, a package that computes many aggregates, such as averages and sums, using a separate Data Flow task to perform each aggregation, can be restarted after computing an aggregation fails and only that aggregation will be recomputed.  
  
 If a package is configured to use checkpoints, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] captures the restart point in the checkpoint file. The type of container that fails and the implementation of features such as transactions affect the restart point that is recorded in the checkpoint file. The current values of variables are also captured in the checkpoint file. However, the values of variables that have the **Object** data type are not saved in checkpoint files.  
  
## Defining Restart Points  
 The task host container, which encapsulates a single task, is the smallest atomic unit of work that can be restarted. The Foreach Loop container and a transacted container are also treated as atomic units of work.  
  
 If a package is stopped while a transacted container is running, the transaction ends and any work performed by the container is rolled back. When the package is restarted, the container that failed is rerun. The completion of any child containers of transacted container is not recorded in the checkpoint file. Therefore, when the package is restarted, the transacted container and its child containers run again.  
  
> [!NOTE]  
>  Using checkpoints and transactions in the same package could cause unexpected results. For example, when a package fails and restarts from a checkpoint, the package might repeat a transaction that has already been successfully committed.  
  
 Checkpoint data is not saved for For Loop and Foreach Loop containers. When a package is restarted, the For Loop and Foreach Loop containers and the child containers are run again. If a child container in the loop runs successfully, it is not recorded in the checkpoint file, instead it is rerun. For more information and a workaround, see [SSIS Checkpoints are not honored for For Loop or Foreach Loop container items](/troubleshoot/sql/integration-services/ssis-checkpoints-not-honored-for-loop).  
  
 If the package is restarted the package configurations are not reloaded, instead the package uses the configuration information written to the checkpoint file. This ensures that the package uses the same configurations when it is rerun as the time it failed.  
  
 A package can be restarted only at the control flow level. You cannot restart a package in the middle of a data flow. To avoid rerunning the whole data flow, the package might be designed to include multiple data flows, each one using a different Data Flow task. This way the package can be restarted, rerunning only one Data Flow task.  
  
## Configuring a Package to Restart  
 The checkpoint file includes the execution results of all completed containers, the current values of system and user-defined variables, and package configuration information. The file also includes the unique identifier of the package. To successfully restart a package, the package identifier in the checkpoint file and the package must match; otherwise the restart fails. This prevents a package from using a checkpoint file written by a different package version. If the package runs successfully, after it is restarted the checkpoint file is deleted.  
  
 The following table lists the package properties that you set to implement checkpoints.  
  
|Property|Description|  
|--------------|-----------------|  
|CheckpointFileName|Specifies the name of the checkpoint file.|  
|CheckpointUsage|Specifies whether checkpoints are used.|  
|SaveCheckpoints|Indicates whether the package saves checkpoints. This property must be set to True to restart a package from a point of failure.|  
  
 Additionally, you must set the FailPackageOnFailure property to **true** for all the containers in the package that you want to identify as restart points.  
  
 You can use the ForceExecutionResult property to test the use of checkpoints in a package. By setting ForceExecutionResult of a task or container to Failure, you can imitate real-time failure. When you rerun the package, the failed task and containers will be rerun.  
  
### Checkpoint Usage  
 The CheckpointUsage property can be set to the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|**Never**|Specifies that the checkpoint file is not used and that the package runs from the start of the package workflow.|  
|**Always**|Specifies that the checkpoint file is always used and that the package restarts from the point of the previous execution failure. If the checkpoint file is not found, the package fails.|  
|**IfExists**|Specifies that the checkpoint file is used if it exists. If the checkpoint file exists, the package restarts from the point of the previous execution failure; otherwise, it runs from the start of the package workflow.|  
  
> [!NOTE]  
>  The **/CheckPointing on** option of dtexec is equivalent to setting the **SaveCheckpoints** property of the package to **True**, and the **CheckpointUsage** property to Always. For more information, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).  
  
## Securing Checkpoint Files  
 Package level protection does not include protection of checkpoint files and you must secure these files separately. Checkpoint data can be stored only in the file system and you should use an operating system access control list (ACL) to secure the location or folder where you store the file. It is important to secure checkpoint files because they contain information about the package state, including the current values of variables. For example, a variable may contain a recordset with many rows of private data such as telephone numbers. For more information, see [Access to Files Used by Packages](../../integration-services/security/security-overview-integration-services.md#files).  

## Configure Checkpoints for Restarting a Failed Package
  You configure [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages to restart from a point of failure, instead of rerunning the entire package, by setting the properties that apply to checkpoints.  
  
### To configure a package to restart  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want to configure.  
  
2.  In **Solution Explorer**, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  Right-click anywhere in the background of the control flow design surface, and then click **Properties**.  
  
5.  Set the SaveCheckpoints property to **True**.  
  
6.  Type the name of the checkpoint file in the CheckpointFileName property.  
  
7.  Set the CheckpointUsage property to one of two values:  
  
    -   Select **Always** to always restart the package from the checkpoint.  
  
        > [!IMPORTANT]  
        >  An error occurs if the checkpoint file is not available.  
  
    -   Select **IfExists** to restart the package only if the checkpoint file is available.  
  
8.  Configure the tasks and containers from which the package can restart.  
  
    -   Right-click a task or container and click **Properties**.  
  
    -   Set the FailPackageOnFailure property to **True** for each selected task and container.  
    
## External Resources  
  
-   Technical article, [Automatic Restart of SSIS packages after Failover or Failure](https://go.microsoft.com/fwlink/?LinkId=200407), on social.technet.microsoft.com  
  
-   Support article, [SSIS Checkpoints are not honored for For Loop or Foreach Loop container items](/troubleshoot/sql/integration-services/ssis-checkpoints-not-honored-for-loop), on support.microsoft.com.