---
title: "SQL Projects Automation"
description: "This overview reviews setting up automation with SQL database projects for CI/CD pipelines using GitHub Actions, Azure DevOps, and other platforms."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 01/29/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-assisted
---

# SQL projects automation

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides an overview of automation options for SQL projects across different software delivery platforms. Use automation to integrate SQL database projects into CI/CD pipelines, and deploy database changes consistently and repeatedly.

## What to automate

SQL projects automation typically involves two key tasks in a CI/CD pipeline:

- **Build the SQL project**: Validate the project and produce the deployment artifact (`.dacpac`) by running `dotnet build` on the SQL database project to compile. Optionally, execute [code analysis](concepts/sql-code-analysis/sql-code-analysis.md) rules to check code quality during project build.

- **Deploy the `.dacpac`**: Publish the `.dacpac` to a target database using SqlPackage or a platform-specific task. Deployment can target Azure SQL Database, Azure SQL Managed Instance, SQL Server, or SQL database in Fabric.

:::image type="content" source="media/sql-database-projects/build-deploy.png" alt-text="Diagram showing the flow from SQL project build to dacpac artifact and deployment to a database." lightbox="media/sql-database-projects/build-deploy.png":::

When you integrate these steps into your CI/CD pipeline, database changes are validated on every commit and deployed consistently across environments.

### Common concepts

**Artifact flow**: A typical pipeline separates the build and deploy stages. The build stage compiles the SQL project and produces a `.dacpac` file, which is then published as a pipeline artifact. In a subsequent deploy job (potentially after manual approval), the artifact is downloaded and deployed to the target database. This separation allows you to build once and deploy the same artifact to multiple environments, ensuring consistency.

**Publish or script**: Before deploying to sensitive environments, you can review the deployment plan. SqlPackage supports a `Script` action that generates the T-SQL script that would be executed during deployment, without applying changes. The provided T-SQL script allows database administrators or reviewers to examine the exact changes necessary to apply the `.dacpac` before approval. Similarly, the `DeployReport` action produces an XML report of the planned changes.

