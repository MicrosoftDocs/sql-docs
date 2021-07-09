---
title: SqlPackage in development pipelines
description: Learn how to troubleshoot database development pipelines with SqlPackage.exe.
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan; sstein"
ms.date: 06/25/2021
---

# SqlPackage in development pipelines

**SqlPackage.exe** is a command-line utility that automates several database development tasks and can be incorporated into CI/CD pipelines.

## Managed virtual environments

The virtual environments used for GitHub Actions hosted runners and Azure Pipelines VM images are managed in the [virtual-environments](https://github.com/actions/virtual-environments) GitHub repository.  SqlPackage is included in the `windows-latest` environment and updates to the images are made within a few weeks of each SqlPackage release.

## Checking the SqlPackage version

During troubleshooting efforts, it is important to know the SqlPackage version in use.  Capturing this information can be done by adding a step to the pipeline to run SqlPackage with the `/version` parameter.  Examples are given below based on the Microsoft and GitHub managed environments, self-hosted environments may have different installation paths for the working directory.

### Azure Pipelines

By using the [script](/azure/devops/pipelines/yaml-schema#script) keyword in an Azure Pipeline, a step can be added to an Azure Pipeline that outputs the SqlPackage version number.

```yaml
- script: sqlpackage.exe /version
  workingDirectory: C:\Program Files\Microsoft SQL Server\150\DAC\bin\
  displayName: 'get sqlpackage version'
```

### GitHub Actions

By using the [run](https://docs.github.com/en/free-pro-team@latest/actions/reference/workflow-syntax-for-github-actions) keyword in a GitHub Action workflow, a step can be added to a GitHub Action that outputs the SqlPackage version number.

```yaml
- name: get sqlpackage version
  working-directory: C:\Program Files\Microsoft SQL Server\150\DAC\bin\
  run: ./sqlpackage.exe /version
```

:::image type="content" source="media/sqlpackage-pipelines-github-action.png" alt-text="GitHub action output displaying build number 15.0.4897.1":::

## Update SqlPackage on the pipeline agent

In some scenarios, the current version of SqlPackage installed in the pipeline environment may be insufficient. An additional step can be used to install a newer version of SqlPackage. It is important to run the install step before running any DacPac or BacPac operations in the pipeline. This task can be combined with a step to [check the version](#checking-the-sqlpackage-version) to ensure that the upgrade completed as expected.

### Azure Pipelines
By using the [PowerShell](/azure/devops/pipelines/tasks/utility/powershell) task in an Azure Pipeline, a step can be added to an Azure Pipeline that downloads the desired MSI and installs it silently. 

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

- Learn more about [sqlpackage](sqlpackage.md)