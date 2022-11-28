---
title: "Maintenance Plan (Reporting and Logging Page)"
description: Maintenance Plan (Reporting and Logging Page)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.maint.reportinglogging.f1"
ms.assetid: 3a30b17a-3deb-446f-900a-62f88934a90f
---
# Maintenance Plan (Reporting and Logging Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Reporting and Logging** dialog box to configure the reports and logs that are generated when maintenance plans are executed.  
  
## Options  
 **Generate a text file report**  
 Specify if you want [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to write a text file report.  
  
 **Create a new file**  
 Create a new report file for each execution of the maintenance plan. By default, the report files are written to the computer hosting the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that contains this maintenance plan, in the folder established as the default log folder during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup. To specify a different folder, enter the full path of the folder in the **Folder** text box, or click the browse button (**...**) and navigate to the desired folder.  
  
 **Append to file**  
 Append the report from each plan execution to the file specified in the **File name** text box. You may also specify a file by clicking the browse button and selecting a file from the dialog box.  
  
 **Send report to an e-mail recipient**  
 Transmit the outcome of a maintenance plan execution via e-mail. This option is only available if Database Mail is enabled and properly configured.  
  
 **Agent operator**  
 Select an agent operator from the list who will be the recipient of the e-mail. This option is only available if mail is enabled and properly  
  
 **Log extended information**  
 Include more information in the log. Including this option will increase the size of the stored maintenance plan history.  
  
 **Log to remote server**  
 Logs maintenance plan history to a remote server.  
  
 **Connection**  
 Specifies the connection information to use when logging to a remote server.  
  
 **New**  
 Displays the **Connection Properties** dialog box. Used to configure new connection information for logging to a remote server.  
  
## See Also  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Database Mail](../../relational-databases/database-mail/database-mail.md)  
  
  