**Environment approvals and gates**: Production deployments typically require approval workflows to prevent unintended changes. Both GitHub Actions and Azure DevOps support environment-based approvals. In GitHub, you can configure [environments](https://docs.github.com/actions/how-tos/deploy/configure-and-manage-deployments/manage-environments) with required reviewers and wait timers. In Azure DevOps, [environments](/azure/devops/pipelines/process/environments) support approval gates, business hours restrictions, and other deployment controls. These controls help ensure that database changes are reviewed and approved before reaching production.

### Prerequisites

SqlPackage is cross-platform and runs on Windows, Linux, and macOS. Install it as a .NET global tool to ensure consistent behavior across environments.

- [.NET SDK](https://dotnet.microsoft.com/download) installed on the build agent.

- [SqlPackage](../sqlpackage/sqlpackage.md) installed on the build agent. You can install SqlPackage as a .NET global tool:

  ```bash
  dotnet tool install --global microsoft.sqlpackage
  ```

## Software delivery platforms

Choose an environment that matches your team's tooling, compliance, and connectivity requirements.

### Managed virtual environments

Microsoft-managed virtual environments for GitHub Actions hosted runners and Azure Pipelines agents include preinstalled tools:

| Environment | .NET SDK | SqlPackage |
| --- | --- | --- |
| **Windows** | Preinstalled | Preinstalled |
| **Linux** | Preinstalled | Not preinstalled |
| **macOS** | Preinstalled | Not preinstalled |

On Linux and macOS environments, install SqlPackage as part of your workflow. For more information on SqlPackage installation and versioning in pipelines, see [SqlPackage in development pipelines](../sqlpackage/sqlpackage-pipelines.md).

### Self-hosted environments

Deploy from a self-hosted runner or agent when you need more control over the environment, such as:

- Network isolation requirements (accessing databases not exposed to the public internet)
- Custom tooling or specific SqlPackage versions
- Compliance or security policies

You can deploy self-hosted runners as [Azure Container Apps jobs](/azure/container-apps/tutorial-ci-cd-runners-jobs) for event-driven, serverless execution. Use this approach to define the environment in a Dockerfile and install SqlPackage and other tools as needed.

## GitHub Actions

The [Azure SQL Deploy action](https://github.com/azure/sql-action) (`azure/sql-action`) provides an integrated experience for deploying SQL projects and `.dacpac` files to Azure SQL and SQL Server from any GitHub Actions runner.

Key capabilities:

- Deploys `.dacpac`, `.sqlproj`, or `.sql` scripts.
- Supports SQL authentication, Microsoft Entra ID authentication, and service principal authentication.
- Automatically adds and removes temporary firewall rules for Azure SQL Database when combined with `azure/login`.
- Works on both Windows and Linux runners.

### Example: Deploy a SQL project with GitHub Actions

```yaml
# .github/workflows/sql-deploy.yml
on: [push]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4

    - uses: azure/sql-action@v2
      with:
        connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
        path: './Database.sqlproj'
        action: 'publish'
```

For Azure SQL Database deployments that require a temporary firewall rule, add the `azure/login` step before `sql-action`:

```yaml
    - uses: azure/login@v2
      with:
        creds: ${{ secrets.AZURE_CREDENTIALS }}

    - uses: azure/sql-action@v2
      with:
        connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
        path: './Database.dacpac'
        action: 'publish'
```

You can also use SqlPackage directly on any runner. For more information, see [SqlPackage in development pipelines](../sqlpackage/sqlpackage-pipelines.md).

## Azure Pipelines

Azure DevOps provides the [SqlAzureDacpacDeployment](/azure/devops/pipelines/tasks/reference/sql-azure-dacpac-deployment-v1) task for deploying `.dacpac` files and SQL scripts to Azure SQL Database.

Key capabilities:

- Deploys `.dacpac` files or executes SQL scripts.
- Supports SQL authentication, Microsoft Entra ID authentication, and service principal authentication.
- Automatically manages firewall rules for Azure SQL Database.
- Requires a Windows agent (use SqlPackage directly on Linux agents).

### Example: Deploy a `.dacpac` with Azure DevOps

```yaml
# azure-pipelines.yml
trigger:
  - main

pool:
  vmImage: 'windows-latest'

steps:
- task: SqlAzureDacpacDeployment@1
  inputs:
    azureSubscription: 'your-service-connection'
    AuthenticationType: 'server'
    ServerName: 'your-server.database.windows.net'
    DatabaseName: 'your-database'
    SqlUsername: '$(SqlUser)'
    SqlPassword: '$(SqlPassword)'
    deployType: 'DacpacTask'
    DeploymentAction: 'Publish'
    DacpacFile: '$(Build.ArtifactStagingDirectory)/Database.dacpac'
```

For Linux agents or more control over the deployment process, use SqlPackage directly:

```yaml
steps:
- task: UseDotNet@2
  inputs:
    packageType: 'sdk'
    version: '8.x'

- script: dotnet tool install --global microsoft.sqlpackage
  displayName: 'Install SqlPackage'

- script: |
    sqlpackage /Action:Publish \
      /SourceFile:$(Build.ArtifactStagingDirectory)/Database.dacpac \
      /TargetConnectionString:"$(ConnectionString)"
  displayName: 'Deploy database'
```

## Other CI/CD platforms

For platforms such as GitLab CI/CD, Jenkins, CircleCI, or others, use SqlPackage directly to build and deploy SQL projects.

### Build the project

```bash
dotnet build ./Database.sqlproj -c Release
```

### Deploy the `.dacpac`

```bash
sqlpackage /Action:Publish \
  /SourceFile:./bin/Release/Database.dacpac \
  /TargetConnectionString:"Server=your-server;Database=your-db;User Id=user;Password=password;"
```

## Related content

- [Tutorial: Create and deploy a SQL project](tutorials/create-deploy-sql-project.md)
- [SqlPackage in development pipelines](../sqlpackage/sqlpackage-pipelines.md)
- [SQL projects tools](sql-projects-tools.md)
- [SqlPackage](../sqlpackage/sqlpackage.md)
- [Azure SQL Deploy action (GitHub)](https://github.com/azure/sql-action)
- [SqlAzureDacpacDeployment task reference](/azure/devops/pipelines/tasks/reference/sql-azure-dacpac-deployment-v1)
