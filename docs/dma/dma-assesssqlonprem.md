---
title: "Perform a SQL Server migration assessment (Data Migration Assistant) | Microsoft Docs"
description: Learn how to use Data Migration Assistant to assess an on-premises SQL Server before migrating to another SQL Server or to Azure SQL Database
ms.custom: ""
ms.date: "03/12/2019"
ms.prod: sql
ms.prod_service: "dma"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Assess"
ms.assetid: ""
author: HJToland3
ms.author: rajpo
manager: craigg
---

# Perform a SQL Server migration assessment with Data Migration Assistant

The following step-by-step instructions help you perform your first assessment for migrating to on-premises SQL Server, SQL Server running on an Azure VM, or Azure SQL Database, by using Data Migration Assistant.

## Create an assessment

1.  Select the **New** (+) icon, and then select the **Assessment** project
    type.

2.  Set the source and target server type.

    If you're upgrading your on-premises SQL Server instance to a modern on-premises SQL Server instance or to SQL Server hosted on an Azure VM, set the source and target server type to **SQL Server**. If you're migrating to Azure SQL Database, instead set the target server type to **Azure SQL Database**.

3.  Click **Create**.

    ![Create an assessment](../dma/media/NewAssessment.png)

## Choose assessment options

1. Select the target SQL Server version that you plan to migrate to.

2. Select the report type.

   When you're assessing your source SQL Server instance for migrating to on-premises SQL Server or to SQL Server hosted on Azure VM targets, you can choose one or both of the following assessment report types:

    -   **Compatibility Issues**
    -   **New features' recommendation**

    ![Select an assessment report type for SQL Server target](../dma/media/AssessmentTypes.png)

   When assessing your source SQL Server instance for migrating to Azure SQL Database, you can choose one or both of the following assessment report types:

    -   **Check database compatibility**
    -   **Check feature parity**

    ![Select assessment report type for SQL Database target](../dma/media/AssessmentTypes_Azure.png)

## Add databases to assess

1.  Select **Add Sources** to open the connection flyout menu.

2.  Enter the SQL server instance name, choose the Authentication type, set the correct connection properties, and then select **Connect**.

3.  Select the databases to assess, and then select **Add**.

    > [!NOTE] 
    > You can remove multiple databases by selecting them while holding the Shift or Ctrl key, and then clicking **Remove Sources**. You can also add databases from multiple SQL Server instances by using the **Add Sources** button.

4.  Click **Next** to start the assessment.

    ![Add sources and start assessment](../dma/media/SelectDatabase.png)

## View results

The duration of the assessment depends on the number of databases added and the schema size of each database. Results are displayed for each database as soon as they're available.

1.  Select the database that has completed the assessment, and then switch between **Compatibility issues** and **Feature recommendations** by using the switcher.

2.  Review the compatibility issues across all compatibility levels supported by the target SQL Server version that you selected on the **Options** page.

You can review compatibility issues by analyzing the affected object, its details, and potentially a fix for every issue identified under **Breaking changes**, **Behavior changes**, and **Deprecated features**.

![View assessment results](../dma/media/ReviewResults.png)

Similarly, you can review feature recommendation across **Performance**, **Storage**, and **Security** areas.

Feature recommendations cover a variety of features such as In-Memory OLTP, Columnstore, Stretch Database, Always Encrypted, Dynamic Data Masking, and Transparent Data Encryption.

![View feature recommendations](../dma/media/FeatureRecommendations.png)

For Azure SQL Database, the assessments provide migration blocking issues and feature parity issues. Review the results for both categories by selecting the specific options.

- The **SQL Server feature parity** category provides a comprehensive set of recommendations, alternative approaches available in Azure, and mitigating steps. It helps you plan this effort in your migration projects.

  ![View information for SQL Server feature parity](../dma/media/SQLFeatureParity.png)

- The **Compatibility issues** category provides partially supported or unsupported features that block migrating on-premises SQL Server databases to Azure SQL databases. It then provides recommendations to help you address those issues.

  ![View compatibility issues](../dma/media/CompatibilityIssues.png)

## Export results

After all databases finish the assessment, select **Export report** to export the results to either a JSON file or a CSV file. You can then analyze the data at your own convenience.

You can run multiple assessments concurrently and view the state of the assessments by opening the **All Assessments** page.
