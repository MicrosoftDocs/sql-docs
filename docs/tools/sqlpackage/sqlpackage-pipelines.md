---
title: SqlPackage in development pipelines
description: Learn how to troubleshoot database development pipelines with SqlPackage.exe by checking the installed build number.
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan; sstein"
ms.date: 11/4/2020
---

# SqlPackage in development pipelines

**SqlPackage.exe** is a command-line utility that automates several database development tasks and can be incorporated into CI/CD pipelines.

## Virtual environments

### Managed virtual environments

The virtual environments used for GitHub Actions hosted runners and Azure Pipelines VM images are managed in the [virtual-environments](https://github.com/actions/virtual-environments) GitHub repository.  SqlPackage is included in several environments including `windows-latest` and `ubuntu-latest`. Updates to the images in [virtual-environments](https://github.com/actions/virtual-environments) are made within a few weeks of each SqlPackage release.

### Self-hosted virtual environments

If you are utilizing SqlPackage in a self-hosted virtual environment such as a self-hosted Azure DevOps agent, it is recommended to [update the application](sqlpackage-download.md) on a regular basis to maintain the environment with the latest version.

## Checking the SqlPackage version

During troubleshooting efforts, it is important to know the SqlPackage version is in use.  Capturing this information can be done by adding a step to the pipeline to run SqlPackage with the `/version` parameter.  Examples are given below based on the Microsoft and GitHub managed environments, self-hosted environments may have different installation paths for the working directory.

### Azure Pipelines

By leveraging the [script](/azure/devops/pipelines/yaml-schema#script) keyword in an Azure Pipeline, a step can be added to an Azure Pipeline that outputs the SqlPackage version number.

```yaml
- script: sqlpackage.exe /version
  workingDirectory: C:\Program Files\Microsoft SQL Server\150\DAC\bin\
  displayName: 'get sqlpackage version'
```

### GitHub Actions

By leveraging the [run](https://docs.github.com/en/free-pro-team@latest/actions/reference/workflow-syntax-for-github-actions) keyword in a GitHub Action workflow, a step can be added to a GitHub Action that outputs the SqlPackage version number.

```yaml
- name: get sqlpackage version
  working-directory: C:\Program Files\Microsoft SQL Server\150\DAC\bin\
  run: ./sqlpackage.exe /version
```

:::image type="content" source="media/sqlpackage-pipelines-github-action.png" alt-text="GitHub action output displaying build number 15.0.4897.1":::

## Tracking deployments

There are a few files related to SqlPackage that can be captured as pipeline artifacts to create pipeline execution reproducibility and improve deployment tracking. The implementation and use cases vary dependent on your specific architecture and automation environment.

- **Dacpac file**
- **Diagnostic file output from any action:** Use the `/DiagnosticsFile:` parameter on any SqlPackage action
- **Output from script action prior to publish action:**  Use the [Script](sqlpackage-script.md) SqlPackage action before invoking a publish action

## Next steps

- Learn more about [sqlpackage](sqlpackage.md)