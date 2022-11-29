---
title: SQL Server Assessment API
description: Learn about the SQL Assessment API that provides a mechanism to evaluate the configuration of your SQL Server for best practices.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: 12/27/2021
---

# SQL Assessment API

[!INCLUDE [SQL Server 2012, ASMI, SQL Server on Azure VM, SQL on Linux](../../includes/applies-to-version/sql-asmi-sqlavm-sql-linux.md)]

The SQL Assessment API provides a mechanism to evaluate the configuration of your SQL Server for best practices. The API is delivered with a ruleset containing best practice rules suggested by SQL Server Team. This ruleset is enhanced with the release of new versions, but at the same time, the API is built with the intent to give a highly customizable and extensible solution. So, users can tune the default rules and create their own.

The SQL Assessment API is useful when you want to make sure your SQL Server configuration is in line with recommended best practices. After an initial assessment, configuration stability can be tracked by regularly scheduled assessments.

The API can be used to assess:
 
* SQL Server on Azure Virtual Machines

* Azure SQL Database Managed Instance

* SQL Server 2012 and higher

* SQL on Linux-based systems

The API is also used by SQL Server Assessment Extension for Azure Data Studio (ADS).

>[!NOTE]
>The SQL Assessment API provides assessment on a variety of areas, but it does not go deeply into security. We recommend you use [SQL Vulnerability Assessment](../../relational-databases/security/sql-vulnerability-assessment.md) to proactively improve your database security.

## Rules

Rules (sometimes referred to as checks) are defined in JSON formatted files. The ruleset format requires a ruleset name and version to be specified. When you use custom rulesets, you can easily know which recommendations from what ruleset come.

The Microsoft's shipped ruleset is available on GitHub. You can view the [entire ruleset](https://github.com/microsoft/sql-server-samples/blob/567d49a42d4cf10e4942b19290ab80828b451b77/samples/manage/sql-assessment-api/DefaultRuleset.csv) in the [samples repository](https://aka.ms/sql-assessment-api).

## SQL Assessment cmdlets and associated extensions

### Use the API directly

The SQL Assessment API is available and can be used through managed code as part of any of these components :

* [Azure Data Studio (ADS)](../../azure-data-studio/what-is-azure-data-studio.md)

    Release version as of June 2020 and higher.

* [SQL Server Management Objects (SMO)](../../relational-databases/server-management-objects-smo/installing-smo.md)

    Release version as of July 2019 and higher.

* [SQL Server PowerShell module](../../powershell/download-sql-server-ps-module.md)

    Release version as of July 2019 and higher.

Before you start using the SQL Assessment API itself, make sure to install either of these:

