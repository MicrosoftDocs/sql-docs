---
title: "Deploy an SSIS project from the command prompt | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Deploy an SSIS project from the command prompt with ISDeploymentWizard.exe
This quick start tutorial demonstrates how to deploy an SSIS project from the command prompt by running the Integration Services Deployment Wizard, `ISDeploymentWizard.exe`.

For more info about the Integration Services Deployment Wizard, see [Integration Services Deployment Wizard](/sql/integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md#integration-services-deployment-wizard).

## Start the Integration Services Deployment Wizard
1. Open a Command Prompt window.

2. Run `ISDeploymentWizard.exe`. The Integration Services Deployment Wizard opens.

    If the folder that contains `ISDeploymentWizard.exe` is not in your `path` environment variable, you may have to use the `cd` command to change to its directory. For SQL Server 2017, this folder is typically `C:\Program Files (x86)\Microsoft SQL Server\140\DTS\Binn`.

## Deploy a project with the wizard
1. On the **Introduction** page of the wizard, review the introduction. Click **Next** to open the **Select Source** page.

2. On the **Select Source** page, select the existing SSIS project to deploy.
    -   To deploy a project deployment file that you created, select **Project deployment file** and enter the path to the .ispac file.
    -   To deploy a project that resides in an SSIS catalog, select **Integration Services catalog**, and then enter the server name and the path to the project in the catalog.
    Click **Next** to see the **Select Destination** page.
  
3.  On the **Select Destination** page, select the destination for the project.
    -   Enter the fully qualified server name. If the target server is an Azure SQL Database server, the name is in this format: `<server_name>.database.windows.net`.
    -   Then click **Browse** to select the target folder in SSISDB.
    Click **Next** to open the **Review** page.  
  
4.  On the **Review** page, review the settings you selected.
    -   You can change your selections by clicking **Previous**, or by clicking any of the steps in the left pane.
    -   Click **Deploy** to start the deployment process.
  
5.  After the deployment process is complete, the **Results** page opens. This page displays the success or failure of each action.
    -   If the action failed, click **Failed** in the **Result** column to display an explanation of the error.
    -   Optionally, click **Save Report...** to save the results to an XML file.
    -   Click **Close** to exit the wizard.

## Next steps
- Consider other ways to deploy a package.
    - [Deploy an SSIS package with SSMS](./ssis-quickstart-deploy-ssms.md)
    - [Deploy an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-deploy-tsql-ssms.md)
    - [Deploy an SSIS package with Transact-SQL (VS Code)](ssis-quickstart-deploy-tsql-vscode.md)
    - [Deploy an SSIS package with PowerShell](ssis-quickstart-deploy-powershell.md)
    - [Deploy an SSIS package with C#](./ssis-quickstart-deploy-dotnet.md) 
- Run a deployed package. To run a package, you can choose from several tools and languages. For more info, see the following articles:
    - [Run an SSIS package with SSMS](./ssis-quickstart-run-ssms.md)
    - [Run an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-run-tsql-ssms.md)
    - [Run an SSIS package with Transact-SQL (VS Code)](ssis-quickstart-run-tsql-vscode.md)
    - [Run an SSIS package from the command prompt](./ssis-quickstart-run-cmdline.md)
    - [Run an SSIS package with PowerShell](ssis-quickstart-run-powershell.md)
    - [Run an SSIS package with C#](./ssis-quickstart-run-dotnet.md) 
