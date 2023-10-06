---
title: GitHub Copilot extension - Overview
description: Install GitHub Copilot extension for Azure Data Studio and get started.
author: subhojit-msft
ms.author: subasak
ms.reviewer: erinstellato, drskwier, randolphwest
ms.date: 10/05/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# GitHub Copilot extension: Overview

[GitHub Copilot](https://github.com/features/copilot) is an AI-powered pair programmer extension for [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)] that provides you with context-aware code completions, suggestions, and even entire code snippets. This powerful tool helps developers write code more efficiently, reduce the time spent on repetitive tasks, and minimize errors.

## What is GitHub Copilot?

GitHub Copilot for [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)] can be used in any editor window. To use GitHub Copilot, you must have an active internet connection. You can use GitHub Copilot in the following ways:

1. When you type code in the editor, GitHub Copilot provides suggestions in real-time.
1. When you type a natural language comment, GitHub Copilot provides suggestions for code that corresponds to the comment.

To accept a suggestion, press `Tab`. To reject a suggestion, press `Esc`.

At any time, pressing `Ctrl`+`Enter` opens the GitHub Copilot Completions Panel, which provides suggestions for code based on the context of the editor.

GitHub Copilot chat isn't currently available for [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)].

## Install the GitHub Copilot extension

To get started, all you need is [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)] [version 1.44](../release-notes-azure-data-studio.md#may-2023) or later, and a GitHub Copilot [subscription](https://docs.github.com/en/enterprise-cloud@latest/billing/managing-billing-for-github-copilot/about-billing-for-github-copilot).

> [!TIP]  
> GitHub Copilot is free for verified students and for maintainers of popular open source projects on GitHub.

1. Select the Extensions Icon to view the available extensions.

   :::image type="content" source="media/add-extensions/extension-manager-icon.png" alt-text="Screenshot showing the Extension manager icon.":::

1. Search for the **GitHub Copilot** extension and select it to view its details. Select **Install** to add the extension.

## How GitHub Copilot works

GitHub Copilot works by utilizing advanced machine learning models trained on a vast dataset of publicly available code from GitHub repositories. As you type code, the AI analyzes the context and provides relevant suggestions in real-time. You can receive suggestions also by writing a natural language comment describing what you want the code to do.

The GitHub Copilot extension in [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)] uses context from the editor to provide suggestions. For example, if you're writing a query that joins two tables, GitHub Copilot suggests the join condition from columns in the open editor, other files in the workspace, and common syntax patterns.

## Privacy

Your code is yours. We follow responsible practices in accordance with our [Privacy Statement](https://docs.github.com/site-policy/privacy-policies/github-privacy-statement) to ensure that your code snippets aren't used as suggested code for other users of GitHub Copilot.
