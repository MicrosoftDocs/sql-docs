---
title: "Copilot in Microsoft Azure SQL"
description: "Learn more about the features and possibilities of Copilot in Azure SQL for administrators and developers."
author: WilliamDAssaf
ms.author: wiassaf
ms.reviewer: josephsack
ms.service: sql-database
ms.subservice: ai-copilot
ms.topic: overview
ms.date: 03/15/2024
---

# Copilot in Azure SQL (preview)

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Copilot in Microsoft Azure SQL is an AI assistant designed to streamline the design, operation, optimization, and troubleshooting of SQL-driven applications from cloud to edge. It improves productivity by offering natural language to SQL conversion, self-help for database administration, and SQL code assistance. 

By leveraging database context, documentation, and other knowledge sources, Copilot provides relevant answers to user questions, simplifying database management. 

- Database administrators can independently manage databases and resolve issues, learn more about the performance and a capabilities of your database.
- Developers can boost productivity with intelligent database solutions, including intuitive code suggestions.

## Copilot in Azure SQL experiences

Copilot in Microsoft Azure SQL provides three experiences:

- [Natural language to SQL](#natural-language-to-sql-query): This experience translates natural language queries into SQL, making database interactions more intuitive. 

- [Azure Copilot integration](#azure-copilot-integration): This experience adds Azure SQL skills into Copilot for Azure, customers with self-guided assistance, empowering them to manage their databases and solve issues independently.

- SQL code assistance: This experience enhances productivity for SQL developers by providing intelligent code suggestions and solutions.

### Natural language to SQL query

Copilot for Azure SQL connects directly to the database schema and metadata to ...

This integration means that Copilot for Azure SQL can answer questions with prompts like ...

### Azure Copilot integration

You can ask and receive helpful, context-rich suggestions from [Microsoft Copilot for Azure](/azure/copilot/overview) within the Azure portal.

### SQL code assistance

Copilot can assist with analyzing, designing, and optimizing database schemas, and then author semantically correct and performant T-SQL queries.

## Key capabilities and use cases

Copilot in Azure SQL provides capabilities in three key areas: code completion and suggestions, querying and data analysis, and database administration.

| Capability | Features |
| :-- |:-- |
| **Code completion and suggestions** | - Autocomplete-style suggestions as you code.<br/> - Generate SQL queries based on comments.<br/> - Explain complex code.<br/> - Fix errors.<br/> - Detect and suggest fixes for anti-patterns in the code.|
| **Querying and data analysis** | - Generate queries using natural language.<br/> - Use a conversational interface to explore your schema and data.|
| **Database administration** | - Answer questions about your databases.<br/> - Author database management commands.<br/> - Surface insights, identify issues, summarize issues, and suggest solutions.<br/> - Suggest best practices for data security and privacy.<br/> - Surface updated documentation based on the customer's inquiries and context. |

Copilot in Azure SQL will integrate data and formulate applicable responses using public documentation, dynamic management views, catalog views, and Azure Support Center supportability diagnostics.

## Related content

- [Frequently asked questions about Copilot in Microsoft Azure SQL (preview)](copilot-azure-sql-faq.yml)
- [What is Microsoft Copilot for Azure?](/azure/copilot/overview)
- [Responsible AI FAQ for Microsoft Copilot for Azure (preview)](/azure/copilot/responsible-ai-faq)
