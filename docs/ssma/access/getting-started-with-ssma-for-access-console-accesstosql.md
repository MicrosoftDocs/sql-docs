---
title: "Getting Started with SSMA for Access Console (AccessToSQL)"
description: "Getting Started with SSMA for Access Console (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: get-started
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-get-started
---
# Get started with the SSMA Console (AccessToSQL)

This section describes how to launch and get started with the Access console application. It also lists the conventions used in a typical SSMA Console output window.

## Launch SSMA Console

Use the following steps to start the SSMA Console application:

1. Go to **Start** and point to **All Programs**.

1. Select the **SQL Server Migration Assistant for Access Command Prompt** shortcut.

   The command prompt displays the SSMA Console usage menu and `(/? Help)`, to help you get started with the console application.

## Procedure for using the SSMA Console

After you launch the console on your Windows system, use the following steps to work on it:

1. Configure SSMA Console through the script files. For more information, see [Create script files](creating-script-files-accesstosql.md).

1. [Create variable value files](creating-variable-value-files-accesstosql.md).

1. [Create the server connection files](creating-the-server-connection-files-accesstosql.md).

1. [Execute the SSMA Console](executing-the-ssma-console-accesstosql.md) based on your project needs.

Additional features:

1. [Manage passwords](managing-passwords-accesstosql.md) and export or import them onto other Windows machines.

1. [Generate reports](generating-reports-accesstosql.md) to view the detailed XML output reports for assessment, conversion, and data migration. Detailed error reports can also be generated for refresh and synchronization commands.

## SSMA Console output conventions

When you run the SSMA script commands and options, the console program shows the results and messages (like information or errors) on the console. If needed, it sends these messages to an XML output file. Each type of message in the output uses a unique color. For example, the text message in white color shows script file commands. The text message in green color represents a prompt for user input.

:::image type="content" source="media/ssma-console-output.png" alt-text="Screenshot of SSMA Console output (Access).":::

The following table explains the color interpretation of the console output:

| Color | Description |
| --- | --- |
| Red | Fatal error during execution |
| Gray | Date and time stamp, message to the user |
| White | Script file commands, message type |
| Yellow | Warning |
| Green | Prompt for user input |
| Cyan | Start, finish, and result of an operation |

## Related content

- [Install SQL Server Migration Assistant for Access](installing-sql-server-migration-assistant-for-access-accesstosql.md)
