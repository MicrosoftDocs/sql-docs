---
title: The msdb database
description: "Learn about the msdb database when working with Azure SQL Managed Instance"
author: MilanMSFT
ms.author: mlazic
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom:
---
# The msdb database in Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes the msdb database in Azure SQL Managed Instance. 

## Overview 

In SQL Server, the [msdb database](/sql/relational-databases/databases/msdb-database) is a system database used for a number of system objects and features, such as the SQL Server Agent, Service Broker and Database mail. In Azure SQL Managed Instance, the msdb database is used for backup history and transparency. As such, there are a number of [differences](#differences-with-sql-server) between the msdb database in Azure SQL Managed Instance and the msdb database in SQL Server. 

## Differences with SQL Server

