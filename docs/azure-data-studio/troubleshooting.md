---
title: Azure Data Studio Troubleshooting
description: "Learn how to get logs and troubleshoot Azure Data Studio, which is helpful in reporting bug reports."
author: dzsquared
ms.author: drskwier
ms.reviewer: hanqin, maghan
ms.date: 11/24/2020
ms.service: azure-data-studio
ms.topic: conceptual
ms.custom: seodec18
---

# Azure Data Studio Troubleshooting
Azure Data Studio tracks issues and feature requests using on a [GitHub repository issue tracker](https://github.com/Microsoft/azuredatastudio/issues) for the `azuredatastudio` repository. 

## If you've experienced any issue

Report issues to [GitHub Issue Tracker](https://github.com/Microsoft/azuredatastudio/issues) and let us know any details that will help reproduce the error. Include any [log information](#how-to-set-the-logging-level) from the log file.

## Writing good bug reports and feature requests

File a single issue per problem and feature request.

* Don't enumerate multiple bugs or feature requests in the same issue.
* Don't add your issue as a comment to an existing issue unless it's for the identical input. Many issues look similar, but have different causes.

The more information you can provide, the more likely someone will be successful reproducing the issue and finding a fix. 

Include the following information with each issue:

* Version of Azure Data Studio
* Reproducible steps (1... 2... 3...) and what you expected versus what you actually saw. 
* Images, animations, or a link to a video. Images and animations illustrate repro-steps but don't replace them.
* A code snippet that demonstrates the issue or a link to a code repository we can easily pull down onto our machine to recreate the issue. 

> [!NOTE]
>  Because we need to copy and paste the code snippet, including a code snippet as a media file (i.e. .gif) is not sufficient. 

* Errors in the Dev Tools Console (Help | Toggle Developer Tools)

Please remember to do the following:

* Search the issue repository to see if there exists a duplicate. 
* Simplify your code around the issue so we can better isolate the problem. 

Don't feel bad if we can't reproduce the issue and ask for more information!

## How to set the logging level

### Azure Data Studio
Run the `Developer: Set Log Level...` command to select the log level for the current session. This value is NOT persisted over multiple sessions - so if you restart Azure Data Studio it will revert back to the default `Info` level. 

If you want to enable debug logging for startup then set the log level to `Debug` and run the `Developer: Reload Window` command

### MSSQL (Built-In Extension)

If the `Mssql: Log Debug Info` user setting is set to true, then debug log info will be sent to the `MSSQL` output channel.

The `Mssql: Tracing Level` user setting is used to control the verbosity of the logging.

## Debug log location
From Azure Data Studio, run the `Developer: Open Logs Folder` command to open the path to the logs. There's many different types of log files that write there, a few of the commonly used ones are:

1. `renderer#.log` (for example, renderer1.log) - this file is the log file for the main process.
1. `telemetry.log` - When the log level is set to `Trace` this file will contain the telemetry events sent by Azure Data Studio
1. `exthost#/exthost.log` - Log file for the extension host process (this is only the process itself, not the extensions running inside it)
1. `exthost#/Microsoft.mssql` - Logs for the mssql extension, which contains much of the core logic for MSSQL-related features
   * sqltools.log is the log for SQL Tools Service
1. `exthost#/output_logging_#######` - these folders contain the messages displayed in the `Output` panel in Azure Data Studio. Each file is named `#-<Channel Name>` so for example the `Notebooks` output channel may output to a file named `3-Notebooks.log`.

If you are asked to provide logs, zip up the entire folder to ensure that the correct logs are included. 

## Next Steps
- [Report an issue](https://github.com/Microsoft/azuredatastudio/issues)
- [What is Azure Data Studio](what-is-azure-data-studio.md)