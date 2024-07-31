---
title: "Get started with SSMA for Db2 console (Db2ToSQL)"
description: Learn how to launch and get started with the SSMA for Db2 console application.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-get-started
---
# Get started with SSMA for Db2 console (Db2ToSQL)

This article describes the procedure to launch and get started with the SQL Server Migration Assistant (SSMA) for Db2 console application. You can also learn about the conventions used in a typical SSMA console output window.

## Launch SSMA console

Use the following steps to start the SSMA console application:

1. From the **Start** menu, navigate to **All Programs** > **SQL Server Migration Assistant for Db2 Command Prompt** shortcut.

   It displays the SSMA console usage menu and `(/? Help)`, to help you get started with the console application.

## Procedure for using the SSMA console

After the console is successfully launched on your Windows system, you could use the following steps to work on it:

1. Configure SSMA console through the script files. For more information on this section, see [Create script files](creating-script-files-db2tosql.md) .

1. [Create variable value files](creating-variable-value-files-db2tosql.md)

1. [Create the server connection files](creating-the-server-connection-files-db2tosql.md)

1. [Execute the SSMA console](executing-the-ssma-console-db2tosql.md) based on your project needs

Extra features:

1. [Manage Passwords](managing-passwords-db2tosql.md) and export/ import it onto other Window machines

1. [Generate reports](generating-reports-db2tosql.md) to view the detailed XML output reports for assessment /conversion and data migration. Detailed error reports can also be generated for refresh and synchronization commands.

## SSMA console output conventions

After the SSMA script commands and options are executed, the console program displays the results and messages (information, error, etc.) to the user on the console or if necessary, redirects to an XML output file. Each type of message in the output is displayed in a unique color. For example, the text message in white color denotes script file commands; the one in green color represents a prompt for user-input, and so on.

:::image type="content" source="media/ssmaconsoleoutput_oracle.jpg" alt-text="Screenshot of SSMA console Output (Oracle).":::

Color-interpretation of the console output in the following table:

| Color | Description |
| --- | --- |
| **Red** | Fatal error during execution |
| **Gray** | Date and time stamp, message to the user |
| **White** | Script file commands, message type |
| **Yellow** | Warning |
| **Green** | Prompt for user-input |
| **Cyan** | Start, finish, and result of an operation |

## Related content

- [Install SSMA for Db2 (Db2ToSQL)](installing-ssma-for-db2-db2tosql.md)
