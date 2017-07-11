---
title: "Set Up the Job History Log | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "jobs [SQL Server Agent], history"
  - "logs [SQL Server], jobs"
  - "SQL Server Agent jobs, history"
  - "historical information [SQL Server], jobs"
ms.assetid: 018e5c49-d3a0-4504-851a-f70996a34bb7
caps.latest.revision: 4
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Set Up the Job History Log
This topic describes how to set up the [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent job history log.  
  
-   **Before you begin:**  [Security](#Security)  
  
-   **To setup the job history log, using:** [SQL Server Management Studio](#SSMS)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
**To set up the job history log**  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion_md.md)], and then expand that instance.  
  
2.  Right-click **SQL Server Agent**, and then click **Properties**.  
  
3.  In the **SQL Server Agent Properties** dialog box, select the **History** page.  
  
4.  Choose from the following options:  
  
    1.  Check **Limit size of job history log**, and then type the maximum number of rows for the job history log, and the maximum number of rows per job.  
  
    2.  Check **Automatically remove agent history**, and specify a time period, such that history older than this period will be purged from the log.  
  
## See Also  
[Implement Jobs](../../ssms/agent/implement-jobs.md)  
[Monitor Job Activity](../../ssms/agent/monitor-job-activity.md)  
[Create Jobs](../../ssms/agent/create-jobs.md)  
  
