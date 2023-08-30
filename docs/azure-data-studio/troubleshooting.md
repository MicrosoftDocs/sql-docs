---
title: Azure Data Studio troubleshooting
description: "Learn how to get logs and troubleshoot Azure Data Studio, which is helpful in reporting bug reports."
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 08/29/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# Azure Data Studio troubleshooting

Azure Data Studio tracks issues and feature requests using on a [GitHub repository issue tracker](https://github.com/Microsoft/azuredatastudio/issues) for the `azuredatastudio` repository.

## Report an issue you've experienced

Report issues to [GitHub Issue Tracker](https://github.com/Microsoft/azuredatastudio/issues) and let us know any details that help reproduce the error. Include any [log information](#how-to-set-the-logging-level) from the log file.

## Write good bug reports and feature requests

File a single issue per problem and feature request.

- Don't enumerate multiple bugs or feature requests in the same issue.
- Don't add your issue as a comment to an existing issue unless it's for the identical input. Many issues look similar, but have different causes.

The more information you can provide, the more likely someone will be successful reproducing the issue and finding a fix.

Include the following information with each issue:

- Version of Azure Data Studio
- Reproducible steps (1... 2... 3...) and what you expected versus what you actually saw.
- Images, animations, or a link to a video. Images and animations illustrate repro-steps but don't replace them.
- A code snippet that demonstrates the issue or a link to a code repository we can easily pull down onto our machine to recreate the issue.

> [!NOTE]  
> Because we need to copy and paste the code snippet, including a code snippet as a media file (that is, `.gif`) isn't sufficient.

- Errors in the Dev Tools Console (**Help > Toggle Developer Tools**)

Remember to take the following steps:

- Search the issue repository to see if the same issue already exists.
- Simplify your code around the issue so we can better isolate the problem.

Don't feel bad if we can't reproduce the issue and ask for more information!

## How to set the logging level

### Azure Data Studio

From the **Command Palette (Ctrl/Cmd + Shift + P)**, run the **Developer: Set Log Level...** command to select the log level for the current session. This value isn't persisted over multiple sessions. If you restart Azure Data Studio, it reverts back to the default `Info` level.

If you want to enable debug logging for startup then set the log level to `Debug` and run the **Developer: Reload Window** command.

### MSSQL (Built-in extension)

If the `Mssql: Log Debug Info` user setting is set to true, then debug log info is sent to the `MSSQL` output channel.

The `Mssql: Tracing Level` user setting is used to control the verbosity of the logging.

## Azure Data Studio logs and location

From the **Command Palette (Ctrl/Cmd + Shift + P)**, run the **Developer: Open Logs Folder** command to open the path to the logs. There are many different types of log files that write there. A few of the commonly used ones are:

1. `renderer#.log` (for example, `renderer1.log`): this file is the log file for the main process.
1. `telemetry.log`: When the log level is set to `Trace`, this file contains the telemetry events sent by Azure Data Studio
1. `exthost#/exthost.log`: Log file for the extension host process (this is only the process itself, not the extensions running inside it)
1. `exthost#/Microsoft.mssql`: Logs for the mssql extension, which contains much of the core logic for MSSQL-related features
   - `sqltools.log` is the log for SQL Tools service
1. `exthost#/output_logging_#######`: these folders contain the messages displayed in the `Output` panel in Azure Data Studio. Each file is named `#-<Channel Name>`. For example, the `Notebooks` output channel may output to a file named `3-Notebooks.log`.

If you're asked to provide logs, zip up the entire folder to ensure that the correct logs are included.

## Recover editor files after a crash

If Azure Data Studio crashes unexpectedly, restarting Azure Data Studio should reopen any unsaved editor files as well. If they don't, there may have been an error trying to reopen them. In that case, follow these steps to navigate to the folder containing the backups where you can manually open them to recover their contents.

1. From the **Command Palette** (**Ctrl/Cmd + Shift + P**), run the **Developer: Open User Data Folder** command.
1. In the folder that opens, navigate to the `Backups` folder.

This contains folders with randomly generated names. Within these folders are files that contain the editor backups (which include some additional metadata along with their text contents).

## Next steps

- [Report an issue](https://github.com/Microsoft/azuredatastudio/issues)
- [What is Azure Data Studio](what-is-azure-data-studio.md)
