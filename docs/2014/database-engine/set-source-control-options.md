---
title: "Set Source Control Options | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Source_Control.Visual_SourceSafe"
  - "VS.ToolsOptionsPages.Source_Control.General"
  - "VS.ToolsOptionsPages.Source_Control.Environment"
helpviewer_keywords: 
  - "source controls [SQL Server Management Studio], options"
ms.assetid: b2c6ca00-46f0-4f86-b067-07bae779c147
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Set Source Control Options
  Before you take advantage of the source control features built into [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], you should configure your source control options for the various environments in which you work.  
  
 You configure source control options by using the **Options** dialog box to configure one or more source control roles. A role consists of a general description of the setting in which you use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], and the source control options associated with that setting.  
  
 For example, if you are an independent database developer, you generally do not create conflicts with other users by keeping files checked out after you check them in. Consequently, [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] defines an Independent Developer role. For this role, [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] automatically selects the **Keep items checked out when checking in** option.  
  
 Because you can define and customize roles, you can work in varying development settings without having to completely reconfigure source control every time you move from one setting to another.  
  
### To set source control options  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  In the **Options** dialog box, expand **Source Control**, and then click the **Plug-in Selection** page.  
  
     **Current source control plug-in**  
     Select the source control you want to use from this list. The source control product client must be installed on this computer to appear on the list. If no source control clients are installed on the computer, the only selection available will be None. If you have installed Microsoft Source Safe, then the following plug ins are displayed:  
  
    -   Microsoft Visual SourceSafe  
  
    -   Microsoft Visual SourceSafe(Internet)  
  
3.  Set login credentials for each source control role that you want to use. This page is only available if a source control plug-in is installed.  
  
     **Role Description**  
     Selecting one of these roles causes the appropriate source control options to be selected automatically.  
  
    |Role|Description|  
    |----------|-----------------|  
    |**Visual SourceSafe**|Specifies that you want to use the settings most commonly used by [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe users.|  
    |**Independent Developer**|Specifies that you are working independently.|  
    |**Custom**|Specifies that you have modified the settings for a role.|  
  
     **Perform background status updates**  
     Automatically updates the source control signal icons in Solution Explorer as an item's status changes. If you experience delays when performing server-intensive operations, especially when opening a solution or project from source control, clearing this check box could improve performance.  
  
     **Login ID**  
     Specifies the user name to use to log in to the source control provider. If supported by your source control provider, this name will be filled in automatically in the **Login** dialog box to reach your source control server. To make this option active, disable automatic user logins by using your source control provider's administrator program, and then restart [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
     **Advanced**  
     Displays additional options for adding items to source control. These options vary depending on your source control provider. Help for these options is provided by the source control program.  
  
4.  Select the **Environment** page.  
  
5.  In the **Source Control Environment Settings** box, select the role on which you want to set source control options.  
  
     [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] automatically selects the default source control options for the role you have selected. If you clear any of the default options, the **Source Control Environment Settings** box displays the **Custom** option to indicate that you have customized the originally selected role.  
  
     **Source Control Environment Settings**  
     Specifies the role that you want to use. [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] defines the following roles.  
  
    |Role|Description|  
    |----------|-----------------|  
    |**Visual SourceSafe**|Specifies that you want to use the settings most commonly used by [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe users.|  
    |**Independent Developer**|Specifies that you are working independently.|  
    |**Custom**|Specifies that you have modified the settings for a role.|  
  
     Selecting one of these roles causes the appropriate source control options to be selected automatically.  
  
     **Keep items checked out when checking in**  
     Specifies that when you check in items to update the source control store, the items should remain checked out to you. If you want to change this option for a specific check-in, click the **Options** arrow in the **Check in** dialog box, and then clear the **Keep checked out** check box.  
  
     **Checked-in items**  
     Displays a list of options that specify how [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] should behave when you attempt to edit an item that is not checked out. The following tables describe the available options.  
  
     **Saving**  
  
    |Action|Description|  
    |------------|-----------------|  
    |**Prompt for check out**|Displays the **Check Out** dialog box.|  
    |**Check out automatically**|Checks out the item without displaying the **Check Out** dialog box. This is the default option.|  
    |**Save as**|Saves as a new file.|  
  
     **Editing**  
  
    |Action|Description|  
    |------------|-----------------|  
    |**Prompt for check out**|Displays the **Check Out** dialog box.|  
    |**Prompt for exclusive checkouts**|Displays the **Check Out** dialog box.|  
    |**Check out automatically**|Checks out the item without displaying the **Check Out** dialog box. This is the default option.|  
    |**Do nothing**|Does not check out the file.|  
  
     **Allow checked-in items to be edited**  
     Specifies that items that are checked in can be edited in memory. If you select this check box, an **Edit** button appears in the **Check Out** dialog box when you attempt to edit a checked-in item. After clicking this button, you can edit the item. If you attempt to save the item, you must check it out or save it to a different location.  
  
     **Reset**  
     Resets source control confirmation dialog boxes to their default settings. For example, if you have selected the **Don't show this dialog again** check box in a source control dialog box, selecting the **Reset** option reverses that action.  
  
## See Also  
 [Source Control Basics](../../2014/database-engine/source-control-basics.md)   
 [Change Source Control Connections](../../2014/database-engine/change-source-control-connections.md)   
 [Exclude Files from Source Control](../../2014/database-engine/exclude-files-from-source-control.md)  
  
  
