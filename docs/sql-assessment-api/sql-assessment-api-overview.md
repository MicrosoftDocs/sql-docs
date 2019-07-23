---
title: SQL Server Assessment APIs
description: Describing what is the SQL Server Assessment APIs.
ms.prod: sql
ms.technology: tools-other
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: 07/26/2019 
---

# SQL Assessment API (Public Preview)

SQL Assessment API provides a mechanism to evaluate the configuration of your SQL Server for best practices. The API methods are used utilizing a SQL Server Management Object (SMO) extension and new cmdlets in SqlServer PowerShell module. API is delivered with a rule set that is highly customizable and extensible.
Currently, it can be used to assess SQL Server versions 2012 and higher. Both SQL on Windows and SQL on Linux are supported but the current version is delivered with checks for SQL on Windows only.
SQL Assessment API is useful when you want to make sure your SQL Server configuration is in line with recommended best practices. After an initial assessment, configuration stability can be tracked by regularly scheduled assessments.


## Rules

Rules sometimes referred to as checks, are defined in JSON formatted files. Microsoft's shipped rule set is available in the [samples repository](https://aka.ms/sql-assessment-api). There is versioning of the rule set for baselining and ease of management. You can visit the [samples repository](https://aka.ms/sql-assessment-api) for more details.

## SQL Assessment cmdlets and SMO extension

SQL Assessment API is part of the [SQL Server Management Objects (SMO)](../relational-databases/server-management-objects-smo/installing-smo.md) July 2019 release version and higher and the [SQL Server PowerShell module](../powershell/download-sql-server-ps-module.md) July 2019 release version and higher.

* [Install SMO](../relational-databases/server-management-objects-smo/installing-smo.md)

* [Install SQL Server PowerShell module](../powershell/download-sql-server-ps-module.md)

SqlServer module is getting two new cmdlets to work with SQL Assessment:
* **Get-SqlAssessmentItem** – Provides a list of available assessment checks for a SQL Server object. 

* **Invoke-SqlAssessment** – Provides results of an assessment

SMO Framework is supplemented by the SQL Assessment extension that provides the following methods:
* **GetAssessmentItems** – Returns available checks for a particular SQL object (IEnumerable<…>)

* **GetAssessmentResults** – Synchronously evaluates assessment and returns results and errors if any (IEnumerable<…>)

* **GetAssessmentResultsList** – Asynchronously evaluates assessment and returns results and errors if any (Task<…>)

## Get started using SQL Assessment cmdlets
An assessment is performed against a chosen SQL Server object. Currently, we have checks for two kinds of objects: database engine and database. If you want to assess an instance and all its databases, you should run the SQL Assessment cmdlets for each object separately. Alternatively, you can pass a list of objects to the SQL Assessment cmdlets (in a variable or the pipeline).

Go through the steps below to get started.

1. Get a list of available checks for the local instance to familiarize yourself with the checks. In this example, we're using a path implemented with the [Windows PowerShell SQL Server provider](https://docs.microsoft.com/en-us/sql/powershell/sql-server-powershell-provider) to pass the instance object to the Get-SqlAssessmentItem cmdlet.

    ```PowerShell
    CD SQLSERVER:\SQL\localhost\default
    Get-SqlAssessmentItem
    ```
2. Get a list of available checks for all databases of the instance. Here, we're putting output of the Get-SqlDatabase cmdlet into a variable so further get Get-SqlAssessmentItem results for each database.
    
    ```powershell
    $database = Get-SqlDatabase -ServerInstance 'localhost'
    Get-SqlAssessmentItem $database
    ```

3. Invoke assessment for the instance and save the results to a SQL table. In this example, we're piping output of the Get-SqlInstance cmdlet to the Invoke-SqlAssessment cmdlet, which results are piped to the Write-SqlTableData cmdlet.

    ```PowerShell
    Get-SqlInstance -ServerInstance 'localhost' |
    Invoke-SqlAssessment | 
    Write-SqlTableData -ServerInstance 'localhost' -DatabaseName SQLAssessmentDemo -SchemaName Assessment -TableName Results -Force
    ```
4. Invoke assessment for the databases and save the results to the same table. In this example, we're using the Get-SqlDatabase cmdlet to pass all databases to the Invoke-SqlAssessment cmdlet.

    ```PowerShell
    Get-SqlDatabase -ServerInstance 'localhost' |
    Invoke-SqlAssessment |
    Write-SqlTableData -ServerInstance 'localhost' -DatabaseName SQLAssessmentDemo -SchemaName Assessment -TableName Results -Force
    ```

3. Follow descriptions and links in the table to further understand the recommendations.

4. Customize the rules based on your environment and organizational requirements (see below).

5. Schedule a task or a job to run the assessment regularly or on-demand to measure progress

## Customizing rules

Rules are designed to be customizable and extensible. Microsoft's ruleset is designed to work for most environments. However, it is impossible to have one rule set that works for every single environment. Users can write their own JSON files and customize existing rules or add new ones. Examples of customization and complete Microsoft released rule set are available in the [samples repository](https://aka.ms/sql-assessment-api). For more details on how to run the SQL Assessment cmdlets with custom JSON files, use the Get-Help cmdlet.

### Options available with rule customization feature

#### Enabling/disabling certain rules or groups of rules (using tags)
You can silence specific rules when they should not be applied to your environment or until scheduled work is done to rectify the issue.

#### Changing threshold parameters
Certain rules have thresholds that are compared against the current value of a metric to find out an issue. If the default thresholds do not fit, you can change them.

#### Adding more rules written by you or third parties
You can "daisy chain" rulesets by adding one or more JSON files as parameters to your SQL Assessment API call. Your organization might write those files or obtain them from a third party. For example, you can have your JSON file that disables specific rules from the Microsoft rule set, and another JSON file by an industry expert that include rules you find useful for your environment, followed by another JSON file that changes some threshold values in that JSON file.

## Next steps 

Take a look at [SQL Server Management Objects (SMO)](../relational-databases/server-management-objects-smo/overview-smo.md) and [PowerShell](../powershell/download-sql-server-ps-module.md).
