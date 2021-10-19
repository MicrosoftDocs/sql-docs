---
title: Notebooks with Azure Monitor Logs in Azure Data Studio
description: This tutorial shows how you can create and run a notebook with Azure Monitor Logs.
ms.topic: how-to
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: markingmyname
ms.author: maghan
ms.reviewer: alayu
ms.custom: 
ms.date: 10/19/2021
---

# Create and run a notebook with Azure Monitor Logs

Once Azure Monitor Logs (Preview) extension is installed in Azure Data Studio Insiders build, you can connect to your Azure Monitor Log workspace(s), browse the tables, write/execute KQL queries against workspaces and write/execute Notebooks connected to the Azure Monitor Log kernel.

There are two main tables in Azure Log Analytics (Azure Monitor Logs) workspace that capture Azure SQL events:

1. AzureDiagnostics
2. AzureMetric

## 1. Connect to an Azure Monitor Logs (Log Analytics) workspace

Workspace is similar to what a database is to SQL. You connect to the Log Analytics workspace to start querying data.

### 1.1 Install Azure Monitor Logs extension first

Go to the Extension viewlet and type in Azure Monitor Logs. Install it and restart Azure Data Studio.

### 1.2 Connect to the desired Azure Monitor Logs workspace

Change the Kernel to "Log Analytics." Set Attach to a new or existing connection to the workspace. Note: you will need a workspace ID that you can obtain from the Azure portal.

> [!Note]
> The name of the kernel is subject to change.

## 2. Analyze events by Diagnostic Settings

Let's do a simple query first to analyze the number of events by Operation Name.

> [!Note]
> Each row in AzureDiagnostic represents an event for a specific Operation or category. Some SQL actions may result in generating multiple events of different types.

AzureDiagnostics
| summarize count() by OperationName

The above query's equivalent in SQL is:

```sql
SELECT OperationName, COUNT(*) AS [count_]
FROM AzureDiagnostics
GROUP BY OperationName
```

Count my Azure SQL DB events by category / diagnostic settings.

AzureDiagnostics
| where LogicalServerName_s == "<databasename>"
// | where TimeGenerated >= ago(5d)
| summarize count() by Category

## 3. Performance troubleshooting Query (from Azure portal)

Potentially a query or deadlock on the system that could lead to poor performance. The following is a query suggested by Azure portal.

AzureMetrics
| where ResourceProvider == "MICROSOFT.SQL"
| where TimeGenerated >=ago(12d)
| where MetricName in ('deadlock')
| parse _ResourceId with * "/microsoft.sql/servers/" Resource // subtract Resource name for _ResourceId
| summarize Deadlock_max_60Mins = max(Maximum) by Resource, MetricName

# Azure Metrics

This is a sample query to dig into AzureMetrics.

`AzureMetrics`

# Azure Diagnostics

This is a sample query to dig into AzureDiagnostics. This table tends to have more details than AzureMetrics.

AzureMetrics
| project-away TenantId, ResourceId, SubscriptionId, _ResourceId, ResourceGroup // hide sensitive info
| project TimeGenerated, MetricName, Total, Count, UnitName
| take 10

AzureDiagnostics
| project-away TenantId, ResourceId, SubscriptionId, ResourceGroup, _ResourceId // Hide sensitive columns :) 
| project TimeGenerated, Category, OperationName
| take 10

## Analyze (non-audit) Events

AzureDiagnostics
| summarize event_count = count() by bin(TimeGenerated, 2d), OperationName
| where OperationName <> "AuditEvent"
| evaluate pivot(OperationName, sum(event_count))
| sort by TimeGenerated asc

AzureDiagnostics
| summarize event_count=count() by bin(TimeGenerated, 2d), OperationName
// | where OperationName <> "AuditEvent"

AzureDiagnostics
| make-series event_count = count() on TimeGenerated from datetime(2021-07-20 22:00:00) to now() step 1m   
| render timechart
## Deadlock Analysis

AzureDiagnostics
| where OperationName == "DeadlockEvent"
| project TimeGenerated, Category, Resource, OperationName, Type, deadlock_xml_s
| sort by TimeGenerated desc
| take 50

Find the deadlock query plan.

AzureDiagnostics
| where OperationName == "DeadlockEvent"
| extend d = parse_xml(deadlock_xml_s)
| project TimeGenerated, QueryPlanHash = d.deadlock.["process-list"].process[0].executionStack.frame[0]["@queryplanhash"], QueryHash = d.deadlock.["process-list"].process[0].executionStack.frame[0]["@queryhash"]
| take 50

## Query Store Runtime Statistics Events

AzureDiagnostics
| where OperationName == "QueryStoreRuntimeStatisticsEvent"
| project TimeGenerated, query_hash_s, statement_sql_handle_s, query_plan_hash_s
| take 10

## Analyze Errors

AzureDiagnostics
| where OperationName == "ErrorEvent"
| extend ErrorNumber =  tostring(error_number_d)
| summarize event_count=count() by EventTime = bin(TimeGenerated, 2d),  ErrorNumber
| evaluate pivot(ErrorNumber, sum(event_count))
| sort by EventTime asc

## Find Deleted table

AzureDiagnostics
| where action_name_s in ('BATCH COMPLETED')
| project TimeGenerated, Category, action_name_s, statement_s
| where statement_s contains "DROP TABLE"
| sort by TimeGenerated desc
| take 10