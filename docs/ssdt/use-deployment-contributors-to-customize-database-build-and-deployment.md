---
title: Customize Database Deployments Using Deployment Contributors
description: Learn how to modify the behavior of database projects. View resources on build and deployment contributors, and see examples of scenarios that use them.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: fe2064bb-e01e-4a12-9f12-a99aa9a5203f
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# Customize Database Build and Deployment by Using Build and Deployment Contributors

Visual Studio provides extensibility points that you can use to modify the behavior of the build and deployment actions for database projects.  
  
## Available Extensibility Points  
You can create an extension for the extensibility points, as shown in the following table:  
  
|**Action**|**Contributor Type**|**Notes**|  
|--------------|------------------------|-------------|  
|Build|BuildContributor|This type of extension is executed when the SQL project is built after the project model has been completely validated. The build contributor can access the completed model, in addition to all properties of the Build task and any custom arguments.|  
|Deploy|DeploymentPlanModifier|This type of extension is executed when the SQL project is deployed, as part of the deployment pipeline, after the deployment plan has been generated, but before the deployment plan is executed. You can use a DeploymentPlanModifier to modify the deployment plan by adding or removing steps. Deployment contributors can access the deployment plan, the comparison results, and the source and target models.|  
|Deploy|DeploymentPlanExecutor|This type of extension is executed when the deployment plan is executed and provides read-only access to the deployment plan. The DeploymentPlanExecutor performs actions based on the deployment plan.|  
  
### Supported Extensibility Scenarios  
You can implement build or deployment contributors to enable the following example scenarios:  
  
-   **Generate schema documentation during a project build** - To support this scenario, you implement a [BuildContributor](/dotnet/api/microsoft.sqlserver.dac.deployment.buildcontributor) and override the OnExecute method to generate the schema documentation. You can create a targets file that defines default arguments that control whether the extension runs and to specify the name of the output file.  
  
-   **Generate a difference report when a SQL project is deployed** - To support this scenario, you implement a [DeploymentPlanExecutor](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanexecutor) that generates the XML file when the SQL project is deployed.  
  
-   **Modify the deployment plan to change when data motion occurs** - To support this scenario, you implement a [DeploymentPlanModifier](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanmodifier) and iterate over the deployment plan. For each SqlTableMigrationStep in that plan, you examine the comparison result to determine whether that step should be performed or skipped.  
  
-   **Copy files to the generated dacpac when a SQL project is deployed** - To support this scenario, you implement a deployment contributor and override the OnEstablishDeploymentConfiguration method to specify which files that are marked as DeploymentExtensionConfiguration by the project system. These files should be copied to the output folder and added inside the generated dacpac. You can also modify the contributor to merge multiple files into one new file that is copied to the output folder and is added to the deployment manifest. During deployment, you can implement the OnApplyDeploymentConfiguration method to extract those files from the dacpac and prepare them for use in the OnExecute method.  
  
In addition, you can expose customized pairs of name/value arguments from your contributor that are written to the database project file. You can use these arguments to enable the contributor to extract information from MSBuild or to enable the end user of your contributor to customize the behavior. For example, you could allow users to specify the name of an input or output file.  
  
## Common Tasks  
  
|**Common Tasks**|**Supporting Content**|  
|--------------------|--------------------------|  
|**Learn more about the extensibility points:** You can read about the base classes that you use to implement build and deployment contributors.|[BuildContributor](/dotnet/api/microsoft.sqlserver.dac.deployment.buildcontributor)<br /><br />[DeploymentContributor](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentcontributor)|  
|**Create sample contributors:** Learn the steps that are required to create a build or deployment contributor. If you follow these walkthroughs, you will:<br /><br />-   Create a build contributor that generates a report that lists all elements in the model.<br />-   Create a deployment contributor that changes the deployment plan before it is executed.<br />-   Create a deployment contributor that generates a deployment report when you deploy a SQL project.<br /><br />You can create all your contributors in a single assembly or among several assemblies, depending on how you want the contributors distributed to your team.|[Walkthrough: Extend Database Project Build to Generate Model Statistics](../ssdt/walkthrough-extend-database-project-build-to-generate-model-statistics.md)<br /><br />[Walkthrough: Extend Database Project Deployment to Modify the Deployment Plan](../ssdt/walkthrough-extend-database-project-deployment-to-modify-the-deployment-plan.md)<br /><br />[Walkthrough: Extend Database Project Deployment to Analyze the Deployment Plan](../ssdt/walkthrough-extend-database-project-deployment-to-analyze-the-deployment-plan.md)|  
  
## See Also  
[Define Custom Conditions for SQL Unit Tests](/previous-versions/sql/sql-server-data-tools/jj860449(v=vs.103))  
