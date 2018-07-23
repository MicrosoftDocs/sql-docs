---
title: "Azure Data Lake Analytics Task | Microsoft Docs"
ms.custom: ""
ms.date: "05/18/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.component: "control-flow"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: conceptual
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPADLSTASK.F1"
  - "SQL14.DTS.DESIGNER.AFPADLSTASK.F1"
author: "yanancai"
ms.author: "yanacai"
ms.reviewer: "douglasl"
manager: craigg
---

# Azure Data Lake Analytics Task

The Azure Data Lake Analytics Task lets users submit U-SQL jobs to Azure Data Lake Analytics service. [Azure Data Lake Analytics (ADLA)](https://azure.microsoft.com/services/data-lake-analytics/).

The Azure Data Lake Analytics Task is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

## Configure the Azure Data Lake Analytics Task

To add an Azure Data Lake Analytics Task to a package, drag it from SSIS Toolbox to the designer canvas. Then double-click the task, or right-click the task and select **Edit**, to open the **Azure Data Lake Analytics Task Editor** dialog box. You can set properties through SSIS Designer or programmatically.

## General Page Configuration

Use the **General** page to configure the Azure Data Lake Analytics Task and provide the U-SQL script that the task submits. To learn more about the U-SQL language, see [U-SQL Language Reference](https://msdn.microsoft.com/azure/data-lake-analytics/u-sql/u-sql-language-reference).

### Basic Configuration

- **Name:** Specifies the name of the Azure Data Lake Analytics task.
- **Description:** Specifies the description of the Azure Data Lake Analytics task.

### U-SQL Configuration

U-SQL configuration has two settings: **SourceType** and dynamic options based on **SourceType** value. 

- **SourceType:** Specifies the source of the U-SQL script. The script will be submitted to an Azure Data Lake Analytics account during SSIS package execution. This property has 3 options listed in the following table.

|Value|Description|  
|-----------|-----------------|  
|**DirectInput**|Specifies the U-SQL script through the inline editor. Selecting this value displays the dynamic option, **USQLStatement**.|  
|**FileConnection**|Specifies a local .usql file that contains the U-SQL script. Selecting this option displays the dynamic option, **FileConnection**.|  
|**Variable**|Specifies an SSIS variable that contains the U-SQL script. Selecting this value displays the dynamic option, **SourceVariable**.|

- **SourceType Dynamic Options:** Specifies the script content for the U-SQL query. 

|SourceType|Dynamic Options|  
|-----------|-----------------|  
|**SourceType = DirectInput**|Type the U-SQL query to be submitted in the option box directly, or click the browse button (...) to type the U-SQL query in the **Enter U-SQL Query** dialog box.|  
|**SourceType = FileConnection**|Select an existing file connection manager, or click <**New connection...**> to create a new file connection. **Related Article:** [File Connection Manager](../../integration-services/connection-manager/file-connection-manager.md), [File Connection Manager Editor](../../integration-services/connection-manager/file-connection-manager-editor.md)|  
|**SourceType = Variable**|Select an existing variable, or click \<**New variable...**> to create a new variable. **Related Article:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md), [Add Variable](http://msdn.microsoft.com/library/d09b5d31-433f-4f7c-8c68-9df3a97785d5)|


### Job Configuration
Job configuration specifies U-SQL job submission properties.

- **AzureDataLakeAnalyticsConnection:** Specifies the Azure Data Lake Analytics account where the U-SQL script will be submitted. Choose the connection from a list of defined connection managers. To create a new connection, select <**New connection**>. Related article: [Azure Data Lake Analytics Connection Manager](../../integration-services/connection-manager/azure-data-lake-analytics-connection-manager.md).

- **JobName:** Specifies the name of the U-SQL job. 
- **AnalyticsUnits:** Specifies the Analytics Unit count of the U-SQL job.
- **Priority:** Specifies the Priority of the U-SQL job. Priority can be set from 0 to 1000, the lower the number, the higher the priority.
- **RuntimeVersion:** Specifies the Azure Data Lake Analytics runtime version of the U-SQL job. It is set to "default" by default. Usually you don't need to change this property.
- **Synchronous:** A Boolean value specifies if the task waits the job execution completes or not. Set to Ture, the task will be marked as succeed after job completes. Set to False, the task will be marked as succeed after job passes the Preparation phase.

|Value|Description|
|-----------|-----------------|
|True|The task result is based on the U-SQL job execution result. Job succeeds --> Task succeeds; Job fails --> Task fails; Task succeeds or fails --> Task completes.|
|False|The task result is based on the U-SQL job submission and preparation result. Job submission succeeds and passes the Preparation phase --> Task succeeds; Job submission fails or job fails at the Preparation phase --> Task fails; Task succeeds or fails --> Task completes.|

- **TimeOut:** Specifies a time-out time in seconds for job execution. The job will be cancelled and the task will be marked as failed after the job is time-out. TimeOut property is not available if Synchronous is set to False. TimeOut property is not available if **Synchronous** is set to **false**.

## Parameter Mapping Page Configuration

Use the **Parameter Mapping** page of the **Azure Data Lake Analytics Task Editor** dialog box to map variables to parameters(U-SQL variables) in U-SQL script.

- **Variable Name:** After you have added a parameter mapping by clicking **Add**, select a system or user-defined variable from the list or click \<**New variable...**> to add a new variable by using the **Add Variable** dialog box. **Related Topics:** [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md)  

- **Parameter Name:** Provide a parameter/variable name in U-SQL script. Make sure the the parameter name starts with the \@ sign, like \@Param1. 

Here is an example of how to pass parameters to U-SQL script.

**Sample U-SQL script**
```
@searchlog =
    EXTRACT UserId          int,
            Start           DateTime,
            Region          string,
            Query           string,
            Duration        int,
            Urls            string,
            ClickedUrls     string
    FROM @in
    USING Extractors.Tsv(nullEscape:"#NULL#");

@rs1 =
    SELECT Start, Region, Duration
    FROM @searchlog
WHERE Region == "en-gb";

@rs1 =
    SELECT Start, Region, Duration
    FROM @rs1
    WHERE Start <= DateTime.Parse("2012/02/19");

OUTPUT @rs1   
    TO @out
      USING Outputters.Tsv(quoting:false, dateTimeFormat:null);
```

In above script example, the input and output paths are defined in **\@in** and **\@out** parameters. The values for **\@in** and **\@out** parameters in the U-SQL script are passed dynamically by Parameter Mapping configuration.

|Variable Name|Parameter Name|
|-------------|--------------|
|User: Variable1|\@in|
|User: Variable2|\@out| 

## Expression Page Configuration

All properties in General page configuration can be assigned as a property expression to enable dynamic update of the property at run time. **Related Topics:** [Use Property Expressions in Packages](../../integration-services/expressions/use-property-expressions-in-packages.md)

## See Also
- [Azure Data Lake Analytics Connection Manager](../../integration-services/connection-manager/azure-data-lake-analytics-connection-manager.md)
- [Azure Data Lake Store File System Task](../../integration-services/control-flow/azure-data-lake-store-file-system-task.md)
- [Azure Data Lake Store Connection Manager](../../integration-services/connection-manager/azure-data-lake-store-connection-manager.md)

