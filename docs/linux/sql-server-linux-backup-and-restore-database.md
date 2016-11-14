---
# required metadata

title: Backup and restore SQL Server databases on Linux | SQL Server vNext CTP1
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 11/10/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: d30090fb-889f-466e-b793-5f284fccc4e6

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Backup and restore SQL Server databases on Linux

You can backup a SQL Server database on a Linux server database. 

You can use SQL Server Management Studio from a Windows computer to connect to a Linux database and take a backup through the user-interface.

You can open `sqlcmd` locally on the Linux server, or from another server and connect to the instance of SQL Server on Linux and take the backup.  

[Create a Full Database Backup (SQL Server)](http://msdn.microsoft.com/library/ms187510.aspx)
[BACKUP (Transact-SQL](http://msdn.microsoft.com/library/ms186865.aspx)