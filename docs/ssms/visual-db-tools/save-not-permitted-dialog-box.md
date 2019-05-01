---
title: "Save (Not Permitted) Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.table.tablerecreatenosave.f1"
ms.assetid: 7efda8e3-739f-4c97-a497-b8808a0acbea
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Save (Not Permitted) Dialog Box
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
The **Save** (Not Permitted) dialog box warns you that saving changes is not permitted because the changes you have made require the listed tables to be dropped and re-created.  
  
The following actions might require a table to be re-created:  
  
-   Adding a new column to the middle of the table  
  
-   Dropping a column  
  
-   Changing column nullability  
  
-   Changing the order of the columns  
  
-   Changing the data type of a column  
  
To change this option, on the **Tools** menu, click **Options**, expand **Designers**, and then click **Table and Database Designers**. Select or clear the **Prevent saving changes that require the table to be re-created** check box.  
  
