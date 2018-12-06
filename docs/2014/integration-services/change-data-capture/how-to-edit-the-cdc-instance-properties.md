---
title: "How to Edit the CDC Instance Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 7a6c719a-3735-43b7-b3ab-dfadd325eca2
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# How to Edit the CDC Instance Properties
  This procedure describes how to use the CDC Designer Console to edit the configuration properties for a CDC instance.  
  
### To edit the CDC instance configuration properties  
  
1.  From the **Start** menu, select the **CDC Designer Console**.  
  
2.  In the left pane, expand **Change Data Capture** then expand the service that contains the instance with the properties that you want to edit.  
  
3.  Select the name of the instance where you want to edit the properties.  
  
4.  From the Actions pane on the right side of the CDC Designer Console, click **Properties**.  
  
     You can also right-click the instance where you want to edit the properties and click **Properties**.  
  
5.  In the Properties editor, edit the properties in the following tabs:  
  
    -   **Oracle**: Use the **Oracle** tab in the Properties editor to make changes to the description you provided in the Create CDC database page in the New Instance wizard and to make changes to the Oracle Log Mining database connection information.  
  
         For information about what you can edit in this tab, see [Edit the Oracle Database Properties](edit-the-oracle-database-properties.md).  
  
    -   **Tables**: Use the **Tables** tab to make changes to the tables and columns that you selected from the Oracle source database.  
  
         For information about what you can edit in this tab, see Edit [Edit Tables](edit-tables.md)  
  
    -   **Scripts**: Use the **Scripts** tab to run or re-run a script on the Oracle source database that will set up supplemental logging.  
  
         For information about what you can do in this tab, see [Review and Generate Supplemental Logging Scripts](review-and-generate-supplemental-logging-scripts.md).  
  
    -   **Advanced**: Use the **Advanced** tab to add special properties to the CDC instance.  
  
         For information about what you can do in this tab, see [Edit the Advanced Properties](edit-the-advanced-properties.md).  
  
  
