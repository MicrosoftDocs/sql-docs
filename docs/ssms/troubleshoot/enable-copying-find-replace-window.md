---
description: "Workaround to enable copying from find and replace window "
title: Enable copying from find and replace window
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: c28ffa44-7b8b-4efa-b755-c7a3b1c11ce4
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.custom: seo-lt-2019
ms.date: 11/03/2020
---

# Workaround to enable copying from find and replace window

[!INCLUDE[Applies to](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

A workaround is required to enable copying from find and replace window.  If you find you are unable to copy from find and replace window in SQL Server Management Studio, follow the [workaround](#workaround) below.

## Error Message

When you attempt to copy text from the find and replace window in SQL Server Management Studio, you get an error message.

> Unsaved documents cannot be cut or copied to the clipboard from the miscellaneous files project. You must save the unsaved document(s) before cutting or copying them.

![Error dialog for: Unsaved documents cannot be cut or copied to the clipboard from the miscellaneous files project. You must save the unsaved document(s) before cutting or copying them](../media/troubleshoot/unable-copy-find-replace-window.png)

## Workaround

To enable copying text from the find and replace window, follow these steps:

1. In the **Tools** menu, open **Options**.

2. Under **Environment**>**Documents**, uncheck the item for "Show Miscellaneous files in Solution Explorer"

3. Close and reopen SQL Server Management Studio

![Options window with "Show Miscellaneous files in Solution Explorer" unchecked](../media/troubleshoot/fix-copy-find-replace-window.png)

