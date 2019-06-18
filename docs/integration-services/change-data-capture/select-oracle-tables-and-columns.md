---
title: "Select Oracle Tables and Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "selTabCol"
ms.assetid: bf73f80e-a954-4c5f-874e-17fdd4082715
author: janinezhang
ms.author: janinez
manager: craigg
---
# Select Oracle Tables and Columns

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Use the Select Oracle Tables and Columns page to select the tables from the Oracle source database where changes are captured. This page has the following elements:  
  
## Options  
 **Table list**  
 The table list has three columns:  
  
-   **Oracle Table Name**: The name of the table, including the table schema.  
  
-   **Capture Instance**: The name of the capture instance used to name instance-specific change data capture objects. The capture instance cannot be NULL.  
  
     If not specified, the name is derived from the source schema name plus the source table name in the format `<schema-name>_<table-name>`. The capture instance name cannot exceed 100 characters and must be unique within the database.  
  
     You can click in any cell in this column to manually edit the **capture_instance**.  
  
-   **Security Role**: The name of the database gating role used to control access to change data.  
  
     You can click in any cell in this column to manually edit the **security_role**.  
  
 **Add Tables**  
 Click **Add Tables** to open the Table Selection dialog box where you can [Select Oracle Tables for Capturing Changes](../../integration-services/change-data-capture/select-oracle-tables-for-capturing-changes.md).  
  
 **Edit**  
 Select a table from the list and select **Edit** to open the **Properties** dialog box for that table where you can [Make Changes to the Tables Selected for Capturing Changes](../../integration-services/change-data-capture/make-changes-to-the-tables-selected-for-capturing-changes.md).  
  
 **Remove**  
 Select a table from the list and click **Remove** to remove the table from the CDC instance.  
  
 After you [Select Oracle Tables for Capturing Changes](../../integration-services/change-data-capture/select-oracle-tables-for-capturing-changes.md) and/or [Make Changes to the Tables Selected for Capturing Changes](../../integration-services/change-data-capture/make-changes-to-the-tables-selected-for-capturing-changes.md) using the correct dialog boxes, click **Next** to [Generate and Run the Supplemental Logging Script](../../integration-services/change-data-capture/generate-and-run-the-supplemental-logging-script.md).  
  
## See Also  
 [How to Create the SQL Server Change Database Instance](../../integration-services/change-data-capture/how-to-create-the-sql-server-change-database-instance.md)   
 [Edit Tables](../../integration-services/change-data-capture/edit-tables.md)   
 [Add Tables to a CDC Instance](../../integration-services/change-data-capture/add-tables-to-a-cdc-instance.md)   
 [Edit the Table Properties](../../integration-services/change-data-capture/edit-the-table-properties.md)  
  
  
