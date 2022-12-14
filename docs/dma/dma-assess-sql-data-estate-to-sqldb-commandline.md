---
title: "DMACMD: Assess SQL Server readiness to migrate to Azure SQL"
titleSuffix: Data Migration Assistant
description: "Learn how to use Data Migration Assistant command line tool (DMACMD) to assess a SQL Server data estate for migration to Azure SQL"
author: rajeshsetlem
ms.author: rajpo
ms.date: "10/02/2020"
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
helpviewer_keywords:
  - "Data Migration Assistant, on-premises SQL Server"
---
# DMACMD: Assess readiness of a SQL Server data estate migrating to Azure SQL 

With many organizations trying to migrate to Azure, it is critical to assess existing on-premises SQL Server instances and identify the right Azure SQL target - Azure SQL Database, Azure SQL Managed Instance, or SQL Server on Azure VMs. 

[Data Migration Assistant (DMA)](dma-overview.md) helps assess a SQL Server instance for a specific Azure SQL target, and gauges the readiness of SQL Server databases migrating to Azure SQL. Upload DMA assessment results to Azure Migrate hub for a centralized readiness view of the entire data estate. 

This article teaches you to perform assessments at scale and upload the results to Azure Migrate hub using the DMA command-line interface (DMACMD). Alternatively, you can use the [DMA GUI](dma-assess-sql-data-estate-to-sqldb.md) to perform the assessment instead.

To learn more, see the following Channel9 video:

>
> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/How-to-Assess-Readiness-of-SQL-Server-Data-Estate-Migrating-to-Azure-SQL/player?WT.mc_id=dataexposed-c9-niner]

## Prerequisites 

To use DMACMD to perform an assessment and upload the results to Azure Migrate hub, you need the following: 

