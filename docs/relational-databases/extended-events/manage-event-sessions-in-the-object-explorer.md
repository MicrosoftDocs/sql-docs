---
title: "Manage Event Sessions in the Object Explorer"
description: You can take actions in Object Explorer that affect Extended Events, such as create, start or stop, export, import, edit, or delete Extended Events sessions.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: xevents
ms.topic: tutorial
ms.assetid: 16849e38-d3fb-414d-8dcb-797b5ffce6ee
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Manage Event Sessions in the Object Explorer

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This topic discusses the actions you can take in **Object Explorer** that affect Extended Events:  
  
-   Create an Extended Events Session  
  
-   Starting or Stopping an Extended Events Session  
  
-   Export an Extended Events Session  
  
-   Import an Extended Events Session Template  
  
-   Edit an Extended Events Session  
  
-   Delete an Extended Events Session  
  
## Create an Extended Events Session  
 For more information about creating an Extended Events session, see [Create an Extended Events Session](/previous-versions/sql/sql-server-2016/hh213147(v=sql.130)).  
  
## Starting or Stopping an Extended Events Session  
 You can start or stop an Extended Events session through the **Query Editor** using the **ALTER EVENT SESSION** statement, or by using the **Extended Events** node of **Object Explorer**.  
  
 When you stop an event session, the session is no longer listed as an active session in the sys.dm_xe_sessions dynamic management view (DMV). However, the session definition remains intact, and you can restart the session. To completely remove a session definition, you must delete the session.  
  
 To start or stop an Extended Events session, you must have the ALTER ANY EVENT SESSION permission.  
  
 When you stop a session that uses an in-memory target, such as the ring buffer, bucketing, event pairing, or synchronous event counter targets, all the information stored in the session's buffer (the target_data column of the sys.dm_xe_session_targets DMV) will be lost. To access event data after you stop the session, you should either save the data before you stop the session, or configure the session to use the file target.  
  
### Start or Stop an Extended Events Session Using Query Editor  
 To start a session, issue the following statements, replacing *session_name* with the name of the Extended Events session:  
  
```  
ALTER EVENT SESSION [session_name]  
ON SERVER  
STATE = START  
```  
  
 To stop a session, issue the following statements, replacing *session_name* with the name of the Extended Events session:  
  
```  
ALTER EVENT SESSION [session_name]  
ON SERVER  
STATE = STOP  
```  
  
### Start or Stop an Extended Events Session in Object Explorer  
 To start or stop an Extended Events session in **Object Explorer**, expand the **Management**, **Extended Events**, and then **Sessions** nodes and right click on a session and then click **Start Session** or **Stop Session**.  
  
## Export an Extended Events Session Template  
 You can export an Extended Events session using **Object Explorer**, and save it as an .xml template file. For example, you may want to export a session and then apply the template to a new event session using the **New Session Wizard** or the **New Session** wizard.  
  
 When you export a session, make sure that you save the template file to a location that uses the NTFS file system, and that you restrict access to users who are authorized to view the information.  
  
 To export an Extended Events session in **Object Explorer**:  
  
1.  Expand the **Management**, **Extended Events**, and then **Sessions** nodes  
  
2.  Right-click the session that you want to export, and select **Export Session**.  
  
3.  In the **Save As** dialog box, select a location to save the file, type the file name in the **File name** box, and then click **Save**.  
  
     If you save the file to the default [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] template location, the template will appear in the dropdown list of predefined templates when you use the **New Session Wizard** and **New Session** dialog.  
  
## Import an Extended Events Session Template  
 Using **Object Explorer**, you can import a template for an Extended Events session. For example, you may want to do this to create a session from a template that was exported from another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 To import an Extended Events session, you must have the necessary **ALTER ANY EVENT SESSION** permissions.  
  
 Before you import a template file, make sure that the file is from a trusted source. Template files should be saved to a location that uses the NTFS file system and where access is restricted to users who are authorized to view the information.  
  
 To import an Extended Events session:  
  
1.  In **Object Explorer**, expand the **Management**, and then **Extended Events** nodes.  
  
2.  Right-click **Sessions** and select **New Session**.  
  
3.  Specify a name for the session.  
  
4.  Expand the **Template** drop down box.  
  
5.  Click **\<File From ...>Open** and browse for the session (XML file) you want to import.  
  
 The session appears under the **Sessions** node. By default, the session is not started.  
  
## Edit an Extended Events Session  
 You can edit an Extended Events session in Object Explorer.  
  
 To edit an Extended Events session:  
  
1.  In **Object Explorer**, expand the **Management**, **Extended Events**, and then **Sessions** nodes.  
  
2.  Right-click a session and select **Properties**.  
  
3.  In the **Select a page** section, select the page or pages you want to edit.  
  
4.  After you finish revising the event session, click **OK**.  
  
## Script an Event Session Definition Using [!INCLUDE[tsql](../../includes/tsql-md.md)]  
 Both the New Session Wizard and the New Session dialog have a Script option that generates the [!INCLUDE[tsql](../../includes/tsql-md.md)] that defines the Extended Events session.  
  
 You can access the [!INCLUDE[tsql](../../includes/tsql-md.md)] for an existing Extended Events session by right clicking the session name, selecting **Script Session as**, and then selecting **Create to**.  
  
## Delete an Extended Events Session  
 You can delete an Extended Events session:  
  
-   In Query Editor using **DROP EVENT SESSION**.  
  
-   In **Object Explorer**.  
  
 When you delete an event session, all configuration information is removed and the session definition no longer appears in the sys.server_event_sessions catalog view.  
  
> [!NOTE]  
>  system_health and Always On_health are included with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; do not delete them. system_health is enabled by default (for more information, see [Use the system_health Session](../../relational-databases/extended-events/use-the-system-health-session.md)). Always On_health is off by default. These sessions collect data that can be useful for diagnosing performance issues.  
  
 To delete an Extended Events session, you must have the ALTER ANY EVENT SESSION permission.  
  
 To delete an Extended Events session in **Object Explorer**:  
  
1.  Expand the **Management**, **Extended Events**, and then **Sessions** nodes.  
  
2.  Right-click a session and select **Delete**.  
  
3.  In the **Delete Object** dialog box, click **OK**.  
  
4.  After you finish revising the event session, click **OK**.  
  
 To delete an Extended Events session in the **Query Editor**, Issue the following statements, replacing *session_name* with the name of the Extended Events session that you want to delete:  
  
```  
DROP EVENT SESSION [session_name]  
ON SERVER  
```  
  
