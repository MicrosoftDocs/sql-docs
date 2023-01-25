---
title: Import Data from Tables
description: Import data from tables and make data changes to a model, in bulk. Use this procedure to add, update, and delete data in the Master Data Services database.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: ad5b83b1-8e40-4ef8-9ba8-4ea17a58b672
author: CordeliaGrey
ms.author: jiwang6
---
# Import Data from Tables (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  You can add data and make data changes to a model in [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], in bulk.  
  
 **Prerequisites**  
  
-   You must have permission to insert data into the stg.\<name>_Leaf, the stg.\<name>_Consolidated, stg.\<name>_Relationship table in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
-   You must have permissions to execute either the stg.udp_\<name>_Leaf, stg.udp\_\<name>_Consolidated, or the stg.udp\_\<name>_Relationship stored procedure in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
-   The model must not have a status of **Committed**.  
  
 **To add, update, and delete data in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database**  
  
1.  Prepare the members for import into the appropriate staging table in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database, including providing values for the required fields. For an overview of staging tables, see [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md)  
  
    -   For leaf members the table is stg.\<name>_Leaf, where \<name> refers to the corresponding entity. For information about the required fields, see [Leaf Member Staging Table &#40;Master Data Services&#41;](../master-data-services/leaf-member-staging-table-master-data-services.md)  
  
    -   For consolidated members, the table is stg.\<name>_Consolidated. For information about the required fields, see [Consolidated Member Staging Table &#40;Master Data Services&#41;](../master-data-services/consolidated-member-staging-table-master-data-services.md).  
  
    -   For moving the location of members in explicit hierarchies, the table is stg.\<name>_Relationship. For information about the required fields, see [Relationship Staging Table &#40;Master Data Services&#41;](../master-data-services/relationship-staging-table-master-data-services.md).  
  
         For an overview on moving members in explicit hierarchies, see [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md).  
  
    -   Use the **ImportType** field value to specify that you're creating new members, deactivating members, or deleting members. For more information about the values, see [Leaf Member Staging Table &#40;Master Data Services&#41;](../master-data-services/leaf-member-staging-table-master-data-services.md) and [Consolidated Member Staging Table &#40;Master Data Services&#41;](../master-data-services/consolidated-member-staging-table-master-data-services.md).  
  
         For an overview of deactivating and deleting members, see [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md).  
  
2.  Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] and connect to the Database Engine instance for your [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
     For more information, see [SQL Server Management Studio](../ssms/sql-server-management-studio-ssms.md).  
  
3.  Import data into the staging tables by using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Import and Export wizard.  
  
     For more information, see [SQL Server Import and Export Wizard](~/integration-services/import-export-data/welcome-to-sql-server-import-and-export-wizard.md).  
  
4.  Load the data from the staging tables to the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] tables, by doing one of the following  
  
    -   Run the staging stored procedure that corresponds to the staging table that you want to move data to.  
  
         For an overview of staging stored procedures and staging tables, see [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md). For more information about parameters for staging stored procedures, and a code example, see [Staging Stored Procedure &#40;Master Data Services&#41;](../master-data-services/staging-stored-procedure-master-data-services.md).  
  
    -   Use the **Integration Management** functional area of Master Data Management.  
  
         On the **Staging Batches** page, select the model to which you're adding data to, in the drop-down list, and then click **Start Batches**. The status of the batch processing is indicated in the **Status** field. For more information about the statuses, see [Import Statuses &#40;Master Data Services&#41;](../master-data-services/import-statuses-master-data-services.md).  
  
         ![Staging Batches Page in Master Data Manager](../master-data-services/media/mds-stagingbatchespage.png "Staging Batches Page in Master Data Manager")  
  
         The staging process  is started at intervals determined by the **Staging batch interval** setting in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)]. For more information, see [System Settings &#40;Master Data Services&#41;](../master-data-services/system-settings-master-data-services.md).  
  
5.  View errors that occurred during staging. For more information, see [View Errors that Occur During Staging &#40;Master Data Services&#41;](../master-data-services/view-errors-that-occur-during-staging-master-data-services.md) and [Staging Process Errors &#40;Master Data Services&#41;](../master-data-services/staging-process-errors-master-data-services.md).  
  
6.  Validate the data against business rules.  
  
     In Master Data Manager, navigate to the **Explorer** functional area for your model, and then apply business rules to validate the data. For more information , see [Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-specific-members-against-business-rules-master-data-services.md). You can also use a stored procedure to validate the data. For more information, see [Validation Stored Procedure &#40;Master Data Services&#41;](../master-data-services/validation-stored-procedure-master-data-services.md).  
  
     When you load data by from the staging tables, the data is not automatically validated against business rules. For more information on what validation is and when it occurs, see [Validation &#40;Master Data Services&#41;](../master-data-services/validation-master-data-services.md).  
  
