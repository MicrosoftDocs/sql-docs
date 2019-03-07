---
title: "Make Changes to the Tables Selected for Capturing Changes | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "makChanToTab"
ms.assetid: a309f82a-c6ef-464d-a6ef-df555bfb609a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Make Changes to the Tables Selected for Capturing Changes
  In this dialog box, you can select specific columns from the selected table to capture changes from. You can also edit the **Security Role** and **Capture Instance** information.  
  
 You can make the following changes to the tables you selected for capturing changes in this dialog box.  
  
 **Make changes to which columns are included in the CDC instance:**  
  
 Do one or both of the following:  
  
-   Select the check box next to any additional column you want to include.  
  
-   Clear the check box next to any column that you no longer want to include.  
  
 **Change the data type for a specific column**:  
  
 You can select a different data type for a specific column. You can only change the data type to a data type that is compatible with the original data type.  
  
 To change the data type, click in the **Data Type** column and select a different data type. Only data types that are compatible with the original data type are available.  
  
> [!NOTE]  
>  If no additional data types can be selected, the drop-down list is not available.  
  
 **Change the Security Role**  
  
 Type a new name or edit the name of the security gating role in the **Security Role** field.  
  
 **Change or add a capture instance**  
  
 In the **Capture Instance** field enter a name for the capture instance.  
  
## See Also  
 [How to Create the SQL Server Change Database Instance](../../integration-services/change-data-capture/how-to-create-the-sql-server-change-database-instance.md)   
 [Select Oracle Tables and Columns](../../integration-services/change-data-capture/select-oracle-tables-and-columns.md)  
  
  
