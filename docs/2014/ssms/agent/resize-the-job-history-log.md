---
title: "Resize the Job History Log | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], history"
  - "resizing job history log"
  - "size [SQL Server], job history log"
  - "logs [SQL Server], jobs"
  - "SQL Server Agent jobs, history"
  - "historical information [SQL Server], jobs"
ms.assetid: ddee1ce8-9d1b-4017-9894-bf7256aed95d
author: stevestein
ms.author: sstein
manager: craigg
---
# Resize the Job History Log
  This topic describes how to set size limits for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job history logs in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To set size limits for job history logs, using:**  
  
     [SQL Server Management Studio](#SSMS)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
 For detailed information, see [Implement SQL Server Agent Security](implement-sql-server-agent-security.md).  
  
##  <a name="SSMS"></a> Using SQL Server Management Studio  
  
#### To resize the job history log based on raw size  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Right-click **SQL Server Agent**, and then click **Properties**.  
  
3.  Select the **History** page, and then confirm that **Limit size of job history log**is checked.  
  
4.  In the **Maximum job history log size** box, enter the maximum number of rows the job history log should allow.  
  
5.  In the **Maximum job history rows per job** box, enter the maximum number of job history rows to allow for a job.  
  
#### To resize the job history log based on time  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Right-click **SQL Server Agent**, and then click **Properties**.  
  
3.  Select the **History** page, and then click **Automatically remove agent history**.  
  
4.  Select the appropriate number of **Days(s)**, **Week(s)**, or **Month(s)**.  
  
  
