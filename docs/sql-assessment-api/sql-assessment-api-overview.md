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
ms.date: 07/24/2019
---

# SQL Assessment APIs (Public Preview)

SQL Assessment API provides a mechanism to evaluate the configuration of your SQL Server for best practices. The API methods are used utilizing a SQL Server Management Object (SMO) extension and new cmdlets in SqlServer PowerShell module. API is delivered with a rule set that is highly customizable and extensible.
Currently, it can be used to assess SQL Server versions 2012 and higher, both on Windows and Linux.
SQL Assessment API is useful when you want to make sure your SQL Server configuration is in line with recommended best practices. After an initial assessment, configuration stability can be tracked by regularly scheduled assessments.

## Rules 

Rules sometimes referred to as checks, are defined in JSON formatted files. Microsoft's shipped rule set is available in [GitHub](htttp://aka.ms/sql-assessment-api). There is versioning of the rule set for baselining and ease of management. Visit [GitHub](htttp://aka.ms/sql-assessment-api) for more details.

## Customizing rules

Rules are designed to be customizable and extensible. Microsoft's ruleset is designed to work for most environments. However, it is impossible to have one rule set that works for every single environment. Users can write their own JSON files and customize existing rules or add new ones. Examples of customization are available in [GitHub](htttp://aka.ms/sql-assessment-api).

### Customizing features enables users to do the  options below.

* Enable/disable individual or groups of rules (using tags)
    * Certain rules might not apply to your environment. So, you can try to silence specific rules until scheduled work is done to rectify the issue

* Change threshold parameters

* Add more rules written by you or third parties
    * You can "daisy chain" rule sets by adding one or more JSON files as parameters to your SQL Assessment api call. Your organization might write those files or obtain them from a third party. For example, you can have your JSON file that disables specific rules from the Microsoft rule set, and another JSON file by an industry expert that include rules you find useful for your environment, followed by another JSON file that changes some threshold values in that JSON file.

## Quick Start

SQL Assessment API is part of SQL Server Management Objects (SMO) version <X> and higher and SqlServer PowerShell module version <Y> and higher.

* [Install SMO](../relational-databases/server-management-objects-smo/installing-smo.md)

* [Install SQL Server PowerShell module](../powershell/download-sql-server-ps-module.md)

You can find the below information in [GitHub](htttp://aka.ms/sql-assessment-api):

* Example scripts
* Complete Microsoft released rule set
* Examples of rule customization

An assessment is performed against a chosen SQL Server object. Currently, we check for two kinds of objects: database engine and database. If you want to assess an instance and all its databases, you should run the SQL Assessment cmdlets for each object separately. Alternatively, you can pass a list of objects to the SQL Assessment cmdlets (in a variable or the pipeline).

* Variable

    ```powershell
    $database = Get-SqlDatabase -ServerInstance 'localhost'
    Invoke-SqlAssessment $database -Verbose
    ```

* Pipeline

    ```powershell
    Get-SqlDatabase -ServerInstance 'localhost' | Invoke-SqlAssessment -Verbose
    ```

1. Get a list of available checks for the local instance to familiarize yourself with the checks

    ```PowerShell
    Get-SqlAssessmentItem SQLSERVER:\SQL\localhost\default
    ```

2. Invoke assessment and pipe results to a table

    ```PowerShell
    Get-SqlInstance -ServerInstance 'localhost' | Invoke-SqlAssessment |
    Write-SqlTableData -ServerInstance 'localhost' -DatabaseName SQLAssessmentDemo -SchemaName Assessment -TableName Results -Force
    ```

3. Follow descriptions and links in the table to further understand the recommendations

4. Customize the rules based on your environment and organizational requirements.
    a. Enable/disable certain rules or groups of rules (using tags).
    b. Change threshold parameters.
    c. Add more rules written by you or third parties.

5. Schedule a task or a job to run the assessment regularly or on-demand to measure progress

6. Once you have the assessment results in a table, you can use it in many different ways, including but not restricted to charting, or uploading results to Azure Log Analytics for further analysis, and so forth.
