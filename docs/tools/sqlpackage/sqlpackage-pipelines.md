---
title: SqlPackage in Development Pipelines
description: Learn how to troubleshoot database development pipelines with SqlPackage.
author: cwebb-msft
ms.author: chweb
ms.reviewer: drskwier
ms.date: 06/27/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom: sfi-ropc-nochange
---

# SqlPackage in development pipelines

**SqlPackage** is a command-line utility that automates several database development tasks and can be incorporated into CI/CD pipelines.

> [!NOTE]  
> Utilizing a [standalone installation of SqlPackage](sqlpackage-download.md) for pipeline automation is recommended over using the SqlPackage executables bundled with other applications, including SQL Server Management Studio or Visual Studio. The standalone installation of SqlPackage is updated more frequently and isn't tied to the release cadence of other applications.

If SqlPackage is installed as a global dotnet tool (recommended), you can invoke it in the pipeline with simply `sqlpackage` from any directory. If SqlPackage is installed as a standalone executable, you must specify the full path to the executable in the pipeline. On Windows, the standalone install of SqlPackage is available on the path `C:\Program Files\Microsoft SQL Server\170\DAC\bin` (DacFx.msi). In both Windows and Linux environments, if you download the self-contained .zip SqlPackage for .NET, you can extract the executable to a location of your choosing.

## Managed virtual environments

