---
title: SQL Projects Extensibility
description: "Customize SQL project deployments with deployment contributors."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, maghan
ms.date: 05/22/2025
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ms.collection:
  - data-tools
---

# Extensibility for SQL projects

The .NET Data-tier Application Framework (DacFx) library provides extensibility points that you can use to modify the behavior of the build and deployment actions for database projects.

- **Build (BuildContributor):** This type of extension is executed when the SQL project is built after the project model is completely validated. The build contributor can access the completed model, in addition to all properties of the Build task and any custom arguments.
- **Deploy (DeploymentPlanModifier):** This type of extension is executed when the SQL project is deployed, as part of the deployment pipeline, after the deployment plan is generated, but before the deployment plan is executed. You can use a DeploymentPlanModifier to modify the deployment plan by adding or removing steps. Deployment contributors can access the deployment plan, the comparison results, and the source and target models.
- **Deploy (DeploymentPlanExecutor):** This type of extension is executed when the deployment plan is executed and provides read-only access to the deployment plan. The DeploymentPlanExecutor performs actions based on the deployment plan.

## Example extensibility scenarios

You can implement build or deployment contributors to enable the following example scenarios:

- **Generate schema documentation during a project build** - To support this scenario, you implement a [BuildContributor](/dotnet/api/microsoft.sqlserver.dac.deployment.buildcontributor) and override the OnExecute method to generate the schema documentation. You can create a targets file that defines default arguments that control whether the extension runs and to specify the name of the output file.
- **Generate a difference report when a SQL project is deployed** - To support this scenario, you implement a [DeploymentPlanExecutor](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanexecutor) that generates the XML file when the SQL project is deployed.
- **Modify the deployment plan to change when data motion occurs** - To support this scenario, you implement a [DeploymentPlanModifier](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanmodifier) and iterate over the deployment plan. For each SqlTableMigrationStep in that plan, you examine the comparison result to determine whether that step should be performed or skipped.
- **Copy files to the generated dacpac when a SQL project is deployed** - To support this scenario, you implement a deployment contributor and override the OnEstablishDeploymentConfiguration method to specify which files that are marked as DeploymentExtensionConfiguration by the project system. These files should be copied to the output folder and added inside the generated dacpac. You can also modify the contributor to merge multiple files into one new file that is copied to the output folder and is added to the deployment manifest. During deployment, you can implement the OnApplyDeploymentConfiguration method to extract those files from the dacpac and prepare them for use in the OnExecute method.

A contributor can accept input at runtime as pairs of name/value arguments. These arguments enable end users at build or deployment time to customize the behavior of your contributor. For example, you could allow users to specify the name of an input or output file or to control selection of objects from the model.

## Deployment contributors

The deployment process for SQL projects supports extensibility through **deployment contributors**, which access the deployment plan and either can modify it (**DeploymentPlanModifier**) or implement an action based on the plan (**DeploymentPlanExecutor**). Deployment contributors can access the deployment plan, the comparison results, and the source and target models. With a **DeploymentPlanModifier**, you can use deployment contributors to add or remove steps from the deployment plan, or to modify the steps in the deployment plan. DeploymentPlanModifiers are the most frequently used deployment contributors.

:::image type="content" source="media/sql-projects-extensibility/dacfx-deployment-process-contributor.png" alt-text="Screenshot of DacFx deployment process where deployment plan is calculated from model differences and modified by a deployment contributor." lightbox="media/sql-projects-extensibility/dacfx-deployment-process-contributor.png":::

Deployment contributors are reusable through parameterization and can be used in multiple projects. In addition to the archived samples for [DacExtensions](https://github.com/microsoft/DACExtensions), community members created and shared their own reusable deployment contributors as open source projects.

## SqlPackage integration

**SqlPackage** is a command-line utility that can be used to create and deploy SQL projects. When used with **SqlPackage**, deployment contributors customize the publish process and can be specified with properties on the publish action, such as `/p:AdditionalDeploymentContributors`. The deployment contributors must be in a location that is accessible to **SqlPackage**, such as the same folder as the **SqlPackage** executable or in a folder that is specified in the property `/p:AdditionalDeploymentContributorPaths`. For more information about the publish action and the properties you can use to specify deployment contributors, see [SqlPackage Publish](../../sqlpackage/sqlpackage-publish.md).

Deployment contributors are required to be built with the same major version of the DacFx library as **SqlPackage** in these scenarios:

- Using the .NET Framework version of **SqlPackage**, which requires a major version match to the deployment contributor.
- When **SqlPackage** is updated with modified DacFx library API definitions, which can change from version to version. Breaking changes are limited to major version updates.

If a deployment contributor isn't built with a compatible version of DacFx, the **SqlPackage** operation will fail when attempting to load the extension at runtime. When a deployment contributor fails to load in **SqlPackage**, you'll see an error message similar to the following message:

```output
Could not load extensions from file 'D:\a\_work\....dll' because the assembly has dependency to older versions of DacFx. For more information check https://aka.ms/sqlprojects-extensions

Error SQL0: Required contributor with id 'MyCompany.MyExtension' could not be loaded.
System.Management.Automation.RemoteException
Contributor initialization error.
```

When integrating deployment contributors with **SqlPackage** in automation pipelines, consider managing the version of **SqlPackage** installed on the build agent. Managing the **SqlPackage** install enables you to ensure it matches the version of the DacFx library used to build your deployment contributors. For greater flexibility, use the [dotnet tool Microsoft.SqlPackage](../../sqlpackage/sqlpackage-download.md#installation-cross-platform) instead of the .NET Framework **SqlPackage**. For more information about how to install **SqlPackage** in your build agent from within the workflow if the environment can't be modified, see the [SqlPackage in development pipelines](../../sqlpackage/sqlpackage-pipelines.md) article.

## Related content

- [Extending the database features](../../../ssdt/extending-the-database-features.md)
- [Tutorial: Create and deploy a SQL project](../tutorials/create-deploy-sql-project.md)
- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../sqlpackage/sqlpackage-publish.md)
- [Code analysis rules extensibility overview](code-analysis-extensibility.md)
