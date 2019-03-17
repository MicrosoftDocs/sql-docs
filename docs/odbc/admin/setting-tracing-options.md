---
title: "Setting Tracing Options | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual Studio Analyzer tracing [ODBC]"
  - "ODBC data source administrator [ODBC], tracing options"
  - "tracing options [ODBC], ODBC data source administrator"
ms.assetid: 44404a79-b716-4bc1-9ffb-70cd8239d237
author: MightyPen
ms.author: genemi
manager: craigg
---
# Setting Tracing Options
The **Tracing** tab of the **ODBC Data Source Administrator** dialog box enables you to configure the way ODBC function calls are traced.  
  
## How Tracing Works  
 When you start tracing from the **Tracing** tab, the Driver Manager will log all ODBC function calls for all subsequently run applications. ODBC function calls from applications that are running before tracing is started are not logged. ODBC function calls are recorded in a log file that you specify.  
  
 Tracing stops only after you click **Stop Tracing Now**. Remember that while tracing is on, the log file continues to increase and that this affects the performance of all your ODBC applications.  
  
 For more information about tracing, see [Tracing](../../odbc/reference/develop-app/tracing.md).  
  
### Changes in ODBC tracing  
 Prior to MDAC 2.7 SP2, ODBC tracing was only allowed to occur on a machine-wide basis, in which trace captures exposed details about all ODBC applications running under any identities. This included tracing for ODBC-related activity that might occur for processes created or run on behalf of other local user accounts and built-in security principals such as the Local Service and Network Service.  
  
 By default, ODBC tracing now uses per-user mode. If you are a local administrator, however, you can still enable machine-wide tracing by using the ODBC Data Source Administrator.  
  
 To configure the ODBC tracing mode:  
  
1.  If it is necessary, log on using an account that has membership in the Local Administrators' group.  
  
2.  From Administrative Tools, open the ODBC Data Source Administrator.  
  
3.  Click the **Tracing** tab.  
  
4.  Configure the tracing mode using the **Machine-Wide tracing for all user identities** check box:  
  
5.  To enable machine-wide tracing, select the check box.  
  
6.  To return to per-user tracing, clear the check box.  
  
7.  Click **Apply**.  
  
> [!NOTE]  
>  If you have already started tracing in one mode, you have to stop tracing and switch to the other mode for the mode to be changed successfully.  
  
> [!IMPORTANT]  
>  Machine-wide tracing should only be enabled when it is needed; otherwise, it should be left turned off.  
  
## Visual Studio Analyzer Tracing  
  
> [!IMPORTANT]  
>  Support for Visual Studio Analyzer was removed beginning in Windows 8 (Visual Studio Analyzer was only included in older versions of Visual Studio.). For an alternative troubleshooting mechanism, use BID tracing.  
  
 Visual Studio® Analyzer Tracing provides performance and debugging information about the ODBC layer. All outgoing events will be fired at the top-level interface to present as accurate a picture as possible regarding time spent in ODBC components. Visual Studio Analyzer Tracing requires any event source to register when the source is set up. For more information about this kind of tracing, see the Visual Studio documentation.
