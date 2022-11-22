---
title: SqlPackage in development pipelines
description: Learn how to troubleshoot database development pipelines with SqlPackage.exe.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 09/17/2021
---

# SqlPackage in development pipelines

**SqlPackage.exe** is a command-line utility that automates several database development tasks and can be incorporated into CI/CD pipelines.

## Virtual environments

### Managed virtual environments

The virtual environments used for GitHub Actions hosted runners and Azure Pipelines VM images are managed in the [virtual-environments](https://github.com/actions/virtual-environments) GitHub repository.  SqlPackage is included in several environments including `windows-latest` and `ubuntu-latest`. Updates to the images in [virtual-environments](https://github.com/actions/virtual-environments) are made within a few weeks of each SqlPackage release.

### Self-hosted virtual environments

If you're utilizing SqlPackage in a self-hosted virtual environment such as a self-hosted Azure DevOps agent, it's recommended to [update the application](sqlpackage-download.md) regularly to maintain the environment with the latest version.

## Tracking deployments

There are a few files related to SqlPackage that can be captured as pipeline artifacts to create pipeline execution reproducibility and improve deployment tracking. The implementation and use cases vary dependent on your specific architecture and automation environment.

- **Dacpac file**
- **Diagnostic file output from any action:** Use the `/DiagnosticsFile:` parameter on any SqlPackage action, see [below example](#obtaining-sqlpackage-diagnostics-in-a-pipeline-agent)
- **Output from script action prior to publish action:**  Use the [Script](sqlpackage-script.md) SqlPackage action before invoking a publish action

## Other SqlPackage examples

### Checking the SqlPackage version

During troubleshooting efforts, it's important to know the SqlPackage version is in use.  Capturing this information can be done by adding a step to the pipeline to run SqlPackage with the `/version` parameter.  Examples are given below based on the Microsoft and GitHub managed environments, self-hosted environments may have different installation paths for the working directory.

#### Azure Pipelines

When the [script](/azure/devops/pipelines/yaml-schema#script) keyword is used in an Azure Pipeline, a step can be added to an Azure Pipeline that outputs the SqlPackage version number.

```yaml
- script: sqlpackage.exe /version
  workingDirectory: C:\Program Files\Microsoft SQL Server\160\DAC\bin\
  displayName: 'get sqlpackage version'
```

#### GitHub Actions

By using the [run](https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions) keyword in a GitHub Action workflow, a step can be added to a GitHub Action that outputs the SqlPackage version number.

```yaml
- name: get sqlpackage version
  working-directory: C:\Program Files\Microsoft SQL Server\160\DAC\bin\
  run: ./sqlpackage.exe /version
```

:::image type="content" source="media/sqlpackage-pipelines-github-action.png" alt-text="GitHub action output displaying build number 15.0.4897.1":::


### Obtaining SqlPackage diagnostics in a pipeline agent

Diagnostic information from SqlPackage is available in the command line through the parameter `/DiagnosticsFile`, which can be used in virtual environments such as Azure Pipelines and GitHub Actions.  The diagnostic information is written to a file in the working directory.  The file name is dictated by the `/DiagnosticsFile` parameter.

#### Azure Pipelines
Adding the `/DiagnosticsFile` parameter to the "Additional SqlPackage.exe Arguments" field in the Azure Pipeline SqlAzureDacpacDeployment configuration will cause the SqlPackage diagnostic information to be written to the file specified.  Following the SqlAzureDacpacDeployment task, the diagnostic file can be made available outside of the virtual environment by publishing a pipeline artifact as seen in the example below.

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
Adding the `/DiagnosticsFile` parameter to the "arguments" field in the GitHub Action sql-action configuration will cause the SqlPackage diagnostic information to be written to the file specified.  Following the sql-action task, the diagnostic file can be made available outside of the virtual environment by publishing an artifact as seen in the example below.

```yaml
- name: Azure SQL Deploy
  uses: Azure/sql-action@v1
  with:
    # Name of the Azure SQL Server name, like Fabrikam.database.windows.net.
    server-name: ${{ secrets.AZURE_SQL_SERVER }}
    # The connection string, including authentication information, for the Azure SQL Server database.
    connection-string: ${{ secrets.AZURE_SQL_CONNECTION_STRING }}
    # Path to DACPAC file to deploy
    dacpac-package: .\DatabaseProjectAdventureWorksLT\bin\Release\DatabaseProjectAdventureWorksLT.dacpac
    # additional SqlPackage.exe arguments
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
