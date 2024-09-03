---
title: SQL projects extensibility
description: "Customize SQL project deployments with deployment contributors."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
---

# Extensibility for SQL projects

The .NET Data-tier Application Framework (DacFx) library provides extensibility points that you can use to modify the behavior of the build and deployment actions for database projects.

- **Build (BuildContributor):** This type of extension is executed when the SQL project is built after the project model has been completely validated. The build contributor can access the completed model, in addition to all properties of the Build task and any custom arguments.
- **Deploy (DeploymentPlanModifier):** This type of extension is executed when the SQL project is deployed, as part of the deployment pipeline, after the deployment plan has been generated, but before the deployment plan is executed. You can use a DeploymentPlanModifier to modify the deployment plan by adding or removing steps. Deployment contributors can access the deployment plan, the comparison results, and the source and target models.
- **Deploy (DeploymentPlanExecutor):** This type of extension is executed when the deployment plan is executed and provides read-only access to the deployment plan. The DeploymentPlanExecutor performs actions based on the deployment plan.

## Example extensibility scenarios

You can implement build or deployment contributors to enable the following example scenarios:

- **Generate schema documentation during a project build** - To support this scenario, you implement a [BuildContributor](/dotnet/api/microsoft.sqlserver.dac.deployment.buildcontributor) and override the OnExecute method to generate the schema documentation. You can create a targets file that defines default arguments that control whether the extension runs and to specify the name of the output file.
- **Generate a difference report when a SQL project is deployed** - To support this scenario, you implement a [DeploymentPlanExecutor](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanexecutor) that generates the XML file when the SQL project is deployed.
- **Modify the deployment plan to change when data motion occurs** - To support this scenario, you implement a [DeploymentPlanModifier](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanmodifier) and iterate over the deployment plan. For each SqlTableMigrationStep in that plan, you examine the comparison result to determine whether that step should be performed or skipped.
- **Copy files to the generated dacpac when a SQL project is deployed** - To support this scenario, you implement a deployment contributor and override the OnEstablishDeploymentConfiguration method to specify which files that are marked as DeploymentExtensionConfiguration by the project system. These files should be copied to the output folder and added inside the generated dacpac. You can also modify the contributor to merge multiple files into one new file that is copied to the output folder and is added to the deployment manifest. During deployment, you can implement the OnApplyDeploymentConfiguration method to extract those files from the dacpac and prepare them for use in the OnExecute method.

Extensibility can be controlled by customized pairs of name/value arguments from your contributor. These arguments enable end users at build or deployment time to customize the behavior of your contributor. For example, you could allow users to specify the name of an input or output file or to control selection of objects from the model.

## Deployment contributors

The deployment process for SQL projects supports extensibility through **deployment contributors**, which access the deployment plan and either can modify it (**DeploymentPlanModifier**) or implement an action based on the plan (**DeploymentPlanExecutor**). Deployment contributors can access the deployment plan, the comparison results, and the source and target models. With a **DeploymentPlanModifier**, you can use deployment contributors to add or remove steps from the deployment plan, or to modify the steps in the deployment plan. These are the most frequently used deployment contributors

:::image type="content" source="media/sql-projects-extensibility/dacfx-deployment-process-contributor.png" alt-text="Screenshot of DacFx deployment process where deployment plan is calculated from model differences and modified by a deployment contributor." lightbox="media/sql-projects-extensibility/dacfx-deployment-process-contributor.png":::

Deployment contributors are reusable through parameterization and can be used in multiple projects. In addition to the archived samples for [DacExtensions](https://github.com/microsoft/DACExtensions), community members have created and shared their own reusable deployment contributors as open source projects.

## Related content

- [Extending the Database Features](../../../ssdt/extending-the-database-features.md)
- [Tutorial: Create and deploy a SQL project](../tutorials/create-deploy-sql-project.md)
- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../sqlpackage/sqlpackage-publish.md)
- [Code analysis rules extensibility overview](code-analysis-extensibility.md)
