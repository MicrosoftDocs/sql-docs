---
title: "Verify Analysis Services cumulative update build version | Microsoft Docs"
ms.date: 07/11/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---

## Verify Analysis Services cumulative update build version

Beginning with SQL Server 2017, the Analysis Services build version number and SQL Server Database Engine build version number do not match. While both Analysis Services and the Database Engine use the same installer, the build systems each use are separate.

 In some cases, it may be necessary to verify if a Cumulative Update (CU) build package has been applied and Analysis Services components have been updated. You do this by comparing the build version numbers of Analysis Services component files installed on your computer with the  build version numbers for a particular CU.

## Verify component file version

To verify component file version, 

1. Go to [SQL Server 2017 build versions](https://support.microsoft.com/help/4047329). 
2. In **SQL Server 2017 cumulative update (CU) builds**, click the **Knowledge Base Number** for the build you want to verify. 
3. In the **Cumulative Update (#) for SQL Server 2017** article, in the **Cumulative Update package information** section, expand **Cumulative update package file information**. 
4. Check the File version for the following component files: **Msmdpump.dll**, **Msmdredir.dll**, **Msmdsrv.exe**, **Msmgdsrv.dll**.

If the CU has been applied, the file version numbers should match the files installed on your computer.

## See also  

[Install SQL Server Servicing Updates](../../database-engine/install-windows/install-sql-server-servicing-updates.md)  
  
  
