---
title: Copilot in Azure with Azure SQL Database List of Sample Prompts
description: "Explore a list of sample prompts to use with Microsoft Copilot in Azure with Azure SQL Database."
author: bluefooted
ms.author: pamela
ms.date: 04/08/2025
ms.service: azure-sql-database
ms.subservice: sql-ai-copilot
ms.topic: concept-article
ms.collection:
  - ce-skilling-ai-copilot
monikerRange: "=azuresql || =azuresql-db"
---

# Copilot sample prompts to use with Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Here's a list of sample prompts that you can use with Microsoft Copilot in Azure SQL Database. These prompts are designed to help you interact with your database and get relevant information quickly.

| Skill Name | Skill Description | Example prompt |
| --- | --- | --- |
| Active User Connections | Shows active user connections to the database. | `Who is currently actively connected to the database?` |
| Antipattern Query Analysis | Identifies queries with anti-patterns and their potential effect on performance. | `Show me all the queries in my workload that have anti-patterns in them.` |
| Automatic Tuning Analysis | Investigates automatic tuning failures and potential solutions. | `Why is automatic plan correction failing?` |
| Basic Database Information | Retrieves basic information about the database. | `What is the name of the logical server for this database?` |
| Blocking Session Analysis | Identifies and analyzes blocking sessions. | `Check top blocking sessions.` |
| Compatibility Level | Provides information about the database compatibility level. | `What's the compatibility level of this database?` |
| Connection String Generation | Generates the appropriate connection string for the database. | `Which connection string should I use to connect to my DB?` |
| Copilot Help | Provides general assistance and guidance with Azure SQL. | `What can you do related to Azure SQL?` |
| Data Synchronization Analysis | Troubleshoots data synchronization issues, particularly with secondaries. | `Why do my secondaries not have the latest data?` |
| Database and Table Size | Provides information about the database and table sizes. | `What's the size of this database?` |
| Database Performance Analysis | Analyzes overall database performance and suggests improvements. | `Why is my database slow?` |
| Database Permission Listing | Lists database permissions and access levels for users. | `Which users have access to `master` database?` |
| Deadlock Analysis | Investigates deadlocks and suggests solutions. | `Why am I getting deadlock errors? How can I fix it?` |
| Dropped Connections Analysis | Investigates instances of dropped database connections. | `Show me all the instances where my database had a dropped connection.` |
| Fragmented Index Analysis | Identifies fragmented indexes and their effect on performance. | `Help me find fragmented indexes.` |
| General Antipattern Information | Provides general information about common SQL anti-patterns. | `What are the most common SQL antipatterns?` |
| Get Database Names for Server | Lists all databases on a specific server. | `List all the databases on this server.` |
| High CPU Consuming Query Analysis | Identifies and analyzes queries with high CPU usage. | `Why is the CPU usage high on this database?` |
| High IO troubleshooting | Checks if the database is experiencing high I/O. | `Is my database experiencing high I/O?` |
| Index Listing | Shows all indexes in the database. | `What are all the indexes?` |
| Index Recommendations for Specific Table | Provides index recommendations for one or more tables. | `Should I add an index on this table?` |
| Latest Backup Information | Provides information about the most recent database backup. | `When was the most recent backup of my database created?` |
| Low Storage Space Troubleshooting | Provides suggestions to free up space in the database. | `Is there a way I can free up space in my database?` |
| MAXDOP Optimization | Analyzes and suggests optimizations for the MAXDOP setting. | `What's the current MAXDOP and how to optimize?` |
| Memory Grant Analysis | Analyzes memory grant issues and potential causes. | `Why am I having memory grant issues?` |
| Missing Index Suggestions | Suggests missing indexes to improve query performance. | `Missing index suggestion for improving query performance?` |
| Point-in-Time Restore Retention | Provides information about the point-in-time restore retention period. | `How far back in time can I go for a point-in-time restore?` |
| Query Performance Analysis | Investigates and suggests solutions for slow-running queries. | `Why is this query running so slow?` |
| Query Store - Find Forced Plans | Shows queries with forced plans within a specified timeframe. | `Show me all the queries from the past 2 days that have forced plans.` |
| Query Store - Find High Execution Time Variation | Identifies queries with high variation in execution time. | `Which queries on my database have a high variation in execution time?` |
| Query Store - Find Highest I/O Queries | Shows queries with the highest I/O usage. | `What queries on this database use the most I/O?` |
| Query Store - Get query text by ID | Shows the query text based on the provided Query ID | `What is the query text for Query ID 1333?` |
| Query Store - Latest Executed Queries | Displays the most recently executed queries. | `What are the most recently executed queries in my database?` |
| Query Store - Longest Running Queries | Shows the longest running queries within a specified timeframe. | `What are the longest running queries in the past day?` |
| Query Store - Queries with Highest Wait Times | Identifies queries with the highest wait times. | `Which queries have had the highest wait times?` |
| Query Store - Queries with Multiple Plans | Checks for queries with multiple execution plans. | `Show me the queries that have had more than one execution plan.` |
| Query Store - Regressed Queries | Identifies queries that have regressed in performance. | `Have any of my queries gotten significantly slower recently?` |
| Query Store - Regressed Queries with Plan Changes | Shows queries with plan changes that have regressed in performance. | `Are there any queries that had plan changes and regressed in performance?` |
| Query Store - Show Executions per Query | Displays the number of executions for each query. | `What queries are being executed most often?` |
| Query Store - Top Resource Consuming Queries | Identifies and analyzes queries with the highest resource consumption. | `What are the most expensive queries in my workload?` |
| Query Store Mode Troubleshooting | Investigates and provides solutions for Query Store being in read-only mode. | `Why is Query Store in read-only mode? How can I fix it?` |
| Related Documentation | Provides links to relevant documentation based on the user's query. | `What does database compatibility level mean?` |
| Resource Usage Analysis | Analyzes resource usage and potential bottlenecks. | `Is the database hitting resource limits? Which limits?` |
| Table Listing | Lists all tables in the database. | `What are the names of all the tables?` |
| Troubleshoot error 18456 | Helps with SQL error 18456. | `Help me with SQL error 18456` |
| Troubleshoot error 40615 | Helps with SQL error 40615. | `Help me with SQL error 40615` |
| Troubleshoot Connection Errors | Checks for causes of connection timeouts. | `Check for causes of connection timeouts in my database` |
| Troubleshoot sign in Failures | Diagnoses sign in failure issues. | `Why can't I login to my database?` |
| Troubleshoot Query timeout | Addresses query timeout issues. | `Help me with query timeout issues` |
| Wait Statistics Analysis | Analyzes wait statistics and potential performance bottlenecks. | `What do the wait statistics look like for my database?` |
| Workloads Increase and Scaling Analysis | Assesses workload increases and potential need for scaling. | `Has increased workload or traffic caused performance issues?` |

> [!NOTE]
> AI powers Copilot, so surprises and mistakes are possible.

## Related content

- [Microsoft Copilot in Azure with Azure SQL Database](copilot-azure-sql-overview.md)
- [Microsoft Copilot in Azure](/azure/copilot/overview)
- [Frequently asked questions](copilot-azure-sql-faq.yml)