- The [latest version of Data Migration Assistant (DMA)](https://www.microsoft.com/en-us/download/details.aspx?id=53595).
- An [Azure Migrate project](dma-assess-sql-data-estate-to-sqldb.md#create-a-project-and-add-a-tool). 
- Contributor role access to the Azure Migrate project resource.

## Use DMACMD

Use an XML file as input to run assessments at scale using the command-line interface (DMACMD.exe). 

Use the following sample command to pass an XML file to DMACMD and start the assessment:

```console
C:\Program Files\Microsoft Data Migration Assistant\DmaCmd.exe /Action=Assess /AssessmentConfiguration= C:\Demo\ScaleAssessment\Assess-for-AzureSQLMI.xml
```

The contents of sample `Assess-for-AzureSQLMI.xml` define the elements to assess SQL Server instances for a SQL Managed Instance target: 

```xml
<?xml version="1.0" encoding="UTF-8"?>
<AssessmentConfiguration xmlns="http://microsoft.com/schemas/SqlServer/Advisor/AssessmentConfiguration">
   <AssessmentName>Scale-Assessment-for-AzureSQLManagedInstance</AssessmentName>
   <AssessmentSourcePlatform>SqlOnPrem</AssessmentSourcePlatform>
   <AssessmentTargetPlatform>ManagedSqlServer</AssessmentTargetPlatform>
   <AssessmentDatabases>
      <AssessmentDatabase>Server=ServerName\SQL2017;Integrated Security=true</AssessmentDatabase>
      <AssessmentDatabase>Server=ServerName\SQL2016;Integrated Security=true;Initial Catalog=AdventureWorks2016</AssessmentDatabase>
      <AssessmentDatabase>Server=ServerName\SQL2016;Integrated Security=true;Initial Catalog=TestDB</AssessmentDatabase>
   </AssessmentDatabases>
   <AssessmentResultDma>C:\Demo\ScaleAssessment\AssessmentConfiguration\Scale-Assessment-for-AzureSQLManagedInstance.dma</AssessmentResultDma>
   <AssessmentResultJson>C:\Demo\ScaleAssessment\AssessmentConfiguration\Scale-Assessment-for-AzureSQLManagedInstance.json</AssessmentResultJson>
   <AssessmentResultCsv>C:\Demo\ScaleAssessment\AssessmentConfiguration\Scale-Assessment-for-AzureSQLManagedInstance.csv</AssessmentResultCsv>
   <AssessmentOverwriteResult>true</AssessmentOverwriteResult>
   <AssessmentEvaluateCompatibilityIssues>true</AssessmentEvaluateCompatibilityIssues>
   <AssessmentEvaluateFeatureParity>true</AssessmentEvaluateFeatureParity>
   <AzureCloudEnvironment>Azure</AzureCloudEnvironment>
   <SubscriptionId>xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxx</SubscriptionId>
   <AzureMigrateProjectName>Scale-Assessment-for-AzureSQLMI</AzureMigrateProjectName>
   <ResourceGroupName>Resource-Group-Name</ResourceGroupName>
   <AzureAuthenticationInteractiveAuthentication>true</AzureAuthenticationInteractiveAuthentication>
   <AzureAuthenticationTenantId>xxxxxxxx-xxxx-xxxxxxxx</AzureAuthenticationTenantId>
   <EnableAssessmentUploadToAzureMigrate>true</EnableAssessmentUploadToAzureMigrate>
</AssessmentConfiguration>
```



## XML Elements 

The XML elements that are passed to DMACMD are defined in the following table: 


|**XML Element** |**Definition**  |
|---------|---------|
|`AssessmentName`|The name of the assessment|
|`AssessmentSourcePlatform`|Source SQL Server platform. The default value is `SqlOnPrem`.|
|`AssessmentTargetPlatform`|Target SQL Server platform.  </br> `AzureSqlDatabase` is for an Azure SQL Database target. </br> `ManagedSqlServer` is for an Azure SQL Managed Instance target. </br></br>The sample **Assess-for-AzureSQLMI** assess a SQL Managed Instance target.|
|`AssessmentDatabases`|If you need to assess all the databases in an instance, then specify just the instance name else list specific databases in each line. </br></br>The sample **Assess-for-AzureSQLMI** assess all databases in instance `Servername\SQL2017` and two specific databases in instance `Servername\SQL2016`.  |
|`AssessmentResultDma` </br> `AssessmentResultJson` </br> `AssessmentResultCsv` | Specifies the format of the result file. `.DMA`, `.JSON`, and `.CSV` respectively. Double-click `.DMA` to open in the DMA UI. <br> `AssessmentResultDma` is required to upload assessment results to Azure Migrate hub.  |
|`AssessmentOverwriteResult`| Indicates whether to overwrite any existing assessment result file with the same path as `AssessmentResultJson`, `AssessmentResultDma` or `AssessmentResultCsv`.|
|`AssessmentEvaluateCompatibilityIssues` </br> `AssessmentEvaluateFeatureParity` |Perform assessment to evaluate compatibility issues and feature parity issues respectively.|
|`AzureCloudEnvironment`|Azure cloud environment to connect to, default is Azure Public Cloud. </br></br> Supported values: </br>`Azure (default)`, `AzureChina`, `AzureGermany`, `AzureUSGovernment`.|
|`SubscriptionId`|Azure subscription ID.|
|`AzureMigrateProjectName`|Azure Migrate project name to upload assessment results to.|
|`ResourceGroupName`|Azure Migrate resource group name.|
|`AzureAuthenticationInteractiveAuthentication`|Set to `true` to pop up the authentication window.|
|`AzureAuthenticationTenantId`|Azure Active Directory tenant ID. </br></br>Obtain this from the **Overview** blade of Azure Active Directory in the [Azure portal](https://portal.azure.com). |
|`EnableAssessmentUploadToAzureMigrate`| Set to `true` to upload and publish assessment results to Azure Migrate hub.|


## Results 

DMACMD outputs a status when it finishes successfully. 


The following is a sample result output: 

```console
Assessment finished for project: Scale-Assessment-for-AzureSQLManagedInstance
DATABASES:
Succeeded             : 4
Failed                : 0
SERVER INSTANCES:
Succeeded             : 2
Failed                : 0
CSV result file       : C:\Demo\ScaleAssessment\Scale-Assessment-for-AzureSQLManagedInstance.csv
JSON result file      : C:\Demo\ScaleAssessment\Scale-Assessment-for-AzureSQLManagedInstance.json
--------------------------------------------------------------------------------
```

View uploaded results in [Azure Migrate](dma-assess-sql-data-estate-to-sqldb.md#view-target-readiness-assessment-results) for a centralized view of the entire data estate. . 

## Best practices 

Consider the following best practices when using DMACMD: 

- Logically group together target SQL Server instances and databases based on the application, rather than assessing all SQL Server instances in the entire data estate. 
- Create a separate Azure Migrate project for each Azure SQL target to avoid overwriting results. 
- The time to run an assessment depends on the number of database objects. If possible, avoid running assessments on production system and offload to a virtual machine or staging server instead, especially for databases with a large number of objects. 


## See also

* [Data Migration Assistant (DMA)](../dma/dma-overview.md)
* [Data Migration Assistant: Configuration settings](../dma/dma-configurationsettings.md)
* [Data Migration Assistant: Best Practices](../dma/dma-bestpractices.md)

