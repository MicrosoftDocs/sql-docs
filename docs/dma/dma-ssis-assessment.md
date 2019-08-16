---
title: "Perform a SSIS projects/packages assessment (Data Migration Assistant) | Microsoft Docs"
description: Learn how to use Data Migration Assistant to assess an SQL Server integration services before migrating to the destination Azure Data Factory (ADF) SSIS Integration Runtime with Azure SQL Database or Azure SQL Database managed instance
ms.custom: ""
ms.date: "08/16/2019"
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
ms.author: siyao
---

# Introduction
If you use SQL Server Integration Services (SSIS) and want to migrate your SSIS projects/packages from projects/packages to the destination Azure Data Factory (ADF) SSIS Integration Runtime with Azure SQL Database or Azure SQL Database managed instance, you can select "Integration Service" assessment type in DMA tool to assess source SSIS projects/packages.

The assessment helps to discover issues that can affect the migration from on-premises SQL Server Integration Services to ADF SSIS Integration Runtime. These are described as compatibility issues and are organized in the following categories:

  - Migration blocking issues: Discovers the compatibility issues that block migrating source projects/packages to ADF SSIS Integration Runtime. DMA provides recommendations to help you address those issues.

  - Information issues: Detects partially supported or deprecated features that aare used in source projects/packages. DMA provides a comprehensive set of recommendations, alternative approaches available in Azure, and mitigating steps so that you can incorporate them into your migration projects.
  
# Perform a SSIS packages assessment with Data Migration Assistant

The following step-by-step instructions help you perform your first assessment for the SSIS packages hosted in the file system by using Data Migration Assistant.

## Create an assessment

1. Select the **New** (+) icon, and then select the **Assessment** project type.

2. Select the **Integration services** assessment type

3. Set the source and target server type.

4. Click **Create**.

## Choose assessment options

Select the report type.

   When you're assessing the SSIS packages hosted in the file system:

    - **Compatibility Issues**

## Select sources 

1. Select **Add Sources** to open the connection flyout menu.

2. Enter the SQL server instance name, choose the Authentication type, set the correct connection properties.

3. Enter a folder path that contains SSIS packages and package encryption password if applicable.

4. Click **Connect**

5. Select the sources to assess. Currently only the file system folder is shown in the sources list and it is checked as default, and then select **Add**.

    > [!NOTE]
    > You can remove multiple databases by selecting them while holding the Shift or Ctrl key, and then clicking **Remove Sources**. You can also add databases from multiple SQL Server instances by selecting **Add Sources**.

6. DMA supports to add multiple file system package folders under one selected server.

7. Click **Next** to start the assessment.

## View results

The duration of the assessment depends on the number of databases added and the schema size of each database. Results are displayed for each database as soon as they're available.

## Export results

After all databases finish the assessment, select **Export report** to export the results to either a JSON file or a CSV file. You can then analyze the data at your own convenience.

You can run multiple assessments concurrently and view the state of the assessments by opening the **All Assessments** page.
