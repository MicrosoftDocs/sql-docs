---
title: "Run Packages in Integration Services (SSIS) Scale Out | Microsoft Docs"
ms.custom: ""
ms.date: "2016-12-16"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 4c2f4cd7-9c47-416d-bb1e-40acc3e05342
caps.latest.revision: 5
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Run Packages in Integration Services (SSIS) Scale Out
After the packages are deployed to the Integration Services server, you can execute them in Scale Out.

## Run packages with **Execute Package In Scale Out** dialog 

1. ### Open the Execute Package In Scale Out dialog box ###
    In [!INCLUDE[ssManStudioFull_md](../includes/ssmanstudiofull-md.md)], connect to the Integration Services server. In Object Explorer, expand the tree to display the nodes under **Integration Services Catalogs**. Right-click the **SSISDB** node or the project or the package you want to run, and then click **Execute in Scale Out**.
2. ### Select packages and set the options ###
    On the **Package Selection** page, you select multiple packages to run and set the environment, parameters, connection managers, and advanced options for each package. Click a package to set these options.
    
    In the **Advanced** tab, you set a Scale Out option called **Retry count**. It sets the number of times a package execution will retry if it fails.
3. ### Select machines ###
    On the **Machine Selection** page, you select the Scale Out Worker machines to run the packages. By default, any machine is allowed to run the packages. 

   > [!NOTE]
> The packages are executed with the credential of the user accounts of the Scale Out Worker services, which are shown on the **Machine Selection** page. By default, the account is NT Service\SSISScaleOutWorker140. You may want to change to your own lab accounts.

4. ### Run the packages and view reports 
    Click **OK** to start the package executions. To view the execution report for a package, right-click the package in Object Explorer, click **Reports**, click **All Executions**, and find the execution.
    
    
## Run packages with stored procedures

1. ### Create executions ###
    Call [catalog].[create_execution] for each package. Set parameter **@runincluster** to True. If not all Scale Out Worker machines are allowed to run the package, set parameter **@useanyworker** to False.   
2. ### Set execution parameters ###
    Call [catalog].[set_execution_parameter_value] for each execution.
3. ### Set Scale Out Workers ###
    Call [catalog].[add_execution_worker]. If any machine is allowed to run the package, you do not need to call this stored procedure. 
4. ### Start executions ###
    Call [catalog].[start_execution]. Set parameter **@retry_count** to set the number of times a package execution will retry if it fails.
    
    ### Example  ###  
    The following example runs two packages, package1.dtsx and package2.dtsx, in Scale Out with one Scale Out Worker.  
    ```
    Declare @execution_id bigint
    EXEC [SSISDB].[catalog].[create_execution] @package_name=N'package1.dtsx', @execution_id=@execution_id OUTPUT, @folder_name=N'folder1', @project_name=N'project1', @use32bitruntime=False, @reference_id=Null, @useanyworker=False, @runincluster=True
    Select @execution_id
    DECLARE @var0 smallint = 1
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=50, @parameter_name=N'LOGGING_LEVEL', @parameter_value=@var0
    EXEC [SSISDB].[catalog].[add_execution_worker] @execution_id,  @workeragent_id=N'64c020e2-f819-4c2d-a22f-efb31a91e70a'
    EXEC [SSISDB].[catalog].[start_execution] @execution_id,  @retry_count=0
    GO

    Declare @execution_id bigint
    EXEC [SSISDB].[catalog].[create_execution] @package_name=N'package2.dtsx', @execution_id=@execution_id OUTPUT, @folder_name=N'folder2', @project_name=N'project2', @use32bitruntime=False, @reference_id=Null, @useanyworker=False, @runincluster=True
    Select @execution_id
    DECLARE @var0 smallint = 1
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=50, @parameter_name=N'LOGGING_LEVEL', @parameter_value=@var0
    EXEC [SSISDB].[catalog].[add_execution_worker] @execution_id,  @workeragent_id=N'64c020e2-f819-4c2d-a22f-efb31a91e70a'
    EXEC [SSISDB].[catalog].[start_execution] @execution_id,  @retry_count=0
    GO
    ```


## Permissions
Running packages in Scale Out requires one the following permissions:

-   Membership in the **ssis_admin** database role  

-   Membership in the **ssis_cluster_executor** database role  
  
-   Membership in the **sysadmin** server role  
    