---
title: "Copilot in Azure SQL Database"
description: "Learn more about the features and possibilities of Copilot in Azure SQL Database for administrators and developers."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: josephsack
ms.date: 03/13/2024
ms.service: sql-database
ms.subservice: ai-copilot
ms.topic: overview
monikerRange: "=azuresql||=azuresql-db"
---

# Copilot in Azure SQL Database (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Copilot in Azure SQL Database is an AI assistant designed to streamline the design, operation, optimization, and health of Azure SQL Database-driven applications. It improves productivity in the Azure portal by offering natural language to SQL conversion and self-help for database administration.

> [!NOTE]
> Copilot in Azure SQL Database is currently in a limited public preview for a limited number of early adopters. To sign up for this program, visit [Request Access to Copilot in Azure SQL Database: Limited Access Public Preview](https://aka.ms/sqlcopilot-signup).

Copilot provides relevant answers to user questions, simplifying database management by leveraging database context, documentation, dynamic management views, Query Store, and other knowledge sources. For example:

- Database administrators can independently manage databases and resolve issues, or learn more about the performance and capabilities of your database.
- Developers can ask questions about their data as they would in text or conversation to generate a T-SQL query. Developers can also learn to write queries faster through detailed explanations of the generated query.

## Copilot in Azure SQL Database experiences

Copilot in Azure SQL Database provides:

- [Natural language to SQL](#natural-language-to-sql-query): This experience within the [Azure portal query editor for Azure SQL Database](../database/query-editor.md) translates natural language queries into SQL, making database interactions more intuitive.

- [Azure Copilot integration](#microsoft-copilot-for-azure-enhanced-scenarios): This experience adds Azure SQL Database skills into [Microsoft Copilot for Azure](/azure/copilot/overview), customers with self-guided assistance, empowering them to manage their databases and solve issues independently.

Copilot in Azure SQL Database integrates data and formulate applicable responses using public documentation, dynamic management views, catalog views, and Azure supportability diagnostics.

### Natural language to SQL query

This experience within the [Azure portal query editor for Azure SQL Database](../database/query-editor.md) utilizes table and view names, column names, primary key, and foreign key metadata to generate T-SQL code. You can then review and execute the code suggestion.

This integration means that Copilot can answer questions with prompts like: 

 - `Which agents have listed more than two properties for sale?`
 - `Tell me the rank of each agent by property sales and show name, total sales, and rank`
 - Even advanced scenarios such as `Show me a pivot summary table that displays the number of properties sold in each year from 2020 to 2023`

For a tutorial and examples of natural language to SQL capabilities of the Copilot in Azure SQL Database, see [Natural language to SQL in the Azure portal Query editor (preview)](../copilot/query-editor-natural-language-to-sql-copilot.md).

### Microsoft Copilot for Azure enhanced scenarios

You can ask and receive helpful, context-rich suggestions from [Microsoft Copilot for Azure](/azure/copilot/overview) within the Azure portal. Get answers with prompts like: 

- `What are the active connections running right now?`
- `What are the top high CPU queries run in the last week`
- `My database is slow.`

> [!WARNING]
> Copilot in Azure SQL Database is a preview set of experiences that are powered by large language models (LLMs). Output produced by Copilot might contain inaccuracies, biases, or other unintended content. This occurs because the model powering Copilot in Azure SQL Database was trained on information from the internet and other sources. As with any generative AI model, humans should review the output produced by Copilot before use.

## Sample prompts

Here are a few examples of sample prompts you can provide today for different capability areas:

| Topic | Example prompts |
| --- | --- |
| Active user connections | `What are the active connections of the databases?` |
| Automatic tuning options | `What is my automatic tuning configuration?` |
| Blocking sessions | `I want to find blocking sessions.` |
| Database basic information | `What is the logical server name for this database?` |
| Database compatibility level | `How to view and change database compatibility level?` |
| Database permissions | `What is the list of database permissions that are granted?` |
| Database size | `What's the database size?` |
| Database tables | `Provide a list of tables in my database.` |
| Deadlocks | `Have any deadlocks happened recently?` |
| Fragmented index | `Which of my indexes are fragmented?` |
| Investigate query performance issues | `My database is slow? Why is my query slow?` |
| Missing index | `Any missing index suggestions for improving query performance?` |
| Query store | `Which queries have forced plans?`<br /> `Which queries have high execution time variation?` <br />`Show me the longest running queries over the last 24 hours.`  |
| Resource usage | `I want to find my database resource usage.` |
| Troubleshoot connection issues | `My SQL database connection dropped. What happened?` |
| Troubleshoot high CPU | `Why is my CPU usage high?` |
| Troubleshoot Query Store | `Why is Query Store in read only mode?` |
| Troubleshoot query timeouts | `I just had a query timeout.`|
| Wait types | `What are the wait type stats for my database?` |

## Examples

- When you're working with a slow Azure SQL Database, you could provide the prompt `My database is slow`.

   Microsoft Copilot for Azure (preview) starts looking at your database based on your context in the Azure portal. After the check, Copilot will detail specific areas that might be contributing to the issue. In this example, there was a specific query driving high CPU utilization:

   :::image type="content" source="media/copilot-azure-sql-overview/slow-database.png" alt-text="Screenshot showing the query prompt and generated sample query about high CPU utilization." lightbox="media/copilot-azure-sql-overview/slow-database.png":::

- You can continue the conversation and investigation with a prompt of `How can I tune that high CPU query?`.

   Copilot understands that this prompt refers to the query identified earlier, and provides a new index suggestion:

   :::image type="content" source="media/copilot-azure-sql-overview/high-cpu-query.png" alt-text="Screenshot showing a second query prompt in the conversation and generated sample query about high CPU utilization." lightbox="media/copilot-azure-sql-overview/high-cpu-query.png":::

## Related content

- [Frequently asked questions about Copilot in Azure SQL Database (preview)](copilot-azure-sql-faq.yml)
- [What is Microsoft Copilot for Azure?](/azure/copilot/overview)
- [Responsible AI FAQ for Microsoft Copilot for Azure (preview)](/azure/copilot/responsible-ai-faq)
- [Intelligent applications with Azure SQL Database](../database/ai-artificial-intelligence-intelligent-applications.md)
