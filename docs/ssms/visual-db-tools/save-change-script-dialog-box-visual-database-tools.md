---
description: "Save Change Script Dialog Box (Visual Database Tools)"
title: Save Change Script Dialog Box
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.generatechangescript"
  - "vdtsql.chm:65544"
ms.assetid: fc9d1639-5efa-44fe-a04f-4d4d0def2833
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Save Change Script Dialog Box (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
This dialog box shows the [!INCLUDE[tsql](../../includes/tsql-md.md)] script for the changes you have made since you last saved the table. It also allows you to save the script to a text file at a location you choose.  
  
You can access this dialog box after you have made unsaved changes to a table in Table Designer. On the **Table Designer** menu, click **Generate Change Script**.  
  
> [!NOTE]  
> Change scripts provided by Visual Database Tools contain no error handling. They assume that database objects have not changed since the tool was opened, and that change-related problems will therefore not occur. Before running a change script, you should include the appropriate error-handling statements.  
  
## Options  
**Automatically generate change script on every save**  
If checked, the **Save Change Script** dialog box will appear any time you save changes to a table.  
  
**Yes**  
Bring up the **Save** dialog box where you can choose the location for the text file.  
  
**No**  
Cancel the creation of the change script.  
  
