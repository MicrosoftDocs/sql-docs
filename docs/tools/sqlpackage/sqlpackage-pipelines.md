---
title: SqlPackage in development pipelines
description: Learn how to troubleshoot database development pipelines with SqlPackage.
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 09/17/2021
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
---

# SqlPackage in development pipelines

**SqlPackage** is a command-line utility that automates several database development tasks and can be incorporated into CI/CD pipelines.


## Virtual environments

> [!NOTE]
> Utilizing a standalone installation of SqlPackage for pipeline automation is recommended over using the SqlPackage executables bundled with other applications, including SQL Server Management Studio or Visual Studio.  The standalone installation of SqlPackage is updated more frequently and is not tied to the release cadence of other applications.

On Windows, the standalone install of SqlPackage is available on the path `C:\Program Files\Microsoft SQL Server\160\DAC\bin` (DacFx.msi) or `%USERPROFILE%\.dotnet\tools` (dotnet tool).  On Linux, the standalone install of SqlPackage is available on the path `~/.dotnet/tools` (dotnet tool).  In both Windows and Linux environments, if you download the self-contained .zip SqlPackage for .NET Core, you can extract the executable to a location of your choosing.

### Managed virtual environments

The virtual environments used for GitHub Actions hosted runners and Azure Pipelines VM images are managed in the [runner-images](https://github.com/actions/runner-images) GitHub repository.  SqlPackage is included in several environments including `windows-latest` and `ubuntu-latest`. Updates to the images in [runner-images](https://github.com/actions/runner-images) are made within a few weeks of each SqlPackage release.

### Self-hosted virtual environments

In a self-hosted virtual environment such as a self-hosted Azure DevOps agent, [update the application](sqlpackage-download.md) regularly to maintain the environment with the latest version.

## Tracking deployments

You can capture some files related to SqlPackage to reproduce pipelines and improve deployment tracking. The implementation and use cases depend on your specific architecture and automation environment.

- **Dacpac file**
- **Diagnostic file output from any action:** Use the `/DiagnosticsFile:` parameter on any SqlPackage action, see [Get SqlPackage diagnostics in a pipeline agent](#get-sqlpackage-diagnostics-in-a-pipeline-agent)
- **Output from script action prior to publish action:**  Use the [Script](sqlpackage-script.md) SqlPackage action before invoking a publish action

## Other SqlPackage examples

### Checking the SqlPackage version

During troubleshooting efforts, it's important to know the SqlPackage version is in use.  Capturing this information can be done by adding a step to the pipeline to run SqlPackage with the `/version` parameter.  Examples are given below based on the Microsoft and GitHub managed environments, self-hosted environments may have different installation paths for the working directory.

#### Azure Pipelines

In an Azure Pipeline, the [script](/azure/devops/pipelines/yaml-schema/steps-script) keyword returns the SqlPackage version number.

```yaml
- script: SqlPackage /version
  workingDirectory: 'C:\Program Files\Microsoft SQL Server\160\DAC\bin\'
  displayName: 'get sqlpackage version'
```

#### GitHub Actions

In a GitHub Action workflow, the [run](https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions) keyword returns the SqlPackage version number.

```yaml
- name: get sqlpackage version
  working-directory: 'C:\Program Files\Microsoft SQL Server\160\DAC\bin\'
  run: ./SqlPackage /version
```

:::image type="content" source="media/sqlpackage-pipelines-github-action.png" alt-text="GitHub action output displaying build number 15.0.4897.1":::

### Get SqlPackage diagnostics in a pipeline agent

Diagnostic information from SqlPackage is available in the command line through the parameter `/DiagnosticsFile`, which can be used in virtual environments such as Azure Pipelines and GitHub Actions.  The diagnostic information is written to a file in the working directory.  The file name is dictated by the `/DiagnosticsFile` parameter.

#### Azure Pipelines
Adding the `/DiagnosticsFile` parameter to the "Additional SqlPackage Arguments" field in the Azure Pipeline SqlAzureDacpacDeployment configuration will cause the SqlPackage diagnostic information to be written to the file specified.  Following the SqlAzureDacpacDeployment task, the diagnostic file can be made available outside of the virtual environment by publishing a pipeline artifact as seen in the example below.

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

After the pipeline run, the diagnostic file can be downloaded from the run summary page under "Published Artifacts".

#### GitHub Actions

Adding the `/DiagnosticsFile` parameter to the "arguments" field in the GitHub Action sql-action configuration will cause the SqlPackage diagnostic information to be written to the file specified. Following the sql-action task, the diagnostic file can be made available outside of the virtual environment by publishing an artifact as seen in the example below.

```yaml
- name: Azure SQL Deploy
  uses: Azure/sql-action@v2
  with:
    # The connection string, including authentication information, for the Azure SQL Server database.
    connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
    # Path to DACPAC file to deploy
    path: .\DatabaseProjectAdventureWorksLT\bin\Release\DatabaseProjectAdventureWorksLT.dacpac
    action: publish
    # additional SqlPackage arguments
    arguments: /DiagnosticsFile:DatabaseProjectAdventureWorksLT/DiagnosticLog.log

- uses: actions/upload-artifact@v2
  with:
    name: 'DiagnosticLog.txt'
    path: 'DatabaseProjectAdventureWorksLT/DiagnosticLog.log'
```

### Update SqlPackage on the pipeline agent

In some scenarios, the current version of SqlPackage installed in the pipeline environment may be insufficient. If the environment can't be directly modified, an additional step can be used to install a newer version of SqlPackage during the pipeline run. It's important to run the install step before running any DacPac or BacPac operations in the pipeline. This task can be combined with a step to [check the version](#checking-the-sqlpackage-version) to ensure that the upgrade completed as expected.

#### Azure Pipelines
When the [PowerShell](/azure/devops/pipelines/tasks/utility/powershell) task is used in an Azure Pipeline, a step can be added to an Azure Pipeline that downloads the desired MSI and installs it silently. 

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


## Next steps

- Learn more about [SqlPackage](sqlpackage.md)
