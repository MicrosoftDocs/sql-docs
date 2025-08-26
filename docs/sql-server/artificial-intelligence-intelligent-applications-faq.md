---
title: Intelligent Applications and AI Frequently Asked Questions (FAQ)
description: "Answers to common questions about using AI with the SQL Database Engine."
author: yorek
ms.author: damauri
ms.reviewer: damauri, randolphwest, mathoma
ms.date: 08/20/2025
ms.update-cycle: 180-days
ms.service: sql
ms.topic: faq
ms.collection:
  - ce-skilling-ai-copilot
ms.custom:
  - intro-quickstart
helpviewer_keywords:
  - "NL2SQL"
  - "Natural Language"
  - "Intelligent Applications"
  - "AI"
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric"
---

# Intelligent applications and AI Frequently Asked Questions (FAQ)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

> [!div class="op_single_selector"]
>
> - [Azure SQL Database](/azure/azure-sql/database/ai-artificial-intelligence-intelligent-applications)
> - [Intelligent applications and AI](artificial-intelligence-intelligent-applications.md)

This article contains frequently asked questions about vectors and embeddings in the SQL Database Engine.

For samples and examples, visit the [SQL AI Samples repository](https://aka.ms/sqlaisamples).

## Can I create a retrieval-augmented generation (RAG) solution completely in T-SQL?

Yes, you can create a Retrieval-Augmented Generation (RAG) solution using T-SQL. This type of solution leverages the SQL Database Engine's capabilities to manage and query your data effectively. You can use T-SQL to implement the necessary data retrieval and processing logic, while also integrating with external AI services for the generation aspect. Vectors can be stored natively in SQL engine and connections to LLMs to provide natural language understanding capabilities are possible via `sp_invoke_external_rest_endpoint`.

- [Implement a RAG solution and call OpenAI right from Azure SQL DB to ask questions about your data](https://github.com/Azure-Samples/azure-sql-db-chatbot)
- [Predictable LLM results with Structured Output and sp_invoke_external_rest_endpoint](https://devblogs.microsoft.com/azure-sql/predictable-llm-output-with-sp_invoke_external_rest_endpoint/)

## Why would I create a RAG solution completely in T-SQL?

If you want to improve an existing application without having to re-architect it to support AI capabilities, use the SQL engine built-in features to implement AI functionalities directly within your database queries. You only need to update your T-SQL code to incorporate AI features, rather than making extensive changes to your application architecture.

- [Migrate and modernize Windows Server, SQL Server, and .NET workloads](https://www.youtube.com/watch?v=H_2OgOL3fpo&t=982s)
- [Modernize applications with Azure SQL, OpenAI, and Data API builder](https://github.com/Azure-Samples/azure-sql-modernize-app-with-ai)

## Are there any end-to-end samples using Azure SQL or Fabric SQL for RAG?

Sure, you can find end-to-end samples for RAG using Azure SQL and Fabric SQL here:

- [RAG samples using Azure SQL](https://ai.awesome.azuresql.dev/?q=Azure%20SQL%20samples%20tagged%20with%20%22RAG%22)
- [RAG samples using Fabric SQL](https://ai.awesome.azuresql.dev/?q=Fabric%20SQL%20samples%20tagged%20with%20%22RAG%22)

## Can I have RAG working on structured data, like columns and rows?

If you need to work with structured data, you can still leverage RAG by combining it with other techniques, such as using embeddings to represent your structured data in a way that can be understood by the AI model. This allows you to perform retrieval and generation tasks on structured data while still benefiting from the capabilities of RAG.

- [Improve the “R” in RAG and embrace Agentic RAG in Azure SQL](https://devblogs.microsoft.com/azure-sql/improve-the-r-in-rag-and-embrace-agentic-rag-in-azure-sql/)

## Why does sending a full, complex schema to an LLM lead to poor SQL generation—and how can I fix it?

If you have a complex and large database schema, with hundreds of tables and views, it's better to use a multi-agent approach to help to reduce the noise and allow AI models to focus on specific areas of the schema. A full description along with a working end-to-end sample is available here:

- [A story of collaborating agents: chatting with your database the right way](https://devblogs.microsoft.com/azure-sql/a-story-of-collaborating-agents-chatting-with-your-database-the-right-way/)

## Is my data used by Microsoft for training models?

No. Data isn't used by Microsoft for training models. See the [Responsible AI documentation](/azure/ai-foundry/responsible-ai/openai/data-privacy) for more information.

## What data does the Azure OpenAI Service process?

Refer to the [Data, privacy, and security for Azure OpenAI Service](/azure/ai-foundry/responsible-ai/openai/data-privacy) document for more information.

## How can I protect my data from unauthorized AI Agent access?

Azure SQL and SQL Server provide extensive support for fine-grained access security:

- [Get started with Database Engine permissions](../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md): Control access to database objects at a granular level using permissions.
- [Row-Level Security (RLS)](../relational-databases/security/row-level-security.md): Control access to rows in a table based on the characteristics of the user executing a query. You can see RLS in action in this [video](https://youtu.be/Uddhx8Bu2ZM?si=90_i05RjhQarN7Jk&t=1236).
- [Dynamic data masking](../relational-databases/security/dynamic-data-masking.md): Limit the exposure of sensitive data by masking it to non-privileged users.
- [Always Encrypted](../relational-databases/security/encryption/always-encrypted-database-engine.md): Protect sensitive data by encrypting it at rest and in transit, ensuring that only authorized users can access the unencrypted data.

It's also possible to audit any operation done on the database using the Audit feature in Azure SQL and SQL Server.

[SQL Server Audit (Database Engine)](../relational-databases/security/auditing/sql-server-audit-database-engine.md)

## Related content

- [Intelligent applications and AI](artificial-intelligence-intelligent-applications.md)
- [Vector and embeddings: Frequently asked questions (FAQ)](../relational-databases/vectors/vectors-faq.md)
- [SQL AI Samples and Examples](https://aka.ms/sqlaisamples)