* [Install ADS](https://techcommunity.microsoft.com/t5/sql-server/released-sql-server-assessment-extension-for-azure-data-studio/ba-p/1470603)

* [Install SMO](../../relational-databases/server-management-objects-smo/installing-smo.md)

* [Install SQL Server PowerShell module](../../powershell/download-sql-server-ps-module.md)

* [Install via Nuget](https://www.nuget.org/packages/Microsoft.SqlServer.Assessment/)


The SMO Framework is supplemented by the SQL Assessment API extension that provides the following methods:

* **GetAssessmentItems** – Returns available checks for a particular SQL object (IEnumerable<…>)

* **GetAssessmentResults** – Synchronously evaluates assessment and returns results and errors if any (IEnumerable<…>)

* **GetAssessmentResultsList** – Asynchronously evaluates assessment and returns results and errors if any (Task<…>)

### Use the API via PowerShell

If you would like to invoke the SQL Assessment API via PowerShell, you must [install SQL Server PowerShell module](../../powershell/download-sql-server-ps-module.md)

The SqlServer module is getting two new cmdlets to work with SQL Assessment API:

* **Get-SqlAssessmentItem** – Provides a list of available assessment checks for a SQL Server object

* **Invoke-SqlAssessment** – Provides results of an assessment



## Get started using SQL Assessment cmdlets

An assessment is performed against a chosen SQL Server object. In the default ruleset, there are checks for two kinds of objects only: Server and Database (in addition to them, the API supports two more kinds: Filegroup and AvailabilityGroup). If you want to assess a SQL instance and all its databases, you should run the SQL Assessment cmdlets for each object separately. Or you can pass objects for assessment to the SQL Assessment cmdlets in a variable or the pipeline.

SqlServer and RegisteredServer objects are interchangeable, so you can pass any to the SQL Assessment cmdlets.

Go through the examples below to get started.

1. Get a list of available checks for a local default instance to familiarize yourself with the checks. In this example, we're piping the output of the Get-SqlInstance cmdlet to the Get-SqlAssessmentItem cmdlet to pass the instance object to it.

    ```powershell
    Get-SqlInstance -ServerInstance 'localhost' | Get-SqlAssessmentItem
    ```

2. Get a list of available checks for all databases of the instance. Here, we're using the Get-Item cmdlet and a path implemented with the Windows PowerShell SQL Server provider to get a list of the databases, and then piping it to the Get-SqlDatabase cmdlet.

    ```powershell
    Get-Item SQLSERVER:\SQL\localhost\default | Get-SqlAssessmentItem
    ```

    Also, you can use the Get-SqlDatabase cmdlet to do the same.

    ```powershell
    Get-SqlDatabase -ServerInstance 'localhost' | Get-SqlAssessmentItem
    ```

3. Invoke assessment for the instance and save the results to a SQL table. In this example, we're piping the output of the Get-SqlInstance cmdlet to the Invoke-SqlAssessment cmdlet, which results are piped to the Write-SqlTableData cmdlet. The Invoke-Assessment cmdlet is run with the `-FlattenOutput` parameter in this example. This parameter makes the output suitable for the Write-SqlTableData cmdlet. The latter raises an error if you omit the parameter.

    ```powershell
    Get-SqlInstance -ServerInstance 'localhost' |
    Invoke-SqlAssessment -FlattenOutput |
    Write-SqlTableData -ServerInstance 'localhost' -DatabaseName SQLAssessmentDemo -SchemaName Assessment -TableName Results -Force
    ```

    Now let's invoke an assessment for all databases of the instance and add the results to the same table.

    ```powershell
    Get-SqlDatabase -ServerInstance 'localhost' |
    Invoke-SqlAssessment -FlattenOutput |
    Write-SqlTableData -ServerInstance 'localhost' -DatabaseName SQLAssessmentDemo -SchemaName Assessment -TableName Results -Force
    ```

4. Follow descriptions and links in the table to further understand the recommendations.

5. Customize the rules based on your environment and organizational requirements (see below).

6. Schedule a task or a job to run the assessment regularly or on-demand to measure progress.

## Customizing rules

Rules are designed to be customizable and extensible. Microsoft's ruleset is designed to work for most environments. However, it's impossible to have one ruleset that works for every single environment. Users can write their own JSON files and customize existing rules or add new ones. Examples of customization and complete Microsoft released ruleset are available in the [samples repository](https://aka.ms/sql-assessment-api). For more details on how to run the SQL Assessment cmdlets with custom JSON files, use the Get-Help cmdlet.

### Options available with rule customization feature

#### Enabling/disabling certain rules or groups of rules (using tags)

You can silence specific rules when they aren't applied to your environment or until scheduled work is done to rectify the issue.

#### Changing threshold parameters

Specific rules have thresholds that are compared against the current value of a metric to find out an issue. If the default thresholds don't fit, you can change them.

#### Adding more rules written by you or third parties

You can string together rulesets by adding one or more JSON files as parameters to your SQL Assessment API call. Your organization might write those files or obtain them from a third party. For example, you can have your JSON file that disables specific rules from the Microsoft ruleset, and another JSON file by an industry expert that include rules you find useful for your environment, followed by another JSON file that changes some threshold values in that JSON file.

>[!IMPORTANT]
>We urge you not to use rulesets that come from untrusted sources until you thoroughly review them to make sure they are safe.

## Next steps

* [SQL Server Management Objects (SMO)](../../relational-databases/server-management-objects-smo/overview-smo.md)
* [PowerShell](../../powershell/download-sql-server-ps-module.md)
* [SQL Vulnerability Assessment](../../relational-databases/security/sql-vulnerability-assessment.md)
