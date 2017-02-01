---
title: "Minimizing Log File Space Usage | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "log file space in RDS [ADO]"
ms.assetid: 669662a0-e20f-483e-ab28-53f66c524c98
caps.latest.revision: 14
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Minimizing Log File Space Usage
A log file may fill quickly (thus halting the server) if there is a large volume of activity on an SQL Server database. You can set the log file to **Truncate on Checkpoint** to significantly extend the life of the log file for a database.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
### To enable Truncate on Checkpoint in Microsoft SQL Server 6.5  
  
1.  Start Microsoft SQL Server Enterprise Manager, open the tree for the Server, and then open the Database Devices tree.  
  
2.  Double-click the name of the database on which this feature will be enabled.  
  
3.  From the **Database** tab, select **Truncate**.  
  
4.  From the **Options** tab, select **Truncate Log on Checkpoint**, and then click **OK**.  
  
### To enable Truncate on Checkpoint in Microsoft SQL Server 7.0  
  
1.  Start Microsoft SQL Server Enterprise Manager, open the tree for the Server, and then open the Databases tree.  
  
2.  Right-click the name of the database on which this feature will be enabled and choose **Properties**.  
  
3.  From the **Options** tab, select **Truncate Log on Checkpoint**, and then click **OK**.  
  
 For more information about the **Truncate on Checkpoint** feature, see the Microsoft SQL Server documentation.  
  
## See Also  
 [RDS Fundamentals](../../../ado/guide/remote-data-service/rds-fundamentals.md)


