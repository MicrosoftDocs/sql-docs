---
description: "Unsuppress Run Custom Report Warnings"
title: "Unsuppress Run Custom Report Warnings"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Management Studio [SQL Server], custom reports"
ms.assetid: 0deed900-c910-4d12-aac0-6ab9e39eb068
author: "markingmyname"
ms.author: "maghan"
---
# Unsuppress Run Custom Report Warnings
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
There are two warning dialog boxes for custom reports. This topic describes how to unsuppress the display of these boxes in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
By default, the **Run Custom Reports** dialog box appears before a custom report runs. If you select the **Please don't show this warning again** check box, the dialog box will no longer appear. Also by default, the **Run Custom Reports** dialog box appears when you open a custom report and then click a link to open another custom report. This dialog box displays the fill path to the drill-through custom report file. If you select the **Please don't show this warning again** check box, the dialog box will no longer appear.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To unsuppress the main custom report warning dialog box  
  
1. Connect to `<server>\<share or drive>\Documents and Settings\<UserProfile>\Application Data\Microsoft\Microsoft SQL Server\130\Tools\Shell\reports.xml`.  
  
2. Right-click **reports.xml**, and then click **Edit**.  
  
3. Change **\<SuppressWarning\>true\<\/SuppressWarning> to \<SuppressWarning\>false\<\/SuppressWarning>**.  
  
4. Restart SQL Server Management Studio.  
  
#### To unsuppress the drill-through custom report warning dialog box  
  
1.  Connect to `<server>\<share or drive>\Documents and Settings\<UserProfile>\Application Data\Microsoft\Microsoft SQL Server\130\Tools\Shell\reports.xml`.  
  
2.  Right-click **reports.xml**, and click **Edit**.  
  
3.  Change **\<SuppressDrillthroughWarning\>true\<\/SuppressDrillthroughWarning>to <\SuppressDrillthroughWarning\>false\<\/SuppressDrillthroughWarning>**.  
  
4.  Restart SQL Server Management Studio.  
  
## See Also  
[Custom Reports in Management Studio](../../ssms/object/custom-reports-in-management-studio.md)  
[Add a Custom Report to Management Studio](../../ssms/object/add-a-custom-report-to-management-studio.md)  
[Use Custom Reports with Object Explorer Node Properties](../../ssms/object/use-custom-reports-with-object-explorer-node-properties.md)  
  
