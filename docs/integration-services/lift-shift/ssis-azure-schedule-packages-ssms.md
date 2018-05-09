---
title: "Schedule SSIS package execution on Azure SQL Database with SSMS | Microsoft Docs"
description: "Describes how to schedule SSIS packages deployed to Azure SQL Database by using the Schedule command in SQL Server Management Studio (SSMS)."
ms.date: "05/09/2018"
ms.topic: conceptual
ms.prod: sql
ms.prod_service: "integration-services"
ms.component: "lift-shift"
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Schedule the execution of an SSIS package deployed to Azure SQL Database with SQL Server Management Studio (SSMS)

SQL Server Management Studio (SSMS) provides a scheduling feature for SSIS packages deployed to Azure SQL Database. Unlike SQL Server on premises and SQL Database Managed Instance (Preview), which have SQL Server Agent as a first-class job scheduler, SQL Database does not have a built-in scheduler. The SSMS feature described in this article provides a familiar user interface that's similar to SQL Server Agent for scheduling packages deployed to SQL Database.

If you're using SQL Database to host the SSIS catalog database, `SSISDB`, you can use this SSMS feature to generate the Data Factory pipelines, activities, and triggers required to schedule SSIS packages. You can then edit and extend these objects in Data Factory.

When you use SSMS to schedule a package, SSIS automatically creates three new Data Factory objects, with names based on the name of the selected package and the timestamp. For example, if the name of the SSIS package is **MyPackage**, SSMS creates the following new Data Factory objects:
-   A pipeline named **Pipeline_MyPackage_2018-05-08T09_00_00Z**.
-   An Execute SSIS Package activity named **Activity_MyPackage_2018-05-08T09_00_00Z**.
-   A trigger named **Trigger_MyPackage_2018-05-08T09_00_00Z**.

## Prerequisites

The feature described in this article requires SQL Server Management Studio version 17.7 or higher. To get the latest version of SSMS, see [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

## Schedule a package in SSMS

1. In SSMS, in Object Explorer, select the SSISDB database, select a project, and then select a package. Right-click on the package and select **Schedule**.

    ![Select a package to schedule.](media/ssis-azure-schedule-packages-ssms/schedule-ssms-image1-schedule.png)

2. The **New Schedule** dialog box opens. On the **General** page of the **New Schedule** dialog box, provide a name and description for the new scheduled job.

    ![General page of New Schedule dialog box](media/ssis-azure-schedule-packages-ssms/schedule-ssms-image2-new-schedule.png)

3. On the **Package** page of the **New Schedule** dialog box, select optional run-time settings and a run-time environment.

    ![Package page of New Schedule dialog box](media/ssis-azure-schedule-packages-ssms/schedule-ssms-image3-new-schedule2.png)

4. On the **Schedule** page of the **New Schedule** dialog box, provide the schedule settings such as frequency, time of day, and duration.

    ![Schedule page of New Schedule dialog box](media/ssis-azure-schedule-packages-ssms/schedule-ssms-image4-new-schedule3.png)

5. After you finish creating the job in the **New Schedule** dialog box, a confirmation appears to remind you about the new Data Factory objects that SSMS is going to create. If you select **Yes** in confirmation dialog box, the new Data Factory pipeline opens in the Azure portal for you to review and customize.

    ![Confirmation of new schedule](media/ssis-azure-schedule-packages-ssms/schedule-ssms-image5-confirmation.png)

6. To customize the scheduling trigger, select **New/Edit** from the **Trigger** menu.

    ![Optionally edit the new pipeline](media/ssis-azure-schedule-packages-ssms/schedule-ssms-image6-edit.png)

    The **Edit Trigger** blade opens for you to customize the scheduling options.

    ![Edit Trigger](media/ssis-azure-schedule-packages-ssms/schedule-ssms-image7-edit2.png)

## Next steps

To learn about other methods for scheduling an SSIS package, see [Schedule the execution of an SSIS package on Azure](ssis-azure-schedule-packages.md).

To learn more about Azure Data Factory pipelines, activities, and triggers, see the following articles:
    [Pipelines and activities in Azure Data Factory](/azure/data-factory/concepts-pipelines-activities.md)
    [Pipeline execution and triggers in Azure Data Factory](/azure/data-factory/concepts-pipeline-execution-triggers.md)
