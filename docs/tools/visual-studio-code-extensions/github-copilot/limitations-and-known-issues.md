---
title: Limitations and Known Issues with GitHub Copilot
titleSuffix: MSSQL Extension for Visual Studio Code
description: Learn about the limitations, nongoals, and known constraints of GitHub Copilot integration with the MSSQL extension for Visual Studio Code.
author: croblesm
ms.author: roblescarlos
ms.reviewer: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: concept-article
ms.collection:
  - data-tools
  - ce-skilling-ai-copilot
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---

# Limitations and known issues

This article outlines the limitations and known constraints of GitHub Copilot integration with the MSSQL extension for Visual Studio Code. This experience is designed for application developers building with SQL databases, not for database administrators managing infrastructure or production environments. Understanding these boundaries ensures proper expectations and supports a productive development workflow.

## Functional limitations

- GitHub Copilot **does not have permission to write data**. Developers must manually review and execute all generated SQL or object-relational mapping (ORM) code.

- The `@mssql` chat participant **needs an active database connection** through the editor to provide schema-aware suggestions.

- **Always review AI-generated responses** before use. GitHub Copilot might produce incorrect or suboptimal recommendations.

- This experience is **designed for developers**, not for database or system administrators. While it can generate SQL scripts for administrative tasks, GitHub Copilot does **not support server management operations** such as configuring backup/restore, managing user permissions, or handling SQL Agent jobs.

- GitHub Copilot sessions **do not persist history when switching context** (for example, changing files or databases). New context resets the chat memory.

- The chat participant works **within the scope of the currently connected database**. Cross-database operations aren't supported.

- The integration is **optimized for modern SQL Database in Fabric, Azure SQL Database, and SQL Server**. Legacy or deprecated features might be unsupported.

- **Azure Synapse Analytics** and its Dedicated SQL pool (formerly SQL DW) features **are not supported** by this GitHub Copilot integration.

- GitHub Copilot provides the best suggestions when it has access to rich context. Keep your database connection active and relevant code or queries open in the editor. The more context GitHub Copilot has, the more accurate and relevant its suggestions are.

## Technical constraints

- **Internet connectivity is required**. GitHub Copilot needs access to the GitHub Copilot cloud service to provide suggestions.

- GitHub Copilot **only accesses open files and the active database connection**. It can't browse folders or repositories unless you explicitly open them in the editor.

- GitHub Copilot **does not support offline usage** or disconnected development.

- **Advanced performance tuning** (for example, full telemetry analysis, workload insights) is out of scope. The assistant might suggest optimizations for individual queries but doesn't replace professional tuning tools.

- GitHub Copilot might struggle with deeply nested or multi-join queries, particularly when working with large datasets or under-specified schema context.

## Privacy and system-generated log collection

- GitHub Copilot **doesn't persist chat interactions**. Prompts and responses are routed through GitHub's privacy-preserving proxy service without being stored.

- **User prompts and completions aren't used** to train the GitHub Copilot models.

- **No chat content is stored** by the MSSQL extension or GitHub Copilot.

- **System-generated log collected by the extension is limited** to usage analytics for improving the product and doesn't include personal data.

To understand how GitHub Copilot safeguards your data and maintains transparency around AI model training and data practices, visit the [GitHub Copilot Trust Center](https://copilot.github.trust.page/) or explore [GitHub's approach to data handling with Copilot](https://resources.github.com/learn/pathways/copilot/essentials/how-github-copilot-handles-data/).

## Known GitHub Copilot limitations

These limitations apply to GitHub Copilot more broadly and might affect your experience in the MSSQL extension:

- GitHub Copilot **might hallucinate APIs or schema elements** that don't exist, especially if context is limited.
- GitHub Copilot **does not have real-time knowledge of remote files** unless you open them in your editor.
- GitHub Copilot **does not track variable types or state across long conversations**; results might drift in relevance.

For more information, see:

- [Best practices for using GitHub Copilot](https://docs.github.com/copilot/get-started/best-practices)
- [Tips and tricks for Copilot in Visual Studio Code](https://code.visualstudio.com/docs/copilot/copilot-tips-and-tricks)
- [Security considerations for GitHub Copilot in Visual Studio Code](https://code.visualstudio.com/docs/copilot/security)

## Share your experience

[!INCLUDE [copilot-feedback](../includes/copilot-feedback.md)]

## Related content

- [GitHub Copilot Trust Center - How GitHub Copilot handles data](https://copilot.github.trust.page/)
- [How GitHub Copilot handles data - Learning pathway](https://resources.github.com/learn/pathways/copilot/essentials/how-github-copilot-handles-data/)
- [GitHub Copilot for MSSQL extension for Visual Studio Code](overview.md)
- [Quickstart: Use chat and inline GitHub Copilot suggestions](inline-copilot-suggestions.md)
- [Quickstart: Generate code](code-generation.md)
- [Quickstart: Use the schema explorer and designer](schema-explorer-designer.md)
- [Quickstart: Use the smart query builder](smart-query-builder.md)
- [Quickstart: Query optimizer assistant](query-optimizer-assistant.md)
- [Quickstart: Use the business logic explainer](business-logic-explainer.md)
- [Quickstart: Security analyzer](security-analyzer.md)
- [Quickstart: Localization and formatting helper](localization-formatting-helper.md)
- [Quickstart: Generate data for testing and mocking](test-and-mocking-data-generator.md)
