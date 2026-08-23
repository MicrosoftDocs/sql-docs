---
title: Troubleshoot SQL Project Build
description: This article provides troubleshooting steps for SQL project build errors.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 02/25/2025
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: how-to
ms.collection:
  - data-tools
---

# Troubleshoot SQL project build

The SQL project build output provides feedback on the database model construction and T-SQL validation. The default command line output only shows errors and some status information. In this article, we discuss how to enable more verbose logging to help troubleshoot build issues and common errors that are encountered with SQL projects.

## Enable verbose logging

To further troubleshoot build issues for SQL projects, you can use command line switches to generate more logs. More logging can help in not only identifying the cause of errors but also degradation in build speed. The two primary options are:

- **Binary logger**: This option generates a binary log file (`msbuild.binlog`) that can be viewed using the [MSBuild Log Viewer](https://msbuildlog.com/). This viewer is helpful for diagnosing dependency issues and optimizing the build process. The command to generate this log is:

    ```bash
    dotnet build -bl
    ```

- **File logger**: This option generates a text log file that contains the most verbose logging from the build. The command to generate this log is:

    ```bash
    dotnet build -flp:v=diag
    ```

To recap, the combined command to generate both logs is:

```bash
dotnet build -bl -flp:v=diag
```

The full set of switches can be found at the [MSBuild Command-Line Reference](/visualstudio/msbuild/msbuild-command-line-reference#switches-for-loggers).

## Common issues

### Build errors

When the build error indicates invalid syntax, the output also specifies which file contains the erroneous code. If you're using syntax that was recently added, you may need to update your project SDK version.​

Build errors from a database project should have `SQLxxxxx` error code​, where `xxxxx` is a five-digit number. Some issues that cause the error for an unresolved reference (`SQL71501`/`SQL71502`)​ are:

- Ambiguous object names​. Recommendations:
  - Use fully resolved names (`[schema].[table].[column]`)​
  - Rename objects as necessary​
- System objects​. Recommendation:
  - Add reference to master or msdb via [package reference​](../concepts/package-references.md) or [database reference​](../concepts/database-references.md)​
- External references. Recommendation:
  - Ensure SQLCMD variables are set correctly​ for the [database reference](../concepts/database-references.md)​ or [package reference](../concepts/package-references.md)​


### Other failures

For an error that occurs during restore, first run a clean build after deleting the `/bin` and `/obj` folders in the project.

If the error includes `SDK 'Microsoft.Build.Sql' specified could not be found​`, begin by verifying NuGet package feeds are valid. The base command to view current feeds is:

```bash
dotnet nuget list source​
```

The public NuGet feed is:

```bash
dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org​
```

If your environment requires private feeds, ensure they're valid and accessible. You may be required to authenticate with package feeds. Enabling authentication during project build can be done with:

```bash
dotnet build --interactive
```

For MSBuild, the equivalent command is:

```bash
msbuild /p:nugetInteractive=true​
```

For other non-SQL error codes, refer to the following resources:

- MSBuild errors: [MSBuild Reference​](/visualstudio/msbuild/msbuild-reference)
- NETSDK errors: [.NET SDK error list ​](/dotnet/core/tools/sdk-errors/)
- NuGet errors: [NuGet Errors and Warnings Reference](/nuget/reference/errors-and-warnings)

## Related content

- [What are SQL database projects?](../sql-database-projects.md)
- [SQL projects tools](../sql-projects-tools.md)
- [Troubleshoot issues and performance with SqlPackage](../../sqlpackage/troubleshooting-issues-and-performance-with-sqlpackage.md)
