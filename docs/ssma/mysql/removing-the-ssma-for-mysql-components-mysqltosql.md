---
title: "Removing the SSMA for MySQL Components (MySQLToSql) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Uninstalling, Extension pack"
  - "Uninstalling, SSMA for MySQL client"
ms.assetid: 87cdbd49-a0c9-4b00-8a93-34188b18d11a
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Removing the SSMA for MySQL Components (MySQLToSql)
When you have finished migrating databases from MySQL to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you might want to uninstall SSMA components. You can uninstall the client components at any time. However, if you uninstall the extension pack from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] , then, SSMA will no longer support migration of data from MySQL to the target database (SQL Server/SQL Azure) using the Server-Side Data Migration Engine.  
  
## Uninstalling the SSMA for MySQL Client  
You can uninstall SSMA by using **Add or Remove Programs**.  
  
**To uninstall SSMA**  
  
1.  In Control Panel, open **Add or Remove Programs**.  
  
2.  Select **Microsoft SQL Server Migration Assistant for MySQL**, and then click **Remove**.  
  
3.  To confirm that you want to uninstall SSMA, click **Yes**.  
  
## Uninstalling the Extension Pack  
You can remove the extension pack by using **Add or Remove Programs**.  
  
**To uninstall the extension pack**  
  
1.  In Control Panel, open **Add or Remove Programs**.  
  
2.  Select **Microsoft SQL Server Migration Assistant for MySQL Extension Pack**, and then click **Remove**.  
  
3.  To confirm that you want to uninstall the extension pack, click **Yes**.  
  
4.  On the Instances with Utilities Database Scripts page, select an instance and then click **Next**.  
  
5.  On the Connection Parameters page, select the authentication method, and then click **Next**.  
  
    Windows Authentication will use your Windows credentials to try to log on to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you select [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name and password.  
  
6.  On the Operation Completed page, click **OK**.  
  
7.  On the Finish page, click **Exit**.  
  
After the uninstallation process is completed, you can confirm that objects in the **sysdb.ssma_MySQL** schema, and possibly the whole **sysdb** database, has been removed by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. However, if you use other SSMA products, they also use the **sysdb** database. If the database exists and you are sure that no other databases reference to the objects in this database, you can detach the database.  
  
## See Also  
[Installing SSMA for MySQL Client &#40;MySQLToSQL&#41;](../../ssma/mysql/installing-ssma-for-mysql-client-mysqltosql.md)  
[Installing SSMA Components on SQL Server](installing-ssma-components-on-sql-server-mysqltosql.md)  
  