The virtual environments used for GitHub Actions hosted runners and Azure Pipelines virtual machine images are managed in the [runner-images](https://github.com/actions/runner-images) GitHub repository. SqlPackage is included in several environments including `windows-latest` and `ubuntu-22.04` but is no longer included by default in `ubuntu-24.04` and `ubuntu-latest`. Updates to the images in [runner-images](https://github.com/actions/runner-images) are made within a few weeks of each SqlPackage release.

In a managed virtual environment, you would install SqlPackage at runtime in the pipeline. The installation is performed as a separate step in either Azure Pipelines and GitHub Actions and adds a short delay to each pipeline run while the installation occurs. To install SqlPackage at runtime, add a step to the pipeline that uses the dotnet CLI to install SqlPackage as a global tool. This step should be run before any SqlPackage actions in the pipeline.

```bash
dotnet tool install --global Microsoft.SqlPackage
```

You might need to perform a .NET setup step in the pipeline before installing SqlPackage, indicated by an error message stating that .NET can't be found. Use an integrated action to configure it such as the [UseDotNet](/azure/devops/pipelines/tasks/reference/use-dotnet-v2) task in Azure Pipelines or the [setup-dotnet](https://github.com/actions/setup-dotnet) action in GitHub Actions.

Optionally, you can specify a version of SqlPackage to install by appending `--version <version>` to the install command or you can use the `--prerelease` flag to install a preview versions.

### Example: Steps to install SqlPackage in Azure DevOps Pipelines

In Azure DevOps Pipelines, you can use the [UseDotNet](/azure/devops/pipelines/tasks/reference/use-dotnet-v2) task to install the .NET SDK and then install SqlPackage as a global tool. If you're using a Windows agent provided by Microsoft, SqlPackage is already installed in `C:\Program Files\Microsoft SQL Server\170\DAC\bin`. If you're using a self-hosted agent, you can also install SqlPackage on the host environment and skip this step in the pipeline.

```yaml
- task: UseDotNet@2
  inputs:
    packageType: 'sdk'
    version: '8.x'
- script: dotnet tool install --global Microsoft.SqlPackage --version <version>
  displayName: 'Install specific version of SqlPackage'
```

### Example: Steps to install SqlPackage in GitHub Actions

In GitHub Actions, you can use the [setup-dotnet](https://github.com/actions/setup-dotnet) action to install the .NET SDK and then install SqlPackage as a global tool. If you're using a Windows runner provided by GitHub, SqlPackage is already installed in `C:\Program Files\Microsoft SQL Server\170\DAC\bin`. If you're using a self-hosted runner, you can also install SqlPackage on the host environment and skip this step in the workflow.

```yaml
- name: Setup .NET
  uses: actions/setup-dotnet@v4
  with:
    dotnet-version: '8.x'
- name: Install SqlPackage
  run: dotnet tool install --global Microsoft.SqlPackage --version <version>
```

<a id="checking-the-sqlpackage-version"></a>

### Check the SqlPackage version

During troubleshooting efforts, it's important to know the SqlPackage version is in use. Capturing this information can be done by adding a step to the pipeline to run SqlPackage with the `/version` parameter. Examples are given in this article based on the Azure DevOps and GitHub managed environments, self-hosted environments might have different installation paths for the working directory.

#### Azure Pipelines

In an Azure Pipeline, the [script](/azure/devops/pipelines/yaml-schema/steps-script) keyword returns the SqlPackage version number.

```yaml
- script: SqlPackage /version
  workingDirectory: 'C:\Program Files\Microsoft SQL Server\170\DAC\bin\'
  displayName: 'get sqlpackage version'
```

#### GitHub Actions

In a GitHub Action workflow, the [run](https://docs.github.com/actions/reference/workflow-syntax-for-github-actions) keyword returns the SqlPackage version number.

```yaml
- name: get sqlpackage version
  working-directory: 'C:\Program Files\Microsoft SQL Server\170\DAC\bin\'
  run: ./SqlPackage /version
```

:::image type="content" source="media/sqlpackage-pipelines-github-action.png" alt-text="GitHub action output displaying build number 15.0.4897.1":::

### Update SqlPackage on the pipeline agent

In some scenarios, the current version of SqlPackage installed in the pipeline environment might be insufficient. If the environment can't be directly modified, an extra step can be used to install a newer version of SqlPackage during the pipeline run. It's important to run the install step before running any DacPac or BacPac operations in the pipeline. This task can be combined with a step to [check the version](#checking-the-sqlpackage-version) to ensure that the upgrade completed as expected.

#### Azure Pipelines, Windows-based agent

When the [PowerShell](/azure/devops/pipelines/tasks/utility/powershell) task is used in an Azure Pipeline, a step can be added to an Azure Pipeline that downloads the desired DacFx installer and installs it silently.

```yaml
- task: PowerShell@2
  displayName: 'upgrade sqlpackage'
  inputs:
    targetType: 'inline'
    script: |
      # use evergreen or specific dacfx msi link below
      wget -O DacFramework.msi "https://aka.ms/dacfx-msi"
      msiexec.exe /i "DacFramework.msi" /qn
```

#### GitHub Actions, Linux-based runner

In a GitHub Action workflow, you can use the [run](https://docs.github.com/actions/reference/workflow-syntax-for-github-actions) keyword to execute the `dotnet tool` commands to install, uninstall, or update SqlPackage. The following example shows how to update SqlPackage to the latest preview version:

```yaml
- name: Update SqlPackage
  run: dotnet tool update --global Microsoft.SqlPackage --prerelease
```

## Self-hosted virtual environments

In a self-hosted virtual environment such as a self-hosted Azure DevOps agent or GitHub Actions runner, you can install SqlPackage on the host environment or install SqlPackage at runtime as described in [managed virtual environments](#managed-virtual-environments). If you install SqlPackage on the host environment, pipelines that run on that host don't need to install SqlPackage at runtime, which can speed up the pipeline runs. When SqlPackage is installed on the host, you should [update SqlPackage](sqlpackage-download.md) regularly to maintain the environment with the latest version.

[Azure Container Apps jobs](/azure/container-apps/tutorial-ci-cd-runners-jobs) can be used to deploy self-hosted runners in a container that is deployed as needed for each workflow invocation. With Container Apps jobs, you control the environment as defined in the Dockerfile and install SqlPackage and other tools as needed. The self-hosted runner can be configured to run on a specific version of SqlPackage by including the installation step in the container image. For example, the [tutorial](https://github.com/Azure-Samples/container-apps-ci-cd-runner-tutorial/tree/main) includes a Dockerfile that installs `curl` and `jq`.

We can modify this example to produce a container image that installs .NET 8.0 and SqlPackage:

```dockerfile
FROM ghcr.io/actions/actions-runner:2.323.0

USER root

# install dotnet sdk and sqlpackage
RUN apt-get update && apt-get install -y dotnet-sdk-8.0 && \
    dotnet tool install --tool-path /usr/local/bin/ Microsoft.SqlPackage

COPY entrypoint.sh ./entrypoint.sh
RUN chmod +x ./entrypoint.sh

USER runner

ENTRYPOINT ["./entrypoint.sh"]
```

<a id="tracking-deployments"></a>

## Track deployments

You can capture some files related to SqlPackage to reproduce pipelines and improve deployment tracking. The implementation and use cases depend on your specific architecture and automation environment.

- **Dacpac file**

- **Diagnostic file output from any action:** Use the `/DiagnosticsFile:` parameter on any SqlPackage action. For more information, see [Get SqlPackage diagnostics in a pipeline agent](#get-sqlpackage-diagnostics-in-a-pipeline-agent)

- **Output from script action prior to publish action:** Use the [Script](sqlpackage-script.md) SqlPackage action before invoking a publish action

## Get SqlPackage diagnostics in a pipeline agent

Diagnostic information from SqlPackage is available in the command line through the parameter `/DiagnosticsFile`, which can be used in virtual environments such as Azure Pipelines and GitHub Actions. The diagnostic information is written to a file in the working directory. The file name is dictated by the `/DiagnosticsFile` parameter.

### Azure Pipelines

Adding the `/DiagnosticsFile` parameter to the "Additional SqlPackage Arguments" field in the Azure Pipeline SqlAzureDacpacDeployment configuration causes the SqlPackage diagnostic information to be written to the file specified. Following the SqlAzureDacpacDeployment task, the diagnostic file is available outside of the virtual environment by publishing a pipeline artifact as seen in the following example.

```yaml
- task: SqlAzureDacpacDeployment@1
  inputs:
    azureSubscription: '$(azuresubscription)'
    AuthenticationType: 'server'
    ServerName: '$(servername)'
    DatabaseName: '$(databasename)'
    SqlUsername: '$(sqlusername)'
    SqlPassword: '$(sqladminpassword)'
    deployType: 'DacpacTask'
    DeploymentAction: 'Publish'
    DacpacFile: '$(Build.Repository.LocalPath)\$(dacpacname).dacpac'
    AdditionalArguments: '/DiagnosticsFile:$(System.DefaultWorkingDirectory)/output.log'
    IpDetectionMethod: 'AutoDetect'

- task: PublishPipelineArtifact@1
  inputs:
    targetPath: '$(System.DefaultWorkingDirectory)/output.log'
    artifact: 'Diagnostic File'
    publishLocation: 'pipeline'
```

After the pipeline run, the diagnostic file can be downloaded from the run summary page under "Published Artifacts."

### GitHub Actions

Adding the `/DiagnosticsFile` parameter to the "arguments" field in the GitHub Action sql-action configuration causes the SqlPackage diagnostic information to be written to the file specified. Following the sql-action task, the diagnostic file can be made available outside of the virtual environment by publishing an artifact as seen in the following example.

```yaml
- name: Azure SQL Deploy
  uses: Azure/sql-action@v2
  with:
    # The connection string, including authentication information, for the Azure SQL Server database.
    connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
    # Path to DACPAC file to deploy
    path: .\DatabaseProjectAdventureWorksLT\bin\Release\DatabaseProjectAdventureWorksLT.dacpac
    action: publish
    # additional SqlPackage arguments
    arguments: /DiagnosticsFile:DatabaseProjectAdventureWorksLT/DiagnosticLog.log

- uses: actions/upload-artifact@v2
  with:
    name: 'DiagnosticLog.txt'
    path: 'DatabaseProjectAdventureWorksLT/DiagnosticLog.log'
```

## Related content

- [SqlPackage](sqlpackage.md)
- [Tutorial: Create and deploy a SQL project](../sql-database-projects/tutorials/create-deploy-sql-project.md)
