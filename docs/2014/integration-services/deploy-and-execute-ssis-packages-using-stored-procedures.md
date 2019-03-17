---
title: "Deploy and Execute SSIS Packages using Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
ms.assetid: 60914b0c-1f65-45f8-8132-0ca331749fcc
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Deploy and Execute SSIS Packages using Stored Procedures
  When you configure an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project to use the project deployment model, you can use stored procedures in the [!INCLUDE[ssIS](../includes/ssis-md.md)] catalog to deploy the project and execute the packages. For information about the project deployment model, see [Deployment of Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md).  
  
 You can also use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to deploy the project and execute the packages. For more information, see the topics in the **See Also** section.  
  
> [!TIP]
>  You can easily generate the Transact-SQL statements for the stored procedures listed in the procedure below, with the exception of catalog.deploy_project, by doing the following:  
> 
>  1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], expand the **Integration Services Catalogs** node in Object Explorer and navigate to the package you want to execute.  
> 2.  Right-click the package, and then click **Execute**.  
> 3.  As needed, set parameters values, connection manager properties, and options in the **Advanced** tab such as the logging level.  
> 
>      For more information about logging levels, see [Enable Logging for Package Execution on the SSIS Server](../../2014/integration-services/enable-logging-for-package-execution-on-the-ssis-server.md).  
> 4.  Before clicking **OK** to execute the package, click **Script**. The Transact-SQL appears in a Query Editor window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
## To deploy and execute a package using stored procedures  
  
1.  Call [catalog.deploy_project &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-deploy-project-ssisdb-database) to deploy the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server.  
  
     To retrieve the binary contents of the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project deployment file, for the *@project_stream* parameter, use a SELECT statement with the OPENROWSET function and the BULK rowset provider. The BULK rowset provider enables you to read data from a file. The SINGLE_BLOB argument for the BULK rowset provider returns the contents of the data file as a single-row, single-column rowset of type varbinary(max). For more information, see [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql).  
  
     In the following example, the SSISPackages_ProjectDeployment project is deployed to the SSIS Packages folder on the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server. The binary data is read from the project file (SSISPackage_ProjectDeployment.ispac) and is stored in the *@ProjectBinary* parameter of type varbinary(max). The *@ProjectBinary* parameter value is assigned to the *@project_stream* parameter.  
  
    ```  
    DECLARE @ProjectBinary as varbinary(max)  
    DECLARE @operation_id as bigint  
    Set @ProjectBinary = (SELECT * FROM OPENROWSET(BULK 'C:\MyProjects\ SSISPackage_ProjectDeployment.ispac', SINGLE_BLOB) as BinaryData)  
  
    Exec catalog.deploy_project @folder_name = 'SSIS Packages', @project_name = 'DeployViaStoredProc_SSIS', @Project_Stream = @ProjectBinary, @operation_id = @operation_id out  
  
    ```  
  
2.  Call [catalog.create_execution &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database) to create an instance of the package execution, and optionally call [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database) to set runtime parameter values.  
  
     In the following example, catalog.create_execution creates an instance of execution for package.dtsx that is contained in the SSISPackage_ProjectDeployment project. The project is located in the SSIS Packages folder. The execution_id returned by the stored procedure is used in the call to catalog.set_execution_parameter_value. This second stored procedure sets the LOGGING_LEVEL parameter to 3 (verbose logging) and sets a package parameter named Parameter1 to a value of 1.  
  
     For parameters such as LOGGING_LEVEL the object_type value is 50. For package parameters the object_type value is 30.  
  
    ```  
    Declare @execution_id bigint  
    EXEC [SSISDB].[catalog].[create_execution] @package_name=N'Package.dtsx', @execution_id=@execution_id OUTPUT, @folder_name=N'SSIS Packages', @project_name=N'SSISPackage_ProjectDeployment', @use32bitruntime=False, @reference_id=1  
  
    Select @execution_id  
    DECLARE @var0 smallint = 3  
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=50, @parameter_name=N'LOGGING_LEVEL', @parameter_value=@var0  
  
    DECLARE @var1 int = 1  
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=30, @parameter_name=N'Parameter1', @parameter_value=@var1  
  
    GO  
  
    ```  
  
3.  Call [catalog.start_execution &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database) to execute the package.  
  
     In the following example, a call to catalog.start_execution is added to the Transact-SQL to start the package execution. The execution_id returned by the catalog.create_execution stored procedure is used.  
  
    ```  
    Declare @execution_id bigint  
    EXEC [SSISDB].[catalog].[create_execution] @package_name=N'Package.dtsx', @execution_id=@execution_id OUTPUT, @folder_name=N'SSIS Packages', @project_name=N'SSISPackage_ProjectDeployment', @use32bitruntime=False, @reference_id=1  
  
    Select @execution_id  
    DECLARE @var0 smallint = 3  
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=50, @parameter_name=N'LOGGING_LEVEL', @parameter_value=@var0  
  
    DECLARE @var1 int = 1  
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=30, @parameter_name=N'Parameter1', @parameter_value=@var1  
  
    EXEC [SSISDB].[catalog].[start_execution] @execution_id  
    GO  
  
    ```  
  
## To deploy a project from server to server using stored procedures  
 You can deploy a project from server to server by using the [catalog.get_project &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-get-project-ssisdb-database) and [catalog.deploy_project &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-deploy-project-ssisdb-database) stored procedures.  
  
 You need to do the following before running the stored procedures.  
  
-   Create a linked server object. For more information, see [Create Linked Servers &#40;SQL Server Database Engine&#41;](../database-engine/sql-server-database-engine-overview.md).  
  
     On the **Server Options** page of the **Linked Server Properties** dialog box, set **RPC** and **RPC Out** to **True**. Also, set **Enable Promotion of Distributed Transactions for RPC** to **False**.  
  
-   Enable dynamic parameters for the provider you selected for the linked server, by expanding the **Providers** node under **Linked Servers** in Object Explorer, right-clicking the provider, and then clicking **Properties**. Select **Enable** next to **Dynamic parameter.**  
  
-   Confirm that the Distributed Transaction Coordinator (DTC) is started on both servers.  
  
 Call catalog.get_project to return the binary for the project, and then call catalog.deploy_project. The value returned by catalog.get_project is inserted into a table variable of type varbinary(max). The linked server can't return results that are varbinary(max).  
  
 In the following example, catalog.get_project returns a binary for the SSISPackages project on the linked server. The catalog.deploy_project deploys the project to the local server, to the folder named DestFolder.  
  
```  
declare @resultsTableVar table (  
project_binary varbinary(max)  
)  
  
INSERT @resultsTableVar (project_binary)  
EXECUTE [MyLinkedServer].[SSISDB].[catalog].[get_project] 'Packages', 'SSISPackages'  
  
declare @project_binary varbinary(max)  
select @project_binary = project_binary from @resultsTableVar  
  
exec [SSISDB].[CATALOG].[deploy_project] 'DestFolder', 'SSISPackages', @project_binary  
  
```  
  
## See Also  
 [Deploy Projects to Integration Services Server](../../2014/integration-services/deploy-projects-to-integration-services-server.md)   
 [Run a Package in SQL Server Data Tools](../../2014/integration-services/run-a-package-in-sql-server-data-tools.md)   
 [Run a Package on the SSIS Server Using SQL Server Management Studio](run-a-package-on-the-ssis-server-using-sql-server-management-studio.md)  
  
  
