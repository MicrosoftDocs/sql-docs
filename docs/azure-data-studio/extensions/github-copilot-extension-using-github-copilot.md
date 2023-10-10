---
title: GitHub Copilot extension - Using GitHub Copilot
description: Using GitHub Copilot extension for Azure Data Studio.
author: subhojit-msft
ms.author: subasak
ms.reviewer: erinstellato, randolphwest
ms.date: 10/05/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# Use GitHub Copilot

This article describes ways to use the [GitHub Copilot extension](github-copilot-extension-overview.md) for [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)].

## Copilot prompts for common SQL syntax

When you create Transact-SQL (T-SQL) in an editor, GitHub Copilot can provide suggestions for common T-SQL syntax. For example, if you're writing a query that joins two tables, Copilot suggests the join condition from columns in the open editor, other files in the workspace, and common syntax patterns.

:::image type="content" source="media/github-copilot-extension/common-syntax.png" alt-text="Screenshot showing Copilot prompting the autocompletion of a table join and where statement.":::

## Copilot prompts from comments

Copilot's suggestions can be directed from comments in the editor, including natural language comments. For example, if you write a comment that describes a query, Copilot provides suggestions for the query.

:::image type="content" source="media/github-copilot-extension/code-suggestion-from-comment.png" alt-text="Screenshot showing Copilot prompting the beginning of a PIVOT query." :::

Copilot's suggestions may complete parts of the syntax required for the query, or may complete the entire query. In the previous example, Copilot began the query to PIVOT the data. In the following image, Copilot completed the PIVOT query based on the comment.

:::image type="content" source="media/github-copilot-extension/code-suggestion-from-comment-2.png" alt-text="Screenshot showing Copilot prompting the completion of a PIVOT query.":::

## See alternative suggestions

During Copilot's use in a T-SQL editor, you can see alternative suggestions, if any are available, by pressing `Alt`+`[` (or `Option`+`[` on macOS) to cycle through the suggestions. The previous suggestion is shown by pressing `Alt`+`]` (or `Option`+`]` on macOS).

You can see multiple suggestions by pressing `Ctrl`+`Enter` to open the Copilot Completions Panel. The Copilot Completions Panel shows multiple suggestions for the current context of the editor.

## Work with IntelliSense

GitHub Copilot works with IntelliSense to provide suggestions for code completion. IntelliSense is a feature of [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)] that provides suggestions for code completion, parameter info, and object names. IntelliSense is enabled by default in [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)] and provides its suggestions based on the context of the current connection and all SQL syntax.

The suggestions provided by IntelliSense are completion of a single word or phrase. GitHub Copilot provides suggestions for entire lines of code, including syntax and formatting.

## IntelliSense, Code Snippets, GitHub Copilot

When you develop your code in [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)], you have three assistants to help you:

- [IntelliSense](/visualstudio/ide/using-intellisense)
- [Create and use code snippets to quickly create Transact-SQL (T-SQL) scripts in Azure Data Studio](../code-snippets.md)
- [GitHub Copilot](https://github.com/features/copilot)

Consider these assistants as an extra pair of hands that work independently. It's crucial to understand the difference between them, and when to use them.

| Assistant | Overview | Activation and deactivation |
| --- | --- | --- |
| **IntelliSense** | Feature in [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)] code editor that provides suggestion for code completion, parameter info, and object names. | Enabled by default in [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)]. To deactivate, Go to Settings or press `Ctrl` + `,`, type IntelliSense and uncheck the boxes as per requirement. |
| **Code Snippets** | Mini T-SQL code templates that are inbuilt or custom made, which assist with generating proper code syntax. | Enabled by default in [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)]. To deactivate, Go to Settings or press `Ctrl` + `,`, type Snippets and uncheck the boxes as per requirement. |
| **GitHub Copilot** | AI powered pair programmer extension that provides context-aware code completions, suggestions, and even entire code snippets. | Comes with the GitHub Copilot Extension in a subscription model. Can activate from Copilot status icon present in [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)] status bar. |

## Privacy

Your code is yours. We follow responsible practices in accordance with our [Privacy Statement](https://docs.github.com/site-policy/privacy-policies/github-privacy-statement) to ensure that your code snippets aren't used as suggested code for other users of GitHub Copilot.
